USE [TMS_V1]
GO
/****** Object:  Schema [Security]    Script Date: 9/15/2024 6:36:27 PM ******/
CREATE SCHEMA [Security]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/15/2024 6:36:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[TaskId] [int] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegUsers]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_RegUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](70) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Attachment] [nvarchar](50) NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[Status] [int] NULL,
	[Priority] [int] NULL,
	[UserId] [nvarchar](450) NULL,
	[TeamId] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeamLeads]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamLeads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_TeamLeads] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTeams]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTeams](
	[UserId] [nvarchar](450) NOT NULL,
	[TeamId] [int] NOT NULL,
 CONSTRAINT [PK_UserTeams] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Security].[RoleClaims]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Security].[Roles]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Security].[UserClaims]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Security].[UserLogins]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Security].[UserRoles]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Security].[Users]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Security].[UserTokens]    Script Date: 9/15/2024 6:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[UserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240910205828_Init', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240911063301_SeedingRoles', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240911192152_AddOtherTables', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240911193545_AddUserTeamsAndCommentsTables', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240915150837_SetNullInTasksTableInTeamIdColumn', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([Id], [Description], [UserId], [TaskId]) VALUES (8, N'this is  a comment from user 1
', N'a678c523-4219-4569-b558-0f9bcef1b44d', 13)
INSERT [dbo].[Comments] ([Id], [Description], [UserId], [TaskId]) VALUES (9, N'this is  a comment number 2 from user 1
', N'a678c523-4219-4569-b558-0f9bcef1b44d', 13)
INSERT [dbo].[Comments] ([Id], [Description], [UserId], [TaskId]) VALUES (10, N'this is  a comment number 3 from user 1
', N'a678c523-4219-4569-b558-0f9bcef1b44d', 13)
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[RegUsers] ON 

INSERT [dbo].[RegUsers] ([Id], [UserId]) VALUES (16, N'8388b9ee-13e0-4d8a-ba10-e09106bd6c89')
INSERT [dbo].[RegUsers] ([Id], [UserId]) VALUES (14, N'9d8d684e-8dbf-4da4-8126-5267f0f3ba67')
INSERT [dbo].[RegUsers] ([Id], [UserId]) VALUES (12, N'a678c523-4219-4569-b558-0f9bcef1b44d')
SET IDENTITY_INSERT [dbo].[RegUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Attachment], [DueDate], [Status], [Priority], [UserId], [TeamId]) VALUES (11, N'Task 2 by admin update', N'A detailed explanation of the task, including its purpose, objectives, and any specific instructions or requirements. created by an admin', N'0eeda_biker.jpg', CAST(N'2024-09-13T00:00:00.0000000' AS DateTime2), 2, 0, NULL, 6)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Attachment], [DueDate], [Status], [Priority], [UserId], [TeamId]) VALUES (13, N'Task 1 by user 1', N'this a task used by user', N'0b645_apple.jpg', CAST(N'2024-09-17T00:00:00.0000000' AS DateTime2), 1, 1, N'a678c523-4219-4569-b558-0f9bcef1b44d', 6)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Attachment], [DueDate], [Status], [Priority], [UserId], [TeamId]) VALUES (14, N'Task 2 by user 1', N'task created by user 1', N'c48b5_Technical documentation.pdf', CAST(N'2024-09-15T00:00:00.0000000' AS DateTime2), 0, 1, N'9d8d684e-8dbf-4da4-8126-5267f0f3ba67', 7)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Attachment], [DueDate], [Status], [Priority], [UserId], [TeamId]) VALUES (17, N'task 9 by admin', N'task created by admin ', N'6f22a_apple.jpg', CAST(N'2024-09-24T00:00:00.0000000' AS DateTime2), 1, 0, NULL, 7)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Attachment], [DueDate], [Status], [Priority], [UserId], [TeamId]) VALUES (18, N'task 8 by admin', N'task created by admin ', N'2c066_mobile1.png', CAST(N'2024-09-17T00:00:00.0000000' AS DateTime2), 0, 2, N'2604afec-4aca-42dc-ab85-c0c01f2e20fd', 6)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Attachment], [DueDate], [Status], [Priority], [UserId], [TeamId]) VALUES (19, N'task 7', N'task created by admin', N'6743a_Technical documentation.pdf', CAST(N'2024-09-08T00:00:00.0000000' AS DateTime2), 2, 0, NULL, 7)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[TeamLeads] ON 

INSERT [dbo].[TeamLeads] ([Id], [UserId]) VALUES (7, N'2604afec-4aca-42dc-ab85-c0c01f2e20fd')
INSERT [dbo].[TeamLeads] ([Id], [UserId]) VALUES (8, N'37b49433-b47d-4d65-8136-055f8b72ab6e')
INSERT [dbo].[TeamLeads] ([Id], [UserId]) VALUES (9, N'4ab21ad3-c654-4ad2-bccf-5b206b174de8')
INSERT [dbo].[TeamLeads] ([Id], [UserId]) VALUES (4, N'71a00579-664e-47fb-87b8-c5cd4583ea08')
INSERT [dbo].[TeamLeads] ([Id], [UserId]) VALUES (6, N'd95513f6-cd0d-470a-bd17-3d40d8d4d814')
SET IDENTITY_INSERT [dbo].[TeamLeads] OFF
GO
SET IDENTITY_INSERT [dbo].[Teams] ON 

INSERT [dbo].[Teams] ([Id], [Name]) VALUES (6, N'FrontEnd ')
INSERT [dbo].[Teams] ([Id], [Name]) VALUES (7, N'DevOps')
SET IDENTITY_INSERT [dbo].[Teams] OFF
GO
INSERT [dbo].[UserTeams] ([UserId], [TeamId]) VALUES (N'2604afec-4aca-42dc-ab85-c0c01f2e20fd', 6)
INSERT [dbo].[UserTeams] ([UserId], [TeamId]) VALUES (N'71a00579-664e-47fb-87b8-c5cd4583ea08', 6)
INSERT [dbo].[UserTeams] ([UserId], [TeamId]) VALUES (N'8388b9ee-13e0-4d8a-ba10-e09106bd6c89', 6)
INSERT [dbo].[UserTeams] ([UserId], [TeamId]) VALUES (N'9d8d684e-8dbf-4da4-8126-5267f0f3ba67', 6)
INSERT [dbo].[UserTeams] ([UserId], [TeamId]) VALUES (N'37b49433-b47d-4d65-8136-055f8b72ab6e', 7)
INSERT [dbo].[UserTeams] ([UserId], [TeamId]) VALUES (N'4ab21ad3-c654-4ad2-bccf-5b206b174de8', 7)
INSERT [dbo].[UserTeams] ([UserId], [TeamId]) VALUES (N'9d8d684e-8dbf-4da4-8126-5267f0f3ba67', 7)
GO
INSERT [Security].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2d50b281-79c7-4d6a-bdb3-3bc7b3bb9e21', N'RegularUser', N'REGULARUSER', N'0f480e55-c174-4e31-a98f-cbd3694cd1fc')
INSERT [Security].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'56786618-1648-4405-8b5a-0d60dee87658', N'Administrator', N'ADMINISTRATOR', N'23642373-2913-4694-92d6-342847b49b55')
INSERT [Security].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'7534120b-c31a-423d-87b8-f084c9f9d07a', N'TeamLead', N'TEAMLEAD', N'089d4824-1994-423a-9972-d75e64437437')
GO
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'8388b9ee-13e0-4d8a-ba10-e09106bd6c89', N'2d50b281-79c7-4d6a-bdb3-3bc7b3bb9e21')
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'9d8d684e-8dbf-4da4-8126-5267f0f3ba67', N'2d50b281-79c7-4d6a-bdb3-3bc7b3bb9e21')
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'a678c523-4219-4569-b558-0f9bcef1b44d', N'2d50b281-79c7-4d6a-bdb3-3bc7b3bb9e21')
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'a74fcbb1-4a00-4ae0-ac7e-de0588be1809', N'56786618-1648-4405-8b5a-0d60dee87658')
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'2604afec-4aca-42dc-ab85-c0c01f2e20fd', N'7534120b-c31a-423d-87b8-f084c9f9d07a')
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'37b49433-b47d-4d65-8136-055f8b72ab6e', N'7534120b-c31a-423d-87b8-f084c9f9d07a')
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'4ab21ad3-c654-4ad2-bccf-5b206b174de8', N'7534120b-c31a-423d-87b8-f084c9f9d07a')
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'71a00579-664e-47fb-87b8-c5cd4583ea08', N'7534120b-c31a-423d-87b8-f084c9f9d07a')
INSERT [Security].[UserRoles] ([UserId], [RoleId]) VALUES (N'd95513f6-cd0d-470a-bd17-3d40d8d4d814', N'7534120b-c31a-423d-87b8-f084c9f9d07a')
GO
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2604afec-4aca-42dc-ab85-c0c01f2e20fd', N'teamLead3@teamLead.com', N'TEAMLEAD3@TEAMLEAD.COM', N'teamLead3@teamLead.com', N'TEAMLEAD3@TEAMLEAD.COM', 1, N'AQAAAAIAAYagAAAAEDnIJDyYcrBwappm1ZiWUIXtiNo5FFZ5X/uC8fRjSRIGCMAVC93a0zUG6YLZCNWOOQ==', N'3OJQQORCROPHJT5HPZTR4GJRWEVBKYF3', N'30c57100-9cbd-4e5b-89a2-a1106698e223', NULL, 0, 0, NULL, 1, 0)
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'37b49433-b47d-4d65-8136-055f8b72ab6e', N'teamLead4@teamLead.com', N'TEAMLEAD4@TEAMLEAD.COM', N'teamLead4@teamLead.com', N'TEAMLEAD4@TEAMLEAD.COM', 1, N'AQAAAAIAAYagAAAAEPWo4Z/r8ZewYMqa78qVk9zNO0xdaPIpepNSXJUuyEWY3+d6j1y2PeEho9STQoSXiQ==', N'6JMDRDZF2JSUCT5QIGLVXBI325RNNTXQ', N'790b334a-7a94-40ee-8452-19d0d2b39a7b', NULL, 0, 0, NULL, 1, 0)
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4ab21ad3-c654-4ad2-bccf-5b206b174de8', N'teamLead5@teamLead.com', N'TEAMLEAD5@TEAMLEAD.COM', N'teamLead5@teamLead.com', N'TEAMLEAD5@TEAMLEAD.COM', 1, N'AQAAAAIAAYagAAAAEFUQLlghM9SiqWw3Do686hwx+TOunPlX7Hps9PWgRHYshBcZ49JXLGQZ3VzLn4NEmg==', N'B4HWEC67KRRPA2RR2XDNB5K7PEIJOIAS', N'9e6f3db3-c331-40f1-83b1-2f1c29207ce3', NULL, 0, 0, NULL, 1, 0)
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'71a00579-664e-47fb-87b8-c5cd4583ea08', N'teamLead1@teamLead.com', N'TEAMLEAD1@TEAMLEAD.COM', N'teamLead1@teamLead.com', N'TEAMLEAD1@TEAMLEAD.COM', 1, N'AQAAAAIAAYagAAAAEPsEwSRx8CF4RwK4BbZMrHAW1AjEGpEfNGTB2bvk5Prbx/HzIBVuC/5Q7MQLmRZ4xw==', N'E7XHUZZNHKJIM4HFDQLXJ6EL2OBOF4J2', N'30c96e9a-7683-4cd1-ac9a-e10e43b373c1', NULL, 0, 0, NULL, 1, 0)
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8388b9ee-13e0-4d8a-ba10-e09106bd6c89', N'tms1192024@gmail.com', N'TMS1192024@GMAIL.COM', N'tms1192024@gmail.com', N'TMS1192024@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJNZHEEYMoiAdCEEAWS3Zub/so1nh37jeqzjapn6LHAm/9NUdijmu9Va+aR/ldLmCA==', N'JODRD7NJZAUPHE2N34F2OA2WCLJJAJO3', N'b6b78477-1ce5-43f1-9427-2cfa8b543054', NULL, 0, 0, NULL, 1, 0)
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9d8d684e-8dbf-4da4-8126-5267f0f3ba67', N'user3@user.com', N'USER3@USER.COM', N'user3@user.com', N'USER3@USER.COM', 1, N'AQAAAAIAAYagAAAAEFREnTTyyE7JWvKKHlon7depQfnHfhpg4azQZZn3ybqc/g/7x/DgmsqqO4VvIkMRQw==', N'YFID3XM64IE2BN6D2BXKN3BA32Y77RK7', N'f0335b64-e832-45f0-9f8e-f8cfa0fc735a', NULL, 0, 0, NULL, 1, 0)
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a678c523-4219-4569-b558-0f9bcef1b44d', N'user1@user.com', N'USER1@USER.COM', N'user1@user.com', N'USER1@USER.COM', 1, N'AQAAAAIAAYagAAAAEHALvtvf/OEwUW4ELiwkR08V2ZUQcXAcV3afzJ0Hs0TQOvAFZqfRz0Mwa0EbqDQ+Tw==', N'DAJFMX4B4DXBVUU2SFX4SNLMGHFWATZR', N'493c7b6a-2bf8-4fe9-bbb7-008f9b372666', NULL, 0, 0, NULL, 1, 0)
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a74fcbb1-4a00-4ae0-ac7e-de0588be1809', N'admin@admin.com', N'ADMIN@ADMIN.COM', N'admin@admin.com', N'ADMIN@ADMIN.COM', 1, N'AQAAAAIAAYagAAAAEAkwN5bw4aJEMIrf3WpyuxaQ7sOTOFmaVhd2MCmBeTNbZNZMm0KqN+pWyuayUxy3NQ==', N'ZO4GSWHHBFCRWCND4GWG6XVEBAQALSC6', N'a4c302e1-1559-469a-973a-5013c1967116', NULL, 0, 0, NULL, 1, 0)
INSERT [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd95513f6-cd0d-470a-bd17-3d40d8d4d814', N'teamLead2@teamLead.com', N'TEAMLEAD2@TEAMLEAD.COM', N'teamLead2@teamLead.com', N'TEAMLEAD2@TEAMLEAD.COM', 1, N'AQAAAAIAAYagAAAAEIDKdHUhh76PG8ZMRgghpTMO2UBv8p6UTl8S8dHV6Jv5lZIhtAetaiGGveLG7V1pZw==', N'7KQ5YAH4MSUVKMH3WNXHJM4LDLETQAWX', N'ec00de1a-6129-4caf-9793-9f549cdbc3fc', NULL, 0, 0, NULL, 1, 0)
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Tasks_TaskId] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Tasks_TaskId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_UserId]
GO
ALTER TABLE [dbo].[RegUsers]  WITH CHECK ADD  CONSTRAINT [FK_RegUsers_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RegUsers] CHECK CONSTRAINT [FK_RegUsers_Users_UserId]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Teams_TeamId] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Teams_TeamId]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users_UserId]
GO
ALTER TABLE [dbo].[TeamLeads]  WITH CHECK ADD  CONSTRAINT [FK_TeamLeads_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TeamLeads] CHECK CONSTRAINT [FK_TeamLeads_Users_UserId]
GO
ALTER TABLE [dbo].[UserTeams]  WITH CHECK ADD  CONSTRAINT [FK_UserTeams_Teams_TeamId] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTeams] CHECK CONSTRAINT [FK_UserTeams_Teams_TeamId]
GO
ALTER TABLE [dbo].[UserTeams]  WITH CHECK ADD  CONSTRAINT [FK_UserTeams_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTeams] CHECK CONSTRAINT [FK_UserTeams_Users_UserId]
GO
ALTER TABLE [Security].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [Security].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Security].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [Security].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Security].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [Security].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Security].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [Security].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [Security].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Security].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [Security].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Security].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [Security].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Security].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
