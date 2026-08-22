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
import { BudgetService } from '../../../../../src/app/core/services/budget';
import { Budget } from '../../../../models/budget.model';
import { BudgetFormDialogComponent } from '../budget-form-dialog/budget-form-dialog';

@Component({
  selector: 'app-budget-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatToolbarModule, MatFormFieldModule, MatSelectModule, FormsModule],
  templateUrl: './budget-list.html',
  styleUrl: './budget-list.scss'
})
export class BudgetListComponent implements OnInit {
  private readonly budgetService = inject(BudgetService);
  private readonly dialog = inject(MatDialog);

  budgets = signal<Budget[]>([]);
  displayedColumns = ['category', 'limit', 'spent', 'remaining', 'status', 'actions'];

  years = Array.from({ length: 6 }, (_, i) => new Date().getFullYear() - i);
  months = Array.from({ length: 12 }, (_, i) => i + 1);

  selectedYear = new Date().getFullYear();
  selectedMonth = new Date().getMonth() + 1;

  ngOnInit(): void {
    this.loadBudgets();
  }

  loadBudgets(): void {
    this.budgetService.getAll(this.selectedYear, this.selectedMonth)
      .subscribe(budgets => this.budgets.set(budgets));
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(BudgetFormDialogComponent, {
      width: '400px',
      data: { budget: null, defaultYear: this.selectedYear, defaultMonth: this.selectedMonth }
    });
    dialogRef.afterClosed().subscribe(result => { if (result) this.loadBudgets(); });
  }

  openEditDialog(budget: Budget): void {
    const dialogRef = this.dialog.open(BudgetFormDialogComponent, {
      width: '400px',
      data: { budget, defaultYear: budget.year, defaultMonth: budget.month }
    });
    dialogRef.afterClosed().subscribe(result => { if (result) this.loadBudgets(); });
  }

  deleteBudget(budget: Budget): void {
    if (!confirm(`Budget für "${budget.categoryName}" wirklich löschen?`)) return;
    this.budgetService.delete(budget.id).subscribe(() => this.loadBudgets());
  }
}
