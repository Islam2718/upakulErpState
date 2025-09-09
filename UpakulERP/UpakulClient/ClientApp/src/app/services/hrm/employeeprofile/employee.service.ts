import { Injectable } from '@angular/core';
import { ConfigService } from '../../../core/config.service';
import { Employee } from '../../../models/hr/employeeprofile/employee';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../../generic/base.service';
import { EmployeeAllDropdown } from '../../../models/hr/employeeprofile/employeeAllDropdown';
import { catchError, map, Observable } from 'rxjs';
import { response } from 'express';
interface DropdownValue {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends BaseService<Employee> {
  private domain_url_global: string;
  private domain_url_project: string;
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.hrmApiBaseUrl()}employee`);
    this.domain_url_global = this.configService.globalApiBaseUrl();
    this.domain_url_project = this.configService.projectApiBaseUrl();
  }

  getEmployeeById(id: number | string): Observable<Employee> {
      return this.getById(id);
    }

  getEmployeeAllDropdown(): Observable<EmployeeAllDropdown> {
    return this.http.get<EmployeeAllDropdown>(`${this.baseUrl}/getalldropdowndata`).pipe(
      map(response => response)
    )
  }
  getBankBranchDropdown(bankid: number): Observable<DropdownValue[]> {
    return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.domain_url_global}BankBranch/GetBranchDropdownXBank?bankid=${bankid}`).pipe(
      map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
    );
  }
  getProjectDropdown(): Observable<DropdownValue[]> {
    return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.domain_url_project}project/getprojectdropDown`).pipe(
      map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
    );
  }
  getGeoLocationDropdown(parentId: number): Observable<DropdownValue[]> {
    return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.domain_url_global}geolocation/GetGeoLocationByParentId?parentId=${parentId}`).pipe(
      map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
    );
  }
  getEmployeeProfileGrid(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder: string = ''
  ): Observable<{ listData: Employee[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }

  addEmployeeProfile(modelData: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}/create`, modelData).pipe(
      //map(response=>response.)
    );

  }

  deleteEmployee(deleteId: number): Observable<any> {
    const deleteCommand = {
      EmployeeId: deleteId,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }
}
