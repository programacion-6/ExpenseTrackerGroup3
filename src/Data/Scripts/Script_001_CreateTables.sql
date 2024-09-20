CREATE TABLE
    IF NOT EXISTS User (
        Id UUID PRIMARY KEY,
        Name VARCHAR(50) NOT NULL,
        Email VARCHAR(50) NOT NULL,
        PasswordHash CHAR(60) BINARY NOT NULL,
        CreatedAt DATE NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS Expense (
        Id UUID PRIMARY KEY,
        UserId UUID REFERENCES User (Id) ON DELETE CASCADE,
        Amount DECIMAL(10, 2) NOT NULL,
        Description VARCHAR(255) NOT NULL,
        Category VARCHAR(50) NOT NULL,
        Date DATE NOT NULL,
        CreatedAt DATE NOT NULL,
        RecurringExpense BOOLEAN NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS Income (
        Id UUID PRIMARY KEY,
        UserId UUID REFERENCES User (Id) ON DELETE CASCADE,
        Amount DECIMAL(10, 2) NOT NULL,
        Source VARCHAR(50) NOT NULL,
        CreatedAt DATE NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS Budget (
        Id UUID PRIMARY KEY,
        UserId UUID REFERENCES User (Id) ON DELETE CASCADE,
        Month DATE NOT NULL,
        BudgetAmount DECIMAL(10, 2) NOT NULL,
        AlertThreshold DECIMAL(10, 2) NULL
    );

CREATE TABLE
    IF NOT EXISTS Goal (
        Id UUID PRIMARY KEY,
        UserId UUID REFERENCES User (Id) ON DELETE CASCADE,
        GoalAmount DECIMAL(10, 2) NOT NULL,
        DeadLine DATE NOT NULL,
        CurrentAmount DECIMAL(10, 2) NOT NULL,
        CreatedAt DATE NOT NULL
    );