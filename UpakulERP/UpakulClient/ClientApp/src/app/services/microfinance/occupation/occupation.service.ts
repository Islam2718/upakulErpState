import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Occupation } from '../../../models/microfinance/occupation/occupation';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';


@Injectable({
  providedIn: 'root'
})

export class OccupationService extends BaseService <Occupation>  {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}Occupation`);
  }

getDataList(
      page: number,
      pageSize: number,
      searchTerm: string = '',
      sortColumn: string = 'OccupationName',
      sortDirection: string = 'asc'
    ): Observable<{ listData: Occupation[]; totalRecords: number }> {
      return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  getDataById(id: number | string): Observable<Occupation> {
    return this.getById(id);
  }

  addData(modelData: Occupation): Observable<Occupation> {
    return this.create(modelData);
  }

  UpdateData(data: Occupation): Observable<Occupation> {
   return this.update(data);
  }

  deleteData(dataId: number): Observable<any> {
     const deleteCommand = {
        OccupationId: dataId,
        isActive: false
      };
      return this.delete(deleteCommand); // calling BaseService's method
  }

}
