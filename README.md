# 🧾 Product API

API RESTful desenvolvida em **.NET (C#)** para gerenciamento de produtos e categorias.

---

## 🚀 Tecnologias utilizadas

* .NET 7 / 8
* Entity Framework Core
* PostgreSQL
* Swagger (OpenAPI)
* LINQ
* Logging com ILogger

---

## 📦 Funcionalidades

* CRUD de Produtos
* CRUD de Categorias
* Relacionamento (Produto → Categoria)
* Paginação
* Busca por nome
* Validações de negócio
* Logs de ações e erros

---

## ⚙️ Pré-requisitos

Antes de rodar o projeto, você precisa ter instalado:

* [.NET SDK](https://dotnet.microsoft.com/download)
* [PostgreSQL](https://www.postgresql.org/download/)
* Git

---

## 🔧 Configuração do projeto

### 1. Clonar repositório

```bash
git clone https://github.com/SEU_USUARIO/SEU_REPO.git
cd ProductApi
```

---

### 2. Configurar banco de dados

No arquivo `appsettings.json`, altere a connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=products_db;Username=postgres;Password=senha"
}
```

---

### 3. Rodar migrations (se estiver usando)

```bash
dotnet ef database update
```

---

### 4. Rodar o projeto

```bash
dotnet run
```

---

## 🌐 Acessando a API

Por padrão:

```
https://localhost:5095
```

---

## 📘 Swagger (documentação)

Acesse:

```
https://localhost:5095/swagger
```

---

## 📌 Endpoints principais

### 🔹 Produtos

| Método | Rota               |
| ------ | ------------------ |
| GET    | /api/products      |
| GET    | /api/products/{id} |
| POST   | /api/products      |
| PUT    | /api/products/{id} |
| DELETE | /api/products/{id} |

---

### 🔹 Categorias

| Método | Rota                 |
| ------ | -------------------- |
| GET    | /api/categories      |
| GET    | /api/categories/{id} |
| POST   | /api/categories      |
| PUT    | /api/categories/{id} |
| DELETE | /api/categories/{id} |

---

## 🔍 Exemplo de query

```http
GET /api/products?page=1&pageSize=10&search=tv
```

---

## 🧠 Regras de negócio

* SKU deve ser único
* Categoria deve existir
* Preço mínimo para eletrônicos
* Estoque não pode ser negativo

---

## 🧾 Logs

A aplicação utiliza `ILogger` para registrar:

* Criação
* Atualização
* Exclusão
* Erros

---

## ▶️ Melhorias futuras

* Autenticação (JWT)
* Logs em arquivo (Serilog)
* Testes automatizados
* Deploy em nuvem

---

## 👨‍💻 Autor

Matheus Bonaretti
