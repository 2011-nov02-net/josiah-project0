create schema Store;
go

--drop table Customer
create table Customer (
	ID int primary key identity,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) not null
	)

--drop table Location
create table Location (
	ID int primary key identity,
	Name nvarchar(200) not null,
	)

--drop table Product;
create table Product (
	ID int primary key identity,
	Name nvarchar(200) not null,
	Price money check (price>=0)
	)

--drop table [Order]
create table [Order] (
	ID int primary key identity,
	CustomerID int not null foreign key references Customer(ID),
	LocationID int not null foreign key references Location(ID),
	Total money
)

--drop table OrderLines
create table OrderLines (
	OrderID int not null foreign key references [Order](ID),
	ProductID int not null foreign key references Product(ID),
	Quantity int not null check (Quantity > 0),
	Discount decimal default(1)
	)

create table LocationLines (
	LocationID int not null foreign key references Location(ID),
	ProductID int not null foreign key references Product(ID),
	Quantity int check (Quantity > 0)
	)