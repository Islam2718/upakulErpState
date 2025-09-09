import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { GroupModel } from '../../../models/microfinance/group/groupModel';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

interface DropdownValue {
  text: string;
  value: string;
  selected:boolean;
}

export interface ApiResponse<T> {
  statusCode: number;
  message: string;
  data: T;
}

interface DropdownValueModal {
  text: string;
  value: string | number | null;
  selected:boolean;
}

interface GroupDropdownValue {
  value: number;
  text: string;
  selected: boolean;
}

@Injectable({
  providedIn: 'root'
})

export class GroupService extends BaseService <GroupModel>  {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}Group`);
  }

  getDaysDropdown(): Observable<DropdownValue[]> {
    return this.http.get<{ statusCode: number; message: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}CommonDropDown/LoadDays`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value
      
    );
  }

  getGroupTypeDropdown(): Observable<DropdownValue[]> {
    return this.http.get<{ name: string; value: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}CommonDropDown/LoadGroupType`).pipe(
      map(response => response.data) // Remove empty value  .filter(item => item.value !== '')
    );
  }

  getGeoLocationDropDownData(): Observable<DropdownValue[]> {
    const url = `${this.configService.mfApiBaseUrl()}CommonDropDown/GetGeoLocationByParentId`;
    return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(url).pipe(
      map(response => response.data) // Remove empty value .filter(item => item.value !== '')
    );
  }

  getGeoLocationDropDownSubData(id: number): Observable<DropdownValue[]> {
    const url = `${this.configService.mfApiBaseUrl()}CommonDropDown/GetGeoLocationByParentId?parentId=`;
    return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${url}` + id).pipe(
      map(response => response.data) // Remove empty value .filter(item => item.value !== '')
    );
  }

  getOfficeDropDownData(): Observable<DropdownValue[]> {
    return this.http.get<{ value: number; text: string; selected:boolean; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}office/GetBranchOfficeDropdown`).pipe(
      map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
    );
  }

  getLederMemberDropDownData(): Observable<DropdownValue[]> {
    return this.http.get<{ value: number; text: string; selected:boolean; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}member/getMemberForDropdown`).pipe(
      map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
    );
  }

  getGroups(
      page: number,
      pageSize: number,
      searchTerm: string = '',
      sortColumn: string = 'name',
      sortDirection: string = 'asc'
    ): Observable<{ listData: GroupModel[]; totalRecords: number }> {
      return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  getDataById(id: number | string): Observable<GroupModel> {
    return this.getById(id);
  }

  addData(modelData: GroupModel): Observable<GroupModel> {
    return this.create(modelData);
  }

  UpdateData(groupData: GroupModel): Observable<GroupModel> {
   return this.update(groupData);
  }

  deleteData(groupId: number): Observable<any> {
     const deleteCommand = {
        groupId: groupId,
        isActive: false
      };
      return this.delete(deleteCommand); // calling BaseService's method
  }

  getGroupDropListByEmpId(empId: number): Observable<DropdownValue[]> {    
    const url = `${this.configService.mfApiBaseUrl()}group/getEmployeeXGroupDropdown`;
    return this.http.get<DropdownValue[]>(url, {      
      params: { empId: empId.toString() }
    });
  }

  // getGroupXMemberDropDownData(): Observable<DropdownValue[]> {
  //   return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${this.configService.mfApiBaseUrl()}Member/GetMemberForDropdown`).pipe(
  //     map(response => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
  //   );
  // }
  
  // getGroupCommitteePositionsData(): Observable<any[]> {
  //   console.log("getGroupCommitteePositionsData");
  //   return this.http.get<{ value: number; text: string; data: any[] }>(`${this.configService.mfApiBaseUrl()}CommonDropDown/LoadGroupCommitteePositions`).pipe(
  //     map(response => response.data.filter(item => item.value !== '')) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
  //   );
  // }

  // getGroupXMemberDropdown(groupId: number): Observable<[]> {
  //   console.log("getGroupXMemberDropdown", groupId);
  //   let params = new HttpParams()
  //     .set('groupId', groupId);
  //   return this.http.get<[]>(`${this.configService.mfApiBaseUrl()}Member/GetGroupXMemberDropdown`, { params });
  // }

  // ---- Member Dropdown ----
  // getGroupXMemberDropDownData(groupId: number): Observable<DropdownValue[]> {
  //   let params = new HttpParams()
  //       .set('groupId', groupId);
  //   return this.http
  //     .get<ApiResponse<DropdownValue[]>>(
  //       `${this.configService.mfApiBaseUrl()}Member/GetMemberDropdown`
  //       , { params }
  //     )
  //     .pipe(
  //       map(response => 
  //         (response.data || []).filter(item => item.value !== null && item.value !== '')
  //       )
  //     );
  // }

// ---- Committee Positions ----
  getGroupCommitteePositionsData(): Observable<DropdownValue[]> {
    return this.http
    .get<ApiResponse<DropdownValue[]>>(
      `${this.configService.mfApiBaseUrl()}CommonDropDown/LoadGroupCommitteePositions`
    )
    .pipe(
      map(response => 
        (response.data || []).filter(item => item.value !== null && item.value !== '')
      )
    );
  }

  groupCommitteeAllDropdown(groupId: number): Observable<DropdownValue[]> {
    let params = new HttpParams()
      .set('groupId', groupId);

      const url = `${this.configService.mfApiBaseUrl()}GroupCommittee/GetGroupXCommitteeAllData`;
      return this.http.get<{ data: [] }>(url, { params }).pipe(
        map(response => response.data) // Remove empty value .filter(item => item.value !== '')
      );
  }
  
  saveCommittee(data: any): Observable<any> {
    return this.http.post(`${this.configService.mfApiBaseUrl()}GroupCommittee/Create`, data);
  }
}
