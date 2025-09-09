import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { GeotypeData } from '../../../models/Global/geolist/geotype-data';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

interface DropdownValue {
  text: string;
  value: string;
}


@Injectable({
  providedIn: 'root'
})
export class GeolocationService  extends BaseService <GeotypeData>  {

    constructor(http: HttpClient, private configService: ConfigService) {
     super(http, `${configService.globalApiBaseUrl()}Geolocation`);
    }

    getGeolocations(
      page: number,
      pageSize: number,
      searchTerm: string = '',
      sortColumn: string = 'name',
      sortDirection: string = 'asc'
    ): Observable<{ listData: GeotypeData[]; totalRecords: number }> {
      return this.getList(page, pageSize, searchTerm, sortColumn, sortDirection);
    }

    getGeoLocationTypeDropdown(): Observable<DropdownValue[]> {
      return this.http.get<{ name: string; value: string; data: DropdownValue[] }>(`${this.configService.globalApiBaseUrl()}CommonDropDown/LoadGeoLocationType`).pipe(
        map(response => response.data.filter(item => item.value !== '')) // Remove empty value  .filter(item => item.value !== '')
      );
    }

    getDropDownData(): Observable<DropdownValue[]> {
       const url=`${this.configService.globalApiBaseUrl()}GeoLocation/GetGeoLocationByParentId`;
       return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(url).pipe(
          map(response => response.data) // Remove empty value .filter(item => item.value !== '')
      );
    }

    getDropDownSubData(id:number): Observable<DropdownValue[]> {
      const url=`${this.configService.globalApiBaseUrl()}GeoLocation/GetGeoLocationByParentId?parentId=`;
      return this.http.get<{ value: number; text: string; data: DropdownValue[] }>(`${url}`+id).pipe(
         map(response => response.data) // Remove empty value .filter(item => item.value !== '')
      );
    }

    getGeolocationById(id: number | string): Observable<GeotypeData> {
      return this.getById(id);
    }

    getSingleData(rowId: string): Observable<any> {
      const url = `${this.configService.globalApiBaseUrl()}GeoLocation/GetById?id=${rowId}`;
      return this.http.get<any>(url).pipe(
        map((res: any) => res.data)
      );
    }
  
    addGeoLocation(modelData: GeotypeData): Observable<GeotypeData> {
       return this.create(modelData);
    }

    UpdateData(updateData: GeotypeData): Observable<GeotypeData> {
      console.log("updateData", updateData);
      return this.update(updateData);
    }
    

    deleteData(deleteId: number): Observable<any> {
      const deleteCommand = {
        GeoLocationId: deleteId,
       
      };
      return this.delete(deleteCommand); // calling BaseService's method
    }
 
  
  }


