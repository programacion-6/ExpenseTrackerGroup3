CREATE TABLE IF NOT EXISTS users (
    id UUID PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    passwordHash VARCHAR(60) NOT NULL,
    createdAt DATE NOT NULL
);

CREATE TABLE
    IF NOT EXISTS Expense (
        id UUID PRIMARY KEY,
        userId UUID REFERENCES users(Id) ON DELETE CASCADE,
        amount DECIMAL(10, 2) NOT NULL,
        description VARCHAR(255) NOT NULL,
        category VARCHAR(50) NOT NULL,
        date DATE NOT NULL,
        createdAt DATE NOT NULL,
        recurringExpense BOOLEAN NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS Income (
        id UUID PRIMARY KEY,
        userId UUID REFERENCES users(Id) ON DELETE CASCADE,
        amount DECIMAL(10, 2) NOT NULL,
        source VARCHAR(50) NOT NULL,
        createdAt DATE NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS Budget (
        id UUID PRIMARY KEY,
        userId UUID REFERENCES users(Id) ON DELETE CASCADE,
        month DATE NOT NULL,
        budgetAmount DECIMAL(10, 2) NOT NULL,
        alertThreshold DECIMAL(10, 2) NULL
    );

CREATE TABLE
    IF NOT EXISTS Goal (
        id UUID PRIMARY KEY,
        userId UUID REFERENCES users(Id) ON DELETE CASCADE,
        goalAmount DECIMAL(10, 2) NOT NULL,
        deadLine DATE NOT NULL,
        currentAmount DECIMAL(10, 2) NOT NULL,
        createdAt DATE NOT NULL
    );
