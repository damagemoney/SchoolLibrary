# Команды и скриншоты для отчета

## 1. Структура проекта

```powershell
tree /F
```

Скриншот: структура проекта с `Dockerfile`, `docker-compose.yml`, папками `Models`, `Data`, `Services`, `Components`.

## 2. Проверка .NET-проекта без Docker

```powershell
dotnet restore
dotnet build
```

Скриншот: успешная сборка проекта.

## 3. Сборка Docker-образа

```powershell
docker build -t username/school-library-coursework:25 .
```

Скриншот: вывод успешной сборки.

## 4. Запуск через Docker Compose

```powershell
docker compose up -d --build
docker compose ps
```

Скриншот: контейнеры `school_library_app` и `school_library_db` запущены.

## 5. Проверка в браузере

Открыть:

```text
http://localhost:8025
```

Скриншоты:

- главная страница;
- каталог книг;
- добавление книги;
- регистрация читателя;
- выдача книги;
- возврат книги.

## 6. Публикация в Docker Hub

```powershell
docker login
docker push username/school-library-coursework:25
```

Скриншот: страница публичного образа на Docker Hub.

## 7. GitHub/GitLab

```powershell
git init
git add .
git commit -m "Initial school library coursework"
git branch -M main
git remote add origin https://github.com/username/school-library-coursework.git
git push -u origin main
```

Скриншот: публичный репозиторий.
