-- Insert admin customer
INSERT INTO customers (name, address, phone, email)
VALUES ('Admin User', '1 Admin Lane, Test City, Test Country', '+1-555-000-0000', 'admin@example.com');

INSERT INTO properties (property_name)
VALUES ('A1'),
       ('A2'),
       ('A3'),
       ('A4'),
       ('A5'),
       ('White'),
       ('Blue'),
       ('Yellow'),
       ('Green'),
       ('Pink'),
       ('Red'),
       ('Black'),
       ('Waterproof'),
       ('Biodegradable');

-- Insert test data into paper
INSERT INTO paper (name, discontinued, stock, price)
VALUES ('A1 White Paper', FALSE, 200, 0.75),
       ('A1 Blue Paper', FALSE, 200, 0.80),
       ('A1 Yellow Paper', FALSE, 200, 0.70),
       ('A1 Green Paper', FALSE, 200, 0.85),
       ('A1 Pink Paper', FALSE, 200, 0.78),
       ('A1 Red Paper', FALSE, 200, 0.72),
       ('A1 Black Paper', FALSE, 200, 0.90),
       ('A2 White Paper', FALSE, 200, 0.75),
       ('A2 Blue Paper', FALSE, 200, 0.80),
       ('A2 Yellow Paper', FALSE, 200, 0.70),
       ('A2 Green Paper', FALSE, 200, 0.85),
       ('A2 Pink Paper', FALSE, 200, 0.78),
       ('A2 Red Paper', FALSE, 200, 0.72),
       ('A2 Black Paper', FALSE, 200, 0.90),
       ('A3 White Paper', FALSE, 200, 0.75),
       ('A3 Blue Paper', FALSE, 200, 0.80),
       ('A3 Yellow Paper', FALSE, 200, 0.70),
       ('A3 Green Paper', FALSE, 200, 0.85),
       ('A3 Pink Paper', FALSE, 200, 0.78),
       ('A3 Red Paper', FALSE, 200, 0.72),
       ('A3 Black Paper', FALSE, 200, 0.90),
       ('A4 White Paper', FALSE, 200, 0.75),
       ('A4 Blue Paper', FALSE, 200, 0.80),
       ('A4 Yellow Paper', FALSE, 200, 0.70),
       ('A4 Green Paper', FALSE, 200, 0.85),
       ('A4 Pink Paper', FALSE, 200, 0.78),
       ('A4 Red Paper', FALSE, 200, 0.72),
       ('A4 Black Paper', FALSE, 200, 0.90),
       ('A5 White Paper', FALSE, 200, 0.75),
       ('A5 Blue Paper', FALSE, 200, 0.80),
       ('A5 Yellow Paper', FALSE, 200, 0.70),
       ('A5 Green Paper', FALSE, 200, 0.85),
       ('A5 Pink Paper', FALSE, 200, 0.78),
       ('A5 Red Paper', FALSE, 200, 0.72),
       ('A5 Black Paper', FALSE, 200, 0.90);

-- Insert test data into paper_properties with combinations of properties
INSERT INTO paper_properties (paper_id, property_id)
VALUES
-- A1 Papers
(1, 1),   -- A1 White Paper
(1, 6),   -- Color White
(1, 14),  -- A1 White Paper, Biodegradable

(2, 1),   -- A1 Blue Paper
(2, 7),   -- Color Blue
(2, 14),  -- A1 Blue Paper, Biodegradable

(3, 1),   -- A1 Yellow Paper
(3, 8),   -- Color Yellow
(3, 14),  -- A1 Yellow Paper, Biodegradable

(4, 1),   -- A1 Green Paper
(4, 9),   -- Color Green
(4, 13),  -- A1 Green Paper, Waterproof

(5, 1),   -- A1 Pink Paper
(5, 10),  -- Color Pink
(5, 14),  -- A1 Pink Paper, Biodegradable

(6, 1),   -- A1 Red Paper
(6, 11),  -- Color Red
(6, 14),  -- A1 Red Paper, Biodegradable

(7, 1),   -- A1 Black Paper
(7, 12),  -- Color Black
(7, 14),  -- A1 Black Paper, Biodegradable

-- A2 Papers
(8, 1),   -- A2 White Paper
(8, 6),   -- Color White
(8, 14),  -- A2 White Paper, Biodegradable

(9, 1),   -- A2 Blue Paper
(9, 7),   -- Color Blue
(9, 14),  -- A2 Blue Paper, Biodegradable

(10, 1),  -- A2 Yellow Paper
(10, 8),  -- Color Yellow
(10, 14), -- A2 Yellow Paper, Biodegradable

(11, 1),  -- A2 Green Paper
(11, 9),  -- Color Green
(11, 14), -- A2 Green Paper, Biodegradable

(12, 1),  -- A2 Pink Paper
(12, 10), -- Color Pink
(12, 14), -- A2 Pink Paper, Biodegradable

(13, 1),  -- A2 Red Paper
(13, 11), -- Color Red
(13, 14), -- A2 Red Paper, Biodegradable

(14, 1),  -- A2 Black Paper
(14, 12), -- Color Black
(14, 13), -- A2 Black Paper, Waterproof

-- A3 Papers
(15, 1),  -- A3 White Paper
(15, 6),  -- Color White
(15, 14), -- A3 White Paper, Biodegradable

(16, 1),  -- A3 Blue Paper
(16, 7),  -- Color Blue
(16, 14), -- A3 Blue Paper, Biodegradable

(17, 1),  -- A3 Yellow Paper
(17, 8),  -- Color Yellow
(17, 13), -- A3 Yellow Paper, Waterproof

(18, 1),  -- A3 Green Paper
(18, 9),  -- Color Green
(18, 14), -- A3 Green Paper, Biodegradable

(19, 1),  -- A3 Pink Paper
(19, 10), -- Color Pink
(19, 14), -- A3 Pink Paper, Biodegradable

(20, 1),  -- A3 Red Paper
(20, 11), -- Color Red
(20, 14), -- A3 Red Paper, Biodegradable

(21, 1),  -- A3 Black Paper
(21, 12), -- Color Black
(21, 14), -- A3 Black Paper, Biodegradable

-- A4 Papers
(22, 1),  -- A4 White Paper
(22, 6),  -- Color White
(22, 14), -- A4 White Paper, Biodegradable

(23, 1),  -- A4 Blue Paper
(23, 7),  -- Color Blue
(23, 14), -- A4 Blue Paper, Biodegradable

(24, 1),  -- A4 Yellow Paper
(24, 8),  -- Color Yellow
(24, 14), -- A4 Yellow Paper, Biodegradable

(25, 1),  -- A4 Green Paper
(25, 9),  -- Color Green
(25, 14), -- A4 Green Paper, Biodegradable

(26, 1),  -- A4 Pink Paper
(26, 10), -- Color Pink
(26, 14), -- A4 Pink Paper, Biodegradable

(27, 1),  -- A4 Red Paper
(27, 11), -- Color Red
(27, 14), -- A4 Red Paper, Biodegradable

(28, 1),  -- A4 Black Paper
(28, 12), -- Color Black
(28, 13), -- A4 Black Paper, Waterproof

-- A5 Papers
(29, 1),  -- A5 White Paper
(29, 6),  -- Color White
(29, 14), -- A5 White Paper, Biodegradable

(30, 1),  -- A5 Blue Paper
(30, 7),  -- Color Blue
(30, 14), -- A5 Blue Paper, Biodegradable

(31, 1),  -- A5 Yellow Paper
(31, 8),  -- Color Yellow
(31, 13), -- A5 Yellow Paper, Waterproof

(32, 1),  -- A5 Green Paper
(32, 9),  -- Color Green
(32, 14), -- A5 Green Paper, Biodegradable

(33, 1),  -- A5 Pink Paper
(33, 10), -- Color Pink
(33, 14), -- A5 Pink Paper, Biodegradable

(34, 1),  -- A5 Red Paper
(34, 11), -- Color Red
(34, 14), -- A5 Red Paper, Biodegradable

(35, 1),  -- A5 Black Paper
(35, 12), -- Color Black
(35, 13); -- A5 Black Paper, Waterproof