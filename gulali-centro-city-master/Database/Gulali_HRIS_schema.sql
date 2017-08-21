USE [master]
GO
/****** Object:  Database [Gulali_HRIS]    Script Date: 8/11/2017 2:49:37 PM ******/
CREATE DATABASE [Gulali_HRIS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Gulali_HRIS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Gulali_HRIS.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Gulali_HRIS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Gulali_HRIS_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Gulali_HRIS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Gulali_HRIS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Gulali_HRIS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET ARITHABORT OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Gulali_HRIS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Gulali_HRIS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Gulali_HRIS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Gulali_HRIS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Gulali_HRIS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Gulali_HRIS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Gulali_HRIS] SET  MULTI_USER 
GO
ALTER DATABASE [Gulali_HRIS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Gulali_HRIS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Gulali_HRIS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Gulali_HRIS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Gulali_HRIS]
GO
/****** Object:  Table [dbo].[TBL_Absen]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Absen](
	[Absen_ID] [int] IDENTITY(1,1) NOT NULL,
	[Absen_Employee] [varchar](100) NULL,
	[Absen_Date] [datetime] NULL,
	[Absen_In] [datetime] NULL,
	[Absen_Out] [datetime] NULL,
	[Insert_Date] [datetime] NULL,
	[Symbol] [varchar](3) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Dashboard]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Dashboard](
	[Dashboard_ID] [int] IDENTITY(1,1) NOT NULL,
	[Dashboard_Name] [varchar](150) NULL,
	[Dashboard_Desc] [varchar](255) NULL,
	[Dashboard_Status] [int] NULL,
	[Dashboard_User] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Dashboard_Mapping]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Dashboard_Mapping](
	[Mapping_ID] [int] IDENTITY(1,1) NOT NULL,
	[Dashboard_ID] [int] NULL,
	[Mapping_User] [int] NULL,
	[Mapping_View] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBL_Dashboard_old]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Dashboard_old](
	[Dashboard_ID] [int] IDENTITY(1,1) NOT NULL,
	[Dashboard_Name] [varchar](150) NULL,
	[Dashboard_Desc] [varchar](255) NULL,
	[Dashboard_Status] [int] NULL,
	[Dashboard_User] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Department]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Department](
	[Department_ID] [int] IDENTITY(1,1) NOT NULL,
	[Department_Name] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Division]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Division](
	[Division_ID] [int] IDENTITY(1,1) NOT NULL,
	[Division_Name] [varchar](100) NULL,
	[Department_ID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee](
	[Employee_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_NIK] [varchar](50) NULL,
	[Employee_Full_Name] [varchar](200) NULL,
	[Employee_First_Name] [varchar](200) NULL,
	[Employee_Middle_Name] [varchar](200) NULL,
	[Employee_Last_Name] [varchar](200) NULL,
	[Employee_Alias_Name] [varchar](200) NULL,
	[Employee_Gender] [varchar](3) NULL,
	[Employee_Blood_Type] [int] NULL,
	[Employee_PlaceOfBirth] [varchar](200) NULL,
	[Employee_DateOfBirth] [datetime] NULL,
	[Employee_Phone_Number_Primary] [varchar](50) NULL,
	[Employee_Phone_Number] [varchar](50) NULL,
	[Employee_IDCard_Number] [varchar](50) NULL,
	[Employee_IDCard_Address] [varchar](255) NULL,
	[Employee_Domicile_Address] [varchar](255) NULL,
	[Employee_Marital_Status] [int] NULL,
	[Employee_Spouse_Name] [varchar](200) NULL,
	[Employee_SIM_A] [bit] NULL,
	[Employee_SIM_C] [bit] NULL,
	[Employee_Company_Email] [varchar](50) NULL,
	[Employee_Personal_Email] [varchar](50) NULL,
	[Employee_Education] [int] NULL,
	[Employee_Education_Major] [varchar](200) NULL,
	[Employee_Photo] [varchar](255) NULL,
	[Employee_DirectSpv] [int] NULL,
	[Employee_Bank_AccName_Primary] [varchar](50) NULL,
	[Employee_Bank_AccNumber_Primary] [varchar](50) NULL,
	[Employee_Bank_AccName] [varchar](50) NULL,
	[Employee_Bank_AccNumber] [varchar](50) NULL,
	[Employee_CV] [varchar](150) NULL,
	[Employee_StartofEmployment] [datetime] NULL,
	[Employee_JoinDate] [datetime] NULL,
	[Employee_EndDate] [datetime] NULL,
	[Employee_Sum_LeaveBalance] [float] NULL,
	[Employee_Sum_SickLeave] [float] NULL,
	[Religion_ID] [int] NULL,
	[Department_ID] [int] NULL,
	[Division_ID] [int] NULL,
	[Position_ID] [int] NULL,
	[Employee_DateCreate] [datetime] NULL,
	[Employee_DateLastModified] [datetime] NULL,
	[Employee_Inactive] [varchar](2) NULL,
	[Employee_Inactive_Date] [datetime] NULL,
	[Employee_Inactive_Remarks] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee_Contract]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee_Contract](
	[Contract_ID] [int] IDENTITY(1,1) NOT NULL,
	[Contract_StartPeriode] [datetime] NULL,
	[Contract_EndPeriode] [datetime] NULL,
	[Contract_Title] [varchar](100) NULL,
	[Contract_FileUpload] [varchar](150) NULL,
	[Contract_Remarks] [varchar](max) NULL,
	[Contract_DateCreate] [datetime] NULL,
	[Contract_LastUpdate] [datetime] NULL,
	[Contract_UserCreate] [int] NULL,
	[Employee_ID] [int] NULL,
	[remarks] [int] NULL,
 CONSTRAINT [PK_TBL_Employee_Contract] PRIMARY KEY CLUSTERED 
(
	[Contract_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee_Dependent]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee_Dependent](
	[Dependent_ID] [int] IDENTITY(1,1) NOT NULL,
	[Dependent_Name] [varchar](50) NULL,
	[Dependent_Gender] [varchar](2) NULL,
	[Dependent_PlaceofBirth] [varchar](50) NULL,
	[Dependent_DateofBirth] [datetime] NULL,
	[Dependent_Relationship] [varchar](50) NULL,
	[Employee_ID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee_Education]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee_Education](
	[Education_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
	[Education_1_Name] [varchar](255) NULL,
	[Education_1_Years] [varchar](50) NULL,
	[Education_1_Location] [text] NULL,
	[Education_2_Name] [varchar](255) NULL,
	[Education_2_Years] [varchar](50) NULL,
	[Education_2_Location] [text] NULL,
	[Education_3_Name] [varchar](255) NULL,
	[Education_3_Years] [varchar](50) NULL,
	[Education_3_Location] [text] NULL,
	[Education_4_Name] [varchar](255) NULL,
	[Education_4_Years] [varchar](50) NULL,
	[Education_4_Location] [text] NULL,
	[Education_5] [varchar](255) NULL,
	[Education_5_Name] [varchar](255) NULL,
	[Education_5_Years] [varchar](50) NULL,
	[Education_5_Location] [text] NULL,
	[Education_6] [varchar](255) NULL,
	[Education_6_Name] [varchar](255) NULL,
	[Education_6_Years] [varchar](50) NULL,
	[Education_6_Location] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee_Emergency_Contact]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee_Emergency_Contact](
	[Emergency_ID] [int] IDENTITY(1,1) NOT NULL,
	[Emergency_Name] [varchar](50) NULL,
	[Emergency_Relation] [varchar](50) NULL,
	[Emergency_Address] [varchar](max) NULL,
	[Emergency_Phone] [varchar](50) NULL,
	[Employee_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee_Payroll]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee_Payroll](
	[Payroll_ID] [int] IDENTITY(1,1) NOT NULL,
	[Payroll_NPWP] [int] NULL,
	[Payroll_NPWP_Number] [varchar](30) NULL,
	[Payroll_OthersAllowance] [float] NULL,
	[Payroll_Jamsostek] [float] NULL,
	[Payroll_BPJS_Pribadi] [int] NULL,
	[Payroll_BPJS_Pribadi_Value] [float] NULL,
	[Payroll_BPJS_Ditanggung] [int] NULL,
	[Payroll_SalaryPPH] [float] NULL,
	[Payroll_SalaryBPJS] [float] NULL,
	[Payroll_Group] [int] NULL,
	[Employee_ID] [int] NULL,
	[PTKP_ID] [int] NULL,
	[Payroll_SalaryBPJSTK] [float] NULL,
	[Payroll_UnpaidLeave] [decimal](20, 2) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee_Temp]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee_Temp](
	[Temp_ID] [int] NOT NULL,
	[User_uploader] [int] NULL,
	[Employee_Name] [varchar](50) NULL,
	[Employee_BCA_AccountNumber] [varchar](50) NULL,
	[Employee_NPWP_Number] [varchar](50) NULL,
	[NPWP] [varchar](10) NULL,
	[PTKP_Status] [varchar](4) NULL,
	[Employee_Department] [int] NULL,
	[Employee_Division] [int] NULL,
	[Payroll_Group] [int] NULL,
	[Employee_Religion] [int] NULL,
	[Employee_BirthDate] [datetime] NULL,
	[Employee_JoinDate] [datetime] NULL,
	[Employee_DateCreate] [datetime] NULL,
	[Employee_DateLastModified] [datetime] NULL,
	[Employee_Inactive] [varchar](max) NULL,
	[Employee_Gender] [varchar](2) NULL,
	[Employee_Blood_Type] [int] NULL,
	[Employee_NPP_BPJS_Ketenagakerjaan] [varchar](50) NULL,
	[Employee_NPP_BPJS_Kesehatan] [varchar](50) NULL,
	[HistorySalary_Value] [decimal](20, 2) NULL,
	[HistorySalary_DateChanged] [datetime] NULL,
	[salaryPPH] [decimal](20, 2) NULL,
	[salaryBPJS] [decimal](20, 2) NULL,
	[salaryBPJSTK] [decimal](20, 2) NULL,
	[BPJS_Pribadi] [varchar](3) NULL,
	[BPJS_Pribadi_Value] [decimal](20, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee_Warning]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee_Warning](
	[Warning_ID] [int] IDENTITY(1,1) NOT NULL,
	[Warning_Type] [varchar](10) NULL,
	[Warning_Remarks] [varchar](max) NULL,
	[Warning_FileTermination] [varchar](100) NULL,
	[Warning_EndDate] [datetime] NULL,
	[Warning_CreateDate] [datetime] NULL,
	[Employee_ID] [int] NULL,
	[Employee_UserCreate] [int] NULL,
 CONSTRAINT [PK_TBL_Employee_Warning] PRIMARY KEY CLUSTERED 
(
	[Warning_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Employee_Working_Experience]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Employee_Working_Experience](
	[Experience_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
	[Experience_1_Company_Name] [varchar](200) NULL,
	[Experience_1_Position] [varchar](50) NULL,
	[Experience_1_Start_Year] [varchar](50) NULL,
	[Experience_1_End_Year] [varchar](50) NULL,
	[Experience_2_Company_Name] [varchar](200) NULL,
	[Experience_2_Position] [varchar](50) NULL,
	[Experience_2_Start_Year] [varchar](50) NULL,
	[Experience_2_End_Year] [varchar](50) NULL,
	[Experience_3_Company_Name] [varchar](200) NULL,
	[Experience_3_Position] [varchar](50) NULL,
	[Experience_3_Start_Year] [varchar](50) NULL,
	[Experience_3_End_Year] [varchar](50) NULL,
	[Experience_4_Company_Name] [varchar](200) NULL,
	[Experience_4_Position] [varchar](50) NULL,
	[Experience_4_Start_Year] [varchar](50) NULL,
	[Experience_4_End_Year] [varchar](50) NULL,
	[Experience_5_Company_Name] [varchar](200) NULL,
	[Experience_5_Position] [varchar](50) NULL,
	[Experience_5_Start_Year] [varchar](50) NULL,
	[Experience_5_End_Year] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_General]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_General](
	[General_ID] [int] IDENTITY(1,1) NOT NULL,
	[General_Area] [varchar](255) NULL,
	[General_Content] [text] NULL,
	[General_Type] [varchar](20) NULL,
	[General_Favicon] [varchar](50) NULL,
	[General_Favicon_Title] [varchar](255) NULL,
	[General_UpdateBy] [varchar](255) NULL,
	[General_LastUpdate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_History_Leave_Balance]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_History_Leave_Balance](
	[Balance_ID] [int] IDENTITY(1,1) NOT NULL,
	[Balance_Value] [float] NULL,
	[Balance_Create_By] [int] NULL,
	[Balance_Create_Date] [datetime] NULL,
	[Balance_Remarks] [varchar](max) NULL,
	[Balance_Status] [int] NULL,
	[Employee_ID] [int] NULL,
	[Division_ID] [int] NULL,
	[Department_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Holiday]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Holiday](
	[Holiday_ID] [int] IDENTITY(1,1) NOT NULL,
	[Holiday_List_ID] [int] NULL,
	[Holiday_Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBL_Holiday_List]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Holiday_List](
	[Holiday_List_ID] [int] IDENTITY(1,1) NOT NULL,
	[Holiday_List_Name] [varchar](300) NULL,
	[Holiday_List_Desc] [varchar](500) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Leave]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Leave](
	[Leave_ID] [int] IDENTITY(1,1) NOT NULL,
	[Leave_RequestDateFrom] [datetime] NULL,
	[Leave_RequestDateTo] [datetime] NULL,
	[Leave_RequestDateCreate] [datetime] NULL,
	[Leave_RequestDate_HalfDay] [datetime] NULL,
	[Leave_Half] [int] NULL,
	[Leave_StatusLeave] [int] NULL,
	[Leave_StatusPage] [int] NULL,
	[Leave_Form] [varchar](255) NULL,
	[flag] [int] NULL,
	[Leave_Remarks] [varchar](max) NULL,
	[ReplacementStaff_id] [int] NULL,
	[Employee_ID] [int] NULL,
	[Type_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Leave_Log]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Leave_Log](
	[Log_ID] [int] IDENTITY(1,1) NOT NULL,
	[Log_DecisionBy] [int] NULL,
	[Log_DecisionUserStatus] [varchar](5) NULL,
	[Log_Remarks] [varchar](max) NULL,
	[Log_StatusLeave] [int] NULL,
	[Log_DateCreate] [datetime] NULL,
	[Leave_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Leave_Type]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Leave_Type](
	[Type_ID] [int] IDENTITY(1,1) NOT NULL,
	[Type_Desc] [varchar](max) NULL,
	[Type_Value] [float] NULL,
	[Type_Minimal_Working] [int] NULL,
	[Type_Order] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Log_History]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Log_History](
	[Log_ID] [int] IDENTITY(1,1) NOT NULL,
	[Log_Date] [datetime] NULL,
	[Log_Activity] [text] NULL,
	[Log_Desc] [text] NULL,
	[Log_IP] [varchar](30) NULL,
	[Log_Hostname] [varchar](100) NULL,
	[Log_Before] [text] NULL,
	[Log_After] [text] NULL,
	[Log_UserID] [int] NULL,
	[Log_Employee_Name] [varchar](200) NULL,
	[Log_Username] [varchar](50) NULL,
	[Log_UserRole] [varchar](50) NULL,
	[User_IsAdmin] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Menu]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Menu](
	[Menu_ID] [int] IDENTITY(1,1) NOT NULL,
	[Menu_Name] [varchar](300) NULL,
	[Menu_isAdmin] [varchar](1) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Notification]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Notification](
	[Notif_ID] [int] IDENTITY(1,1) NOT NULL,
	[Notif_Link] [varchar](max) NULL,
	[Notif_Employee_ID] [int] NULL,
	[Notif_UserGroup_ID] [int] NULL,
	[Notif_Status] [int] NULL,
	[Notif_StatusActionBy] [int] NULL,
	[Notif_StatusPage] [int] NULL,
	[Notif_UserCreate] [int] NULL,
	[Notif_Remarks] [varchar](max) NULL,
	[Notif_DateCreate] [datetime] NULL,
	[Notif_LastUpdate] [datetime] NULL,
	[Leave_ID] [int] NULL,
	[Lembur_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Office_Regulations]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Office_Regulations](
	[Regulation_ID] [int] IDENTITY(1,1) NOT NULL,
	[Regulation_Title] [varchar](255) NULL,
	[Regulation_Desc] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Position]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Position](
	[Position_ID] [int] IDENTITY(1,1) NOT NULL,
	[Position_Name] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Privilege]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Privilege](
	[Privilege_ID] [int] IDENTITY(1,1) NOT NULL,
	[Privilege_View] [int] NULL,
	[Privilege_Insert] [int] NULL,
	[Privilege_Update] [int] NULL,
	[Privilege_Delete] [int] NULL,
	[Menu_ID] [int] NULL,
	[Role_ID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBL_Religion]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Religion](
	[Religion_ID] [int] IDENTITY(1,1) NOT NULL,
	[Religion_Name_EN] [varchar](30) NULL,
	[Religion_Name_ID] [varchar](30) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Role]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Role](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[Role_Name] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Schedule_Kalendar]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Schedule_Kalendar](
	[Schedule_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
	[Schedule_Role_ID] [int] NULL,
	[Schedule_Date] [datetime] NULL,
	[Schedule_Replace_ID] [int] NULL,
	[Schedule_CreateDate] [datetime] NULL,
	[Schedule_CreateBy] [int] NULL,
	[Schedule_History_Update] [text] NULL,
	[Schedule_UpdateDate] [datetime] NULL,
	[Schedule_UpdateBy] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBL_Schedule_Kalendar_Temp]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Schedule_Kalendar_Temp](
	[Schedule_Temp_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
	[Schedule_Role_ID] [int] NULL,
	[Schedule_Temp_Date] [datetime] NULL,
	[Schedule_Temp_CreateDate] [datetime] NULL,
	[Schedule_Temp_CreateBy] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBL_Schedule_Role]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Schedule_Role](
	[Schedule_Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[Schedule_Role_Code] [varchar](255) NULL,
	[Schedule_Role_Nama] [varchar](255) NULL,
	[Schedule_Role_Status] [int] NULL,
	[Schedule_Role_Toleransi_Telat] [int] NULL,
	[Schedule_Role_Durasi_Istirahat] [int] NULL,
	[Schedule_Role_UpdateBy] [int] NULL,
	[Schedule_Role_CreateDate] [datetime] NULL,
	[Schedule_Role_JamMasuk] [time](7) NULL,
	[Schedule_Role_JamKeluar] [time](7) NULL,
	[Schedule_Role_Color] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_User]    Script Date: 8/11/2017 2:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_User](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [varchar](255) NULL,
	[User_Email] [varchar](255) NULL,
	[User_Photo] [varchar](255) NULL,
	[User_Authorized] [varchar](1) NULL,
	[User_Password] [varchar](255) NULL,
	[User_LastLogin] [datetime] NULL,
	[User_LastIP] [varchar](255) NULL,
	[User_LastHostName] [varchar](255) NULL,
	[User_TglBlokir] [datetime] NULL,
	[User_CountLogin] [int] NULL,
	[User_CountSalahPass] [int] NULL,
	[User_LastModified] [datetime] NULL,
	[User_TglCreate] [datetime] NULL,
	[Role_ID] [int] NULL,
	[Employee_ID] [int] NULL,
	[User_ForgetPass_Link_ExpiredDate] [datetime] NULL,
	[User_ForgetPass_Link] [varchar](255) NULL,
	[User_ForgetPass_Link_status] [bit] NULL,
	[User_ResetPass_Status] [bit] NULL,
	[User_ResetPass_Date] [datetime] NULL,
	[User_IsAdmin] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TBL_Employee] ADD  CONSTRAINT [DF_TBL_Employee_Employee_Sum_LeaveBalance]  DEFAULT ((0)) FOR [Employee_Sum_LeaveBalance]
GO
ALTER TABLE [dbo].[TBL_User] ADD  CONSTRAINT [DF_TBL_User_User_Autorized]  DEFAULT ('Y') FOR [User_Authorized]
GO
ALTER TABLE [dbo].[TBL_User] ADD  CONSTRAINT [DF_TBL_User_User_CountLogin]  DEFAULT ((0)) FOR [User_CountLogin]
GO
ALTER TABLE [dbo].[TBL_User] ADD  CONSTRAINT [DF_TBL_User_User_CountSalahPass]  DEFAULT ((0)) FOR [User_CountSalahPass]
GO
ALTER TABLE [dbo].[TBL_User] ADD  CONSTRAINT [DF_TBL_User_User_FPassword_Link_status]  DEFAULT ((0)) FOR [User_ForgetPass_Link_status]
GO
ALTER TABLE [dbo].[TBL_User] ADD  CONSTRAINT [DF_TBL_User_User_ResPassword_Status]  DEFAULT ((0)) FOR [User_ResetPass_Status]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Single, 2 = Married, 3 = Widow, 4 = Widower' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Employee', @level2type=N'COLUMN',@level2name=N'Employee_Marital_Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = High School, 2 = Bachelor Degree, 3 = Master Degree, 4 = Doctoral Degree' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Employee', @level2type=N'COLUMN',@level2name=N'Employee_Education'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = yes, 2 = no' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Employee', @level2type=N'COLUMN',@level2name=N'Employee_Inactive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = NPWP, 2 = NonNPWP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Employee_Payroll', @level2type=N'COLUMN',@level2name=N'Payroll_NPWP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = yes, 2 = no' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Employee_Payroll', @level2type=N'COLUMN',@level2name=N'Payroll_BPJS_Pribadi'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = yes, 2 = no' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Employee_Payroll', @level2type=N'COLUMN',@level2name=N'Payroll_BPJS_Ditanggung'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 -> create, 2 -> LM approve, 3 - > LM reject, 4 -> SPV approve, 5 -> SPV reject, 6 ->Cancel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Leave', @level2type=N'COLUMN',@level2name=N'Leave_StatusLeave'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 -> ok, 1-> revisi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Leave', @level2type=N'COLUMN',@level2name=N'flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'superadmin = 1, admin = 2, user = 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Menu', @level2type=N'COLUMN',@level2name=N'Menu_isAdmin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee dengan ID berapa yang bisa akses notification' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Notification', @level2type=N'COLUMN',@level2name=N'Notif_Employee_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee dengan usergroup berapa yang bisa akses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Notification', @level2type=N'COLUMN',@level2name=N'Notif_UserGroup_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 -> Pending, 2 -> Approve, 3 -> Reject' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Notification', @level2type=N'COLUMN',@level2name=N'Notif_Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notif ini di approve/reject oleh siapa?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Notification', @level2type=N'COLUMN',@level2name=N'Notif_StatusActionBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 -> belum di baca, 1-> sudah di baca, 2 -> sudah melakukan approve atau reject' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Notification', @level2type=N'COLUMN',@level2name=N'Notif_StatusPage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notification ini untuk module apa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Notification', @level2type=N'COLUMN',@level2name=N'Notif_Remarks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Full Time  1 = Shifting' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Schedule_Role', @level2type=N'COLUMN',@level2name=N'Schedule_Role_Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 -> superadmin. 2 -> admin. 3 -> user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_User', @level2type=N'COLUMN',@level2name=N'User_IsAdmin'
GO
USE [master]
GO
ALTER DATABASE [Gulali_HRIS] SET  READ_WRITE 
GO
