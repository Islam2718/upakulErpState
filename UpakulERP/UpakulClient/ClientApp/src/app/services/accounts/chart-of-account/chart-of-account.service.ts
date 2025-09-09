import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

// Define your interface for AccountHead data
export interface AccountHead {
  accountId: number;
  headCode: string;
  headName: string;
  parentId: number | null;
  isTransactable: boolean;
  child?: AccountHead[];
}

interface ApiResponse<T> {
  statusCode: number;
  message: string;
  data: T;
}
export interface Office {
  officeId: number;
  officeCode: string;
  officeName: string;
  isAssign: boolean; // indicates whether this office is assigned
  selected?: boolean; // for checkbox selection in the modal
}

@Injectable({
  providedIn: 'root',
})
export class ChartOfAccountService extends BaseService<AccountHead> {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.accountApiBaseUrl()}AccountHead`);
  }

  /**
   * Get account head list
   * @param parentId optional parentId to filter results
   */
  getAccountHeads(
    parentId?: number,
    requestType: string = 'L'
  ): Observable<AccountHead[]> {
    let url = `${this.baseUrl}/GetAccountHeads`;
    const params: string[] = [];

    if (parentId) {
      params.push(`parentId=${parentId}`);
    }

    if (requestType) {
      params.push(`requestType=${requestType}`);
    }

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http
      .get<ApiResponse<AccountHead[]>>(url)
      .pipe(map((res) => res.data));
  }

  /** Get by ID */
  getAccountHeadById(id: number | string): Observable<AccountHead> {
    return this.getById(id);
  }

  /** Add new AccountHead */
  addAccountHead(payload: FormData): Observable<any> {
    return this.createFromData(payload);
  }

  /** Update existing AccountHead */
  updateAccountHead(payload: FormData): Observable<any> {
    return this.updateFromData(payload);
  }

  /** Delete AccountHead (soft delete or hard delete depending on backend) */
  deleteAccountHead(id: number): Observable<any> {
    return this.delete({ accountId: id, isActive: false });
  }

  /** Get paginated + searchable list */
  getAccountHeadList(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'headCode',
    sortDirection: string = 'asc'
  ): Observable<{ listData: AccountHead[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  getAssignableOfficeList(accountId: number): Observable<Office[]> {
    const url = `${this.configService.accountApiBaseUrl()}AccountHead/GetOfficeAssign?accountId=${accountId}`;
    return this.http
      .get<ApiResponse<Office[]>>(url)
      .pipe(
        map((res) => res.data.map((o) => ({ ...o, selected: o.isAssign })))
      );
  }

  assignOffices(
    payload: { officeId: number; accountId: number }[]
  ): Observable<any> {
    const url = `${this.configService.accountApiBaseUrl()}AccountHead/createOfficeAssign`;
    return this.http.post(url, payload);
  }
}
