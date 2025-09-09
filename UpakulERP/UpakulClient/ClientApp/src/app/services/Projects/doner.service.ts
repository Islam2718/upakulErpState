import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Doner } from '../../models/projects/doner';
import { ConfigService } from '../../core/config.service';
import { BaseService } from '../generic/base.service';


interface Dropdown {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})

export class DonerService extends BaseService<Doner> {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.projectApiBaseUrl()}Doner`);
  }

  getDoners(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'name',
    sortDirection: string = 'asc'
  ): Observable<{ listData: Doner[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  getDonerById(id: number | string): Observable<Doner> {
    return this.getById(id);
  }

  addDoner(obj: Doner): Observable<Doner> {
    return this.create(obj);
  }

  updateData(donerData: Doner): Observable<Doner> {
    return this.update(donerData);
  }

  deleteData(donerId: number): Observable<any> {
    const deleteCommand = {
      DonerId: donerId,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }
  
  getCountryDropdown(): Observable<Dropdown[]> {
    return this.http.get<{ data: Dropdown[] }>(`${this.configService.projectApiBaseUrl()}Country/GetCountryForDropdown`).pipe(
      map(response => response.data.filter(item => item.value !== ''))
    );
  }
}
