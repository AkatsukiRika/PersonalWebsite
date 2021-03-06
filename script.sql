USE [Aquarium]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 2020/6/10 17:37:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleID] [int] IDENTITY(1,1) NOT NULL,
	[ArticleType] [varchar](32) NOT NULL,
	[ArticleDate] [date] NOT NULL,
	[ArticleContent] [varchar](max) NULL,
	[Markdown] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 2020/6/10 17:37:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[ContactName] [varchar](256) NOT NULL,
	[EMail] [varchar](256) NOT NULL,
	[Details] [varchar](1024) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News]    Script Date: 2020/6/10 17:37:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[NewsID] [int] IDENTITY(1,1) NOT NULL,
	[NewsType] [varchar](32) NOT NULL,
	[NewsDate] [date] NOT NULL,
	[ImgSrc] [varchar](256) NOT NULL DEFAULT ('NoImg'),
	[ImgText] [varchar](1024) NOT NULL DEFAULT ('DefaultNews'),
	[BigImg] [bit] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Post]    Script Date: 2020/6/10 17:37:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Post](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[TableHead] [varchar](256) NOT NULL,
	[TableData] [varchar](1024) NOT NULL,
	[ImgSrc] [varchar](256) NOT NULL DEFAULT ('NoImg'),
	[Tamidashi] [bit] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 2020/6/10 17:37:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profile](
	[ProfileID] [int] NOT NULL,
	[ProfileDescription] [varchar](1024) NULL,
	[BasicProfile] [varchar](2048) NULL,
	[Information] [varchar](2048) NULL,
	[Mutter] [varchar](2048) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ('INFORMATION') FOR [ArticleType]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (getdate()) FOR [ArticleDate]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT ('NoName') FOR [ContactName]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT ('NoEmail') FOR [EMail]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT ('NoDetails') FOR [Details]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD CHECK  (([NewsType]='MEDIA' OR [NewsType]='EVENT' OR [NewsType]='INFORMATION'))
GO
