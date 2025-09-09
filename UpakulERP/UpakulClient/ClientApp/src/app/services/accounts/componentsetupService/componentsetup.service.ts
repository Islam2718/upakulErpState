import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ComponentSetup } from '../../../models/accounts/componentsetup/componentsetup';
import { ConfigService } from '../../../core/config.service';
interface ComponentType {
  text: string;
  value: string;
}
interface ComponentTypeDropdown {
  text: string;
  value: string;
}

interface PrincipalType {
  text: string;
  value: string;
}

@Injectable({
  providedIn: 'root'
})
export class ComponentsetupService {
  page = 1;
  searchTerm = '';
  sortColumn = '';
  sortDirection = '';
  totalPages = 1;
  pageSize = 10;
  private domain_url: string;

  constructor(private http: HttpClient, private configService: ConfigService) { 
    this.domain_url = this.configService.accountApiBaseUrl();
  }
    getDataTypes(): Observable<ComponentType[]> {
      const url = `${this.domain_url}CommonDropDown/LoadComponentType`;
      return this.http.get<{ statusCode: number; message: string; data: ComponentType[] }>(url).pipe(
        map(response => response.data)
      );
    }
      getDataByParentId(parentId?: number): Observable<PrincipalType[]> {
        let url = `${this.domain_url}ComponentSetup/GetDataByParentId`;
    
        // Append the query parameter only if parentId is provided
        if (parentId !== undefined && parentId !== null) {
          url += `?parentId=${parentId}`;
        }
        return this.http.get<{ statusCode: number; message: string; data: PrincipalType[] }>(url)
          .pipe(
            map(response => response.data)
          );
      }
    GetList(page: number, pageSize: number, searchTerm: string, sortColumn: string, sortDirection: string): Observable<{ componentsetups: ComponentSetup[], totalRecords: number }> {
        const url = `${this.domain_url}ComponentSetup/LoadGrid?page=${page}&pageSize=${pageSize}&search=${searchTerm}&sortColumn=${sortColumn}&sortDirection=${sortDirection}`;
        // console.log("API Request URL:", url); // ✅ Log the request to check sortColumn
        return this.http.get<{ statusCode: number; message: string;  componentsetups: ComponentSetup[]; totalRecords: number }>(url)
          .pipe(
            map(response => ({
              componentsetups: response.componentsetups, // 🔹 Rename `data` to `offices`
              totalRecords: response.totalRecords
    
            }))
          );
      }
      GetData(ComponetSetupId: string): Observable<any> {
        const url = `${this.domain_url}ComponentSetup/GetById?id=${ComponetSetupId}`;
        return this.http.get<any>(url).pipe(
          map((res: any) => res.data)
        );
      }
       Update(componentsetup: any): Observable<any> {
          return this.http.put<ComponentSetup>(`${this.domain_url}ComponentSetup/Update`, componentsetup);
        }
      
  delete(ComponetSetupId: string): Observable<any> {
    const id = parseInt(ComponetSetupId, 10); // Ensure it's an integer if needed

    const deleteCommand = {
      ComponetSetupId: id,
    };
    return this.http.delete(`${this.domain_url}ComponentSetup/Delete`, {
      body: deleteCommand // pass the object in the body
    });
  }
  
    getDataTypeDropdown(): Observable<ComponentTypeDropdown[]> {
      return this.http.get<{ name: string; value: string; data: ComponentTypeDropdown[] }>(`${this.domain_url}CommonDropDown/LoadComponentType`).pipe(
        map(response => response.data.filter(item => item.value !== '')) // Remove empty value
      );
    }
    add(modelData: ComponentType): Observable<any> {
      return this.http.post<ComponentType>(`${this.domain_url}ComponentSetup/Create`, modelData);
    }
  }
