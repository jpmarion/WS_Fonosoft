-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         8.0.30 - MySQL Community Server - GPL
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.5.0.6677
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para fonosoft
CREATE DATABASE IF NOT EXISTS `fonosoft` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `fonosoft`;

-- Volcando estructura para tabla fonosoft.obrasocial
CREATE TABLE IF NOT EXISTS `obrasocial` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(200) COLLATE utf8mb4_general_ci NOT NULL DEFAULT '0',
  `FechaInicio` datetime DEFAULT NULL,
  `FechaFin` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla fonosoft.tipodocumento
CREATE TABLE IF NOT EXISTS `tipodocumento` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `FechaInicio` datetime NOT NULL,
  `FechaFin` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Nombre` (`Nombre`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla fonosoft.usuario
CREATE TABLE IF NOT EXISTS `usuario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NombreUsuario` text COLLATE utf8mb4_general_ci NOT NULL,
  `Email` text COLLATE utf8mb4_general_ci NOT NULL,
  `Password` text COLLATE utf8mb4_general_ci NOT NULL,
  `FechaCreacion` datetime NOT NULL,
  `Confirmacion` bit(1) NOT NULL DEFAULT (0),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para procedimiento fonosoft.ObraSocialDeshabilitar_U
DELIMITER //
CREATE PROCEDURE `ObraSocialDeshabilitar_U`(
	IN `@Id` INT
)
BEGIN
   UPDATE obrasocial
      SET 
         obrasocial.FechaFin = NOW()
   WHERE obrasocial.Id = `@Id`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.ObraSocialHabilitar_U
DELIMITER //
CREATE PROCEDURE `ObraSocialHabilitar_U`(
	IN `@Id` INT
)
BEGIN
   UPDATE obrasocial
      SET 
         obrasocial.FechaFin = '9999-12-31 23:59:59'
   WHERE obrasocial.Id = `@Id`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.ObraSocialXDescripcion_S
DELIMITER //
CREATE PROCEDURE `ObraSocialXDescripcion_S`(
	IN `@Nombre` VARCHAR(200)
)
BEGIN
   SELECT 
      obrasocial.Id
      ,obrasocial.Nombre
      ,obrasocial.FechaInicio
      ,obrasocial.FechaFin
   FROM obrasocial
   WHERE obrasocial.Nombre = `@Nombre`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.ObraSocialXId_S
DELIMITER //
CREATE PROCEDURE `ObraSocialXId_S`(
	IN `@Id` INT
)
BEGIN
   SELECT 
      obrasocial.Id
      ,obrasocial.Nombre
      ,obrasocial.FechaInicio
      ,obrasocial.FechaFin
   FROM obrasocial
   WHERE obrasocial.Id = `@Id`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.ObraSocial_I
DELIMITER //
CREATE PROCEDURE `ObraSocial_I`(
	IN `@Nombre` VARCHAR(200)
)
BEGIN
   INSERT INTO obrasocial(
      obrasocial.Nombre
      ,obrasocial.FechaInicio
      ,obrasocial.FechaFin)
   VALUE(
      `@Nombre`
      ,NOW()
      ,'9999-12-31 23:59:59');
      
   SELECT  LAST_INSERT_ID() Id;
   
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.ObraSocial_S
DELIMITER //
CREATE PROCEDURE `ObraSocial_S`()
BEGIN
   SELECT 
      obrasocial.Id
      ,obrasocial.Nombre
      ,obrasocial.FechaInicio
      ,obrasocial.FechaFin
   FROM obrasocial
   ORDER BY obrasocial.Nombre;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.ObraSocial_U
DELIMITER //
CREATE PROCEDURE `ObraSocial_U`(
	IN `@Id` INT,
	IN `@Nombre` VARCHAR(200)
)
BEGIN
   UPDATE obrasocial 
      SET 
         obrasocial.Nombre = `@Nombre`
   WHERE obrasocial.Id = `@Id`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.TipoDocumentoDeshabilitado_U
DELIMITER //
CREATE PROCEDURE `TipoDocumentoDeshabilitado_U`(
	IN `@Id` INT
)
BEGIN
   UPDATE tipodocumento
      SET tipodocumento.FechaFin = NOW()
   WHERE tipodocumento.Id = `@Id`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.TipoDocumentoHabilitado_U
DELIMITER //
CREATE PROCEDURE `TipoDocumentoHabilitado_U`(
	IN `@Id` INT
)
BEGIN
   UPDATE tipodocumento
      SET tipodocumento.FechaFin = '9999-12-31 23:59:59'
   WHERE tipodocumento.Id = `@Id`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.TipoDocumentoXId_S
DELIMITER //
CREATE PROCEDURE `TipoDocumentoXId_S`(
	IN `@Id` INT
)
BEGIN
   select
      tipodocumento.Id
      ,tipodocumento.Nombre
      ,tipodocumento.FechaInicio
      ,tipodocumento.FechaFin
   FROM tipodocumento
   WHERE tipodocumento.Id = `@Id`
   ORDER BY tipodocumento.Nombre ASC;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.TipoDocumentoXNombre_S
DELIMITER //
CREATE PROCEDURE `TipoDocumentoXNombre_S`(
	IN `@Nombre` VARCHAR(100)
)
BEGIN
   SELECT 
      tipodocumento.Nombre
      ,tipodocumento.FechaInicio
      ,tipodocumento.FechaFin
   FROM tipodocumento
   WHERE tipodocumento.Nombre = `@Nombre`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.TipoDocumento_I
DELIMITER //
CREATE PROCEDURE `TipoDocumento_I`(
	IN `@Nombre` VARCHAR(100)
)
BEGIN
   INSERT INTO tipodocumento(
      tipodocumento.Nombre
      ,tipodocumento.FechaInicio
      ,tipodocumento.FechaFin)
   VALUE(
      `@Nombre`
      ,NOW()
      ,'9999-12-31 23:59:59');
   
   SELECT LAST_INSERT_ID() Id;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.TipoDocumento_S
DELIMITER //
CREATE PROCEDURE `TipoDocumento_S`()
BEGIN
   SELECT 
      tipodocumento.Id
      ,tipodocumento.Nombre
      ,tipodocumento.FechaInicio
      ,tipodocumento.FechaFin
   FROM tipodocumento
   ORDER BY tipodocumento.Nombre ASC;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.TipoDocumento_U
DELIMITER //
CREATE PROCEDURE `TipoDocumento_U`(
	IN `@Id` INT,
	IN `@Nombre` VARCHAR(100)
)
BEGIN
   UPDATE tipodocumento
      SET
         tipodocumento.Nombre = `@Nombre`
   WHERE tipodocumento.Id = `@Id`; 
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.UsuarioConfirmar_U
DELIMITER //
CREATE PROCEDURE `UsuarioConfirmar_U`(
	IN `@Id` INT
)
BEGIN
   UPDATE usuario
      SET usuario.Confirmacion = 1
   WHERE usuario.Id = `@Id`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.UsuarioModificarPassword_U
DELIMITER //
CREATE PROCEDURE `UsuarioModificarPassword_U`(
	IN `@IdUsuario` INT,
	IN `@Password` TEXT
)
BEGIN
   UPDATE usuario
      SET usuario.Password = `@Password`
   WHERE usuario.Id = `@IdUsuario`; 
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.UsuarioResetPassword_U
DELIMITER //
CREATE PROCEDURE `UsuarioResetPassword_U`(
	IN `@NombreUsuario` TEXT,
	IN `@Password` TEXT
)
BEGIN
   UPDATE usuario
   set
      usuario.Password = `@Password`
   WHERE usuario.NombreUsuario = `@NombreUsuario`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.UsuarioXNombreUsuarioXPassword_S
DELIMITER //
CREATE PROCEDURE `UsuarioXNombreUsuarioXPassword_S`(
	IN `@NombreUsuario` TEXT,
	IN `@Password` TEXT
)
BEGIN
   SELECT 
      usuario.Id
      ,usuario.NombreUsuario
      ,usuario.Email
      ,usuario.FechaCreacion
      ,usuario.Confirmacion
   FROM usuario
   WHERE usuario.NombreUsuario = `@NombreUsuario`
      AND usuario.Password =  `@Password`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.UsuarioXNombreUsuario_S
DELIMITER //
CREATE PROCEDURE `UsuarioXNombreUsuario_S`(
	IN `@NombreUsuario` TEXT
)
BEGIN
   SELECT   
      usuario.Id
      ,usuario.NombreUsuario
      ,usuario.Email
      ,usuario.Password
      ,usuario.FechaCreacion
      ,usuario.Confirmacion
   FROM usuario
   WHERE usuario.NombreUsuario = `@NombreUsuario`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.Usuario_I
DELIMITER //
CREATE PROCEDURE `Usuario_I`(
	IN `@NombreUsuario` TEXT,
	IN `@Email` TEXT,
	IN `@Password` TEXT
)
BEGIN
   INSERT INTO usuario(
      usuario.NombreUsuario
      ,usuario.Email
      ,usuario.password
      ,usuario.FechaCreacion)
   VALUE(
      `@NombreUsuario`
      ,`@Email`
      ,`@Password`
      ,NOW());
   
   SELECT  LAST_INSERT_ID() Id;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
