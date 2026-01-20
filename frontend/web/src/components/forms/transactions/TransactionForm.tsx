import { useMemo, useState } from "react";
import { Modal } from "../../ui/Modal";
import type { User } from "../../../types/user";
import { CategoryType, type Category } from "../../../types/category";
import { TransactionType, type CreateTransactionRequest, type TransactionTypeValue } from "../../../types/transaction";

type TxDraft = {
  description: string;
  amount: string;
  transactionType: TransactionTypeValue;
  userID: string;
  categoryID: string;
};

type Props = {
  open: boolean;
  onClose: () => void;
  users: User[];
  categories: Category[];
  onOpenCreateCategory: () => void;

  createTransaction: (payload: CreateTransactionRequest) => Promise<void>;

  onCreatedSuccessfully: () => void;
};

export function TransactionCreateModal({
  open,
  onClose,
  users,
  categories,
  onOpenCreateCategory,
  createTransaction,
  onCreatedSuccessfully,
}: Props) {
  const [form, setForm] = useState<TxDraft>({
    description: "",
    amount: "",
    transactionType: TransactionType.Expense,
    userID: "",
    categoryID: "",
  });

  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const filteredCategories = useMemo(() => {
    return categories.filter((c) => {
      if (form.transactionType === TransactionType.Expense) {
        return c.categoryType === CategoryType.Expense || c.categoryType === CategoryType.Both;
      } else {
        return c.categoryType === CategoryType.Income || c.categoryType === CategoryType.Both;
      }
    });
  }, [categories, form.transactionType]);

  const setField = (patch: Partial<TxDraft>) => setForm((prev) => ({ ...prev, ...patch }));

  const handleSave = async () => {
    setError(null);

    if (!form.description.trim()) return setError("Descrição é obrigatória.");
    if (!form.userID) return setError("Selecione um usuário.");
    if (!form.categoryID) return setError("Selecione uma categoria.");

    const amount = Number(form.amount.replace(",", "."));
    if (!Number.isFinite(amount) || amount <= 0) return setError("Valor inválido.");

    const user = users.find((u: any) => u.id === form.userID);
    if (user?.age != null && user.age < 18 && form.transactionType === TransactionType.Income) {
      return setError("Usuário menor de idade não pode registrar receitas.");
    }

    setSaving(true);
    try {
      await createTransaction({
        description: form.description.trim(),
        amount,
        transactionType: form.transactionType,
        userID: form.userID,
        categoryID: form.categoryID,
      });

      onClose();
      onCreatedSuccessfully();

      setForm({
        description: "",
        amount: "",
        transactionType: TransactionType.Expense,
        userID: "",
        categoryID: "",
      });
    } catch (e) {
      console.error(e);
      setError("Falha ao criar transação.");
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal open={open} onClose={onClose} title="Nova transação">
      {error && (
        <div className="mb-3 rounded-lg border border-red-800/40 bg-red-900/20 px-3 py-2 text-sm text-red-200">
          {error}
        </div>
      )}

      <div className="space-y-3">
        <div className="space-y-1">
          <label className="text-sm text-slate-300">Descrição</label>
          <input
            value={form.description}
            onChange={(e) => setField({ description: e.target.value })}
            className="w-full rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
            placeholder="Ex: Mercado"
          />
        </div>

        <div className="grid grid-cols-1 gap-3 sm:grid-cols-2">
          <div className="space-y-1">
            <label className="text-sm text-slate-300">Valor</label>
            <input
              value={form.amount}
              onChange={(e) => setField({ amount: e.target.value })}
              className="w-full rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
              placeholder="Ex: 120.50"
              inputMode="decimal"
            />
          </div>

          <div className="space-y-1">
            <label className="text-sm text-slate-300">Tipo</label>
            <select
              value={form.transactionType}
              onChange={(e) => {
                const newType = Number(e.target.value) as TransactionTypeValue;
                setField({ transactionType: newType, categoryID: "" });
              }}
              className="w-full rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
            >
              <option value={TransactionType.Expense}>Despesa</option>
              <option value={TransactionType.Income}>Receita</option>
            </select>
          </div>
        </div>

        <div className="space-y-1">
          <label className="text-sm text-slate-300">Usuário</label>
          <select
            value={form.userID}
            onChange={(e) => setField({ userID: e.target.value })}
            className="w-full rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
          >
            <option value="">Selecione…</option>
            {users.map((u: any) => (
              <option key={u.id} value={u.id}>
                {u.name}
              </option>
            ))}
          </select>
        </div>

        <div className="space-y-1">
          <label className="text-sm text-slate-300">Categoria</label>

          <div className="flex gap-2">
            <select
              value={form.categoryID}
              onChange={(e) => setField({ categoryID: e.target.value })}
              className="flex-1 rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
            >
              <option value="">Selecione…</option>
              {filteredCategories.map((c: any) => (
                <option key={c.id} value={c.id}>
                  {c.description}
                </option>
              ))}
            </select>

            <button
              type="button"
              onClick={onOpenCreateCategory}
              className="shrink-0 rounded-lg border border-slate-800 bg-slate-900 px-3 py-2 text-sm hover:bg-slate-800"
            >
              + Categoria
            </button>
          </div>
        </div>

        <div className="flex justify-end gap-2 pt-2">
          <button
            type="button"
            onClick={onClose}
            className="rounded-lg border border-slate-800 bg-slate-900 px-4 py-2 text-sm hover:bg-slate-800"
          >
            Cancelar
          </button>

          <button
            type="button"
            onClick={handleSave}
            disabled={saving}
            className="rounded-lg bg-emerald-600 px-4 py-2 text-sm font-semibold text-white hover:bg-emerald-500 disabled:opacity-60"
          >
            {saving ? "Salvando..." : "Salvar"}
          </button>
        </div>
      </div>
    </Modal>
  );
}
