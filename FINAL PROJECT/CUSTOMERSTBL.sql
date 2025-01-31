use finalproject

drop table customerstbl

delete from orderstbl

select * from orderstbl
select * from customerstbl

create table customerstbl(
customer_id int primary key identity not null,
customer_lname varchar(50),
customer_fname varchar(50) not null,
customer_con varchar(50),
customer_address varchar(50)
)

DROP PROCEDURE [add_customer];
DROP PROCEDURE [edit_customer];
DROP PROCEDURE [delete_customer];
DROP PROCEDURE [viewall_customers];
DROP PROCEDURE [search_customer];

create proc add_customer
@customer_fname nvarchar(50),
@customer_lname nvarchar(50),
@customer_con nvarchar(50),
@customer_address nvarchar(50)
as begin
insert into customerstbl (customer_fname, customer_lname, customer_con, customer_address)
values (@customer_fname, @customer_lname, @customer_con, @customer_address)
end

create proc edit_customer
@customer_id int,
@customer_lname nvarchar(50),
@customer_fname nvarchar(50),
@customer_con nvarchar(50),
@customer_address nvarchar(50)
as begin
update customerstbl set customer_lname = @customer_lname, customer_fname = @customer_fname, customer_con = @customer_con, customer_address = @customer_address
where customer_id = @customer_id
end

create proc delete_customer
@customer_id int
as 
begin
delete customerstbl where customer_id = @customer_id
end

create proc viewall_customers
as 
begin
select customer_id as 'Customer ID', customer_lname as 'LASTNAME', customer_fname as 'FIRSTNAME', customer_con as 'CONTACT', customer_address as 'ADDRESS'
from customerstbl
end

create proc search_customer
@customer_fname int
as 
begin
select customer_id as 'Customer ID', customer_lname as 'LASTNAME', customer_fname as 'FIRSTNAME', customer_con as 'CONTACT', customer_address as 'ADDRESS'
from customerstbl where customer_fname = @customer_fname
end