create table Departaments(
DepartamentID int not null identity(1,1),
DepartamentName varchar (500),
PRIMARY KEY(DepartamentID)
);

create table Employee(
EmployeeID int primary key identity(1,1),
EmployeeName varchar(500),
Departament int not null foreign key references Departaments(DepartamentID),
DateOfJoining date,
photoFileName varchar(500),
);

select * from Employee

insert into Employee values ('Bryan Cabrera Santana',1,'2020-11-12','profile.png');

insert into Departaments values ('Support')


drop table Departaments

drop table Employee