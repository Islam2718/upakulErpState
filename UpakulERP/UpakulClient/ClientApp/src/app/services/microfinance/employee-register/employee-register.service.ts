import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { EmployeeRegister } from '../../../models/microfinance/employee-register/employee-register';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';


interface DropdownValue {
  text: string;
  value: string;
  selected:boolean;
}

@Injectable({
  providedIn: 'root'
})



  export class EmployeeRegisterService extends BaseService <EmployeeRegister>  {
    constructor(http: HttpClient, private configService: ConfigService) {
      super(http, `${configService.mfApiBaseUrl()}EmployeeRegister`);
    }

      getEmployeeForDropDownData(): Observable<DropdownValue[]> {
        return this.http.get<{ value: number; text: string; selected:boolean; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}employee/getEmployeeforDropdown`).pipe(
          map(response => response.data) // Remove empty value .filter(item => item.value !== '') 
        );
      }

      getAvaliableGroupForDropdownData(): Observable<DropdownValue[]> {
        return this.http.get<{ value: number; text: string; selected:boolean; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}employeeregister/getavaliablegroup`).pipe(
          map(response => response.data) // Remove empty value .filter(item => item.value !== '') 
        );
      }

      getGroupByEmployeeIdForDropdownData(id: number): Observable<DropdownValue[]> {
         const url = `${this.configService.mfApiBaseUrl()}employeeregister/GetGroupByEmployeeIdDropdown?employeeId=`;
         return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${url}` + id).pipe(
           map(response => response.data.filter(item => item.value !== '')) // Remove empty value .filter(item => item.value !== '')
         );
      }

      getDataList(
            page: number,
            pageSize: number,
            searchTerm: string = '',
            sortColumn: string = 'id',
            sortDirection: string = 'asc'
          ): Observable<{ listData: EmployeeRegister[]; totalRecords: number }> {
            return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
      }

      addData(data: any): Observable<any> {
        return this.http.post(`${this.configService.mfApiBaseUrl()}EmployeeRegister/Create`, data);
      }

      release(data: any): Observable<any> {
        return this.http.put(`${this.configService.mfApiBaseUrl()}EmployeeRegister/Release`, data);
      }

      employeeRegisterAllDropdown(): Observable<DropdownValue[]> {
      const url = `${this.configService.mfApiBaseUrl()}EmployeeRegister/GetAllGrpWiseEmployeeDropDownData`;
      return this.http.get<{ data: [] }>(url).pipe(
        map(response => response.data) // Remove empty value .filter(item => item.value !== '')
      );
  }



}
