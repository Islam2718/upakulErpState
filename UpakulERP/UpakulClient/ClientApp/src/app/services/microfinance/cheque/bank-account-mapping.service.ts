import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { BankAccountMapping } from '../../../models/microfinance/cheque/bank-account-mapping';
import { BankAccountChequeIDetails } from '../../../models/microfinance/cheque/bank-account-cheque-details';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

interface DropdownValue {
  text: string;
  value: string;
  selected: boolean;
}

@Injectable({
  providedIn: 'root'
})

export class BankAccountMappingService extends BaseService<BankAccountMapping> {

  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}BankAccountMapping`);
  }

  /**
      * Get paginated, searchable, and sortable list of countries.
      */
    getDataList(
      page: number,
      pageSize: number,
      searchTerm: string = '',
      sortColumn: string = 'accountNumber',
      sortDirection: string = 'asc'
    ): Observable<{ listData: BankAccountMapping[]; totalRecords: number }> {
      return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
    }

   getChequeDetailsList(
    mappingId : number = 0,
    page: number,
    pageSize: number,
    searchTerm: string,
    sortOrder: string,
    sortColumn?: string,
    sortDirection?: string,    
   ): Observable<{ listData: BankAccountChequeIDetails[]; totalRecords: number }> {
    const url = `${this.baseUrl}/LoadChequeDetailsGrid?page=${page}&pageSize=${pageSize}&search=${searchTerm}&sortColumn=${sortColumn}&sortDirection=${sortDirection}&sortOrder=${sortOrder}&mappingId=${mappingId}`;
    return this.http.get<{ listData: BankAccountChequeIDetails[]; totalRecords: number }>(url).pipe(
      map(res => ({
        listData: res.listData,
        totalRecords: res.totalRecords
      })),
      
    );
  }

  
    getDataById(id: number | string): Observable<BankAccountMapping> {
      return this.getById(id);
    }
  
    addData(data: BankAccountMapping): Observable<BankAccountMapping> {
      return this.create(data);
    }
  
    updateData(data: BankAccountMapping): Observable<BankAccountMapping> {
      return this.update(data);
    }
  
    deleteData(id: number): Observable<any> {
      const deleteCommand = {
        bankAccountMappingId: id
      };
      return this.delete(deleteCommand); // calling BaseService's method
    }

    getOfficeDropdown(): Observable<DropdownValue[]> {
      return this.http.get<{ name: string; value: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Office/GetOfficeDropdown`).pipe(
        map(response => response.data) // Remove empty value  .filter(item => item.value !== '') GetAllEmployeeforDropdown?empId=0
      );
    }
  
    getBankDropdown(): Observable<DropdownValue[]> {
      return this.http.get<{ name: string; value: string; data: DropdownValue[] }>(`${this.configService.globalApiBaseUrl()}Bank/getbanksfordropdown`).pipe(
        map(response => response.data) // Remove empty value  .filter(item => item.value !== '') GetAllEmployeeforDropdown?empId=0
      );
    }
  
    getBankBranchDropDownData(id?: number): Observable<DropdownValue[]> {
      return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.globalApiBaseUrl()}BankBranch/GetBranchDropdownXBank?bankid=`+ id).pipe(
        map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
    }

    createCheque(data: any) {
        console.log('in service:', data);
        return this.http.post<any>(`${this.configService.mfApiBaseUrl()}BankAccountMapping/CreateCheque`, data); // adjust base URL as needed
    }


    officeBankAssignDropdown(): Observable<DropdownValue[]> {
    // let params = new HttpParams()
    //   .set('groupId', groupId);

      // const url = `${this.configService.mfApiBaseUrl()}GroupCommittee/GetGroupXMemberDropdown`;
      // return this.http.get<{ data: [] }>(url, { params }).pipe(
      //   map(response => response.data) // Remove empty value .filter(item => item.value !== '')
      // );

      const url = `${this.configService.mfApiBaseUrl()}BankAccountMapping/GetOfficeBankAssignDropdownData`;
      return this.http.get<{ data: [] }>(url).pipe(
        map(response => response.data) // Remove empty value .filter(item => item.value !== '')
      );
  }



}
