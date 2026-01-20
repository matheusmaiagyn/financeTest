import { CategoryType, type Category } from "../../types/category";

type Props = {
    rows: Category[];
};

function getCategoryTypeLabel(type: number): string {
    switch (type) {
        case CategoryType.Expense:
            return "Despesa";
        case CategoryType.Income:
            return "Receita";
        case CategoryType.Both:
            return "Ambas";
        default:
            return "Desconhecido";
    }
}

function getCategoryTypeBadgeClass(type: number): string {
    switch (type) {
        case CategoryType.Expense:
            return "bg-red-900/30 text-red-300";
        case CategoryType.Income:
            return "bg-emerald-900/30 text-emerald-300";
        case CategoryType.Both:
            return "bg-blue-900/30 text-blue-300";
        default:
            return "bg-slate-900/30 text-slate-300";
    }
}

export function CategoriesTable({ rows }: Props) {
    return (
        <div className="rounded-xl overflow-hidden border border-slate-800 bg-slate-900">
            <table className="w-full table-auto border-collapse">
                <thead>
                    <tr className="bg-slate-900/80">
                        <th className="border-b border-slate-800 px-4 py-2 text-left">Descrição</th>
                        <th className="border-b border-slate-800 px-4 py-2 text-center">Finalidade</th>
                    </tr>
                </thead>

                <tbody>
                    {rows.length === 0 ? (
                        <tr>
                            <td colSpan={2} className="px-4 py-8 text-center text-slate-500">
                                Nenhuma categoria cadastrada.
                            </td>
                        </tr>
                    ) : (
                        rows.map((category) => (
                            <tr key={category.id} className="even:bg-slate-800/50">
                                <td className="px-4 py-2">{category.description}</td>
                                <td className="px-4 py-2 text-center">
                                    <span className={`rounded-full px-2 py-0.5 text-xs ${getCategoryTypeBadgeClass(category.categoryType)}`}>
                                        {getCategoryTypeLabel(category.categoryType)}
                                    </span>
                                </td>
                            </tr>
                        ))
                    )}
                </tbody>
            </table>
        </div>
    );
}
