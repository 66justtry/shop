use master
go
if exists(select * from sys.databases where name = 'coursework_shop')
begin
	alter database coursework_shop set single_user with rollback immediate
	drop database coursework_shop
end
go
create database coursework_shop
go
use coursework_shop
go

create table product
(
	id int not null identity(1,1) primary key,
	price int not null default 0
)
go

insert into product(price) values
	(8400),
	(899),
	(13200),
	(5700),
	(2700),
	(4499),
	(8190),
	(12000),
	(19300),
	(28240),
	(12300),
	(1299),
	(4200),
	(15800),
	(6400)
go

create table storage
(
	id int not null identity(1,1) primary key,
	id_product int not null foreign key references product(id),
	k int not null default 0
)
go

insert into storage(id_product, k) values
	(1, 4),
	(2, 12),
	(3, 1),
	(4, 0),
	(5, 1),
	(6, 1),
	(7, 5),
	(8, 1),
	(9, 6),
	(10, 2),
	(11, 1),
	(12, 4),
	(13, 5),
	(14, 1),
	(15, 0)
go

create table sale
(
	id int not null identity(1,1) primary key,
	date_sale date,
	sum_sale int not null default 0
)
go

create trigger tr1
on sale
instead of delete
as
begin
declare @id int = (select top 1 id from deleted)
delete from sale_row where id_sale = @id
delete from sale where id = @id
end
go

insert into sale(date_sale, sum_sale) values
	('2022-09-22', 12000),
	('2022-09-30', 3940),
	('2022-10-02', 11400),
	('2022-11-21', 899),
	('2022-11-21', 8400),
	('2022-11-24', 18700),
	('2022-11-25', 4499)
go

create table sale_row
(
	id int not null identity(1,1) primary key,
	id_sale int not null foreign key references sale(id),
	id_product int not null foreign key references product(id),
	k int not null default 1
)
go

create trigger tr2
on sale_row
after update, insert
as
begin
declare @id int = (select top 1 id_sale from inserted)
update sale set sum_sale = (select sum(product.price * sale_row.k) from sale_row join product on sale_row.id_product = product.id where id_sale = @id) where id = @id
end
go

insert into sale_row(id_sale, id_product, k) values (1, 8, 1)
go
insert into sale_row(id_sale, id_product, k) values (2, 5, 1)
go
insert into sale_row(id_sale, id_product, k) values (2, 10, 1)
go
insert into sale_row(id_sale, id_product, k) values (3, 4, 2)
go
insert into sale_row(id_sale, id_product, k) values (4, 2, 1)
go
insert into sale_row(id_sale, id_product, k) values (5, 13, 2)
go
insert into sale_row(id_sale, id_product, k) values (6, 15, 1)
go
insert into sale_row(id_sale, id_product, k) values (6, 11, 1)
go
insert into sale_row(id_sale, id_product, k) values (7, 6, 1)
go

create table group_product
(
	id int not null identity(1,1) primary key,
	obj_name nvarchar(50) not null,
	group_name nvarchar(50) not null
)
go

insert into group_product(obj_name, group_name) values
	(N'�����������', N'������� ������� �������'),
	(N'���������� ������', N'������� ������� �������'),
	(N'������', N'������������� �������'),
	(N'�����������', N'������������� �������'),
	(N'�������������', N'������� ��� �����'),
	(N'�����������', N'������� ��� �����')
go

create table info
(
	id int not null identity(1,1) primary key,
	id_product int not null foreign key references product(id),
	brand nvarchar(50) not null,
	model nvarchar(50) not null,
	country nvarchar(50)
)
go

insert into info(id_product, brand, model, country) values
	(1, N'Samsung', N'MF-230 V80', N'�����'),
	(2, N'LG', N'Kettle Home KK Black', N'��������'),
	(3, N'Frost', N'F6 No Frost 200', N'������'),
	(4, N'Samsung', N'C10 Remote White', N'�����'),
	(5, N'Redmond', N'Cooker Mini 5X', N'��������'),
	(6, N'LG', N'RMC-25', N'������'),
	(7, N'LG', N'BFD-20 V50 Remote Control', N'��������'),
	(8, N'Samsung', N'Wash LM-340 White', N'�����'),
	(9, N'Frost', N'G14 Smart Black', N'������'),
	(10, N'Gree', N'LSMK-23 Electric', N'�������'),
	(11, N'Saturn', N'CW 286', N'������'),
	(12, N'Philips', N'HD9318', N'�����'),
	(13, N'Redmond', N'RMC-507 Auto', N'�������'),
	(14, N'Samsung', N'OMTA46F', N'��������'),
	(15, N'Nova', N'Tec 80K Standart', N'��������')
go

create table fridge
(
	id int not null identity(1,1) primary key,
	id_group int default 1 foreign key references group_product(id),
	id_product int not null foreign key references info(id),
	volume int,
	freezer nvarchar(50),
	compressor nvarchar(50)
)
go

insert into fridge(id_product, volume, freezer, compressor) values
	(3, 230, N'�������', N'�����������'),
	(9, 320, N'������', N'��������'),
	(11, 350, N'������', N'��������')
go

create table washing_machine
(
	id int not null identity(1,1) primary key,
	id_group int default 2 foreign key references group_product(id),
	id_product int not null foreign key references info(id),
	max_weight int,
	engine nvarchar(50),
	loading nvarchar(50)
)
go

insert into washing_machine(id_product, max_weight, engine, loading) values
	(8, 8, N'�����������', N'�����������'),
	(14, 9, N'������������', N'������������')
go

create table boiler
(
	id int not null identity(1,1) primary key,
	id_group int default 3 foreign key references group_product(id),
	id_product int not null foreign key references info(id),
	volume int,
	shape nvarchar(50),
	heater nvarchar(50)
)
go

insert into boiler(id_product, volume, shape, heater) values
	(1, 80, N'��������������', N'�������� (������)'),
	(7, 50, N'�������������', N'�������� (�����)'),
	(15, 80, N'��������������', N'�������� (�����)')
go

create table conditioner
(
	id int not null identity(1,1) primary key,
	id_group int default 4 foreign key references group_product(id),
	id_product int not null foreign key references info(id),
	mark int,
	room_square int,
	kind nvarchar(50)
)
go

insert into conditioner(id_product, mark, room_square, kind) values
	(4, 9, 25, N'�����-�������'),
	(10, 12, 60, N'�������')
go

create table electric_kettle
(
	id int not null identity(1,1) primary key,
	id_group int default 5 foreign key references group_product(id),
	id_product int not null foreign key references info(id),
	voltage_power int,
	volume int,
	material nvarchar(50)
)
go

insert into electric_kettle(id_product, voltage_power, volume, material) values
	(2, 1500, 2, N'����������� �����'),
	(12, 1700, 3, N'�������')
go

create table multicooker
(
	id int not null identity(1,1) primary key,
	id_group int default 6 foreign key references group_product(id),
	id_product int not null foreign key references info(id),
	voltage_power int,
	volume int,
	kind nvarchar(50)
)
go

insert into multicooker(id_product, voltage_power, volume, kind) values
	(5, 700, 4, N'�����������'),
	(6, 800, 5, N'�����������-����������'),
	(13, 1000, 5, N'���������')
go

create view view_client
as
select group_product.obj_name as '���', info.brand as '�������������', info.model as '������', product.price as '����'
from (product join info on info.id_product = product.id join fridge on fridge.id_product = info.id join group_product on fridge.id_group = group_product.id)
union
select group_product.obj_name as '���', info.brand as '�������������', info.model as '������', product.price as '����'
from (product join info on info.id_product = product.id join washing_machine on washing_machine.id_product = info.id join group_product on washing_machine.id_group = group_product.id)
union
select group_product.obj_name as '���', info.brand as '�������������', info.model as '������', product.price as '����'
from (product join info on info.id_product = product.id join boiler on boiler.id_product = info.id join group_product on boiler.id_group = group_product.id)
union
select group_product.obj_name as '���', info.brand as '�������������', info.model as '������', product.price as '����'
from (product join info on info.id_product = product.id join conditioner on conditioner.id_product = info.id join group_product on conditioner.id_group = group_product.id)
union
select group_product.obj_name as '���', info.brand as '�������������', info.model as '������', product.price as '����'
from (product join info on info.id_product = product.id join electric_kettle on electric_kettle.id_product = info.id join group_product on electric_kettle.id_group = group_product.id)
union
select group_product.obj_name as '���', info.brand as '�������������', info.model as '������', product.price as '����'
from (product join info on info.id_product = product.id join multicooker on multicooker.id_product = info.id join group_product on multicooker.id_group = group_product.id)
go

create view view_sales
as
select sale.id as '����� ����', sale.date_sale as '����', info.brand as '�������������', info.model as '������', sale_row.k as '����������', product.price as '����'
from sale join sale_row on sale_row.id_sale = sale.id join product on sale_row.id_product = product.id join info on info.id_product = product.id
go

create view view_admin
as
select info.brand as '�������������', info.model as '������', product.price as '����', CAST(conditioner.mark�as�varchar(50)) as '�������������� 1', CAST(conditioner.room_square�as�varchar(50)) as '�������������� 2', conditioner.kind as '�������������� 3', storage.k as '����������'
from (product join info on info.id_product = product.id join conditioner on conditioner.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as '�������������', info.model as '������', product.price as '����', CAST(electric_kettle.voltage_power�as�varchar(50)) as '�������������� 1', CAST(electric_kettle.volume�as�varchar(50)) as '�������������� 2', electric_kettle.material as '�������������� 3', storage.k as '����������'
from (product join info on info.id_product = product.id join electric_kettle on electric_kettle.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as '�������������', info.model as '������', product.price as '����', CAST(multicooker.voltage_power�as�varchar(50)) as '�������������� 1', CAST(multicooker.volume�as�varchar(50)) as '�������������� 2', multicooker.kind as '�������������� 3', storage.k as '����������'
from (product join info on info.id_product = product.id join multicooker on multicooker.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as '�������������', info.model as '������', product.price as '����', fridge.volume as '�������������� 1', fridge.freezer as '�������������� 2', fridge.compressor as '�������������� 3', storage.k as '����������'
from (product join info on info.id_product = product.id join fridge on fridge.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as '�������������', info.model as '������', product.price as '����', washing_machine.max_weight as '�������������� 1', washing_machine.engine as '�������������� 2', washing_machine.loading as '�������������� 3', storage.k as '����������'
from (product join info on info.id_product = product.id join washing_machine on washing_machine.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as '�������������', info.model as '������', product.price as '����', boiler.volume as '�������������� 1', boiler.shape as '�������������� 2', boiler.heater as '�������������� 3', storage.k as '����������'
from (product join info on info.id_product = product.id join boiler on boiler.id_product = info.id join storage on storage.id_product = product.id)
go

alter database coursework_shop set multi_user
go
