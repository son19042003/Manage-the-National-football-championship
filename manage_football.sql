if not exists (select * from sys.databases where name = 'FootballManagement')
begin
	create database FootballManagement
end

use [FootballManagement]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Club](
	[clubId] char(10) NOT NULL,
	[clubName] varchar(100) NOT NULL,
	[shortName] char(5) NOT NULL,
	[stadium] varchar(100) NOT NULL,
	[logo] varchar(255),
	[linkFb] varchar(255),
	[linkIg] varchar(255),
 CONSTRAINT [PK_Club] PRIMARY KEY CLUSTERED 
(
	[clubId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Club]
ADD [isActive] BIT NOT NULL DEFAULT 1;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[playerId] int IDENTITY(1,1) NOT NULL,
	[playerName] varchar(100) NOT NULL,
	[birthday] date NOT NULL,
	[height] float NOT NULL,
	[nationality] varchar(50) NOT NULL,
	[typePlayer] bit,
	[position] varchar(50) NOT NULL,
	[number] int NOT NULL,
	[goals] int default 0,
	[avatar] varchar(255),
	[linkFb] varchar(255),
	[linkIg] varchar(255),
	[clubId] char(10),
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[playerId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Players]
DROP COLUMN [playerName]
GO
ALTER TABLE [dbo].[Players]
ADD [firstName] varchar(50) NOT NULL,
	[lastName] varchar(50) NOT NULL
GO
ALTER TABLE [dbo].[Players]
ALTER COLUMN [clubId] char(10) NOT NULL

ALTER TABLE [dbo].[Players]
ADD CONSTRAINT FK_Players_Club
FOREIGN KEY ([clubId])
REFERENCES [dbo].[Club]([clubId]);

GO
ALTER TABLE [dbo].[Players]
ADD [isInClub] BIT NOT NULL DEFAULT 1;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matches](
	[matchId] int IDENTITY(1,1) NOT NULL,
	[round] int NOT NULL,
	[timeStart] time NOT NULL,
	[dateStart] date NOT NULL,
	[homeTeam] char(10) NOT NULL,
	[awayTeam] char(10) NOT NULL,
	[goalsH] int default 0,
	[goalsA] int default 0,
	[timeGoal] time,
	[typeGoal] varchar(50),
	[status] varchar(50) NOT NULL,
	[playerId] int,
 CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED 
(
	[matchId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Goals] (
    goalId INT IDENTITY(1,1) PRIMARY KEY,
    matchId INT NOT NULL,
    playerId INT NOT NULL,
    teamId CHAR(10) NOT NULL,
    timeScored VARCHAR(12) NOT NULL,
    typeGId INT NOT NULL,
    FOREIGN KEY (matchId) REFERENCES Matches(matchId),
    FOREIGN KEY (playerId) REFERENCES Players(playerId),
    FOREIGN KEY (teamId) REFERENCES Club(clubId),
    FOREIGN KEY (typeGId) REFERENCES TypeGoal(typeGId)
);

ALTER TABLE Goals ADD GoalIndex INT NOT NULL DEFAULT 1;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Standing](
	[standingId] int IDENTITY(1,1) NOT NULL,
	[clubId] char(10) NOT NULL,
	[played] int default 0,
	[won] int default 0,
	[drawn] int default 0,
	[lost] int default 0,
	[goalF] int default 0,
	[goalA] int default 0,
	[points] int default 0,
 CONSTRAINT [PK_Standing] PRIMARY KEY CLUSTERED 
(
	[standingId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Standing]
ADD CONSTRAINT FK_Standing_Club
FOREIGN KEY ([clubId])
REFERENCES [dbo].[Club]([clubId]);

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeGoal](
	[typeGId] int IDENTITY(1,1) NOT NULL,
	[typeGName] varchar(50),
	[typeGDes] varchar(255),
 CONSTRAINT [PK_TypeGoal] PRIMARY KEY CLUSTERED 
(
	[typeGId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleId] int IDENTITY(1,1) NOT NULL,
	[roleName] varchar(50) NOT NULL,
	[roleDes] varchar(255),
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[accountId] int IDENTITY(1,1) NOT NULL,
	[username] varchar(50) NOT NULL,
	[password] varchar(255) NOT NULL,
	[email] varchar(100) NOT NULL UNIQUE,
	[phoneNum] varchar(20) NOT NULL,
	[firstName] varchar(50) NOT NULL,
	[lastName] varchar(50) NOT NULL,
	[gender] char(10) NOT NULL,
	[dateOfBirth] date NOT NULL,
	[avatar] varchar(255),
	[roleId] int NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[accountId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Account]
ADD [isActive] BIT NOT NULL DEFAULT 1;

ALTER TABLE [dbo].[Account]
ALTER COLUMN [gender] CHAR(15) NOT NULL;

GO
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT FK_Account_Role
FOREIGN KEY ([roleId])
REFERENCES [dbo].[Role]([roleId]);

ALTER TABLE [dbo].[Account] ADD [randomKey] VARCHAR(255);
ALTER TABLE [dbo].[Account] ADD [resetKeyExpires] DATETIME;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeNews](
	[typeNewsId] int IDENTITY(1,1) NOT NULL,
	[typeNewsName] varchar(50) NOT NULL,
	[typeNewsDes] varchar(255),
 CONSTRAINT [PK_TypeNews] PRIMARY KEY CLUSTERED 
(
	[typeNewsId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[newsId] int IDENTITY(1,1) NOT NULL,
	[title] varchar(255) NOT NULL,
	[image] varchar(255) NOT NULL,
	[imgContent] varchar(255),
	[content] text NOT NULL,
	[dateU] datetime NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[newsId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[News]
ADD [typeNewsId] int NOT NULL;

ALTER TABLE [dbo].[News]
ADD CONSTRAINT FK_News_TypeNews
FOREIGN KEY ([typeNewsId])
REFERENCES [dbo].[TypeNews]([typeNewsId]);

ALTER TABLE [dbo].[News]
ADD [Status] bit NOT NULL default 1;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rule](
	[ruleId] int IDENTITY(1,1) NOT NULL,
	[minAge] int NOT NULL,
	[maxForeignPlayers] int NOT NULL,
	[minPlayer] int NOT NULL,
	[maxPlayer] int NOT NULL,
 CONSTRAINT [PK_Rule] PRIMARY KEY CLUSTERED 
(
	[ruleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]