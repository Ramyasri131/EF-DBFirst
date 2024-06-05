Use RamyaEmployeeDirectoryDB;
Create database RamyaEmployeeDirectoryDB;

create table employee(
 [Employee_Id] varchar(30) primary key not null,
 [FirstName] varchar(30) not null,
 [LastName] varchar(30) not null,
 [Email] varchar(30) not null,
 [MobileNumber] bigint not null,
 [DateOfBirth] date not null,
 [DateOfJoin] date not null,
 [Location] int not null,
 [JobTitle] int not null,
 [Department] int not null,
 [Manager] int not null,
 [Project] int not null
 constraint fk_employee_Departments foreign key (Department) references Departments(Dept_Id),
 constraint fk_employee_Location foreign key ([Location]) references [Location](Location_Id),
 constraint fk_employee_roles foreign key (JobTitle) references Roles(Role_Id),
 constraint fk_employee_Manager foreign key ([Manager]) references Manager(Manager_Id),
 constraint fk_employee_Projects foreign key ([Project]) references projects(Project_Id)
 )



create table [Roles](
 [Role_Id] int IDENTITY(1, 1) primary key,
 [Role_Name] varchar(30) not null,
 [Location] int not null,
 [Department] int not null,
 [Description] varchar(30),
 constraint fk_Roles_Departments foreign key (Department) references Departments(Dept_Id),
 constraint fk_Roles_Location foreign key ([Location]) references Location(Location_Id)
);



create table [Departments](
[Dept_Id] int IDENTITY(1, 1) primary key,
[Dept_Name] varchar(30) not null
);



create table [Manager](
[Manager_Id] int IDENTITY(1, 1) primary key,
[Manager_Name] varchar(30) not null
);



create table [Projects](
[Project_Id] int IDENTITY(1, 1) primary key,
[Project_Name] varchar(30) not null
);



create table [Location](
[Location_Id] int IDENTITY(1, 1) primary key,
[Location_Name] varchar(30) not null
);






