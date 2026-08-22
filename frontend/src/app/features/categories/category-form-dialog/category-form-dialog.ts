import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryService } from '../../../../../src/app/core/services/category.spec';
import { Category } from '../../../../models/category.model';
import { TransactionType } from '../../../../models/transaction-type.enum';
@Component({
  selector: 'app-category-form-dialog',
  standalone: true,
  imports: [MatDialogModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './category-form-dialog.html',
  styleUrl: './category-form-dialog.scss'
})
export class CategoryFormDialogComponent {
  private readonly fb = inject(FormBuilder);
  private readonly categoryService = inject(CategoryService);
  private readonly dialogRef = inject(MatDialogRef<CategoryFormDialogComponent>);
  readonly data = inject<Category | null>(MAT_DIALOG_DATA);

  TransactionType = TransactionType;
  isEditMode = !!this.data;

  form = this.fb.group({
    name: [this.data?.name ?? '', Validators.required],
    type: [this.data?.transactionType ?? TransactionType.Expense, Validators.required]
  });

  save(): void {
    if (this.form.invalid) return;

    const value = this.form.getRawValue();
    const dto = { name: value.name!, transactionType: value.type! };

    if (this.isEditMode) {
      this.categoryService.update(this.data!.id, dto).subscribe(() => this.dialogRef.close(true));
    } else {
      this.categoryService.create(dto).subscribe(() => this.dialogRef.close(true));
    }
  }

  cancel(): void {
    this.dialogRef.close(false);
  }
}
