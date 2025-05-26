# PaSho Tracker

Система керування завданнями та проектами  
**Backend:** ASP.NET Core + EF Core + PostgreSQL  
**Frontend:** React (Vite) + shadcn/ui + TailwindCSS

---

## Передумови

- .NET 7 SDK  
- Node.js ≥ 16  
- Docker та Docker Compose  
- Git

---

## Клонування репозиторію

```bash
git clone https://github.com/m1mik1/pasho-tracker.git
cd pasho-tracker
```

---

## Конфігурація

### Бекенд

У файлі `backend/appsettings.json` вкажіть:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=pasho_tracker;Username=admin;Password=adminpassword"
  },
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "Sender": "verifyfreak@gmail.com",
    "Password": "jcjo njlu yqug ubeb"
  },
  "Frontend": {
    "BaseUrl": "http://localhost:3000"
  },
  "Jwt": {
    "Key": "VerySecureKey_1234567890_!@#$%_SuperLong",
    "Issuer": "PaShoTracker",
    "Audience": "PaShoClient"
  }
}
```

### Фронтенд

У папці `frontend` створи файл `.env`:

```env
VITE_API_URL=http://localhost:5000
```

### Docker Compose

У корені репозиторію файл `docker-compose.yml`:

```yaml
version: "3.8"

services:
  postgres:
    image: postgres:latest
    container_name: pasho-tracker-db
    restart: always
    environment:
      POSTGRES_USER: admin
      POSTGRES_PASSWORD: adminpassword
      POSTGRES_DB: pasho_tracker
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:
```

---

## Запуск

### База даних

```bash
docker-compose up -d
```

### Бекенд

```bash
cd backend
dotnet ef database update
dotnet run
```

API запуститься на `http://localhost:5000`

### Фронтенд

```bash
cd frontend
npm install
npm run dev
```

Фронтенд: `http://localhost:3000`

---

## Перевірка API

```http
GET http://localhost:5000/weatherforecast
Accept: application/json
```

---

## Демонстрація функціоналу

1. Авторизація  
2. Створення категорії  
3. Додавання задач  
4. Перетягування задач між колонками  

(Скріншоти вставити сюди у форматі Markdown після збереження у папці `docs/screens`)

---

## Архітектура

### Backend

- ASP.NET Core Web API
- Слоями: `Controllers` → `Services` → `Repositories` → `EF Core`
- DTO, SOLID, JWT, Email SMTP, PostgreSQL

### Frontend

- React + Vite
- shadcn/ui компоненти
- TailwindCSS
- Запити до API через `lib/api`
- Стейт: React Context + React Query

---

## Розгортання через Docker

```bash
# Бекенд
docker build -t pasho-backend ./backend
docker run -d -p 5000:5000 --env-file backend/appsettings.json pasho-backend

# Фронтенд
cd frontend
docker build -t pasho-frontend .
docker run -d -p 3000:3000 pasho-frontend
```

---

## Структура звіту (ЛР-12)

- Титульний аркуш  
- Мета роботи  
- Інструкція з запуску  
- Демонстрація функціоналу (зі скрінами)  
- Схема архітектури (draw.io, якщо треба)  
- Висновки  
- README як частина оцінювання
