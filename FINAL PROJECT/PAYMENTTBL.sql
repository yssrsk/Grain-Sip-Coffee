use finalproject
drop table paymenttbl

create table paymenttbl(
payment_id int primary key identity,
order_id int foreign key references orderstbl(order_id),
payment_amount decimal(18,2),
payment_change decimal(18,2)
)

create proc delete_payment
@payment_id int
as 
begin
delete paymenttbl where payment_id = @payment_id
end

select * from orderstbl
select * from paymenttbl