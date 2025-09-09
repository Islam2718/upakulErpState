import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { BaseService } from '../../../services/generic/base.service';
import { ConfigService } from '../../../core/config.service';
import { Leavemapping } from '../../../models/hr/leavemapping/leavemapping';


interface OfficeTypeDropdown {
  text: string;
  value: string;
}
interface DesignationTypeDropdown {
  text: string;
  value: string;
}
interface LeaveCategoryTypeDropdown {
  text: string;
  value: string;
}
@Injectable({
  providedIn: 'root'
})

export class LeavemappingService extends BaseService<Leavemapping> {
  private domanin_url!: string;

  constructor(http: HttpClient, configService: ConfigService) {
    super(http, `${configService.hrmApiBaseUrl()}LeaveMapping`);
    this.domanin_url = configService.hrmApiBaseUrl();
  }

  getLeaveMappings(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'name',
    sortDirection: string = 'asc'
  ): Observable<{ listData: Leavemapping[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }

  getLeavemappingById(id: number | string): Observable<Leavemapping> {
    return this.getById(id);
  }

 // Create master (OfficeType + Designation + LeaveCategory)
createLeaveMappingMaster(data: any): Observable<any> {
  return this.http.post(`${this.domanin_url}LeaveMapping/CreateMaster`, data);
}

createLeaveMappingWithDetails(data: any): Observable<any> {
  return this.http.post(`${this.domanin_url}LeaveMapping/CreateWithDetails`, data);
}

// Create one detail (per mapping row)
createLeaveMappingDetail(data: any): Observable<any> {
  return this.http.post(`${this.domanin_url}LeaveMapping/CreateDetails`, data);
}


  updateLeavemapping(leavemapping: Leavemapping): Observable<Leavemapping> {
    return this.update(leavemapping);
  }

  deleteLeavemapping(leavemappingId: number): Observable<any> {
    //console.log("leavetypeId", leavetypeId);
    const deleteCommand = {
      LeavemappingId: leavemappingId,
      isActive: false
    };
    return this.delete(deleteCommand);
  }
  // Dropdowns
  getOfficeTypeDropdown(): Observable<OfficeTypeDropdown[]> {
    return this.http.get<{ name: string; value: string; data: OfficeTypeDropdown[] }>(`${this.domanin_url}CommonDropDown/LoadOfficeType`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value
    );
  }
  getDesignationTypeDropdown(): Observable<DesignationTypeDropdown[]> {
    return this.http.get<{ data: DesignationTypeDropdown[] }>(`${this.domanin_url}Designation/DesignationDropdown`).pipe(
      map(response => response.data.filter(item => item.value !== ''))
    );
  }


  getLeaveCategoryTypeDropdown(): Observable<LeaveCategoryTypeDropdown[]> {
    return this.http.get<{ name: string; value: string; data: LeaveCategoryTypeDropdown[] }>(`${this.domanin_url}CommonDropDown/LoadLeaveCategory`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value
    );
  }
}
