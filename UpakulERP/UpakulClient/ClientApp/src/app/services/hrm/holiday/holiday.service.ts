import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { tap } from 'rxjs/operators';

import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';
import { Holiday } from '../../../models/hr/holiday/holiday.model';

@Injectable({
  providedIn: 'root'
})
export class HoliDayService extends BaseService <Holiday>  {

  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.hrmApiBaseUrl()}HoliDay`);
  }
  getHoliDays(
    page: number,
    pageSize: number,
    searchTerm: string = '',
     sortOrder:string=''
  ): Observable<{ listData: Holiday[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }
   getHoliDayById(id: number | string): Observable<Holiday> {
        return this.getById(id);
      }
 UpdateData(updateData: Holiday): Observable<Holiday> {
      return this.update(updateData);
      }
addData(modelData: Holiday): Observable<Holiday> {
             return this.create(modelData);
          }
 deleteData(deleteId: number): Observable<any> {
      const deleteCommand = {
        holiDayId: deleteId,
        isActive: false
      };
      return this.delete(deleteCommand); // calling BaseService's method
    }  
}
  