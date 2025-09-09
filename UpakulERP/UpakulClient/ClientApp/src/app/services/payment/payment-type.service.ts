import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
// import { ConfigService } from './../../app.config'
import { ConfigService } from '../../core/config.service';

export interface PaymentTypeDropdown {
  text: any;
  name: string;
  value: string;
}

export interface Bank {
  text: string;
  value: string;
  selected: false;
}

export interface Branch {  
  text: string;
  value: string;
  selected: string; // optional, to filter by bank
}

export interface Cheque {  
  text: string;
  value: string;
  selected: string; // optional, to filter by bank
}

@Injectable({
  providedIn: 'root',
})
export class PaymentTypeService {
  constructor(private http: HttpClient, private configService: ConfigService) {}

  getPaymentTypeDropdown(): Observable<PaymentTypeDropdown[]> {
    return this.http
      .get<{ name: string; value: string; data: PaymentTypeDropdown[] }>(
        `${this.configService.mfApiBaseUrl()}CommonDropDown/LoadPaymentType`
      )
      .pipe(
        map((response) => response.data.filter((item) => item.value !== '')) // remove empty values
      );
  }

  // http://localhost:5004/api/v1/Bank/getbanksfordropdown?id=
  /** Get list of banks */
  getBankDropdown(): Observable<Bank[]> {
    return this.http
      .get<{ name: string; value: string; data: Bank[] }>(
        `${this.configService.mfApiBaseUrl()}bankAccountMapping/getOfficeXBankDropdown`
      )
      .pipe(
        map((response) => response.data) // optionally filter if needed
      );
  }
  /** Get list of branches by bank ID */
  // http://localhost:5004/api/v1/BankBranch/GetBranchDropdownXBank?bankid=`+
  // branchList: Branch[] = [];
  // getBranchDropdown(bankId: string): Observable<Branch[]> {
  //   return this.http
  //     .get<{ name: string; value: string; data: Branch[] }>(
  //       `${this.configService.globalApiBaseUrl()}BankBranch/GetBranchDropdownXBank?bankId=${bankId}`
  //     )
  //     .pipe(map((response) => response.data));
  // }

  chequeList: Cheque[] = [];
  getChequeDropdown(bankId: string): Observable<Cheque[]> {
    // console.log('__:', bankBranchId);
    return this.http
      .get<{ name: string; value: string; data: Cheque[] }>(
        `${this.configService.mfApiBaseUrl()}bankAccountMapping/GetChequeDetailsDropdown?bankId=${bankId}`
      )
      .pipe(map((response) => response.data));
  }
}
