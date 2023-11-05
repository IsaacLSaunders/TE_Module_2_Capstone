
USE master
GO

IF DB_ID('tebucksV2') IS NOT NULL
BEGIN
	ALTER DATABASE tebucksV2 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE tebucksV2;
END

CREATE DATABASE tebucksV2
GO

USE tebucksV2
GO


CREATE TABLE users (
	user_id int IDENTITY(1001,1) NOT NULL,
	firstname varchar(50) NOT NULL,
	lastname varchar(50) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (user_id),
	CONSTRAINT UQ_username UNIQUE (username)
)

CREATE TABLE accounts(
	account_id int IDENTITY (1,1) NOT NULL,
	userId int NOT NULL,
	balance int NOT NULL
	CONSTRAINT PK_account PRIMARY KEY (account_id)
)

CREATE TABLE transfers(
	transferId int IDENTITY (1,1) NOT NULL,
	transferType varchar(50) NOT NULL,
	transferStatus varchar(50) NOT NULL,
	userFrom int NOT NULL,
	userTo int NOT NULL,
	amount money NOT NULL,
	CONSTRAINT PK_transfer PRIMARY KEY (transferId),
	CONSTRAINT CHK_amount CHECK(amount > 0)
)

ALTER TABLE accounts ADD CONSTRAINT FK_accounts_users foreign key(userId) references users(user_id);
ALTER TABLE transfers ADD CONSTRAINT FK_transfers_from_users foreign key(userFrom) references users(user_id);
ALTER TABLE transfers ADD CONSTRAINT FK_transfers_to_users foreign key(userto) references users(user_id);
ALTER TABLE accounts ADD CONSTRAINT DF_balance DEFAULT 1000.00 FOR balance;



