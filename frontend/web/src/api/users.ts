import http from './http';
import type { User, CreateUserRequest } from '../types/user';

export async function getAllUsers(): Promise<User[]> {
    const { data } = await http.get<User[]>('/User');
    return data;
}

export async function createUser(user: CreateUserRequest): Promise<void> {
    await http.post<User>('/User', user);
}

export async function deleteUser(id: string): Promise<void> {
  await http.delete(`/User/${id}`);
}