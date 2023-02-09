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
	(N'Холодильник', N'Крупная бытовая техника'),
	(N'Стиральная машина', N'Крупная бытовая техника'),
	(N'Бойлер', N'Климатическая техника'),
	(N'Кондиционер', N'Климатическая техника'),
	(N'Электрочайник', N'Техника для кухни'),
	(N'Мультиварка', N'Техника для кухни')
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
	(1, N'Samsung', N'MF-230 V80', N'Корея'),
	(2, N'LG', N'Kettle Home KK Black', N'Германия'),
	(3, N'Frost', N'F6 No Frost 200', N'Польша'),
	(4, N'Samsung', N'C10 Remote White', N'Китай'),
	(5, N'Redmond', N'Cooker Mini 5X', N'Германия'),
	(6, N'LG', N'RMC-25', N'Польша'),
	(7, N'LG', N'BFD-20 V50 Remote Control', N'Германия'),
	(8, N'Samsung', N'Wash LM-340 White', N'Корея'),
	(9, N'Frost', N'G14 Smart Black', N'Польша'),
	(10, N'Gree', N'LSMK-23 Electric', N'Франция'),
	(11, N'Saturn', N'CW 286', N'Италия'),
	(12, N'Philips', N'HD9318', N'Корея'),
	(13, N'Redmond', N'RMC-507 Auto', N'Франция'),
	(14, N'Samsung', N'OMTA46F', N'Германия'),
	(15, N'Nova', N'Tec 80K Standart', N'Германия')
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
	(3, 230, N'Верхнее', N'Инверторный'),
	(9, 320, N'Нижнее', N'Линейный'),
	(11, 350, N'Нижнее', N'Линейный')
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
	(8, 8, N'Инверторный', N'Фронтальная'),
	(14, 9, N'Коллекторный', N'Вертикальная')
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
	(1, 80, N'Цилиндрическая', N'Открытый (мокрый)'),
	(7, 50, N'Прямоугольная', N'Закрытый (сухой)'),
	(15, 80, N'Цилиндрическая', N'Закрытый (сухой)')
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
	(4, 9, 25, N'Сплит-система'),
	(10, 12, 60, N'Внешний')
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
	(2, 1500, 2, N'Нержавеющая сталь'),
	(12, 1700, 3, N'Пластик')
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
	(5, 700, 4, N'Мультиварка'),
	(6, 800, 5, N'Мультиварка-скороварка'),
	(13, 1000, 5, N'Рисоварка')
go

create view view_client
as
select group_product.obj_name as 'Тип', info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена'
from (product join info on info.id_product = product.id join fridge on fridge.id_product = info.id join group_product on fridge.id_group = group_product.id)
union
select group_product.obj_name as 'Тип', info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена'
from (product join info on info.id_product = product.id join washing_machine on washing_machine.id_product = info.id join group_product on washing_machine.id_group = group_product.id)
union
select group_product.obj_name as 'Тип', info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена'
from (product join info on info.id_product = product.id join boiler on boiler.id_product = info.id join group_product on boiler.id_group = group_product.id)
union
select group_product.obj_name as 'Тип', info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена'
from (product join info on info.id_product = product.id join conditioner on conditioner.id_product = info.id join group_product on conditioner.id_group = group_product.id)
union
select group_product.obj_name as 'Тип', info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена'
from (product join info on info.id_product = product.id join electric_kettle on electric_kettle.id_product = info.id join group_product on electric_kettle.id_group = group_product.id)
union
select group_product.obj_name as 'Тип', info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена'
from (product join info on info.id_product = product.id join multicooker on multicooker.id_product = info.id join group_product on multicooker.id_group = group_product.id)
go

create view view_sales
as
select sale.id as 'Номер чека', sale.date_sale as 'Дата', info.brand as 'Производитель', info.model as 'Модель', sale_row.k as 'Количество', product.price as 'Цена'
from sale join sale_row on sale_row.id_sale = sale.id join product on sale_row.id_product = product.id join info on info.id_product = product.id
go

create view view_admin
as
select info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена', CAST(conditioner.mark as varchar(50)) as 'Характеристика 1', CAST(conditioner.room_square as varchar(50)) as 'Характеристика 2', conditioner.kind as 'Характеристика 3', storage.k as 'Количество'
from (product join info on info.id_product = product.id join conditioner on conditioner.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена', CAST(electric_kettle.voltage_power as varchar(50)) as 'Характеристика 1', CAST(electric_kettle.volume as varchar(50)) as 'Характеристика 2', electric_kettle.material as 'Характеристика 3', storage.k as 'Количество'
from (product join info on info.id_product = product.id join electric_kettle on electric_kettle.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена', CAST(multicooker.voltage_power as varchar(50)) as 'Характеристика 1', CAST(multicooker.volume as varchar(50)) as 'Характеристика 2', multicooker.kind as 'Характеристика 3', storage.k as 'Количество'
from (product join info on info.id_product = product.id join multicooker on multicooker.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена', fridge.volume as 'Характеристика 1', fridge.freezer as 'Характеристика 2', fridge.compressor as 'Характеристика 3', storage.k as 'Количество'
from (product join info on info.id_product = product.id join fridge on fridge.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена', washing_machine.max_weight as 'Характеристика 1', washing_machine.engine as 'Характеристика 2', washing_machine.loading as 'Характеристика 3', storage.k as 'Количество'
from (product join info on info.id_product = product.id join washing_machine on washing_machine.id_product = info.id join storage on storage.id_product = product.id)
union
select info.brand as 'Производитель', info.model as 'Модель', product.price as 'Цена', boiler.volume as 'Характеристика 1', boiler.shape as 'Характеристика 2', boiler.heater as 'Характеристика 3', storage.k as 'Количество'
from (product join info on info.id_product = product.id join boiler on boiler.id_product = info.id join storage on storage.id_product = product.id)
go

alter database coursework_shop set multi_user
go
