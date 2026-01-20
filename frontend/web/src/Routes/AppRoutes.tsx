import { Routes, Route, Navigate } from 'react-router-dom'
import { UsersPage } from '../pages/UsersPage'
import { TransactionsPage } from '../pages/TransactionsPage'
import { DashboardPage } from '../pages/DashboardPage'
import { CategoriesPage } from '../pages/CategoriesPage'
import { AppLayout } from '../components/layout/AppLayout'

export function AppRoutes() {

  return (
    <Routes>
      <Route path="/" element={<AppLayout />}>
        <Route index element={<DashboardPage />} />
        <Route path="users" element={<UsersPage />} />
        <Route path="categories" element={<CategoriesPage />} />
        <Route path="transactions" element={<TransactionsPage />} />
      </Route>
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  )
}