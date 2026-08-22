import { Component, OnInit, inject, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { BudgetService } from '../../../../app/core/services/budget';
import { CategoryService } from '../../../../app/core/services/category';
import { Budget } from '../../../../models/budget.model';
import { Category } from '../../../../models/category.model';
import { TransactionType } from '../../../../models/transaction-type.enum';

export interface BudgetFormDialogData {
  budget: Budget | null;
  defaultYear: number;
  defaultMonth: number;
}

@Component({
  selector: 'app-budget-form-dialog',
  standalone: true,
  imports: [MatDialogModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './budget-form-dialog.html',
  styleUrl: './budget-form-dialog.scss'
})
export class BudgetFormDialogComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly budgetService = inject(BudgetService);
  private readonly categoryService = inject(CategoryService);
  private readonly dialogRef = inject(MatDialogRef<BudgetFormDialogComponent>);
  readonly dialogData = inject<BudgetFormDialogData>(MAT_DIALOG_DATA);

  isEditMode = !!this.dialogData.budget;
  expenseCategories = signal<Category[]>([]);

  years = Array.from({ length: 6 }, (_, i) => new Date().getFullYear() - i);
  months = Array.from({ length: 12 }, (_, i) => i + 1);

  form = this.fb.group({
    categoryId: [this.dialogData.budget?.categoryId ?? null, Validators.required],
    year: [this.dialogData.budget?.year ?? this.dialogData.defaultYear, Validators.required],
    month: [this.dialogData.budget?.month ?? this.dialogData.defaultMonth, Validators.required],
    limit: [this.dialogData.budget?.limit ?? 0, [Validators.required, Validators.min(0.01)]]
  });

  ngOnInit(): void {
    this.categoryService.getAll().subscribe(categories => {
      this.expenseCategories.set(categories.filter(c => c.transactionType === TransactionType.Expense));
    });
  }

  save(): void {
    if (this.form.invalid) return;

    const value = this.form.getRawValue();
    const dto = { categoryId: value.categoryId!, year: value.year!, month: value.month!, limit: value.limit! };

    const onSuccess = () => this.dialogRef.close(true);
    const onError = () => alert('Fehler beim Speichern. Für diese Kategorie und diesen Monat existiert möglicherweise schon ein Budget.');

    if (this.isEditMode) {
      this.budgetService.update(this.dialogData.budget!.id, dto).subscribe({ next: onSuccess, error: onError });
    } else {
      this.budgetService.create(dto).subscribe({ next: onSuccess, error: onError });
    }
  }

  cancel(): void {
    this.dialogRef.close(false);
  }
}
