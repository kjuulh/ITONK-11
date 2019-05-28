import { Component, OnInit } from '@angular/core';
import { TokenService } from './services/token/token.service';
import { tokenKey } from '@angular/core/src/view';
import { UsersService } from './services/users/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  isLoggedIn: boolean;

  ngOnInit(): void {
    this.isLoggedIn = this.token.loggedIn;
    this.token.statusChange.subscribe(data => {
      this.isLoggedIn = data;
    });
  }

  constructor(private token: TokenService) {}

  logout() {
    console.log('logout : fired');
    this.token.signOut();
  }
}
