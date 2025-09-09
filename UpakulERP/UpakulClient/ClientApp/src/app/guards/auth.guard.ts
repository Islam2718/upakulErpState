import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/administration/auth/auth.service';
import { Observable } from 'rxjs';
import { ModuleWiseMenuService } from '../services/administration/menu/moduleWiseMenu.service';
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private menuService: ModuleWiseMenuService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean>{
     console.log("auth hit")
    if (this.authService.isAuthenticated()) { 
      const { moduleName, moduleId, roleId } = this.menuService.getModuleInfo();
      return new Promise((resolve) => {
        resolve(true);
        // this.menuService.getMenuByRoleAndModule(roleId, moduleId).subscribe({
        //   next: (response) => {
        //     console.log(response)
        //     // You can do something with the response if needed
        //     resolve(true); // Allow route activation
        //   },
        //   error: (err) => {
        //     console.error('Menu fetch error', err);
        //     resolve(false); // Deny route activation on error
        //   }
        // });
      });    
    } else {
      this.router.navigate(['/login']);
       return Promise.resolve(false);
    }
  }
}
