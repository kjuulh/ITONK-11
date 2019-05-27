import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/user';
import { Observable } from 'rxjs';
import { Share } from '../../models/share';

const USER_ID = 'UserId';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  // Replace with environment variable
  apiUrl = 'http://api.kjuulh.io/api/users';

  constructor(private http: HttpClient) {}

  public saveUserId(email: string) {
    this.http.get<User>(this.apiUrl + '/email/' + email).subscribe(data => {
      window.sessionStorage.removeItem(USER_ID);
      window.sessionStorage.setItem(USER_ID, data.userId);
    });
  }

  public getUserId(): string {
    return sessionStorage.getItem(USER_ID);
  }
}
