import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Office } from '../../../models/Global/office/office';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

interface OfficeType {
  text: string;
  value: string;
}

interface PrincipalType {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})

export class OfficeService extends BaseService <Office>{
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.globalApiBaseUrl()}Office`);
  }

  GetOffices(
       page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder:string=''
    ): Observable<{ listData: Office[]; totalRecords: number }> {
      return this.getList(page, pageSize, searchTerm, sortOrder);
    }

  GetOffice(officeId: string): Observable<any> {
      const url = `${this.configService.globalApiBaseUrl()}Office/GetById?id=${officeId}`;
      return this.http.get<any>(url).pipe(
        map((res: any) => res.data)
      );
    }

  getOfficeTypes(): Observable<OfficeType[]> {
    return this.http.get<{ name: string; value: string; data:OfficeType[] }>(`${this.configService.globalApiBaseUrl()}CommonDropDown/LoadOfficeType`).pipe(
      map(response => response.data.filter(item => item.value !== '')) 
    );
  }

  getOfficeByParentId(parentId?: number): Observable<PrincipalType[]> {
    let url = `${this.configService.globalApiBaseUrl()}Office/GetOfficeByParentId`;
    // Append the query parameter only if parentId is provided
    if (parentId !== undefined && parentId !== null) {
      url += `?parentId=${parentId}`;
    }
    return this.http.get<{ statusCode: number; message: string; data: PrincipalType[] }>(url)
      .pipe(
        map(response => response.data)
      );
  }

  getData(getId: string): Observable<any> {
    const url =  `${this.configService.globalApiBaseUrl()}Office/GetById?id=${getId}`;
    return this.http.get<any>(url).pipe(
      map((res: any) => res.data)
    );
  }

  getOfficeById(id: number | string): Observable<Office> {
    return this.getById(id);
  }

  addOffice(office: Office): Observable<Office> {
    return this.create(office);
  }

  Update(updateData: Office): Observable<Office> {
    return this.update(updateData);
  }
  
  deleteData(deleteId: number): Observable<any> {
    const deleteCommand = {
      OfficeId: deleteId,
   
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }
  
}
  