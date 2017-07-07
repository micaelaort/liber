delimiter $$
create procedure SeleccionarUsuario(
in PUser       varchar(45), 
in PPassword varchar(45)
)
begin
select  
usuarios.usuario, usuarios.password ,usuarios.admin
 from usuarios 
 where usuario=PUser && password=PPasswordSeleccionarUsuario ;
end