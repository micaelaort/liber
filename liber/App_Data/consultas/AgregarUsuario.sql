delimiter $$

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

end