# SchoolLibraryApp — курсовой проект «Школьная библиотека»

Тема: создание web-приложения для учета школьной библиотеки.  
Вариант: 25.  
Порт приложения: `8025`.

## Назначение

Приложение позволяет вести простую школьную библиотеку:

- просматривать каталог книг;
- добавлять новые книги;
- регистрировать читателей;
- выдавать книгу читателю;
- отмечать возврат книги;
- смотреть активные выдачи и статистику.

## Технологии

- .NET 8;
- ASP.NET Core Blazor Server;
- Entity Framework Core CodeFirst;
- PostgreSQL;
- FluentValidation;
- Docker и Docker Compose.

## Структура проекта

```text
Components/        Razor-компоненты и страницы Blazor
Data/              DbContext, миграция, начальные данные
Models/            сущности предметной области
Repositories/      слой доступа к данным
Services/          бизнес-логика приложения
Validators/        правила валидации форм
ViewModels/        модели форм
Dockerfile         multi-stage сборка приложения
docker-compose.yml запуск app + PostgreSQL
```

## Запуск через Docker Compose

Открыть PowerShell в корне проекта и выполнить:

```powershell
docker compose up -d --build
```

Проверить контейнеры:

```powershell
docker compose ps
```

Открыть приложение:

```text
http://localhost:8025
```

## Остановка

```powershell
docker compose down
```

Для удаления БД вместе с volume:

```powershell
docker compose down -v
```

## Публикация образа в Docker Hub

Перед публикацией заменить `username` в `docker-compose.yml` или создать `.env` на основе `.env.example`:

```powershell
copy .env.example .env
notepad .env
```

Затем выполнить:

```powershell
docker login
docker compose build
docker compose push app
```

Или вручную:

```powershell
docker build -t username/school-library-coursework:25 .
docker push username/school-library-coursework:25
```
