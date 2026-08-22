import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { FormsModule } from '@angular/forms';
import { SummaryService } from '../../../core/services/summary';
import { MonthlySummary } from '../../../../models/summary.model';
import { TransactionType } from '../../../../models/transaction-type.enum';

@Component({
  selector: 'app-summary',
  standalone: true,
  imports: [CommonModule, MatToolbarModule, MatFormFieldModule, MatSelectModule, MatCardModule, MatTableModule, FormsModule],
  templateUrl: './summary.html',
  styleUrl: './summary.scss'
})
export class SummaryComponent implements OnInit {
  private readonly summaryService = inject(SummaryService);

  summary = signal<MonthlySummary | null>(null);
  TransactionType = TransactionType;

  years = Array.from({ length: 6 }, (_, i) => new Date().getFullYear() - i);
  months = Array.from({ length: 12 }, (_, i) => i + 1);

  selectedYear = new Date().getFullYear();
  selectedMonth = new Date().getMonth() + 1;

  categoryColumns = ['category', 'type', 'total'];
  budgetColumns = ['category', 'limit', 'spent', 'remaining', 'status'];

  ngOnInit(): void {
    this.loadSummary();
  }

  loadSummary(): void {
    this.summaryService.getMonthlySummary(this.selectedYear, this.selectedMonth)
      .subscribe(summary => this.summary.set(summary));
  }
}
