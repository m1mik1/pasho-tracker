# PaSho Tracker

**Система керування завданнями та проектами**

* **Backend:** ASP.NET Core 7 + EF Core + PostgreSQL
* **Frontend:** React.js + Next.js  + shadcn/ui + TailwindCSS

---

## 🚀 Вміст

1. [Передумови](#-передумови)
2. [Клонування репозиторію](#-клонування-репозиторію)
3. [Конфігурація](#-конфігурація)

   * [Backend](#backend)
   * [Frontend](#frontend)
   * [Launch Settings (опціонально)](#launch-settings-опціонально)
4. [Запуск](#-запуск)
5. [Перевірка API](#-перевірка-api)
6. [Демонстрація функціоналу](#-демонстрація-функціоналу)
7. [Архітектура](#-архітектура)
8. [Docker Deployment](#-docker-deployment)
9. [Звіт (Lab12)](#-звіт-lab12)
10. [Контакти та внески](#-контакти-та-внески)

---

## 📋 Передумови

* .NET 7 SDK
* Node.js ≥ 16
* Docker та Docker Compose
* Git CLI
* PostgreSQL (або Docker для локального запуску)

---

## 📥 Клонування репозиторію

```bash
git clone https://github.com/m1mik1/pasho-tracker.git
cd pasho-tracker
```

---

## 🔧 Конфігурація

### Backend

1. Перейменуйте `backend/appsettings.json.example` → `backend/appsettings.json`.
2. Заповніть параметри підключення та SMTP. **Не зберігайте справжні паролі чи email в репозиторії!**

```json
{
  "Logging": {
    "LogLevel": { "Default": "Information", "Microsoft.AspNetCore": "Warning" }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=<YOUR_DB_NAME>;Username=<USER>;Password=<PASSWORD>"
  },
  "Email": {
    "SmtpServer": "smtp.<YOUR_PROVIDER>.com",
    "Port": 587,
    "Sender": "<your_email@example.com>",
    "Password": "<app_password>"
  },
  "Frontend": {
    "BaseUrl": "http://localhost:3000"
  },
  "Jwt": {
    "Key": "<YOUR_SUPER_SECRET_KEY>",
    "Issuer": "PaShoTracker",
    "Audience": "PaShoClient"
  }
}
```

### Frontend

В директорії `frontend` створіть файл `.env`:

```env
VITE_API_URL=http://localhost:5000
```

### Launch Settings (опціонально)

Файл `.vscode/launch.json` або `Properties/launchSettings.json` для локального дебагу:

```json
{
  "iisSettings": { ... },
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "http://localhost:5178",
      "environmentVariables": { "ASPNETCORE_ENVIRONMENT": "Development" }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7248;http://localhost:5178",
      "environmentVariables": { "ASPNETCORE_ENVIRONMENT": "Development" }
    }
  }
}
```

---

## ▶️ Запуск

### 1. Запуск БД

```bash
docker-compose up -d
```

### 2. Backend

```bash
cd backend
dotnet ef database update
dotnet run
```

API буде доступний за `http://localhost:5000` (або за launchSettings `http://localhost:5178`)

### 3. Frontend

```bash
cd frontend
npm install
npm run dev
```

Фронтенд: `http://localhost:3000`

---

## 🔍 Перевірка API

```http
GET http://localhost:5178/api/categories
Authorization: Bearer <YOUR_JWT_TOKEN>
```

---

## 🎬 Демонстрація функціоналу

1. Авторизація (JWT)
2. CRUD для категорій
3. CRUD для задач
4. Drag & Drop між статусами

<details>
<summary>Приклад скріншотів (помістіть у `docs/screens` та оновіть шляхи)</summary>

![Login](/docs/screens/login.png)
![Dashboard](/docs/screens/dashboard.png)

</details>

---

## 🏗 Архітектура

### Backend

* **ASP.NET Core Web API**
* Шари:

  * `Controllers`
  * `Services` (бізнес-логіка)
  * `Repositories` (дані через EF Core)
* Патерни: SOLID, DTO, Unit of Work
* Email SMTP через `IEmailService`
* Аутентифікація через JWT

### Frontend

* **React** + **Vite**
* **TailwindCSS** + **shadcn/ui**
* Запити через `lib/api` (відокремлені функції)
* Стан: React Context + React Query
* Drag & Drop: `@dnd-kit`

---

## 📦 Docker Deployment

```bash
# Побудова та запуск backend
docker build -t pasho-backend ./backend
docker run -d -p 5000:5000 --env-file backend/appsettings.json pasho-backend

# Побудова та запуск frontend
cd frontend
docker build -t pasho-frontend .
docker run -d -p 3000:3000 pasho-frontend
```

---

### Короткий опис функціоналу

Проект дозволяє користувачеві:

* Створювати та редагувати категорії задач.
* Додавати, редагувати та видаляти задачі.
* Отримувати email-сповіщення про підтвердження данних (за налаштувань SMTP).

### Скріни роботи програми

* `<img width="1512" alt="image" src="https://github.com/user-attachments/assets/6c33855f-c0f1-4273-a218-bdae60449697" />` — головна сторінка
* `<img width="1510" alt="image" src="https://github.com/user-attachments/assets/bab2c268-5dd2-4111-9074-da3b1ff3e783" />` — вікно авторизації
* `<img width="1511" alt="image" src="https://github.com/user-attachments/assets/fff82f13-b242-41a2-b014-cef8f7bcc924" />` — сторінка реєстрації

---

## 🤝 Контакти та внески

Якщо маєте питання чи знайдете баги, створюйте issue або надсилайте PR.
