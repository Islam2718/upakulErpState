import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Role } from '../../../models/administration/role';
import { ConfigService } from '../../../core/config.service';
import { HttpParams } from '@angular/common/http';


interface Dropdown {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})



export class RoleConfigService {

  private domain_url: string;

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.domain_url = this.configService.authApiBaseUrl();
  }
  
      getModuleDropdown(): Observable<Dropdown[]> {
         return this.http.get<{ name: string; value: string; data: Dropdown[] }>(`${this.domain_url}Module/GetModuleByDropdown`).pipe(
            map(response => response.data) // Remove empty value  .filter(item => item.value !== '')
          );
      }
      
      getRolesByModuleIdDropdown(moduleId: number) {
        return this.http.get<{ data: any[] }>(`${this.domain_url}Role/GetRoleByModuleIdDropdown`, {
          params: { moduleId: moduleId }
        }).pipe(
          map(res => res.data)
        );
      }

      getMenuPermission(moduleId: number, roleId: number): Observable<any[]> {
        return this.http.get<{data: any[]}>(`${this.domain_url}menu/GetMenuPermission`, {
          params: { moduleId: moduleId, roleId:roleId }
        }).pipe(
          map(res => res.data)
        );
      }
      
      saveMenuPermissions(data: any[]): Observable<any> {
        return this.http.post(`${this.domain_url}menu/RoleXMenuCreate`, data);
      }
      
}
