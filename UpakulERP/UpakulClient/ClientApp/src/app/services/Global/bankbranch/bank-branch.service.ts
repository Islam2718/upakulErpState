import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { tap } from 'rxjs/operators';
import { BankBranch } from '../../../models/Global/bankbranch/bankbranch';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

interface Banks {
  text: string;
  value: string;
}


@Injectable({
  providedIn: 'root'
})
export class BankBranchService extends BaseService <BankBranch> {
  
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.globalApiBaseUrl()}BankBranch`);
  }
  getBankBranches(
     page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder:string=''
  ): Observable<{ listData: BankBranch[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }
  getBankBranchsById(id: number | string): Observable<BankBranch> {
    return this.getById(id);
  }
  getBankDropdown(): Observable<Banks[]> {
    return this.http.get<{ name: string; value: string; data: Banks[] }>(`${this.configService.globalApiBaseUrl()}Bank/GetBanksForDropdown`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value
);
}
 updateBankBranch(bankbranch: BankBranch): Observable<BankBranch> {
  return this.update(bankbranch);
}
deleteBankBranch(id: number): Observable<any> {
  const deleteCommand = {
    bankbranchId: id,
    isActive: false
  };
  return this.delete(deleteCommand); // calling BaseService's method
}
addBankBranch(bankbranch: BankBranch): Observable<BankBranch> {
  return this.create(bankbranch);
}
}
  

