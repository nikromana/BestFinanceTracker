import { TransactionType } from './transaction-type.enum';

export interface Transaction {
  id: number;
  amount: number;
  date: string;
  type: TransactionType;
  description: string | null;
  categoryId: number;
  categoryName: string;
}

export interface CreateTransactionDto {
  amount: number;
  date: string;
  type: TransactionType;
  description: string | null;
  categoryId: number;
}

export interface UpdateTransactionDto {
  amount: number;
  date: string;
  type: TransactionType;
  description: string | null;
  categoryId: number;
}
