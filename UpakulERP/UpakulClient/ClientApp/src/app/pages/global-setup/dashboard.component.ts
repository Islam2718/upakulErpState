import { Component, OnInit  } from '@angular/core';
import { ActivatedRoute , RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ModuleWiseMenuService } from '../../services/administration/menu/moduleWiseMenu.service';

@Component({
  selector: 'app-global-setup',
  imports: [CommonModule, RouterOutlet],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class GlobalDashboardComponent  implements OnInit {
  moduleId!: number;
  roleId!: number;
  menuData: any;

  constructor(private route: ActivatedRoute, private menuService: ModuleWiseMenuService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.moduleId = Number(params.get('moduleId'));
      this.roleId = Number(params.get('roleId'));
      // if (this.roleId && this.moduleId) {
      //   this.loadMenuData();
      // }
    });
  }

  // loadMenuData() {
  //   this.menuService.getMenuByRoleAndModule(this.roleId, this.moduleId).subscribe({
  //     next: (data) => {
  //       this.menuData = data;
  //       //console.log('Menu Data:', this.menuData);
  //     },
  //     error: (err) => console.error('Error fetching menu data:', err)
  //   });
  // }
}
