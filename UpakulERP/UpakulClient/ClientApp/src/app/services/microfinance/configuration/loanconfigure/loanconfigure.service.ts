import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,map } from 'rxjs';
import { BaseService } from '../../../generic/base.service';
import { ConfigService } from '../../../../core/config.service';
import { Loanconfiguration } from '../../../../models/microfinance/configuration/loanconfiguration/loanconfiguration.model';
@Injectable({
  providedIn: 'root'
})
export class LoanconfigureService  extends BaseService<Loanconfiguration>{
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}Loanconfiguration`);
}

  getLoanconfigurations(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'name',
    sortDirection: string = 'asc'
  ): Observable<{ listData: Loanconfiguration[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }
  
  getLoanconfigurationById(id: number | string): Observable<Loanconfiguration> {
    return this.getById(id);
  }

  addCodegenerator(loadconfiguration: Loanconfiguration): Observable<Loanconfiguration> {
    return this.create(loadconfiguration);
  }

  updateCodegenerator(loadconfiguration: Loanconfiguration): Observable<Loanconfiguration> {
    return this.update(loadconfiguration);
  }

  deleteCodegenerator(id: number): Observable<any> {
    const deleteCommand = {
      Id: id,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }

}