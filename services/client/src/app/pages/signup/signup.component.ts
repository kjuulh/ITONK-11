import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users/users.service';
import { User } from '../../models/user';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';
import { TokenService } from 'src/app/services/token/token.service';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  username = '';
  password = '';

  constructor(
    private auth: AuthService,
    private router: Router,
    private token: TokenService,
    private snackbar: MatSnackBar,
  ) {}

  ngOnInit() {}

  signup() {
    this.auth.attemptRegister(this.username, this.password).subscribe(
      data => {
        this.snackbar.open('Registration was a success, now login');
        this.router.navigate(['login']);
      },
      error => {
        this.snackbar.open('Something went wrong, try again...');
      },
    );
  }
}
