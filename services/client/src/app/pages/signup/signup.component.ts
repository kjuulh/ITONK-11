import {Component, OnInit} from '@angular/core';
import {UsersService} from '../../services/users/users.service';
import {User} from '../../models/user';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  username = '';
  password: '';

  constructor(private usersService: UsersService) {
  }

  ngOnInit() {
  }

  onSubmit() {
    const model = new User();
    model.name = this.username;
    model.password = this.password;

    if (this.username !== '' && this.password !== '') {
      this.usersService.registerUser(model).subscribe(res => {
        if (res != null) {
          localStorage.setItem('name', (<User>res).name);
          localStorage.setItem('password', (<User>res).password);
        }
      });
    }
  }
}
