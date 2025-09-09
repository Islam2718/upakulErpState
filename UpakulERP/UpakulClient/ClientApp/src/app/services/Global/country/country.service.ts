import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Country } from '../../../models/Global/country/country';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';
@Injectable({
  providedIn: 'root'
})

export class CountryService extends BaseService<Country> {
  constructor(http: HttpClient, configService: ConfigService) {
    super(http, `${configService.globalApiBaseUrl()}Country`);
  }

  /**
   * Get paginated, searchable, and sortable list of countries.
   */
  getCountries(
    page: number,
    pageSize: number,
    searchTerm: string = '',
    sortOrder:string=''
  ): Observable<{ listData: Country[]; totalRecords: number }> {
    return this.getList(page, pageSize, searchTerm, sortOrder);
  }

  getCountryById(id: number | string): Observable<Country> {
    return this.getById(id);
  }

  addCountry(country: Country): Observable<Country> {
    return this.create(country);
  }

  updateCountry(country: Country): Observable<Country> {
    return this.update(country);
  }

  deleteCountry(id: number): Observable<any> {
    const deleteCommand = {
      countryId: id,
      isActive: false
    };
    return this.delete(deleteCommand); // calling BaseService's method
  }

}