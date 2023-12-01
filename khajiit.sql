-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : ven. 01 déc. 2023 à 11:56
-- Version du serveur : 8.0.31
-- Version de PHP : 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `khajiit`
--

-- --------------------------------------------------------

--
-- Structure de la table `armors`
--

DROP TABLE IF EXISTS `armors`;
CREATE TABLE IF NOT EXISTS `armors` (
  `id` int NOT NULL AUTO_INCREMENT,
  `item_id` int DEFAULT NULL,
  `slot` enum('Head','Shoulders','Torso','Wrists','Hands','Waist','Legs','Feet','Jewelry') CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `item_id` (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `armors`
--

INSERT INTO `armors` (`id`, `item_id`, `slot`) VALUES
(1, 2, 'Torso'),
(3, 8, 'Hands');

-- --------------------------------------------------------

--
-- Structure de la table `customers`
--

DROP TABLE IF EXISTS `customers`;
CREATE TABLE IF NOT EXISTS `customers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `wallet` decimal(8,2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `items`
--

DROP TABLE IF EXISTS `items`;
CREATE TABLE IF NOT EXISTS `items` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `type` enum('Weapon','Armor','Accessory','') CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  `rarity` varchar(255) DEFAULT NULL,
  `stat` int NOT NULL,
  `price` decimal(8,2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `items`
--

INSERT INTO `items` (`id`, `name`, `type`, `rarity`, `stat`, `price`) VALUES
(1, 'Deathwish', 'Weapon', 'Legendary', 347, '980.00'),
(2, 'Requiem Cereplate', 'Armor', 'Legendary', 68, '1280.00'),
(5, 'The Burning Axe of Sankis', 'Weapon', 'Legendary', 275, '920.00'),
(6, 'Deadly Rebirth', 'Weapon', 'Legendary', 11, '540.00'),
(7, 'Fury of the Vanished Peak', 'Weapon', 'Legendary', 126, '1280.00'),
(8, 'Delightful Cuirass', 'Armor', 'Legendary', 58, '1258.00'),
(9, 'Le D\0serteur', 'Weapon', 'Legendary', 175, '2400.00'),
(10, 'Arme Random', 'Weapon', 'Rare', 123, '120.00');

-- --------------------------------------------------------

--
-- Structure de la table `item_properties`
--

DROP TABLE IF EXISTS `item_properties`;
CREATE TABLE IF NOT EXISTS `item_properties` (
  `id` int NOT NULL AUTO_INCREMENT,
  `item_id` int DEFAULT NULL,
  `property_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `item_id` (`item_id`),
  KEY `property_id` (`property_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `item_properties`
--

INSERT INTO `item_properties` (`id`, `item_id`, `property_id`) VALUES
(1, 1, 1),
(2, 2, 2),
(6, 5, 4),
(7, 6, 5),
(8, 7, 6),
(9, 8, 7),
(10, 9, 8),
(11, 10, 9);

-- --------------------------------------------------------

--
-- Structure de la table `properties`
--

DROP TABLE IF EXISTS `properties`;
CREATE TABLE IF NOT EXISTS `properties` (
  `id` int NOT NULL AUTO_INCREMENT,
  `element` enum('Physical','Poison','Cold','Lightning','Holy','Arcane','Fire','Earth') DEFAULT 'Physical',
  `bonus` int DEFAULT NULL,
  `unique_ability` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `properties`
--

INSERT INTO `properties` (`id`, `element`, `bonus`, `unique_ability`) VALUES
(1, 'Physical', 981, 'While channeling Arcane Torrent, Disintegrate or Ray of Frost for at least 1 second, all damage is increased by 294%.'),
(2, 'Physical', 416, 'Devour restores an additional 85% Essence and Life. In addition, when Devour restores Essence or Life above your maximum, the excess is granted over 3 seconds.'),
(3, 'Physical', 147, 'Test'),
(4, 'Physical', 981, 'Update test.'),
(5, 'Arcane', 981, 'Grasp of the Dead gains the effect of the Rain of Corpses rune.'),
(6, 'Holy', 946, 'Reduces the Fury cost of Seismic Slam by 50% and increases its damage by 458%.'),
(7, 'Lightning', 587, 'Test'),
(8, 'Poison', 789, 'Blows a poisonous breath.'),
(9, 'Physical', 123, '0');

-- --------------------------------------------------------

--
-- Structure de la table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
CREATE TABLE IF NOT EXISTS `transactions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `customer_id` int DEFAULT NULL,
  `vendor_id` int DEFAULT NULL,
  `item_id` int DEFAULT NULL,
  `type` varchar(255) DEFAULT NULL,
  `amount` decimal(5,2) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `customer_id` (`customer_id`),
  KEY `vendor_id` (`vendor_id`),
  KEY `item_id` (`item_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `vendors`
--

DROP TABLE IF EXISTS `vendors`;
CREATE TABLE IF NOT EXISTS `vendors` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `wallet` decimal(8,2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `vendors`
--

INSERT INTO `vendors` (`id`, `name`, `wallet`) VALUES
(1, 'Deirdre', '8500.00'),
(2, 'Kjarllan', '2500.00');

-- --------------------------------------------------------

--
-- Structure de la table `vendor_inventory`
--

DROP TABLE IF EXISTS `vendor_inventory`;
CREATE TABLE IF NOT EXISTS `vendor_inventory` (
  `id` int NOT NULL AUTO_INCREMENT,
  `vendor_id` int NOT NULL,
  `item_id` int NOT NULL,
  `quantity` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `item_id` (`item_id`),
  KEY `vendor_id` (`vendor_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `vendor_inventory`
--

INSERT INTO `vendor_inventory` (`id`, `vendor_id`, `item_id`, `quantity`) VALUES
(5, 1, 2, 2);

-- --------------------------------------------------------

--
-- Structure de la table `warehouse`
--

DROP TABLE IF EXISTS `warehouse`;
CREATE TABLE IF NOT EXISTS `warehouse` (
  `id` int NOT NULL AUTO_INCREMENT,
  `item_id` int NOT NULL,
  `quantity` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `item_id` (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `warehouse`
--

INSERT INTO `warehouse` (`id`, `item_id`, `quantity`) VALUES
(1, 1, 0),
(2, 7, 18),
(4, 8, 10);

-- --------------------------------------------------------

--
-- Structure de la table `weapons`
--

DROP TABLE IF EXISTS `weapons`;
CREATE TABLE IF NOT EXISTS `weapons` (
  `id` int NOT NULL AUTO_INCREMENT,
  `item_id` int DEFAULT NULL,
  `type` enum('Axe','Dagger','Mace','Spear','Sword','Ceremonial Knife','Fist Weapon','Flail','Mighty Weapon','Scythe','Polearm','Staff','Daibo','Bow','Crossbow','Hand Crossbow','Wand') CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  `size` enum('One-Handed','Two-Handed','Ranged') CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `item_id` (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `weapons`
--

INSERT INTO `weapons` (`id`, `item_id`, `type`, `size`) VALUES
(1, 1, 'Sword', 'One-Handed'),
(2, 5, 'Axe', NULL),
(3, 6, 'Ceremonial Knife', NULL),
(4, 7, 'Mighty Weapon', 'Two-Handed'),
(5, 9, 'Scythe', ''),
(6, 10, 'Scythe', '');

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `armors`
--
ALTER TABLE `armors`
  ADD CONSTRAINT `armors_ibfk_1` FOREIGN KEY (`item_id`) REFERENCES `items` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT;

--
-- Contraintes pour la table `item_properties`
--
ALTER TABLE `item_properties`
  ADD CONSTRAINT `item_properties_ibfk_1` FOREIGN KEY (`item_id`) REFERENCES `items` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  ADD CONSTRAINT `item_properties_ibfk_2` FOREIGN KEY (`property_id`) REFERENCES `properties` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT;

--
-- Contraintes pour la table `transactions`
--
ALTER TABLE `transactions`
  ADD CONSTRAINT `transactions_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`id`),
  ADD CONSTRAINT `transactions_ibfk_2` FOREIGN KEY (`vendor_id`) REFERENCES `vendors` (`id`),
  ADD CONSTRAINT `transactions_ibfk_3` FOREIGN KEY (`item_id`) REFERENCES `items` (`id`);

--
-- Contraintes pour la table `vendor_inventory`
--
ALTER TABLE `vendor_inventory`
  ADD CONSTRAINT `item_id` FOREIGN KEY (`item_id`) REFERENCES `items` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  ADD CONSTRAINT `vendor_id` FOREIGN KEY (`vendor_id`) REFERENCES `vendors` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `warehouse`
--
ALTER TABLE `warehouse`
  ADD CONSTRAINT `FK_warehouse_item` FOREIGN KEY (`item_id`) REFERENCES `items` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Contraintes pour la table `weapons`
--
ALTER TABLE `weapons`
  ADD CONSTRAINT `weapons_ibfk_1` FOREIGN KEY (`item_id`) REFERENCES `items` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
