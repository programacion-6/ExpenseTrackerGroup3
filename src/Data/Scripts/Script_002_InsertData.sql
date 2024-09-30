CREATE EXTENSION IF NOT EXISTS pgcrypto;

INSERT INTO users (id, name, email, passwordHash, createdAt) 
VALUES
('4a3a8e37-bd77-4266-aaee-de276f6b85e0', 'John Doe', 'john.doe@example.com', crypt('Pass@word1', gen_salt('bf')), '2023-09-01'),
('0c3c5fef-b022-45af-b1d2-2d9792dfcec8', 'Jane Smith', 'jane.smith@example.com', crypt('Secure@123', gen_salt('bf')), '2023-09-02'),
('a5be0528-182b-4794-ae53-ec6b2b1fdb94', 'Mike Johnson', 'mike.johnson@example.com', crypt('Strong#Pass3', gen_salt('bf')), '2023-09-03'),
('bcec534b-ee13-4048-9298-82bbeae07aeb', 'Alice Brown', 'alice.brown@example.com', crypt('Complex@4567', gen_salt('bf')), '2023-09-04'),
('6c6ae333-0665-4df3-b95a-bcb8b3d50a35', 'Bob White', 'bob.white@example.com', crypt('Secure$Pass5', gen_salt('bf')), '2023-09-05'),
('7e9d4c13-64fc-472c-9389-6bbb7dab80ee', 'Emma Davis', 'emma.davis@example.com', crypt('EmmaP@ss6', gen_salt('bf')), '2023-09-06'),
('344f5819-7820-4a84-a451-058b5489d4d8', 'Chris Taylor', 'chris.taylor@example.com', crypt('TaylorC#7', gen_salt('bf')), '2023-09-07'),
('a69ac8c6-bf9e-4ea8-97b5-b7fda6faa918', 'Sophia Miller', 'sophia.miller@example.com', crypt('SophiaM$8', gen_salt('bf')), '2023-09-08'),
('27e1afb8-3212-407e-8792-0717b535b0e7', 'Liam Wilson', 'liam.wilson@example.com', crypt('WilsonL@9', gen_salt('bf')), '2023-09-09'),
('11948d4d-02c3-4ac3-b689-47a4b0a1f9a7', 'Olivia Martinez', 'olivia.martinez@example.com', crypt('OliviaM#10', gen_salt('bf')), '2023-09-10')
ON CONFLICT (id) DO NOTHING;

INSERT INTO Expense (id, userId, amount, description, category, date, createdAt, recurringExpense) 
VALUES
('2dc8e76d-8e6c-435e-aec4-277f62b688f5', '4a3a8e37-bd77-4266-aaee-de276f6b85e0', 100.00, 'Groceries', 'Food', '2023-09-01', '2023-09-01', false),
('9d35430f-9d90-4f12-a832-c01f0501d959', '0c3c5fef-b022-45af-b1d2-2d9792dfcec8', 50.00, 'Gas', 'Transport', '2023-09-02', '2023-09-02', false),
('e3673e87-d73c-45f5-a735-7814bde88dad', 'a5be0528-182b-4794-ae53-ec6b2b1fdb94', 1200.50, 'Rent', 'Housing', '2023-09-03', '2023-09-03', true),
('ffcab1f2-c7ad-4a11-8a68-5b8f067d6fc5', 'bcec534b-ee13-4048-9298-82bbeae07aeb', 75.00, 'Dining Out', 'Food', '2023-09-04', '2023-09-04', false),
('09855b68-f1aa-42c0-9255-18f4610ecb52', '6c6ae333-0665-4df3-b95a-bcb8b3d50a35', 30.00, 'Electricity', 'Utilities', '2023-09-05', '2023-09-05', true),
('f629f101-fe29-4387-b5e3-c762b675563d', '7e9d4c13-64fc-472c-9389-6bbb7dab80ee', 20.00, 'Internet', 'Utilities', '2023-09-06', '2023-09-06', true),
('ff52448a-2d53-4635-8725-683a1ebb88d2', '344f5819-7820-4a84-a451-058b5489d4d8', 15.00, 'Coffee', 'Food', '2023-09-07', '2023-09-07', false),
('828c64be-b063-4ea9-9810-0ae51e187ffd', 'a69ac8c6-bf9e-4ea8-97b5-b7fda6faa918', 200.00, 'Car Payment', 'Transport', '2023-09-08', '2023-09-08', true),
('6cee7987-8c6d-4e03-8db9-1ad48a716cc5', '27e1afb8-3212-407e-8792-0717b535b0e7', 90.00, 'Clothing', 'Shopping', '2023-09-09', '2023-09-09', false),
('78add9fe-4efe-4362-a2f0-bee368281106', '11948d4d-02c3-4ac3-b689-47a4b0a1f9a7', 150.00, 'Gym Membership', 'Health', '2023-09-10', '2023-09-10', true)
ON CONFLICT (id) DO NOTHING;

INSERT INTO Income (id, userId, amount, source, createdAt) 
VALUES
('aed98f99-f83a-4ef6-b28c-7109622a9ee1', '4a3a8e37-bd77-4266-aaee-de276f6b85e0', 3000.00, 'Salary', '2023-09-01'),
('106de85b-1ae5-4051-8360-e192498e9b35', '0c3c5fef-b022-45af-b1d2-2d9792dfcec8', 150.00, 'Freelance', '2023-09-02'),
('8bbd4a28-3d40-482f-9ecc-787aa2cd667e', 'a5be0528-182b-4794-ae53-ec6b2b1fdb94', 500.00, 'Investments', '2023-09-03'),
('d6981efa-8160-463e-94c5-7260b0d07c78', 'bcec534b-ee13-4048-9298-82bbeae07aeb', 1000.00, 'Bonus', '2023-09-04'),
('88515ad4-eeb9-462a-848d-3e74c71dcb53', '6c6ae333-0665-4df3-b95a-bcb8b3d50a35', 2000.00, 'Salary', '2023-09-05'),
('8a36b7f7-eb60-43c3-888b-56df7a8afd2d', '7e9d4c13-64fc-472c-9389-6bbb7dab80ee', 250.00, 'Freelance', '2023-09-06'),
('6131a7e1-9346-4ec9-97f2-bf81dda0b898', '344f5819-7820-4a84-a451-058b5489d4d8', 100.00, 'Gifts', '2023-09-07'),
('ac03bb04-403a-4d89-a30b-06b3838ea071', 'a69ac8c6-bf9e-4ea8-97b5-b7fda6faa918', 800.00, 'Investments', '2023-09-08'),
('3ff5fba9-0024-4468-bf00-43d505582315', '27e1afb8-3212-407e-8792-0717b535b0e7', 5000.00, 'Salary', '2023-09-09'),
('f1830b94-f3fa-47b3-a16c-e710c87dd863', '11948d4d-02c3-4ac3-b689-47a4b0a1f9a7', 400.00, 'Bonus', '2023-09-10')
ON CONFLICT (id) DO NOTHING;

INSERT INTO Budget (id, userId, month, budgetAmount, alertThreshold) 
VALUES
('2d2ec04c-2b63-41bb-8dcc-6158e6fa758a', '4a3a8e37-bd77-4266-aaee-de276f6b85e0', '2023-09-01', 2500.00, 10.00),
('94df030d-a863-46cb-874b-280851bc6283', '0c3c5fef-b022-45af-b1d2-2d9792dfcec8', '2023-09-01', 1500.00, 15.00),
('a7613046-b220-4e5d-8f34-7d62e808bf4d', 'a5be0528-182b-4794-ae53-ec6b2b1fdb94', '2023-09-01', 3000.00, 20.00),
('1d502f48-ac00-498a-a47d-5e8099d19833', 'bcec534b-ee13-4048-9298-82bbeae07aeb', '2023-09-01', 1800.00, 25.00),
('a10bcd66-b660-4c54-aeee-c176d339a9f4', '6c6ae333-0665-4df3-b95a-bcb8b3d50a35', '2023-09-01', 2200.00, 30.00),
('b032d658-8880-46e8-894b-0e928f08b097', '7e9d4c13-64fc-472c-9389-6bbb7dab80ee', '2023-09-01', 1000.00, 35.00),
('fecde3aa-0592-40e7-9539-72855603efe8', '344f5819-7820-4a84-a451-058b5489d4d8', '2023-09-01', 3500.00, 40.00),
('26a7ceed-9d23-4170-b1a4-5ff15e5f4728', 'a69ac8c6-bf9e-4ea8-97b5-b7fda6faa918', '2023-09-01', 1750.00, 45.00),
('f084cdad-e31c-4c62-b374-15b939908df5', '27e1afb8-3212-407e-8792-0717b535b0e7', '2023-09-01', 2700.00, 50.00),
('2f4c7ed2-70d3-453c-a87d-49e552ee2947', '11948d4d-02c3-4ac3-b689-47a4b0a1f9a7', '2023-09-01', 2000.00, 55.00)
ON CONFLICT (id) DO NOTHING;

INSERT INTO Goal (id, userId, goalAmount, deadLine, currentAmount, createdAt) 
VALUES
('7b163d81-7a35-4b29-90ec-6d100c352a5d', '4a3a8e37-bd77-4266-aaee-de276f6b85e0', 5000.00, '2024-12-01', 2500.00, '2023-09-01'),
('a37ea5ea-fd69-4ac1-8734-cb53f2d2223a', '0c3c5fef-b022-45af-b1d2-2d9792dfcec8', 3000.00, '2024-11-01', 1500.00, '2023-09-02'),
('86a09ba0-84a2-43d8-8e66-76f9c9c861c4', 'a5be0528-182b-4794-ae53-ec6b2b1fdb94', 10000.00, '2024-10-01', 5000.00, '2023-09-03'),
('84826351-5b47-4042-b43f-9202ca75308a', 'bcec534b-ee13-4048-9298-82bbeae07aeb', 7500.00, '2024-09-01', 3750.00, '2023-09-04'),
('2a713230-dd81-4a38-ae22-6b4358921ac9', '6c6ae333-0665-4df3-b95a-bcb8b3d50a35', 4000.00, '2024-08-01', 2000.00, '2023-09-05'),
('d8ee395b-05d9-42d3-8e88-885b397aa093', '7e9d4c13-64fc-472c-9389-6bbb7dab80ee', 1500.00, '2024-07-01', 750.00, '2023-09-06'),
('d8ee395b-05d9-42d3-8e88-885b397aa093', '344f5819-7820-4a84-a451-058b5489d4d8', 6000.00, '2024-06-01', 3000.00, '2023-09-07'),
('f1830b94-f3fa-47b3-a16c-e710c87dd863', 'a69ac8c6-bf9e-4ea8-97b5-b7fda6faa918', 8500.00, '2024-05-01', 4250.00, '2023-09-08'),
('1f080f56-743a-40e7-9b2f-be8ab064cc0c', '27e1afb8-3212-407e-8792-0717b535b0e7', 20000.00, '2024-04-01', 10000.00, '2023-09-09'),
('bd8f1e3e-5719-4684-ab03-c42f13fbba55', '11948d4d-02c3-4ac3-b689-47a4b0a1f9a7', 9500.00, '2024-03-01', 4750.00, '2023-09-10')
ON CONFLICT (id) DO NOTHING;
