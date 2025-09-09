import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { BudgetComponent } from '../../../models/accounts/budget/budget-component';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';


interface DropdownValue {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})


export class BudgetComponentService extends BaseService<BudgetComponent>{

   constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.accountApiBaseUrl()}Component`);
  }

    /**
   * Get paginated, searchable, and sortable list of countries.
   */
   getDataList(
      page: number,
      pageSize: number,
      searchTerm: string = '',
      sortColumn: string = 'id',
      sortDirection: string = 'asc'
    ): Observable<{ listData: BudgetComponent[]; totalRecords: number }> {
      return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
    }

    

  getDropDownComponent(id:number=0): Observable<DropdownValue[]> {
          const url=`${this.configService.accountApiBaseUrl()}Component/GetComponentForDropdown?parentId=`;
          return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${url}`+id).pipe(
              map(response => response.data) // Remove empty value .filter(item => item.value !== '')
          );
   }

  getDataById(id: number | string): Observable<BudgetComponent> {
    return this.getById(id);
  }

  addData(obj: BudgetComponent): Observable<BudgetComponent> {
    return this.create(obj);
  }

  updateData(obj: BudgetComponent): Observable<BudgetComponent> {
    return this.update(obj);
  }

  deleteData(id: number): Observable<any> {
    console.log("in Delete Service", id);
    const deleteCommand = {
      Id: id,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }


}
