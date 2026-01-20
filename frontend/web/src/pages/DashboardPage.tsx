import { useEffect, useState } from "react";
import { getTransactionReportForCategories, getTransactionReportForUsers } from "../api/transactions";
import type { TransactionSummaryForCategories, TransactionSummaryForUsers } from "../types/reports";
import { money } from "../helpers/Currency";
import { UserSummaryTable } from "../components/presentation/UsersSummaryTable";
import { CategorySummaryTable } from "../components/presentation/CategorySummaryTable";

export function DashboardPage() {
  const [userSummaryData, setUserSummaryData] = useState<TransactionSummaryForUsers>({ userSummaries: [], totalIncome: 0, totalExpense: 0, netBalance: 0 });
  const [categorySummaryData, setCategorySummaryData] = useState<TransactionSummaryForCategories>({ categorySummaries: [], totalIncome: 0, totalExpense: 0, netBalance: 0 });
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  
  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError(null);

      try { 
        const [users, categories] = await Promise.all([
          getTransactionReportForUsers(),
          getTransactionReportForCategories()
        ]);
        setUserSummaryData(users);
        setCategorySummaryData(categories);
      } catch (error) {
        console.error("Error fetching dashboard data:", error);
        setError("Falha ao carregar dados do dashboard.");
      }
      finally {
        setLoading(false);  
      }
    };

    fetchData();
  }, []);

  const netClass =
    userSummaryData.netBalance < 0 ? "text-red-400" : "text-emerald-400";

  if (loading) {
    return (
      <div className="space-y-4">
        <div className="h-7 w-56 rounded bg-slate-800/60 animate-pulse" />
        <div className="grid gap-6 lg:grid-cols-2">
          <div className="h-44 rounded-xl border border-slate-800 bg-slate-900/60 animate-pulse" />
          <div className="h-44 rounded-xl border border-slate-800 bg-slate-900/60 animate-pulse" />
        </div>
        <div className="h-28 rounded-xl border border-slate-800 bg-slate-900/60 animate-pulse" />
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <header className="space-y-1">
        <h1 className="text-2xl font-bold">Dashboard</h1>
        <p className="text-sm text-slate-300">
          Resumo de receitas, despesas e saldos por usuário e por categoria.
        </p>
      </header>

      {error && (
        <div className="rounded-lg border border-red-800/40 bg-red-900/20 px-4 py-3 text-red-200">
          {error}
        </div>
      )}

      {!error && (
        <>
          <div className="grid gap-6 lg:grid-cols-2">
            <section className="rounded-xl border border-slate-800 bg-slate-900/60 p-4">
              <div className="flex items-center justify-between">
                <div>
                  <h2 className="text-lg font-semibold">
                    Resumo por usuário
                  </h2>
                  <p className="text-sm text-slate-300">
                    Totais de receitas, despesas e saldo por pessoa.
                  </p>
                </div>
              </div>

              <div className="mt-4">
                <UserSummaryTable rows={userSummaryData.userSummaries} />
              </div>
            </section>

            <section className="rounded-xl border border-slate-800 bg-slate-900/60 p-4">
              <div className="flex items-center justify-between">
                <div>
                  <h2 className="text-lg font-semibold">
                    Resumo por categoria
                  </h2>
                  <p className="text-sm text-slate-300">
                    Totais agregados por categoria.
                  </p>
                </div>
              </div>

              <div className="mt-4">
                <CategorySummaryTable rows={categorySummaryData.categorySummaries} />
              </div>
            </section>
          </div>

          <section className="rounded-xl border border-slate-800 bg-slate-900/60 p-4">
            <h2 className="text-lg font-semibold">Total geral</h2>
            <p className="text-sm text-slate-300">
              Soma de todas as pessoas (receitas – despesas).
            </p>

            <div className="mt-4 grid gap-4 sm:grid-cols-3">
              <div className="rounded-lg bg-slate-800/50 p-4">
                <p className="text-xs uppercase tracking-wide text-slate-300">
                  Receitas
                </p>
                <p className="mt-1 text-lg font-semibold">
                  {money(userSummaryData.totalIncome)}
                </p>
              </div>

              <div className="rounded-lg bg-slate-800/50 p-4">
                <p className="text-xs uppercase tracking-wide text-slate-300">
                  Despesas
                </p>
                <p className="mt-1 text-lg font-semibold">
                  {money(userSummaryData.totalExpense)}
                </p>
              </div>

              <div className="rounded-lg bg-slate-800/50 p-4">
                <p className="text-xs uppercase tracking-wide text-slate-300">
                  Saldo líquido
                </p>
                <p className={`mt-1 text-lg font-semibold ${netClass}`}>
                  {money(userSummaryData.netBalance)}
                </p>
              </div>
            </div>
          </section>
        </>
      )}
    </div>
  );
}