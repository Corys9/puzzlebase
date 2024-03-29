-- MySQL dump 10.13  Distrib 8.0.18, for Win64 (x86_64)
--
-- Host: localhost    Database: puzzlebase
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `ProductVersion` varchar(32) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('00000000000000_CreateIdentitySchema','3.0.0');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL,
  `RoleId` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ClaimType` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ClaimValue` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroleclaims`
--

LOCK TABLES `aspnetroleclaims` WRITE;
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ConcurrencyStamp` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ClaimType` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ClaimValue` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserclaims`
--

LOCK TABLES `aspnetuserclaims` WRITE;
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ProviderKey` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ProviderDisplayName` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `UserId` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserlogins`
--

LOCK TABLES `aspnetuserlogins` WRITE;
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `RoleId` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserroles`
--

LOCK TABLES `aspnetuserroles` WRITE;
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusers` (
  `Id` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `SecurityStamp` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ConcurrencyStamp` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `PhoneNumber` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` timestamp(6) NULL DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusers`
--

LOCK TABLES `aspnetusers` WRITE;
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` VALUES ('580c9cb0-ea37-4865-91cf-5724e81fda8f','Teodor Najdan Trifunov','TAMIRLYN@GMAIL.COM','tamirlyn@gmail.com','TAMIRLYN@GMAIL.COM',_binary '\0','AQAAAAEAACcQAAAAECKCJMQATYbjWB3CAGRhKO0nNg8UPJTmSqj6qIZFlcv26iTQcN18xCMzMo7WsNsM9A==','MRKECPL2EST2KH7WV6W7XVTVNTVDSV55','3479baf5-7638-4a06-888f-f9e8656a29a2',NULL,_binary '\0',_binary '\0',NULL,_binary '',0);
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `LoginProvider` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Value` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusertokens`
--

LOCK TABLES `aspnetusertokens` WRITE;
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `author`
--

DROP TABLE IF EXISTS `author`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `author` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `WebSite` varchar(255) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `author`
--

LOCK TABLES `author` WRITE;
/*!40000 ALTER TABLE `author` DISABLE KEYS */;
INSERT INTO `author` VALUES (1,'WSC 2019','http://www.worldpuzzle.org/wpf-archive/2019/'),(2,'Rajesh Kumar','https://www.funwithpuzzles.com'),(3,'Krazy Dad','https://krazydad.com');
/*!40000 ALTER TABLE `author` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `history`
--

DROP TABLE IF EXISTS `history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `history` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `PuzzleID` int(11) NOT NULL,
  `IsCompleted` tinyint(1) NOT NULL,
  `Duration` time DEFAULT NULL,
  `State` varchar(2000) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `LastUpdatedTS` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_History_Puzzle_idx` (`PuzzleID`),
  KEY `fk_History_User_idx` (`UserID`),
  CONSTRAINT `fk_History_Puzzle` FOREIGN KEY (`PuzzleID`) REFERENCES `puzzle` (`ID`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_History_User` FOREIGN KEY (`UserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `history`
--

LOCK TABLES `history` WRITE;
/*!40000 ALTER TABLE `history` DISABLE KEYS */;
/*!40000 ALTER TABLE `history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `puzzle`
--

DROP TABLE IF EXISTS `puzzle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `puzzle` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `AuthorID` int(11) DEFAULT NULL,
  `Content` varchar(2000) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `CreatedTS` datetime NOT NULL,
  `Difficulty` int(11) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `OwnerID` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_Puzzle_Author_idx` (`AuthorID`),
  KEY `fk_Puzzle_Owner_idx` (`OwnerID`),
  CONSTRAINT `fk_Puzzle_Author` FOREIGN KEY (`AuthorID`) REFERENCES `author` (`ID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_Puzzle_Owner` FOREIGN KEY (`OwnerID`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `puzzle`
--

LOCK TABLES `puzzle` WRITE;
/*!40000 ALTER TABLE `puzzle` DISABLE KEYS */;
INSERT INTO `puzzle` VALUES (1,1,'{\n   \"givens\": [\n      [1,1,1],[1,2,2],\n      [2,1,3],[2,2,4],[2,4,5],[2,6,7],[2,7,9],\n      [3,4,6],[3,8,5],\n      [4,2,9],[4,3,8],[4,4,7],[4,8,4],\n      [6,2,6],[6,6,3],[6,7,2],[6,8,1],\n      [7,2,8],[7,6,4],\n      [8,3,4],[8,4,8],[8,6,5],[8,8,6],[8,9,7],\n      [9,8,8],[9,9,9]\n   ]\n}','2019-11-25 00:00:00',5,1,'580c9cb0-ea37-4865-91cf-5724e81fda8f'),(2,1,'{\n   \"givens\": [\n      [1,6,1],[1,7,5],\n      [2,5,2],\n      [3,1,6],[3,5,7],\n      [4,1,7],\n      [5,2,8],[5,3,5],[5,7,1],[5,8,9],\n      [6,9,2],\n      [7,5,6],[7,9,5],\n      [8,5,1],\n      [9,3,7],[9,4,4]\n   ],\n   \"constraints\": [\n      {\n         \"type\": \"thermo\",\n         \"thermos\": [\n            [ [1,2,\"c-se\"],[2,3,\"nw-se\"],[3,4,\"nw-se\"],[4,5,\"c-nw\"] ],\n            [ [2,9,\"c-sw\"],[3,8,\"ne-sw\"],[4,7,\"ne-sw\"],[5,6,\"c-ne\"] ],\n            [ [4,4,\"c-nw\"],[3,3,\"nw-se\"],[2,2,\"nw-se\"],[1,1,\"c-se\"] ],\n            [ [4,6,\"c-ne\"],[3,7,\"ne-sw\"],[2,8,\"ne-sw\"],[1,9,\"c-sw\"] ],\n            [ [6,4,\"c-sw\"],[7,3,\"ne-sw\"],[8,2,\"ne-sw\"],[9,1,\"c-ne\"] ],\n            [ [6,6,\"c-se\"],[7,7,\"nw-se\"],[8,8,\"nw-se\"],[9,9,\"c-nw\"] ],\n            [ [8,1,\"c-ne\"],[7,2,\"ne-sw\"],[6,3,\"ne-sw\"],[5,4,\"c-sw\"] ],\n            [ [9,8,\"c-nw\"],[8,7,\"nw-se\"],[7,6,\"nw-se\"],[6,5,\"c-se\"] ]\n         ]\n      }\n   ]\n}','2019-11-26 09:58:00',6,0,'580c9cb0-ea37-4865-91cf-5724e81fda8f'),(3,2,'{\n   \"givens\": [\n      [1,3,4],[1,7,3],\n      [2,2,9],[2,4,4],[2,6,1],[2,8,5],\n      [3,1,6],[3,9,4],\n      [4,2,8],[4,4,7],[4,6,4],[4,8,3],\n      [5,5,5],\n      [6,2,1],[6,4,9],[6,6,6],[6,8,4],\n      [7,1,2],[7,9,3],\n      [8,2,5],[8,4,8],[8,6,2],[8,8,1],\n      [9,3,9],[9,7,2]\n   ],\n   \"constraints\": [\n      {\n         \"type\": \"jigsaw\",\n         \"regions\": [\n            [11,21,22,23,24,25,33,34,35],\n            [12,13,14,15,16,17,18,27,37],\n            [19,29,39,49,59,69,79,88,89],\n            [26,36,46,47,53,54,55,56,63],\n            [28,38,48,57,58,65,66,67,68],\n            [31,32,41,42,43,44,45,51,52],\n            [61,62,71,81,82,83,91,92,93],\n            [64,72,73,74,84,85,86,94,95],\n            [75,76,77,78,87,96,97,98,99]\n         ]\n      }\n   ]\n}','2019-11-26 18:12:00',6,1,'580c9cb0-ea37-4865-91cf-5724e81fda8f'),(4,3,'{\n   \"givens\": [],\n   \"constraints\": [\n      {\n         \"type\": \"killer\",\n         \"cages\": [\n            { \"sum\": 17, \"boxes\": [11,21,31,32] },\n            { \"sum\": 17, \"boxes\": [12,22,23] },\n            { \"sum\": 16, \"boxes\": [13,14] },\n            { \"sum\": 17, \"boxes\": [15,25,35] },\n            { \"sum\": 7, \"boxes\": [16,17] },\n	    { \"sum\": 8, \"boxes\": [18,27,28] },\n            { \"sum\": 25, \"boxes\": [19,29,38,39] },\n            { \"sum\": 8, \"boxes\": [24,34,44] },\n            { \"sum\": 20, \"boxes\": [26,36,46] },\n            { \"sum\": 22, \"boxes\": [33,41,42,43] },\n            { \"sum\": 18, \"boxes\": [37,47,48,49] },\n            { \"sum\": 19, \"boxes\": [45,54,55,56] },\n            { \"sum\": 7, \"boxes\": [51,52] },\n            { \"sum\": 24, \"boxes\": [53,63,73,83] },\n            { \"sum\": 18, \"boxes\": [57,67,77,87] },\n	    { \"sum\": 7, \"boxes\": [58,59] },\n            { \"sum\": 11, \"boxes\": [61,62] },\n            { \"sum\": 15, \"boxes\": [64,65,66] },\n            { \"sum\": 15, \"boxes\": [68,69] },\n            { \"sum\": 20, \"boxes\": [71,72,82] },\n            { \"sum\": 13, \"boxes\": [74,75,76] },\n            { \"sum\": 12, \"boxes\": [78,79,88] },\n            { \"sum\": 6, \"boxes\": [81,91] },\n	    { \"sum\": 32, \"boxes\": [84,85,86,94,95,96] },\n            { \"sum\": 10, \"boxes\": [89,99] },\n            { \"sum\": 4, \"boxes\": [92,93] },\n            { \"sum\": 17, \"boxes\": [97,98] }\n         ]\n      }\n   ]\n}','2019-11-26 18:42:00',7,1,'580c9cb0-ea37-4865-91cf-5724e81fda8f');
/*!40000 ALTER TABLE `puzzle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ActivatedTS` datetime DEFAULT NULL,
  `CreatedTS` datetime NOT NULL,
  `DisplayName` varchar(45) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `Email` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `IsAdministrator` tinyint(1) NOT NULL,
  `IsSuspended` tinyint(1) NOT NULL,
  `LastLoginTS` datetime DEFAULT NULL,
  `PasswordHash` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `SuspensionReason` varchar(255) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'2019-11-25 00:00:00','2019-11-25 00:00:00','Teodor','tamirlyn@gmail.com',1,0,NULL,'test',NULL);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-12-05 14:45:02
