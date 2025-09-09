import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Member } from '../../../models/microfinance/member/member';
import { MemberViewModal } from '../../../models/microfinance/member/member-view-modal/member-view-modal';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

interface DropdownValue {
  text: string;
  value: string;
  selected: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class MemberService extends BaseService<Member> {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}Member`);
  }

  // member - check service class method
  checkMember(data: any): Observable<any> {
    const url = `${this.configService.mfApiBaseUrl()}Member/checker`;
    return this.http.post<any>(url, data);
  }

  createMember(formData: FormData): Observable<any> {
    return this.createFromData(formData);
    //return this.http.post(`${this.baseUrl}/Create`, formData);
  }

  updateMember(formData: FormData): Observable<any> {
    return this.updateFromData(formData);
    //return this.http.put(`${this.baseUrl}/Update`, formData);
  }
  updateMigrateMember(
    memberId: number,
    migratedNote: string
  ): Observable<Member> {
    const params: any = {
      MemberId: memberId,
      IsMigrated: true,
      MigratedNote: migratedNote,
    };
    return this.update(params, 'MigrateMember');
  }
  updateVerifyMemberOtp(memberId: number, otpNo: string): Observable<any> {
    const params: any = { memberId: memberId, oTPNo: otpNo };
    return this.update(params, 'MobileNoVerified');
  }
  deleteData(id: number): Observable<any> {
    const deleteCommand = {
      memberId: id,
      isActive: false,
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }
  approvedMember(memberId: number, isApproved: boolean, note?: string) {
    const body = {
      memberId,
      isApproved,
      note: note ?? null,
    };
    return this.http.put<any>(`${this.baseUrl}/Approved`, body);
  }

  getCountryDropDownData(): Observable<DropdownValue[]> {
    return this.http
      .get<{ value: number; text: string; data: DropdownValue[] }>(
        `${this.configService.globalApiBaseUrl()}country/GetCountriesForDropdown`
      )
      .pipe(
        map((response) => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
  }

  getGeoLocationDropdown(parentId: number): Observable<DropdownValue[]> {
    return this.http
      .get<{ value: number; text: string; data: DropdownValue[] }>(
        `${this.configService.globalApiBaseUrl()}geolocation/GetGeoLocationByParentId?parentId=${parentId}`
      )
      .pipe(
        map((response) => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
  }

  memberAllDropdown(): Observable<DropdownValue[]> {
    const url = `${this.configService.mfApiBaseUrl()}member/GetAllDropDownData`;
    return this.http.get<{ data: [] }>(url).pipe(
      map((response) => response.data) // Remove empty value .filter(item => item.value !== '')
    );
  }
  getMemberDropListByGroupId(groupId: number): Observable<DropdownValue[]> {
    //const url = `${this.configService.mfApiBaseUrl()}group/`;
    var pram = { groupId: groupId };
    return this.getListData('GetMemberDropdown', pram).pipe(
      map((response) => {
        return response as DropdownValue[];
      })
    );
  }
  /*mid wise Data */
  getMemberDetailById(id: number): Observable<MemberViewModal> {
    const params = { id: id };
    return this.getListData('GetMemberDetailById', params).pipe(
      map((response: any) => response as MemberViewModal)
    );
  }

  /** Get paginated, searchable, and sortable list of countries. */
  getDataList(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'memberName',
    sortDirection: string = 'asc'
  ): Observable<{ listData: Member[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  getDataById(id: number | string): Observable<Member> {
    return this.getById(id);
  }
  getEmployeeDropdown(emp?: number): Observable<DropdownValue[]> {
    //console.log("in emp", emp);
    return this.http
      .get<{ name: string; value: string; data: DropdownValue[] }>(
        `${this.configService.mfApiBaseUrl()}employee/getemployeefordropdown`
      )
      .pipe(
        map((response) => response.data) // Remove empty value  .filter(item => item.value !== '') GetAllEmployeeforDropdown?empId=0
      );
  }

  getMemberDropDownData(): Observable<DropdownValue[]> {
    return this.http
      .get<{ value: number; text: string; data: DropdownValue[] }>(
        `${this.configService.mfApiBaseUrl()}Member/GetMemberForDropdown`
      )
      .pipe(
        map((response) => response.data) // Remove empty value .filter(item => item.value !== '')    GetOfficeDropdown
      );
  }
}
