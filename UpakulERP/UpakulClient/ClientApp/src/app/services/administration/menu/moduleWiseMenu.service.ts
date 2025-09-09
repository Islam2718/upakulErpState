import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, BehaviorSubject, tap } from 'rxjs';
import { Menu, MenuResponse } from '../../../models/administration/menu/menu';
import { ConfigService } from '../../../core/config.service';

@Injectable({
  providedIn: 'root'
})
export class ModuleWiseMenuService {
  private tokenKey = 'authToken'; // Storage key for token
  private transactionDate = 'transactionDate'; // Storage key for transactionDate
  private moduleNameKey = 'moduleName'; // Storage key 
  private moduleIdKey = 'moduleId'; // Storage key 
  private roleIdKey = 'roleId'; // Storage key 
  private moduleIdSubject = new BehaviorSubject<number | null>(null);
  private roleIdSubject = new BehaviorSubject<number | null>(null);
  private selectedModuleInfo = {
    moduleId: 0,
    moduleName: '',
    roleId: 0
  };
  moduleId$ = this.moduleIdSubject.asObservable();
  roleId$ = this.roleIdSubject.asObservable();

  constructor(private http: HttpClient, private configService: ConfigService) {
  }

  getMenuByRoleAndModule(roleId: number, moduleId: number): Observable<MenuResponse> { //alert('hi i am being called ')
    
    //console.log('roleid: ' + roleId + ' moduleiddd:' + moduleId)
    const url = `${this.configService.authApiBaseUrl()}account/RefreshTokenWithModuleMenu`;
    return this.http
      .get<{ statusCode: number; message: string; data: MenuResponse }>(`${url}?roleId=${roleId}&moduleId=${moduleId}`)
      .pipe(
        tap(response => {      
          // Replace the token only if a new token is present
          if (response.data?.token) {
            this.setTransactionDate(response.data.transactionDate);
            this.setToken(response.data.token);
          }
        }), // Logs the raw HTTP response
        map(response => response.data),
        tap(data =>data /*console.log('Extracted MenuResponse:', )*/)     // Logs just the menu data
      );
  }

  // Store token in localStorage
  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

   setTransactionDate(transDate: string): void {
    localStorage.setItem(this.transactionDate, transDate);
  }

  getModuleInfo() {
    // console.log("getModuleInfo");
    const moduleName = localStorage.getItem(this.moduleNameKey) || ''; // persist
    const moduleId = Number(localStorage.getItem(this.moduleIdKey)) || 0; // persist
    const roleId = Number(localStorage.getItem(this.roleIdKey)) || 0; // persist
    return this.selectedModuleInfo = { moduleName, moduleId, roleId };
  }
}