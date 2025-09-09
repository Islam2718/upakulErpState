// src/app/core/config.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { get } from 'http';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  static globalApiBaseUrl() {
      throw new Error('Method not implemented.');
  }
  private config: any;

  constructor(private http: HttpClient) {}

  loadConfig(): Promise<void> {
    return firstValueFrom(this.http.get('/assets/environment.json'))
      .then(data => {
        this.config = data;
      })
      .catch(err => {
        console.error('Error loading config:', err);
        return Promise.resolve(); // Avoid breaking bootstrap
      });
  }

  private get(key: string): any {
    return this.config?.[key];
  }

  authApiBaseUrl(): string {
    return `${this.get('authApiBaseUrl')}`;
  }

  globalApiBaseUrl(): string {
    return `${this.get('globalApiBaseUrl')}`;
  }

  hrmApiBaseUrl(): string {
    return `${this.get('hrmApiBaseUrl')}`;
  }

  mfApiBaseUrl(): string {
    return `${this.get('mfApiBaseUrl')}`;
  }

  accountApiBaseUrl(): string {
    return `${this.get('accountApiBaseUrl')}`;
  }
  
  projectApiBaseUrl(): string {
    return `${this.get('projectApiBaseUrl')}`;
  }
}
