import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDialog } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../../../core/services/transaction';
import { Transaction } from '../../../../models/transaction.model';
import { TransactionType } from '../../../../models/transaction-type.enum';
import { TransactionFormDialogComponent } from '../transaction-form-dialog/transaction-form-dialog';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatToolbarModule, MatFormFieldModule, MatSelectModule, FormsModule],
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.scss'
})
export class TransactionListComponent implements OnInit {
  private readonly transactionService = inject(TransactionService);
  private readonly dialog = inject(MatDialog);

  transactions = signal<Transaction[]>([]);
  displayedColumns = ['date', 'category', 'description', 'type', 'amount', 'actions'];
  TransactionType = TransactionType;

  years = Array.from({ length: 6 }, (_, i) => new Date().getFullYear() - i);
  months = Array.from({ length: 12 }, (_, i) => i + 1);

  selectedYear = new Date().getFullYear();
  selectedMonth = new Date().getMonth() + 1;

  ngOnInit(): void {
    this.loadTransactions();
  }

  loadTransactions(): void {
    this.transactionService.getAll(this.selectedYear, this.selectedMonth)
      .subscribe(transactions => this.transactions.set(transactions));
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(TransactionFormDialogComponent, { width: '450px', data: null });
    dialogRef.afterClosed().subscribe(result => { if (result) this.loadTransactions(); });
  }

  openEditDialog(transaction: Transaction): void {
    const dialogRef = this.dialog.open(TransactionFormDialogComponent, { width: '450px', data: transaction });
    dialogRef.afterClosed().subscribe(result => { if (result) this.loadTransactions(); });
  }

  deleteTransaction(transaction: Transaction): void {
    if (!confirm('Transaktion wirklich löschen?')) return;
    this.transactionService.delete(transaction.id).subscribe(() => this.loadTransactions());
  }
}
