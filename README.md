# FinanceControl

Aplicação web para **controle de gastos residenciais**, permitindo o cadastro de pessoas, categorias e transações (receitas e despesas), além de **consultas de totais e saldos**.

O projeto foi construído com **API em .NET** e **frontend em React + TypeScript**, mantendo uma separação clara entre camadas e responsabilidades.

---

## ✨ Funcionalidades

### Pessoas
- Criar, listar e remover pessoas
- Ao remover uma pessoa, suas transações associadas são removidas automaticamente

### Categorias
- Criar e listar categorias
- Categorias podem ser configuradas para: **Despesa**, **Receita** ou **Ambas**

### Transações
- Criar e listar transações (receitas/despesas)
- Regras de negócio aplicadas:
  - **Menores de idade (< 18)** podem registrar **apenas despesas**
  - A categoria deve ser compatível com o tipo da transação (ex.: uma transação do tipo *Despesa* não pode usar categoria com finalidade *Receita*)

### Totais
- Consulta de totais por pessoa:
  - Total de receitas
  - Total de despesas
  - Saldo (receitas - despesas)
  - Total geral consolidado

> (Opcional) Totais por categoria podem ser adicionados seguindo a mesma ideia.

---

## 🧱 Arquitetura do projeto

Estrutura separada em dois módulos:

- `backend/` → Web API em .NET (C#)
- `frontend/` → SPA em React + TypeScript

A API aplica validações e regras de negócio no servidor, mantendo o frontend responsável por UI/UX e consumo dos endpoints.

---

## 🚀 Como executar

### Pré-requisitos
- **.NET SDK** (recomendado: 10)
- **Node.js** (recomendado: 18+)
- Gerenciador de pacotes: `npm` ou `pnpm`

---

### 1) Backend (API)

```bash
cd backend
dotnet restore
dotnet run
