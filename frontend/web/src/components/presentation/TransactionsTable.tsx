import { money } from "../../helpers/Currency";
import { TransactionType, type Transaction } from "../../types/transaction";

type Props = {
  rows: Transaction[];
  onDelete: (id: string) => void;
  deletingId?: string | null;
};

function getTransactionTypeLabel(type: number): string {
  switch (type) {
    case TransactionType.Expense:
      return "Despesa";
    case TransactionType.Income:
      return "Receita";
    default:
      return "Desconhecido";
  }
}

export function TransactionsTable({ rows, onDelete, deletingId }: Props) {
  return (
    <div className="rounded-xl overflow-hidden border border-slate-800 bg-slate-900">
      <table className="w-full table-auto border-collapse">
        <thead>
          <tr className="bg-slate-900/80">
            <th className="border-b border-slate-800 px-4 py-2 text-center">Descrição</th>
            <th className="border-b border-slate-800 px-4 py-2 text-center">Valor</th>
            <th className="border-b border-slate-800 px-4 py-2 text-center">Tipo</th>
            <th className="border-b border-slate-800 px-4 py-2 text-center">Categoria</th>
            <th className="border-b border-slate-800 px-4 py-2 text-center">Usuário</th>
            <th className="border-b border-slate-800 px-4 py-2 text-right">Ações</th>
          </tr>
        </thead>

        <tbody>
          {rows.map((transaction) => {
            const isDeleting = deletingId === transaction.id;

            return (
              <tr key={transaction.id} className="even:bg-slate-800/50">
                <td className="px-4 py-2 text-center">{transaction.description}</td>
                <td className="px-4 py-2 text-center">{money(transaction.amount)}</td>
                <td className="px-4 py-2 text-center">{getTransactionTypeLabel(transaction.transactionType)}</td>
                <td className="px-4 py-2 text-center">{transaction.categoryDescription}</td>
                <td className="px-4 py-2 text-center">{transaction.userName}</td>

                <td className="px-4 py-2 text-right">
                  <button
                    type="button"
                    onClick={() => onDelete(transaction.id)}
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
          })}
        </tbody>
      </table>
    </div>
  );
}
