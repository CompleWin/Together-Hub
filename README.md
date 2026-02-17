# Короткое описание того, что не совпадает с реализацией Сергея.

## JwtTokenSerivce
Интерфейс сервиса перемещен в слой Application
`Application/Security/Services/IJwtSecurityService.cs`  
Реализация сервиса перемещена в слой Infrastructure
`Infrastructure/Security/Services/JwtSecurityService.cs`  
🤓Потому что именно слой инфрастуктуру отвечает за безопасность, но при этом слою Application как-то надо создавать токены и выдавать их

## Security Dtos Model
Дто модели `IdentityUserResponseDto`, `LoginRequestDto`, `RegisterUserRequestDto` были перемещены из слоя `Domain` в слой `Application`

## Commands
### Login
Были добавлено `LoginUserResult`, `LoginUserQuery`, `LoginUserHandler` в слое Application => логика из Endpoint переехала в слой Application  
Изменился Endpoint /login до одной команды `await mediator.Send(...)`

`Application/Auth/Queries/LoginUser`

### Register
Были добавлено `RegisterUserResult`, `RegisterUserCommand`, `RegisterUserHandler` в слое Application => логика из Endpoint переехала в слой Application  
Изменился Endpoint /register до одной команды `await mediator.Send(...)`

`Application/Auth/Commands/RegisterUser`

## Exceptions
Был добавлен `UserException` и его наследники:  
`UserEmailAlreadyTakenException`  
`UserUsernameAlreadyTakenException`  
`UserWrongEmailException`  
`UserWrongPasswordException`  
=> Добавлена обработка родительского `UserException` в `CustomExceptionHandler`

## Mapping
Добавлен маппинг `CustomIdentityUser` в `IdentityUserResponseDto`  
Добавлен маппинг `RegisterUserRequestDto` в `CustomIdentityUser`

## Рефакторинг using'ов
По мелочи убрал using'и в `GlobalUsing`