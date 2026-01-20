import { NavLink } from "react-router-dom";

export function NavBar() {
  const linkClass = ({ isActive }: { isActive: boolean }) =>
    isActive ? "text-sm font-semibold text-slate-900" : "text-sm text-slate-600 hover:text-slate-900";

  return (
    <nav className="flex items-center justify-center gap-16 py-3">
      <NavLink to="/" end className={linkClass}>Dashboard</NavLink>
      <NavLink to="/users" className={linkClass}>Usuários</NavLink>
      <NavLink to="/categories" className={linkClass}>Categorias</NavLink>
      <NavLink to="/transactions" className={linkClass}>Transações</NavLink>
    </nav>
  );
}