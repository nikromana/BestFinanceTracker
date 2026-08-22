export interface Budget {
  id: number;
  categoryId: number;
  categoryName: string;
  year: number;
  month: number;
  limit: number;
  spent: number;
  remaining: number;
  isOverBudget: boolean;
}

export interface CreateBudgetDto {
  categoryId: number;
  year: number;
  month: number;
  limit: number;
}

export interface UpdateBudgetDto {
  categoryId: number;
  year: number;
  month: number;
  limit: number;
}
