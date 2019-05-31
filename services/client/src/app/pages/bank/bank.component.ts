import { Component, OnInit } from '@angular/core';
import {
  BankService,
  AccountViewModel,
} from 'src/app/services/bank/bank.service';
import { UsersService } from 'src/app/services/users/users.service';
import { MatDialog } from '@angular/material';
import { DepositDialogComponent } from 'src/app/dialogs/deposit-dialog/deposit-dialog.component';

@Component({
  selector: 'app-bank',
  templateUrl: './bank.component.html',
  styleUrls: ['./bank.component.scss'],
})
export class BankComponent implements OnInit {
  account: AccountViewModel;
  constructor(
    private bank: BankService,
    private user: UsersService,
    private dialog: MatDialog,
  ) {}

  ngOnInit() {
    this.getAccount();
  }

  private getAccount() {
    this.bank.getAccount(
      this.user.getUserId(),
      account => {
        console.log(account);
        this.account = account;
      },
      error => {
        console.log(error);
      },
    );
  }

  deposit() {
    const dialogRef = this.dialog.open(DepositDialogComponent, {
      data: { amount: 0 },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != null && result.amount > 0) {
        this.bank
          .createTransaction(this.account.accountId, result.amount)
          .subscribe(
            data => {
              this.getAccount();
            },
            error => {
              console.log(error);
            },
          );
      }
    });
  }
}
