import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { LoanApprovalModel } from '../../../../models/microfinance/loan-approval/loan-approval-model';
import { ConfigService } from '../../../../core/config.service';
import { BaseService } from '../../../generic/base.service';

interface DropdownValue {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})

export class LoanApprovalService extends BaseService <LoanApprovalModel>{
  
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}LoanApproval`);
  }

  getDesignationDropDownData(): Observable<DropdownValue[]> {
    return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}employee/designationDropdown`).pipe(
        map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
    );
  }
  
 getAll(): Observable<LoanApprovalModel[]> {
    return this.http.get<LoanApprovalModel[]>(`${this.baseUrl}/GetAll`);
  }

  // CreateData(data: LoanApproval[]): Observable<LoanApproval[]> {
  //   return this.create(data[]);
  // }
  CreateData(approvals: LoanApprovalModel[]) {
    return this.http.post(`${this.baseUrl}/Create`, approvals);
  }

  deleteData(id: number): Observable<any> {
    const deleteCommand = {
            Lavel: id
      };
   return this.delete(deleteCommand); // calling BaseService's method
  }


}