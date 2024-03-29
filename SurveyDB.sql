USE [master]
GO
/****** Object:  Database [SurveyDB]    Script Date: 08/03/2019 7:08:14 PM ******/
CREATE DATABASE [SurveyDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SurveyDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SurveyDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SurveyDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SurveyDB_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SurveyDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SurveyDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SurveyDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SurveyDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SurveyDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SurveyDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SurveyDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SurveyDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SurveyDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SurveyDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SurveyDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SurveyDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SurveyDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SurveyDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SurveyDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SurveyDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SurveyDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SurveyDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SurveyDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SurveyDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SurveyDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SurveyDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SurveyDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SurveyDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SurveyDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SurveyDB] SET  MULTI_USER 
GO
ALTER DATABASE [SurveyDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SurveyDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SurveyDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SurveyDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SurveyDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SurveyDB', N'ON'
GO
USE [SurveyDB]
GO
/****** Object:  Table [dbo].[tblAdmin]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdmin](
	[AdminID] [nvarchar](50) NOT NULL,
	[AdminName] [nvarchar](50) NOT NULL,
	[AdminCreationDate] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblAdmin] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblClass]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblClass](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[ReportingDatetime] [datetime] NULL,
	[ClassActive] [bit] NOT NULL,
 CONSTRAINT [PK_tblClass] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCompetition]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompetition](
	[CompID] [int] IDENTITY(1,1) NOT NULL,
	[CompName] [nvarchar](50) NOT NULL,
	[CompDescription] [nvarchar](50) NULL,
	[CompDate] [nvarchar](50) NOT NULL,
	[CompActive] [bit] NOT NULL,
	[UserTypeID] [int] NOT NULL,
 CONSTRAINT [PK_tblCompetition] PRIMARY KEY CLUSTERED 
(
	[CompID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCompParticipant]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompParticipant](
	[CompParticID] [int] IDENTITY(1,1) NOT NULL,
	[CompID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[CompParticName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblCompParticipant] PRIMARY KEY CLUSTERED 
(
	[CompParticID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCompWinner]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompWinner](
	[CompWinID] [int] IDENTITY(1,1) NOT NULL,
	[FirstWinner] [int] NOT NULL,
	[SecondWinner] [int] NULL,
	[ThirdWinner] [int] NULL,
 CONSTRAINT [PK_tblCompWinner] PRIMARY KEY CLUSTERED 
(
	[CompWinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblFaculty]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblFaculty](
	[FacultyID] [nvarchar](50) NOT NULL,
	[FacultyName] [nvarchar](50) NOT NULL,
	[FacultyGender] [char](1) NOT NULL,
	[FacultyJoiningDate] [nvarchar](50) NOT NULL,
	[FacultyActive] [bit] NOT NULL,
	[FacultySpecification] [nvarchar](50) NOT NULL,
	[FacultyEmail] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblFaculty] PRIMARY KEY CLUSTERED 
(
	[FacultyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblFAQ]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFAQ](
	[FAQID] [int] IDENTITY(1,1) NOT NULL,
	[FAQQuestion] [nvarchar](max) NOT NULL,
	[FAQAnswer] [nvarchar](max) NOT NULL,
	[FAQDateTime] [datetime] NOT NULL,
	[FAQActive] [bit] NOT NULL,
 CONSTRAINT [PK_tblFAQ] PRIMARY KEY CLUSTERED 
(
	[FAQID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSection]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSection](
	[SectionID] [int] IDENTITY(1,1) NOT NULL,
	[SectionName] [nvarchar](50) NOT NULL,
	[ClassID] [int] NOT NULL,
	[ReportingDateTime] [datetime] NULL,
	[SectionActive] [bit] NOT NULL,
 CONSTRAINT [PK_tblSection] PRIMARY KEY CLUSTERED 
(
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblStudent]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblStudent](
	[StudentID] [nvarchar](50) NOT NULL,
	[StudentName] [nvarchar](50) NOT NULL,
	[SectionID] [int] NOT NULL,
	[StudentGender] [char](1) NOT NULL,
	[StudentAdmissionDate] [nvarchar](50) NOT NULL,
	[StudentActive] [bit] NOT NULL,
	[StudentEmail] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblStudent] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSupport]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupport](
	[SupportID] [int] IDENTITY(1,1) NOT NULL,
	[SupportNumber] [nvarchar](50) NOT NULL,
	[SupportEmail] [nvarchar](50) NOT NULL,
	[SupportAddress] [nvarchar](max) NOT NULL,
	[SupportPerson] [nvarchar](50) NOT NULL,
	[ReportingDateTime] [datetime] NOT NULL,
	[SupportActive] [bit] NOT NULL,
 CONSTRAINT [PK_tblSupport] PRIMARY KEY CLUSTERED 
(
	[SupportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSurvey]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSurvey](
	[SurveyID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyTitle] [nvarchar](50) NOT NULL,
	[SurveyDueDate] [nvarchar](50) NOT NULL,
	[SurveyDescription] [nvarchar](150) NOT NULL,
	[UserTypeID] [int] NOT NULL,
	[SurveyConducts] [int] NOT NULL,
	[SurveyActive] [bit] NOT NULL,
	[SurveyReportingDateTime] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblSurvey] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSurveyQuestion]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSurveyQuestion](
	[SurveyQuestionID] [int] NOT NULL,
	[SurveyID] [int] NOT NULL,
	[SurveyQuestion] [nvarchar](50) NOT NULL,
	[SurveyOptions] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tblSurveyQuestions] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSurveyResponse]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSurveyResponse](
	[SurveyResponseID] [int] NOT NULL,
	[SurveyQuestionID] [int] NOT NULL,
	[QuestionResponse] [nvarchar](50) NOT NULL,
	[UserID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblSurveyResponse] PRIMARY KEY CLUSTERED 
(
	[SurveyResponseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[AdminNo] [nvarchar](50) NULL,
	[StaffNo] [nvarchar](50) NULL,
	[StudentNo] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[UserPassword] [nvarchar](50) NULL,
	[UserRequestDate] [nvarchar](50) NOT NULL,
	[UserTypeID] [int] NOT NULL,
	[UserActive] [bit] NOT NULL,
	[UserRegStatus] [nvarchar](50) NULL,
	[UserReqRejectReason] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblUser_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUserType]    Script Date: 08/03/2019 7:08:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUserType](
	[UserTypeID] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeName] [nvarchar](50) NOT NULL,
	[UserTypeKeyword] [char](1) NOT NULL,
 CONSTRAINT [PK_tblUserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[tblAdmin] ([AdminID], [AdminName], [AdminCreationDate]) VALUES (N'Admin1', N'Asim', N'19/02/2019')
SET IDENTITY_INSERT [dbo].[tblClass] ON 

INSERT [dbo].[tblClass] ([ClassID], [ClassName], [ReportingDatetime], [ClassActive]) VALUES (1, N'9', NULL, 1)
INSERT [dbo].[tblClass] ([ClassID], [ClassName], [ReportingDatetime], [ClassActive]) VALUES (2, N'10', NULL, 1)
INSERT [dbo].[tblClass] ([ClassID], [ClassName], [ReportingDatetime], [ClassActive]) VALUES (3, N'First Year', NULL, 0)
SET IDENTITY_INSERT [dbo].[tblClass] OFF
SET IDENTITY_INSERT [dbo].[tblCompetition] ON 

INSERT [dbo].[tblCompetition] ([CompID], [CompName], [CompDescription], [CompDate], [CompActive], [UserTypeID]) VALUES (1, N'ApTech Quiz', N'Quiz competition for all students', N'2019-02-18', 1, 2)
INSERT [dbo].[tblCompetition] ([CompID], [CompName], [CompDescription], [CompDate], [CompActive], [UserTypeID]) VALUES (2, N'TEST', N'TESTDescription', N'2019-02-18', 1, 3)
INSERT [dbo].[tblCompetition] ([CompID], [CompName], [CompDescription], [CompDate], [CompActive], [UserTypeID]) VALUES (6, N'StudentTest', N'asdasd', N'2019-02-18', 1, 2)
INSERT [dbo].[tblCompetition] ([CompID], [CompName], [CompDescription], [CompDate], [CompActive], [UserTypeID]) VALUES (7, N'TEST111111', N'asdasd', N'0001-01-01', 0, 2)
INSERT [dbo].[tblCompetition] ([CompID], [CompName], [CompDescription], [CompDate], [CompActive], [UserTypeID]) VALUES (8, N'dfssdf', N'asdas', N'2019-02-20', 1, 2)
SET IDENTITY_INSERT [dbo].[tblCompetition] OFF
SET IDENTITY_INSERT [dbo].[tblCompParticipant] ON 

INSERT [dbo].[tblCompParticipant] ([CompParticID], [CompID], [UserID], [CompParticName]) VALUES (1, 1, 1, N'Asim')
INSERT [dbo].[tblCompParticipant] ([CompParticID], [CompID], [UserID], [CompParticName]) VALUES (2, 1, 7, N'Nabeel')
SET IDENTITY_INSERT [dbo].[tblCompParticipant] OFF
SET IDENTITY_INSERT [dbo].[tblCompWinner] ON 

INSERT [dbo].[tblCompWinner] ([CompWinID], [FirstWinner], [SecondWinner], [ThirdWinner]) VALUES (6, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblCompWinner] OFF
INSERT [dbo].[tblFaculty] ([FacultyID], [FacultyName], [FacultyGender], [FacultyJoiningDate], [FacultyActive], [FacultySpecification], [FacultyEmail]) VALUES (N'faculty1', N'Ahmed', N'M', N'01/01/2019', 1, N'PHP', N'ahmed@aptech.com')
INSERT [dbo].[tblFaculty] ([FacultyID], [FacultyName], [FacultyGender], [FacultyJoiningDate], [FacultyActive], [FacultySpecification], [FacultyEmail]) VALUES (N'faculty2', N'Waseem', N'M', N'11/02/2019', 1, N'Marketing', N'waseem@aptech.com')
INSERT [dbo].[tblFaculty] ([FacultyID], [FacultyName], [FacultyGender], [FacultyJoiningDate], [FacultyActive], [FacultySpecification], [FacultyEmail]) VALUES (N'faculty3', N'Zubair', N'M', N'20/02/2019', 1, N'C#', N'zubair@aptech.com')
SET IDENTITY_INSERT [dbo].[tblFAQ] ON 

INSERT [dbo].[tblFAQ] ([FAQID], [FAQQuestion], [FAQAnswer], [FAQDateTime], [FAQActive]) VALUES (1, N'How to register for the survey? ', N'xyz
abc', CAST(N'2019-02-18 16:31:33.073' AS DateTime), 1)
INSERT [dbo].[tblFAQ] ([FAQID], [FAQQuestion], [FAQAnswer], [FAQDateTime], [FAQActive]) VALUES (2, N'How to participate in the survey?', N'xyz abc', CAST(N'2019-02-18 16:34:36.267' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[tblFAQ] OFF
SET IDENTITY_INSERT [dbo].[tblSection] ON 

INSERT [dbo].[tblSection] ([SectionID], [SectionName], [ClassID], [ReportingDateTime], [SectionActive]) VALUES (1, N'A', 1, NULL, 1)
INSERT [dbo].[tblSection] ([SectionID], [SectionName], [ClassID], [ReportingDateTime], [SectionActive]) VALUES (2, N'B', 1, NULL, 1)
INSERT [dbo].[tblSection] ([SectionID], [SectionName], [ClassID], [ReportingDateTime], [SectionActive]) VALUES (3, N'C', 1, NULL, 1)
INSERT [dbo].[tblSection] ([SectionID], [SectionName], [ClassID], [ReportingDateTime], [SectionActive]) VALUES (4, N'A', 2, NULL, 1)
INSERT [dbo].[tblSection] ([SectionID], [SectionName], [ClassID], [ReportingDateTime], [SectionActive]) VALUES (5, N'Pre Medical A', 3, CAST(N'2019-02-18 16:46:20.867' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[tblSection] OFF
INSERT [dbo].[tblStudent] ([StudentID], [StudentName], [SectionID], [StudentGender], [StudentAdmissionDate], [StudentActive], [StudentEmail]) VALUES (N'student1', N'Asim', 4, N'M', N'11/02/2019', 1, N'asim1@gmail.com')
INSERT [dbo].[tblStudent] ([StudentID], [StudentName], [SectionID], [StudentGender], [StudentAdmissionDate], [StudentActive], [StudentEmail]) VALUES (N'student2', N'Nabeel', 1, N'M', N'11/02/2019', 1, N'nabeel@gmail.com')
INSERT [dbo].[tblStudent] ([StudentID], [StudentName], [SectionID], [StudentGender], [StudentAdmissionDate], [StudentActive], [StudentEmail]) VALUES (N'student3', N'Shehryar', 3, N'M', N'11/02/2019', 1, N'shehryar@gmail.com')
INSERT [dbo].[tblStudent] ([StudentID], [StudentName], [SectionID], [StudentGender], [StudentAdmissionDate], [StudentActive], [StudentEmail]) VALUES (N'student4', N'Fahad', 2, N'M', N'11/02/2019', 1, N'fahad@gmail.com')
INSERT [dbo].[tblStudent] ([StudentID], [StudentName], [SectionID], [StudentGender], [StudentAdmissionDate], [StudentActive], [StudentEmail]) VALUES (N'student5', N'Usama', 4, N'M', N'11/02/2019', 1, N'usama@gmail.com')
INSERT [dbo].[tblStudent] ([StudentID], [StudentName], [SectionID], [StudentGender], [StudentAdmissionDate], [StudentActive], [StudentEmail]) VALUES (N'student6', N'TESTER', 5, N'F', N'21/02/2019', 1, N'tester@test.com')
INSERT [dbo].[tblStudent] ([StudentID], [StudentName], [SectionID], [StudentGender], [StudentAdmissionDate], [StudentActive], [StudentEmail]) VALUES (N'student7', N'asd', 2, N'M', N'21/02/2019', 1, N'asim1@gmail.com')
SET IDENTITY_INSERT [dbo].[tblSupport] ON 

INSERT [dbo].[tblSupport] ([SupportID], [SupportNumber], [SupportEmail], [SupportAddress], [SupportPerson], [ReportingDateTime], [SupportActive]) VALUES (1, N'0333-1234567', N'xyz@gmail.com', N'Aptech, Rawalpindi', N'Manager', CAST(N'2019-02-18 16:38:37.717' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[tblSupport] OFF
SET IDENTITY_INSERT [dbo].[tblSurvey] ON 

INSERT [dbo].[tblSurvey] ([SurveyID], [SurveyTitle], [SurveyDueDate], [SurveyDescription], [UserTypeID], [SurveyConducts], [SurveyActive], [SurveyReportingDateTime]) VALUES (1, N'yolo survedddsdsdsdds', N'2019-03-28', N'jgfdjgfdghfd', 4, 0, 1, N'06/03/2019')
SET IDENTITY_INSERT [dbo].[tblSurvey] OFF
SET IDENTITY_INSERT [dbo].[tblUser] ON 

INSERT [dbo].[tblUser] ([UserID], [AdminNo], [StaffNo], [StudentNo], [UserName], [UserPassword], [UserRequestDate], [UserTypeID], [UserActive], [UserRegStatus], [UserReqRejectReason]) VALUES (1, NULL, NULL, N'student1', NULL, NULL, N'13/02/2019', 2, 1, N'Accepted', N'Too many registered candidates from students class')
INSERT [dbo].[tblUser] ([UserID], [AdminNo], [StaffNo], [StudentNo], [UserName], [UserPassword], [UserRequestDate], [UserTypeID], [UserActive], [UserRegStatus], [UserReqRejectReason]) VALUES (3, NULL, N'faculty2', NULL, NULL, NULL, N'16/02/2019', 3, 1, N'Accepted', N'Not good boi')
INSERT [dbo].[tblUser] ([UserID], [AdminNo], [StaffNo], [StudentNo], [UserName], [UserPassword], [UserRequestDate], [UserTypeID], [UserActive], [UserRegStatus], [UserReqRejectReason]) VALUES (6, N'admin1', NULL, NULL, N'asd', N'asd', N'19/02/2019', 4, 1, N'Accepted', NULL)
INSERT [dbo].[tblUser] ([UserID], [AdminNo], [StaffNo], [StudentNo], [UserName], [UserPassword], [UserRequestDate], [UserTypeID], [UserActive], [UserRegStatus], [UserReqRejectReason]) VALUES (7, NULL, NULL, N'student2', NULL, NULL, N'19/02/2019', 2, 1, N'Requested', NULL)
INSERT [dbo].[tblUser] ([UserID], [AdminNo], [StaffNo], [StudentNo], [UserName], [UserPassword], [UserRequestDate], [UserTypeID], [UserActive], [UserRegStatus], [UserReqRejectReason]) VALUES (8, NULL, N'faculty1', NULL, NULL, NULL, N'19/02/2019', 3, 1, N'Requested', NULL)
INSERT [dbo].[tblUser] ([UserID], [AdminNo], [StaffNo], [StudentNo], [UserName], [UserPassword], [UserRequestDate], [UserTypeID], [UserActive], [UserRegStatus], [UserReqRejectReason]) VALUES (10, NULL, NULL, N'student3', NULL, NULL, N'08/03/2019', 2, 1, N'Rejected', N'Too good to give an opinion')
SET IDENTITY_INSERT [dbo].[tblUser] OFF
SET IDENTITY_INSERT [dbo].[tblUserType] ON 

INSERT [dbo].[tblUserType] ([UserTypeID], [UserTypeName], [UserTypeKeyword]) VALUES (2, N'Student', N'S')
INSERT [dbo].[tblUserType] ([UserTypeID], [UserTypeName], [UserTypeKeyword]) VALUES (3, N'Teacher', N'T')
INSERT [dbo].[tblUserType] ([UserTypeID], [UserTypeName], [UserTypeKeyword]) VALUES (4, N'Admin', N'A')
SET IDENTITY_INSERT [dbo].[tblUserType] OFF
ALTER TABLE [dbo].[tblCompetition]  WITH CHECK ADD  CONSTRAINT [FK_tblCompetition_tblUserType] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[tblUserType] ([UserTypeID])
GO
ALTER TABLE [dbo].[tblCompetition] CHECK CONSTRAINT [FK_tblCompetition_tblUserType]
GO
ALTER TABLE [dbo].[tblCompParticipant]  WITH CHECK ADD  CONSTRAINT [FK_tblCompParticipant_tblCompetition] FOREIGN KEY([CompID])
REFERENCES [dbo].[tblCompetition] ([CompID])
GO
ALTER TABLE [dbo].[tblCompParticipant] CHECK CONSTRAINT [FK_tblCompParticipant_tblCompetition]
GO
ALTER TABLE [dbo].[tblCompParticipant]  WITH CHECK ADD  CONSTRAINT [FK_tblCompParticipant_tblUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUser] ([UserID])
GO
ALTER TABLE [dbo].[tblCompParticipant] CHECK CONSTRAINT [FK_tblCompParticipant_tblUser]
GO
ALTER TABLE [dbo].[tblCompWinner]  WITH CHECK ADD  CONSTRAINT [FK_tblCompWinner_tblCompParticipant] FOREIGN KEY([FirstWinner])
REFERENCES [dbo].[tblCompParticipant] ([CompParticID])
GO
ALTER TABLE [dbo].[tblCompWinner] CHECK CONSTRAINT [FK_tblCompWinner_tblCompParticipant]
GO
ALTER TABLE [dbo].[tblCompWinner]  WITH CHECK ADD  CONSTRAINT [FK_tblCompWinner_tblCompParticipant1] FOREIGN KEY([SecondWinner])
REFERENCES [dbo].[tblCompParticipant] ([CompParticID])
GO
ALTER TABLE [dbo].[tblCompWinner] CHECK CONSTRAINT [FK_tblCompWinner_tblCompParticipant1]
GO
ALTER TABLE [dbo].[tblCompWinner]  WITH CHECK ADD  CONSTRAINT [FK_tblCompWinner_tblCompParticipant2] FOREIGN KEY([ThirdWinner])
REFERENCES [dbo].[tblCompParticipant] ([CompParticID])
GO
ALTER TABLE [dbo].[tblCompWinner] CHECK CONSTRAINT [FK_tblCompWinner_tblCompParticipant2]
GO
ALTER TABLE [dbo].[tblSection]  WITH CHECK ADD  CONSTRAINT [FK_tblSection_tblClass] FOREIGN KEY([ClassID])
REFERENCES [dbo].[tblClass] ([ClassID])
GO
ALTER TABLE [dbo].[tblSection] CHECK CONSTRAINT [FK_tblSection_tblClass]
GO
ALTER TABLE [dbo].[tblStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblStudent_tblSection] FOREIGN KEY([SectionID])
REFERENCES [dbo].[tblSection] ([SectionID])
GO
ALTER TABLE [dbo].[tblStudent] CHECK CONSTRAINT [FK_tblStudent_tblSection]
GO
ALTER TABLE [dbo].[tblSurvey]  WITH CHECK ADD  CONSTRAINT [FK_tblSurvey_tblUserType] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[tblUserType] ([UserTypeID])
GO
ALTER TABLE [dbo].[tblSurvey] CHECK CONSTRAINT [FK_tblSurvey_tblUserType]
GO
ALTER TABLE [dbo].[tblSurveyQuestion]  WITH CHECK ADD  CONSTRAINT [FK_tblSurveyQuestion_tblSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[tblSurvey] ([SurveyID])
GO
ALTER TABLE [dbo].[tblSurveyQuestion] CHECK CONSTRAINT [FK_tblSurveyQuestion_tblSurvey]
GO
ALTER TABLE [dbo].[tblSurveyResponse]  WITH CHECK ADD  CONSTRAINT [FK_tblSurveyResponse_tblSurveyQuestion] FOREIGN KEY([SurveyQuestionID])
REFERENCES [dbo].[tblSurveyQuestion] ([SurveyQuestionID])
GO
ALTER TABLE [dbo].[tblSurveyResponse] CHECK CONSTRAINT [FK_tblSurveyResponse_tblSurveyQuestion]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_tblAdmin] FOREIGN KEY([AdminNo])
REFERENCES [dbo].[tblAdmin] ([AdminID])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblAdmin]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_tblFaculty] FOREIGN KEY([StaffNo])
REFERENCES [dbo].[tblFaculty] ([FacultyID])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblFaculty]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_tblStudent] FOREIGN KEY([StudentNo])
REFERENCES [dbo].[tblStudent] ([StudentID])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblStudent]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_tblUserType] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[tblUserType] ([UserTypeID])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblUserType]
GO
USE [master]
GO
ALTER DATABASE [SurveyDB] SET  READ_WRITE 
GO
