import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { OfficeComponentMapping } from '../../../models/microfinance/office-component-mapping/office-component-mapping';
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
export class OfficeComponentMappingService extends BaseService <OfficeComponentMapping>{

  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}OfficeComponentMapping`);
  }

  // getComponentDropdown(): Observable<DropdownValue[]> {
  //     return this.http.get<{ name: string; value: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Component/GetComponentDropdown`).pipe(
  //       map(response => response.data) // Remove empty value  .filter(item => item.value !== '')
  //   );
  // }
  
  getBranchDropDown(): Observable<DropdownValue[]> {
    return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Office/GetBranchOfficeDropdown`).pipe(
      map(response => response.data) // Remove empty value .filter(item => item.value !== '')
    );
  }

  addData(modelData: OfficeComponentMapping): Observable<OfficeComponentMapping> {
    return this.create(modelData);
  }

  getByComponentId(id: number): Observable<any> {
    return this.http.get(`${this.configService.mfApiBaseUrl()}OfficeComponentMapping/GetByComponentId`, {
      params: { id: id.toString() }
    });
  }

  
}
