import { Component, OnInit, inject, signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CategoryService } from '../../../../../src/app/core/services/category.spec';
import { Category } from '../../../../../src/models/category.model';
import { TransactionType } from '../../../../../src/models/transaction-type.enum';
import { CategoryFormDialogComponent } from '../../../features/categories/category-form-dialog/category-form-dialog';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [MatTableModule, MatButtonModule, MatIconModule, MatToolbarModule],
  templateUrl: './category-list.html',
  styleUrl: './category-list.scss'
})
export class CategoryListComponent implements OnInit {
  private readonly categoryService = inject(CategoryService);
  private readonly dialog = inject(MatDialog);

  categories = signal<Category[]>([]);
  displayedColumns = ['name', 'type', 'actions'];
  TransactionType = TransactionType;

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAll().subscribe(categories => this.categories.set(categories));
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(CategoryFormDialogComponent, { width: '400px', data: null });
    dialogRef.afterClosed().subscribe(result => { if (result) this.loadCategories(); });
  }

  openEditDialog(category: Category): void {
    const dialogRef = this.dialog.open(CategoryFormDialogComponent, { width: '400px', data: category });
    dialogRef.afterClosed().subscribe(result => { if (result) this.loadCategories(); });
  }

  deleteCategory(category: Category): void {
    if (!confirm(`Kategorie "${category.name}" wirklich löschen?`)) return;
    this.categoryService.delete(category.id).subscribe(() => this.loadCategories());
  }
}
