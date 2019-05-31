import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

export interface AccountViewModel {
  accountId: string;
  balance: number;
  transactions: TransactionViewModel[];
  dataAdded: string;
}

export interface TransactionViewModel {
  transactionId: string;
  amount: number;
  dataAdded: string;
}

export interface BankViewModel {
  userId: string;
  dateAdded: string;
  accounts: BankAccountViewModel[];
}

export interface BankAccountViewModel {
  accountId: string;
  userId: string;
}

@Injectable({
  providedIn: 'root',
})
export class BankService {
  baseUrl = 'http://api.kjuulh.io/api/';

  constructor(private http: HttpClient) {}

  getAccount(userId: string, next: any, err: any): void {
    console.log('getAccount : fired');
    this.http.get<BankViewModel>(this.baseUrl + 'bank/' + userId).subscribe(
      res => {
        this.http
          .get<AccountViewModel>(
            this.baseUrl + 'account/' + res.accounts[0].accountId,
          )
          .subscribe(acc => {
            next(acc);
          }),
          error => err(error);
      },
      error => err(error),
    );
  }

  createTransaction(
    accountId: string,
    amount: number,
  ): Observable<TransactionViewModel> {
    console.log('createTransaction: fired');
    return this.http.post<TransactionViewModel>(
      this.baseUrl + 'account/' + accountId + '/transactions',
      { amount: amount },
    );
  }
}
