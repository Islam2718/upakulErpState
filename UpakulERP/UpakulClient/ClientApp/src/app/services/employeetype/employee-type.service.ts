import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { EmployeeType } from '../../models/hr/emp-type/employee-type';
import { ConfigService } from '../../core/config.service';

interface EmployeeTypeDropdown {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})
export class EmployeeTypeService {
  private domain_url: string;
  constructor(private http: HttpClient,private configService: ConfigService) {
    this.domain_url = this.configService.globalApiBaseUrl();
  }

    getEmployeeTypeDropdown(): Observable<EmployeeTypeDropdown[]> {
      return this.http.get<{ name: string; value: string; data: EmployeeTypeDropdown[] }>(`${this.domain_url}CommonDropDown/LoadEmployeeType`).pipe(
        map(response => response.data.filter(item => item.value !== '')) // Remove empty value
      );
    }


  addEmployeeType(modelData: EmployeeType): Observable<any> {
    return this.http.post<EmployeeType>(`${this.domain_url}EmployeeType/Create`, modelData);
  }

}
