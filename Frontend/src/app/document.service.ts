import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private apiUrl = `http://localhost:5150`;
  // Update with your API endpoint

  constructor(private http: HttpClient) {}

  getDocuments(): Observable<any> {
    return this.http.get(`${this.apiUrl}/document`);
  }

  uploadDocument(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/document`, formData);
  }

  verifyDocument(verificationCode: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/verify`, { verificationCode });
  }
}

