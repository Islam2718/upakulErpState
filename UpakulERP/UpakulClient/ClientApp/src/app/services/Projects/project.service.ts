import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Project } from '../../models/projects/project';
import { ConfigService } from '../../core/config.service';
import { BaseService } from '../generic/base.service';

interface Dropdown {
  text: string;
  value: string;
}

interface ProjectDropDown {
  text: string;
  value: string;
  selected: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class ProjectService extends BaseService<Project> {
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.projectApiBaseUrl()}Project`);
  }
  getProjects(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortColumn: string = 'name',
    sortDirection: string = 'asc'
  ): Observable<{ listData: Project[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
  }
  getProjectById(id: number | string): Observable<Project> {
    return this.getById(id);
  }
  addProject(project: Project): Observable<Project> {
    return this.create(project);
  }
  updateProject(project: Project): Observable<Project> {
    return this.update(project);
  }
  deleteData(projectId: number): Observable<any> {
    const deleteCommand = {
      ProjectId: projectId,
      isActive: false,
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }

  //use at activity plan
  getProjectDropdown(): Observable<ProjectDropDown[]> {
    const url = `${this.configService.projectApiBaseUrl()}project/GetProjectDropDown`;
    return this.http.get<{ data: ProjectDropDown[] }>(url).pipe(
      map((response) => response.data.filter((item) => item.value !== '')) // optional filter
    );
  }

  getProjectActivityByProjectId(
    projectId: number
  ): Observable<ProjectDropDown[]> {
    const url = `${this.configService.projectApiBaseUrl()}ActivityPlan/GetProjectXActivity?projectId=${projectId}`;
    return this.http
      .get<{ data: ProjectDropDown[] }>(url)
      .pipe(
        map((response) => response.data.filter((item) => item.value !== ''))
      );
  }
  //use at activity plan
  getProjectTypeDropdown(): Observable<Project[]> {
    const url = `${this.configService.projectApiBaseUrl()}activityPlan/LoadTargetType`;
    return this.http.get<{ data: [] }>(url).pipe(
      map((response) => response.data) // Remove empty value .filter(item => item.value !== '')
    );
  }
  getProjectTargetTypes(): Observable<ProjectDropDown[]> {
    const url = `${this.configService.projectApiBaseUrl()}ActivityPlan/LoadTargetType`;
    return this.http
      .get<{ data: ProjectDropDown[] }>(url)
      .pipe(
        map((response) => response.data.filter((item) => item.value !== ''))
      );
  }
}
