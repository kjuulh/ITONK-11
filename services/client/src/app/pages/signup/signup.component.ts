import {Component, OnInit} from '@angular/core';
import {UsersService} from '../../services/users/users.service';
import {User} from '../../models/user';
import {Router} from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  username = '';
  password: '';

  constructor(private usersService: UsersService,
              private router: Router) {
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
          this.router.navigate(['/login']);
        }
      });
    }
  }
}
