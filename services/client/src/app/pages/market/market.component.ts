import {Component, OnInit} from '@angular/core';
import {SharesService} from '../../services/shares/shares.service';
import {Share} from '../../models/share';
import {Observable} from 'rxjs';
import {UsersService} from '../../services/users/users.service';
import {parseIntAutoRadix} from '@angular/common/src/i18n/format_number';

@Component({
  selector: 'app-market',
  templateUrl: './market.component.html',
  styleUrls: ['./market.component.scss']
})
export class MarketComponent implements OnInit {
  shares: Share[] = [];

  constructor(private sharesService: SharesService, private usersService: UsersService) {
  }

  ngOnInit() {
    this.sharesService.getAll().subscribe(res => res.forEach((model, index) => this.shares.push(model)));
  }

  buyShare(share: Share) {
    this.usersService.getUserById(Number(localStorage.getItem('id'))).subscribe(resUser => {
      this.sharesService.getAll().subscribe(res => {
        const userShares: Share[] = [];

        // taking id for test should be owner id
        res.forEach(model => {
          if (model.id === resUser.id) {
            userShares.push(model);
          }
        });
        if (userShares !== null) {
          if (userShares.find(model => model.name === share.name)) {
            let userShare = userShares.find(model => model.name === share.name).count++;
            resUser.shares = userShares;
            this.usersService.updateUser(resUser);
          } else {
            userShares.push(share);
            resUser.shares = userShares;
            this.usersService.updateUser(resUser);
          }
        } else {
          userShares.push(share);
          resUser.shares = userShares;
          this.usersService.updateUser(resUser);
        }
        console.log(res);
      });
    });
  }

  sellShare(share: Share) {
  }

}
