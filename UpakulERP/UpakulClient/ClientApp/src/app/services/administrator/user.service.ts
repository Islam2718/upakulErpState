import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ConfigService } from '../../core/config.service';
import { User } from '../../models/administration/user';
import { HttpParams } from '@angular/common/http';


// src/app/models/api-response.model.ts
export interface ApiResponse<T> {
  statusCode: number;
  message: string;
  data: T;
}

export interface ResetPassword {
  UserName: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private domain_url: string;
  constructor(private http: HttpClient, private configService: ConfigService) {
    this.domain_url = this.configService.authApiBaseUrl();
  }

  //  getUsers(): Observable<User[]> {
  //     return this.http.get<ApiResponse<User[]>>(`${this.domain_url}user/LoadList`).pipe(
  //       map(response => response.data) // Extract only the user list
  //     );
  //     //console.log("in getUsers");
  //   }

  LoadList(
    page: number,
    pageSize: number,
    searchTerm: string,
    sortOrder: string,
    sortColumn?: string,
    sortDirection?: string,): Observable<any> {
    const url = `${this.domain_url}user/loadGrid?page=${page}&pageSize=${pageSize}&search=${searchTerm}&sortColumn=${sortColumn}&sortDirection=${sortDirection}&sortOrder=${sortOrder}`;
    //console.log(url);
    return this.http.get<any>(url).pipe(
      map((res: any) => res.data)
    );
  }

  Update(updateData: any): Observable<any> {
    console.log("updateData" + updateData);
    return this.http.put<User>(`${this.domain_url}User/Register`, updateData);
  }

  resetUserPassword(userName: string): Observable<any> {
    const url = `${this.domain_url}Account/ResetPassword`;
    const username = {
      UserName: userName
    };
    return this.http.post(url, username);
  }

  delete(deleteId: number): Observable<any> {
    const id = deleteId;// parseInt(GroupId, 10); // Ensure it's an integer if needed 
    console.log("in delete", deleteId);
    const deleteCommand = {
      UserId: id, // assuming this is how the command expects it
      // Add other required fields for DeleteBankCommand here if necessary
    };
    return this.http.delete(`${this.domain_url}User/Delete`, {
      body: deleteCommand // pass the object in the body
    });
  }

}
