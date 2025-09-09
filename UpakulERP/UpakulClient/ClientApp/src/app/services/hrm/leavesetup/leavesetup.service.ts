import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Leavesetup } from '../../../models/hr/leavesetup/leavesetup';
import { BaseService } from '../../../services/generic/base.service';
import { ConfigService } from '../../../core/config.service';

@Injectable({
  providedIn: 'root'
})
export class LeavesetupService extends BaseService<Leavesetup> {
  private apiRoot: string;

  constructor(http: HttpClient, configService: ConfigService) {
    super(http, `${configService.hrmApiBaseUrl()}LeaveSetup`);
    this.apiRoot = `${configService.hrmApiBaseUrl()}`; // ✅ Fix here
    // to build other URLs
  }

  getLeaveSetups(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'name',
    sortDirection: string = 'asc'
  ): Observable<{ listData: Leavesetup[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  getLeaveSetupById(id: number | string): Observable<Leavesetup> {
    return this.getById(id);
  }

  addLeaveSetup(leavesetup: Leavesetup): Observable<Leavesetup> {
    return this.create(leavesetup);
  }

  updateLeaveSetup(leavesetup: Leavesetup): Observable<Leavesetup> {
    return this.update(leavesetup);
  }

 deleteLeaveSetup(leavetypeId: number): Observable<any> {
  //console.log("leavetypeId", leavetypeId);
  const deleteCommand = {
    LeaveTypeId: leavetypeId,
    isActive: false // or true if you do soft delete
  };
 return this.delete(deleteCommand); 
}


  // Dropdowns
  getLeaveCategories(): Observable<{ value: string; text: string }[]> {
    return this.http.get<{ value: string; text: string }[]>(`${this.apiRoot}CommonDropdown/loadLeaveCategory`);
  }

  getEmployeeTypes(): Observable<{ value: string; text: string }[]> {
    return this.http.get<{ value: string; text: string }[]>(`${this.apiRoot}CommonDropdown/loadEmployeeType`);
  }

  getGenders(): Observable<{ value: string; text: string }[]> {
    return this.http.get<{ value: string; text: string }[]>(`${this.apiRoot}CommonDropdown/loadGender`);
  }

  getEligibleFromList(): Observable<{ value: string; text: string }[]> {
    return this.http.get<{ value: string; text: string }[]>(`${this.apiRoot}CommonDropdown/EligibleFrom`);
  }

 
}
