/*
SQLyog Community v13.1.5  (64 bit)
MySQL - 8.0.18 : Database - csgo game data
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`csgo game data` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `csgo game data`;

/*Table structure for table `gamedata` */

DROP TABLE IF EXISTS `gamedata`;

CREATE TABLE `gamedata` (
  `UniqueGameNumber` double NOT NULL,
  `gDate` varchar(32) DEFAULT NULL,
  `Kills` int(11) DEFAULT NULL,
  `Assists` int(11) DEFAULT NULL,
  `Deaths` int(11) DEFAULT NULL,
  `Map` varchar(12) DEFAULT NULL,
  `MostUsedWeapon` varchar(32) DEFAULT NULL,
  `UserWins` int(11) DEFAULT NULL,
  `EnemyWins` int(11) DEFAULT NULL,
  `username` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`UniqueGameNumber`,`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `gamedata` */

insert  into `gamedata`(`UniqueGameNumber`,`gDate`,`Kills`,`Assists`,`Deaths`,`Map`,`MostUsedWeapon`,`UserWins`,`EnemyWins`,`username`) values 
(0,'2020-11-13',28,4,10,'Inferno','weapon_ak47',16,10,'Jumbo69'),
(0,'2020-11-14',27,10,5,'Mirage','weapon_negev',16,13,'Thumby25'),
(0,'2020-08-19',19,6,21,'Inferno','weapon_negev',14,16,'Zziolek03'),
(1,'2020-11-13',28,5,14,'Inferno','weapon_ak47',16,10,'Jumbo69'),
(1,'2020-10-03',30,15,10,'Inferno','weapon_negev',16,6,'Thumby25'),
(1,'2020-09-18',18,6,21,'Nuke','weapon_negev',13,16,'Zziolek03'),
(2,'2020-05-17',20,5,18,'Inferno','weapon_ma4a1_silencer',16,13,'Zziolek03'),
(3,'2020-05-18',23,10,20,'Nuke','weapon_ak47',16,9,'Zziolek03'),
(4,'2020-10-07',13,7,18,'Mirage','weapon_sg556',15,15,'Zziolek03'),
(5,'2020-09-13',18,6,20,'Mirage','weapon_m4a1_silencer',16,13,'Zziolek03'),
(6,'2020-09-21',17,6,20,'Inferno','weapon_galilar',16,13,'Zziolek03'),
(7,'2020-05-12',14,4,19,'Inferno','weapon_negev',15,15,'Zziolek03'),
(8,'2020-11-27',18,3,16,'Mirage','weapon_m4a1_silencer',16,13,'Zziolek03'),
(9,'2020-07-14',14,2,15,'Mirage','weapon_galilar',4,16,'Zziolek03'),
(10,'2020-11-24',13,8,16,'Inferno','weapon_galilar',14,16,'Zziolek03'),
(11,'2020-11-10',17,6,16,'Mirage','weapon_m4a1_silencer',15,15,'Zziolek03'),
(12,'2020-10-14',19,6,23,'Mirage','weapon_galilar',16,13,'Zziolek03'),
(13,'2020-10-22',18,6,19,'Inferno','weapon_aug',16,13,'Zziolek03'),
(14,'2020-05-10',24,14,19,'Mirage','weapon_galilar',16,8,'Zziolek03'),
(15,'2020-04-13',19,4,15,'Overpass','weapon_ak47',15,15,'Zziolek03'),
(16,'2020-10-21',14,4,19,'Overpass','weapon_negev',5,16,'Zziolek03'),
(17,'2020-10-07',17,6,18,'Overpass','weapon_ak47',16,14,'Zziolek03'),
(18,'2020-09-04',16,5,20,'Mirage','weapon_galilar',14,16,'Zziolek03'),
(19,'2020-06-13',19,7,15,'Overpass','weapon_bizon',16,7,'Zziolek03'),
(20,'2020-04-13',17,6,14,'Nuke','weapon_ak47',15,15,'Zziolek03'),
(21,'2019-12-27',14,5,18,'Inferno','weapon_m4a1',16,13,'Zziolek03'),
(22,'2020-08-15',19,7,14,'Mirage','weapon_m4a1_silencer',16,12,'Zziolek03'),
(23,'2020-07-19',14,5,13,'Overpass','weapon_ak47',16,7,'Zziolek03'),
(24,'2020-09-03',18,5,21,'Mirage','weapon_m4a1_silencer',15,15,'Zziolek03'),
(25,'2020-10-10',20,6,15,'Overpass','weapon_galilar',16,6,'Zziolek03'),
(26,'2020-11-30',18,5,17,'Overpass','weapon_ak47',16,12,'Zziolek03'),
(27,'2020-03-16',17,6,20,'Nuke','weapon_m4a1_silencer',15,15,'Zziolek03'),
(28,'2020-06-14',12,4,19,'Mirage','weapon_bizon',16,13,'Zziolek03'),
(29,'2020-03-16',17,3,12,'Overpass','weapon_ak47',12,16,'Zziolek03'),
(30,'2020-03-16',18,6,15,'Cache','weapon_ak47',15,15,'Zziolek03'),
(31,'2020-03-18',22,6,18,'Overpass','weapon_bizon',16,5,'Zziolek03'),
(32,'2020-03-05',18,12,20,'Cache','weapon_ak47',14,16,'Zziolek03'),
(33,'2020-10-05',18,6,15,'Mirage','weapon_ak47',16,13,'Zziolek03'),
(34,'2020-09-08',17,6,15,'Mirage','weapon_m4a1_silencer',16,14,'Zziolek03'),
(35,'2020-06-14',20,14,15,'Overpass','weapon_ak47',16,14,'Zziolek03'),
(36,'2020-12-05',4,5,16,'Vertigo','weapon_galilar',16,6,'Zziolek03'),
(37,'2020-06-17',6,6,15,'Mirage','weapon_ak47',16,8,'Zziolek03'),
(38,'2020-12-06',7,3,8,'Inferno','weapon_galilar',16,8,'Zziolek03'),
(39,'2020-12-06',6,9,16,'Nuke','weapon_ak47',16,10,'Zziolek03'),
(40,'2020-03-13',7,5,13,'Nuke','weapon_ak47',16,10,'Zziolek03'),
(41,'2020-02-07',18,13,10,'Inferno','weapon_m4a1_silencer',16,6,'Zziolek03'),
(42,'2020-02-09',16,4,10,'Inferno','weapon_ak47',14,16,'Zziolek03'),
(43,'2020-03-16',16,4,10,'Mirage','weapon_galilar',16,13,'Zziolek03'),
(44,'2020-11-10',12,6,14,'Inferno','weapon_galilar',15,15,'Zziolek03'),
(45,'2020-09-14',19,7,15,'Nuke','weapon_ak47',16,12,'Zziolek03'),
(46,'2020-12-05',15,4,10,'Mirage','weapon_m4a1_silencer',16,8,'Zziolek03'),
(47,'2020-09-08',17,54,13,'Vertigo','weapon_ak47',15,15,'Zziolek03'),
(48,'2020-10-16',15,10,20,'Nuke','weapon_ak47',16,14,'Zziolek03'),
(49,'2020-12-08',10,7,15,'Nuke','weapon_ump45',16,10,'Zziolek03'),
(50,'2020-12-27',10,6,20,'Inferno','weapon_ak47',16,10,'Zziolek03'),
(51,'2020-01-07',17,8,20,'Cache','weapon_ak47',16,7,'Zziolek03'),
(52,'2021-02-07',16,5,20,'Inferno','weapon_ak47',16,7,'Zziolek03');

/*Table structure for table `gundata` */

DROP TABLE IF EXISTS `gundata`;

CREATE TABLE `gundata` (
  `GunEntity` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `MagazineCapacity1` int(11) DEFAULT NULL,
  `MagazineCapacity2` int(11) DEFAULT NULL,
  `ReloadTime` double DEFAULT NULL,
  `Damage` int(11) DEFAULT NULL,
  `Recoil1` int(11) DEFAULT NULL,
  `Recoil2` int(11) DEFAULT NULL,
  `AccurateRange` double DEFAULT NULL,
  `ArmorPenetration` double DEFAULT NULL,
  `Price` double DEFAULT NULL,
  `KillAward` int(11) DEFAULT NULL,
  PRIMARY KEY (`GunEntity`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `gundata` */

insert  into `gundata`(`GunEntity`,`MagazineCapacity1`,`MagazineCapacity2`,`ReloadTime`,`Damage`,`Recoil1`,`Recoil2`,`AccurateRange`,`ArmorPenetration`,`Price`,`KillAward`) values 
('weapon_ak47',30,90,2.43,36,18,26,21.74,77.5,2700,300),
('weapon_aug',30,90,3.8,28,19,26,28,90,3300,300),
('weapon_awp',10,30,3.7,115,1,26,69,97.5,4750,100),
('weapon_bizon',64,120,2.4,27,21,26,10,60,1400,600),
('weapon_cz75a',12,12,2.83,31,17,26,11.35,77.65,500,100),
('weapon_deagle',7,35,2.2,63,3,26,24.58,93.2,700,300),
('weapon_elite',30,120,3.77,38,18,26,16.93,57.5,400,300),
('weapon_famas',25,90,3.3,30,21,26,15,70,2050,300),
('weapon_g3sg1',20,90,4.7,80,17,26,66,82.5,5000,300),
('weapon_glock',20,120,2.27,30,22,26,20.05,47,200,300),
('weapon_hkp2000',13,52,2.2,35,19,26,22,50.5,200,300),
('weapon_m249',100,200,5.7,32,19,26,16,80,5200,300),
('weapon_m4a1',30,90,3.1,33,20,26,28,70,3100,300),
('weapon_ma41_silencer',25,75,3.1,33,19,26,28,70,2900,300),
('weapon_mac10',30,100,3.2,29,21,26,11,57.5,1050,600),
('weapon_mag7',5,32,2.4,30,1,26,4.6,75,1300,900),
('weapon_mp5sd',30,120,2.97,27,21,25,15,62.5,1500,600),
('weapon_mp7',30,120,3.1,29,22,26,14,62.5,1500,600),
('weapon_mp9',30,120,2.1,26,21,26,16,60,1250,600),
('weapon_negev',150,300,5.7,35,20,26,13,71,1700,300),
('weapon_nova',8,32,0,26,1,26,3.2,50,1050,900),
('weapon_p250',13,26,2.2,38,18,26,14,64,300,300),
('weapon_p90',50,100,3.3,26,22,26,10,69,2360,300),
('weapon_revolver',8,8,2.3,86,0,0,25,93.2,600,300),
('weapon_sawedoff',7,32,3.2,32,1,26,2.21,75,1100,900),
('weapon_scar20',20,90,3.1,80,17,26,66,82.5,5000,300),
('weapon_sg556',30,90,2.77,30,18,26,24,100,3000,300),
('weapon_ssg08',10,90,3.7,88,12,26,47,85,1700,300),
('weapon_tec9',18,90,2.5,33,17,26,13,90.15,500,300),
('weapon_ump45',25,100,3.5,35,20,26,11,65,1200,600),
('weapon_usp_silencer',12,24,2.2,35,18,26,21,50.5,200,300),
('weapon_xm1014',7,32,2.8,20,1,26,3.4,80,2000,900);

/*Table structure for table `progress` */

DROP TABLE IF EXISTS `progress`;

CREATE TABLE `progress` (
  `OverallGameNumber` double NOT NULL,
  `MostMUW` varchar(32) DEFAULT NULL,
  `KillAverage` int(32) DEFAULT NULL,
  `AssistAverage` int(32) DEFAULT NULL,
  `DeathAverage` int(32) DEFAULT NULL,
  `WinPercentageIncrease` double DEFAULT NULL,
  `AdviceRecord` tinyint(1) DEFAULT NULL,
  `username` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`OverallGameNumber`,`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `progress` */

insert  into `progress`(`OverallGameNumber`,`MostMUW`,`KillAverage`,`AssistAverage`,`DeathAverage`,`WinPercentageIncrease`,`AdviceRecord`,`username`) values 
(48,'weapon_ak47',16,7,16,0.6,0,'Zziolek03'),
(49,'weapon_ak47',16,7,16,0.588235294117647,0,'Zziolek03'),
(50,'weapon_ak47',16,7,16,0.596153846153846,0,'Zziolek03'),
(51,'weapon_ak47',16,7,16,0.60377358490566,0,'Zziolek03'),
(52,'weapon_ak47',16,7,16,0.611111111111111,0,'Zziolek03'),
(53,'weapon_ak47',16,7,16,0.618181818181818,0,'Zziolek03');

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `username` varchar(10) NOT NULL,
  `steamid` varchar(32) DEFAULT NULL,
  `steamkey` varchar(64) DEFAULT NULL,
  `knowncode` varchar(64) DEFAULT NULL,
  `password` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`username`,`steamid`,`steamkey`,`knowncode`,`password`) values 
('Jshelby19','mini_ginge_','9BGC-NR9HB-C58L','CSGO-qSMZy-oTDLR-j6EDJ-PjFqW-e6siB',NULL),
('Jumbo69','JUmbo','8B75-GB4HB-ECKZ','CSGO-xOzjO-bX3V5-5XSwN-pjk7m-WNmkG',NULL),
('Thumby25','lolepicguy25','8B75-GB4HB-ECKD','CSGO-xOzjO-bX3V5-5XSwN-pjk7m-WNmkP',NULL),
('Zziolek03','pz1111','8TUD-HP8HT-ANKQ','CSGO-HTPHa-7WhrG-2LJrH-mkFqC-UoZUN',NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
