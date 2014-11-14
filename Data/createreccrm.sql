DROP TABLE IF EXISTS [companies];
CREATE TABLE `companies`
(
       id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
       name VARCHAR(50) NOT NULL,
       website VARCHAR(50)
, `notes`  TEXT);
DROP TABLE IF EXISTS [interactions];
CREATE TABLE `interactions`
(
       id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
       date DATETIME NOT NULL,
       recruiter_id INTEGER NOT NULL,
       lead_id INTEGER NOT NULL,
       notes TEXT
);
DROP TABLE IF EXISTS [interviews];
CREATE TABLE `interviews`
(
       id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
       date DATETIME NOT NULL,
       lead_id INTEGER NOT NULL,
       recruiter_id INTEGER NOT NULL,
       notes TEXT
);
DROP TABLE IF EXISTS [leads];
CREATE TABLE `leads`
(
       id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
       name VARCHAR(50) NOT NULL,
       website VARCHAR(50),
       notes TEXT
);
DROP TABLE IF EXISTS [recruiters];
CREATE TABLE `recruiters`
(
       id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
       name VARCHAR(50) NOT NULL,
       email VARCHAR(50) NOT NULL,
       telephone VARCHAR(50),
       company_id INTEGER NOT NULL,
       first_contact DATETIME NOT NULL,
       notes TEXT
);
