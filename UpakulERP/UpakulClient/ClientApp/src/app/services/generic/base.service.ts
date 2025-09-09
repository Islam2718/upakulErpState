import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService<T> {
  constructor(protected http: HttpClient, protected baseUrl: string) { }



  getList(
    page: number,
    pageSize: number,
    searchTerm: string,
    sortOrder: string,
    sortColumn?: string,
    sortDirection?: string,
  ): Observable<{ listData: T[]; totalRecords: number }> {
    const url = `${this.baseUrl}/LoadGrid?page=${page}&pageSize=${pageSize}&search=${searchTerm}&sortColumn=${sortColumn}&sortDirection=${sortDirection}&sortOrder=${sortOrder}`;
    // const url = `${this.baseUrl}/LoadGrid`;
    return this.http.get<{ listData: T[]; totalRecords: number }>(url).pipe(
      map(res => ({
        listData: res.listData,
        totalRecords: res.totalRecords
      })),
      catchError(this.handleError)
    );
  }
  getListData<T>(path: string, queryParam: {}): Observable<T[]> {
    var qry = ''
    if (queryParam)
      qry = '?' + new URLSearchParams(queryParam);
    const url = `${this.baseUrl}/${path}${qry}`;
    return this.http.get<{data:T[]}>(url).pipe(
       map(response =>response.data
        /*{console.log(response); return response.data;}*/ ),
      catchError(this.handleError)
    );
  }
  getById(id: number | string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/GetById?id=${id}`).pipe(
      map((res: any) => res.data),
      catchError(this.handleError)
    );
  }

  create(data: T, actionName: string = ''): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${actionName ? actionName : 'Create'}`, data).pipe(
      catchError(this.handleError)
    );
  }

  createFromData(data: FormData): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/Create`, data).pipe(
      catchError(this.handleError)
    );
  }


  update(data: T, actionName: string = ''): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${actionName ? actionName : 'Update'}`, data).pipe(
      catchError(this.handleError)
    );
  }
  updateFromData(data: FormData): Observable<any> {
    return this.http.put<T>(`${this.baseUrl}/Update`, data).pipe(
      catchError(this.handleError)
    );
  }
  /*Mahfuz will remove*/
  approved(approvedCommand: any): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/Approved`, approvedCommand).pipe(
      catchError(this.handleError)
    );
  }
  verifymemberOtp(otpCommand: any, otpNo: number): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/Approved`, otpCommand).pipe(
      catchError(this.handleError)
    );
  }
  checked(checkedCommand: any): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}`, checkedCommand).pipe(
      catchError(this.handleError)
    );
  }
  // Accept a full object instead of just an ID
  delete(deleteCommand: any): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Delete`, { body: deleteCommand }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    //console.log('__ServiceError__: ', error);
    //debugger
    if (error.status == 0 || error.status == 502) {
      return throwError({
        type: 'strongerror',
        message: `Network error or Service unavailable.`,
      });
    }
    else if (error.status == 400) // Object Validation
    {
      const validationErrors = error.error?.errors;
      let objXerr_json = (validationErrors ? JSON.stringify(validationErrors) : null)  // stringify validation errors if no general message
      if (!objXerr_json) {
        return throwError({
          type: 'warning',
          message: `${error.statusText}: ${error.error?.message ? error.error?.message : 'Request data format error'}`,
        });
      } else {
        return throwError({
          type: 'modelObjectError',
          message: `${error.statusText}: ${error.error?.title ? error.error?.title : 'Request data format error'}`,
          modelobjJson: objXerr_json
        });
      }
    }
    else if (error.status == 401) {
      return throwError({
        type: 'strongerror',
        message: `The request is unauthenticated.`,
      });
    }
    else if (error.status == 403) {
      return throwError({
        type: 'strongerror',
        message: `${error.error?.message ? error.error?.message : 'Access Not allow'}`,
      });
    }
    else if (error.status == 404) {
      return throwError({
        type: 'strongerror',
        message: `${error.error?.message ? error.error?.message : 'Domain is wrong or unreachable'}`,
      });
    }
    else if (error.status == 405) {
      return throwError({
        type: 'strongerror',
        message: `${error.error?.message ? error.error?.message : 'HTTP method not allowed for this request.'}`,
      });
    }
    else if (error.status == 408) { // Request time out
      return throwError({
        type: 'strongerror',
        message: `${error.error?.message ? error.error?.message : 'Request time out. Please contact the administrator'}`,
      });
    }
    else {
      const isClientError = error.status >= 401 && error.status < 500;
      const type = isClientError ? 'warning' : 'strongerror';

      // Return both the backend message and the derived type
      return throwError({
        type,
        message: error.error?.message || 'Please contact the administrator.'
      });
    }
  }

}
