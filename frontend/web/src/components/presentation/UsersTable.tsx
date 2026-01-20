import type { User } from "../../types/user";

type Props = {
    rows: User[];
    onDelete: (id: string) => void;
    deletingId?: string | null;
};

export function UsersTable({ rows, onDelete, deletingId }: Props) {
    return (
        <div className="rounded-xl overflow-hidden border border-slate-800 bg-slate-900">
            <table className="w-full table-auto border-collapse">
                <thead>
                    <tr className="bg-slate-900/80">
                        <th className="border-b border-slate-800 px-4 py-2 text-left">Nome</th>
                        <th className="border-b border-slate-800 px-4 py-2 text-center">Idade</th>
                        <th className="border-b border-slate-800 px-4 py-2 text-center">Status</th>
                        <th className="border-b border-slate-800 px-4 py-2 text-right">Ações</th>
                    </tr>
                </thead>

                <tbody>
                    {rows.length === 0 ? (
                        <tr>
                            <td colSpan={4} className="px-4 py-8 text-center text-slate-500">
                                Nenhuma pessoa cadastrada.
                            </td>
                        </tr>
                    ) : (
                        rows.map((user) => {
                            const isDeleting = deletingId === user.id;
                            const isMinor = user.age < 18;

                            return (
                                <tr key={user.id} className="even:bg-slate-800/50">
                                    <td className="px-4 py-2">{user.name}</td>
                                    <td className="px-4 py-2 text-center">{user.age}</td>
                                    <td className="px-4 py-2 text-center">
                                        {isMinor ? (
                                            <span className="rounded-full bg-amber-900/30 px-2 py-0.5 text-xs text-amber-300">
                                                Menor de idade
                                            </span>
                                        ) : (
                                            <span className="rounded-full bg-emerald-900/30 px-2 py-0.5 text-xs text-emerald-300">
                                                Maior de idade
                                            </span>
                                        )}
                                    </td>

                                    <td className="px-4 py-2 text-right">
                                        <button
                                            type="button"
                                            onClick={() => onDelete(user.id)}
                                            disabled={isDeleting}
                                            className={`rounded-md px-3 py-1 text-sm ${isDeleting
                                                    ? "cursor-not-allowed bg-red-900/10 text-red-200/60"
                                                    : "bg-red-900/20 text-red-200 hover:bg-red-900/30"
                                                }`}
                                        >
                                            {isDeleting ? "Excluindo..." : "Excluir"}
                                        </button>
                                    </td>
                                </tr>
                            );
                        })
                    )}
                </tbody>
            </table>
        </div>
    );
}
