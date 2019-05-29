import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Portfolio } from 'src/app/models/portfolio';

@Injectable({
  providedIn: 'root',
})
export class PortfolioService {
  baseUrl = 'http://api.kjuulh.io/api/Portfolio';

  constructor(private http: HttpClient) {}

  getPortfolio(userId: string): Observable<Portfolio> {
    console.log('getPortfolio : fired');
    return this.http.get<Portfolio>(this.baseUrl + '/user/' + userId);
  }

  createPortfolio(userId: string): Observable<any> {
    console.log('createPortfolio : fired');
    return this.http.post<any>(this.baseUrl, { userId: userId });
  }
}
