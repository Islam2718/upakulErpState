import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ImageService {
    constructor(private http: HttpClient) { }

    imageExists(url: string) {

        return this.http.head(url, { observe: 'response' }).pipe(
            map(response => response.status === 200),
            catchError(() => of(false)) // If error (e.g., 404), return false
        );
    }

    checkImageExists(url: string): Promise<boolean> {
        return new Promise(resolve => {
            const img = new Image();
            img.src = url;

            img.onload = () => resolve(true);   // Image loaded successfully
            img.onerror = () => resolve(false); // Failed to load (404, etc.)
        });
    }
}