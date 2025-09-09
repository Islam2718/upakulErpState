import {
  Component,
  OnInit,
  ChangeDetectorRef,
} from '@angular/core'; /*Mahfuz used  ChangeDetectorRef*/
import { AuthService } from '../../services/administration/auth/auth.service';
import { CommonModule } from '@angular/common'; //
import { MenuResponse, Menu } from '../../models/administration/menu/menu';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { ModuleWiseMenuService } from '../../services/administration/menu/moduleWiseMenu.service';
import { ImageurlMappingConstant } from '../image-url-mapping-constant';
import { ConfigService } from '../../core/config.service';
import { ImageService } from '../../services/commonService/image.service';
import { FormsModule } from '@angular/forms';
// import model notification
import {
  NotificationService,
  NotificationData,
} from '../../services/notification.service';
import { LPComponent } from '../notification/lp/lp.component';
import { GsComponent } from '../notification/gs/gs.component';
import { EncryptionService } from '../../services/generic/encryption.service';
// state implementation
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import {
  selectStatePersonal,
  selectStateNotification,
  selectAuthState,
} from '../../state/auth.selectors';
import { handleModuleStateFunc } from '../../state/auth.actions';
import { take } from 'rxjs/operators';

export interface NotificationDetails {
  id: number;
  title: string;
  applicationDate: string;
  graceFrom: string | null;
  graceTo: string | null;
  group: string;
  loanType: string;
  notificationType: string;
  office: string;
  orderBy: string | null;
  proposedAmount: number;
}

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, LPComponent, GsComponent], // Import RouterModule for routerLink
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  notifications: NotificationData = { count: 0, summary: [] };

  private moduleWiseMenuKey = 'moduleWiseMenu'; // Storage key for menu
  private modulesKey = 'modules'; // Storage key for modules
  private moduleIdKey = 'moduleId'; // Storage key
  private roleIdKey = 'roleId'; // Storage key
  private moduleNameKey = 'moduleName'; // Storage key
  modules: any[] = []; // Array to hold modules
  moduleId!: number | null;
  roleId!: number | null;
  menuData: any[] = [];
  employeeName: string | null = '';
  userId: string | null = '';
  office: string | null = '';
  userImage: string | null = '/assets/img/no-user.gif';
  refToken: string | null = '';
  isOpen: { [menuId: number]: boolean } = {}; //Add this
  menuList: any[] = [];
  transactionDate: string | null = '';
  selectedIndex: number | null = null;
  toggle(menuId: number): void {
    // this.isOpen[menuId] = !this.isOpen[menuId];
  }

  isExpanded = false;

  toggleMenu(link: HTMLElement, menu: HTMLElement, item: any): void {
    item.isExpanded = !item.isExpanded;
    link.classList.toggle('collapsed', !item.isExpanded);
    link.setAttribute('aria-expanded', String(item.isExpanded));
    menu.classList.toggle('show', item.isExpanded);
  }

  toggleChildMenu(event: Event, child: any): void {
    event.preventDefault(); // Prevent default navigation if href or routerLink present
    child.isExpanded = !child.isExpanded;
  }

  closeNavbar(index: number): void {
    this.selectedIndex = index;
    const navbar = document.querySelector(
      '.navbar-collapse.collapse.show'
    ) as HTMLElement;
    if (navbar) {
      navbar.classList.remove('show');
    }
  }
  private domain_url_hrm: string;

  // state manage
  statePersonal$: Observable<any> | undefined;
  stateNotification$: Observable<any[]> | undefined;
  // store: any;

  constructor(
    private router: Router,
    private authService: AuthService,
    private route: ActivatedRoute,
    private menuService: ModuleWiseMenuService,
    private configService: ConfigService,
    private imageService: ImageService,
    private notificationService: NotificationService,
    private cdr: ChangeDetectorRef,
    private encryptionService: EncryptionService,
    private store: Store
  ) {
    this.domain_url_hrm = configService.hrmApiBaseUrl();
    // state manages code
    this.statePersonal$ = this.store.select(selectStatePersonal);
    this.stateNotification$ = this.store.select(selectStateNotification);
  }
  ngOnInit(): void {
    // this.store.select(selectAuthState).subscribe(s => console.log('_AuthState after login:', s));
    // this.stateNotification$.subscribe(data => console.log('Notification:', data));
    // console.log('__LocalStorage:', localStorage.getItem('Notification'));
    // this.notificationService.notifications$.subscribe(data => {
    //    //console.log('Notification data:', data);
    //   this.notifications = data;
    //   this.cdr.markForCheck();
    // });
    const ntf = localStorage.getItem('Notification');
    if (ntf) {
      this.notifications = JSON.parse(ntf);
      // alert('here...');
    }
    // role wise module
    const userData = localStorage.getItem('personal');
    if (userData) {
      const parsedData = JSON.parse(userData);
      if (parsedData.image_url) {
        this.userImage =
          this.domain_url_hrm.replace('/api/v1', '') +
          ImageurlMappingConstant.IMG_BASE_URL +
          parsedData.image_url;
      }

      parsedData.image_url;
      this.employeeName = `${parsedData.emp_code} - ${parsedData.emp_name}`;
      this.userId = parsedData.userId;
      this.office = parsedData.office_name;
    }
    this.transactionDate = localStorage.getItem('transactionDate');

    // modulewise menu
    this.loadMenuFromLocalStorage();
  }

  navigateToLoanList() {
    this.router.navigate(['mf/loan/loan-proposal-workflow']);
  }

  loadMenuFromLocalStorage(): void {
    const storedMenu = localStorage.getItem(this.moduleWiseMenuKey);
    const storedModules = localStorage.getItem(this.modulesKey);
    try {
      this.menuData = storedMenu
        ? this.buildMenuTree(
            JSON.parse(storedMenu).map((item: any) => ({
              menuId: item.menuId,
              menuText: item.menuText,
              url: item.url,
              queryParams: item.url
                ? {
                    qry: `${encodeURIComponent(
                      this.encryptionService.encryptUrlParm({
                        isViewMenu: item.isViewMenu,
                        isView: item.isView,
                        isAdd: item.isAdd,
                        isEdit: item.isEdit,
                        isDelete: item.isDelete,
                      })
                    )}`,
                  }
                : {},
              parentId: item.parentId,
              iconCss: item.iconCss,
            }))
          )
        : [];
      this.modules = storedModules ? JSON.parse(storedModules) : [];
    } catch (err) {
      console.error('Error parsing menu data:', err);
      this.menuData = [];
    }
  }

  buildMenuTree(menuList: Menu[]): Menu[] {
    const menuMap = new Map<number, Menu>();
    const roots: Menu[] = [];

    // Step 1: Initialize the map with all items
    for (const item of menuList) {
      menuMap.set(item.menuId, { ...item, children: [] });
    }

    // Step 2: Populate children and determine roots
    for (const item of menuList) {
      const current = menuMap.get(item.menuId)!;
      if (item.parentId !== null) {
        const parent = menuMap.get(item.parentId);
        if (parent) {
          parent.children!.push(current);
        }
      } else {
        roots.push(current);
      }
    }
    return roots;
  }

  // getAuthMethod(moduleName: string, roleId: number, moduleId: number){
  //   alert(moduleName);
  // }

  // handleModuleClick(
  //   moduleName: string,
  //   roleId: number,
  //   moduleId: number,
  //   menuResponse?: any
  // ) {
  //   // Dispatch the action to update state
  //   this.store.dispatch(
  //     handleModuleStateFunc({
  //       moduleName,
  //       roleId,
  //       moduleId,
  //       menus: menuResponse?.menus,
  //       modules: menuResponse?.modules,
  //       notifications: menuResponse?.notification,
  //     })
  //   );

  //   // Subscribe to store just once
  //   this.store
  //     .select(selectAuthState)
  //     .pipe(take(1))
  //     .subscribe((s) => {
  //       console.log('_NavTS:', s);
  //     });
  // }

  handleModuleClick(moduleName: string, roleId: number, moduleId: number) {
    console.log("nav hit")
    this.menuService.getMenuByRoleAndModule(roleId, moduleId).subscribe({
      next: (menuResponse) => {
        localStorage.setItem(
          this.moduleWiseMenuKey,
          JSON.stringify(menuResponse.menus)
        ); // persist
        this.menuService.getModuleInfo(); // <-- Store info
        localStorage.setItem(
          this.modulesKey,
          JSON.stringify(menuResponse.modules)
        ); // persist
        localStorage.setItem(this.moduleIdKey, JSON.stringify(moduleId)); // persist
        localStorage.setItem(this.roleIdKey, JSON.stringify(roleId)); // persist
        localStorage.setItem(this.moduleNameKey, JSON.stringify(moduleName)); // persist
        localStorage.removeItem('Notification');
        if (menuResponse.notification) {
          this.notifications = menuResponse.notification;
          localStorage.setItem(
            'Notification',
            JSON.stringify(menuResponse.notification)
          );
        }

        this.loadMenuFromLocalStorage(); // Refresh menuData

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
      },
    });
  }

  notificationDetails: any;
  notificationDetailsFunc(notificationObj: any) {
    //console.log(':noDe:__', notificationObj);
    this.notificationDetails = notificationObj;
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
  navigateToHome() {
    const { moduleName, moduleId } = this.menuService.getModuleInfo();
    //alert('navigate to home')
    if (moduleName && moduleId) {
      this.navigateByModule(moduleName, moduleId);
    } else {
      console.warn('Module info not available. Redirecting to default home.');
    }
  }
  private navigateByModule(moduleName: string, moduleId: number) {
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
  }
  navigate(menu: any) {
    if (menu.startsWith('http')) {
      window.open(menu, '_blank');
    } else {
      this.router.navigate([menu]);
    }
  }
  onLogout(): void {
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/login']); // Redirect to login page
      },
      error: (error) => console.error('Logout error:', error),
    });
  }
}
