export type TransactionSummaryForUsers = {
    userSummaries: UserSummary[];
    totalIncome: number;
    totalExpense: number;
    netBalance: number;
};

export type UserSummary = {
    userID: string;
    userName: string;
    totalIncome: number;
    totalExpense: number;
    netBalance: number;
}

export type TransactionSummaryForCategories = {
    categorySummaries: CategorySummary[];
    totalIncome: number;
    totalExpense: number;
    netBalance: number;
};

export type CategorySummary = {
    categoryID: string;
    description: string;
    totalIncome: number;
    totalExpense: number;
    netBalance: number;
}