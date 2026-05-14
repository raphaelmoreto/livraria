# 📚 Livraria - Sistema de Gestão

## 📖 Sobre o Projeto

O **Livraria** é um sistema de controle desenvolvido para gerenciar:

* 📚 Catálogo de livros
* 👥 Clientes
* 💰 Vendas
* 📦 Estoque

O objetivo é auxiliar livrarias na organização de seus processos, facilitando o cadastro de produtos, acompanhamento de vendas e geração de relatórios.

---

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas, promovendo separação de responsabilidades e facilidade de manutenção:

```
Livraria
│
├── Livraria.API           → Camada de apresentação (Web API)
├── Livraria.Application   → Regras de negócio / Serviços
├── Livraria.Domain        → Entidades e interfaces
├── Livraria.Infrastructure→ Acesso a dados e integrações
└── Livraria.IoC           → Injeção de dependência
```

---

## ⚙️ Tecnologias Utilizadas

* **.NET 8**
* **ASP.NET Core Web API**
* **Dapper** (ORM leve)
* **SQL Server**
* **JWT Authentication**
* **Swagger** (documentação da API)
* **EPPlus** (manipulação de arquivos Excel)

---

## 📦 Dependências por Projeto

### 🔹 Livraria.API

* JWT Authentication
* Swagger (Swashbuckle)
* Referência para Domain e IoC

### 🔹 Livraria.Application

* Manipulação de Tokens JWT
* Regras de negócio
* Referência para Domain e Infrastructure

### 🔹 Livraria.Domain

* Entidades
* Interfaces
* Dapper.Contrib

### 🔹 Livraria.Infrastructure

* Dapper
* SQL Server (Microsoft.Data.SqlClient)
* EPPlus (Excel)
* Configurações

### 🔹 Livraria.IoC

* Configuração de Injeção de Dependência
* Integração entre todas as camadas

---

## 🔐 Autenticação

A API utiliza autenticação baseada em **JWT (JSON Web Token)**.

Fluxo básico:

1. Usuário realiza login
2. Recebe um token JWT
3. Envia o token nas requisições autenticadas

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

* .NET 8 SDK
* SQL Server
* Visual Studio ou VS Code

### Passos

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/livraria.git

# Acesse a pasta
cd livraria

# Execute a API
dotnet run --project Livraria.API
```

A API estará disponível em:

```
https://localhost:xxxx/swagger
```

---

## 📊 Funcionalidades

* Cadastro de livros
* Gerenciamento de clientes
* Controle de estoque
* Registro de vendas
* Importação de dados (Excel, CSV, TXT)
* Autenticação de usuários

---

## 🧠 Boas Práticas Aplicadas

* Separação por camadas
* Injeção de dependência
* Uso de interfaces
* Código desacoplado (tentei KK)
* Foco em manutenção e escalabilidade

---

## 📌 Melhorias Futuras

* Adicionar logs estruturados
* Criar dashboard administrativo
* Melhorar controle de permissões

---

## 👨‍💻 Autor

Desenvolvido por **Raphael**

---
