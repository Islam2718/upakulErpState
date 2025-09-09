import { Component, OnInit  } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';
import { ModuleWiseMenuService } from '../../services/administration/menu/moduleWiseMenu.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-hr',
  imports: [CommonModule, RouterOutlet],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class HrDashboardComponent {
  moduleId!: number;
  roleId!: number;
  menuData: any;

  constructor(private route: ActivatedRoute, private menuService: ModuleWiseMenuService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
     
    });
  }

  
}
