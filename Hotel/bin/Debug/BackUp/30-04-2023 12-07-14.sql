-- MySqlBackup.NET 2.3.6
-- Dump Time: 2023-04-30 12:07:14
-- --------------------------------------
-- Server version 5.6.51 MySQL Community Server (GPL)


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of client
-- 

DROP TABLE IF EXISTS `client`;
CREATE TABLE IF NOT EXISTS `client` (
  `IDclient` int(11) NOT NULL AUTO_INCREMENT,
  `surname` varchar(60) NOT NULL,
  `name` varchar(60) NOT NULL,
  `patronymic` varchar(60) NOT NULL,
  `birth_date` date NOT NULL,
  `age` int(3) NOT NULL,
  `phone_number` varchar(11) NOT NULL,
  `passport_series` varchar(4) NOT NULL,
  `passport_number` varchar(6) NOT NULL,
  `date_of_issue` date NOT NULL,
  PRIMARY KEY (`IDclient`),
  UNIQUE KEY `IDclient_UNIQUE` (`IDclient`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table client
-- 

/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client`(`IDclient`,`surname`,`name`,`patronymic`,`birth_date`,`age`,`phone_number`,`passport_series`,`passport_number`,`date_of_issue`) VALUES(1,'Мухина','Екатерина','Евгеньевна','2004-07-30 00:00:00',18,'88005553535','1234','789654','2018-08-08 00:00:00'),(4,'Фролов','Максим','Артурович','1963-11-14 00:00:00',59,'89684749596','4680','963004','2019-01-30 00:00:00'),(5,'Андреев','Иван','Арсентьевич','1982-01-13 00:00:00',41,'89967503520','4499','285409','2020-01-12 00:00:00'),(6,'Гаврилова','София','Ивановна','1998-07-29 00:00:00',24,'89329656329','4572','121456','2015-03-18 00:00:00'),(7,'Федорова','Сафия','Денисовна','1985-06-19 00:00:00',37,'89329656329','4333','460681','2019-07-17 00:00:00'),(8,'Кузьмина','Кира','Константиновна','1995-09-25 00:00:00',27,'89875021704','4522','187890','2019-05-31 00:00:00'),(9,'Колпакова','Мария','Адамовна','1968-04-22 00:00:00',54,'89169415016','4634','510600','2021-05-15 00:00:00'),(10,'Демидов','Тимофей','Дмитриевич','1997-11-27 00:00:00',25,'89241249144','4086','154604','2019-02-25 00:00:00'),(11,'Маркова','Валерия','Юрьевна','1981-07-01 00:00:00',41,'89013683826','4692','986342','2014-12-02 00:00:00'),(12,'Калмыков','Тимур','Степанович','1985-05-29 00:00:00',37,'89166059872','4019','918052','2013-09-06 00:00:00'),(13,'Еремеева','Агата','Тимофеевна','2000-10-30 00:00:00',22,'89118259562','4771','869642','2018-05-26 00:00:00'),(14,'Морозов','Николай','Артёмович','1973-03-26 00:00:00',49,'89869232894','4418','519893','2020-11-19 00:00:00'),(15,'Зимин','Давид','Данилович','1989-04-03 00:00:00',33,'89154826902','4641','4641','2020-02-29 00:00:00'),(16,'Комаров','Матвей','Львович','1985-07-06 00:00:00',37,'89233470518','4956','902897','2019-01-22 00:00:00'),(17,'Иванов','Иван','Иванович','1999-03-02 00:00:00',24,'88005553535','1234','123456','2015-02-02 00:00:00');
/*!40000 ALTER TABLE `client` ENABLE KEYS */;

-- 
-- Definition of feed
-- 

DROP TABLE IF EXISTS `feed`;
CREATE TABLE IF NOT EXISTS `feed` (
  `IDfeed` int(11) NOT NULL AUTO_INCREMENT,
  `f_name` varchar(50) NOT NULL,
  `f_cost` double NOT NULL,
  PRIMARY KEY (`IDfeed`),
  UNIQUE KEY `IDfeed_UNIQUE` (`IDfeed`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table feed
-- 

/*!40000 ALTER TABLE `feed` DISABLE KEYS */;
INSERT INTO `feed`(`IDfeed`,`f_name`,`f_cost`) VALUES(1,'Завтрак',300),(2,'Завтрак+обед',600),(3,'Завтрак+обед+ужин',900),(4,'Без питания',0);
/*!40000 ALTER TABLE `feed` ENABLE KEYS */;

-- 
-- Definition of room
-- 

DROP TABLE IF EXISTS `room`;
CREATE TABLE IF NOT EXISTS `room` (
  `NUMroom` int(11) NOT NULL AUTO_INCREMENT,
  `r_name` varchar(45) NOT NULL,
  `area` int(2) NOT NULL,
  `visitors_quant` int(1) NOT NULL,
  `cost` double NOT NULL,
  `available_quant` int(1) NOT NULL,
  `image` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`NUMroom`),
  UNIQUE KEY `NUMroom_UNIQUE` (`NUMroom`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table room
-- 

/*!40000 ALTER TABLE `room` DISABLE KEYS */;
INSERT INTO `room`(`NUMroom`,`r_name`,`area`,`visitors_quant`,`cost`,`available_quant`,`image`) VALUES(1,'Однокомнатный',18,1,2800,3,'id1.jpg'),(2,'Однокомнатный',20,2,3300,1,'id2.jpg'),(4,'Полулюкс',40,4,6600,1,'id4.jpg'),(5,'Студия',42,5,7000,2,'id5.jpg'),(6,'Люкс',40,4,7300,2,'id6.jpg'),(7,'Люкс',80,2,9000,0,'id6.jpg'),(9,'Двухкомнатный',22,3,4200,1,'id9.jpg');
/*!40000 ALTER TABLE `room` ENABLE KEYS */;

-- 
-- Definition of booking
-- 

DROP TABLE IF EXISTS `booking`;
CREATE TABLE IF NOT EXISTS `booking` (
  `reg_num` int(11) NOT NULL AUTO_INCREMENT,
  `arrival_date` date NOT NULL,
  `departure_date` date NOT NULL,
  `IDclient` int(11) NOT NULL,
  `NUMroom` int(11) NOT NULL,
  `count_day` int(15) NOT NULL,
  `IDfeed` int(11) NOT NULL,
  `amount` double NOT NULL,
  PRIMARY KEY (`reg_num`),
  UNIQUE KEY `reg_num_UNIQUE` (`reg_num`),
  KEY `fk_booking_client_idx` (`IDclient`),
  KEY `fk_booking_room1_idx` (`NUMroom`),
  KEY `fk_booking_feed1_idx` (`IDfeed`),
  CONSTRAINT `fk_booking_client` FOREIGN KEY (`IDclient`) REFERENCES `client` (`IDclient`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_booking_feed1` FOREIGN KEY (`IDfeed`) REFERENCES `feed` (`IDfeed`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_booking_room1` FOREIGN KEY (`NUMroom`) REFERENCES `room` (`NUMroom`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table booking
-- 

/*!40000 ALTER TABLE `booking` DISABLE KEYS */;
INSERT INTO `booking`(`reg_num`,`arrival_date`,`departure_date`,`IDclient`,`NUMroom`,`count_day`,`IDfeed`,`amount`) VALUES(1,'2023-01-23 00:00:00','2023-01-28 00:00:00',1,1,4,3,6800),(48,'2023-03-03 00:00:00','2023-03-09 00:00:00',5,5,6,1,10000),(50,'2023-02-05 00:00:00','2023-02-15 00:00:00',4,5,10,3,15600),(51,'2023-04-18 00:00:00','2023-04-20 00:00:00',5,6,2,2,8000),(52,'2023-08-09 00:00:00','2023-08-18 00:00:00',6,7,9,1,3500),(53,'2023-07-07 00:00:00','2023-07-20 00:00:00',7,6,13,2,6600),(54,'2023-06-25 00:00:00','2023-06-30 00:00:00',8,5,5,3,4500),(55,'2023-03-30 00:00:00','2023-04-02 00:00:00',9,4,3,2,9300),(56,'2023-04-18 00:00:00','2023-04-30 00:00:00',10,6,12,1,4500),(57,'2023-04-01 00:00:00','2023-04-15 00:00:00',11,2,14,2,9800),(58,'2023-12-12 00:00:00','2023-12-15 00:00:00',12,1,3,3,10200),(59,'2023-10-15 00:00:00','2023-10-25 00:00:00',13,2,10,2,3000),(60,'2023-09-30 00:00:00','2023-10-15 00:00:00',14,6,16,1,4500),(61,'2023-02-14 00:00:00','2023-02-15 00:00:00',15,4,1,3,3600),(62,'2023-05-16 00:00:00','2023-05-19 00:00:00',16,5,3,3,8900),(63,'2023-03-03 00:00:00','2023-05-05 00:00:00',12,5,3,3,6);
/*!40000 ALTER TABLE `booking` ENABLE KEYS */;

-- 
-- Definition of user
-- 

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `IDuser` int(11) NOT NULL AUTO_INCREMENT,
  `u_name` varchar(60) NOT NULL,
  `u_login` varchar(45) NOT NULL,
  `u_password` varchar(100) NOT NULL,
  `u_type` enum('admin','user') NOT NULL,
  PRIMARY KEY (`IDuser`),
  UNIQUE KEY `IDuser_UNIQUE` (`IDuser`),
  UNIQUE KEY `u_login_UNIQUE` (`u_login`),
  UNIQUE KEY `u_password_UNIQUE` (`u_password`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table user
-- 

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user`(`IDuser`,`u_name`,`u_login`,`u_password`,`u_type`) VALUES(1,'Иванов И.И.','admin','61646d696e','admin'),(2,'Петров П.A.','user','75736572','user');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2023-04-30 12:07:14
-- Total time: 0:0:0:0:200 (d:h:m:s:ms)
