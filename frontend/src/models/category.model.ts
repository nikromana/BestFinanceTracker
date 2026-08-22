import { TransactionType } from './transaction-type.enum';

export interface Category {
  id: number;
  name: string;
  transactionType: TransactionType;
}

export interface CreateCategoryDto {
  name: string;
  transactionType: TransactionType;
}

export interface UpdateCategoryDto {
  name: string;
  transactionType: TransactionType;
}
