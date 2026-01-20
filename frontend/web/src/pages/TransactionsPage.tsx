import { useEffect, useState } from "react";
import type { Transaction } from "../types/transaction";
import type { User } from "../types/user";
import type { Category } from "../types/category";

import { getAllTransactions, deleteTransaction, createTransaction } from "../api/transactions";
import { getAllUsers } from "../api/users";
import { getAllCategories, createCategory } from "../api/categories";

import { TransactionsTable } from "../components/presentation/TransactionsTable";
import { TransactionCreateModal } from "../components/forms/transactions/TransactionForm";
import { CategoryCreateModal } from "../components/forms/categories/CategoryForm";

export function TransactionsPage() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [users, setUsers] = useState<User[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [deletingId, setDeletingId] = useState<string | null>(null);

  const [isCreateTxOpen, setIsCreateTxOpen] = useState(false);
  const [isCreateCategoryOpen, setIsCreateCategoryOpen] = useState(false);

  const fetchAll = async () => {
    setLoading(true);
    setError(null);

    try {
      const [txs, us, cats] = await Promise.all([
        getAllTransactions(),
        getAllUsers(),
        getAllCategories(),
      ]);

      setTransactions(txs);
      setUsers(us);
      setCategories(cats);
    } catch (e) {
      console.error(e);
      setError("Falha ao carregar dados.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchAll();
  }, []);

  const handleDelete = async (id: string) => {
    const ok = window.confirm("Deseja excluir esta transação?");
    if (!ok) return;

    setDeletingId(id);
    setError(null);

    try {
      await deleteTransaction(id);
      setTransactions((prev) => prev.filter((t) => t.id !== id));
    } catch (e) {
      console.error(e);
      setError("Falha ao excluir transação.");
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
          <h1 className="text-2xl font-bold">Transações</h1>
          <p className="text-sm text-slate-300">Transações financeiras registradas no sistema.</p>
        </div>

        <button
          type="button"
          onClick={() => setIsCreateTxOpen(true)}
          className="rounded-lg bg-emerald-600 px-4 py-2 text-sm font-semibold text-white hover:bg-emerald-500"
        >
          + Nova transação
        </button>
      </header>

      {error && (
        <div className="rounded-lg border border-red-800/40 bg-red-900/20 px-4 py-3 text-red-200">
          {error}
        </div>
      )}

      <TransactionsTable rows={transactions} onDelete={handleDelete} deletingId={deletingId} />

      <TransactionCreateModal
        open={isCreateTxOpen}
        onClose={() => setIsCreateTxOpen(false)}
        users={users}
        categories={categories}
        onOpenCreateCategory={() => setIsCreateCategoryOpen(true)}
        createTransaction={createTransaction}
        onCreatedSuccessfully={fetchAll}
      />

      <CategoryCreateModal
        open={isCreateCategoryOpen}
        onClose={() => setIsCreateCategoryOpen(false)}
        createCategory={createCategory}
        onCreated={async () => {
          const cats = await getAllCategories();
          setCategories(cats);
        }}
      />
    </div>
  );
}
