import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Designation } from '../../../models/hr/designation/designation';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

@Injectable({
  providedIn: 'root'
})
export class DesignationService extends BaseService<Designation> {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.hrmApiBaseUrl()}Designation`);
  }
  getDesignations(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder:string=''
  ): Observable<{ listData: Designation[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }
  getDesignationsById(id: number | string): Observable<Designation> {
    return this.getById(id);
  }
  UpdateDesignation(designation: Designation): Observable<Designation> {
    return this.update(designation);
  }
  deleteDesignation(deleteId: number): Observable<any> {
    const deleteCommand = {
      DesignationId: deleteId,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }
  addDesignation(modelData: Designation): Observable<Designation> {
    return this.create(modelData);
  }
}
