delimiter $$
create procedure SeleccionarUsuario(
in PUser       varchar(45), 
in PContraseña varchar(45)
)
begin
select  
usuarios.usuario, usuarios.contraseña ,usuarios.admin
 from usuarios 
 where usuario=PUser &&contraseña=PContraseña ;
end