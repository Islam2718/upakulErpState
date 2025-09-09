import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Role } from '../../models/administration/role';
import { ConfigService } from '../../core/config.service';
import { HttpParams } from '@angular/common/http';
import { BaseService } from '../generic/base.service';

interface Dropdown {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})


export class RoleService extends BaseService <Role>  {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.authApiBaseUrl()}Role`);
  }
  getModuleDropdown(): Observable<Dropdown[]> {
    return this.http.get<{ name: string; value: string; data: Dropdown[] }>(`${this.configService.authApiBaseUrl()}Module/GetModuleByDropdown`).pipe(
      map(response => response.data) // Remove empty value  .filter(item => item.value !== '')
    );
  }
    getRoleById(id: number | string): Observable<Role> {
          return this.getById(id);
        }
  getData(getId: string): Observable<any> {
    const url = `${this.configService.authApiBaseUrl()}Role/GetById?id=${getId}`;
    return this.http.get<any>(url).pipe(
      map((res: any) => res.data)
    );
  }

  LoadList(getId: string): Observable<any> {
    const url = `${this.configService.authApiBaseUrl()}Role/LoadList?id=${getId}`;
    console.log(url);
    return this.http.get<any>(url).pipe(
      map((res: any) => res.data)
    );
  }

  getRolesByModuleId(moduleId: number) {
    return this.http.get<{ data: any[] }>(`${this.configService.authApiBaseUrl()}Role/LoadList`, {
      params: { moduleId: moduleId }
    }).pipe(
      map(res => res.data)
    );
  }
  addRole(role: Role): Observable<Role> {
    
               return this.create(role);
            }
  UpdateRole(role: Role): Observable<Role> {
        return this.update(role);
        }
   deleteRole(id: number): Observable<any> {
  return this.http.delete(`${this.configService.authApiBaseUrl()}Role/Delete?id=${id}`);
}  
}





