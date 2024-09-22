INSERT INTO "user" (id, name, email, passwordHash, createdAt) 
VALUES
('a1b2c3d4-e5f6-7g8h-9i0j-a1b2c3d4e5f6', 'John Doe', 'john.doe@example.com', 'hash1', '2023-09-01'),
('b2c3d4e5-f6g7-h8i9-j0k1-b2c3d4e5f6g7', 'Jane Smith', 'jane.smith@example.com', 'hash2', '2023-09-02'),
('c3d4e5f6-g7h8-i9j0-k1l2-c3d4e5f6g7h8', 'Mike Johnson', 'mike.johnson@example.com', 'hash3', '2023-09-03'),
('d4e5f6g7-h8i9-j0k1-l2m3-d4e5f6g7h8i9', 'Alice Brown', 'alice.brown@example.com', 'hash4', '2023-09-04'),
('e5f6g7h8-i9j0-k1l2-m3n4-e5f6g7h8i9j0', 'Bob White', 'bob.white@example.com', 'hash5', '2023-09-05'),
('f6g7h8i9-j0k1-l2m3-n4o5-f6g7h8i9j0k1', 'Emma Davis', 'emma.davis@example.com', 'hash6', '2023-09-06'),
('g7h8i9j0-k1l2-m3n4-o5p6-g7h8i9j0k1l2', 'Chris Taylor', 'chris.taylor@example.com', 'hash7', '2023-09-07'),
('h8i9j0k1-l2m3-n4o5-p6q7-h8i9j0k1l2m3', 'Sophia Miller', 'sophia.miller@example.com', 'hash8', '2023-09-08'),
('i9j0k1l2-m3n4-o5p6-q7r8-i9j0k1l2m3n4', 'Liam Wilson', 'liam.wilson@example.com', 'hash9', '2023-09-09'),
('j0k1l2m3-n4o5-p6q7-r8s9-j0k1l2m3n4o5', 'Olivia Martinez', 'olivia.martinez@example.com', 'hash10', '2023-09-10')
ON CONFLICT (id) DO NOTHING;

INSERT INTO Expense (id, userId, amount, description, category, date, createdAt, recurringExpense) 
VALUES
('e1f2g3h4-i5j6-k7l8-m9n0-o1p2q3r4s5t6', 'a1b2c3d4-e5f6-7g8h-9i0j-a1b2c3d4e5f6', 100.00, 'Groceries', 'Food', '2023-09-01', '2023-09-01', false),
('f2g3h4i5-j6k7-l8m9-n0o1-p2q3r4s5t6u7', 'b2c3d4e5-f6g7-h8i9-j0k1-b2c3d4e5f6g7', 50.00, 'Gas', 'Transport', '2023-09-02', '2023-09-02', false),
('g3h4i5j6-k7l8-m9n0-o1p2-q3r4s5t6u7v8', 'c3d4e5f6-g7h8-i9j0-k1l2-c3d4e5f6g7h8', 120.50, 'Rent', 'Housing', '2023-09-03', '2023-09-03', true),
('h4i5j6k7-l8m9-n0o1-p2q3-r4s5t6u7v8w9', 'd4e5f6g7-h8i9-j0k1-l2m3-d4e5f6g7h8i9', 75.00, 'Dining Out', 'Food', '2023-09-04', '2023-09-04', false),
('i5j6k7l8-m9n0-o1p2-q3r4-s5t6u7v8w9x0', 'e5f6g7h8-i9j0-k1l2-m3n4-e5f6g7h8i9j0', 30.00, 'Electricity', 'Utilities', '2023-09-05', '2023-09-05', true),
('j6k7l8m9-n0o1-p2q3-r4s5-t6u7v8w9x0y1', 'f6g7h8i9-j0k1-l2m3-n4o5-f6g7h8i9j0k1', 20.00, 'Internet', 'Utilities', '2023-09-06', '2023-09-06', true),
('k7l8m9n0-o1p2-q3r4-s5t6-u7v8w9x0y1z2', 'g7h8i9j0-k1l2-m3n4-o5p6-g7h8i9j0k1l2', 15.00, 'Coffee', 'Entertainment', '2023-09-07', '2023-09-07', false),
('l8m9n0o1-p2q3-r4s5-t6u7-v8w9x0y1z2a3', 'h8i9j0k1-l2m3-n4o5-p6q7-h8i9j0k1l2m3', 200.00, 'Car Payment', 'Transport', '2023-09-08', '2023-09-08', true),
('m9n0o1p2-q3r4-s5t6-u7v8-w9x0y1z2a3b4', 'i9j0k1l2-m3n4-o5p6-q7r8-i9j0k1l2m3n4', 90.00, 'Clothing', 'Shopping', '2023-09-09', '2023-09-09', false),
('n0o1p2q3-r4s5-t6u7-v8w9-x0y1z2a3b4c5', 'j0k1l2m3-n4o5-p6q7-r8s9-j0k1l2m3n4o5', 150.00, 'Gym Membership', 'Health', '2023-09-10', '2023-09-10', true)
ON CONFLICT (id) DO NOTHING;

INSERT INTO Income (id, userId, amount, source, createdAt) 
VALUES
('i1j2k3l4-m5n6-o7p8-q9r0-s1t2u3v4w5x6', 'a1b2c3d4-e5f6-7g8h-9i0j-a1b2c3d4e5f6', 3000.00, 'Salary', '2023-09-01'),
('j2k3l4m5-n6o7-p8q9-r0s1-t2u3v4w5x6y7', 'b2c3d4e5-f6g7-h8i9-j0k1-b2c3d4e5f6g7', 150.00, 'Freelance', '2023-09-02'),
('k3l4m5n6-o7p8-q9r0-s1t2-u3v4w5x6y7z8', 'c3d4e5f6-g7h8-i9j0-k1l2-c3d4e5f6g7h8', 500.00, 'Investments', '2023-09-03'),
('l4m5n6o7-p8q9-r0s1-t2u3-v4w5x6y7z8a9', 'd4e5f6g7-h8i9-j0k1-l2m3-d4e5f6g7h8i9', 1000.00, 'Bonus', '2023-09-04'),
('m5n6o7p8-q9r0-s1t2-u3v4-w5x6y7z8a9b0', 'e5f6g7h8-i9j0-k1l2-m3n4-e5f6g7h8i9j0', 2000.00, 'Salary', '2023-09-05'),
('n6o7p8q9-r0s1-t2u3-v4w5-x6y7z8a9b0c1', 'f6g7h8i9-j0k1-l2m3-n4o5-f6g7h8i9j0k1', 250.00, 'Freelance', '2023-09-06'),
('o7p8q9r0-s1t2-u3v4-w5x6-y7z8a9b0c1d2', 'g7h8i9j0-k1l2-m3n4-o5p6-g7h8i9j0k1l2', 100.00, 'Gifts', '2023-09-07'),
('p8q9r0s1-t2u3-v4w5-x6y7-z8a9b0c1d2e3', 'h8i9j0k1-l2m3-n4o5-p6q7-h8i9j0k1l2m3', 800.00, 'Investments', '2023-09-08'),
('q9r0s1t2-u3v4-w5x6-y7z8-a9b0c1d2e3f4', 'i9j0k1l2-m3n4-o5p6-q7r8-i9j0k1l2m3n4', 5000.00, 'Salary', '2023-09-09'),
('r0s1t2u3-v4w5-x6y7-z8a9-b0c1d2e3f4g5', 'j0k1l2m3-n4o5-p6q7-r8s9-j0k1l2m3n4o5', 400.00, 'Bonus', '2023-09-10')
ON CONFLICT (id) DO NOTHING;

INSERT INTO Budget (id, userId, month, budgetAmount, alertThreshold) 
VALUES
('b1c2d3e4-f5g6-h7i8-j9k0-l1m2n3o4p5q6', 'a1b2c3d4-e5f6-7g8h-9i0j-a1b2c3d4e5f6', '2023-09-01', 2500.00, 200.00),
('c2d3e4f5-g6h7-i8j9-k0l1-m2n3o4p5q6r7', 'b2c3d4e5-f6g7-h8i9-j0k1-b2c3d4e5f6g7', '2023-09-01', 1500.00, 100.00),
('d3e4f5g6-h7i8-j9k0-l1m2-n3o4p5q6r7s8', 'c3d4e5f6-g7h8-i9j0-k1l2-c3d4e5f6g7h8', '2023-09-01', 3000.00, 250.00),
('e4f5g6h7-i8j9-k0l1-m2n3-o4p5q6r7s8t9', 'd4e5f6g7-h8i9-j0k1-l2m3-d4e5f6g7h8i9', '2023-09-01', 1800.00, 150.00),
('f5g6h7i8-j9k0-l1m2-n3o4-p5q6r7s8t9u0', 'e5f6g7h8-i9j0-k1l2-m3n4-e5f6g7h8i9j0', '2023-09-01', 2200.00, 300.00),
('g6h7i8j9-k0l1-m2n3-o4p5-q6r7s8t9u0v1', 'f6g7h8i9-j0k1-l2m3-n4o5-f6g7h8i9j0k1', '2023-09-01', 1000.00, 50.00),
('h7i8j9k0-l1m2-n3o4-p5q6-r7s8t9u0v1w2', 'g7h8i9j0-k1l2-m3n4-o5p6-g7h8i9j0k1l2', '2023-09-01', 3500.00, 400.00),
('i8j9k0l1-m2n3-o4p5-q6r7-s8t9u0v1w2x3', 'h8i9j0k1-l2m3-n4o5-p6q7-h8i9j0k1l2m3', '2023-09-01', 1750.00, 100.00),
('j9k0l1m2-n3o4-p5q6-r7s8-t9u0v1w2x3y4', 'i9j0k1l2-m3n4-o5p6-q7r8-i9j0k1l2m3n4', '2023-09-01', 2700.00, 300.00),
('k0l1m2n3-o4p5-q6r7-s8t9-u0v1w2x3y4z5', 'j0k1l2m3-n4o5-p6q7-r8s9-j0k1l2m3n4o5', '2023-09-01', 2000.00, 150.00)
ON CONFLICT (id) DO NOTHING;

INSERT INTO Goal (id, userId, goalAmount, deadLine, currentAmount, createdAt) 
VALUES
('g1h2i3j4-k5l6-m7n8-o9p0-q1r2s3t4u5v6', 'a1b2c3d4-e5f6-7g8h-9i0j-a1b2c3d4e5f6', 5000.00, '2024-12-01', 2500.00, '2023-09-01'),
('h2i3j4k5-l6m7-n8o9-p0q1-r2s3t4u5v6w7', 'b2c3d4e5-f6g7-h8i9-j0k1-b2c3d4e5f6g7', 3000.00, '2024-11-01', 1500.00, '2023-09-02'),
('i3j4k5l6-m7n8-o9p0-q1r2-s3t4u5v6w7x8', 'c3d4e5f6-g7h8-i9j0-k1l2-c3d4e5f6g7h8', 10000.00, '2024-10-01', 5000.00, '2023-09-03'),
('j4k5l6m7-n8o9-p0q1-r2s3-t4u5v6w7x8y9', 'd4e5f6g7-h8i9-j0k1-l2m3-d4e5f6g7h8i9', 7500.00, '2024-09-01', 3750.00, '2023-09-04'),
('k5l6m7n8-o9p0-q1r2-s3t4-u5v6w7x8y9z0', 'e5f6g7h8-i9j0-k1l2-m3n4-e5f6g7h8i9j0', 4000.00, '2024-08-01', 2000.00, '2023-09-05'),
('l6m7n8o9-p0q1-r2s3-t4u5-v6w7x8y9z0a1', 'f6g7h8i9-j0k1-l2m3-n4o5-f6g7h8i9j0k1', 1500.00, '2024-07-01', 750.00, '2023-09-06'),
('m7n8o9p0-q1r2-s3t4-u5v6-w7x8y9z0a1b2', 'g7h8i9j0-k1l2-m3n4-o5p6-g7h8i9j0k1l2', 6000.00, '2024-06-01', 3000.00, '2023-09-07'),
('n8o9p0q1-r2s3-t4u5-v6w7-x8y9z0a1b2c3', 'h8i9j0k1-l2m3-n4o5-p6q7-h8i9j0k1l2m3', 8500.00, '2024-05-01', 4250.00, '2023-09-08'),
('o9p0q1r2-s3t4-u5v6-w7x8-y9z0a1b2c3d4', 'i9j0k1l2-m3n4-o5p6-q7r8-i9j0k1l2m3n4', 20000.00, '2024-04-01', 10000.00, '2023-09-09'),
('p0q1r2s3-t4u5-v6w7-x8y9-z0a1b2c3d4e5', 'j0k1l2m3-n4o5-p6q7-r8s9-j0k1l2m3n4o5', 9500.00, '2024-03-01', 4750.00, '2023-09-10')
ON CONFLICT (id) DO NOTHING;
