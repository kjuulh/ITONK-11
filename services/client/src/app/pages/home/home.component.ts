import {Component, OnInit} from '@angular/core';
import {UsersService} from '../../services/users/users.service';
import {User} from '../../models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {

  constructor(private usersService: UsersService) {
  }

  ngOnInit() {
    const user = new User();
    user.userName = 'Kasper Hermansen';
    user.password = 'Test123456';

    this.usersService.registerUser(user).subscribe((res) => {
      console.log(res);
    });
  }

}
