import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../../models/user';
import {Observable} from 'rxjs';
import {Share} from '../../models/share';

@Injectable({
  providedIn: 'root'
})
export class SharesService {
  // Replace with environment variable
  apiUrl = 'http://35.246.254.34/api/sharemodels';

  constructor(private http: HttpClient) {
  }

  public register(share: Share) {
    return this.http.post(this.apiUrl, share);
  }

  public update(share: Share) {
    return this.http.put(this.apiUrl, share);
  }

  public getById(id: number): Observable<Share> {
    return this.http.get<Share>(`${this.apiUrl}/${id}`);
  }

  public getAll(url?: string): Observable<Share[]> {
    return this.http.get<Share[]>(this.apiUrl);
  }

  public delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
