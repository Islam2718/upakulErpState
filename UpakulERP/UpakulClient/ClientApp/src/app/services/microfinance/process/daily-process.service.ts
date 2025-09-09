import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { DailyProcess } from '../../../models/microfinance/process/daily-process';
import { ConfigService } from '../../../core/config.service';
import { BaseService } from '../../generic/base.service';

@Injectable({
  providedIn: 'root'
})

export class DailyProcessService extends BaseService<DailyProcess> {
  constructor(http: HttpClient, configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}DailyProcess`);
  }


    //getData(id: number | string): Observable<DailyProcess> {
     // return this.getById(id);
    //}
  
  
  addData(dailyProcess: DailyProcess): Observable<DailyProcess> {
    return this.create(dailyProcess);
  }



}
