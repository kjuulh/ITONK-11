import { Component, OnInit } from '@angular/core';
import {
  SharesService,
  RequestShareViewModel,
} from '../../services/shares/shares.service';
import { Share } from '../../models/share';
import { Observable } from 'rxjs';
import { UsersService } from '../../services/users/users.service';
import { parseIntAutoRadix } from '@angular/common/src/i18n/format_number';
import { MatTable, MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-market',
  templateUrl: './market.component.html',
  styleUrls: ['./market.component.scss'],
})
export class MarketComponent implements OnInit {
  hasData = false;
  displayedColumns: string[] = ['name', 'price', 'buy'];
  dataSource: RequestShareViewModel[];
  constructor(
    private sharesService: SharesService,
    private snackbar: MatSnackBar,
  ) {}

  ngOnInit() {
    this.getShares();
  }

  getShares(): void {
    this.sharesService.getOpenShares(next => {
      this.dataSource = next;
      this.hasData = true;
    });
  }

  buyShare(share: RequestShareViewModel) {
    console.log(share);
    this.sharesService.buyShare(share).subscribe(
      data => {
        if (data.status.toLowerCase() === 'success') {
          this.getShares();
          this.snackbar.open('You\'ve bought "' + share.share.name + '"', 'X', {
            duration: 4000,
          });
        }
      },
      err => {
        this.snackbar.open(
          'It wasn\'t possible to buy "' + share.share.name + '"',
          'X',
          {
            duration: 4000,
          },
        );
      },
    );
  }
}
