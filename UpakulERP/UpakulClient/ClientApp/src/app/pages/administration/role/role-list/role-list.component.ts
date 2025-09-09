import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RoleService } from '../../../../services/administration/role.service'; // Adjust path if needed
import { Role } from '../../../../models/administration/role';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup} from '@angular/forms';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { ToastrService } from 'ngx-toastr';

interface DropdownValues {
  text: string;
  value: string;
}

@Component({
  selector: 'app-role-list',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './role-list.component.html',
  styleUrl: './role-list.component.css'
})
export class RoleListComponent implements OnInit{
  dataList: any[] = [];
  ModuleValues: DropdownValues[] = [];
  ModId: number = 0;
  ModIdParam: number = 0;

  constructor(private fb: FormBuilder, // FormBuilder to easily create the form group
    private router: Router, 
    private http: HttpClient, 
    private toastr:ToastrService,
    private apiService: RoleService) {     
      
    }

    ngOnInit() {
      this.loadDropDownModule();
    }
    
    
    edit(editId: number) {
       console.log('In edit');
       console.log(editId);
       this.ModId = editId;
       this.router.navigate(['/role/edit'], { state: { editId: editId } });
     }
 // Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.apiService.deleteRole(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadDropDownModule(); // Reload list
      },
      error: (err) => { this.toastr.error('Delete failed'); }
    });
    this.deleteIdToConfirm = null; // reset
  }
  delete(id: number) {
    this.deleteIdToConfirm = id;
    this.confirmModal.show(); // ✅ show the modal
  }
     loadDropDownModule() {
      this.apiService.getModuleDropdown().subscribe({
        next: (data) => {
          this.ModuleValues = data;
        },
        error: (err) => {
          console.error('Error fetching dropdown data:', err);
        }
      });
    }

     onDropDownModuleChange(event: any) {
      const selectedValue = event.target.value; 
      console.log('onDropDownModuleChange ' +selectedValue);
      this.ModId = selectedValue;
      this.loadList(selectedValue)
    }

    loadList(moduleId: number) {     
      this.apiService.getRolesByModuleId(moduleId).subscribe({
        next: (data) => this.dataList = data,
        error: (err) => console.error(err)
      });    
    }

    navigateToCreate() {
      this.router.navigate(['adm/role/role']);
    }

    activeLabel: string | null = null;
}
