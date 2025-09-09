import { Component, OnInit  } from '@angular/core';
import { ActivatedRoute , RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-administration',
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class AccountDashboardsComponent  implements OnInit {
  moduleId!: number;
  roleId!: number;
  menuData: any;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      
    });
  }
}
