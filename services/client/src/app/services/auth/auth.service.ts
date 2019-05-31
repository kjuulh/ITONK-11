import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = 'http://api.kjuulh.io/api/authentication';

  constructor(private http: HttpClient) {}

  attemptLogin(username: string, password: string): Observable<any> {
    const credentials = { username: username, password: password };
    console.log('attemptLogin : fired');
    return this.http.post<any>(this.baseUrl + '/authenticate', credentials);
  }

  attemptRegister(username: string, password: string): Observable<any> {
    const credentials = { username: username, password: password };
    console.log('attemptRegister : fired');
    return this.http.post<any>(this.baseUrl + '/register', credentials);
  }
}
