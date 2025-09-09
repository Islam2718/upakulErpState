import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { LoanProposal } from '../../../../models/microfinance/loan-proposal/loan-proposal';
import { LoanApplication } from '../../../../models/microfinance/loan-workflow/loan-application';
import { ConfigService } from '../../../../core/config.service';
import { BaseService } from '../../../generic/base.service';
import { Member } from '../../../../models/microfinance/member/member';

interface ApiResponse<T> {
  statusCode: number;
  message: string;
  data: T;
}

interface DropdownValue {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})


export class LoanWorkflowService extends BaseService <LoanApplication>{
   constructor(http: HttpClient, private configService: ConfigService) {
      super(http, `${configService.mfApiBaseUrl()}LoanProposal`);
    }

    getGroupDropDownData(): Observable<DropdownValue[]> {
      return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Group/GetGroupForDropdown`).pipe(
          map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
    }

    getMemberDropDownData(): Observable<DropdownValue[]> {
      return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Member/GetMemberForDropdown`).pipe(
          map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
    }

    getProposedByDropDownData(): Observable<DropdownValue[]> {
      return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.authApiBaseUrl()}User/GetEmployeeforDropdown`).pipe(
          map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
    }
    
    // getComponentDropDownData(): Observable<DropdownValue[]> {
    //   return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Component/GetComponentDropdown`).pipe(
    //       map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
    //   );
    // }
    
    getMainPurposeDropdown(): Observable<DropdownValue[]> {
      return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Purpose/GetMainPurposeDropdown`).pipe(
          map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
    }

    getDataById(id: number | string): Observable<LoanApplication> {
      return this.getById(id);
    }
 
    getMemberById(id: number | string): Observable<ApiResponse<Member>> {
      return this.http.get<ApiResponse<Member>>(`${this.configService.mfApiBaseUrl()}member/GetById?id=${id}`); //.pipe(
    }

    UpdateData(data: LoanApplication): Observable<LoanApplication> {
      return this.update(data);
    }


}
