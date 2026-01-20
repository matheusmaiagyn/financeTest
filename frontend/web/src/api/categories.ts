import http from './http';
import type { Category, CreateCategoryRequest } from '../types/category';

export async function getAllCategories(): Promise<Category[]> {
    const { data } = await http.get<Category[]>('/Category');
    return data;
}

export async function createCategory(category: CreateCategoryRequest): Promise<void> {
    await http.post<Category>('/Category', category);
}

export async function deleteCategory(id: string): Promise<void> {
    await http.delete(`/Category/${id}`);
}