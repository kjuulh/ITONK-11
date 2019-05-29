import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

const TOKEN_KEY = 'AuthToken';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {
    if (this.getToken() == null) {
      this.loggedIn = false;
    } else {
      this.loggedIn = true;
    }
    this.statusChange.next(this.loggedIn);
  }

  loggedIn: boolean;
  statusChange: Subject<boolean> = new Subject();

  signOut() {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.clear();
    this.loggedIn = false;
    this.statusChange.next(this.loggedIn);
  }

  public saveToken(token: string) {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
    this.loggedIn = true;
    this.statusChange.next(this.loggedIn);
  }

  public getToken(): string {
    return sessionStorage.getItem(TOKEN_KEY);
  }
}
