import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';
  loggedIn = false;

  getLoggedInStatus() {
    return localStorage.getItem("name") !== null;
  }

  logout() {
    localStorage.clear();
  }
}
