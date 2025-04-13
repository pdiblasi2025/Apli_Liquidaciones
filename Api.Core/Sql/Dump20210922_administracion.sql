-- MySQL dump 10.13  Distrib 5.7.29, for Win64 (x86_64)
--
-- Host: administracion.cszy8imk8two.us-east-1.rds.amazonaws.com    Database: Administracion
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.13-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `CajaEstado`
--

DROP TABLE IF EXISTS `CajaEstado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `CajaEstado` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `FechaInicio` datetime(6) NOT NULL,
  `FechaCierre` datetime(6) DEFAULT NULL,
  `SaldoInicial` decimal(18,2) NOT NULL DEFAULT 0.00,
  `SaldoCierre` decimal(18,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `CajaMovimientos`
--

DROP TABLE IF EXISTS `CajaMovimientos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `CajaMovimientos` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `CajaEstadoId` int(11) NOT NULL,
  `Ingreso` decimal(18,2) NOT NULL,
  `Egreso` decimal(18,2) NOT NULL DEFAULT 0.00,
  `BalanceEstado` decimal(18,2) NOT NULL DEFAULT 0.00,
  `BalanceEstadoOrigen` decimal(18,2) NOT NULL DEFAULT 0.00,
  `BalanceTotal` decimal(18,2) NOT NULL DEFAULT 0.00,
  `BalanceOrigenTotal` decimal(18,2) NOT NULL DEFAULT 0.00,
  `OrigenId` int(11) DEFAULT NULL,
  `EstadoMovimientoId` int(11) NOT NULL,
  `OrdenFacturacionId` int(11) DEFAULT NULL,
  `OrdenPagoId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_CajaMovimientos_CajaEstado_idx` (`CajaEstadoId`),
  KEY `FK_CajaMovimientos_OrigenId_idx` (`OrigenId`),
  CONSTRAINT `FK_CajaMovimientos_CajaEstado` FOREIGN KEY (`CajaEstadoId`) REFERENCES `CajaEstado` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_CajaMovimientos_OrigenId` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Categoria`
--

DROP TABLE IF EXISTS `Categoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Categoria` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Nombre` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Cliente`
--

DROP TABLE IF EXISTS `Cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Cliente` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Cuit` longtext DEFAULT NULL,
  `Cuil` longtext DEFAULT NULL,
  `Dni` longtext DEFAULT NULL,
  `Nombres` longtext DEFAULT NULL,
  `Apellidos` longtext DEFAULT NULL,
  `Direccion` longtext DEFAULT NULL,
  `Email` longtext DEFAULT NULL,
  `Celular` longtext DEFAULT NULL,
  `Contacto` longtext DEFAULT NULL,
  `DiasPago` int(11) DEFAULT NULL,
  `PipeDriveId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Comercial`
--

DROP TABLE IF EXISTS `Comercial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Comercial` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Cuit` longtext DEFAULT NULL,
  `Cuil` longtext DEFAULT NULL,
  `Dni` longtext DEFAULT NULL,
  `Nombre` longtext DEFAULT NULL,
  `Direccion` longtext DEFAULT NULL,
  `Email` longtext DEFAULT NULL,
  `Celular` longtext DEFAULT NULL,
  `PipeDriveId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Compania`
--

DROP TABLE IF EXISTS `Compania`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Compania` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Nombre` longtext DEFAULT NULL,
  `Descripcion` longtext DEFAULT NULL,
  `NombreUsuario` longtext DEFAULT NULL,
  `ApellidoUsuario` longtext DEFAULT NULL,
  `AuthId` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Concepto`
--

DROP TABLE IF EXISTS `Concepto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Concepto` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Nombre` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ContactoCliente`
--

DROP TABLE IF EXISTS `ContactoCliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ContactoCliente` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Nombre` longtext DEFAULT NULL,
  `Apellido` varchar(250) DEFAULT NULL,
  `Email` longtext DEFAULT NULL,
  `Celular` longtext DEFAULT NULL,
  `ClienteId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_ContactoCliente_Cliente_idx` (`ClienteId`),
  CONSTRAINT `FK_ContactoCliente_Cliente` FOREIGN KEY (`ClienteId`) REFERENCES `Cliente` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ContactoProveedor`
--

DROP TABLE IF EXISTS `ContactoProveedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ContactoProveedor` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Nombre` longtext DEFAULT NULL,
  `Apellido` varchar(250) DEFAULT NULL,
  `Email` longtext DEFAULT NULL,
  `Celular` longtext DEFAULT NULL,
  `ProveedorId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_ContactoProveedor_Proveedor_idx` (`ProveedorId`),
  CONSTRAINT `FK_ContactoProveedor_Proveedor` FOREIGN KEY (`ProveedorId`) REFERENCES `Proveedor` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `CotizacionMoneda`
--

DROP TABLE IF EXISTS `CotizacionMoneda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `CotizacionMoneda` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `MonedaId` int(11) NOT NULL,
  `Valor` decimal(18,2) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `CotizacionMoneda_Moneda` (`MonedaId`),
  CONSTRAINT `CotizacionMoneda_Moneda` FOREIGN KEY (`MonedaId`) REFERENCES `Moneda` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `CuentaBancaria`
--

DROP TABLE IF EXISTS `CuentaBancaria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `CuentaBancaria` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `MonedaId` int(11) NOT NULL,
  `TipoDeCuentaId` int(11) DEFAULT NULL,
  `Banco` varchar(250) DEFAULT NULL,
  `NroDeCuenta` varchar(250) DEFAULT NULL,
  `Fecha` datetime(6) DEFAULT NULL,
  `SaldoInicial` decimal(18,2) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_CuentaBancaria_Moneda_idx` (`MonedaId`),
  CONSTRAINT `FK_CuentaBancaria_Moneda` FOREIGN KEY (`MonedaId`) REFERENCES `Moneda` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Estado`
--

DROP TABLE IF EXISTS `Estado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Estado` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Descripcion` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `EstadoHito`
--

DROP TABLE IF EXISTS `EstadoHito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `EstadoHito` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Descripcion` varchar(250) DEFAULT NULL,
  `Tipo` int(11) DEFAULT 1,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `EstadoMovimientoCaja`
--

DROP TABLE IF EXISTS `EstadoMovimientoCaja`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `EstadoMovimientoCaja` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Descripcion` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Gasto`
--

DROP TABLE IF EXISTS `Gasto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Gasto` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Descripcion` varchar(250) DEFAULT NULL,
  `ProyectoId` int(11) DEFAULT NULL,
  `EstadoId` int(11) NOT NULL,
  `FechaFactura` datetime(6) NOT NULL,
  `ImporteSinIva` decimal(18,2) NOT NULL,
  `ProveedorId` int(11) DEFAULT NULL,
  `OrigenIngresoId` int(11) NOT NULL,
  `ConceptoId` int(11) DEFAULT NULL,
  `FechaPago` datetime(6) DEFAULT NULL,
  `FechaRealPago` datetime(6) DEFAULT NULL,
  `NumeroFactura` varchar(45) DEFAULT NULL,
  `TipoComprobanteId` int(11) DEFAULT NULL,
  `CotizacionMonedaId` int(11) DEFAULT NULL,
  `ImporteEnPesos` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Gasto_Proyecto_idx` (`ProyectoId`),
  KEY `FK_Gasto_Concepto_idx` (`ConceptoId`),
  KEY `FK_Gasto_Proveedor_idx` (`ProveedorId`),
  KEY `FK_Gasto_Origen_idx` (`OrigenIngresoId`),
  KEY `FK_Gasto_CotizacionMoneda_idx` (`CotizacionMonedaId`),
  CONSTRAINT `FK_Gasto_Concepto` FOREIGN KEY (`ConceptoId`) REFERENCES `Concepto` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Gasto_Origen` FOREIGN KEY (`OrigenIngresoId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Gasto_Proveedor` FOREIGN KEY (`ProveedorId`) REFERENCES `Proveedor` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Gasto_Proyecto` FOREIGN KEY (`ProyectoId`) REFERENCES `Proyecto` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `HistorialProyecto`
--

DROP TABLE IF EXISTS `HistorialProyecto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `HistorialProyecto` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `Codigo` longtext DEFAULT NULL,
  `Nombre` varchar(250) DEFAULT NULL,
  `Descripcion` varchar(500) DEFAULT NULL,
  `ClienteId` int(11) NOT NULL,
  `UsuarioId` varchar(42) NOT NULL,
  `FechaDeAprobacion` datetime(6) DEFAULT NULL,
  `DuracionEnMeses` int(11) NOT NULL,
  `ValorTotal` decimal(18,2) NOT NULL,
  `FormaFacturacionId` int(11) NOT NULL,
  `DealPipeDrive` int(11) NOT NULL,
  `Observaciones` longtext DEFAULT NULL,
  `EstadoId` int(11) NOT NULL,
  `OrigenId` int(11) DEFAULT NULL,
  `ProyectoId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_HistorialProyecto_Origen_idx` (`OrigenId`),
  KEY `FK_HistorialProyecto_Cliente_idx` (`ClienteId`),
  KEY `FK_HistorialProyecto_Estado_idx` (`EstadoId`),
  CONSTRAINT `FK_HistorialProyecto_Cliente` FOREIGN KEY (`ClienteId`) REFERENCES `Cliente` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_HistorialProyecto_Estado` FOREIGN KEY (`EstadoId`) REFERENCES `Estado` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_HistorialProyecto_Origen` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Hito`
--

DROP TABLE IF EXISTS `Hito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Hito` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `ProyectoId` int(11) NOT NULL,
  `ClienteId` int(11) DEFAULT NULL,
  `Descripcion` varchar(45) NOT NULL,
  `ProveedorId` int(11) DEFAULT NULL,
  `FechaFinalizacion` datetime(6) DEFAULT NULL,
  `ImporteSinIva` decimal(18,2) NOT NULL,
  `EstadoHitoId` int(11) NOT NULL,
  `Observaciones` longtext DEFAULT NULL,
  `TipoHitoId` int(11) DEFAULT NULL,
  `OrigenId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Hito_Proyecto_idx` (`ProyectoId`),
  KEY `FK_Hito_Origen_idx` (`OrigenId`),
  CONSTRAINT `FK_Hito_Origen` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Hito_Proyecto` FOREIGN KEY (`ProyectoId`) REFERENCES `Proyecto` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Licencia`
--

DROP TABLE IF EXISTS `Licencia`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Licencia` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Codigo` longtext DEFAULT NULL,
  `Expiracion` datetime(6) NOT NULL,
  `Estado` int(11) NOT NULL,
  `CompaniaId` int(11) NOT NULL,
  `PlanId` int(11) NOT NULL,
  `Descripcion` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Licencia_CompaniaId` (`CompaniaId`),
  KEY `IX_Licencia_PlanId` (`PlanId`),
  CONSTRAINT `FK_Licencia_Compania_CompaniaId` FOREIGN KEY (`CompaniaId`) REFERENCES `Compania` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Licencia_Plan_PlanId` FOREIGN KEY (`PlanId`) REFERENCES `Plan` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Moneda`
--

DROP TABLE IF EXISTS `Moneda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Moneda` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Nombre` longtext DEFAULT NULL,
  `Decimales` float NOT NULL,
  `Valor` float NOT NULL,
  `Abreviatura` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `OrdenFacturacion`
--

DROP TABLE IF EXISTS `OrdenFacturacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `OrdenFacturacion` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `FechaFactura` datetime(6) DEFAULT NULL,
  `FechaVencimientoPago` datetime(6) NOT NULL,
  `FechaRealPago` datetime(6) DEFAULT NULL,
  `Descripcion` varchar(160) NOT NULL,
  `ClienteId` int(11) NOT NULL,
  `NumeroFactura` varchar(150) DEFAULT NULL,
  `EstadoId` int(11) NOT NULL DEFAULT 1,
  `TipoComprobanteId` int(11) DEFAULT NULL,
  `CotizacionMonedaId` int(11) DEFAULT NULL,
  `OrigenId` int(11) NOT NULL,
  `Valor` decimal(18,2) NOT NULL DEFAULT 0.00,
  `ValorPesos` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_OrdenFacturacion_Cliente_idx` (`ClienteId`),
  KEY `FK_OrdenFacturacion_Origen_idx` (`OrigenId`),
  CONSTRAINT `FK_OrdenFacturacion_Cliente` FOREIGN KEY (`ClienteId`) REFERENCES `Cliente` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrdenFacturacion_Origen` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `OrdenFacturacionHito`
--

DROP TABLE IF EXISTS `OrdenFacturacionHito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `OrdenFacturacionHito` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `HitoId` int(11) NOT NULL,
  `OrdenFacturacionId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `OrdenFacturacionHito_Hito` (`HitoId`),
  KEY `OrdenFacturacionHito_OrdenFacturacion` (`OrdenFacturacionId`),
  CONSTRAINT `OrdenFacturacionHito_Hito` FOREIGN KEY (`HitoId`) REFERENCES `Hito` (`Id`),
  CONSTRAINT `OrdenFacturacionHito_OrdenFacturacion` FOREIGN KEY (`OrdenFacturacionId`) REFERENCES `OrdenFacturacion` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `OrdenPago`
--

DROP TABLE IF EXISTS `OrdenPago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `OrdenPago` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Descripcion` varchar(160) NOT NULL,
  `ProveedorId` int(11) NOT NULL,
  `NumeroFactura` varchar(150) DEFAULT NULL,
  `FechaFactura` datetime(6) DEFAULT NULL,
  `FechaVencimientoPago` datetime(6) NOT NULL,
  `FechaRealPago` datetime(6) DEFAULT NULL,
  `EstadoId` int(11) NOT NULL DEFAULT 1,
  `OrigenId` int(11) DEFAULT NULL,
  `TipoComprobanteId` int(11) DEFAULT NULL,
  `CotizacionMonedaId` int(11) DEFAULT NULL,
  `Valor` decimal(18,2) NOT NULL DEFAULT 0.00,
  `ValorPesos` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_OrdenPago_Proveedor_idx` (`ProveedorId`),
  KEY `FK_OrdenPago_Origen_idx` (`OrigenId`),
  CONSTRAINT `FK_OrdenPago_Origen` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrdenPago_Proveedor` FOREIGN KEY (`ProveedorId`) REFERENCES `Proveedor` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `OrdenPagoHistorial`
--

DROP TABLE IF EXISTS `OrdenPagoHistorial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `OrdenPagoHistorial` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Descripcion` varchar(160) NOT NULL,
  `OrdenPagoId` int(11) NOT NULL,
  `ProveedorId` int(11) NOT NULL,
  `Valor` decimal(18,2) NOT NULL,
  `NumeroFactura` varchar(150) DEFAULT NULL,
  `FechaFactura` datetime(6) DEFAULT NULL,
  `FechaVencimientoPago` datetime(6) NOT NULL,
  `EstadoId` int(11) NOT NULL DEFAULT 1,
  `OrigenId` int(11) DEFAULT NULL,
  `TipoComprobanteId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_OrdenPagoHistorial_Proveedor_idx` (`ProveedorId`),
  KEY `FK_OrdenPagoHistorial_Origen_idx` (`OrigenId`),
  KEY `FK_OrdenPagoHistorial_OrigenPago_idx` (`OrdenPagoId`),
  CONSTRAINT `FK_OrdenPagoHistorial_OrdenPago` FOREIGN KEY (`OrdenPagoId`) REFERENCES `OrdenPago` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrdenPagoHistorial_Origen` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrdenPagoHistorial_Proveedor` FOREIGN KEY (`ProveedorId`) REFERENCES `Proveedor` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `OrdenPagoHito`
--

DROP TABLE IF EXISTS `OrdenPagoHito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `OrdenPagoHito` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `HitoId` int(11) NOT NULL,
  `OrdenPagoId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `OrdenPagoHito_Hito` (`HitoId`),
  KEY `OrdenPagoHito_OrdenPago` (`OrdenPagoId`),
  CONSTRAINT `OrdenPagoHito_Hito` FOREIGN KEY (`HitoId`) REFERENCES `Hito` (`Id`),
  CONSTRAINT `OrdenPagoHito_OrdenPago` FOREIGN KEY (`OrdenPagoId`) REFERENCES `OrdenPago` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Origen`
--

DROP TABLE IF EXISTS `Origen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Origen` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Nombre` longtext DEFAULT NULL,
  `MonedaId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Origen_Moneda_idx` (`MonedaId`),
  CONSTRAINT `FK_Origen_Moneda` FOREIGN KEY (`MonedaId`) REFERENCES `Moneda` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Plan`
--

DROP TABLE IF EXISTS `Plan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Plan` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Codigo` longtext DEFAULT NULL,
  `Nombre` longtext DEFAULT NULL,
  `Descripcion` longtext DEFAULT NULL,
  `CantidadUsuarios` int(11) DEFAULT NULL,
  `CantidadEmprendimientos` int(11) NOT NULL,
  `MesesValidez` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Proveedor`
--

DROP TABLE IF EXISTS `Proveedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Proveedor` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Cuit` longtext DEFAULT NULL,
  `Nombre` longtext DEFAULT NULL,
  `Direccion` longtext DEFAULT NULL,
  `Email` longtext DEFAULT NULL,
  `Celular` longtext DEFAULT NULL,
  `Contacto` longtext DEFAULT NULL,
  `DiasPago` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Proyeccion`
--

DROP TABLE IF EXISTS `Proyeccion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Proyeccion` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `CajaEstadoId` int(11) DEFAULT NULL,
  `Ingreso` decimal(18,2) NOT NULL,
  `Egreso` decimal(18,2) NOT NULL DEFAULT 0.00,
  `OrigenId` int(11) DEFAULT NULL,
  `HitoId` int(11) DEFAULT NULL,
  `GastoId` int(11) DEFAULT NULL,
  `ProyeccionFecha` datetime(6) NOT NULL,
  `Finalizado` tinyint(1) DEFAULT 0,
  `FinalizadoFecha` datetime(6) DEFAULT NULL,
  `FinalizadoBy` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Proyeccion_CajaEstado_idx` (`CajaEstadoId`),
  KEY `FK_Proyeccion_OrigenId_idx` (`OrigenId`),
  KEY `FK_Proyeccion_HitoId_idx` (`HitoId`),
  KEY `FK_Proyeccion_Gasto_idx` (`GastoId`),
  CONSTRAINT `FK_Proyeccion_CajaEstado` FOREIGN KEY (`CajaEstadoId`) REFERENCES `CajaEstado` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Proyeccion_Gasto` FOREIGN KEY (`GastoId`) REFERENCES `Gasto` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Proyeccion_Hito` FOREIGN KEY (`HitoId`) REFERENCES `Hito` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Proyeccion_OrigenId` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Proyecto`
--

DROP TABLE IF EXISTS `Proyecto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Proyecto` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `Codigo` longtext DEFAULT NULL,
  `Nombre` varchar(250) DEFAULT NULL,
  `Descripcion` varchar(500) DEFAULT NULL,
  `ClienteId` int(11) NOT NULL,
  `UsuarioId` varchar(42) NOT NULL,
  `FechaDeAprobacion` datetime(6) DEFAULT NULL,
  `DuracionEnMeses` int(11) NOT NULL,
  `ValorTotal` decimal(18,2) NOT NULL,
  `FormaFacturacionId` int(11) NOT NULL,
  `DealPipeDrive` int(11) DEFAULT NULL,
  `Observaciones` longtext DEFAULT NULL,
  `EstadoId` int(11) NOT NULL,
  `OrigenId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Proyectos_Clientes_idx` (`ClienteId`),
  KEY `FK_Proyecto_Origen_idx` (`OrigenId`),
  CONSTRAINT `FK_Proyecto_Cliente` FOREIGN KEY (`ClienteId`) REFERENCES `Cliente` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Proyecto_Origen` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`admin`@`%`*/ /*!50003 TRIGGER historial_proyecto
AFTER UPDATE
ON Proyecto FOR EACH ROW
BEGIN                    
    INSERT INTO HistorialProyecto
    (CreateDate, UpdateDate, UpdatedBy, Codigo, Nombre, Descripcion, ClienteId, UsuarioId, FechaDeAprobacion, DuracionEnMeses, ValorTotal, FormaFacturacionId, DealPipeDrive, Observaciones, EstadoId, OrigenId, ProyectoId)
    SELECT OLD.CreateDate, OLD.UpdateDate, OLD.UpdatedBy, OLD.Codigo, OLD.Nombre, OLD.Descripcion, OLD.ClienteId, OLD.UsuarioId, OLD.FechaDeAprobacion, OLD.DuracionEnMeses, OLD.ValorTotal, OLD.FormaFacturacionId, OLD.DealPipeDrive, OLD.Observaciones, OLD.EstadoId, OLD.OrigenId, NEW.Id
    FROM Proyecto WHERE Proyecto.Id = NEW.Id;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `SaldoInicial`
--

DROP TABLE IF EXISTS `SaldoInicial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `SaldoInicial` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime(6) NOT NULL,
  `UpdateDate` datetime(6) DEFAULT NULL,
  `DeleteDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext DEFAULT NULL,
  `UpdatedBy` longtext DEFAULT NULL,
  `DeleteBy` longtext DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Deleted` tinyint(1) NOT NULL,
  `CajaEstadoId` int(11) NOT NULL,
  `Ingreso` decimal(18,2) NOT NULL,
  `OrigenId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_SaldoInicial_CajaEstado_idx` (`CajaEstadoId`),
  KEY `FK_SaldoInicial_OrigenId_idx` (`OrigenId`),
  CONSTRAINT `FK_SaldoInicial_CajaEstado` FOREIGN KEY (`CajaEstadoId`) REFERENCES `CajaEstado` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_SaldoInicial_OrigenId` FOREIGN KEY (`OrigenId`) REFERENCES `Origen` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping events for database 'Administracion'
--

--
-- Dumping routines for database 'Administracion'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-09-22 15:31:48
