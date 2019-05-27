import { Component } from '@angular/core';
import { TokenService } from './services/token/token.service';
import { tokenKey } from '@angular/core/src/view';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'client';

  constructor(private token: TokenService) {}

  logout() {
    console.log('logout : fired');
    this.token.signOut();
  }
}
