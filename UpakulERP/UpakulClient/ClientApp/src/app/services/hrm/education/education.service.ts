import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Education } from '../../../models/hr/education/education';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

@Injectable({
  providedIn: 'root'
})
export class EducationService extends BaseService <Education>  {

  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.hrmApiBaseUrl()}Education`);
  }
  getEducationList(
    page: number,
    pageSize: number,
    searchTerm: string = '',
     sortOrder:string=''
  ): Observable<{ listData: Education[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }
  addEducation(education: Education): Observable<Education> {
       return this.create(education);
    }
 getEducationById(id: number | string): Observable<Education> {
      return this.getById(id);
    }

  // getEducation(educationId: string): Observable<any> {
  //   const url = `${this.domain_url}Education/GetById?id=${educationId}`;
  //   return this.http.get<any>(url).pipe(
  //     map((res: any) => res.data)
  //   );
  // }


  UpdateEducation(educationData: Education): Observable<Education> {
    return this.update(educationData);
    }
       deleteEducation(educationId: number): Observable<any> {
      const deleteCommand = {
        EducationId: educationId,
        isActive: false
      };
      return this.delete(deleteCommand); // calling BaseService's method
    }
}
