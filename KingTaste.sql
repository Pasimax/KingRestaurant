Create Database King_Taste_Restaurant;
Use King_Taste_Restaurant;

create table Employee
(Emp_ID varchar(5) primary key,
Emp_Name varchar(50) not null,
Address varchar(50) not null,
Contact_No varchar(10) not null,
Gender varchar(10) not null,
Designation varchar(10) not null,
email varchar(50) not null);


Create Table Customer
(Cus_ID Varchar(5) Primary Key not null,
Cus_Name Varchar(50) not null,
Address Varchar(50) not null,
Contact_No int not null);

Create Table Food_Item
(Food_ID Varchar(5) Primary Key not null,
Food_Name Varchar(10) not null,
Price int not null,
Discount int,
Category Varchar(10) not null,
Description Varchar(20) not null,
Em_ID Varchar(5) References Employee(Emp_ID));

Create Table Reservation
(Reservation_ID Varchar(5) Primary Key not null,
Reservation_Name Varchar(20) not null,
C_ID Varchar(5) References Customer(Cus_ID),
Date_Applied Date not null,
Date_of_sheduled Date not null);

Create Table Orders
(Order_ID Varchar(5) Primary Key not null,
Date Date not null,
Total_Amount int ,
Cu_ID Varchar(5) References Customer(Cus_ID));

Create Table Payment
(Payment_ID Varchar(5) Primary Key not null,
Date Date,
Payment_Type Varchar(10),
Payment_Amount int,
O_ID varchar(5) References Orders(Order_ID),
Cust_ID Varchar(5) References Customer(Cus_ID));

Create Table Invoice
(Invoice_ID Varchar(5) Primary Key not null,
Date Date not null,
Amount int,
Or_ID Varchar(5) References Orders (Order_ID));

Create Table Order_FdItem
(Ord_ID Varchar(5) References Orders(Order_ID),
F_ID Varchar(5) References Food_Item(Food_ID),
Qty int,
Primary Key(Ord_ID,F_ID));

Create Table Login
(User_name Varchar(20) Primary Key not null,
Password Varchar(10) not null,
E_ID Varchar(5) References Employee (EMP_ID));

insert into Login
values
('admin','admin123','M1');

insert into Employee
values 
('M1','Kamal','Colombo','0770500218','Male','Manager','Kamal@gmail.com');