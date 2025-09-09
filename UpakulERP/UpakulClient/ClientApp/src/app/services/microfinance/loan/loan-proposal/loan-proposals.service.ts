import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { LoanProposal } from '../../../../models/microfinance/loan-proposal/loan-proposal';
import { LoanApplication } from '../../../../models/microfinance/loan-workflow/loan-application';
import { ConfigService } from '../../../../core/config.service';
import { BaseService } from '../../../generic/base.service';
import { Member } from '../../../../models/microfinance/member/member';
import { LoanApprovalModel } from '../../../../models/microfinance/loan-approval/loan-approval-model';
import { LoanProposalPut } from '../../../../models/microfinance/loan-proposal/loan-proposal-put';

interface ApiResponse<T> {
  statusCode: number;
  message: string;
  data: T;
}

interface DropdownValue {
  selected: unknown;
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root',
})
export class LoanProposalsService extends BaseService<LoanProposal> {
  [x: string]: any;

  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}LoanProposal`);
  }

   getProposedByDropDownData(): Observable<DropdownValue[]> {
    return this.http
      .get<{ value: number; text: string; data: DropdownValue[] }>(
        `${this.configService.mfApiBaseUrl()}employee/getEmployeeforDropdown`
      )
      .pipe(
        map((response) => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
  }

  // getGroupDropDownData(): Observable<DropdownValue[]> {
  //   return this.http
  //     .get<{ value: number; text: string; data: DropdownValue[] }>(
  //       `${this.configService.mfApiBaseUrl()}group/GetGroupForDropdown`
  //     )
  //     .pipe(
  //       map((response) => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
  //     );
  // }

  // getMemberDropDownData(): Observable<DropdownValue[]> {
  //   return this.http
  //     .get<{ value: number; text: string; data: DropdownValue[] }>(
  //       `${this.configService.mfApiBaseUrl()}member/getGroupXMemberDropdown`
  //     )
  //     .pipe(
  //       map((response) => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
  //     );
  // }

 

  // getComponentDropDownData(): Observable<DropdownValue[]> {
  //   return this.http
  //     .get<{ value: number; text: string; data: DropdownValue[] }>(
  //       `${this.configService.mfApiBaseUrl()}Component/GetComponentDropdown`
  //     )
  //     .pipe(
  //       map((response) => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
  //     );
  // }

  getMainPurposeDropdown(): Observable<DropdownValue[]> {
    return this.http
      .get<{ value: number; text: string; data: DropdownValue[] }>(
        `${this.configService.mfApiBaseUrl()}Purpose/GetMainPurposeDropdown`
      )
      .pipe(
        map((response) => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
  }

  getAll(): Observable<LoanProposal[]> {
    return this.http.get<LoanProposal[]>(`${this.baseUrl}/GetAll`);
  }

  getDataById(id: number | string): Observable<LoanProposal> {
    return this.getById(id);
  }

  getMemberById(id: number | string): Observable<ApiResponse<Member>> {
    return this.http.get<ApiResponse<Member>>(
      `${this.configService.mfApiBaseUrl()}member/GetById?id=${id}`
    ); //.pipe(
    //map((res: any) => res.data),
    //catchError(this.handleError)
    //);
  }

  // getMemberDetailByIdService(
  //   id: number | string
  // ): Observable<ApiResponse<Member>> {
  //   return this.http.get<ApiResponse<Member>>(
  //     `${this.configService.mfApiBaseUrl()}member/GetMemberDetailById?id=${id}`
  //   );
  // }

  //  addData(modelData: LoanProposal): Observable<LoanProposal> {
  //   //  console.log("in AddData");
  //    return this.create(modelData);
  //  }

  // addData(payload: LoanProposal): Observable<any> {
  //   // plain JSON POST
  //   return this.http.post(`${this.baseUrl}/create`, payload);
  // }

  addData(payload: FormData): Observable<any> {
    //const url = `${this.baseUrl}/create`; // backend endpoint
    //return this.http.post(url, payload); // content-type automatically multipart/form-data

    return this.createFromData(payload);
  }

  updateData(payload: FormData): Observable<any> {
    // const url = `${this.baseUrl}/update`; // backend update endpoint
    // return this.http.put(url, payload); // or post if your API uses POST
    return this.updateFromData(payload)
  }

  updateLoanWorkFlow(payload:any): Observable<any> {
    // const url = `${this.baseUrl}/loanApprovalFlow`;
    // return this.http.put(url, payload);
    return this.update(payload, 'loanApprovalFlow')
  }
  // updateLoanWorkFlow(
  //   payload: LoanApplication | LoanProposalPut
  // ): Observable<any> {
  //   const url = `${this.baseUrl}/loanApprovalFlow`;
  //   return this.http.put(url, payload);
  // }

  deleteData(dataId: number): Observable<any> {
    const deleteCommand = {
      LoanApplicationId: dataId,
      isActive: false,
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }

  getDataList(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'ApplicationDate',
    sortDirection: string = 'asc'
  ): Observable<{ listData: LoanProposal[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  //LoanApplicationWorkflow
  getDataLoanWorkflowList(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder: string,
    sortColumn: string = 'ApplicationDate',
    sortDirection: string = 'asc'
  ): Observable<{ listData: LoanApplication[]; totalRecords: number }> {
    const url = `${this.baseUrl}/LoadGridForLoanApprove?page=${page}&pageSize=${pageSize}&search=${searchTerm}&sortColumn=${sortColumn}&sortDirection=${sortDirection}&sortOrder=${sortOrder}`;
    return this.http
      .get<{ listData: LoanApplication[]; totalRecords: number }>(url)
      .pipe(
        map((res) => ({
          listData: res.listData,
          totalRecords: res.totalRecords,
        }))
      );
    // return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  /** PUT method for loan approval */
  approveLoan(payload: LoanApprovalModel): Observable<any> {
    const url = `${this.baseUrl}/loanApprovalFlow`;
    return this.http.put(url, payload);
    // return this.update(payload, 'loanApprovalFlow');
  }
}
