import { Component, OnInit } from '@angular/core';
import { SharesService } from '../../services/shares/shares.service';
import { Share } from '../../models/share';
import { Observable } from 'rxjs';
import { UsersService } from '../../services/users/users.service';
import { parseIntAutoRadix } from '@angular/common/src/i18n/format_number';

@Component({
  selector: 'app-market',
  templateUrl: './market.component.html',
  styleUrls: ['./market.component.scss'],
})
export class MarketComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
