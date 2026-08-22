import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/categories/category-list/category-list';
import { TransactionListComponent } from './features/transactions/transaction-list/transaction-list';
import { BudgetListComponent } from './features/budgets/budget-list/budget-list';
import { SummaryComponent } from './features/summary/summary/summary';

export const routes: Routes = [
  { path: 'summary', component: SummaryComponent },
  { path: 'categories', component: CategoryListComponent },
  { path: 'transactions', component: TransactionListComponent },
  { path: 'budgets', component: BudgetListComponent },
  { path: '', redirectTo: 'summary', pathMatch: 'full' }
];
