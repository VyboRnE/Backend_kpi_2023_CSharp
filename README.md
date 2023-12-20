# Backend_kpi_2023_CSharp

Репозиторій для виконання лабораторних робіт з дисципліни "Технології розробки серверного програмного забезпечення(back end)". Було прийнято рішення змінити мову програмування проєкту (Python -> C#) для покращення навичок особистих навичок розробки у заданому руслі.

## Інструкції для запуску та тестування лабораторної роботи №2
Для локального тестування скопіюйте репозиторій збілдіть контейнер та запустіть контейнер за допомогою команд:\
$ docker compose buid\
$ docker compose up\
Перейдіть за наданим посиланням. Для перевірки роботи ендпоінту healthcheck додайте до посилання "/healthcheck".

Деплой був проведений за допомою сервісу reander.com. Для тестування лабораторної роботи №4 перейдіть за посиланням:\
https://backendlab4csharp.onrender.com \
Для перевірки роботи ендпоінту healthcheck:\
https://backendlab4csharp.onrender.com/healthcheck

Тестування роботи ендпоінтів /Customer, /Category, /Record було застосовано Postman, потрібні файли прикріплені у ClassRoom

# Реєвтрація та авторизація
Реєстрацію та авторизацію було реалізовано за допомогою JWT. Усі ендпоінти було захищено, окрім Customer/register, Customer/login та /healthcheck. Протестувати роботу реєстрації та авторизації можна за допомогою Postman Flow, посилання на які прикріплене у ClassRoo. 

## Database
Базу даних, що використовується в Лабораторній роботі №4 було розроблено з коду за допомогою EntityFramework міграцій, та задеплоєно на сервісі AWS.