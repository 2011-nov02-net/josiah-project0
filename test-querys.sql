insert into Customer (FirstName, LastName)
values ('Jerry', 'Smith')

insert into Customer(FirstName, LastName)
values ('Morty', 'Smith')

insert into Customer(FirstName, LastName)
values ('Beth', 'Smith')

delete from Customer

dbcc checkident (Customer, reseed, 0)

select * from Customer