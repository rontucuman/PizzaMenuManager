/*==============================================================*/
/* Database name:  PizzaMenuManager                             */
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     5/31/2020 10:12:35 PM                        */
/*==============================================================*/


use PizzaMenuManager
go

use PizzaMenuManager
go

/*==============================================================*/
/* User: dbo                                                    */
/*==============================================================*/
create schema dbo
go

/*==============================================================*/
/* Table: Ingredient                                            */
/*==============================================================*/
create table dbo.Ingredient (
   Id                   int                  not null,
   Name                 varchar(64)          not null,
   constraint Ingredient_PK primary key (Id)
)
go

/*==============================================================*/
/* Table: Pizza                                                 */
/*==============================================================*/
create table dbo.Pizza (
   Id                   int                  not null,
   Name                 varchar(64)          not null,
   constraint Pizza_PK primary key (Id)
)
go

/*==============================================================*/
/* Table: PizzaIngredient                                       */
/*==============================================================*/
create table dbo.PizzaIngredient (
   Id                   int                  not null,
   PizzaId              int                  not null,
   IngredientId         int                  not null,
   constraint PizzaIngredient_PK primary key (Id)
)
go

/*==============================================================*/
/* Index: PizzaIngredient_UK                                    */
/*==============================================================*/




create unique nonclustered index PizzaIngredient_UK on dbo.PizzaIngredient (PizzaId ASC,
  IngredientId ASC)
go

alter table dbo.PizzaIngredient
   add constraint PizzaIngredient_Ingredient_FK foreign key (IngredientId)
      references dbo.Ingredient (Id)
go

alter table dbo.PizzaIngredient
   add constraint PizzaIngredient_Pizza_FK foreign key (PizzaId)
      references dbo.Pizza (Id)
go

