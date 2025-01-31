create database finalproject
use finalproject

drop table productstbl
drop table userstbl
drop table orderstbl
drop table customerstbl

create table productstbl(
product_id int primary key identity not null,
product_name varchar(50) not null,
product_price decimal(18,2) not null
)

drop table productstbl

DROP PROCEDURE [add_product];
DROP PROCEDURE [edit_product];
DROP PROCEDURE [delete_product];
DROP PROCEDURE [viewall_products];
DROP PROCEDURE [search_product];

select * from products

drop procedure [add_product]
drop procedure [edit_product]
drop procedure [delete_product]
drop procedure [search_product]
drop procedure [viewall_products]

create proc add_product
@product_name nvarchar(50),
@product_price decimal(18,2)
as begin
insert into productstbl (product_name, product_price)
values (@product_name, @product_price)
end

create proc edit_product
@product_id int,
@product_name nvarchar(50),
@product_price decimal(18,2)
as begin
update productstbl set product_name = @product_name, product_price = @product_price
where product_id = @product_id
end

create proc delete_product
@product_id int
as 
begin
delete productstbl where product_id = @product_id
end



create proc viewall_products
as 
begin
select product_id as 'PRODUCT ID', product_name as 'PRODUCT NAME', product_price as 'PRODUCT PRICE'
from productstbl
end
