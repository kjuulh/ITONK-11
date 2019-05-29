import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Share } from 'src/app/models/share';

export interface ProviderModel {
  userId: string;
  portfolioId: string;
  name: string;
  totalValue: number;
  count: number;
}

@Injectable({
  providedIn: 'root',
})
export class ProviderService {
  baseUrl = 'http://api.kjuulh.io/api/Provider';

  constructor(private http: HttpClient) {}

  createShare(providerModel: ProviderModel): Observable<Share> {
    console.log('createShare : fired');
    return this.http.post<Share>(this.baseUrl, providerModel);
  }
}
