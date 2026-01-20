import type { UserSummary } from "../../types/reports";
import { money } from "../../helpers/Currency";

export function UserSummaryTable({ rows }: { rows: UserSummary[] }) {
    return (
      <div className="rounded-xl overflow-hidden border border-slate-800 bg-slate-900">
          <table className="w-full table-auto border-collapse">
            <thead>
              <tr className="bg-slate-900/80">
                <th className="border-b border-slate-800 px-4 py-2 text-center">Nome do usuário</th>
                <th className="border-b border-slate-800 px-4 py-2 text-center">Receitas</th>
                <th className="border-b border-slate-800 px-4 py-2 text-center">Despesas</th>
                <th className="border-b border-slate-800 px-4 py-2 text-center">Saldo Líquido</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((summary) => (
                <tr key={summary.userID} className="even:bg-slate-800/50">
                  <td className="px-4 py-2 text-center">{summary.userName}</td>
                  <td className="px-4 py-2 text-center">{money(summary.totalIncome)}</td>
                  <td className="px-4 py-2 text-center">{money(summary.totalExpense)}</td>
                  <td className="px-4 py-2 text-center">{money(summary.netBalance)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
  );
} 
