import { Component, OnInit  } from '@angular/core';
import { ActivatedRoute , RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ModuleWiseMenuService } from '../../services/administration/menu/moduleWiseMenu.service';


@Component({
  selector: 'app-administration',
  imports: [CommonModule, RouterOutlet],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class AdministrationComponent  implements OnInit {
  moduleId!: number;
  roleId!: number;
  menuData: any;

  constructor(private route: ActivatedRoute, private menuService: ModuleWiseMenuService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
    
    });
  }
}
