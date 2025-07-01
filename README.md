# ⏱️ TimeTrackr.API

**TimeTrackr** é uma API REST para controle de ponto e produtividade de colaboradores. Projetado com foco em boas práticas de arquitetura, uso do padrão CQRS, validação, tratamento de erros e extensibilidade com monitoramento e autenticação.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core + PostgreSQL
- MediatR (CQRS)
- FluentValidation
- FluentResults (padronização de respostas)
- Docker + Docker Compose
- Prometheus + Grafana (monitoramento)
- PgAdmin (administração de banco)
- AutoMapper
- Swagger (documentação)

---

## 📦 Estrutura do Projeto

```
TimeTrackr/
├── Core/               # Entidades, Interfaces, Enums e Erros
├── Application/        # Casos de uso, comandos, queries e validadores
├── Infrastructure/     # Persistência, EF Core, Repositórios
├── WebAPI/             # Camada de apresentação (controllers, middlewares, DI)
├── docker-compose.yml  # Serviços (API, banco, monitoramento)
└── README.md
```

---

## ⚙️ Como Rodar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)
- [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet)

### 1. Clonar o projeto

```bash
git clone https://github.com/seu-usuario/TimeTrackr.API.git
cd TimeTrackr.API
```

### 2. Gerar o banco e subir os serviços

```bash
docker-compose up --build
```

### 3. Acessos úteis

- **API Swagger**: http://localhost:5050/swagger
- **PgAdmin**: http://localhost:5051  
  - Email: `admin@admin.com`  
  - Senha: `admin`
- **Prometheus**: http://localhost:9090  
- **Grafana**: http://localhost:3000  
  - Usuário: `admin`  
  - Senha: `admin`

---

## 📚 Funcionalidades

- Cadastro de usuários com papéis (Admin / Funcionário)
- Registro diário de ponto (`StartWork`, `Lunch`, `EndWork`)
- Consulta de registros por data e colaborador
- Validações com mensagens amigáveis
- Erros estruturados com ProblemDetails
- Observabilidade com Prometheus + Grafana
