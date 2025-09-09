import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,map } from 'rxjs';
import { BaseService } from '../../../generic/base.service';
import { ConfigService } from '../../../../core/config.service';
import { Codegenerator } from '../../../../models/microfinance/configuration/codegenerator/codegenerator.model';

@Injectable({
  providedIn: 'root'
})
export class CodegeneratorService extends BaseService<Codegenerator>{
  
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}CodeGenerator`);
  }

  getAll(): Observable<Codegenerator[]> {
    return this.http.get<Codegenerator[]>(`${this.baseUrl}/GetAll`);
  }

  updateCodegenerator(codegenerator: Codegenerator): Observable<Codegenerator> {
    return this.update(codegenerator);
  }
  updateCodegeneratorList(data: Codegenerator[]): Observable<Codegenerator[]> {
    return this.http.put<Codegenerator[]>(`${this.baseUrl}/UpdateList`, data); 
  }


}