import { useState } from "react";
import { Modal } from "../../ui/Modal";
import { CategoryType, type CreateCategoryRequest, type CategoryTypeValue } from "../../../types/category";

type Props = {
  open: boolean;
  onClose: () => void;

  createCategory: (payload: CreateCategoryRequest) => Promise<void>;

  onCreated: () => void;
};

export function CategoryCreateModal({ open, onClose, onCreated, createCategory }: Props) {
  const [description, setDescription] = useState("");
  const [categoryType, setCategoryType] = useState<CategoryTypeValue>(CategoryType.Both);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSave = async () => {
    setError(null);

    if (!description.trim()) {
      setError("Descrição é obrigatória.");
      return;
    }

    setSaving(true);
    try {
      await createCategory({ description: description.trim(), categoryType });
      onCreated();
      setDescription("");
      setCategoryType(CategoryType.Both);
      onClose();
    } catch (e) {
      console.error(e);
      setError("Falha ao criar categoria.");
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal open={open} onClose={onClose} title="Nova categoria" zClassName="z-50">
      {error && (
        <div className="mb-3 rounded-lg border border-red-800/40 bg-red-900/20 px-3 py-2 text-sm text-red-200">
          {error}
        </div>
      )}

      <div className="space-y-3">
        <div className="space-y-1">
          <label className="text-sm text-slate-300">Descrição</label>
          <input
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="w-full rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
            placeholder="Ex: Alimentação"
          />
        </div>

        <div className="space-y-1">
          <label className="text-sm text-slate-300">Finalidade</label>
          <select
            value={categoryType}
            onChange={(e) => setCategoryType(Number(e.target.value) as CategoryTypeValue)}
            className="w-full rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
          >
            <option value={CategoryType.Expense}>Despesa</option>
            <option value={CategoryType.Income}>Receita</option>
            <option value={CategoryType.Both}>Ambas</option>
          </select>
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
