import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

export interface CreateStockData {
  userId: string;
  portfolioId: string;
  name: string;
  totalValue: number;
  count: number;
}

@Component({
  selector: 'app-create-stock',
  templateUrl: './create-stock.component.html',
  styleUrls: ['./create-stock.component.scss'],
})
export class CreateStockComponent {
  constructor(
    public dialogRef: MatDialogRef<CreateStockComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CreateStockData,
  ) {}

  onCancelClick() {
    this.dialogRef.close();
  }
}
