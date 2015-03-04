USE [master]
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'orm_example')
DROP DATABASE [orm_example]
GO

USE [master]
GO

CREATE DATABASE [orm_example]
GO

use [orm_example]
go

create table [user] (
	id bigint identity not null,
	name nvarchar(32) not null,
	primary key (id)
)

create table [order] (
	id bigint identity not null,
	[user_id] bigint not null,
	[date] datetime not null,
	primary key (id)
)

create table order_items(
	id bigint identity not null,
	order_id bigint not null,
	product_id bigint not null,
	primary key (id)
)

create table [product] (
	id bigint identity not null,
	name nvarchar(64) not null,
	primary key (id)
)

create table [user_detail] (
	id bigint identity not null,
	[user_id] bigint not null,
	phone nvarchar(32) not null,
	primary key (id)
)
go

create unique nonclustered index [idx_user_name] on [user] 
(
	[name] asc
)

create nonclustered index [idx_order_user_id] on [order] 
(
	[user_id] asc
)

create nonclustered index [idx_order_items_order_id] on [order_items] 
(
	[order_id] asc
)

go

set identity_insert [user] on
go

insert into [user] ([id], [name]) values (1, 'Conan')
insert into [user] ([id], [name]) values (2, 'Xi Yangyang')
go

set identity_insert [user] off
go

set identity_insert [order] on
go

insert into [order] ([id], [user_id], [date]) values (1, 1, '2015-01-01')

set identity_insert [order] off
go

set identity_insert [order_items] on
go

insert into [order_items] ([id], [order_id], [product_id]) values (1, 1, 1)
insert into [order_items] ([id], [order_id], [product_id]) values (2, 1, 2)
insert into [order_items] ([id], [order_id], [product_id]) values (3, 1, 3)

set identity_insert [order_items] off
go

set identity_insert [product] on
go

insert into [product] ([id], [name]) values (1, 'Glass')
insert into [product] ([id], [name]) values (2, 'Skateboard')
insert into [product] ([id], [name]) values (3, 'Cap')

set identity_insert [product] off
go