use finalproject
drop table orderstbl
create table orderstbl(
order_id int primary key identity,
customer_id int foreign key references customerstbl(customer_id),
product_id int foreign key references productstbl(product_id),
customer_nm varchar(50),
order_product varchar(50),
order_quantity int,
order_price decimal(18,2),
total_amount decimal(18, 2),
dot datetime,
order_type varchar(50),
payment_method varchar(50)
)

delete from orderstbl
delete from paymenttbl
select * from userstbl
select * from productstbl
select * from customerstbl
select * from orderstbl
select * from paymenttbl


DROP PROCEDURE [add_order];
DROP PROCEDURE [edit_customer];
DROP PROCEDURE [delete_order];
DROP PROCEDURE [viewall_customers];
DROP PROCEDURE [search_customer];

SELECT 
    orderstbl.order_id as 'Order No.',
    customerstbl.customer_fname + customerstbl.customer_lname as 'Customer Name',
    customerstbl.customer_con as 'Contact No.',
    customerstbl.customer_address as 'Address',
    orderstbl.order_product as 'Product Name',
    orderstbl.order_price as 'Product Price',
    orderstbl.order_Quantity as 'Quantity',
    orderstbl.total_amount as 'Total',
    orderstbl.Order_type as 'Order Type',
    orderstbl.payment_method as 'Payment Method',
    paymenttbl.payment_id as 'Payment ID',
    paymenttbl.payment_amount as 'Payment Amount',
    paymenttbl.Payment_change as 'Payment Change'
FROM 
    orderstbl
INNER JOIN 
    customerstbl ON orderstbl.customer_id = customerstbl.customer_id
LEFT JOIN 
    paymenttbl ON orderstbl.order_id = paymenttbl.order_id
GROUP BY 
    orderstbl.order_id, customerstbl.customer_fname + customerstbl.customer_lname, customerstbl.customer_con,
    customerstbl.customer_address, orderstbl.order_product, orderstbl.order_price, orderstbl.order_Quantity,
    orderstbl.total_amount, orderstbl.Order_type, orderstbl.payment_method, paymenttbl.payment_id,
    paymenttbl.payment_amount, paymenttbl.Payment_change;


create proc edit_order
@order_id int,
@order_product nvarchar(50),
@order_quantity int,
@order_price decimal(18,2),
@total_amount decimal(18,2),
@order_type nvarchar(50),
@payment_method nvarchar(50)
as begin
update orderstbl set order_product = @order_product, order_quantity = @order_quantity, order_price = @order_price, total_amount = @total_amount, order_type = @order_type, payment_method = @payment_method
where order_id = @order_id
end