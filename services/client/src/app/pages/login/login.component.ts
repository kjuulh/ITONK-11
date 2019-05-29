import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';
import { TokenService } from 'src/app/services/token/token.service';
import { MatSnackBar } from '@angular/material';
import { UsersService } from 'src/app/services/users/users.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  username = '';
  password = '';

  constructor(
    private auth: AuthService,
    private router: Router,
    private token: TokenService,
    private snackbar: MatSnackBar,
    private user: UsersService,
  ) {}

  ngOnInit() {}

  login() {
    this.auth.attemptLogin(this.username, this.password).subscribe(
      data => {
        this.token.saveToken(data.token);
        this.user.saveUserId(this.username);
        this.router.navigate(['home']);
        this.snackbar.open('Login was successful');
      },
      error => {
        this.snackbar.open('Invalid credentials');
      },
    );
  }
}
