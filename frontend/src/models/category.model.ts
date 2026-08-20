import { TransactionType } from './transaction-type.enum';

export interface Category {
  id: number;
  name: string;
  type: TransactionType;
}

export interface CreateCategoryDto {
  name: string;
  type: TransactionType;
}

export interface UpdateCategoryDto {
  name: string;
  type: TransactionType;
}
