Use Master
GO
Create Database MessageBoardDB
GO

Use MessageBoardDB
GO

-- Create Tables

Create Table [dbo].[Category](
    [CategoryId] [Int] NOT NULL Primary Key Identity(1, 1),
    [CategoryName] [NVarChar](100) NOT NULL,
)

Create Table [dbo].[Users](
    [UserId] [Int] NOT NULL Primary Key Identity(1, 1),
    [Username] [VarChar](30) NOT NULL,
    [Password] [VarChar](60) NOT NULL
)
GO
Create Table [dbo].[Messages] (
    [MessageId] [Int] NOT NULL Primary Key Identity(1, 1),
    [Title] [NVarChar](30) NOT NULL,
    [Content] [NVarChar](500) NOT NULL,
    [Dato] Datetime Not NULL Default Getdate(),
    [UserId] [Int] NOT NULL References Users(UserId),
    [CategoryId] [Int] NOT NULL References Category(CategoryId),
)
GO

------------------------------------------------------------------------------------------------------------
-- Create Sample Data

Insert Into Category(CategoryName)
Values('Default'),
('meh'),
('yeet');
GO

Insert Into Users(Username, Password)
Values('DefultUser','meh'),
('Peter','meh'),
('Nicolai','meh'),
('Casper','meh');

GO

Insert Into [Messages](Title, Content, UserId, CategoryId)
Values('Default', 'Default', 1, 1),
('meh', 'meh', 2, 2),
('Hacking', 'Ha Yeah like im gonna show you!', 3, 3),
('Toxisety', '***** ******* *******', 4, 2),
('Yeet', 'yeet', 1, 3);
GO


------------------------------------------------------------------------------------------------------------
--Category CRUD

--Create
Create Procedure CreateCategory
@NewCategoryName VarChar(100)
AS
IF NOT EXISTS (SELECT CategoryName FROM Category WHERE CategoryName = @NewCategoryName)
BEGIN

    INSERT INTO Category (CategoryName)
    VALUES (@NewCategoryName);

END;
GO

--ReadAll
Create Procedure ReadCategorys
As
Select *
From Category
GO

--GetoneCategory
Create Procedure ReadOneCategory
@CategoryId int
As
Select *
From Category
Where CategoryId = @CategoryId
GO

--Update
Create Procedure UpdateCategorys
@CategoryRename VarChar(100),
@CategoryId Int
As
UPDATE Category 
SET CategoryName = @CategoryRename 
WHERE CategoryID = @CategoryId
GO


--Delete
Create Procedure DeleteCategorys
@CategoryId Int
As
Delete Category WHERE CategoryID = @CategoryId
Go


------------------------------------------------------------------------------------------------------------
-- User CRUD

--Create
Create Procedure CreateUser
@Username VarChar(30),
@Password VarChar(60),
@UserId int out
AS
Insert Into Users (Username, Password)
Values(@Username, @Password);

set @UserId = SCOPE_IDENTITY()
GO

--ReadAll
Create Procedure ReadUsers
As
Select UserId, Username
From Users
GO

--ReadOneUser
Create Procedure ReadOneUser
@UserId int
As
Select *
From Users
Where UserId = @UserId
GO

--ReadOneUserWithPassword
Create Procedure ReadOneUserwithPass
@Username VarChar(30),
@Password VarChar(60)
As
Select *
From Users
Where Username = @Username And Password = @Password
GO

--Update
Create Procedure UpdateUsers
@UpdateUsername VarChar(30),
@UpdatePassword VarChar(60),
@UserId Int
As
UPDATE Users 
SET Username = @UpdateUsername, Password = @UpdatePassword
WHERE UserId = @UserId
GO

--Delete
Create Procedure DeleteUser
@UserId Int
As
Delete Users WHERE UserId = @UserId
Go


------------------------------------------------------------------------------------------------------------
--Message CRUD

--Create
CREATE Procedure CreateMessage
@Title VarChar(30),
@Content VarChar(500),
@UserId Int,
@CategoryId Int,
@MessageId int out
AS
Insert Into [Messages] (Title, Content, UserId, CategoryId)
Values(@Title, @Content, @UserId, @CategoryId);

set @MessageId = SCOPE_IDENTITY()
GO

--ReadAll
Create Procedure ReadMessages
As
Select *
From [Messages]
GO

--Update
Create Procedure UpdateMessage
@Title VarChar(30),
@Content VarChar(500),
@MessageId Int,
@CategoryId int
As
UPDATE [Messages] 
SET Title = @Title, Content = @Content, CategoryId = @CategoryId
WHERE MessageId = @MessageId
GO

--Delete
Create Procedure DeleteMessage
@MessageId Int
As
Delete [Messages] WHERE MessageId = @MessageId
Go

--Read Messages in category
Create Procedure GetCategoryMessage
@CategoryId Int
As
Select *
From [Messages]
Where CategoryId = @CategoryId
GO

--Read top 10
Create Procedure GetTopTenMessages
As
Select Top 10 *
From [Messages]
go


Create Procedure GetMessage
@GetMessageId int
As
Select *
From [Messages]
Where [MessageId] = @GetMessageId
GO


Create Procedure GetMessageFromUser
@UserId int
As
Select *
From [Messages]
Where UserId = @UserId
GO

------------------------------------------------------------------------------------------------------------

