-- Insert admin customer
INSERT INTO customers (name, address, phone, email)
VALUES ('Admin User', '1 Admin Lane, Test City, Test Country', '+1-555-000-0000', 'admin@example.com');

-- Insert 49 additional customers with varied locations
INSERT INTO customers (name, address, phone, email)
VALUES ('John Doe', '123 Elm St, New York, USA', '+1-555-123-4567', 'john.doe@example.com'),
       ('Jane Smith', '456 Oak St, London, UK', '+44 20 7946 0958', 'jane.smith@example.com'),
       ('Alice Johnson', '789 Pine St, Berlin, Germany', '+49 30 12345678', 'alice.j@example.com'),
       ('Bob Brown', '101 Maple St, Sydney, Australia', '+61 2 9876 5432', 'bob.brown@example.com'),
       ('Charlie Davis', '202 Birch St, Toronto, Canada', '+1-416-555-0123', 'charlie.d@example.com'),
       ('David Wilson', '303 Cedar St, Tokyo, Japan', '+81 3 1234 5678', 'david.wilson@example.com'),
       ('Emma Thompson', '404 Spruce St, Cape Town, South Africa', '+27 21 123 4567', 'emma.thompson@example.com'),
       ('Frank Martinez', '505 Willow St, Madrid, Spain', '+34 91 123 45 67', 'frank.martinez@example.com'),
       ('Grace Lee', '606 Pine St, Paris, France', '+33 1 23 45 67 89', 'grace.lee@example.com'),
       ('Henry Adams', '707 Ash St, Rome, Italy', '+39 06 12345678', 'henry.adams@example.com'),
       ('Isabella Garcia', '808 Fir St, Mexico City, Mexico', '+52 55 1234 5678', 'isabella.g@example.com'),
       ('Jack Brown', '909 Birch St, São Paulo, Brazil', '+55 11 91234-5678', 'jack.brown@example.com'),
       ('Laura White', '1010 Cherry St, Buenos Aires, Argentina', '+54 11 1234-5678', 'laura.white@example.com'),
       ('Mike Davis', '1111 Chestnut St, Beijing, China', '+86 10 1234 5678', 'mike.d@example.com'),
       ('Nina Patel', '1212 Cedar St, Mumbai, India', '+91 22 1234 5678', 'nina.patel@example.com'),
       ('Oscar Kim', '1313 Ash St, Seoul, South Korea', '+82 2 1234 5678', 'oscar.kim@example.com'),
       ('Peter Brown', '1414 Maple St, Moscow, Russia', '+7 495 123-45-67', 'peter.brown@example.com'),
       ('Quinn Lopez', '1515 Pine St, Bangkok, Thailand', '+66 2 123 4567', 'quinn.l@example.com'),
       ('Rita Wong', '1616 Fir St, Singapore, Singapore', '+65 6123 4567', 'rita.wong@example.com'),
       ('Sam Turner', '1717 Elm St, Nairobi, Kenya', '+254 20 1234567', 'sam.turner@example.com'),
       ('Tina Green', '1818 Birch St, Cairo, Egypt', '+20 2 12345678', 'tina.green@example.com'),
       ('Ursula Knight', '1919 Oak St, Tel Aviv, Israel', '+972 3-123-4567', 'ursula.k@example.com'),
       ('Victor Ramirez', '2020 Willow St, Lisbon, Portugal', '+351 21 123 4567', 'victor.ramirez@example.com'),
       ('Wendy Hall', '2121 Cherry St, Amsterdam, Netherlands', '+31 20 123 4567', 'wendy.hall@example.com'),
       ('Xavier Carter', '2222 Fir St, Stockholm, Sweden', '+46 8 123 4567', 'xavier.carter@example.com'),
       ('Yara Brown', '2323 Elm St, Oslo, Norway', '+47 21 12 34 56', 'yara.brown@example.com'),
       ('Zane Lee', '2424 Birch St, Copenhagen, Denmark', '+45 12 34 56 78', 'zane.lee@example.com'),
       ('Adam Fisher', '2525 Oak St, Helsinki, Finland', '+358 9 123 4567', 'adam.fisher@example.com'),
       ('Bella Turner', '2626 Pine St, Dublin, Ireland', '+353 1 123 4567', 'bella.turner@example.com'),
       ('Caleb King', '2727 Fir St, Reykjavik, Iceland', '+354 5 123 4567', 'caleb.king@example.com'),
       ('Diana Watson', '2828 Maple St, Vienna, Austria', '+43 1 1234567', 'diana.watson@example.com'),
       ('Eli Brown', '2929 Cedar St, Brussels, Belgium', '+32 2 123 45 67', 'eli.brown@example.com'),
       ('Fiona Chen', '3030 Elm St, Budapest, Hungary', '+36 1 234 5678', 'fiona.chen@example.com'),
       ('George Smith', '3131 Birch St, Zurich, Switzerland', '+41 44 123 45 67', 'george.smith@example.com'),
       ('Hannah Lee', '3232 Oak St, Luxembourg City, Luxembourg', '+352 20 123 456', 'hannah.lee@example.com'),
       ('Isaac Kim', '3333 Pine St, Tallinn, Estonia', '+372 6 123 456', 'isaac.kim@example.com'),
       ('Jessica Davis', '3434 Fir St, Vilnius, Lithuania', '+370 5 1234567', 'jessica.davis@example.com'),
       ('Kevin White', '3535 Cedar St, Riga, Latvia', '+371 6 1234567', 'kevin.white@example.com'),
       ('Linda Scott', '3636 Birch St, Bratislava, Slovakia', '+421 2 1234567', 'linda.scott@example.com'),
       ('Mike Brown', '3737 Elm St, Ljubljana, Slovenia', '+386 1 123 4567', 'mike.brown@example.com'),
       ('Nancy Johnson', '3838 Oak St, Sarajevo, Bosnia and Herzegovina', '+387 33 123 456',
        'nancy.johnson@example.com'),
       ('Oliver Green', '3939 Pine St, Sofia, Bulgaria', '+359 2 123 4567', 'oliver.green@example.com'),
       ('Pamela Walker', '4040 Fir St, Chisinau, Moldova', '+373 22 123 456', 'pamela.walker@example.com'),
       ('Quincy Brown', '4141 Cedar St, Pristina, Kosovo', '+383 38 123 456', 'quincy.brown@example.com'),
       ('Rachel Clark', '4242 Birch St, Tirana, Albania', '+355 4 123 4567', 'rachel.clark@example.com'),
       ('Steven Moore', '4343 Elm St, Skopje, North Macedonia', '+389 2 123 4567', 'steven.moore@example.com'),
       ('Tracy Young', '4444 Oak St, Belgrade, Serbia', '+381 11 1234567', 'tracy.young@example.com');

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