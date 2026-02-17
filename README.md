# Together Hub. Социальная сеть для одаренных


## API Endpoints
### Общий формат ошибок

Проект возвращает ошибки в стиле `ProblemDetails`:

```json
{
  "title": "SomeExceptionName",
  "detail": "Human-readable message",
  "status": 401,
  "instance": "/api/auth/login",
  "traceId": "0HM..."
}
```

## Auth

### POST `/api/auth/register`

Регистрация пользователя и выдача JWT.

**Auth:** не требуется

**Request body** (`RegisterUserRequestDto`)

```json
{
  "firstName": "John",
  "lastName": "Doe",
  "username": "johndoe",
  "email": "johndoe@example.com",
  "password": "qwerty"
}
```

**Response**

* `200 OK` (`RegisterUserResult`)

```json
{
  "result": {
    "username": "johndoe",
    "email": "johndoe@example.com",
    "jwtToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

**Errors**

* `400 Bad Request` — некорректный JSON / middleware-валидация
* `401 Unauthorized` — email или username уже заняты

---

### POST `/api/auth/login`

Логин по email + password, выдача JWT.

**Auth:** не требуется

**Request body** (`LoginRequestDto`)

```json
{
  "email": "johndoe@example.com",
  "password": "qwerty"
}
```

**Response**

* `200 OK` (`LoginUserResult`)

```json
{
  "result": {
    "username": "johndoe",
    "email": "johndoe@example.com",
    "jwtToken": "qwerty"
  }
}
```

**Errors**

* `400 Bad Request` — некорректный JSON / middleware-валидация
* `401 Unauthorized` — неправильный email или пароль (`UserWrongEmailException`, `UserWrongPasswordException`)

---

## Topics

**Все эндпойнты требуют:** `Authorization: Bearer <JWT>`

### GET `/api/topics`

Получение списка топиков.

**Response**

* `200 OK`

```json
{
  "topics": [
    {
      "id": "4a8b3e2b-0e1b-4e8c-8c5a-7d0f7e1a2c11",
      "title": "Инновации 2030",
      "summary": "Обзор ключевых технологических инноваций",
      "topicType": "Конференция",
      "location": { "city": "Москва", "street": "Пушкина, 10" },
      "eventStart": "2026-03-01T10:00:00Z"
    }
  ]
}
```

**Errors**

* `401 Unauthorized` — нет/невалидный токен

---

### GET `/api/topics/{id}`

Получение одного топика по id.

**Path params**

* `id` — `uuid`

**Response**

* `200 OK`

```json
{
  "topic": {
    "id": "4a8b3e2b-0e1b-4e8c-8c5a-7d0f7e1a2c11",
    "title": "Инновации 2030",
    "summary": "Обзор ключевых технологических инноваций",
    "topicType": "Конференция",
    "location": { 
      "city": "Москва", 
      "street": "Пушкина, 10"
    },
    "eventStart": "2026-03-01T10:00:00Z"
  }
}
```

**Errors**

* `401 Unauthorized`
* `404 Not Found` — топик не найден или помечен удалённым

---

### POST `/api/topics`

Создание топика.

**Request body** 

```json
{
  "title": "Будущие технологии",
  "summary": "Анализ будущих трендов",
  "topicType": "Семинар",
  "location": { 
    "city": "Санкт-Петербург", 
    "street": "Советская, 12"
  },
  "eventStart": "2026-03-05T12:00:00Z"
}
```

**Response**

* `201 Created`

```json
{
  "id": "b6d5c7f6-9cdd-4b2a-9d67-8bb7e1e5a4f0",
  "title": "Будущие технологии",
  "summary": "Анализ будущих трендов",
  "topicType": "Семинар",
  "location": { 
    "city": "Санкт-Петербург", 
    "street": "Советская, 12"
  },
  "eventStart": "2026-03-05T12:00:00Z"
}
```
**Errors**

* `401 Unauthorized`
* `400 Bad Request` — некорректный JSON

---

### PUT `/api/topics/{id}`

Обновление топика по id.

**Request body**  — такой же как create:

```json
{
  "title": "Новое название",
  "summary": "Новое описание",
  "topicType": "Вебинар",
  "location": { 
    "city": "Москва", 
    "street": "Тверская, 1"
  },
  "eventStart": "2026-03-10T18:00:00Z"
}
```

**Response**

* `200 OK`

```json
{
  "id": "4a8b3e2b-0e1b-4e8c-8c5a-7d0f7e1a2c11",
  "title": "Новое название",
  "summary": "Новое описание",
  "topicType": "Вебинар",
  "location": { 
    "city": "Москва", 
    "street": "Тверская, 1"
  },
  "eventStart": "2026-03-10T18:00:00Z"
}
```

**Errors**

* `401 Unauthorized`
* `404 Not Found` 

---

### DELETE `/api/topics/{id}`

Мягкое удаление топика 

**Response**

* `200 OK` (`DeleteTopicResult`)

```json
{
  "isSuccess": true
}
```

**Errors**

* `401 Unauthorized`
* `404 Not Found`

---

## TestAuth

**Все эндпойнты требуют:** `Authorization: Bearer <JWT>`

### GET `/api/testauth/test1`

**Response**

* `200 OK`

```json
{ "result": "test1 ok" }
```

### GET `/api/testauth/test2`

**Response**

* `200 OK`

```json
{ "result": "test2 ok" }
```

---
