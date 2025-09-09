import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Department } from '../../../models/hr/department/department';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService extends BaseService<Department> {

  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.hrmApiBaseUrl()}Department`);
  }
  getDepartments(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder: string = ''
  ): Observable<{ listData: Department[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }
  getDepartmentById(id: number | string): Observable<Department> {
    return this.getById(id);
  }
  UpdateDepartment(updateData: Department): Observable<Department> {
    return this.update(updateData);
  }
  deleteDepartment(deleteId: number): Observable<any> {
    const deleteCommand = {
      DepartmentId: deleteId,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }

  addDepartment(modelData: Department): Observable<Department> {
    return this.create(modelData);
  }
}
