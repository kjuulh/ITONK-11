import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/user';
import { Observable } from 'rxjs';
import { Share } from '../../models/share';

@Injectable({
  providedIn: 'root',
})
export class SharesService {
  // Replace with environment variable
  baseUrl = 'http://api.kjuulh.io/api/Shares';

  constructor(private http: HttpClient) {}

  getShare(shareId: string): Observable<Share> {
    console.log('getShare : fired');
    return this.http.get<Share>(this.baseUrl + '/' + shareId);
  }
}
