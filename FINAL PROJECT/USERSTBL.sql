use finalproject

drop table userstbl

create table userstbl(
userpin int primary key not null,
ulname varchar(50),
ufname varchar(50),
ucontact varchar(50),
username varchar(50),
userpass varchar(50)
)

insert into userstbl values ('7171', 'Bersabe', 'Henrich', '09099730124', 'admin', 'admin')

select * from userstbl

DROP PROCEDURE [add_user];
DROP PROCEDURE [edit_user];
DROP PROCEDURE [delete_user];
DROP PROCEDURE [viewall_users];
DROP PROCEDURE [search_user];
DROP PROCEDURE [reg_user];

create proc reg_user
@userpin int,
@username nvarchar(50),
@userpass nvarchar(50)
as begin
insert into userstbl (userpin, username, userpass)
values (@userpin, @username, @userpass)
end

create proc add_user
@userpin int,
@ulname nvarchar(50),
@ufname nvarchar(50),
@ucontact nvarchar(50),
@username nvarchar(50),
@userpass nvarchar(50)
as begin
insert into userstbl (userpin, ulname, ufname, ucontact, username, userpass)
values (@userpin, @ulname, @ufname, @ucontact, @username, @userpass)
end

create proc edit_user
@userpin int,
@ulname nvarchar(50),
@ufname nvarchar(50),
@ucontact nvarchar(50),
@username nvarchar(50),
@userpass nvarchar(50)
as begin
update userstbl set ulname = @ulname, ufname = @ufname, ucontact = @ucontact, username = @username, userpass = @userpass
where userpin = @userpin
end

create proc delete_user
@userpin int
as 
begin
delete userstbl where userpin = @userpin
end

create proc viewall_users
as 
begin
select userpin as 'USER ID', ulname as 'LASTNAME', Ufname as 'FIRSTNAME', ucontact as 'CONTACT', username as 'USERNAME'
from userstbl
end

create proc search_user
@userpin int
as 
begin
select userpin as 'USER ID', ulname as 'LASTNAME', Ufname as 'FIRSTNAME', ucontact as 'CONTACT', username as 'USERNAME'
from userstbl where userpin = @userpin 
end