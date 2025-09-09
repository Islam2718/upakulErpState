import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { BaseService } from '../../../generic/base.service';
import { ConfigService } from '../../../../core/config.service';
import { componentMF } from '../../../../models/microfinance/components/componentMf/componentMF.model';

interface Dropdown {
  text: string;
  value: string | number;
}
interface ComponentDropdown {
  text: string;
  value: string;
}
@Injectable({
  providedIn: 'root'
})

export class ComponentMFService extends BaseService<componentMF> {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}Component`);
  }

  getComponentMFs(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'name',
    sortDirection: string = 'asc'
  ): Observable<{ listData: componentMF[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }
  getComponentTypeDropdown(): Observable<ComponentDropdown[]> {
    return this.http.get<{ name: string; value: string; data: ComponentDropdown[] }>(`${this.configService.mfApiBaseUrl()}CommonDropDown/LoadComponent`).pipe(
      map(response => response.data)) // .filter(item => item.value !== '' Remove empty value
  }


  getLoanComponentDropdown(): Observable<ComponentDropdown[]> {
    //const url = `${this.configService.mfApiBaseUrl()}group/`;
    return this.getListData('getLoanComponentDropdown', {}).pipe(
      map(response => {
        return response as ComponentDropdown[];
      })
    );
  }
  getGeneralLoanComponentDropdown(): Observable<ComponentDropdown[]> {
    return this.getListData('getGeneralLoanComponentDropdown', {}).pipe(
      map(response => {
        return response as ComponentDropdown[];
      })
    );
  }
  getProjectLoanComponentDropdown(): Observable<ComponentDropdown[]> {
    return this.getListData('getProjectLoanComponentDropdown', {}).pipe(
      map(response => {
        return response as ComponentDropdown[];
      })
    );
  }

  getSavingComponentDropdown(): Observable<ComponentDropdown[]> {
    return this.getListData('getSavingComponentDropdown', {}).pipe(
      map(response => {
        return response as ComponentDropdown[];
      })
    );
  }

  getDPSComponentDropdown(): Observable<ComponentDropdown[]> {
    return this.getListData('getDPSComponentDropdown', {}).pipe(
      map(response => {
        return response as ComponentDropdown[];
      })
    );
  }

  getFDRComponentDropdown(): Observable<ComponentDropdown[]> {
    return this.getListData('getFDRComponentDropdown', {}).pipe(
      map(response => {
        return response as ComponentDropdown[];
      })
    );
  }


  getComponentMFById(id: number | string): Observable<componentMF> {
    return this.getById(id);
  }
  getMasterComponentDropdown(): Observable<Dropdown[]> {
    return this.http.get<{ name: string; value: string; data: Dropdown[] }>(`${this.configService.mfApiBaseUrl()}masterComponent/GetMasterComponentForDropdown`).pipe(
      map(response => response.data) // Remove empty value
    );

  }

  getPaymentFequencyDropdown(): Observable<ComponentDropdown[]> {
    return this.http.get<{ name: string; value: string; data: ComponentDropdown[] }>(`${this.configService.mfApiBaseUrl()}CommonDropDown/LoadPeriodicPayment`).pipe(
      map(response => response.data)) // .filter(item => item.value !== '' Remove empty value
  }

  addData(componentmf: componentMF): Observable<componentMF> {
    return this.create(componentmf);
  }

  updateData(componentmf: componentMF): Observable<componentMF> {
    return this.update(componentmf);
  }

  deleteData(id: number): Observable<any> {
    const deleteCommand = {
      Id: id
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }

}