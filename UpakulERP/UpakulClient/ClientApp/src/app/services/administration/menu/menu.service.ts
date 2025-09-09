import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Menu } from '../../../models/administration/menu/menu.model';
import { ConfigService } from '../../../core/config.service';

interface Module {
  text: string;
  value: string;
}
interface parentMenuEntry {
  text: string;
  value: string;
}
@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private domain_url: string;
  constructor(private http: HttpClient, private configService: ConfigService) {
    this.domain_url = this.configService.authApiBaseUrl();
  }

  // Get modules from API
  getModules(): Observable<Module[]> {
    return this.http.get<{ statusCode: number; message: string; data: Module[] }>(`${this.domain_url}Module/GetModuleByDropdown`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value
    );
  }

  // Get parent menu entries from API
  getParentMenuEntries(selectedModule: number): Observable<parentMenuEntry[]> {
    return this.http.get<{ statusCode: number; message: string; data: parentMenuEntry[] }>(`${this.domain_url}Menu/GetMenubyModuleDropdown?moduleId=${selectedModule}`).pipe(
      map(response => response.data.filter(item => item.value !== '')) // Remove empty value
    );
  }

  // Add new menu
  addMenu(menu: Menu): Observable<any> {
    return this.http.post<Menu>(`${this.domain_url}Menu/Create`, menu); // Ensure correct endpoint
  }
}