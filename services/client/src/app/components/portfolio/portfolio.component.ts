import { Component, OnInit } from '@angular/core';
import { Portfolio } from 'src/app/models/portfolio';
import { PortfolioService } from 'src/app/services/portfolio/portfolio.service';
import { UsersService } from 'src/app/services/users/users.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs';
import { Share } from 'src/app/models/share';
import { SharesService } from 'src/app/services/shares/shares.service';
import { TokenService } from 'src/app/services/token/token.service';
import { CreateStockComponent } from 'src/app/dialogs/create-stock/create-stock.component';
import { ProviderService } from 'src/app/services/provider/provider.service';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.scss'],
})
export class PortfolioComponent implements OnInit {
  portfolio: Observable<Portfolio>;
  shares: Share[];

  displayedColumns: string[] = [
    'name',
    'value',
    'count',
    'single share value',
    'total value',
    'total count',
  ];
  userId: string;
  portfolioId: string;
  isLoggedIn: boolean;

  constructor(
    private portfolioService: PortfolioService,
    private user: UsersService,
    private sharesService: SharesService,
    private token: TokenService,
    public dialog: MatDialog,
    private provider: ProviderService,
    private snackbar: MatSnackBar,
  ) {}

  ngOnInit() {
    this.isLoggedIn = this.token.loggedIn;
    this.token.statusChange.subscribe(data => {
      this.isLoggedIn = data;
    });

    this.retreivePortfolio();
    this.user.statusChanged.subscribe(data => {
      this.userId = data;
      this.retreivePortfolio();
    });
  }

  private retreivePortfolio() {
    this.userId = this.user.getUserId();
    console.log(this.userId);
    this.portfolio = this.portfolioService.getPortfolio(this.userId);
    this.portfolio.subscribe(data => {
      this.portfolioId = data.portfolioId;
      this.shares = data.shares;
      this.shares.forEach(element => {
        this.sharesService.getShare(element.shareId).subscribe(data => {
          element.name = data.name;
          element.totalCount = data.totalCount;
          element.totalValue = data.totalValue;
          element.singleShareValue = data.singleShareValue;
          element.value = data.singleShareValue * element.count;
          console.log(element);
        });
      });
    });
  }

  onCreateStockClicked() {
    const dialogRef = this.dialog.open(CreateStockComponent, {
      data: { userId: this.user.userId, portfolioId: this.portfolioId },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != null) {
        this.provider.createShare(result).subscribe(data => {
          if (data != null) {
            this.retreivePortfolio();
          }
        });
      }
    });
  }

  sellShare(share: Share) {
    console.log(share);

    this.sharesService
      .sellShare({
        userId: this.userId,
        shareId: share.shareId,
        amount: share.count,
      })
      .subscribe(
        res => this.snackbar.open('Request added', 'X', { duration: 2000 }),
        err =>
          this.snackbar.open("Couldn't create request", 'X', {
            duration: 2000,
          }),
      );
  }
}
