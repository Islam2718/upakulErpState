import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ConfigService } from '../../core/config.service';
import { User } from '../../models/administration/user';
import { HttpParams } from '@angular/common/http';
import { AnyAaaaRecord } from 'dns';


interface Dropdown {
  text: string;
  value: string;
}

interface Role {
  text: string;
  value: string | null;
  selected: boolean;
}

interface Module {
  moduleId: number;
  moduleName: string;
  isSelected: boolean;
  roles: Role[];
}

interface ApiResponse {
  statusCode: number;
  message: string;
  data: {
    employeeId: number;
    firstName: string;
    lastName: string;
    userId: number;
    userName: string | null;
    userXModule: Module[];
  };
}

@Injectable({
  providedIn: 'root'
})

export class EmployeeRegistrationService {
  private domain_url: string;
  constructor(private http: HttpClient, private configService: ConfigService) {
    this.domain_url = this.configService.authApiBaseUrl();
  }

  getModuleDropdown(): Observable<Dropdown[]> {
    return this.http.get<{ name: string; value: string; data: Dropdown[] }>(`${this.domain_url}Module/GetModuleByDropdown`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value  .filter(item => item.value !== '')
    );
  }

  getEmployeeDropdown(empId?: number): Observable<Dropdown[]> {
    return this.http.get<{ name: string; value: string; data: Dropdown[] }>(`${this.domain_url}user/getEmployeeforDropdown?empId=${empId}`).pipe(
      map(response => response.data) // Remove empty value  .filter(item => item.value !== '')
    );
  }

  saveMenuPermissions(data: any[]): Observable<any> {
    return this.http.post(`${this.domain_url}menu/RoleXMenuCreate`, data);
  }

  saveUserRegistration(payload: any): Observable<any> {
    console.log(payload);
    return this.http.post(`${this.domain_url}user/register`, payload);
  }

  getModulePermission(employeeId: number): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.domain_url}module/GetUserXModule?employeeid=${employeeId}`);
  }

  getUserList(): Observable<any> {
    return this.http.get<any>(`${this.domain_url}user/GetUsersList`).pipe(
      map((res: any) => res.data)
    );
  }

}
