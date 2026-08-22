import { TransactionType } from './transaction-type.enum';
import { Budget } from './budget.model';

export interface CategoryBreakdown {
  categoryId: number;
  categoryName: string;
  type: TransactionType;
  total: number;
}

export interface MonthlySummary {
  year: number;
  month: number;
  totalIncome: number;
  totalExpense: number;
  balance: number;
  categoryBreakdown: CategoryBreakdown[];
  budgetComparisons: Budget[];
  topExpenseCategory: CategoryBreakdown | null;
}
