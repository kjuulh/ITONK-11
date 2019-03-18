import {Component, OnInit} from '@angular/core';
import {UsersService} from '../../services/users/users.service';
import {User} from '../../models/user';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  username = '';
  password: '';

  constructor(private usersService: UsersService,
              private router: Router) {
  }

  ngOnInit() {
  }

  onSubmit() {
    // Primitive login service
    this.usersService.getUsers().subscribe(res => res.forEach((model, index) => {
      if (this.username === model.name && this.password === model.password) {
        localStorage.setItem('name', model.name);
        this.router.navigate(['/home']);
      }
    }));

  }
}
