import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import  {MasterComponent} from '../../../../models/microfinance/components/masterComponents/masterComponent';
import { Observable,map } from 'rxjs';
import { BaseService } from '../../../generic/base.service';
import { ConfigService } from '../../../../core/config.service';


@Injectable({
  providedIn: 'root'
})
export class MasterComponentService extends BaseService<MasterComponent> {
  constructor(http: HttpClient, configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}MasterComponent`);
}

  getMasterComponents(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'name',
    sortDirection: string = 'asc'
  ): Observable<{ listData: MasterComponent[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  getMasterComponentById(id: number | string): Observable<MasterComponent> {
    return this.getById(id);
  }


  addMasterComponent(masterComponent: MasterComponent): Observable<MasterComponent> {
    return this.create(masterComponent);
  }

  updateMasterComponent(masterComponent: MasterComponent): Observable<MasterComponent> {
    return this.update(masterComponent);
  }

  deleteMasterComponent(id: number): Observable<any> {
    const deleteCommand = {
      Id: id,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }

}