import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { BoardUniversity } from '../../../models/hr/university/board-university';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';


@Injectable({
  providedIn: 'root'
})


export class BoardUniversityService extends BaseService<BoardUniversity> {

  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.hrmApiBaseUrl()}BoardUniversity`);
  }
  getBoardUniversitys(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder: string = ''
  ): Observable<{ listData: BoardUniversity[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }

  getBoardUniversityById(id: number | string): Observable<BoardUniversity> {
    return this.getById(id);
  }

  updateData(updateData: BoardUniversity): Observable<BoardUniversity> {
    return this.update(updateData);
  }
  addBoartUniversity(modelData: BoardUniversity): Observable<BoardUniversity> {
    return this.create(modelData);
  }
  deleteData(deleteId: number): Observable<any> {
    const deleteCommand = {
      BUId: deleteId,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }
}
