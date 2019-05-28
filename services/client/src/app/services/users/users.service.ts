import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/user';
import { Subject } from 'rxjs';

const USER_ID = 'UserId';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  // Replace with environment variable
  apiUrl = 'http://api.kjuulh.io/api/users';

  userId: string;
  statusChanged: Subject<string> = new Subject();

  constructor(private http: HttpClient) {
    this.userId = this.getUserId();
    this.statusChanged.next(this.userId);
  }

  public saveUserId(email: string) {
    this.http.get<User>(this.apiUrl + '/email/' + email).subscribe(data => {
      window.sessionStorage.removeItem(USER_ID);
      window.sessionStorage.setItem(USER_ID, data.userId);
      this.userId = data.userId;
      this.statusChanged.next(this.userId);
    });
  }

  public getUserId(): string {
    return sessionStorage.getItem(USER_ID);
  }
}
