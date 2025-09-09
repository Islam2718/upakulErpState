import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Bank } from '../../../models/Global/bank/bank';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

interface BankType {
  text: string;
  value: string;
}
interface BankTypeDropdown {
  text: string;
  value: string;
}

interface PrincipalType {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})
export class BankService extends BaseService <Bank> {
  
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.globalApiBaseUrl()}Bank`);
  }
  getBanks(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder:string=''
  ): Observable<{ listData: Bank[]; totalRecords: number }> {
    
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }
  getBankByParentId(parentId?: number): Observable<PrincipalType[]> {
    let url =`${this.configService.globalApiBaseUrl()}Bank/GetBankByParentId`;

    // Append the query parameter only if parentId is provided
    if (parentId !== undefined && parentId !== null) {
      url += `?parentId=${parentId}`;
    }
    return this.http.get<{ statusCode: number; message: string; data: PrincipalType[] }>(url)
      .pipe(
        map(response => response.data)
      );
  }
  getBankTypeDropdown(): Observable<BankTypeDropdown[]> {
    return this.http.get<{ name: string; value: string; data: BankTypeDropdown[] }>(`${this.configService.globalApiBaseUrl()}CommonDropDown/LoadBankType`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value
);
}
  getBankById(id: number | string): Observable<Bank> {
      return this.getById(id);
    }
  
  updateBank(bank: Bank): Observable<Bank> {
      return this.update(bank);
    }
    
  deleteBank(id: number): Observable<any> {
      const deleteCommand = {
        bankId: id,
      };
      return this.delete(deleteCommand); // calling BaseService's method
    }
  addBank(bank: Bank): Observable<Bank> {
    return this.create(bank);
  }
}
 