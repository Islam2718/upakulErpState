import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/administration/auth/auth.service';
import { ModuleWiseMenuService } from '../../services/administration/menu/moduleWiseMenu.service';
import { CommonModule, NgFor } from '@angular/common';// ✅ Import this for *ngFor
import { ImageurlMappingConstant } from '../../shared/image-url-mapping-constant';
import { ConfigService } from '../../core/config.service';
import { ImageService } from '../../services/commonService/image.service';

@Component({
  selector: 'app-dashboard',
  //imports: [CommonModule], // ✅ Import CommonModule for directives like *ngFor
  imports: [
    CommonModule
  ],

  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})


export class DashboardComponent implements OnInit {
  modules: any[] = []; // Array to hold modules
  private moduleWiseMenuKey = 'moduleWiseMenu'; // Storage key for menu
  private modulesKey = 'modules'; // Storage key 
  private moduleIdKey = 'moduleId'; // Storage key 
  private roleIdKey = 'roleId'; // Storage key 
  private moduleNameKey = 'moduleName'; // Storage key 
  username: string | null = '';
  userImage: string | null = '/assets/img/thumb/prof.png';
  moduleCount: number = 0;
  private domain_url_hrm: string;
  constructor(private router: Router, private authService: AuthService, private menuService: ModuleWiseMenuService, private configService: ConfigService, private imageService: ImageService) {
    this.domain_url_hrm = configService.hrmApiBaseUrl();
  }
  ngOnInit(): void {
    this.loadModules();
    const userData = localStorage.getItem('personal');
    if (userData) {
      const parsedData = JSON.parse(userData);
      if (parsedData.image_url) {
         //this.imageService.checkImageExists(this.domain_url_hrm.replace("/api/v1", "") + ImageurlMappingConstant.IMG_BASE_URL + parsedData.image_url).then(exists => {
           //if (exists)
            // this.userImage = this.domain_url_hrm.replace("/api/v1", "") + ImageurlMappingConstant.IMG_BASE_URL + parsedData.image_url;
        //});
      }
      this.username = parsedData.emp_name;
    }
    this.authService.setIsMainDashboardTrigger(true); // Dashboard initiated
    this.authService.checkTokenValidity(true).subscribe((isValid) => {
      if (!isValid) {
        this.authService.logout();
        this.router.navigate(['/login']);
      }
    });

  }


  loadModules(): void {
    const modules = this.authService.getModules();
    // Sort modules by display_order in ascending order
    this.modules = modules.sort((a, b) => a.display_order - b.display_order);
    // this.moduleCount = modules.length;
    //console.log('Module Count:', this.modules); // Debugging
  }

  handleModuleClick(moduleName: string, roleId: number, moduleId: number) {
    console.log("dashboard hit")
    this.menuService.getMenuByRoleAndModule(roleId, moduleId).subscribe({
      next: (menuResponse) => {
        
        localStorage.removeItem('Notification');
        localStorage.setItem(this.moduleWiseMenuKey, JSON.stringify(menuResponse.menus)); // persist
        this.menuService.getModuleInfo(); // 
        localStorage.setItem(this.modulesKey, JSON.stringify(menuResponse.modules)); // persist
        localStorage.setItem(this.moduleIdKey, JSON.stringify(moduleId)); // persist
        localStorage.setItem(this.roleIdKey, JSON.stringify(roleId)); // persist
        localStorage.setItem(this.moduleNameKey, JSON.stringify(moduleName)); // persist
        localStorage.setItem('Notification',JSON.stringify(menuResponse.notification));

        if (moduleName.includes('Global Setup')) {
          this.router.navigate(['/gs', moduleId]);
        } else if (moduleName.includes('Accounts')) {
          this.router.navigate(['/ac', moduleId]);
        } else if (moduleName.includes('Micro Finance')) {
          this.router.navigate(['/mf', moduleId]);
        } else if (moduleName.includes('Administration')) {
          this.router.navigate(['/adm', moduleId]);
        } else if (moduleName.includes('Projects')) {
          this.router.navigate(['/pr', moduleId]);
        } else if (moduleName.includes('Human Resources')) {
          this.router.navigate(['/hr', moduleId]);
        }
      },
      error: (err) => {
        console.error('Error fetching menu:', err);
      }
    });
  }

  navigateToGlobalSetup() {
    this.router.navigate(['/gs']);
  }
  navigateToMicrofinance() {
    this.router.navigate(['/mf']);
  }
  navigateToProject() {
    this.router.navigate(['/pr']);
  }
  navigateToAccounts() {
    this.router.navigate(['/ac']);
  }
  navigateToAdministration() {
    this.router.navigate(['/adm']);
  }

  onLogout(): void {
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/login']); // Redirect to login page
      },
      error: (error) => console.error('Logout error:', error)
    });
  }
}
