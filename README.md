# Desafio - Sistema de Cadastro de Usuários

Este é um projeto que utiliza **.NET 8 (backend)** e **AngularJS 1.7.8 (frontend)** para criar um sistema de login e cadastro de usuários. A aplicação está usando o **Docker** e e o **Docker Compose**.

## 📌 Funcionalidades

- Login autenticado (usuário fixo: `SISTEMA`, senha: `canditado123`)
- Cadastro de usuários com os campos:
  - **Código**
  - **Nome** (obrigatório)
  - **CPF** (obrigatório)
  - **Endereço** (opcional)
  - **Telefone** (opcional)
- Backend em **.NET 8** usando **SQLite**
- Frontend em **AngularJS 1.7.8** com **Bootstrap 5.1.3**
- Containers do backend e frontend rodando na mesma **rede Docker**

---

## 📂 Estrutura do Projeto

```
/
├── SistemaAPI/       # Backend em .NET 8 (Web API)
├── frontend/         # Frontend em AngularJS
├── docker-compose.yml # Arquivo de orquestração do Docker
├── README.md         # Documentação do projeto
```

---

## 🚀 Como Rodar o Projeto com Docker

### **1️⃣ Clonar o Repositório**

```sh
git clone <URL_DO_REPOSITORIO>
cd <NOME_DO_PROJETO>
```

### **2️⃣ Construir e Rodar os Containers**

```sh
docker-compose up --build
```

Ou, para rodar em segundo plano:

```sh
docker-compose up -d --build
```

Isso iniciará:

- **Backend**: Disponível em `http://localhost:5198`
- **Frontend**: Disponível em `http://localhost:3000`

### **3️⃣ Parar os Containers**

```sh
docker-compose down
```
