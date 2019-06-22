/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : darkgod

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2019-06-22 16:40:57
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for account
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `acct` varchar(255) DEFAULT NULL,
  `pass` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `level` int(11) DEFAULT NULL,
  `exp` int(11) DEFAULT NULL,
  `power` int(11) DEFAULT NULL,
  `coin` int(11) DEFAULT NULL,
  `diamond` int(11) DEFAULT NULL,
  `ad` int(11) DEFAULT NULL,
  `hp` int(11) DEFAULT NULL,
  `ap` int(11) DEFAULT NULL,
  `addef` int(11) DEFAULT NULL,
  `apdef` int(11) DEFAULT NULL,
  `dodge` int(11) DEFAULT NULL,
  `pierce` int(11) DEFAULT NULL,
  `critical` int(11) DEFAULT NULL,
  `guideid` int(11) DEFAULT NULL,
  `crystal` int(11) DEFAULT NULL,
  `strong` varchar(255) DEFAULT NULL,
  `time` bigint(20) DEFAULT NULL,
  `task` varchar(255) DEFAULT NULL,
  `fuben` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of account
-- ----------------------------
INSERT INTO `account` VALUES ('1', '111', '111', '屠苏婉', '1', '0', '150', '5000', '500', '275', '2000', '265', '67', '43', '7', '5', '2', '1001', null, null, null, null, null);
INSERT INTO `account` VALUES ('2', 'bearyang', '1', '狄越', '1', '0', '150', '5000', '500', '275', '2000', '265', '67', '43', '7', '5', '2', '1001', null, null, null, null, null);
INSERT INTO `account` VALUES ('3', 'we', 'we', '殷武', '1', '0', '150', '5000', '500', '275', '2000', '265', '67', '43', '7', '5', '2', '1001', '500', '0#0#0#0#0#0#', '1561192766578', '1|0|0#2|0|0#3|0|0#4|0|0#5|0|0#6|0|0#', '10001');
