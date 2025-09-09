import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ConfigService } from '../../../core/config.service';
import { EmployeeStatus } from '../../../models/hr/emp-status/employee-status';



interface EmployeeStatusDropdown {
  text: string;
  value: string;
}


@Injectable({
  providedIn: 'root'
})
export class EmployeeStatusService {

  constructor(private http: HttpClient, private configService: ConfigService) {}

  getEmployeeStatusDropdown(): Observable<EmployeeStatusDropdown[]> {
        return this.http.get<{ name: string; value: string; data: EmployeeStatusDropdown[] }>(`${this.configService.globalApiBaseUrl()}CommonDropDown/LoadEmployeeStatus`).pipe(
          map(response => response.data.filter(item => item.value !== '')) // Remove empty value
        );
      }
  

  addEmployeeStatus(modelData: EmployeeStatus): Observable<any> {
    return this.http.post<EmployeeStatus>(`${this.configService.hrmApiBaseUrl()}EmployeeStatus/Create`, modelData);
  }

}
