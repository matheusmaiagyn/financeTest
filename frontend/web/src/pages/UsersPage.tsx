import { useEffect, useState } from "react";
import type { User } from "../types/user";

import { getAllUsers, createUser, deleteUser } from "../api/users";
import { UsersTable } from "../components/presentation/UsersTable";
import { UserCreateModal } from "../components/forms/users/UserForm";

export function UsersPage() {
    const [users, setUsers] = useState<User[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [deletingId, setDeletingId] = useState<string | null>(null);
    const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);

    const fetchUsers = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await getAllUsers();
            setUsers(response);
        } catch (e) {
            console.error(e);
            setError("Falha ao carregar pessoas.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    const handleDelete = async (id: string) => {
        const user = users.find((u) => u.id === id);
        const msg = user
            ? `Deseja excluir "${user.name}"?\n\nAtenção: Todas as transações desta pessoa também serão excluídas.`
            : "Deseja excluir esta pessoa?";

        if (!window.confirm(msg)) return;

        setDeletingId(id);
        setError(null);

        try {
            await deleteUser(id);
            setUsers((prev) => prev.filter((u) => u.id !== id));
        } catch (e) {
            console.error(e);
            setError("Falha ao excluir pessoa.");
        } finally {
            setDeletingId(null);
        }
    };

    if (loading) {
        return (
            <div className="space-y-4">
                <div className="h-7 w-56 rounded bg-slate-800/60 animate-pulse" />
                <div className="h-44 rounded-xl border border-slate-800 bg-slate-900/60 animate-pulse" />
            </div>
        );
    }

    return (
        <div className="space-y-6">
            <header className="flex items-end justify-between gap-4">
                <div className="space-y-1">
                    <h1 className="text-2xl font-bold">Pessoas</h1>
                    <p className="text-sm text-slate-300">
                        Gerencie as pessoas cadastradas no sistema.
                    </p>
                </div>

                <button
                    type="button"
                    onClick={() => setIsCreateModalOpen(true)}
                    className="rounded-lg bg-emerald-600 px-4 py-2 text-sm font-semibold text-white hover:bg-emerald-500"
                >
                    + Nova pessoa
                </button>
            </header>

            {error && (
                <div className="rounded-lg border border-red-800/40 bg-red-900/20 px-4 py-3 text-red-200">
                    {error}
                </div>
            )}

            <UsersTable rows={users} onDelete={handleDelete} deletingId={deletingId} />

            <UserCreateModal
                open={isCreateModalOpen}
                onClose={() => setIsCreateModalOpen(false)}
                createUser={createUser}
                onCreatedSuccessfully={fetchUsers}
            />
        </div>
    );
}