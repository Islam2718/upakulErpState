import { Component, computed, OnDestroy, signal } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NavComponent } from './shared/nav/nav.component';
import { switchMap, takeUntil, timer, Subject } from 'rxjs';
import { AuthService } from '../app/services/administration/auth/auth.service'
import { ModuleWiseMenuService } from '../app/services/administration/menu/moduleWiseMenu.service';
import $ from 'jquery';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, RouterModule, NavComponent],
  standalone: true,
  // template: `<router-outlet></router-outlet>`,  // This will load LoginComponent via routing
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnDestroy {
  destroy$ = new Subject<void>();
  IsMainDashboard: boolean = false;
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  showNavbar = signal(true);
  title = 'ClientApp';

  constructor(private router: Router, private authService: AuthService, private menuService: ModuleWiseMenuService) {
    this.router.events.subscribe(() => {
      const hiddenRoutes = ['/login', '/dashboard']; // Routes where NavComponent should be hidden
      this.showNavbar.set(!hiddenRoutes.includes(this.router.url));
    });
  }
  ngOnInit(): void {
    this.startTokenValidationTimer();
    console.log('___localStorage:',localStorage);
  }

  startTokenValidationTimer(): void {
    timer(0, 930000) // 0 delay, then run every 15 min 30 sec
    // timer(0, 10000) // 0 delay, then run every 15 min 30 sec
      .pipe(
        // switchMap(() => this.authService.checkTokenValidity(this.IsMainDashboard)),
        switchMap(() => {
          return this.authService.getIsMainDashboardTrigger(); // Get the latest flag
        }),
        switchMap((isMainDashboard: boolean) => {
          return this.authService.checkTokenValidity(isMainDashboard);
        }),
        takeUntil(this.destroy$)
      )
      .subscribe((isValid: boolean) => {
        if (!isValid) {
          this.authService.logout(); // Clear localStorage and navigate to login
          this.router.navigate(['/login']);
        }
      });
  }
}
