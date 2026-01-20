import { Outlet } from "react-router-dom";
import { NavBar } from "./NavBar";

export function AppLayout() {
  return (
    <div className="min-h-screen">
        <header className="sticky top-0 bg-white backdrop-blur border-b">
            <div className="px-4">
                <NavBar />
            </div>
        </header>
        <main className="mx-auto max-w-7xl p-4">
            <Outlet />
        </main>
    </div>
  );
}