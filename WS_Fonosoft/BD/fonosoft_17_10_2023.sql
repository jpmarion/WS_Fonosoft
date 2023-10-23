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

-- Volcando estructura para tabla fonosoft.entidad
CREATE TABLE IF NOT EXISTS `entidad` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Apellido` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla fonosoft.entidad_obrasocial
CREATE TABLE IF NOT EXISTS `entidad_obrasocial` (
  `IdEntidad` int DEFAULT NULL,
  `IdObraSocial` int DEFAULT NULL,
  `NroObraSocial` varchar(300) COLLATE utf8mb4_general_ci DEFAULT NULL,
  KEY `IdEntidad` (`IdEntidad`,`IdObraSocial`),
  KEY `FK_entidad_obrasocial_obrasocial` (`IdObraSocial`),
  CONSTRAINT `FK_entidad_obrasocial_entidad` FOREIGN KEY (`IdEntidad`) REFERENCES `entidad` (`Id`),
  CONSTRAINT `FK_entidad_obrasocial_obrasocial` FOREIGN KEY (`IdObraSocial`) REFERENCES `obrasocial` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla fonosoft.entidad_tipocontacto
CREATE TABLE IF NOT EXISTS `entidad_tipocontacto` (
  `IdEntidad` int DEFAULT NULL,
  `IdTipoContacto` int DEFAULT NULL,
  `Contacto` varchar(300) COLLATE utf8mb4_general_ci DEFAULT NULL,
  KEY `IdEntidad` (`IdEntidad`,`IdTipoContacto`),
  KEY `FK_entidad_tipocontacto_tipocontacto` (`IdTipoContacto`),
  CONSTRAINT `FK_entidad_tipocontacto_entidad` FOREIGN KEY (`IdEntidad`) REFERENCES `entidad` (`Id`),
  CONSTRAINT `FK_entidad_tipocontacto_tipocontacto` FOREIGN KEY (`IdTipoContacto`) REFERENCES `tipocontacto` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla fonosoft.entidad_tipodocumento
CREATE TABLE IF NOT EXISTS `entidad_tipodocumento` (
  `IdEntidad` int DEFAULT NULL,
  `IdTipoDocumento` int DEFAULT NULL,
  `NroDocumento` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  KEY `IdEntidad` (`IdEntidad`,`IdTipoDocumento`),
  KEY `FK_entidad_tipodocumento_tipodocumento` (`IdTipoDocumento`),
  CONSTRAINT `FK_entidad_tipodocumento_entidad` FOREIGN KEY (`IdEntidad`) REFERENCES `entidad` (`Id`),
  CONSTRAINT `FK_entidad_tipodocumento_tipodocumento` FOREIGN KEY (`IdTipoDocumento`) REFERENCES `tipodocumento` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla fonosoft.entidad_tipoentidad
CREATE TABLE IF NOT EXISTS `entidad_tipoentidad` (
  `IdEntidad` int DEFAULT NULL,
  `IdTipoEntidad` int DEFAULT NULL,
  KEY `IdEntidad` (`IdEntidad`,`IdTipoEntidad`),
  KEY `FK_entidad_tipoentidad_tipoentidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_entidad_tipoentidad_entidad` FOREIGN KEY (`IdEntidad`) REFERENCES `entidad` (`Id`),
  CONSTRAINT `FK_entidad_tipoentidad_tipoentidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidad` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla fonosoft.obrasocial
CREATE TABLE IF NOT EXISTS `obrasocial` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(200) COLLATE utf8mb4_general_ci NOT NULL DEFAULT '0',
  `FechaInicio` datetime DEFAULT NULL,
  `FechaFin` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla fonosoft.tipocontacto
CREATE TABLE IF NOT EXISTS `tipocontacto` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Nombre` (`Nombre`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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

-- Volcando estructura para tabla fonosoft.tipoentidad
CREATE TABLE IF NOT EXISTS `tipoentidad` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
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

-- Volcando estructura para procedimiento fonosoft.EntidadDocumentoXIdEntidad_S
DELIMITER //
CREATE PROCEDURE `EntidadDocumentoXIdEntidad_S`(
	IN `@IdEntidad` INT
)
BEGIN
   SELECT 
      entidad_tipodocumento.IdEntidad
      ,entidad_tipodocumento.IdTipoDocumento
      ,tipodocumento.Nombre TipoDocumento
      ,entidad_tipodocumento.NroDocumento
   FROM entidad_tipodocumento
   INNER JOIN tipodocumento
      ON entidad_tipodocumento.IdTipoDocumento = tipodocumento.Id
   WHERE entidad_tipodocumento.IdEntidad = `@IdEntidad`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadObraSocialXIdEntidad_S
DELIMITER //
CREATE PROCEDURE `EntidadObraSocialXIdEntidad_S`(
	IN `@IdEntidad` INT
)
BEGIN
   SELECT 
      entidad_obrasocial.IdEntidad
      ,entidad_obrasocial.IdObraSocial
      ,obrasocial.Nombre ObraSocial
      ,entidad_obrasocial.NroObraSocial
   FROM entidad_obrasocial
   INNER JOIN obrasocial
      ON entidad_obrasocial.IdObraSocial = obrasocial.Id
   WHERE entidad_obrasocial.IdEntidad = `@IdEntidad`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadObraSocial_I
DELIMITER //
CREATE PROCEDURE `EntidadObraSocial_I`(
	IN `@IdEntidad` INT,
	IN `@IdObraSocial` INT,
	IN `@NroObraSocial` VARCHAR(300)
)
BEGIN
   INSERT INTO entidad_obrasocial(
      entidad_obrasocial.IdEntidad
      ,entidad_obrasocial.IdObraSocial
      ,entidad_obrasocial.NroObraSocial)
   VALUE(
      `@IdEntidad`
      ,`@IdObraSocial`
      ,`@NroObraSocial`);
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadTipoContactoXIdEntidad_S
DELIMITER //
CREATE PROCEDURE `EntidadTipoContactoXIdEntidad_S`(
	IN `@IdEntidad` INT
)
BEGIN
   SELECT 
      entidad_tipocontacto.IdEntidad
      ,entidad_tipocontacto.IdTipoContacto
      ,tipocontacto.Nombre TipoContacto
      ,entidad_tipocontacto.Contacto
   FROM entidad_tipocontacto
   INNER JOIN tipocontacto
      ON entidad_tipocontacto.IdTipoContacto = tipocontacto.Id
   WHERE entidad_tipocontacto.IdEntidad = `@IdEntidad`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadTipoContacto_D
DELIMITER //
CREATE PROCEDURE `EntidadTipoContacto_D`(
	IN `@IdEntidad` INT
)
BEGIN
   DELETE FROM entidad_tipocontacto
   WHERE entidad_tipocontacto.IdEntidad = `@IdEntidad`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadTipoContacto_I
DELIMITER //
CREATE PROCEDURE `EntidadTipoContacto_I`(
	IN `@IdEntidad` INT,
	IN `@IdTipoContacto` INT,
	IN `@Contacto` VARCHAR(300)
)
BEGIN
   INSERT INTO entidad_tipocontacto(
      entidad_tipocontacto.IdEntidad
      ,entidad_tipocontacto.IdTipoContacto
      ,entidad_tipocontacto.Contacto)
   VALUE(
      `@IdEntidad`
      ,`@IdTipoContacto`
      ,`@Contacto`);   
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadTipoContacto_U
DELIMITER //
CREATE PROCEDURE `EntidadTipoContacto_U`(
	IN `@IdEntidad` INT,
	IN `@IdTipoContacto` INT,
	IN `@Contacto` VARCHAR(300)
)
BEGIN
   UPDATE entidad_tipocontacto
      SET entidad_tipocontacto.Contacto = `@Contacto`
   WHERE entidad_tipocontacto.IdEntidad = `@IdEntidad`
   AND entidad_tipocontacto.IdTipoContacto = `@IdTipoContacto`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadTipoDocumento_I
DELIMITER //
CREATE PROCEDURE `EntidadTipoDocumento_I`(
	IN `@IdEntidad` INT,
	IN `@IdTipoDocumento` INT,
	IN `@NroDocumento` VARCHAR(100)
)
BEGIN
   INSERT INTO entidad_tipodocumento(
      entidad_tipodocumento.IdEntidad
      ,entidad_tipodocumento.IdTipoDocumento
      ,entidad_tipodocumento.NroDocumento)
   VALUE(
      `@IdEntidad`
      ,`@IdTipoDocumento`
      ,`@NroDocumento`);
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadTipoEntidadxIdentidad_S
DELIMITER //
CREATE PROCEDURE `EntidadTipoEntidadxIdentidad_S`(
	IN `@IdEntidad` INT
)
BEGIN
   SELECT 
      entidad_tipoentidad.IdEntidad
      ,entidad_tipoentidad.IdTipoEntidad
      ,tipoentidad.Nombre TipoEntidad
   FROM entidad_tipoentidad
   INNER JOIN tipoentidad
      ON entidad_tipoentidad.IdTipoEntidad = tipoentidad.Id
   WHERE entidad_tipoentidad.IdEntidad = `@IdEntidad`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadTipoEntidad_I
DELIMITER //
CREATE PROCEDURE `EntidadTipoEntidad_I`(
	IN `@IdEntidad` INT,
	IN `@IdTipoEntidad` INT
)
BEGIN
   INSERT INTO entidad_tipoentidad(
      entidad_tipoentidad.IdEntidad
      ,entidad_tipoentidad.IdTipoEntidad)
   VALUE(
      `@IdEntidad`
      ,`@IdTipoEntidad`);
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.EntidadXId_S
DELIMITER //
CREATE PROCEDURE `EntidadXId_S`(
	IN `@IdEntidad` INT,
	IN `@IdTipoEntidad` INT
)
BEGIN
   SELECT 
      entidad.Id
      ,entidad.Apellido
      ,entidad.Nombre
   FROM entidad
   INNER JOIN entidad_tipoentidad
      ON entidad.Id = entidad_tipoentidad.IdEntidad
   WHERE entidad.Id = `@IdEntidad`
   AND entidad_tipoentidad.IdTipoEntidad = `@IdTipoEntidad`;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.Entidad_I
DELIMITER //
CREATE PROCEDURE `Entidad_I`(
	IN `@Apellido` VARCHAR(100),
	IN `@Nombre` VARCHAR(100)
)
BEGIN
   INSERT INTO entidad(
      entidad.Apellido
      ,entidad.Nombre)
   VALUE(
      `@Apellido`
      ,`@Nombre`);
      
   SELECT  LAST_INSERT_ID() Id;
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.Entidad_S
DELIMITER //
CREATE PROCEDURE `Entidad_S`(
	IN `@IdTipoEntidad` INT
)
BEGIN
   SELECT 
      entidad.Id
      ,entidad.Apellido
      ,entidad.Nombre
      ,tipodocumento.Nombre TipoDocumento
      ,entidad_tipodocumento.NroDocumento
      FROM entidad
      INNER JOIN  entidad_tipoentidad
         ON entidad.Id = entidad_tipoentidad.IdEntidad
      LEFT JOIN entidad_tipodocumento
         ON entidad.Id = entidad_tipodocumento.IdEntidad
      LEFT JOIN tipodocumento
         ON tipodocumento.Id =  entidad_tipodocumento.IdTipoDocumento 
      WHERE entidad_tipoentidad.IdTipoEntidad = `@IdTipoEntidad`
      ORDER BY 
         entidad.Apellido
         ,entidad.Nombre;      
END//
DELIMITER ;

-- Volcando estructura para procedimiento fonosoft.Entidad_U
DELIMITER //
CREATE PROCEDURE `Entidad_U`(
	IN `@IdEntidad` INT,
	IN `@Apellido` VARCHAR(100),
	IN `@Nombre` VARCHAR(100)
)
BEGIN
   UPDATE entidad
   SET   
      entidad.Apellido = `@Apellido`
      ,entidad.Nombre = `@Nombre`
   WHERE entidad.Id = `@IdEntidad`;
END//
DELIMITER ;

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
