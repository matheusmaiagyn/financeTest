import { useEffect, useState } from "react";
import type { Category } from "../types/category";

import { getAllCategories, createCategory } from "../api/categories";
import { CategoriesTable } from "../components/presentation/CategoriesTable";
import { CategoryCreateModal } from "../components/forms/categories/CategoryForm";

export function CategoriesPage() {
    const [categories, setCategories] = useState<Category[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);

    const fetchCategories = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await getAllCategories();
            setCategories(response);
        } catch (e) {
            console.error(e);
            setError("Falha ao carregar categorias.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchCategories();
    }, []);

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
                    <h1 className="text-2xl font-bold">Categorias</h1>
                    <p className="text-sm text-slate-300">
                        Gerencie as categorias de transações.
                    </p>
                </div>

                <button
                    type="button"
                    onClick={() => setIsCreateModalOpen(true)}
                    className="rounded-lg bg-emerald-600 px-4 py-2 text-sm font-semibold text-white hover:bg-emerald-500"
                >
                    + Nova categoria
                </button>
            </header>

            {error && (
                <div className="rounded-lg border border-red-800/40 bg-red-900/20 px-4 py-3 text-red-200">
                    {error}
                </div>
            )}

            <CategoriesTable rows={categories} />

            <CategoryCreateModal
                open={isCreateModalOpen}
                onClose={() => setIsCreateModalOpen(false)}
                createCategory={createCategory}
                onCreated={fetchCategories}
            />
        </div>
    );
}
