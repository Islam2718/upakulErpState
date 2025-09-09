import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { BudgetEntry } from '../../../models/accounts/budget/budget-entry';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';


interface Dropdown {
  text: string;
  value: string;
}



// budget-component.model.ts
export interface BudgetComponent {
  id: number;
  parentId: number;
  componentName: string;
  isMedical: boolean;
  isDesignation: boolean;
}

export interface ApiResponse {
  statusCode: number;
  message: string;
  data: BudgetComponent[];
}


@Injectable({
  providedIn: 'root'
})
export class BudgetEntryService extends BaseService<BudgetEntry>{
  page = 1;
  searchTerm = '';
  sortColumn = '';
  sortDirection = '';
  totalPages = 1;
  pageSize = 10;
  private domain_url: string ="";
  private apiOfficeUrl: string;
  private apiComponentUrl: string;


  selectedComponentId: number =0;
  selectedOfficeId: number =0;
  selectedFinancialYear: string ="";
  tableData: any[] = [];


    constructor(http: HttpClient, private configService: ConfigService) {
      super(http, `${configService.accountApiBaseUrl()}BudgetEntry`);
      this.apiOfficeUrl = `${this.configService.globalApiBaseUrl()}Office`;
      this.apiComponentUrl = `${this.configService.accountApiBaseUrl()}Component`; 
    } 
   

    //  getComponentDropdown(): Observable<Dropdown[]> {
    //       return this.http.get<{ name: string; value: string; data: Dropdown[] }>(`${this.apiComponentUrl}/GetComponentForDropdown`).pipe(
    //           map(response => response.data) // Remove empty value  .filter(item => item.value !== '')
    //       );
    //   }

     getOfficeDropdown(): Observable<Dropdown[]> {
          return this.http.get<{ name: string; value: string; data: Dropdown[] }>(`${this.apiOfficeUrl}/GetOfficeDropdown`).pipe(
              map(response => response.data) // Remove empty value  .filter(item => item.value !== '')
          );
      }
      
      getComponentListByParentId(parentId: number): Observable<ApiResponse> {
          return this.http.get<ApiResponse>(`${this.apiComponentUrl}/GetComponentListByParentId?parentId=${parentId}`);
      }

      // , ComponentId: number
      // &&ComponentId=${ComponentId}  ApiResponse
      getComponentListByParams(financialYear: string, officeId: number, componentParentId: number): Observable<any> {
          return this.http.get<any>(`${this.configService.accountApiBaseUrl()}BudgetEntry/GetBudgetEntryComponent?financialYear=${financialYear}&&officeId=${officeId}&&componentParentId=${componentParentId}`); 
      }

      // saveComponentTableData(data: any): Observable<any> {
      //   return this.http.post('http://localhost:5005/v1/api/Component/SaveComponentData', data);
      // }


  // getDataByParentId(): Observable<Dropdown[]> {
  //       let url = `${this.domain_url}ComponentSetup/GetDataByParentId`; 
       
  //       return this.http.get<{ statusCode: number; message: string; data: Dropdown[] }>(url)
  //         .pipe(
  //           map(response => response.data)
  //         );
  //     }

}
