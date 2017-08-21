USE [master]
GO
/****** Object:  Database [Gulali_Payroll]    Script Date: 8/11/2017 2:50:51 PM ******/
CREATE DATABASE [Gulali_Payroll]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Gulali_Payroll', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Gulali_Payroll.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Gulali_Payroll_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Gulali_Payroll_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Gulali_Payroll] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Gulali_Payroll].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Gulali_Payroll] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET ARITHABORT OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Gulali_Payroll] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Gulali_Payroll] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Gulali_Payroll] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Gulali_Payroll] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Gulali_Payroll] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Gulali_Payroll] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Gulali_Payroll] SET  MULTI_USER 
GO
ALTER DATABASE [Gulali_Payroll] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Gulali_Payroll] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Gulali_Payroll] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Gulali_Payroll] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Gulali_Payroll]
GO
/****** Object:  Table [dbo].[TBL_HistorySalary]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_HistorySalary](
	[HistorySalary_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [varchar](max) NULL,
	[HistorySalary_Value] [decimal](18, 0) NULL,
	[HistorySalary_DateChanged] [datetime] NULL,
	[HistorySalary_Description] [text] NULL,
 CONSTRAINT [PK_TBL_HistorySalary] PRIMARY KEY CLUSTERED 
(
	[HistorySalary_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Lembur]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Lembur](
	[Lembur_ID] [int] IDENTITY(1,1) NOT NULL,
	[Lembur_DatePeriode] [datetime] NULL,
	[Lembur_DateCreate] [datetime] NULL,
	[Employee_ID] [int] NULL,
	[Lembur_Remarks] [varchar](max) NULL,
	[Lembur_Status] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Lembur_List]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Lembur_List](
	[List_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
	[Lembur_ID] [int] NULL,
	[List_Deskripsi_Lembur] [varchar](255) NULL,
	[List_Tanggal] [datetime] NULL,
	[List_JamMulai] [datetime] NULL,
	[List_JamSelesai] [datetime] NULL,
	[List_DateCreate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Lembur_List_Temp]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Lembur_List_Temp](
	[List_ID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
	[List_Deskripsi_Lembur] [varchar](255) NULL,
	[List_Tanggal] [datetime] NULL,
	[List_JamMulai] [datetime] NULL,
	[List_JamSelesai] [datetime] NULL,
	[List_DateCreate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Lembur_Log]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Lembur_Log](
	[Log_ID] [int] IDENTITY(1,1) NOT NULL,
	[Log_DecisionBy] [int] NULL,
	[Log_DecisionUserStatus] [varchar](5) NULL,
	[Log_Remarks] [varchar](max) NULL,
	[Log_StatusLembur] [int] NULL,
	[Log_DateCreate] [datetime] NULL,
	[Lembur_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_MasterComponent]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_MasterComponent](
	[component_id] [int] IDENTITY(1,1) NOT NULL,
	[component_code] [varchar](50) NULL,
	[component_name] [varchar](50) NULL,
	[component_kind] [varchar](50) NULL,
	[component_type] [varchar](50) NULL,
	[status_delete] [bit] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Parameter]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Parameter](
	[Param_ID] [int] IDENTITY(1,1) NOT NULL,
	[Param_Value] [decimal](18, 4) NULL,
	[Param_Description] [varchar](400) NULL,
	[Param_Choose] [int] NULL,
	[Param_Remarks] [varchar](500) NULL,
	[Param_UpdateBy] [int] NULL,
	[Param_UpdateDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Payroll]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Payroll](
	[Payroll_ID] [int] IDENTITY(1,1) NOT NULL,
	[Payroll_EmployeeID] [varchar](max) NULL,
	[Payroll_PeriodFrom] [datetime] NULL,
	[Payroll_PeriodTo] [datetime] NULL,
	[Payroll_BasicSalary] [decimal](20, 2) NULL,
	[Payroll_BasicSalaryBPJS] [decimal](20, 2) NULL,
	[Payroll_BasicSalaryPPH] [decimal](20, 2) NULL,
	[Payroll_TotalAllowance] [decimal](20, 2) NULL,
	[Payroll_Allowance] [decimal](20, 2) NULL,
	[Payroll_IrregularIncome] [decimal](20, 2) NULL,
	[Payroll_UnpaidLeave] [decimal](20, 2) NULL,
	[Payroll_THR] [decimal](20, 2) NULL,
	[Payroll_Overtime] [decimal](20, 2) NULL,
	[Payroll_Bonus] [decimal](20, 2) NULL,
	[Payroll_Incentive] [decimal](20, 2) NULL,
	[Payroll_tunjangan_lainnya] [decimal](20, 2) NULL,
	[Payroll_BPJSPribadi] [decimal](20, 2) NULL,
	[Payroll_BPJS] [decimal](20, 2) NULL,
	[Payroll_BPJSfix] [decimal](20, 2) NULL,
	[payroll_JHT] [decimal](20, 2) NULL,
	[Payroll_JKK] [decimal](20, 2) NULL,
	[Payroll_JKM] [decimal](20, 2) NULL,
	[Payroll_BPJSPeg] [decimal](20, 2) NULL,
	[Payroll_JHTPeg] [decimal](20, 2) NULL,
	[Payroll_pph21_disetahunkan] [decimal](20, 2) NULL,
	[Payroll_pph21_periode_ini] [decimal](20, 2) NULL,
	[Payroll_biaya_jabatan] [decimal](20, 2) NULL,
	[Payroll_pkp] [decimal](20, 2) NULL,
	[Payroll_ptkp] [decimal](20, 2) NULL,
	[Payroll_netto] [decimal](20, 2) NULL,
	[Payroll_bruto] [decimal](20, 2) NULL,
	[Payroll_setahunkan] [decimal](20, 2) NULL,
	[Payroll_npwp] [varchar](50) NULL,
	[Flag_ID] [int] NULL,
	[Payroll_DateCreate] [datetime] NULL,
	[Payroll_UserCreate] [int] NULL,
 CONSTRAINT [PK_TBL_Payroll] PRIMARY KEY CLUSTERED 
(
	[Payroll_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Payroll_Det_Allowance]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Payroll_Det_Allowance](
	[det_allowance_id] [int] IDENTITY(1,1) NOT NULL,
	[det_allowance_payroll_id] [int] NULL,
	[det_allowance_component_id] [int] NULL,
	[det_allowance_value] [decimal](18, 2) NULL,
	[det_allowance_group] [varchar](50) NULL,
	[det_allowance_employee_id] [varchar](max) NULL,
	[det_allowance_flag_id] [int] NULL,
 CONSTRAINT [PK_TBL_Payroll_Det_Allowance] PRIMARY KEY CLUSTERED 
(
	[det_allowance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Payroll_Flag]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Payroll_Flag](
	[Flag_ID] [int] IDENTITY(1,1) NOT NULL,
	[Flag_Month] [int] NULL,
	[Flag_Year] [int] NULL,
	[Flag_ForMonth] [int] NULL,
	[Flag_TotalGaji] [varchar](100) NULL,
	[Flag_DateCreate] [datetime] NULL,
	[Flag_Status] [int] NULL,
 CONSTRAINT [PK_TBL_Payroll_Flag] PRIMARY KEY CLUSTERED 
(
	[Flag_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Payroll_old]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Payroll_old](
	[Payroll_ID] [int] IDENTITY(1,1) NOT NULL,
	[Payroll_EmployeeID] [varchar](max) NULL,
	[Payroll_PeriodFrom] [datetime] NULL,
	[Payroll_PeriodTo] [datetime] NULL,
	[Payroll_BasicSalary] [decimal](20, 2) NULL,
	[Payroll_TotalAllowance] [decimal](20, 2) NULL,
	[Payroll_Allowance] [decimal](20, 2) NULL,
	[Payroll_IrregularIncome] [decimal](20, 2) NULL,
	[Payroll_Overtime] [decimal](20, 2) NULL,
	[Payroll_Assurance] [decimal](20, 2) NULL,
	[Payroll_BPJS] [decimal](20, 2) NULL,
	[Payroll_BPJSfix] [decimal](20, 2) NULL,
	[Payroll_UnpaidLeave] [decimal](20, 2) NULL,
	[Payroll_THR] [decimal](20, 2) NULL,
	[Payroll_UserCreate] [int] NULL,
	[Payroll_DateCreate] [datetime] NULL,
	[Flag_ID] [int] NULL,
	[Payroll_Tax] [decimal](20, 2) NULL,
	[Payroll_BasicSalaryPPH] [decimal](20, 2) NULL,
	[Payroll_BasicSalaryBPJS] [decimal](20, 2) NOT NULL,
	[Payroll_Bonus] [decimal](20, 2) NULL,
	[Payroll_Incentive] [decimal](20, 2) NULL,
	[pkp] [decimal](20, 2) NULL,
	[ptkp] [decimal](20, 2) NULL,
	[netto] [decimal](20, 2) NULL,
	[bruto] [decimal](20, 2) NULL,
	[setahunkan] [decimal](20, 2) NULL,
	[npwp] [varchar](50) NULL,
	[taxsetahun] [decimal](20, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Payroll_Privilege]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Payroll_Privilege](
	[Privilege_ID] [int] IDENTITY(1,1) NOT NULL,
	[Component_ID] [int] NULL,
	[Admin_Role] [varchar](100) NULL,
	[Privilege_Choose] [int] NULL,
	[privilege_value] [decimal](18, 0) NULL,
 CONSTRAINT [PK_TBL_Privilege] PRIMARY KEY CLUSTERED 
(
	[Privilege_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Payroll_Privilege_old_for_cahyo]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Payroll_Privilege_old_for_cahyo](
	[Privilege_ID] [int] IDENTITY(1,1) NOT NULL,
	[Component_ID] [int] NULL,
	[Admin_Role] [varchar](100) NULL,
	[Privilege_Choose] [int] NULL,
	[privilege_value] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Payroll_Role]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Payroll_Role](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[Admin_Role] [varchar](100) NULL,
 CONSTRAINT [PK_TBL_Role] PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Pinjaman]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Pinjaman](
	[Pinjaman_ID] [int] IDENTITY(1,1) NOT NULL,
	[Pinjaman_No_Perjanjian] [varchar](255) NULL,
	[Employee_ID] [int] NULL,
	[Pinjaman_DateFrom] [datetime] NULL,
	[Pinjaman_DateTo] [datetime] NULL,
	[Pinjaman_Lama_Bulan] [int] NULL,
	[Pinjaman_Angsuran_ke] [int] NULL,
	[Pinjaman_Pokok] [int] NULL,
	[Pinjaman_Pokok_Terbilang] [varchar](255) NULL,
	[Pinjaman_Keperluan] [varchar](255) NULL,
	[Pinjaman_Bunga] [varchar](255) NULL,
	[Pinjaman_Total] [int] NULL,
	[Pinjaman_Angsuran_Perbulan] [int] NULL,
	[Pinjaman_Pembayaran] [int] NULL,
	[Pinjaman_Sisa] [int] NULL,
	[Pinjaman_CreateById] [int] NULL,
	[Pinjaman_DateCreate] [datetime] NULL,
	[Pinjaman_DateUpdate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Progressive_Tax]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Progressive_Tax](
	[progressive_tax_id] [int] IDENTITY(1,1) NOT NULL,
	[progressive_tax_desc] [varchar](50) NULL,
	[progressive_tax_prosentase] [varchar](50) NULL,
	[progressive_tax_min] [decimal](18, 2) NULL,
	[progressive_tax_max] [decimal](18, 2) NULL,
 CONSTRAINT [PK_TBL_Progressive_Tax] PRIMARY KEY CLUSTERED 
(
	[progressive_tax_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_PTKP]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_PTKP](
	[PTKP_ID] [int] IDENTITY(1,1) NOT NULL,
	[PTKP_Status] [varchar](4) NULL,
	[PTKP_Description] [text] NULL,
	[PTKP_Value] [decimal](18, 0) NULL,
	[PTKP_UpdateBy] [int] NULL,
	[PTKP_UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_PTKP] PRIMARY KEY CLUSTERED 
(
	[PTKP_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Rumus_Lembur]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Rumus_Lembur](
	[Rumus_Category_ID] [int] IDENTITY(1,1) NOT NULL,
	[Rumus_Category_Title] [varchar](255) NULL,
	[Rumus_Category_Status] [int] NULL,
	[Rumus_Category_CreateBy] [int] NULL,
	[Rumus_Category_CreateDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Rumus_lembur_List]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Rumus_lembur_List](
	[Rumus_List_ID] [int] IDENTITY(1,1) NOT NULL,
	[Rumus_List] [varchar](255) NULL,
	[Rumus_List_Jam] [varchar](255) NULL,
	[Rumus_List_Bulan] [varchar](255) NULL,
	[Rumus_List_Description] [text] NULL,
	[Rumus_List_UpdateBy] [int] NULL,
	[Rumus_List_UpdateDate] [datetime] NULL,
	[Rumus_Category_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_TaxRate]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_TaxRate](
	[TaxRate_ID] [int] IDENTITY(1,1) NOT NULL,
	[TaxRate_Begin] [decimal](18, 0) NULL,
	[TaxRate_End] [decimal](18, 0) NULL,
	[TaxRate_Prosentase] [int] NULL,
 CONSTRAINT [PK_TBL_TaxRate] PRIMARY KEY CLUSTERED 
(
	[TaxRate_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBL_THR]    Script Date: 8/11/2017 2:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_THR](
	[thr_id] [int] IDENTITY(1,1) NOT NULL,
	[holiday_list_id] [int] NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TBL_MasterComponent] ADD  CONSTRAINT [DF_TBL_MasterComponent_status_delete]  DEFAULT ((1)) FOR [status_delete]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Lembur Hari Kerja 1 = Lembur Hari Libur' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL_Rumus_Lembur', @level2type=N'COLUMN',@level2name=N'Rumus_Category_Status'
GO
USE [master]
GO
ALTER DATABASE [Gulali_Payroll] SET  READ_WRITE 
GO
