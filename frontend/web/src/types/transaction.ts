export const TransactionType = {
    Expense: 1,
    Income: 2,
} as const;

export type TransactionTypeValue = typeof TransactionType[keyof typeof TransactionType];

export type Transaction = {
    id: string;
    description: string;
    amount: number;
    transactionType: TransactionTypeValue;
    categoryID: string;
    categoryDescription: string;
    userID: string;
    userName: string;
}

export type CreateTransactionRequest = {
    description: string;
    amount: number;
    transactionType: TransactionTypeValue;
    categoryID: string;
    userID: string;
}