USE [master]
GO
/****** Object:  Database [MoviesPPPK]    Script Date: 01/01/2022 12:05:01 ******/
CREATE DATABASE [MoviesPPPK]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MoviesPPPK', FILENAME = N'/var/opt/mssql/data/MoviesPPPK.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MoviesPPPK_log', FILENAME = N'/var/opt/mssql/data/MoviesPPPK_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MoviesPPPK] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MoviesPPPK].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
USE [MoviesPPPK]
GO
/****** Object:  Table [dbo].[Actor]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actor](
	[IDActor] [int] NOT NULL,
	[Name] [nvarchar](90) NULL,
	[MovieID] [int] NOT NULL,
 CONSTRAINT [PK_Actor] PRIMARY KEY CLUSTERED 
(
	[IDActor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[IDMovie] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](90) NULL,
	[Description] [nvarchar](900) NULL,
	[Picture] [varbinary](max) NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[IDMovie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Actor]  WITH CHECK ADD  CONSTRAINT [FK_Actor_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([IDMovie])
GO
ALTER TABLE [dbo].[Actor] CHECK CONSTRAINT [FK_Actor_Movie]
GO

USE [MoviesPPPK]
GO

/****** Object:  Table [dbo].[Director]    Script Date: 01/01/2022 15:33:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Director](
	[IDDirector] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](90) NULL,
	[MovieID] [int] NOT NULL,
 CONSTRAINT [PK_Director] PRIMARY KEY CLUSTERED 
(
	[IDDirector] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Director]  WITH CHECK ADD  CONSTRAINT [FK_Director_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([IDMovie])
GO

ALTER TABLE [dbo].[Director] CHECK CONSTRAINT [FK_Director_Movie]
GO
/****** Object:  StoredProcedure [dbo].[AddMovie]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddMovie]
	@Name NVARCHAR(90),
	@Description NVARCHAR(900),
	@Picture VARBINARY(MAX),

	@IdMovie INT OUTPUT
AS 
BEGIN 
	INSERT INTO Movie VALUES(@Name, @Description, @Picture)
	SET @IdMovie = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[createMovie]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[createMovie]
	@Name NVARCHAR(90),
	@Description NVARCHAR(900),

	@Id INT OUTPUT
AS 
BEGIN 
	INSERT INTO Movie VALUES(@Name, @Description)
	SET @Id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[deleteMovie]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteMovie]
	@IdMovie INT	 
AS 
BEGIN 
        DELETE FROM Actor WHERE MovieID = @IdMovie
        DELETE FROM Director WHERE MovieID = @IdMovie

	DELETE  
	FROM 
			Movie
	WHERE 
		IDMovie = @IdMovie
END
GO
/****** Object:  StoredProcedure [dbo].[GetMovie]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMovie]
	@IdMovie INT
AS 
BEGIN 
	SELECT 
		* 
	FROM 
		Movie
	WHERE 
		IDMovie = @IdMovie
END
GO
/****** Object:  StoredProcedure [dbo].[GetMovies]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMovies]
AS 
BEGIN 
	SELECT * FROM Movie
END
GO
/****** Object:  StoredProcedure [dbo].[selectMovie]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[selectMovie]
	@IdMovie INT
AS 
BEGIN 
	SELECT 
		* 
	FROM 
		Movie
	WHERE 
		IDMovie = @IdMovie
END
GO
/****** Object:  StoredProcedure [dbo].[selectMovies]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[selectMovies]
AS 
BEGIN 
	SELECT * FROM Movie
END
GO
/****** Object:  StoredProcedure [dbo].[updateMovie]    Script Date: 01/01/2022 12:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateMovie]
	@Name NVARCHAR(90),
	@Description NVARCHAR(900),
	@IdMovie INT,
	@Picture VARBINARY(MAX)
	 
AS 
BEGIN 
	UPDATE Movie SET 
		Name = @Name,
		Description = @Description,
		Picture = @Picture
	WHERE 
		IDMovie = @IdMovie
END
GO

CREATE PROCEDURE [dbo].[addActor]
	@Name NVARCHAR(90),
	@MovieID int,

	@Id INT OUTPUT
AS 
BEGIN 
	INSERT INTO Actor VALUES(@Name, @MovieID)
	SET @Id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[deleteActor]
	@IdActor INT	 
AS 
BEGIN 
	DELETE  
	FROM 
			Actor
	WHERE 
		IDActor = @IdActor
END
GO

CREATE PROCEDURE [dbo].[getActor]
	@IdActor INT
AS 
BEGIN 
	SELECT 
		* 
	FROM 
		Actor
	WHERE 
		IDActor = @IdActor
END
GO

CREATE PROCEDURE [dbo].[getActors]
        @MovieId INT
AS 
BEGIN 
	SELECT * FROM Actor WHERE MovieId = @MovieId
END
GO


CREATE PROCEDURE [dbo].[updateActor]
	@Name NVARCHAR(90),
	@MovieID int,

	@IdActor INT 
AS 
BEGIN 
	UPDATE Actor SET 
		Name = @Name,
		MovieID = @MovieID
	
	WHERE 
		IDActor = @IdActor
END
GO

CREATE PROCEDURE [dbo].[addDirector]
	@Name NVARCHAR(90),
	@MovieID int,

	@Id INT OUTPUT
AS 
BEGIN 
	INSERT INTO Director VALUES(@Name, @MovieID)
	SET @Id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[deleteDirector]
	@IdDirector INT	 
AS 
BEGIN 
	DELETE  
	FROM 
			Director
	WHERE 
		IDDirector = @IdDirector
END
GO

CREATE PROCEDURE [dbo].[getDirector]
	@IdDirector INT
AS 
BEGIN 
	SELECT 
		* 
	FROM 
		Director
	WHERE 
		IDDirector = @IdDirector
END
GO

CREATE PROCEDURE [dbo].[getDirectors]
        @MovieId INT
AS 
BEGIN 
	SELECT * FROM Director WHERE MovieId = @MovieId
END
GO


CREATE PROCEDURE [dbo].[updateDirector]
	@Name NVARCHAR(90),
	@MovieID int,

	@IdDirector INT 
AS 
BEGIN 
	UPDATE Director SET 
		Name = @Name,
		MovieID = @MovieID
	
	WHERE 
		IDDirector = @IdDirector
END
GO

USE [master]
GO
ALTER DATABASE [MoviesPPPK] SET  READ_WRITE 
GO
