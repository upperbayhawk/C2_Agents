CREATE DATABASE IF NOT EXISTS CurrentForCarbon;
USE CurrentForCarbon;
CREATE TABLE PlayerConfidential(
PlayerIdx INT NOT NULL AUTO_INCREMENT,
PlayerName VARCHAR(255) NOT NULL,
PlayerID VARCHAR(255) NOT NULL,
playerNumber  VARCHAR(255) NOT NULL,
playerStreet   VARCHAR(255) NOT NULL,
playerCity  VARCHAR(255) NOT NULL,
PlayerState VARCHAR(255) NOT NULL,
PlayerZipcode VARCHAR(255) NOT NULL,
PlayerEmail VARCHAR(255) NOT NULL,
PlayerPhone VARCHAR(255) NOT NULL,
PlayerElectricCo VARCHAR(255) NOT NULL,
PlayerETHAddress VARCHAR(255) NOT NULL,
PlayerETHKey VARCHAR(255) NOT NULL,
PlayerETHContract VARCHAR(255) NOT NULL,
PlayerDataConnectString VARCHAR(255) NOT NULL,
PlayerYTDYear VARCHAR(255),
PlayerYTDPoints VARCHAR(255),
PlayerYTDAwards VARCHAR(255),
PRIMARY KEY (PlayerIdx,PlayerID,PlayerName));