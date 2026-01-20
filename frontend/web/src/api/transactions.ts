import http from "./http";
import type { Transaction, CreateTransactionRequest } from "../types/transaction";
import type { TransactionSummaryForUsers, TransactionSummaryForCategories } from "../types/reports";

export async function getAllTransactions(): Promise<Transaction[]> {
    const { data } = await http.get<Transaction[]>('/Transaction');
    return data;
}

export async function createTransaction(transaction: CreateTransactionRequest): Promise<void> {
    await http.post<Transaction>('/Transaction', transaction);
}

export async function deleteTransaction(id: string): Promise<void> {
    await http.delete(`/Transaction/${id}`);
}

export async function getTransactionReportForUsers(): Promise<TransactionSummaryForUsers> {
    const { data } = await http.get<TransactionSummaryForUsers>('/Transaction/GetTransactionSummaryForUsers');
    return data;
}

export async function getTransactionReportForCategories(): Promise<TransactionSummaryForCategories> {
    const { data } = await http.get<TransactionSummaryForCategories>('/Transaction/GetTransactionSummaryForCategories');
    return data;
}