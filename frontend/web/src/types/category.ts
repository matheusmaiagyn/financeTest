export const CategoryType = {
    Expense: 1,
    Income: 2,
    Both: 3,
} as const;

export type CategoryTypeValue = typeof CategoryType[keyof typeof CategoryType];

export type Category = {
    id: string;
    description: string;
    categoryType: CategoryTypeValue;
}

export type CreateCategoryRequest = {
    description: string;
    categoryType: CategoryTypeValue;
}