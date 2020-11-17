insert into Customer (FirstName, LastName)
values ('Jerry', 'Smith')

insert into Customer(FirstName, LastName)
values ('Morty', 'Smith')

insert into Customer(FirstName, LastName)
values ('Beth', 'Smith')

delete from Customer

dbcc checkident (Customer, reseed, 0)

select * from Customer

select * from Location

select * from Product

select * from LocationLines

select * from [Order]

select * from OrderLines

insert into Product values ('muffin', 2.0)

insert into Product values ('lollipop', 1.0)

insert into LocationLines values (1, 1, 10)

insert into LocationLines values (1, 2, 5)

insert into [Order] values (1, 1, 5.00)

insert into [Order] values (2, 1, 10.00)

insert into OrderLines values (1, 1, 1, 1.0)
insert into OrderLines values (1, 2, 3, 1.0)

insert into OrderLines values(2, 1, 10, 1.0)

