USE [ConnectUO]
GO
/****** Object:  Table [dbo].[BanType]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BanType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[ShardId] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Path] [nvarchar](255) NOT NULL,
	[GlassOverlay] [bit] NOT NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[ShardId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BanList]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BanList](
	[BanId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Reason] [text] NULL,
	[ByUserId] [int] NULL,
	[ByUsername] [varchar](50) NULL,
	[DateBanned] [datetime] NULL,
	[LiftDate] [int] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_BanList] PRIMARY KEY CLUSTERED 
(
	[BanId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Era]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Era](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](8) NOT NULL,
 CONSTRAINT [PK_Era] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OnlineCounts]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlineCounts](
	[Date] [datetime] NOT NULL,
	[Count] [int] NOT NULL,
	[ShardId] [int] NOT NULL,
	[Down] [bit] NOT NULL,
 CONSTRAINT [PK_OnlineCounts_1] PRIMARY KEY CLUSTERED 
(
	[Date] ASC,
	[ShardId] ASC,
	[Down] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogTypes]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LogTypes](
	[id] [int] NOT NULL,
	[description] [varchar](20) NULL,
 CONSTRAINT [PK_LogTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Username] [varchar](100) NULL,
	[Message] [text] NULL,
	[LogDate] [datetime] NULL,
	[IPAddress] [char](15) NULL,
	[LogType] [int] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Lang]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lang](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](8) NOT NULL,
 CONSTRAINT [PK_Lang] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[IsLeapYear]    Script Date: 11/03/2009 01:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[IsLeapYear]
(
	@p_year int
)
RETURNS bit
AS
BEGIN

	DECLARE @p_leap_date SMALLDATETIME
    DECLARE @p_check_day TINYINT
 
    SET @p_leap_date = CONVERT(VARCHAR(4), @p_year) + '0228'
    SET @p_check_day = DATEPART(d, DATEADD(d, 1, @p_leap_date))
    IF (@p_check_day = 29)
        RETURN 1

    RETURN 0  

END
GO
/****** Object:  UserDefinedFunction [dbo].[IsDown]    Script Date: 11/03/2009 01:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[IsDown]
(
	@Count INT
)
RETURNS BIT
AS
BEGIN
	DECLARE @Down AS BIT
	SELECT @Down = CASE WHEN @COunt = -1 THEN 1 ELSE 0 END
	RETURN @Down
END
GO
/****** Object:  Table [dbo].[Icon]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Icon](
	[ShardId] [int] NOT NULL,
	[Data] [image] NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Path] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ShardId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerRulesCategories]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerRulesCategories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_ServerRulesCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerRules]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerRules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Catagory] [int] NOT NULL,
	[RuleText] [nvarchar](1000) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_ServerRules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BetaTester]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BetaTester](
	[BetaId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserName] [varchar](255) NULL,
	[Email] [varchar](100) NULL,
	[DxDiag] [xml] NULL,
	[ClientVersion] [varchar](30) NULL,
	[Razor] [bit] NULL,
	[EmailedUser] [bit] NULL,
 CONSTRAINT [PK_BetaTester] PRIMARY KEY CLUSTERED 
(
	[BetaId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[GetUpTime]    Script Date: 11/03/2009 01:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetUpTime]
(
	@TotalCount FLOAT = NULL,
	@UpCount FLOAT = NULL
)
RETURNS FLOAT
AS
BEGIN
	DECLARE @UpTime AS FLOAT 
	
	IF(@UpCount IS NULL OR @TotalCount IS NULL OR @TotalCount = 0)
	BEGIN
		SET @UpTime = 0
	END
	ELSE
	BEGIN
		SET @UpTime = (1 - (@UpCount / @TotalCount))
	END	
	
	RETURN @UpTime
END
GO
/****** Object:  Table [dbo].[ShardPlayStatistics]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShardPlayStatistics](
	[UserId] [uniqueidentifier] NOT NULL,
	[ShardId] [int] NOT NULL,
	[CreateOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShardPatch]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShardPatch](
	[PatchUrl] [varchar](255) NOT NULL,
	[ShardId] [int] NOT NULL,
	[PatchId] [int] IDENTITY(1,1) NOT NULL,
	[UpdateOn] [datetime] NOT NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_ShardPatch] PRIMARY KEY CLUSTERED 
(
	[ShardId] ASC,
	[PatchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccessLevel]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccessLevel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_access_level] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Type]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](8) NOT NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Test]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Status](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](25) NOT NULL,
	[RequiresModApproval] [bit] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Version]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Version](
	[Version] [nvarchar](50) NOT NULL,
	[UpdateUrl] [nvarchar](255) NOT NULL,
	[ChangeLog] [nvarchar](max) NULL,
	[Date] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsageStats]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsageStats](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LastSeenInUse] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
 CONSTRAINT [PK_UsageStats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsageStatistics]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsageStatistics](
	[UserId] [nvarchar](36) NOT NULL,
	[Version] [nvarchar](50) NULL,
	[UpdateOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_Statistics] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwOnlineCounts]    Script Date: 11/03/2009 01:18:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwOnlineCounts]
AS
SELECT     oc.ShardId, CASE WHEN ISNULL(counts.Average, 0) = - 1 THEN 0 ELSE ISNULL(counts.Average, 0) END AS AvgOnline, 
                      CASE WHEN oc.ShardId = 1 THEN 7984 WHEN ISNULL(counts.Maximum, 0) = - 1 THEN 0 ELSE ISNULL(counts.Maximum, 0) END AS MaxOnline, 
                      CASE WHEN ISNULL(oc.Count, 0) = - 1 THEN 0 ELSE ISNULL(oc.Count, 0) END AS CurOnline, uptime.UpTime
FROM         dbo.OnlineCounts AS oc INNER JOIN
                          (SELECT     ShardId, dbo.GetUpTime(COUNT(1), SUM(CAST(Down AS INT))) AS UpTime
                            FROM          dbo.OnlineCounts
                            GROUP BY ShardId) AS uptime ON oc.ShardId = uptime.ShardId INNER JOIN
                          (SELECT     ShardId, MAX(Date) AS Date
                            FROM          dbo.OnlineCounts AS OnlineCounts_2
                            GROUP BY ShardId) AS maxDate ON oc.ShardId = maxDate.ShardId AND oc.Date = maxDate.Date INNER JOIN
                          (SELECT     ShardId, AVG(Count) AS Average, MAX(Count) AS Maximum
                            FROM          dbo.OnlineCounts AS OnlineCounts_1
                            GROUP BY ShardId) AS counts ON oc.ShardId = counts.ShardId
GROUP BY oc.ShardId, ISNULL(counts.Average, 0), ISNULL(counts.Maximum, 0), ISNULL(oc.Count, 0), uptime.UpTime
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "oc"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 276
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "uptime"
            Begin Extent = 
               Top = 6
               Left = 278
               Bottom = 84
               Right = 445
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "maxDate"
            Begin Extent = 
               Top = 6
               Left = 483
               Bottom = 84
               Right = 650
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "counts"
            Begin Extent = 
               Top = 6
               Left = 688
               Bottom = 99
               Right = 855
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 3885
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwOnlineCounts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwOnlineCounts'
GO
/****** Object:  View [dbo].[vwInstallationStatistics]    Script Date: 11/03/2009 01:18:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwInstallationStatistics]
AS
SELECT     (SELECT     COUNT(UserId) AS Expr1
                       FROM          dbo.UsageStatistics
                       WHERE      (CreatedOn > DATEADD(HOUR, - 1, GETDATE()))) AS LastHour,
                          (SELECT     COUNT(UserId) AS Expr1
                            FROM          dbo.UsageStatistics AS UsageStatistics_5
                            WHERE      (CreatedOn > DATEADD(DAY, - 1, GETDATE()))) AS LastDay,
                          (SELECT     COUNT(UserId) AS Expr1
                            FROM          dbo.UsageStatistics AS UsageStatistics_4
                            WHERE      (CreatedOn > DATEADD(DAY, - 7, GETDATE()))) AS LastWeek,
                          (SELECT     COUNT(UserId) AS Expr1
                            FROM          dbo.UsageStatistics AS UsageStatistics_3
                            WHERE      (CreatedOn > DATEADD(DAY, - 30, GETDATE()))) AS LastMonth,
                          (SELECT     COUNT(UserId) AS Expr1
                            FROM          dbo.UsageStatistics AS UsageStatistics_2
                            WHERE      (CreatedOn > DATEADD(MONTH, - 6, GETDATE()))) AS LastSixMonths,
                          (SELECT     COUNT(UserId) AS Expr1
                            FROM          dbo.UsageStatistics AS UsageStatistics_1
                            WHERE      (CreatedOn > DATEADD(YEAR, - 1, GETDATE()))) AS LastYear
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwInstallationStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwInstallationStatistics'
GO
/****** Object:  View [dbo].[vwInstallationsLastHour]    Script Date: 11/03/2009 01:18:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwInstallationsLastHour]
AS
SELECT     COUNT(UserId) AS Count
FROM         dbo.UsageStatistics
WHERE     (CreatedOn > DATEADD(HOUR, - 1, GETDATE()))
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UsageStatistics"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 205
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwInstallationsLastHour'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwInstallationsLastHour'
GO
/****** Object:  StoredProcedure [dbo].[UpdateShardIcon]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateShardIcon] 
	@ShardID int,
	@Path nvarchar(255),
	@ImageData image,
	@Width int,
	@Height int
AS
BEGIN
	SET NOCOUNT ON;
	
	IF EXISTS ( SELECT [ShardId] FROM [Icon] WHERE [ShardId] = @ShardID )
	BEGIN	
	  UPDATE [Icon]
		SET 
			[Data] = @ImageData,
			[Width] = @Width,
			[Height] = @Height,
			[Path] = @Path
		WHERE
			[ShardId] = @ShardID
	END
	ELSE	    
	BEGIN
	
		INSERT INTO [Icon] ( [ShardId], [Data], [Width], [Height], [Path] )
		VALUES ( @ShardID, @ImageData, @Width, @Height, @Path );
	
	END

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateShardBannerSettings]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateShardBannerSettings] 
	@ShardID int,
	@GlassOverlay bit
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE [Banner]
		SET 
			[GlassOverlay] = @GlassOverlay
		WHERE
			[ShardId] = @ShardID

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateShardBanner]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateShardBanner] 
	@ShardID int,
	@Path nvarchar(255),
	@Width int,
	@Height int,
	@GlassOverlay bit
AS
BEGIN
	SET NOCOUNT ON;
	
	IF EXISTS ( SELECT [ShardId] FROM [Banner] WHERE [ShardId] = @ShardID )
	BEGIN	
	  UPDATE [Banner]
		SET 
			[Width] = @Width,
			[Height] = @Height,
			[Path] = @Path,
			[GlassOverlay] = @GlassOverlay
		WHERE
			[ShardId] = @ShardID
	END
	ELSE	    
	BEGIN
	
		INSERT INTO [Banner] ( [ShardId], [Width], [Height], [Path], [GlassOverlay] )
		VALUES ( @ShardID, @Width, @Height, @Path, @GlassOverlay );
	
	END

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVersionStats]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVersionStats]
	 @Guid nvarchar(36)
	,@Version nvarchar(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS( SELECT UserId FROM [UsageStatistics] WHERE UserId = @Guid)
	BEGIN
		INSERT INTO [UsageStatistics]
		SELECT @Guid, @Version, GETDATE(), GETDATE()
	END
	ELSE
	BEGIN
		UPDATE [UsageStatistics]
		SET UpdateOn = GETDATE()
		   ,Version = @Version
		WHERE UserId = @Guid
	END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePlayStatistics]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePlayStatistics]
	 @Guid nvarchar(36)
	,@ShardId int
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO ShardPlayStatistics
	SELECT @Guid, @ShardId, GETDATE()
END
GO
/****** Object:  Table [dbo].[Shard]    Script Date: 11/03/2009 01:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[Era] [int] NOT NULL,
	[ShardType] [int] NOT NULL,
	[Lang] [int] NOT NULL,
	[RemoveEncryption] [bit] NOT NULL,
	[AllowRazor] [bit] NOT NULL,
	[HostAddress] [nvarchar](255) NOT NULL,
	[Port] [int] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ServerClientVersion] [nvarchar](15) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[LastError] [nvarchar](2000) NULL,
 CONSTRAINT [PK_Shard_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetPatches]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPatches]
		@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT PatchUrl, Version FROM ShardPatch WHERE ShardId = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[GetLatestVersion]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLatestVersion] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 * FROM dbo.[Version] ORDER BY [Version] DESC

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllVersions]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllVersions] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.[Version] ORDER BY [Version] DESC

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPatches]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllPatches]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ShardId, PatchUrl, Version FROM ShardPatch 

END
GO
/****** Object:  StoredProcedure [dbo].[GetServerRules]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetServerRules] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT s.RuleText, cat.CategoryId FROM [ServerRules] s
	JOIN [ServerRulesCategories] cat ON cat.CategoryId = s.Catagory
	ORDER BY cat.DisplayOrder ASC, s.DisplayOrder ASC;

END
GO
/****** Object:  StoredProcedure [dbo].[BanUser]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benjamin Willard
-- Create date: 10/17/2009
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[BanUser] 
	-- Add the parameters for the stored procedure here
	@UserID int, 
	@ByUserId int,
	@ByUserName varchar(255),
	@Reason text,
	@Type int = 1,
	@Duration int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [BanList]
	(
		[UserId],
		[ByUserId],
		[ByUsername],
		[Type],
		[Reason],
		[LiftDate],
		[DateBanned]
	)
	VALUES
	(
		@UserID,
		@ByUserId,
		@ByUserName,
		@Type,
		@Reason,
		@Duration,
		GETDATE()
	);
	
END
GO
/****** Object:  StoredProcedure [dbo].[SiteRemoveShardPatch]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SiteRemoveShardPatch] 
	@ShardID int,
	@PatchID int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [ShardPatch] WHERE [PatchId] = @PatchID AND [ShardId] = @ShardID;
	
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetUpTimePastThirtyDays]    Script Date: 11/03/2009 01:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetUpTimePastThirtyDays]
(
	@ShardId INT
)
RETURNS FLOAT
AS
BEGIN
	DECLARE @UpTime AS FLOAT 
	
	SELECT @UpTime = (1 - dbo.[GetUpTime](COUNT(1), SUM(CASE WHEN Down = 0 THEN 1 ELSE 0 END)))
	FROM OnlineCounts
	WHERE [Date] BETWEEN (GETDATE() - 30) AND GETDATE()
	AND ShardId = @ShardId
	
	RETURN @UpTime
END
GO
/****** Object:  StoredProcedure [dbo].[EnsureStatisticsUser]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EnsureStatisticsUser]
	 @Guid nvarchar(36)
	,@Version nvarchar(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS( SELECT UserId FROM [UsageStatistics] WHERE UserId = @Guid)
	BEGIN
		INSERT INTO [UsageStatistics]
		SELECT @Guid, @Version
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddLog]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddLog]
	-- Add the parameters for the stored procedure here
	@UserId int,
	@Username varchar(255),
	@Message text,
	@IPAddress char(15),
	@LogType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO
		[Logs]
		(
			[UserId],
			[Username],
			[Message],
			[LogDate],
			[IPAddress],
			[LogType]
		)
	VALUES
		(
			@UserId,
			@Username,
			@Message,
			GETDATE(),
			@IPAddress,
			@LogType
		);
    
END
GO
/****** Object:  StoredProcedure [dbo].[AddBetaTester]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benjamin Willard
-- Create date: 10/17/2009
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AddBetaTester] 
	-- Add the parameters for the stored procedure here
	@UserID int, 
	@UserName varchar(255),
	@Email varchar(255),
	@DxDiag xml,
	@ClientVersion varchar(30),
	@Razor bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [BetaTester]
	(
		[UserId],
		[UserName],
		[Email],
		[DxDiag],
		[ClientVersion],
		[Razor]
	)
	VALUES
	(
		@UserID,
		@UserName,
		@Email,
		@DxDiag,
		@ClientVersion,
		@Razor
	);
	
END
GO
/****** Object:  StoredProcedure [dbo].[AddShardPatch]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddShardPatch] 
	@PatchUrl nvarchar(255),
	@ShardID int
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [ShardPatch] 
	( 
		[PatchUrl], 
		[ShardId], 
		[UpdateOn],
		[Version] 
	)
	VALUES
	(
		@PatchUrl,
		@ShardID,
		GETDATE(),
		1
	)
	
END
GO
/****** Object:  StoredProcedure [dbo].[AddShard]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddShard] 
	@UserID int,
	@Name nvarchar(255),
	@Description nvarchar(max),
	@Url nvarchar(255),
	@Era int,
	@ShardType int,
	@Lang int,
	@RemoveEncryption bit,
	@AllowRazor bit,
	@HostAddress nvarchar(255),
	@Port int,
	@ServerClientVersion nvarchar(15)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Shard] 
	( 
		[UserId], 
		[Name], 
		[Description],
		[Url],
		[Era],
		[ShardType],
		[Lang],
		[RemoveEncryption],
		[AllowRazor],
		[HostAddress],
		[Port],
		[LastUpdate],
		[Status],
		[ServerClientVersion],
		[CreatedOn]
	)
	VALUES
	(
		@UserID,
		@Name,
		@Description,
		@Url,
		@Era,
		@ShardType,
		@Lang,
		@RemoveEncryption,
		@AllowRazor,
		@HostAddress,
		@Port,
		GETDATE(),
		1,
		@ServerClientVersion,
		GETDATE()
	);	
	
	SELECT @@IDENTITY AS 'ShardID';
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveShard]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RemoveShard]
	-- Add the parameters for the stored procedure here
	@ShardID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM [Banner] WHERE [ShardId] = @ShardID;
	DELETE FROM [Icon] WHERE [ShardId] = @ShardID;
	DELETE FROM [OnlineCounts] WHERE [ShardId] = @ShardID;
	DELETE FROM [ShardPatch] WHERE [ShardId] = @ShardID;
	DELETE FROM [Shard] WHERE [Id] = @ShardID;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateOnlineCount]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateOnlineCount]
	 @Id INT
	,@Count INT
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Down AS BIT
	
	SELECT @Down = CASE WHEN @Count = -1 THEN 1 ELSE 0 END

	INSERT INTO OnlineCounts
	(
		 [ShardId]
		,[Date]
		,[Count]
		,[Down]
	)
	VALUES
	(
		 @Id
		,GETDATE()
		,@Count
		,@Down
	)
	
	--Set the server as inactive 
	--if the uptime 0% across the past 30 days
	UPDATE Shard 
	SET [Status] = 6 
	WHERE Id = @Id
	AND dbo.GetUpTimePastThirtyDays(@Id) = 0
	AND (
		SELECT COUNT(ShardId) 
		FROM OnlineCounts
		WHERE ShardId = @Id
		AND [Date] BETWEEN (GETDATE() - 30) AND GETDATE()
	) > 1000
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateShardStatus]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateShardStatus]
	 @Id INT
	,@Status INT
	,@Exception NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Shard
	SET [Status] = @Status
	   ,[LastError] = @Exception
	WHERE Id = @Id
	AND [Status] IN (1, 5)
END
GO
/****** Object:  StoredProcedure [dbo].[SiteUpdateShardStatus]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SiteUpdateShardStatus]
	 @ShardId INT,
	 @Status INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Shard
	SET [Status] = @Status
	WHERE Id = @ShardId
	
END
GO
/****** Object:  StoredProcedure [dbo].[SiteUpdateShard]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SiteUpdateShard] 
	@ShardID int,
	@Name nvarchar(255),
	@Description nvarchar(max),
	@Url nvarchar(255),
	@Era int,
	@ShardType int,
	@Lang int,
	@RemoveEncryption bit,
	@AllowRazor bit,
	@HostAddress nvarchar(255),
	@Port int,
	@ServerClientVersion nvarchar(15),
	@Status int,
	@RemoveBanner bit,
	@RemoveIcon bit
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [Shard]
	SET
		Name = @Name,
		Description = @Description,
		Url = @Url,
		Era = @Era,
		ShardType = @ShardType,
		Lang = @Lang,
		RemoveEncryption = @RemoveEncryption,
		AllowRazor = @AllowRazor,
		HostAddress = @HostAddress,
		Port = @Port,
		ServerClientVersion = @ServerClientVersion,
		LastUpdate = GETDATE(),
		Status = @Status
	WHERE
		[Id] = @ShardID;		
		
	  DELETE FROM [Banner] WHERE [Banner].[ShardId] = @ShardID AND 1 = @RemoveBanner;
	  DELETE FROM [Icon] WHERE [Icon].[ShardId] = @ShardID AND 1 = @RemoveIcon;
	
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateShard]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateShard] 
	@ShardID int,
	@Name nvarchar(255),
	@Description nvarchar(max),
	@Url nvarchar(255),
	@Era int,
	@ShardType int,
	@Lang int,
	@RemoveEncryption bit,
	@AllowRazor bit,
	@HostAddress nvarchar(255),
	@Port int,
	@ServerClientVersion nvarchar(15),
	@Inactive bit
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ShardStatus INT;
	DECLARE @RequiresApproval BIT

	SELECT @ShardStatus = shard.[Status], 
		   @RequiresApproval = status.RequiresModApproval 
	FROM [Shard] shard
	JOIN [Status] status
	ON shard.[Status] = status.[Id]
	WHERE shard.[Id] = @ShardID
	
	UPDATE [Shard]
	SET
		Name = @Name,
		Description = @Description,
		Url = @Url,
		Era = @Era,
		ShardType = @ShardType,
		Lang = @Lang,
		RemoveEncryption = @RemoveEncryption,
		AllowRazor = @AllowRazor,
		HostAddress = @HostAddress,
		Port = @Port,
		ServerClientVersion = @ServerClientVersion,
		LastUpdate = GETDATE(),
		Status = 
		(
			CASE
				WHEN (@Inactive = 1) THEN 6
				WHEN (@Inactive = 0 AND @RequiresApproval = 0) THEN 1 
			    -- Shard is suspended therefore we need to mark it as needs approval
				WHEN (@RequiresApproval = 1) THEN 3
				ELSE @ShardStatus -- otherwise we will just set the status to what it was before
			END
		),
		LastError = ''
	WHERE
		[Id] = @ShardID;		
		
	SELECT [Status] FROM [Shard] WHERE [Id] = @ShardID; -- select the new 
		
END
GO
/****** Object:  View [dbo].[vwShards]    Script Date: 11/03/2009 01:18:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwShards]
AS
SELECT     TOP (100) PERCENT shard.Id, shard.Name, shard.Description, shard.Url, shard.Era, shard.ShardType, shard.Lang, shard.RemoveEncryption, shard.AllowRazor, 
                      shard.HostAddress, shard.Port, shard.Status, ISNULL(count.AvgOnline, 0) AS AvgOnline, ISNULL(count.MaxOnline, 0) AS MaxOnline, ISNULL(count.CurOnline, 0) 
                      AS CurOnline, ISNULL(count.UpTime, 0) AS UpTime, shard.LastUpdate, icon.Data, shard.ServerClientVersion, CASE WHEN
                          (SELECT     COUNT(PatchId)
                            FROM          ShardPatch
                            WHERE      ShardId = shard.Id) > 0 THEN 1 ELSE 0 END AS HasPatches, shard.LastError
FROM         dbo.Shard AS shard LEFT OUTER JOIN
                      dbo.vwOnlineCounts AS count ON shard.Id = count.ShardId LEFT OUTER JOIN
                      dbo.Icon AS icon ON shard.Id = icon.ShardId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "shard"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 321
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "count"
            Begin Extent = 
               Top = 6
               Left = 228
               Bottom = 278
               Right = 380
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "icon"
            Begin Extent = 
               Top = 6
               Left = 418
               Bottom = 108
               Right = 578
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 4815
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwShards'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwShards'
GO
/****** Object:  View [dbo].[vwPlayStatistics]    Script Date: 11/03/2009 01:18:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwPlayStatistics]
AS
SELECT     Name,
                          (SELECT     CASE WHEN COUNT(stats.[UserId]) IS NULL THEN 0 ELSE COUNT(stats.[UserId]) END AS PlayCount
                            FROM          dbo.ShardPlayStatistics AS stats
                            WHERE      (ShardId = shard.Id) AND (CreateOn > DATEADD(HOUR, - 1, GETDATE()))) AS LastHour,
                          (SELECT     CASE WHEN COUNT(stats.[UserId]) IS NULL THEN 0 ELSE COUNT(stats.[UserId]) END AS PlayCount
                            FROM          dbo.ShardPlayStatistics AS stats
                            WHERE      (ShardId = shard.Id) AND (CreateOn > DATEADD(DAY, - 1, GETDATE()))) AS LastDay,
                          (SELECT     CASE WHEN COUNT(stats.[UserId]) IS NULL THEN 0 ELSE COUNT(stats.[UserId]) END AS PlayCount
                            FROM          dbo.ShardPlayStatistics AS stats
                            WHERE      (ShardId = shard.Id) AND (CreateOn > DATEADD(DAY, - 6, GETDATE()))) AS LastWeek,
                          (SELECT     CASE WHEN COUNT(stats.[UserId]) IS NULL THEN 0 ELSE COUNT(stats.[UserId]) END AS PlayCount
                            FROM          dbo.ShardPlayStatistics AS stats
                            WHERE      (ShardId = shard.Id) AND (CreateOn > DATEADD(MONTH, - 1, GETDATE()))) AS LastMonth,
                          (SELECT     CASE WHEN COUNT(stats.[UserId]) IS NULL THEN 0 ELSE COUNT(stats.[UserId]) END AS PlayCount
                            FROM          dbo.ShardPlayStatistics AS stats
                            WHERE      (ShardId = shard.Id) AND (CreateOn > DATEADD(MONTH, - 6, GETDATE()))) AS LastSixMonths,
                          (SELECT     CASE WHEN COUNT(stats.[UserId]) IS NULL THEN 0 ELSE COUNT(stats.[UserId]) END AS PlayCount
                            FROM          dbo.ShardPlayStatistics AS stats
                            WHERE      (ShardId = shard.Id) AND (CreateOn > DATEADD(YEAR, - 1, GETDATE()))) AS LastYear,
                          (SELECT     CASE WHEN COUNT(stats.[UserId]) IS NULL THEN 0 ELSE COUNT(stats.[UserId]) END AS PlayCount
                            FROM          dbo.ShardPlayStatistics AS stats
                            WHERE      (ShardId = shard.Id)) AS AllTime
FROM         dbo.vwShards AS shard
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "shard"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPlayStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPlayStatistics'
GO
/****** Object:  StoredProcedure [dbo].[SiteGetServerListTotal]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SiteGetServerListTotal]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(*) FROM vwShards
	WHERE [Status] in (1, 5)
	
END
GO
/****** Object:  StoredProcedure [dbo].[SiteGetServerList]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SiteGetServerList]
	@StartIndex int,
	@Length int,
	@SortBy int,
	@Reverse bit
AS
BEGIN
	SET NOCOUNT ON;	
	
	IF @Reverse = 0
	BEGIN
		
		SELECT shards.*,		
		banner.[GlassOverlay], 
		banner.[Path] AS BannerPath, 
		icon.[Path] AS IconPath,
		era.description AS [EraText],
		lang.description AS [LangText],
		shardtype.description AS [ShardTypeText]			
		FROM
		(
			SELECT TOP 100 PERCENT *,
			ROW_NUMBER() OVER
			( ORDER BY Id )
			AS RN,
			ASCII(Name) AS [AsciiNum]
			FROM vwShards			
			WHERE [Status] in (1, 5)
			ORDER BY CASE @SortBy
			WHEN 1 THEN [Id]
			WHEN 2 THEN [Name]
			WHEN 3 THEN [Status]
			WHEN 4 THEN [ShardType]
			WHEN 5 THEN [Era]
			WHEN 6 THEN [Lang]
			WHEN 7 THEN [CurOnline]
			WHEN 8 THEN [AvgOnline]
			WHEN 9 THEN [MaxOnline]
			WHEN 10 THEN [Uptime] END	
			DESC
		) shards
		LEFT JOIN [Banner] banner ON banner.[ShardId] = [Id]
		LEFT JOIN [Icon] icon ON icon.[ShardId] = [Id]
		JOIN [Era] era ON era.id = shards.[Era]
		JOIN [Lang] lang ON lang.id = shards.[Lang]
		JOIN [Type] shardtype ON shardtype.id = shards.[ShardType]
		WHERE RN BETWEEN @StartIndex + 1 AND @StartIndex + @Length
		
	END
	ELSE
	BEGIN
	
		SELECT shards.*,		
		banner.[GlassOverlay], 
		banner.[Path] AS BannerPath, 
		icon.[Path] AS IconPath,
		era.description AS [EraText],
		lang.description AS [LangText],
		shardtype.description AS [ShardTypeText]			
		FROM
		(
			SELECT TOP 100 PERCENT *,
			ROW_NUMBER() OVER
			( ORDER BY Id )
			AS RN,
			ASCII(Name) AS [AsciiNum]
			FROM vwShards			
			WHERE [Status] in (1, 5)
			ORDER BY CASE @SortBy
			WHEN 1 THEN [Id]
			WHEN 2 THEN [Name]
			WHEN 3 THEN [Status]
			WHEN 4 THEN [ShardType]
			WHEN 5 THEN [Era]
			WHEN 6 THEN [Lang]
			WHEN 7 THEN [CurOnline]
			WHEN 8 THEN [AvgOnline]
			WHEN 9 THEN [MaxOnline]
			WHEN 10 THEN [Uptime] END	
			ASC
		) shards
		LEFT JOIN [Banner] banner ON banner.[ShardId] = [Id]
		LEFT JOIN [Icon] icon ON icon.[ShardId] = [Id]
		JOIN [Era] era ON era.id = shards.[Era]
		JOIN [Lang] lang ON lang.id = shards.[Lang]
		JOIN [Type] shardtype ON shardtype.id = shards.[ShardType]
		WHERE RN BETWEEN @StartIndex + 1 AND @StartIndex + @Length
		
	END;
		
END
GO
/****** Object:  StoredProcedure [dbo].[GetServerList]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetServerList]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id]
		  ,[Name]
		  ,[Description]
		  ,[Url]
		  ,[Era]
		  ,[ShardType]
		  ,[Lang]
		  ,[RemoveEncryption]
		  ,[AllowRazor]
		  ,[HostAddress]
		  ,[Port]
		  ,[Status]
		  ,[AvgOnline]
		  ,[MaxOnline]
		  ,[CurOnline]
		  ,[UpTime]
		  ,[Data]
		  ,[ServerClientVersion]
		  ,[HasPatches]
	FROM vwShards
	WHERE status in (1, 5)

END
GO
/****** Object:  StoredProcedure [dbo].[GetServerById]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec SELECT_ServerListById 3
CREATE PROCEDURE [dbo].[GetServerById]
		@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM vwShards
	WHERE Id = @Id
	AND [Status] <> 3
	AND [Status] <> 4

END
GO
/****** Object:  StoredProcedure [dbo].[AdminSiteGetServerList]    Script Date: 11/03/2009 01:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AdminSiteGetServerList]
	@StartIndex int,
	@Length int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT shards.*, 
	banner.[GlassOverlay], 
	banner.[Path] AS BannerPath, 
	icon.[Path] AS IconPath
	FROM
	(
		SELECT
			*,
			ROW_NUMBER() OVER
			( ORDER BY [Name] ASC )
			AS RN
			FROM vwShards
	) shards
	LEFT JOIN [Banner] banner ON banner.[ShardId] = [Id]
	LEFT JOIN [Icon] icon ON icon.[ShardId] = [Id]
	WHERE RN BETWEEN @StartIndex + 1 AND @StartIndex + @Length
	ORDER BY shards.[Name] ASC
	
END
GO
/****** Object:  Default [DF_Banner_Width]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_Width]  DEFAULT ((600)) FOR [Width]
GO
/****** Object:  Default [DF_Banner_Height]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_Height]  DEFAULT ((100)) FOR [Height]
GO
/****** Object:  Default [DF_Banner_GlassOverlay]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_GlassOverlay]  DEFAULT ((1)) FOR [GlassOverlay]
GO
/****** Object:  Default [DF_BetaTester_UserID]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[BetaTester] ADD  CONSTRAINT [DF_BetaTester_UserID]  DEFAULT ((0)) FOR [UserId]
GO
/****** Object:  Default [DF_BetaTester_EmailedUser]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[BetaTester] ADD  CONSTRAINT [DF_BetaTester_EmailedUser]  DEFAULT ((0)) FOR [EmailedUser]
GO
/****** Object:  Default [DF_Icon_Size]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Icon] ADD  CONSTRAINT [DF_Icon_Size]  DEFAULT ((72)) FOR [Width]
GO
/****** Object:  Default [DF_Icon_Height]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Icon] ADD  CONSTRAINT [DF_Icon_Height]  DEFAULT ((72)) FOR [Height]
GO
/****** Object:  Default [DF__online_cou__date__023D5A04]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[OnlineCounts] ADD  CONSTRAINT [DF__online_cou__date__023D5A04]  DEFAULT (((1900)/(1))/(1)) FOR [Date]
GO
/****** Object:  Default [DF__online_co__count__03317E3D]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[OnlineCounts] ADD  CONSTRAINT [DF__online_co__count__03317E3D]  DEFAULT ('0') FOR [Count]
GO
/****** Object:  Default [DF_ServerRules_DisplayOrder]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[ServerRules] ADD  CONSTRAINT [DF_ServerRules_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO
/****** Object:  Default [DF_ServerRulesCategories_DisplayOrder]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[ServerRulesCategories] ADD  CONSTRAINT [DF_ServerRulesCategories_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO
/****** Object:  Default [DF__shards__name__09DE7BCC]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__name__09DE7BCC]  DEFAULT ('') FOR [Name]
GO
/****** Object:  Default [DF__shards__url__0AD2A005]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__url__0AD2A005]  DEFAULT ('') FOR [Url]
GO
/****** Object:  Default [DF__shards__era__0DAF0CB0]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__era__0DAF0CB0]  DEFAULT ((1)) FOR [Era]
GO
/****** Object:  Default [DF__shards__type__0EA330E9]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__type__0EA330E9]  DEFAULT ((1)) FOR [ShardType]
GO
/****** Object:  Default [DF__shards__lang__0F975522]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__lang__0F975522]  DEFAULT ((1)) FOR [Lang]
GO
/****** Object:  Default [DF__shards__remove_e__117F9D94]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__remove_e__117F9D94]  DEFAULT ('1') FOR [RemoveEncryption]
GO
/****** Object:  Default [DF__shards__razor__1273C1CD]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__razor__1273C1CD]  DEFAULT ('1') FOR [AllowRazor]
GO
/****** Object:  Default [DF__shards__addr__164452B1]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__addr__164452B1]  DEFAULT ('') FOR [HostAddress]
GO
/****** Object:  Default [DF__shards__port__173876EA]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__port__173876EA]  DEFAULT ('2593') FOR [Port]
GO
/****** Object:  Default [DF__shards__lastupda__182C9B23]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__lastupda__182C9B23]  DEFAULT ('1900-01-01 00:00:00') FOR [LastUpdate]
GO
/****** Object:  Default [DF__shards__status__1920BF5C]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard] ADD  CONSTRAINT [DF__shards__status__1920BF5C]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  ForeignKey [FK_Shard_Era]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard]  WITH CHECK ADD  CONSTRAINT [FK_Shard_Era] FOREIGN KEY([Era])
REFERENCES [dbo].[Era] ([id])
GO
ALTER TABLE [dbo].[Shard] CHECK CONSTRAINT [FK_Shard_Era]
GO
/****** Object:  ForeignKey [FK_Shard_Lang1]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard]  WITH CHECK ADD  CONSTRAINT [FK_Shard_Lang1] FOREIGN KEY([Lang])
REFERENCES [dbo].[Lang] ([id])
GO
ALTER TABLE [dbo].[Shard] CHECK CONSTRAINT [FK_Shard_Lang1]
GO
/****** Object:  ForeignKey [FK_Shard_Status]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard]  WITH CHECK ADD  CONSTRAINT [FK_Shard_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[Status] ([id])
GO
ALTER TABLE [dbo].[Shard] CHECK CONSTRAINT [FK_Shard_Status]
GO
/****** Object:  ForeignKey [FK_Shard_Type]    Script Date: 11/03/2009 01:18:11 ******/
ALTER TABLE [dbo].[Shard]  WITH CHECK ADD  CONSTRAINT [FK_Shard_Type] FOREIGN KEY([ShardType])
REFERENCES [dbo].[Type] ([id])
GO
ALTER TABLE [dbo].[Shard] CHECK CONSTRAINT [FK_Shard_Type]
GO
