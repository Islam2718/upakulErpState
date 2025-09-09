import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { BaseService } from '../../../services/generic/base.service';
import { ConfigService } from '../../../core/config.service';
import { GraceSchedule } from '../../../models/microfinance/GraceSchedule/graceschedule.model';

interface BranchOfficeDropdown {
  text: string;
  value: string;
}
interface GroupTypeDropdown {
  text: string;
  value: string;
}
@Injectable({
  providedIn: 'root'
})
export class GraceScheduleService extends BaseService<GraceSchedule> {

  private domain_url!: string;
  constructor(http: HttpClient, 
    private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}GraceSchedule`);
    this.domain_url = configService.mfApiBaseUrl();
  }
 getGraceSchedules(
     page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder:string=''
  ): Observable<{ listData: GraceSchedule[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }

  getGraceScheduleById(id: number | string): Observable<GraceSchedule> {
    return this.getById(id);
  }


  UpdateData(updateData: GraceSchedule): Observable<GraceSchedule> {
    return this.update(updateData);
  }
  addData(modelData: GraceSchedule): Observable<GraceSchedule> {
    return this.create(modelData);
  }
  approveData(id: number): Observable<any> {
  const approvedCommand = { Id: id };
  return this.approved(approvedCommand);
}


  deleteData(deleteId: number): Observable<any> {
    const deleteCommand = {
      Id: deleteId,
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }
  
  // Dropdowns
  getBranchOfficeDropdown(): Observable<BranchOfficeDropdown[]> {
    return this.http.get<{ name: string; value: string; data: BranchOfficeDropdown[] }>(`${this.domain_url}Office/GetBranchOfficeDropdown`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value
    );
  }
  getGroupTypeDropdown(): Observable<GroupTypeDropdown[]> {
    return this.http.get<{ data: GroupTypeDropdown[] }>(`${this.domain_url}Group/GetGroupDropdown`).pipe(
      map(response => response.data.filter(item => item.value !== ''))
    );
  }

}
