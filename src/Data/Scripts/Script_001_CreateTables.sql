CREATE TABLE
    IF NOT EXISTS "user" (
        Id UUID PRIMARY KEY,
        "name" VARCHAR(50) NOT NULL,
        Email VARCHAR(50) NOT NULL,
        PasswordHash VARCHAR(60) NOT NULL,
        CreatedAt DATE NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS Expense (
        Id UUID PRIMARY KEY,
        UserId UUID REFERENCES "user" (Id) ON DELETE CASCADE,
        Amount DECIMAL(10, 2) NOT NULL,
        "description" VARCHAR(255) NOT NULL,
        Category VARCHAR(50) NOT NULL,
        "date" DATE NOT NULL,
        CreatedAt DATE NOT NULL,
        RecurringExpense BOOLEAN NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS Income (
        Id UUID PRIMARY KEY,
        UserId UUID REFERENCES "user" (Id) ON DELETE CASCADE,
        Amount DECIMAL(10, 2) NOT NULL,
        Source VARCHAR(50) NOT NULL,
        CreatedAt DATE NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS Budget (
        Id UUID PRIMARY KEY,
        UserId UUID REFERENCES "user" (Id) ON DELETE CASCADE,
        "month" DATE NOT NULL,
        BudgetAmount DECIMAL(10, 2) NOT NULL,
        AlertThreshold DECIMAL(10, 2) NULL
    );

CREATE TABLE
    IF NOT EXISTS Goal (
        Id UUID PRIMARY KEY,
        UserId UUID REFERENCES "user" (Id) ON DELETE CASCADE,
        GoalAmount DECIMAL(10, 2) NOT NULL,
        DeadLine DATE NOT NULL,
        CurrentAmount DECIMAL(10, 2) NOT NULL,
        CreatedAt DATE NOT NULL
    );
