SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema liber
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema liber
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `liber` DEFAULT CHARACTER SET utf8 ;
USE `liber` ;

-- -----------------------------------------------------
-- Table `liber`.`Usuarios`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `liber`.`Usuarios` (
  `idUsuario` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `apellido` VARCHAR(45) NOT NULL,
  `usuario` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NULL,
  `password` VARCHAR(45) NOT NULL,
  `admin` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idUsuario`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `liber`.`Autores`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `liber`.`Autores` (
  `idAutor` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  PRIMARY KEY (`idAutor`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `liber`.`Generos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `liber`.`Generos` (
  `idGenero` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idGenero`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `liber`.`Libros`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `liber`.`Libros` (
  `idLibro` INT NOT NULL AUTO_INCREMENT,
  `titulo` VARCHAR(45) NULL,
  `autor` INT NULL,
  `sinopsis` LONGTEXT NULL,
  `genero` VARCHAR(45) NULL,
  `puntuacion` INT NULL,
  `tapa` VARCHAR(105) NULL,
  `Autores_idAutor` INT NOT NULL,
  `Generos_idGenero` INT NOT NULL,
  PRIMARY KEY (`idLibro`),
  INDEX `fk_Libros_Autores_idx` (`Autores_idAutor` ASC),
  INDEX `fk_Libros_Generos1_idx` (`Generos_idGenero` ASC),
  CONSTRAINT `fk_Libros_Autores`
    FOREIGN KEY (`Autores_idAutor`)
    REFERENCES `liber`.`Autores` (`idAutor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Libros_Generos1`
    FOREIGN KEY (`Generos_idGenero`)
    REFERENCES `liber`.`Generos` (`idGenero`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `liber`.`Leidos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `liber`.`Leidos` (
  `idLeidos` INT NOT NULL AUTO_INCREMENT,
  `idUsuario` VARCHAR(45) NULL,
  `idLibro` VARCHAR(45) NULL,
  PRIMARY KEY (`idLeidos`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `liber`.`Banners`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `liber`.`Banners` (
  `idBanner` INT NOT NULL AUTO_INCREMENT,
  `fechainicio` VARCHAR(45) NOT NULL,
  `fechafinal` VARCHAR(45) NOT NULL,
  `titulobanner` VARCHAR(45) NOT NULL,
  `imagen` VARCHAR(100) NOT NULL,
  `imagenpath` VARCHAR(100) NULL,
  PRIMARY KEY (`idBanner`))
ENGINE = InnoDB;

USE `liber` ;

-- -----------------------------------------------------
-- Placeholder table for view `liber`.`view1`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `liber`.`view1` (`id` INT);

-- -----------------------------------------------------
-- Placeholder table for view `liber`.`view2`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `liber`.`view2` (`id` INT);

-- -----------------------------------------------------
-- procedure SeleccionarUsuarioLogin
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
create procedure SeleccionarUsuarioLogin(
in PUser       varchar(45), 
in PPassword varchar(45)
)
begin
select  
*
 from usuarios 
 where usuario=PUser && password=PPassword ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure AgregarUsuario
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
create procedure AgregarUsuario(
in PUser       varchar(45), 
in PPassword varchar(45),
in PNombre     varchar(45),
in PApellido   varchar(45),
in PEmail      varchar(45),
in PAdmin      boolean
)
begin
insert into usuarios (nombre,apellido,usuario,email,password,admin)values (PNombre , PApellido, PUser,PEmail ,PPassword,PAdmin );

end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure Libros5Usuarios
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
create procedure Libros5Usuarios(
in PID       varchar(45)

)
begin
select * from Libros 
inner join Usuarios on Usuarios.idUsuario= Libros.idUsuario


where Usuarios.idUsuario= PID;

end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SeleccionarBanners
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE PROCEDURE `SeleccionarBanners` ()
BEGIN
select * from banners;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure AgregarBanner
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE PROCEDURE `AgregarBanner` (
in PTitulo        varchar(45), 
in PImagen        varchar(100),
in PFechaInicio   varchar(45),
in PFechaFinal    varchar(45)
)
BEGIN
insert into banners (fechainicio, fechafinal,titulobanner, imagen)
values (PFechaInicio, PFechaFinal,PTitulo,PImagen);

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure EliminarBanner
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE PROCEDURE `EliminarBanner` (

in PID int
)
BEGIN
delete  from banners 
where banners.idBanner=PID;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SeleccionarBanner
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE PROCEDURE `SeleccionarBanner` (
in PID int)
BEGIN
select * from banners where idBanner=PID;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure ModificarBanner
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ModificarBanner`(
in PID            int,
in PTitulo        varchar(45), 
in PImagen        varchar(100),
in PFechaInicio   varchar(45),
in PFechaFinal    varchar(45)
)
BEGIN
update banners 
set fechainicio=PFechaInicio, fechafinal=PFechaFinal,titulobanner=PTitulo,imagen=PImagen
where banners.idBanner=PID
;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SeleccionarTodo
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE PROCEDURE `SeleccionarTodo` ()
BEGIN
select Libros.titulo, Libros.sinopsis, Libros.puntuacion, Autores.nombre, Generos.nombre
from Libros 
inner join Autores on Libros.autor = Autores.idAutor
inner join Generos on Libros.genero= Generos.idGenero;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure BuscarAutor
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE PROCEDURE `BuscarAutor` (
in PAutor varchar(45)
)
BEGIN
select Libros.titulo, Libros.sinopsis, Libros.puntuacion, Autores.nombre, Generos.nombre
from Libros 
inner join Autores on Libros.autor = Autores.idAutor
inner join Generos on Libros.genero= Generos.idGenero
where  Autores.nombre=PAutor
;
END$$

DELIMITER ;

-- -----------------------------------------------------
--  routine1
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure BuscarGenero
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE PROCEDURE `BuscarGenero` (
in PGenero varchar(45)
)
BEGIN
select Libros.titulo, Libros.sinopsis, Libros.puntuacion, Autores.nombre, Generos.nombre
from Libros 
inner join Autores on Libros.autor = Autores.idAutor
inner join Generos on Libros.genero= Generos.idGenero
where  Generos.nombre=PAutor
;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure BuscarTitulo
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE PROCEDURE `BuscarTitulo` (
in PTitulo varchar(45)
)
BEGIN
select Libros.titulo, Libros.sinopsis, Libros.puntuacion, Autores.nombre, Generos.nombre
from Libros 
inner join Autores on Libros.autor = Autores.idAutor
inner join Generos on Libros.genero= Generos.idGenero
where  Libros.tituloPAutor=PTitulo 
;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure Buscar
-- -----------------------------------------------------

DELIMITER $$
USE `liber`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Buscar`(
in PIngresado varchar(45)


)
BEGIN
select Libros.titulo, Libros.sinopsis, Libros.puntuacion, Autores.nombre, Generos.nombre
from Libros 
inner join Autores on Libros.autor = Autores.idAutor
inner join Generos on Libros.genero= Generos.idGenero
where  Libros.titulo=PIngresado||  Generos.nombre=PIngresado||Autores.nombre=PIngresado
;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- View `liber`.`view1`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `liber`.`view1`;
USE `liber`;


-- -----------------------------------------------------
-- View `liber`.`view2`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `liber`.`view2`;
USE `liber`;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
