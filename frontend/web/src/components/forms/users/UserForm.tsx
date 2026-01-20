import { useState } from "react";
import { Modal } from "../../ui/Modal";
import type { CreateUserRequest } from "../../../types/user";

type Props = {
    open: boolean;
    onClose: () => void;
    createUser: (payload: CreateUserRequest) => Promise<void>;
    onCreatedSuccessfully: () => void;
};

export function UserCreateModal({
    open,
    onClose,
    createUser,
    onCreatedSuccessfully,
}: Props) {
    const [name, setName] = useState("");
    const [age, setAge] = useState("");
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handleSave = async () => {
        setError(null);

        if (!name.trim()) {
            setError("Nome é obrigatório.");
            return;
        }

        const ageNumber = parseInt(age, 10);
        if (isNaN(ageNumber) || ageNumber <= 0) {
            setError("Idade deve ser um número inteiro positivo.");
            return;
        }

        setSaving(true);
        try {
            await createUser({ name: name.trim(), age: ageNumber });
            onClose();
            onCreatedSuccessfully();
            setName("");
            setAge("");
        } catch (e) {
            console.error(e);
            setError("Falha ao criar usuário.");
        } finally {
            setSaving(false);
        }
    };

    return (
        <Modal open={open} onClose={onClose} title="Nova pessoa">
            {error && (
                <div className="mb-3 rounded-lg border border-red-800/40 bg-red-900/20 px-3 py-2 text-sm text-red-200">
                    {error}
                </div>
            )}

            <div className="space-y-3">
                <div className="space-y-1">
                    <label className="text-sm text-slate-300">Nome</label>
                    <input
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        className="w-full rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
                        placeholder="Ex: João Silva"
                    />
                </div>

                <div className="space-y-1">
                    <label className="text-sm text-slate-300">Idade</label>
                    <input
                        value={age}
                        onChange={(e) => setAge(e.target.value)}
                        className="w-full rounded-lg border border-slate-800 bg-slate-950 px-3 py-2 text-sm outline-none focus:border-slate-600"
                        placeholder="Ex: 25"
                        inputMode="numeric"
                        type="number"
                        min="1"
                    />
                    <p className="text-xs text-slate-500">
                        Menores de 18 anos só podem registrar despesas.
                    </p>
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
