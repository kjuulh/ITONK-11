import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/user';
import { Observable } from 'rxjs';
import { Share } from '../../models/share';
import { MatSnackBar } from '@angular/material';
import { UsersService } from '../users/users.service';

export interface SellShareViewModel {
  userId: string;
  shareId: string;
  amount: number;
}

export interface RequestShareViewModel {
  requestId: string;
  shareId: string;
  amount: number;
  share: Share;
}

export interface StatusViewModel {
  status: string;
}

@Injectable({
  providedIn: 'root',
})
export class SharesService {
  // Replace with environment variable
  baseUrl = 'http://api.kjuulh.io/api/';

  constructor(
    private http: HttpClient,
    private snackbar: MatSnackBar,
    private usersService: UsersService,
  ) {}

  getShare(shareId: string): Observable<Share> {
    console.log('getShare : fired');
    return this.http.get<Share>(this.baseUrl + 'shares' + '/' + shareId);
  }

  sellShare(sellShareViewModel: SellShareViewModel): Observable<any> {
    console.log('sellShare : fired');
    return this.http.post<SellShareViewModel>(
      this.baseUrl + 'seller',
      sellShareViewModel,
    );
  }

  getOpenShares(next: any) {
    console.log('getOpenShares : fired');
    this.http
      .get<RequestShareViewModel[]>(this.baseUrl + 'trader/open')
      .subscribe(
        data => {
          data.forEach(element => {
            this.getShare(element.shareId).subscribe(
              res => (element.share = res),
            );
          });

          return next(data);
        },
        err =>
          this.snackbar.open("Couldn't get shares", 'X', { duration: 2000 }),
      );
  }

  buyShare(share: RequestShareViewModel): Observable<StatusViewModel> {
    console.log('buyShare : fired');
    return this.http.post<StatusViewModel>(
      this.baseUrl + 'buyer/request/' + share.requestId,
      {
        userId: this.usersService.getUserId(),
      },
    );
  }
}
