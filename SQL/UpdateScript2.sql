USE [master]
GO
/****** Object:  Database [AnimalShelter]    Script Date: 03.06.2025 16:47:56 ******/
CREATE DATABASE [AnimalShelter]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AnimalShelter', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AnimalShelter.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AnimalShelter_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AnimalShelter_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AnimalShelter] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AnimalShelter].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AnimalShelter] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AnimalShelter] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AnimalShelter] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AnimalShelter] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AnimalShelter] SET ARITHABORT OFF 
GO
ALTER DATABASE [AnimalShelter] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AnimalShelter] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AnimalShelter] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AnimalShelter] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AnimalShelter] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AnimalShelter] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AnimalShelter] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AnimalShelter] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AnimalShelter] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AnimalShelter] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AnimalShelter] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AnimalShelter] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AnimalShelter] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AnimalShelter] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AnimalShelter] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AnimalShelter] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AnimalShelter] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AnimalShelter] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AnimalShelter] SET  MULTI_USER 
GO
ALTER DATABASE [AnimalShelter] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AnimalShelter] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AnimalShelter] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AnimalShelter] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AnimalShelter] SET DELAYED_DURABILITY = DISABLED 
GO
USE [AnimalShelter]
GO
/****** Object:  Table [dbo].[Adoption]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Adoption](
	[ID_adoption] [int] IDENTITY(1,1) NOT NULL,
	[Animal] [int] NOT NULL,
	[New_owner] [int] NOT NULL,
	[Date_of_adoption] [date] NOT NULL,
	[Contract] [varchar](255) NULL,
	[Adoption_status] [int] NOT NULL,
	[Comment] [text] NULL,
	[ContractPDF] [varchar](255) NULL,
 CONSTRAINT [PK__Adoption__0B8190088567EB63] PRIMARY KEY CLUSTERED 
(
	[ID_adoption] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Adoption_status]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Adoption_status](
	[ID_adoption_status] [int] IDENTITY(1,1) NOT NULL,
	[Name_adoption_status] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Adoption__0B3D83D697C299F7] PRIMARY KEY CLUSTERED 
(
	[ID_adoption_status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Animal]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Animal](
	[ID_animal] [int] IDENTITY(1,1) NOT NULL,
	[Volunteer] [int] NULL,
	[Species] [int] NOT NULL,
	[Breed] [int] NULL,
	[Gender] [int] NOT NULL,
	[Nickname] [varchar](50) NOT NULL,
	[Photo] [varchar](255) NULL,
	[Date_of_birth] [date] NOT NULL,
	[Date_of_registration] [date] NOT NULL,
	[Source_of_receipt] [int] NOT NULL,
	[Animal_status] [int] NOT NULL,
	[Note] [text] NULL,
 CONSTRAINT [PK__Animal__8A011B3E6FE2A136] PRIMARY KEY CLUSTERED 
(
	[ID_animal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Animal_status]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Animal_status](
	[ID_animal_status] [int] IDENTITY(1,1) NOT NULL,
	[Name_animal_status] [varchar](30) NOT NULL,
 CONSTRAINT [PK__Animal_s__D10C7F50A3F7FC54] PRIMARY KEY CLUSTERED 
(
	[ID_animal_status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Breed]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Breed](
	[ID_breed] [int] IDENTITY(1,1) NOT NULL,
	[Species] [int] NOT NULL,
	[Name_breed] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Breed__B4E8AFB0086CF137] PRIMARY KEY CLUSTERED 
(
	[ID_breed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Care_log]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Care_log](
	[ID_care_log] [int] IDENTITY(1,1) NOT NULL,
	[Date_care_log] [date] NOT NULL,
	[Employee] [int] NULL,
	[Volunteer] [int] NULL,
	[Animal] [int] NOT NULL,
	[Care_type] [int] NOT NULL,
	[Task_status] [int] NOT NULL,
	[Comment] [text] NULL,
 CONSTRAINT [PK__Care_log__60DF40590EA7CB84] PRIMARY KEY CLUSTERED 
(
	[ID_care_log] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Care_type]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Care_type](
	[ID_care_type] [int] IDENTITY(1,1) NOT NULL,
	[Name_care_type] [varchar](60) NOT NULL,
 CONSTRAINT [PK__Care_typ__85C59964DE995B88] PRIMARY KEY CLUSTERED 
(
	[ID_care_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contractor]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contractor](
	[ID_contractor] [int] IDENTITY(1,1) NOT NULL,
	[Contractor_type] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Contact_person] [varchar](100) NULL,
	[Phone_number] [varchar](15) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[INN] [varchar](12) NOT NULL,
 CONSTRAINT [PK__Contract__C3E285F67C0E1471] PRIMARY KEY CLUSTERED 
(
	[ID_contractor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contractor_type]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contractor_type](
	[ID_contractor_type] [int] IDENTITY(1,1) NOT NULL,
	[Name_contractor_type] [varchar](90) NOT NULL,
 CONSTRAINT [PK__Contract__C9A2C4EA1EAC000F] PRIMARY KEY CLUSTERED 
(
	[ID_contractor_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Donation]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donation](
	[ID_donation] [int] IDENTITY(1,1) NOT NULL,
	[Date_of_donation] [date] NOT NULL,
	[Contractor] [int] NULL,
	[Volunteer] [int] NULL,
	[Donation_type] [int] NOT NULL,
	[Amount] [decimal](10, 2) NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK__Donation__FCB7A47B1E56F7DC] PRIMARY KEY CLUSTERED 
(
	[ID_donation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Donation_type]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Donation_type](
	[ID_donation_type] [int] IDENTITY(1,1) NOT NULL,
	[Name_donation_type] [varchar](90) NOT NULL,
 CONSTRAINT [PK__Donation__1826EA9BBC6ED68F] PRIMARY KEY CLUSTERED 
(
	[ID_donation_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[ID_employee] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[First_name] [varchar](30) NOT NULL,
	[Patronymic] [varchar](50) NULL,
	[Gender] [int] NOT NULL,
	[Date_of_birth] [date] NOT NULL,
	[Position] [int] NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[Phone_number] [varchar](15) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Date_of_hire] [date] NOT NULL,
	[Login] [varchar](20) NULL,
	[Password] [varchar](40) NULL,
 CONSTRAINT [PK__Employee__D98466349784B912] PRIMARY KEY CLUSTERED 
(
	[ID_employee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gender](
	[ID_gender] [int] IDENTITY(1,1) NOT NULL,
	[Name_gender] [varchar](10) NOT NULL,
 CONSTRAINT [PK__Gender__0EB95DB2E7DDE9A7] PRIMARY KEY CLUSTERED 
(
	[ID_gender] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Housing_type]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Housing_type](
	[ID_housing_type] [int] IDENTITY(1,1) NOT NULL,
	[Name_housing_type] [varchar](40) NOT NULL,
 CONSTRAINT [PK__Housing___16F3E6D193A38AFA] PRIMARY KEY CLUSTERED 
(
	[ID_housing_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medical_record]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medical_record](
	[ID_medical_record] [int] IDENTITY(1,1) NOT NULL,
	[Animal] [int] NOT NULL,
	[Last_update_date] [date] NOT NULL,
	[Weight] [decimal](5, 2) NOT NULL,
	[Height] [decimal](5, 2) NOT NULL,
	[Chronic_illness] [text] NULL,
	[Medical_history] [text] NULL,
	[Allergy] [text] NULL,
	[Vaccination] [text] NULL,
	[Sterilized] [bit] NOT NULL,
	[Care_recommendation] [text] NULL,
 CONSTRAINT [PK__Medical___94A1C068042895CA] PRIMARY KEY CLUSTERED 
(
	[ID_medical_record] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[New_owner]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[New_owner](
	[ID_new_owner] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[First_name] [varchar](30) NOT NULL,
	[Patronymic] [varchar](50) NULL,
	[Date_of_birth] [date] NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[Phone_number] [varchar](15) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Housing_type] [int] NOT NULL,
	[Gender] [int] NOT NULL,
	[Contractor_type] [int] NOT NULL,
 CONSTRAINT [PK__New_owne__931D9955EEAF43F7] PRIMARY KEY CLUSTERED 
(
	[ID_new_owner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Position]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Position](
	[ID_position] [int] IDENTITY(1,1) NOT NULL,
	[Name_position] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Position__91FB1AE0E4D3E146] PRIMARY KEY CLUSTERED 
(
	[ID_position] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Source_of_receipt]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Source_of_receipt](
	[ID_source_of_receipt] [int] IDENTITY(1,1) NOT NULL,
	[Name_source_of_receipt] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Source_o__AD49C502446BC829] PRIMARY KEY CLUSTERED 
(
	[ID_source_of_receipt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Species]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Species](
	[ID_species] [int] IDENTITY(1,1) NOT NULL,
	[Name_species] [varchar](20) NOT NULL,
 CONSTRAINT [PK__Species__39DF0B150825746F] PRIMARY KEY CLUSTERED 
(
	[ID_species] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Task_status]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Task_status](
	[ID_task_status] [int] IDENTITY(1,1) NOT NULL,
	[Name_task_status] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Task_sta__ECA559C46710305D] PRIMARY KEY CLUSTERED 
(
	[ID_task_status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Veterinary_examination]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Veterinary_examination](
	[ID_veterinary_examination] [int] IDENTITY(1,1) NOT NULL,
	[Date_of_veterinary_examination] [date] NOT NULL,
	[Medical_record] [int] NOT NULL,
	[Veterinarian] [int] NOT NULL,
	[Conclusion] [text] NOT NULL,
	[Recommendation] [text] NULL,
 CONSTRAINT [PK__Veterina__3E75C14EB14DD632] PRIMARY KEY CLUSTERED 
(
	[ID_veterinary_examination] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Volunteer]    Script Date: 03.06.2025 16:47:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Volunteer](
	[ID_volunteer] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[First_name] [varchar](30) NOT NULL,
	[Patronymic] [varchar](50) NULL,
	[Gender] [int] NOT NULL,
	[Date_of_birth] [date] NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[Phone_number] [varchar](15) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Date_of_hire] [date] NOT NULL,
	[Login] [varchar](20) NULL,
	[Password] [varchar](40) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK__Voluntee__1096E16985C58145] PRIMARY KEY CLUSTERED 
(
	[ID_volunteer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Adoption] ON 

INSERT [dbo].[Adoption] ([ID_adoption], [Animal], [New_owner], [Date_of_adoption], [Contract], [Adoption_status], [Comment], [ContractPDF]) VALUES (1, 3, 4, CAST(N'2025-03-03' AS Date), N'C:\Users\acer\Desktop\AnimalsShelterDocuments\Сгенерированный договор усыновления_ID_1_Эшли.pdf', 4, N'', NULL)
INSERT [dbo].[Adoption] ([ID_adoption], [Animal], [New_owner], [Date_of_adoption], [Contract], [Adoption_status], [Comment], [ContractPDF]) VALUES (2, 3, 4, CAST(N'2025-01-01' AS Date), N'C:\Users\acer\Desktop\AnimalsShelterDocuments\Сгенерированный договор усыновления_ID_2_Эшли.docx', 1, N'', NULL)
INSERT [dbo].[Adoption] ([ID_adoption], [Animal], [New_owner], [Date_of_adoption], [Contract], [Adoption_status], [Comment], [ContractPDF]) VALUES (3, 10, 6, CAST(N'2025-06-02' AS Date), N'C:\Users\acer\Desktop\AnimalsShelterDocuments\Сгенерированный договор усыновления_ID_3_Берри.pdf', 2, N'', NULL)
SET IDENTITY_INSERT [dbo].[Adoption] OFF
SET IDENTITY_INSERT [dbo].[Adoption_status] ON 

INSERT [dbo].[Adoption_status] ([ID_adoption_status], [Name_adoption_status]) VALUES (1, N'На рассмотрении')
INSERT [dbo].[Adoption_status] ([ID_adoption_status], [Name_adoption_status]) VALUES (2, N'Одобрено')
INSERT [dbo].[Adoption_status] ([ID_adoption_status], [Name_adoption_status]) VALUES (3, N'Ожидание подписание договора')
INSERT [dbo].[Adoption_status] ([ID_adoption_status], [Name_adoption_status]) VALUES (4, N'Завершено')
INSERT [dbo].[Adoption_status] ([ID_adoption_status], [Name_adoption_status]) VALUES (5, N'Отказано')
SET IDENTITY_INSERT [dbo].[Adoption_status] OFF
SET IDENTITY_INSERT [dbo].[Animal] ON 

INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (3, 1, 1, 4, 1, N'Эшли', N'C:\Users\acer\Pictures\Saved Pictures\vecteezy_ai-generated-dog-on-transparent-background_35672431.png', CAST(N'2023-01-01' AS Date), CAST(N'2024-01-01' AS Date), 1, 1, N'изменить')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (4, 9, 1, NULL, 2, N'Белка', N'C:\Users\acer\Pictures\Картинки животных\соб2.jpg', CAST(N'2022-01-01' AS Date), CAST(N'2024-02-01' AS Date), 3, 1, N'')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (5, 3, 2, NULL, 2, N'Колин', N'C:\Users\acer\Pictures\Картинки животных\кот 1.jpg', CAST(N'2024-01-01' AS Date), CAST(N'2025-01-02' AS Date), 1, 1, NULL)
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (7, 3, 2, NULL, 1, N'Райли', N'C:\Users\acer\Pictures\Картинки животных\кот 2.jpg', CAST(N'2020-01-01' AS Date), CAST(N'2024-01-01' AS Date), 1, 1, NULL)
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (10, 5, 1, NULL, 2, N'Берри', N'C:\Users\acer\Pictures\Saved Pictures\vecteezy_welsh-corgi-dog-smiling-ai-generative_29721967.png', CAST(N'2025-01-01' AS Date), CAST(N'2025-02-03' AS Date), 1, 1, N'Корги')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (11, 9, 1, 5, 1, N'Мэй', N'C:\Users\acer\Pictures\Картинки животных\соб4.jpg', CAST(N'2024-01-01' AS Date), CAST(N'2024-05-04' AS Date), 1, 1, N'')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (12, 11, 1, NULL, 1, N'Злата', N'C:\Users\acer\Pictures\Картинки животных\соб5.jpg', CAST(N'2024-01-01' AS Date), CAST(N'2024-03-03' AS Date), 1, 1, NULL)
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (13, 13, 2, NULL, 2, N'Юви', N'C:\Users\acer\Pictures\Картинки животных\кот 3.jpg', CAST(N'2020-01-01' AS Date), CAST(N'2024-03-03' AS Date), 2, 1, NULL)
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (14, 9, 2, NULL, 1, N'Тася', N'C:\Users\acer\Pictures\Saved Pictures\938-9385766_cat.png', CAST(N'2022-01-01' AS Date), CAST(N'2023-02-20' AS Date), 1, 1, N'')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (17, 15, 1, NULL, 2, N'Файт', N'C:\Users\acer\Pictures\Картинки животных\соб6.jpg', CAST(N'2022-01-01' AS Date), CAST(N'2024-03-03' AS Date), 6, 1, NULL)
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (19, 16, 1, NULL, 1, N'Бетти', NULL, CAST(N'2024-01-01' AS Date), CAST(N'2024-03-01' AS Date), 1, 2, N'')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (20, 16, 1, 21, 2, N'test', NULL, CAST(N'2025-01-21' AS Date), CAST(N'2025-01-01' AS Date), 6, 4, N'testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest1testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest2testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest3testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest4testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest5testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttestv6testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttestv7testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttestvvv8testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest 9 testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest 10.')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (21, 2, 1, 4, 2, N'Тестовое животное', NULL, CAST(N'2012-12-12' AS Date), CAST(N'2025-05-15' AS Date), 2, 2, N'Тестовое животное')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (22, 2, 1, 4, 2, N'Тестовое животное', N'C:\Users\acer\Pictures\Saved Pictures\png-transparent-pet-sitting-dog-toys-cat-dog-walking-dog-park-thumbnail.png', CAST(N'0001-01-18' AS Date), CAST(N'0001-01-13' AS Date), 2, 2, N'Тестовое животное')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (23, 2, 1, 4, 2, N'Тест', N'C:\Users\acer\Pictures\Saved Pictures\kindpng_321453.png', CAST(N'0001-01-19' AS Date), CAST(N'0001-01-19' AS Date), 2, 2, N'Тест')
INSERT [dbo].[Animal] ([ID_animal], [Volunteer], [Species], [Breed], [Gender], [Nickname], [Photo], [Date_of_birth], [Date_of_registration], [Source_of_receipt], [Animal_status], [Note]) VALUES (24, 5, 1, 4, 2, N'Рекс', N'C:\Users\acer\Pictures\Saved Pictures\kindpng_3867320.png', CAST(N'2025-02-03' AS Date), CAST(N'2025-06-03' AS Date), 5, 1, N'')
SET IDENTITY_INSERT [dbo].[Animal] OFF
SET IDENTITY_INSERT [dbo].[Animal_status] ON 

INSERT [dbo].[Animal_status] ([ID_animal_status], [Name_animal_status]) VALUES (1, N'В приюте')
INSERT [dbo].[Animal_status] ([ID_animal_status], [Name_animal_status]) VALUES (2, N'Пристроено')
INSERT [dbo].[Animal_status] ([ID_animal_status], [Name_animal_status]) VALUES (3, N'На лечении')
INSERT [dbo].[Animal_status] ([ID_animal_status], [Name_animal_status]) VALUES (4, N'Умерло')
SET IDENTITY_INSERT [dbo].[Animal_status] OFF
SET IDENTITY_INSERT [dbo].[Breed] ON 

INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (4, 1, N'Бигль')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (5, 1, N'Бишон фризе')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (6, 1, N'Бультерьер')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (7, 1, N'Джек-рассел-терьер')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (8, 1, N'Доберман')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (9, 1, N'Мальтипу')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (10, 1, N'Помски')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (12, 2, N'Абиссинская')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (13, 2, N'Американский бобтейл')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (14, 2, N'Гималайская')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (15, 2, N'Девон-рекс')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (16, 2, N'Канадский сфинкс')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (17, 2, N'Манчкин')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (18, 2, N'Ориентальная')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (19, 2, N'Русская голубая')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (20, 2, N'Турецкая ангора')
INSERT [dbo].[Breed] ([ID_breed], [Species], [Name_breed]) VALUES (21, 2, N'Шотландская вислоухая')
SET IDENTITY_INSERT [dbo].[Breed] OFF
SET IDENTITY_INSERT [dbo].[Care_log] ON 

INSERT [dbo].[Care_log] ([ID_care_log], [Date_care_log], [Employee], [Volunteer], [Animal], [Care_type], [Task_status], [Comment]) VALUES (1, CAST(N'2025-02-28' AS Date), NULL, 1, 3, 1, 3, NULL)
INSERT [dbo].[Care_log] ([ID_care_log], [Date_care_log], [Employee], [Volunteer], [Animal], [Care_type], [Task_status], [Comment]) VALUES (2, CAST(N'2025-02-28' AS Date), NULL, 2, 4, 2, 3, NULL)
INSERT [dbo].[Care_log] ([ID_care_log], [Date_care_log], [Employee], [Volunteer], [Animal], [Care_type], [Task_status], [Comment]) VALUES (3, CAST(N'2025-03-01' AS Date), NULL, 3, 5, 1, 3, NULL)
INSERT [dbo].[Care_log] ([ID_care_log], [Date_care_log], [Employee], [Volunteer], [Animal], [Care_type], [Task_status], [Comment]) VALUES (4, CAST(N'2025-03-01' AS Date), NULL, 1, 3, 1, 3, NULL)
INSERT [dbo].[Care_log] ([ID_care_log], [Date_care_log], [Employee], [Volunteer], [Animal], [Care_type], [Task_status], [Comment]) VALUES (6, CAST(N'2025-03-02' AS Date), NULL, 9, 11, 3, 3, NULL)
SET IDENTITY_INSERT [dbo].[Care_log] OFF
SET IDENTITY_INSERT [dbo].[Care_type] ON 

INSERT [dbo].[Care_type] ([ID_care_type], [Name_care_type]) VALUES (1, N'Кормление')
INSERT [dbo].[Care_type] ([ID_care_type], [Name_care_type]) VALUES (2, N'Смена воды')
INSERT [dbo].[Care_type] ([ID_care_type], [Name_care_type]) VALUES (3, N'Выгул')
INSERT [dbo].[Care_type] ([ID_care_type], [Name_care_type]) VALUES (4, N'Чистка вольера/места содержания')
INSERT [dbo].[Care_type] ([ID_care_type], [Name_care_type]) VALUES (5, N'Гигиенические процедуры')
SET IDENTITY_INSERT [dbo].[Care_type] OFF
SET IDENTITY_INSERT [dbo].[Contractor] ON 

INSERT [dbo].[Contractor] ([ID_contractor], [Contractor_type], [Name], [Contact_person], [Phone_number], [Email], [Address], [INN]) VALUES (1, 1, N'Харитонова Вероника Станиславовна', NULL, N'8(902)022-02-02', N'kajsna@mail.ru', N'Москва', N'848382950492')
INSERT [dbo].[Contractor] ([ID_contractor], [Contractor_type], [Name], [Contact_person], [Phone_number], [Email], [Address], [INN]) VALUES (2, 2, N'Ветеринарный центр "Радениса"', N'Иванов Григорий Тимофеевич', N'8(939)929-92-93', N'vetraden@mail.ru', N'Зеленоград', N'849204983741')
INSERT [dbo].[Contractor] ([ID_contractor], [Contractor_type], [Name], [Contact_person], [Phone_number], [Email], [Address], [INN]) VALUES (3, 2, N'Сеть ветеринарных клиник "Пульс"', N'Виноградова Дарья Дмитриевна', N'8(965)525-25-25', N'puls13@mail.ru', N'Москва', N'846374826374')
INSERT [dbo].[Contractor] ([ID_contractor], [Contractor_type], [Name], [Contact_person], [Phone_number], [Email], [Address], [INN]) VALUES (9, 2, N'Фонд "Берегиня"', N'Мальцева Дарья Родионовна', N'8(903)022-02-02', N'jsdhfh@mail.ru', N'Москва', N'836574839201')
INSERT [dbo].[Contractor] ([ID_contractor], [Contractor_type], [Name], [Contact_person], [Phone_number], [Email], [Address], [INN]) VALUES (10, 1, N'Касаткин Егор Дмитриевич', NULL, N'8(929)949-94-94', N'ojdf3@mail.ru', N'Москва', N'029375647382')
SET IDENTITY_INSERT [dbo].[Contractor] OFF
SET IDENTITY_INSERT [dbo].[Contractor_type] ON 

INSERT [dbo].[Contractor_type] ([ID_contractor_type], [Name_contractor_type]) VALUES (1, N'Физическое лицо')
INSERT [dbo].[Contractor_type] ([ID_contractor_type], [Name_contractor_type]) VALUES (2, N'Юридическое лицо')
SET IDENTITY_INSERT [dbo].[Contractor_type] OFF
SET IDENTITY_INSERT [dbo].[Donation] ON 

INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (1, CAST(N'2025-01-01' AS Date), 1, NULL, 1, CAST(5000.00 AS Decimal(10, 2)), N'комментарий')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (2, CAST(N'2025-02-03' AS Date), 2, NULL, 3, NULL, N'')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (3, CAST(N'2025-03-04' AS Date), 3, NULL, 2, NULL, NULL)
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (4, CAST(N'2025-03-05' AS Date), 9, NULL, 3, NULL, NULL)
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (5, CAST(N'2025-03-06' AS Date), 10, NULL, 1, CAST(3000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (7, CAST(N'2025-05-10' AS Date), NULL, 1, 2, NULL, N'Пожертвование не от волонтера')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (12, CAST(N'2025-03-07' AS Date), 1, NULL, 1, CAST(1500.00 AS Decimal(10, 2)), N'Первый донор')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (13, CAST(N'2025-05-10' AS Date), NULL, 1, 2, NULL, N'Пожертвование от волонтера')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (14, CAST(N'2025-05-11' AS Date), NULL, 2, 3, NULL, N'Пожертвование от друга')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (15, CAST(N'2025-05-12' AS Date), 9, NULL, 1, CAST(2500.00 AS Decimal(10, 2)), N'Помощь для приюта')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (16, CAST(N'2025-05-13' AS Date), 3, NULL, 2, NULL, N'Анонимное пожертвование')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (17, CAST(N'2025-05-14' AS Date), 2, NULL, 1, CAST(4000.00 AS Decimal(10, 2)), N'Ежемесячный взнос')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (18, CAST(N'2025-05-15' AS Date), 1, NULL, 1, CAST(6000.00 AS Decimal(10, 2)), N'Пожертвование от бизнеса')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (19, CAST(N'2025-05-16' AS Date), NULL, 3, 2, NULL, N'На нужды проекта')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (20, CAST(N'2025-05-17' AS Date), 10, NULL, 1, CAST(10000.00 AS Decimal(10, 2)), N'Поддержка благотворительности')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (21, CAST(N'2025-05-13' AS Date), 9, NULL, 1, CAST(5000.00 AS Decimal(10, 2)), N'Проверка пожертвования')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (22, CAST(N'2025-05-13' AS Date), NULL, 16, 2, NULL, N'корм для кошек 3 кг')
INSERT [dbo].[Donation] ([ID_donation], [Date_of_donation], [Contractor], [Volunteer], [Donation_type], [Amount], [Description]) VALUES (23, CAST(N'2025-05-13' AS Date), NULL, 2, 1, CAST(500.00 AS Decimal(10, 2)), N'')
SET IDENTITY_INSERT [dbo].[Donation] OFF
SET IDENTITY_INSERT [dbo].[Donation_type] ON 

INSERT [dbo].[Donation_type] ([ID_donation_type], [Name_donation_type]) VALUES (1, N'Денежные средства')
INSERT [dbo].[Donation_type] ([ID_donation_type], [Name_donation_type]) VALUES (2, N'Корма и питание')
INSERT [dbo].[Donation_type] ([ID_donation_type], [Name_donation_type]) VALUES (3, N'Медикаменты и ветеринарная помощь')
INSERT [dbo].[Donation_type] ([ID_donation_type], [Name_donation_type]) VALUES (4, N'Амуниция и аксессуары')
INSERT [dbo].[Donation_type] ([ID_donation_type], [Name_donation_type]) VALUES (5, N'Игрушки')
INSERT [dbo].[Donation_type] ([ID_donation_type], [Name_donation_type]) VALUES (6, N'Подстилки и клетки')
INSERT [dbo].[Donation_type] ([ID_donation_type], [Name_donation_type]) VALUES (7, N'Строительные материалы и оборудование')
SET IDENTITY_INSERT [dbo].[Donation_type] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([ID_employee], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Position], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password]) VALUES (2, N'Иванова', N'Мария', N'Алексеевна', 1, CAST(N'2000-09-04' AS Date), 1, N'Москва', N'8(903)123-23-23', N'ivanova@mail.ru', CAST(N'2024-03-30' AS Date), N'admin', N'123')
INSERT [dbo].[Employee] ([ID_employee], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Position], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password]) VALUES (3, N'Сидорова', N'Виктория', N'Евгеньевна', 1, CAST(N'1998-10-13' AS Date), 2, N'Москва', N'8(923)143-43-43', N'lksfad@mail.ru', CAST(N'2023-03-29' AS Date), N'vet', N'1234')
INSERT [dbo].[Employee] ([ID_employee], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Position], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password]) VALUES (4, N'Морозова', N'София', N'Викторовна', 1, CAST(N'2000-10-13' AS Date), 3, N'Москва', N'8(939)939-93-93', N'moroz@mail.ru', CAST(N'2023-03-28' AS Date), N'vetas', N'12345')
INSERT [dbo].[Employee] ([ID_employee], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Position], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password]) VALUES (5, N'Назаров', N'Георгий', N'Георгиевич', 2, CAST(N'2000-10-13' AS Date), 3, N'Москва', N'8(903)234-34-34', N'nzrf@mail.ru', CAST(N'2023-02-28' AS Date), N'vetass1', N'12345')
INSERT [dbo].[Employee] ([ID_employee], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Position], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password]) VALUES (7, N'Яковлева', N'Софья', N'Викторовна', 1, CAST(N'2001-02-03' AS Date), 4, N'Москва', N'8(902)020-02-02', N'sofa3@mail.ru', CAST(N'2022-06-12' AS Date), N'kurator1', N'12345')
INSERT [dbo].[Employee] ([ID_employee], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Position], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password]) VALUES (8, N'Королева', N'Кира', N'Александровна', 1, CAST(N'1990-04-03' AS Date), 4, N'Москва', N'8(932)120-12-12', N'krrr@mail.ru', CAST(N'2022-09-09' AS Date), N'kurator2', N'12345')
INSERT [dbo].[Employee] ([ID_employee], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Position], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password]) VALUES (9, N'Михайлова', N'Алена', N'Руслановна', 1, CAST(N'1994-04-03' AS Date), 4, N'Москва', N'8(939)022-22-22', N'sjdfsd@mail.ru', CAST(N'2021-09-09' AS Date), N'kurator3', N'12345')
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[Gender] ON 

INSERT [dbo].[Gender] ([ID_gender], [Name_gender]) VALUES (1, N'Женский')
INSERT [dbo].[Gender] ([ID_gender], [Name_gender]) VALUES (2, N'Мужской')
SET IDENTITY_INSERT [dbo].[Gender] OFF
SET IDENTITY_INSERT [dbo].[Housing_type] ON 

INSERT [dbo].[Housing_type] ([ID_housing_type], [Name_housing_type]) VALUES (1, N'Квартира')
INSERT [dbo].[Housing_type] ([ID_housing_type], [Name_housing_type]) VALUES (2, N'Частный дом')
SET IDENTITY_INSERT [dbo].[Housing_type] OFF
SET IDENTITY_INSERT [dbo].[Medical_record] ON 

INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (1, 3, CAST(N'2025-03-20' AS Date), CAST(10.00 AS Decimal(5, 2)), CAST(40.00 AS Decimal(5, 2)), NULL, NULL, NULL, N'Комплексная', 0, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (3, 4, CAST(N'2025-06-03' AS Date), CAST(16.00 AS Decimal(5, 2)), CAST(52.50 AS Decimal(5, 2)), N'test2', N'test1', N'test3', N'Комплексная test4', 0, N'test5')
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (4, 5, CAST(N'2025-02-23' AS Date), CAST(4.00 AS Decimal(5, 2)), CAST(30.00 AS Decimal(5, 2)), NULL, NULL, NULL, N'Комплексная', 1, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (6, 7, CAST(N'2025-03-30' AS Date), CAST(4.50 AS Decimal(5, 2)), CAST(30.00 AS Decimal(5, 2)), NULL, NULL, NULL, N'Комплексная', 1, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (8, 10, CAST(N'2025-01-01' AS Date), CAST(11.00 AS Decimal(5, 2)), CAST(45.00 AS Decimal(5, 2)), NULL, NULL, NULL, N'Комплексная', 0, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (10, 11, CAST(N'2025-02-11' AS Date), CAST(22.00 AS Decimal(5, 2)), CAST(73.00 AS Decimal(5, 2)), NULL, NULL, NULL, N'Комплексная', 1, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (11, 12, CAST(N'2025-03-03' AS Date), CAST(12.00 AS Decimal(5, 2)), CAST(70.00 AS Decimal(5, 2)), NULL, NULL, NULL, N'Комплексная', 1, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (12, 13, CAST(N'2024-12-12' AS Date), CAST(3.50 AS Decimal(5, 2)), CAST(31.00 AS Decimal(5, 2)), NULL, NULL, NULL, N'Комплексная', 1, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (13, 14, CAST(N'2025-02-06' AS Date), CAST(3.00 AS Decimal(5, 2)), CAST(32.00 AS Decimal(5, 2)), NULL, NULL, NULL, N'Комплексная', 1, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (14, 17, CAST(N'2025-02-23' AS Date), CAST(25.00 AS Decimal(5, 2)), CAST(67.00 AS Decimal(5, 2)), N'Мастоцитома', N'Прошел химиотерапию, сейчас в ремиссии. Наблюдается у врача и регулярно ездит сдавать анализы', NULL, N'Комплексная', 1, NULL)
INSERT [dbo].[Medical_record] ([ID_medical_record], [Animal], [Last_update_date], [Weight], [Height], [Chronic_illness], [Medical_history], [Allergy], [Vaccination], [Sterilized], [Care_recommendation]) VALUES (15, 19, CAST(N'2025-06-03' AS Date), CAST(12.00 AS Decimal(5, 2)), CAST(45.00 AS Decimal(5, 2)), NULL, NULL, NULL, NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[Medical_record] OFF
SET IDENTITY_INSERT [dbo].[New_owner] ON 

INSERT [dbo].[New_owner] ([ID_new_owner], [Surname], [First_name], [Patronymic], [Date_of_birth], [Address], [Phone_number], [Email], [Housing_type], [Gender], [Contractor_type]) VALUES (1, N'Козловский', N'Дмитрий', N'Арсентьевич', CAST(N'1998-04-23' AS Date), N'Москва', N'8(903)829-29-29', N'dmm2@mail.ru', 1, 2, 2)
INSERT [dbo].[New_owner] ([ID_new_owner], [Surname], [First_name], [Patronymic], [Date_of_birth], [Address], [Phone_number], [Email], [Housing_type], [Gender], [Contractor_type]) VALUES (2, N'Куликова', N'Валерия', N'Георгиевна', CAST(N'2000-07-29' AS Date), N'Москва', N'8(903)344-34-34', N'lerch123@mail.ru', 1, 1, 2)
INSERT [dbo].[New_owner] ([ID_new_owner], [Surname], [First_name], [Patronymic], [Date_of_birth], [Address], [Phone_number], [Email], [Housing_type], [Gender], [Contractor_type]) VALUES (4, N'Гусев', N'Кирилл', N'Демидович', CAST(N'2000-01-04' AS Date), N'Москва', N'8(930)033-03-03', N'gusev@mail.ru', 2, 2, 2)
INSERT [dbo].[New_owner] ([ID_new_owner], [Surname], [First_name], [Patronymic], [Date_of_birth], [Address], [Phone_number], [Email], [Housing_type], [Gender], [Contractor_type]) VALUES (5, N'Крюков', N'Александр', N'Александрович', CAST(N'2001-12-08' AS Date), N'Москва', N'8(939)099-09-09', N'kryukv@mail.ru', 1, 2, 2)
INSERT [dbo].[New_owner] ([ID_new_owner], [Surname], [First_name], [Patronymic], [Date_of_birth], [Address], [Phone_number], [Email], [Housing_type], [Gender], [Contractor_type]) VALUES (6, N'Агапова', N'Виктория', N'Борисовна', CAST(N'1997-09-09' AS Date), N'Москва', N'8(939)030-03-03', N'agapova@mail.ru', 2, 1, 1)
SET IDENTITY_INSERT [dbo].[New_owner] OFF
SET IDENTITY_INSERT [dbo].[Position] ON 

INSERT [dbo].[Position] ([ID_position], [Name_position]) VALUES (1, N'Администратор')
INSERT [dbo].[Position] ([ID_position], [Name_position]) VALUES (2, N'Ветеринар')
INSERT [dbo].[Position] ([ID_position], [Name_position]) VALUES (3, N'Ассистент ветеринара')
INSERT [dbo].[Position] ([ID_position], [Name_position]) VALUES (4, N'Куратор')
SET IDENTITY_INSERT [dbo].[Position] OFF
SET IDENTITY_INSERT [dbo].[Source_of_receipt] ON 

INSERT [dbo].[Source_of_receipt] ([ID_source_of_receipt], [Name_source_of_receipt]) VALUES (1, N'Найдено')
INSERT [dbo].[Source_of_receipt] ([ID_source_of_receipt], [Name_source_of_receipt]) VALUES (2, N'Передано от владельцев')
INSERT [dbo].[Source_of_receipt] ([ID_source_of_receipt], [Name_source_of_receipt]) VALUES (3, N'Изъятие из неблагоприятных условий')
INSERT [dbo].[Source_of_receipt] ([ID_source_of_receipt], [Name_source_of_receipt]) VALUES (4, N'Перевод из другого приюта')
INSERT [dbo].[Source_of_receipt] ([ID_source_of_receipt], [Name_source_of_receipt]) VALUES (5, N'Рождение в приюте')
INSERT [dbo].[Source_of_receipt] ([ID_source_of_receipt], [Name_source_of_receipt]) VALUES (6, N'Передача от зоозащитной организации')
SET IDENTITY_INSERT [dbo].[Source_of_receipt] OFF
SET IDENTITY_INSERT [dbo].[Species] ON 

INSERT [dbo].[Species] ([ID_species], [Name_species]) VALUES (1, N'Собака')
INSERT [dbo].[Species] ([ID_species], [Name_species]) VALUES (2, N'Кошка')
SET IDENTITY_INSERT [dbo].[Species] OFF
SET IDENTITY_INSERT [dbo].[Task_status] ON 

INSERT [dbo].[Task_status] ([ID_task_status], [Name_task_status]) VALUES (1, N'Назначено')
INSERT [dbo].[Task_status] ([ID_task_status], [Name_task_status]) VALUES (2, N'В процессе')
INSERT [dbo].[Task_status] ([ID_task_status], [Name_task_status]) VALUES (3, N'Выполнено')
SET IDENTITY_INSERT [dbo].[Task_status] OFF
SET IDENTITY_INSERT [dbo].[Veterinary_examination] ON 

INSERT [dbo].[Veterinary_examination] ([ID_veterinary_examination], [Date_of_veterinary_examination], [Medical_record], [Veterinarian], [Conclusion], [Recommendation]) VALUES (1, CAST(N'2025-03-20' AS Date), 1, 3, N'Проведена вакцинация. Со здоровьем все хорошо', NULL)
INSERT [dbo].[Veterinary_examination] ([ID_veterinary_examination], [Date_of_veterinary_examination], [Medical_record], [Veterinarian], [Conclusion], [Recommendation]) VALUES (2, CAST(N'2025-02-23' AS Date), 3, 3, N'Проведена стерилизация. Со здоровьем все хорошо', NULL)
INSERT [dbo].[Veterinary_examination] ([ID_veterinary_examination], [Date_of_veterinary_examination], [Medical_record], [Veterinarian], [Conclusion], [Recommendation]) VALUES (3, CAST(N'2025-02-23' AS Date), 4, 3, N'Проведен плановый осмотр. Со здоровьем все хорошо', NULL)
INSERT [dbo].[Veterinary_examination] ([ID_veterinary_examination], [Date_of_veterinary_examination], [Medical_record], [Veterinarian], [Conclusion], [Recommendation]) VALUES (4, CAST(N'2025-03-30' AS Date), 6, 3, N'Проведен плановый осмотр. Со здоровьем все хорошо', NULL)
SET IDENTITY_INSERT [dbo].[Veterinary_examination] OFF
SET IDENTITY_INSERT [dbo].[Volunteer] ON 

INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (1, N'Медведева', N'Дарья', N'Михайловна', 1, CAST(N'2000-01-21' AS Date), N'Москва', N'8(920)111-11-11', N'meda@gmail.com', CAST(N'2024-01-09' AS Date), N'meda123', N'1234wewf', 1)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (2, N'Осипова', N'Елизавета', N'Юрьевна', 1, CAST(N'2005-01-03' AS Date), N'Москва', N'8(920)222-22-22', N'osip823@mail.ru', CAST(N'2024-09-08' AS Date), N'osip093', N'f0923r9i', 1)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (3, N'Соколов', N'Евгений', N'Максимович', 2, CAST(N'2000-02-15' AS Date), N'Москва', N'8(920)333-33-33', N'sokol@gmail.com', CAST(N'2023-03-21' AS Date), N'sokol324', N'fmlkj3ri3', 1)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (5, N'Семёнова', N'Елизавета', N'Ивановна', 1, CAST(N'2001-02-02' AS Date), N'Москва', N'8(920)444-44-44', N'sdffe@gmail.com', CAST(N'2023-03-02' AS Date), N'lizz2442', N'0v-0t24ds', 1)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (9, N'Иванова', N'Вера', N'Максимовна', 1, CAST(N'2004-02-04' AS Date), N'Москва', N'8(920)555-55-55', N'vera234@gmail.com', CAST(N'2024-06-22' AS Date), N'vera012', N'fjwor23984', 1)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (11, N'Фёдорова', N'Алина', N'Павловна', 1, CAST(N'1994-07-19' AS Date), N'Москва', N'8(920)666-66-66', N'sklfjj3aw@gmail.com', CAST(N'2021-09-23' AS Date), N'faamv312', N'lkvjweijr3', 1)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (13, N'Маркин', N'Семён', N'Егорович', 2, CAST(N'1999-10-13' AS Date), N'Москва', N'8(920)777-77-77', N'smdf@mail.ru', CAST(N'2023-10-31' AS Date), N'mark88283', N'sdf124d', 0)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (14, N'Макарова', N'Полина', N'Константиновна', 1, CAST(N'1999-08-08' AS Date), N'Москва', N'8(920)888-88-88', N'polksdjsk@gmail.com', CAST(N'2023-11-11' AS Date), N'polya87624', N'fwerw432', 0)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (15, N'Николаев', N'Георгий', N'Савельевич', 2, CAST(N'2005-04-29' AS Date), N'Москва', N'8(920)999-99-99', N'nikola@gmail.com', CAST(N'2024-08-22' AS Date), N'nikola39i', N'lsdfjwlejrie3', 1)
INSERT [dbo].[Volunteer] ([ID_volunteer], [Surname], [First_name], [Patronymic], [Gender], [Date_of_birth], [Address], [Phone_number], [Email], [Date_of_hire], [Login], [Password], [Active]) VALUES (16, N'Тихонова', N'Алиса', N'Кирилловна', 1, CAST(N'2006-12-05' AS Date), N'Москва', N'8(920)100-10-10', N'alisa1993jdi@gmail.com', CAST(N'2024-03-19' AS Date), N'alisss838', N'sfwer234', 0)
SET IDENTITY_INSERT [dbo].[Volunteer] OFF
ALTER TABLE [dbo].[Adoption]  WITH CHECK ADD  CONSTRAINT [FK_Adoption_Adoption_status] FOREIGN KEY([Adoption_status])
REFERENCES [dbo].[Adoption_status] ([ID_adoption_status])
GO
ALTER TABLE [dbo].[Adoption] CHECK CONSTRAINT [FK_Adoption_Adoption_status]
GO
ALTER TABLE [dbo].[Adoption]  WITH CHECK ADD  CONSTRAINT [FK_Adoption_Animal] FOREIGN KEY([Animal])
REFERENCES [dbo].[Animal] ([ID_animal])
GO
ALTER TABLE [dbo].[Adoption] CHECK CONSTRAINT [FK_Adoption_Animal]
GO
ALTER TABLE [dbo].[Adoption]  WITH CHECK ADD  CONSTRAINT [FK_Adoption_New_owner] FOREIGN KEY([New_owner])
REFERENCES [dbo].[New_owner] ([ID_new_owner])
GO
ALTER TABLE [dbo].[Adoption] CHECK CONSTRAINT [FK_Adoption_New_owner]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Animal_Animal_status] FOREIGN KEY([Animal_status])
REFERENCES [dbo].[Animal_status] ([ID_animal_status])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Animal_Animal_status]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Animal_Breed] FOREIGN KEY([Breed])
REFERENCES [dbo].[Breed] ([ID_breed])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Animal_Breed]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Animal_Gender] FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([ID_gender])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Animal_Gender]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Animal_Source_of_receipt] FOREIGN KEY([Source_of_receipt])
REFERENCES [dbo].[Source_of_receipt] ([ID_source_of_receipt])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Animal_Source_of_receipt]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Animal_Species] FOREIGN KEY([Species])
REFERENCES [dbo].[Species] ([ID_species])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Animal_Species]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Animal_Volunteer] FOREIGN KEY([Volunteer])
REFERENCES [dbo].[Volunteer] ([ID_volunteer])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Animal_Volunteer]
GO
ALTER TABLE [dbo].[Breed]  WITH CHECK ADD  CONSTRAINT [FK_Breed_Species] FOREIGN KEY([Species])
REFERENCES [dbo].[Species] ([ID_species])
GO
ALTER TABLE [dbo].[Breed] CHECK CONSTRAINT [FK_Breed_Species]
GO
ALTER TABLE [dbo].[Care_log]  WITH CHECK ADD  CONSTRAINT [FK_Care_log_Animal] FOREIGN KEY([Animal])
REFERENCES [dbo].[Animal] ([ID_animal])
GO
ALTER TABLE [dbo].[Care_log] CHECK CONSTRAINT [FK_Care_log_Animal]
GO
ALTER TABLE [dbo].[Care_log]  WITH CHECK ADD  CONSTRAINT [FK_Care_log_Care_type] FOREIGN KEY([Care_type])
REFERENCES [dbo].[Care_type] ([ID_care_type])
GO
ALTER TABLE [dbo].[Care_log] CHECK CONSTRAINT [FK_Care_log_Care_type]
GO
ALTER TABLE [dbo].[Care_log]  WITH CHECK ADD  CONSTRAINT [FK_Care_log_Employee] FOREIGN KEY([Employee])
REFERENCES [dbo].[Employee] ([ID_employee])
GO
ALTER TABLE [dbo].[Care_log] CHECK CONSTRAINT [FK_Care_log_Employee]
GO
ALTER TABLE [dbo].[Care_log]  WITH CHECK ADD  CONSTRAINT [FK_Care_log_Task_status] FOREIGN KEY([Task_status])
REFERENCES [dbo].[Task_status] ([ID_task_status])
GO
ALTER TABLE [dbo].[Care_log] CHECK CONSTRAINT [FK_Care_log_Task_status]
GO
ALTER TABLE [dbo].[Care_log]  WITH CHECK ADD  CONSTRAINT [FK_Care_log_Volunteer] FOREIGN KEY([Volunteer])
REFERENCES [dbo].[Volunteer] ([ID_volunteer])
GO
ALTER TABLE [dbo].[Care_log] CHECK CONSTRAINT [FK_Care_log_Volunteer]
GO
ALTER TABLE [dbo].[Contractor]  WITH CHECK ADD  CONSTRAINT [FK_Contractor_Contractor_type] FOREIGN KEY([Contractor_type])
REFERENCES [dbo].[Contractor_type] ([ID_contractor_type])
GO
ALTER TABLE [dbo].[Contractor] CHECK CONSTRAINT [FK_Contractor_Contractor_type]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [FK_Donation_Contractor] FOREIGN KEY([Contractor])
REFERENCES [dbo].[Contractor] ([ID_contractor])
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [FK_Donation_Contractor]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [FK_Donation_Donation_type] FOREIGN KEY([Donation_type])
REFERENCES [dbo].[Donation_type] ([ID_donation_type])
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [FK_Donation_Donation_type]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [FK_Donation_Volunteer] FOREIGN KEY([Volunteer])
REFERENCES [dbo].[Volunteer] ([ID_volunteer])
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [FK_Donation_Volunteer]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Gender] FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([ID_gender])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Gender]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Position] FOREIGN KEY([Position])
REFERENCES [dbo].[Position] ([ID_position])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Position]
GO
ALTER TABLE [dbo].[Medical_record]  WITH CHECK ADD  CONSTRAINT [FK_Medical_record_Animal] FOREIGN KEY([Animal])
REFERENCES [dbo].[Animal] ([ID_animal])
GO
ALTER TABLE [dbo].[Medical_record] CHECK CONSTRAINT [FK_Medical_record_Animal]
GO
ALTER TABLE [dbo].[New_owner]  WITH CHECK ADD  CONSTRAINT [FK_New_owner_Contractor_type] FOREIGN KEY([Contractor_type])
REFERENCES [dbo].[Contractor_type] ([ID_contractor_type])
GO
ALTER TABLE [dbo].[New_owner] CHECK CONSTRAINT [FK_New_owner_Contractor_type]
GO
ALTER TABLE [dbo].[New_owner]  WITH CHECK ADD  CONSTRAINT [FK_New_owner_Gender] FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([ID_gender])
GO
ALTER TABLE [dbo].[New_owner] CHECK CONSTRAINT [FK_New_owner_Gender]
GO
ALTER TABLE [dbo].[New_owner]  WITH CHECK ADD  CONSTRAINT [FK_New_owner_Housing_type] FOREIGN KEY([Housing_type])
REFERENCES [dbo].[Housing_type] ([ID_housing_type])
GO
ALTER TABLE [dbo].[New_owner] CHECK CONSTRAINT [FK_New_owner_Housing_type]
GO
ALTER TABLE [dbo].[Veterinary_examination]  WITH CHECK ADD  CONSTRAINT [FK_Veterinary_examination_Employee] FOREIGN KEY([Veterinarian])
REFERENCES [dbo].[Employee] ([ID_employee])
GO
ALTER TABLE [dbo].[Veterinary_examination] CHECK CONSTRAINT [FK_Veterinary_examination_Employee]
GO
ALTER TABLE [dbo].[Veterinary_examination]  WITH CHECK ADD  CONSTRAINT [FK_Veterinary_examination_Medical_record] FOREIGN KEY([Medical_record])
REFERENCES [dbo].[Medical_record] ([ID_medical_record])
GO
ALTER TABLE [dbo].[Veterinary_examination] CHECK CONSTRAINT [FK_Veterinary_examination_Medical_record]
GO
ALTER TABLE [dbo].[Volunteer]  WITH CHECK ADD  CONSTRAINT [FK_Volunteer_Gender] FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([ID_gender])
GO
ALTER TABLE [dbo].[Volunteer] CHECK CONSTRAINT [FK_Volunteer_Gender]
GO
USE [master]
GO
ALTER DATABASE [AnimalShelter] SET  READ_WRITE 
GO
