create table BookStore;

--------------------Create Users Table---------------------
CREATE TABLE Users(
	Id int Identity(1,1) PRIMARY KEY,
	Fullname varchar (200),
	Email varchar (100),
	Mobile varchar(30),
	Password varchar(150)
	);

select * from Users 
-----------------------Add user-Store Procesure----------------
USE [Bookstore]
GO
/****** Object:  StoredProcedure [dbo].[SpRegister]    Script Date: 3/11/2022 1:32:47 PM ******/
ALTER procedure SpRegister
(
	@Fullname varchar(200),
	@Email varchar(200),
	@Mobile varchar(100),
	@Password varchar(100)
)
as
begin
IF (select Id from Users where Email=@Email) is not null 
	begin
		select 1;
	end
	else
	begin   
		Insert into Users (Fullname,Email,Mobile,Password)    
		Values (@Fullname,@Email,@Mobile,@Password) 
	end   
END

---------------------------Login-store procedure-----------------------------------
ALTER procedure Splogin
(
	@Email varchar(200),
	@Password varchar(100)
)
as
begin
select * from Users where Email=@Email and Password=@Password

END

-----------------------------------Forget password-stored procedure-------------
ALTER PROCEDURE [dbo].[SpForgetPass]
@Email varchar(200)
AS
BEGIN
	SELECT * from Users where Email=@Email
END
--------------------------------Reset Password-------------------------------------------------
ALTER PROCEDURE [dbo].[SpReset]
@Email varchar(200),
@Password varchar(200)
AS
BEGIN
	Update Users Set Password=@Password where Email=@Email
END

/***Book stored procedure***/

------------------------Create book table---------------------
create TABLE Books(
	bookId int Identity(1,1) PRIMARY KEY,
	bookName varchar(200),
	authorName varchar(200),
    rating varchar(200),   
	totalRating int,
	discountPrice int,
	originalPrice int,
	description varchar(255),
	bookImage varchar(200),
	BookCount int not null
	);
---------------------------Insert Book------------------
ALTER PROCEDURE Sp_AddBook
@bookName varchar(200),
@authorName varchar(200),@rating varchar(200),@totalRating int,@discountPrice int,
@originalPrice int,@description varchar(255),@bookImage varchar(200),@BookCount int

AS
BEGIN
insert into Books values(@bookName,@authorName,@rating,@totalRating,@discountPrice,
@originalPrice,@description,@bookImage,@BookCount);
SELECT * from Books
END

---------------------------Delete Book----------------
ALTER PROCEDURE [dbo].[Sp_Delete]
	@bookId int
AS
BEGIN
delete from Books where bookId=@bookId

	SELECT * from Books
END
---------------------------Update book---------------------
ALTER PROCEDURE [dbo].[Sp_Updatebook]
@bookId int,
@bookName varchar(200),
@authorName varchar(200),@rating varchar(200),@totalRating int,@discountPrice int,
@originalPrice int,@description varchar(255),@bookImage varchar(200),@BookCount int
AS
BEGIN
update Books set bookName=@bookName,authorName=@authorName,rating=@rating,totalRating=@totalRating,discountPrice=@discountPrice,
originalPrice=@originalPrice,description=@description,bookImage=@bookImage,BookCount=@BookCount where bookId=@bookId
SELECT * from Books
END

-------------------------Get all books--------------------------
ALTER procedure [dbo].[spGetAll]
as
begin
select * from Books
END
----------------------------Get book By id----------------------
ALTER procedure [dbo].[Retrive_1_BookDetails]
(
	@bookId int
)
as
begin
select * from Books where bookId=@bookId
END


/*************************************Cart stored Procedure******************/
---------------------------Create cart table---------------------
CREATE TABLE Cart
(
	CartId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES Users(id),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES Books(bookId),	
	Quantity INT default 1
);
-------------------------Add cart-store procedure----------------------------

ALTER procedure [dbo].[Sp_AddCart]
(
@UserId int,@BookId int,@Quantity int
)
as
begin
IF (EXISTS(SELECT * FROM Books WHERE bookId=@BookId))		
	begin
		INSERT INTO Cart(UserId,BookId)
		VALUES (@UserId,@BookId)
	end
	else
	begin 
		select 2
	end
SET NOCOUNT ON;
END
-----------------------------Delete cart------------------------
ALTER procedure [dbo].[Sp_DeleteCart]
(
@CartId int
)
as
begin
delete from Cart where CartId=@CartId
select * from Cart
SET NOCOUNT ON;
END

-----------------------Update cart-----------------------------
ALTER procedure [dbo].[Sp_UpdateCart]
(@CartId int,
@BookId int,@UserId int,@AddressId int,@Quantity int
)
as
begin
update Cart set BookId=@BookId,Userid=@UserId,AddressId=@AddressId,Quantity=@Quantity where CartId=@CartId
select * from Cart
SET NOCOUNT ON;
END

--------------------------Getcart----------------------------
ALTER procedure [dbo].[Sp_RetriveCart]
(@UserId int
)
as
begin
select * from Cart where UserId=@UserId;
SET NOCOUNT ON;
END


/****************Address store procedure************************/
select * from AddressType
create table AddressType
(
    TypeId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Type varchar(200)
);
insert into Addresses values('Home');
insert into Addresses values('Office');
insert into Addresses values('Other');
----------------------------Create table-------------------------
create table Addresse
(
    AddressId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Address varchar(max) not null,
	City varchar(100),
	State varchar(100),
	Type int FOREIGN KEY (Type) REFERENCES AddressType(TypeId),UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES Users(id),
);
select * from Addresse

----------------------Add Address Store procedure-------------------

ALTER procedure [dbo].[SpAddress]
(
	@AddressId int ,@Address varchar(600),@City varchar(200),@State varchar(200),@Type varchar(200),@UserId int
)
as
begin
	IF (EXISTS(SELECT * FROM Users WHERE @UserId = @UserId))
	Begin
	insert into Addresse values(@Address,@City,@State,@Type,@UserId);
	End
	Else
	Begin
		Select 1
	End
END

-----------------------------Update address--------------------
ALTER PROCEDURE [dbo].[Sp_UpdateAddress]
	@AddressId int,@Address varchar(200),@City varchar(200),@State varchar(200),@Type int
AS
BEGIN
  If (exists(Select * from Addresse where AddressId=@AddressId))
		begin
			
update Addresse set Address=@Address,City=@City,State=@State,Type=@Type where AddressId=@AddressId
		 end
		 else
		 begin
		 select 1;
		 end
SELECT * from Addresse
END
-----------------------------Get all Address--------------------
ALTER PROCEDURE [dbo].[Sp_GetUserAddress]
AS
BEGIN
	SELECT * from Addresse
END

------------------------get address by userid--------------------
create PROCEDURE [dbo].[Sp_GetUserAddressById]
(@UserId int
)
AS
BEGIN
	SELECT * from Addresse where UserId=@UserID
END
----------------------------Delete Addresss--------------------


/**********************************Orders****************************/

-------------------------------Create table------------------------
create table OrderTable(
OrdersId int identity(1,1) not null primary key,UserId int FOREIGN KEY (UserId) REFERENCES Users(id),BookId int FOREIGN KEY (BookId) REFERENCES Books(bookId),
AddressId int FOREIGN KEY (AddressId) REFERENCES Addresse(AddressId),TotalPrice int,BookQuantity int,OrderDate Date);

-----------------------------Add Order--------------------------
ALTER PROCEDURE [dbo].[Sp_AddOrder]
@UserId INT,
	@AddressId int,
	@BookId INT ,
	@BookQuantity int
AS
	Declare @TotPrice int
BEGIN
Select @TotPrice=discountprice from Books where BookId = @BookId;
	IF (EXISTS(SELECT * FROM Books WHERE bookId = @BookId))
	begin
		IF (EXISTS(SELECT * FROM Users WHERE id = @UserId))
		Begin
		Begin try
			Begin transaction			
				INSERT INTO OrderTable(UserId,AddressId,BookId,Totalprice,BookQuantity,OrderDate)
				VALUES ( 1,2,2,3*200,2,GETDATE())
				Update Books set BookCount=BookCount-@BookQuantity
				Delete from Cart where BookId = @BookId and UserId = @UserId
				select * from OrderTable
			commit Transaction
		End try
		Begin catch
			Rollback transaction
		End catch
		end
		Else
		begin
			Select 1
		end
	end 
	Else
	begin
			Select 2
	end	
END
------------------------------Get Orders---------------------------
ALTER procedure [dbo].[Sp_GetOrderById]
@UserId INT
as
begin
select 
		Books.bookId,Books.bookName,Books.AuthorName,Books.DiscountPrice,Books.OriginalPrice,Books.bookImage,OrderTable.OrdersId
		FROM Books
		inner join OrderTable
		on OrderTable.BookId=Books.bookId where OrderTable.UserId=@UserId
SET NOCOUNT ON;
END

/****************************Wishlist*********************************/

-----------------------------AddWishlist--------------------------
ALTER procedure [dbo].[Sp_AddWishlist]
(
@UserId INT,
	@BookId INT
	)
AS
BEGIN 
	IF EXISTS(SELECT * FROM WishlistTable WHERE BookId = @BookId AND UserId = @UserId)
		SELECT 1;
	ELSE
	BEGIN
		IF EXISTS(SELECT * FROM Books WHERE bookId = @BookId)
		BEGIN
			INSERT INTO WishlistTable(UserId,BookId)
			VALUES ( @UserId,@BookId)
		END
		ELSE
			SELECT 2;
	END
END

ALTER procedure [dbo].[Sp_deleteWishlist]
(
@wishlistId int
)
as
begin
delete from WishlistTable where WishlistId=@wishlistId
select * FROM WishlistTable
SET NOCOUNT ON;
END

---------------------Getwishlist------------------------

ALTER PROCEDURE [dbo].[sp_ShowWishlistbyUserId]
  @UserId int
AS
BEGIN
	   select 
		Books.bookId,Books.BookName,Books.AuthorName,Books.rating,Books.description,Books.DiscountPrice,Books.OriginalPrice,Books.bookImage,Books.Reviewer,WishlistTable.WishlistId,WishlistTable.UserId,WishlistTable.BookId
		FROM Books
		inner join WishlistTable
		on WishlistTable.BookId=Books.BookId where WishlistTable.UserId=@UserId
		
End

/**************Feedback Stored procedure******************************/





--------------------------Create feedback table------------------------------------
create table Feedback(FeedbackId int identity(1,1) not null primary key,UserID int not null foreign key (Userid) References Users(id),
BookId int not null foreign key (bookId) References Books(bookId),Comment varchar(max),Ratings int);
select * from Feedback;

---------------------------Add Feedback-------------------------
alter procedure Sp_AddFeedback
(
@UserId int,@BookId int,@Comment varchar(max),@Ratings int
)
as
Declare @AverageRating int;
Begin
	IF (EXISTS(SELECT * FROM Feedback WHERE BookId = @BookId and UserId=@UserId))
		select 1; --already given feedback--
	Else
	Begin
		IF (EXISTS(SELECT * FROM Books WHERE bookId = @BookId))
		Begin
			Begin try
				Begin transaction
					Insert into Feedback values (@UserId,@BookId,@Comment,@Ratings);		
					select @AverageRating=AVG(Ratings) from Feedback where BookId = @BookId;
					Update Books set rating=@AverageRating, Reviewer=Reviewer+1 where bookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
End
--------------------------GetFeedback--------------------
alter PROC spGetFeedbacks
	@BookId INT
AS
BEGIN
	select 
		Feedback.FeedbackId,Feedback.UserId,Feedback.BookId,Feedback.Comment,Feedback.Ratings,Users.Fullname
		FROM Users
		inner join Feedback
		on Feedback.UserId=Users.id
		where BookId=@BookId
END
select * from Feedback