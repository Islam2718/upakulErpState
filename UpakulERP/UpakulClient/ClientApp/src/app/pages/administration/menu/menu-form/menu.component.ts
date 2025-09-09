import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';  // Adjust path if needed
import { Message } from '../../../../shared/enums/message.enum';
import { MenuService } from '../../../../services/administration/menu/menu.service';  // Adjust path if needed
import { Menu } from '../../../../models/administration/menu/menu.model';  // Adjust path if needed
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';

interface MenuModule {
  text: string;
  value: string;
}

interface ParentMenuEntry {
  text: string;
  value: string;
}
interface MenuApiResponse {
  statusCode: number;
  message: string;
  data: Menu[];  // Ensure the 'data' is of type 'Menu[]'
}

@Component({
  selector: 'app-menu',
  standalone: true,  // Allow the component to be used without a module
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  menuForm!: FormGroup;
  isSubmitting = false;
  modules: MenuModule[] = [];

  showSubMenu = false;
  button = Button;  // Access Button Text Enum
  message = Message;

  constructor(
    private fb: FormBuilder,
    private menuService: MenuService,  // Adjust the service path
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.menuForm = this.fb.group({
      ModuleId: ['', Validators.required],
      ParentId: [''],
      MenuText: ['', Validators.required],
      ParentUrl: [''],
      ParentComponent: [''],
      ChildUrl: [''],
      ChildComponent: [''],
      IconCss: [''],

    });
    this.loadModules();
  }



  loadModules() {
    // Assuming the service fetches modules data from a backend
    this.menuService.getModules().subscribe({
      next: (data) => {
        this.modules = data;
      },
      error: (err) => {
        //console.error('Error fetching modules:', err);
      }
    });
  }


  parentMenuEntries$: Observable<ParentMenuEntry[]> = of([]); // Initialize with empty observable

  onModuleChange(event: any): void {
    // const selectedModule = event.target.value;
    const selectedModule = parseInt(event.target.value, 10);
    //console.log('Selected Module:', selectedModule);
    this.parentMenuEntries$ = this.menuService.getParentMenuEntries(selectedModule);
    //console.log(this.parentMenuEntries$)
  }


  onSubmit() {

    if (this.menuForm.valid) {
      //console.log(this.menuForm.value);

      this.isSubmitting = true;
      const newMenu: Menu = this.menuForm.value;

      this.menuService.addMenu(newMenu).subscribe({
        next: (response) => {
          //  debugger
          // console.log('Menu added successfully:', response);
          this.toastr.success('menu' + this.message.SaveMsg, 'Success');
          this.menuForm.reset();
        },
        error: (err) => {
          //   debugger
          //  console.error('menu:', err);
          this.toastr.error(this.message.SaveErr, 'menu Adding Failed');
        },
        complete: () => {
          this.isSubmitting = false;
        }
      });
    }
  }
}
