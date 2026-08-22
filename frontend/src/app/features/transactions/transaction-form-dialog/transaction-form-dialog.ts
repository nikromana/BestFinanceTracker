import { Component, OnInit, inject, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryService } from '../../../core/services/category';
import { TransactionService } from '../../../core/services/transaction';
import { Transaction } from '../../../../models/transaction.model';
import { TransactionType } from '../../../../models/transaction-type.enum';
import { Category } from '../../../../models/category.model';

@Component({
  selector: 'app-transaction-form-dialog',
  standalone: true,
  imports: [MatDialogModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './transaction-form-dialog.html',
  styleUrl: './transaction-form-dialog.scss'
})
export class TransactionFormDialogComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly transactionService = inject(TransactionService);
  private readonly categoryService = inject(CategoryService);
  private readonly dialogRef = inject(MatDialogRef<TransactionFormDialogComponent>);
  readonly data = inject<Transaction | null>(MAT_DIALOG_DATA);

  TransactionType = TransactionType;
  isEditMode = !!this.data;
  categories = signal<Category[]>([]);

  form = this.fb.group({
    amount: [this.data?.amount ?? 0, [Validators.required, Validators.min(0.01)]],
    date: [this.data?.date ?? this.today(), Validators.required],
    type: [this.data?.type ?? TransactionType.Expense, Validators.required],
    description: [this.data?.description ?? ''],
    categoryId: [this.data?.categoryId ?? null, Validators.required]
  });

  ngOnInit(): void {
    this.categoryService.getAll().subscribe(categories => this.categories.set(categories));
  }

  private today(): string {
    return new Date().toISOString().slice(0, 10);
  }

  save(): void {
    if (this.form.invalid) return;

    const value = this.form.getRawValue();
    const dto = {
      amount: value.amount!,
      date: value.date!,
      type: value.type!,
      description: value.description || null,
      categoryId: value.categoryId!
    };

    if (this.isEditMode) {
      this.transactionService.update(this.data!.id, dto).subscribe(() => this.dialogRef.close(true));
    } else {
      this.transactionService.create(dto).subscribe(() => this.dialogRef.close(true));
    }
  }

  cancel(): void {
    this.dialogRef.close(false);
  }
}
