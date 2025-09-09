import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, catchError, map, of, tap } from 'rxjs';
import { Purpose } from '../../../models/microfinance/purpose/purpose';

import { ConfigService } from '../../../core/config.service';
import { PurposeGrid } from '../../../models/microfinance/purpose/purpose-grid';
import { GridRequestModel } from '../../../models/girdRequestModel';
interface Dropdown {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})

export class PurposeServiceTs {
  private domain_url: string;
  constructor(private http: HttpClient, private configService: ConfigService) {
    this.domain_url = this.configService.mfApiBaseUrl();
  }

  add(obj: Purpose): Observable<any> {
    return this.http.post<Purpose>(`${this.domain_url}Purpose/Create`, obj);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${this.domain_url}Purpose/GetById?id=${id}`);
  }

  Update(purposeData: any): Observable<any> {
    // console.log("purposeData" + purposeData);
    return this.http.put<Purpose>(`${this.domain_url}Purpose/Update`, purposeData);
  }

  delete(purposeId: number): Observable<any> {
    const id = purposeId;// parseInt(GroupId, 10); // Ensure it's an integer if needed        
    const deleteCommand = {
      Id: id, // assuming this is how the command expects it
      // Add other required fields for DeleteBankCommand here if necessary
    };
    return this.http.delete(`${this.domain_url}Purpose/Delete`, {
      body: deleteCommand // pass the object in the body
    });
  }

  getMRAPurposeDropdown(category: string, subcategory: string): Observable<Dropdown[]> {
    return this.http.get<{ data: Dropdown[] }>(`${this.domain_url}MRAPurpose/GetMRAPurposeDropdown?category=${category}&subcategory=${subcategory}`).pipe(
      map(response => response.data)
    );
  }

  getMainPurposeDropdown(pid: any = null): Observable<Dropdown[]> {
    return this.http.get<{ data: Dropdown[] }>(`${this.domain_url}Purpose/GetMainPurposeDropdown?pId=${(pid ? pid : 0)}`).pipe(
      map(response => response.data)
    );
  }

  // getChildPurposeDropdown(selectedParentPurpose: any): Observable<Dropdown[]> {
  //   return this.http.get<{ data: Dropdown[] }>(`${this.domain_url}MRAPurpose/GetChildPurposeDropdown`).pipe(
  //     map(response => response.data)
  //   );
  // }

getList(obj: GridRequestModel): Observable<{ purposes: PurposeGrid[], totalRecords: number }> {
  const url = `${this.domain_url}Purpose/GetPurposes`;
  const data = obj;

  return this.http.post<{
    statusCode: number;
    message: string;
    listData: PurposeGrid[];
    totalRecords: number;
  }>(url, data).pipe(
    tap(response => {
      //console.log('Raw API Response:', response.listData);
    }),
    map(response => ({
      purposes: response.listData,
      totalRecords: response.totalRecords
    }))
  ); 
} 




  // searchMainPurpose(searchText: string): Observable<any[]> {
  //   const apiUrl = `${this.domain_url}Purpose/GetMainPurposeDropdown?searchText=${searchText}`;
  //   return this.http.get<any[]>(apiUrl);
  // }

  // searchMainPurposeDropdown(searchText: string) {
  //   return this.http.get<any>(`${this.domain_url}Purpose/GetMainPurposeDropdown?searchText=${searchText}`)
  //     .pipe(
  //       catchError(() => of({ data: [] }))  // Handle error gracefully
  //     );
  // }

}