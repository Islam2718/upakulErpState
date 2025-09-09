import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { DailyCollection } from '../../../models/microfinance/daily_collection/daily-collection';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';


interface DropdownValue {
  text: string;
  value: string;
  selected:boolean;
}


@Injectable({
  providedIn: 'root'
})

export class DailyCollectionService extends BaseService <DailyCollection>  {

  constructor(http: HttpClient, private configService: ConfigService) {
      super(http, `${configService.mfApiBaseUrl()}DailyCollection`);
  }

  groupDropdown(): Observable<DropdownValue[]> {
      const url = `${this.configService.mfApiBaseUrl()}Group/GetGroupDropdown`;
      //console.log(url);
      return this.http.get<{ data: [] }>(url).pipe(
        map(response => response.data) // Remove empty value .filter(item => item.value !== '')
      );
  }

  // getGroupXMemberDropdown(groupId: number): Observable<DropdownValue[]> {
  //   return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Member/GetGroupXMemberDropdown?groupId=${groupId}`).pipe(
  //     map(response => response.data.filter(item => item.value !== null)) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
  //   );
  // }

//  getGroupXMemberComponentDetails(groupId: number): Observable<DailyCollection[]> {
//     let params = new HttpParams()
//       .set('groupId', groupId);
//     return this.http.get<DailyCollection[]>(`${this.configService.mfApiBaseUrl()}Member/GetGroupXMemberComponentDetails`, { params });
//   }

  getEmployeeXGroupSheet(): Observable<any> {
    return this.http.get<any>(`${this.configService.mfApiBaseUrl()}DailyCollection/GetEmployeeXGroupSheet`);
  }


  getGroupXMemberSheet(groupId: number): Observable<any> {
    let params = new HttpParams()
      .set('groupId', groupId);
    return this.http.get<any>(`${this.configService.mfApiBaseUrl()}DailyCollection/GetGroupXMemberSheet`, { params });
  }


}
