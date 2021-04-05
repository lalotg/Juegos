Create Table Equipos(
EquipoId int primary key identity(1,1),
Descripcion varchar(15),
Fundacion date,
Logotipo varchar(350),
Creacion datetime default getdate(),
Modificacion datetime default getdate()
)
GO
CREATE table Jugadores (
JugadorId int primary key identity(1,1),
Nombre varchar(150),
Nacimiento date,
Descripcion varchar(200),
EquipoId int,
Creacion datetime default getdate(),
Modificacion datetime default getdate(),
foreign key(EquipoId) references Equipos(EquipoId)
)
GO
CREATE  Table Partidos(
PartidoId int primary key identity(1,1),
Fecha date,
Anfitrion int,
Visitante int,
Marcador varchar(10),
Creacion datetime default getdate(),
Modificacion datetime default getdate()
)
Go
Create Procedure SP_Add_Partido
@Fecha date,
@Anfitrion int,
@Visitante int,
@Marcador varchar(10),
@Id int out
As
Begin
	Insert Into Partidos (
		Fecha,
		Anfitrion,
		Visitante,
		Marcador
	) Values(
		@Fecha,
		@Anfitrion,
		@Visitante,
		@Marcador
	)
	Set @Id = SCOPE_IDENTITY()
END
GO
Create Procedure SP_Up_Partido
@Fecha date,
@Anfitrion int,
@Visitante int,
@Marcador varchar(10),
@Id int
As
Begin
	Update 
		Partidos
	Set
		Fecha = @Fecha,
		Anfitrion = Anfitrion,
		Visitante = @Visitante,
		Marcador = @Marcador,
        Modificacion = getdate()
	Where
		PartidoId = @Id 
END