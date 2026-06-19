-- Создаем базу данных 
CREATE DATABASE NetworkMonitoringDB; 
GO 
USE NetworkMonitoringDB; 
GO 

-- 1. Таблица Главного администратора 
CREATE TABLE [Главный_администратор] ( 
    [ID_Администратора] INT IDENTITY(1,1) PRIMARY KEY, 
    [ФИО] NVARCHAR(100) NOT NULL, 
    [Роль] NVARCHAR(30) NOT NULL, 
    [Логин] NVARCHAR(20) UNIQUE NOT NULL, 
    [Пароль] NVARCHAR(20) NOT NULL 
); 

-- 2. Таблица Пользователей 
CREATE TABLE [Пользователь] ( 
    [ID_Пользователя] INT IDENTITY(1,1) PRIMARY KEY, 
    [ID_Администратора] INT NOT NULL FOREIGN KEY REFERENCES [Главный_администратор]([ID_Администратора]) ON DELETE NO ACTION, 
    [ФИО] NVARCHAR(100) NOT NULL, 
    [Роль] NVARCHAR(30) NOT NULL, 
    [Логин] NVARCHAR(20) UNIQUE NOT NULL, 
    [Пароль] NVARCHAR(20) NOT NULL 
); 

-- 3. Таблица Сетевых устройств 
CREATE TABLE [Сетевое_устройство] ( 
    [ID_Устройства] INT IDENTITY(1,1) PRIMARY KEY, 
    [ID_Пользователя] INT NOT NULL FOREIGN KEY REFERENCES [Пользователь]([ID_Пользователя]) ON DELETE NO ACTION, 
    [IP_адрес] VARCHAR(100) NOT NULL, 
    [Название_устройства] NVARCHAR(50) NOT NULL, 
    [Местоположение] NVARCHAR(200) NOT NULL, 
    [Имя_хоста] VARCHAR(100) NOT NULL, 
    [Версия_прошивки] VARCHAR(100) NOT NULL, 
    [Срок_гарантии] DATE NULL, 
    [Текущий_статус] VARCHAR(10) DEFAULT 'offline', 
    [Флаг_мониторинга] BIT NOT NULL DEFAULT 1, 
    [Guid_Устройства] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [Стоимость_Обслуживания] DECIMAL(10, 2) NOT NULL DEFAULT 0.00, 
    CONSTRAINT UQ_Device_Host_IP UNIQUE ([Имя_хоста], [IP_адрес]) 
); 

-- 4. Журнал инцидентов (Связь 1:N с Сетевым устройством) 
CREATE TABLE [Журнал_инцидентов] ( 
    [ID_Отчёта] INT IDENTITY(1,1) PRIMARY KEY, 
    [ID_Устройства] INT NOT NULL FOREIGN KEY REFERENCES [Сетевое_устройство]([ID_Устройства]) ON DELETE CASCADE, 
    [Результат_мониторинга] NVARCHAR(50) NOT NULL, 
    [Метка_времени] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [Время_ответа_мс] INT NULL, 
    [Описание_инцидента] NVARCHAR(MAX) NULL 
); 

-- 5. Таблица Сетевых служб (для организации связи N:N) 
CREATE TABLE [Сетевая_служба] ( 
    [ID_Службы] INT IDENTITY(1,1) PRIMARY KEY, 
    [Название_Службы] NVARCHAR(50) NOT NULL UNIQUE, 
    [Порт] INT NOT NULL 
); 

-- 6. Связующая таблица для реализации связи N:N (Многие-ко-Многим) 
CREATE TABLE [Устройство_Служба] ( 
    [ID_Устройства] INT NOT NULL FOREIGN KEY REFERENCES [Сетевое_устройство]([ID_Устройства]) ON DELETE CASCADE, 
    [ID_Службы] INT NOT NULL FOREIGN KEY REFERENCES [Сетевая_служба]([ID_Службы]) ON DELETE CASCADE, 
    PRIMARY KEY ([ID_Устройства], [ID_Службы]) 
); 
GO
