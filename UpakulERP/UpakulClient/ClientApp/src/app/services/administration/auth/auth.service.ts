import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ConfigService } from '../../../core/config.service';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
// Define an interface matching the actual response structure
interface LoginResponse {
  data: {
    token: string;
    personal: {
      userId: string;
      employeeId: number;
      emp_code: string;
      emp_name: string;
      office_name: string | null;
      email: string;
      image_url: string;
      module_role_id: number;
    };
    modules: {
      module_name: string;
      secend_div_class: string;
      icon_class: string;
      title: string;
      url: string;
      display_order: number;
    }[];
    menus: any; // If menus exist, define its structure or use `any`
  };
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private dashboardTriggeredCheck = new BehaviorSubject<boolean>(false);

  setIsMainDashboardTrigger(value: boolean): void {
    this.dashboardTriggeredCheck.next(value);
  }

  getIsMainDashboardTrigger(): Observable<boolean> {
    return this.dashboardTriggeredCheck.asObservable();
  }
  private tokenKey = 'authToken'; // Storage key for token
  private userKey = 'personal'; // Storage key for self user
  private moduleKey = 'modules';
  private moduleWiseMenuKey = 'moduleWiseMenu'; // Storage key for menu
  private moduleIdKey = 'moduleId'; // Storage key 
  private roleIdKey = 'roleId'; // Storage key 
  private moduleNameKey = 'moduleName'; // Storage key 

  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(private http: HttpClient, private router: Router, private configService: ConfigService) {
    this.currentUserSubject = new BehaviorSubject<any>(this.getToken());
    this.currentUser = this.currentUserSubject.asObservable();
  }

  // Login method
  login(credentials: { UserId: string; Password: string }): Observable<any> {
    // console.log('🔍 Debug - Sending Login Request:', credentials); // Log data
    const url = `${this.configService.authApiBaseUrl()}account/login`;
    return this.http.post<LoginResponse>(url, credentials, {
      headers: { 'Content-Type': 'application/json; charset=utf-8' }  // Ensure JSON format
    }).pipe(
      map(response => {
        if (response.data.token) {
          this.setToken(response.data.token);
          localStorage.setItem('modules', JSON.stringify(response.data.modules)); // Store modules
          localStorage.setItem('personal', JSON.stringify(response.data.personal));
          //  console.log(response)
        }
        return response;
      })
    );
  }

  // Method to get stored modules
  getModules(): any[] {
    const modules = localStorage.getItem('modules');
    return modules ? JSON.parse(modules) : [];
  }

  // Store token in localStorage
  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    this.currentUserSubject.next(token);
  }

  // Get token
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  // Check if user is logged in
  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getModuleId(): number | null {
    const storedModuleId = localStorage.getItem('moduleId');
    return storedModuleId ? Number(storedModuleId) : null;
  }

  getRoleId(): number | null {
    const storedRoleId = localStorage.getItem('roleId');
    return storedRoleId ? Number(storedRoleId) : null;
  }

  // Logout user
  logout(): Observable<void> {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
    localStorage.removeItem(this.moduleKey);
    localStorage.removeItem(this.moduleIdKey);
    localStorage.removeItem(this.roleIdKey);
    localStorage.removeItem(this.moduleNameKey);

    localStorage.clear();
    this.currentUserSubject.next(null);
    return of(); // Return an observable
  }

  checkTokenValidity(IsMainDashBoard: boolean): Observable<boolean> {
    // alert('from dashboard compp '+ IsMainDashBoard)
    if (IsMainDashBoard) {
      IsMainDashBoard = true;
    } else {
      IsMainDashBoard = false;
    }
    const token = this.getToken();
    if (!token) return of(false);

    const url = `${this.configService.authApiBaseUrl()}account/IsTokenValid`;
    const payload = {
      token: token,
      moduleId: this.getModuleId(),   // or null/undefined if needed
      roleId: this.getRoleId(),
      IsMainDashBoard: IsMainDashBoard
    };

    return this.http.post<boolean>(url, payload).pipe(
      catchError(() => of(false))
    );
  }
}