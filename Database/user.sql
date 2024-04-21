DROP TABLE IF EXISTS users;
CREATE TABLE users (
  username TEXT DEFAULT NULL,
  userpassword TEXT DEFAULT NULL,
  userrole TEXT DEFAULT NULL
);

INSERT INTO users (username, userpassword, userrole) VALUES
('Admin', 'null', 'Admin'),
('User', '0', 'User'),
('Poster', '1', 'Poster'),
('Putter', '2', 'Put');