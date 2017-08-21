<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_data_karyawan_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Gulali HRIS - Data Karyawan - Detail</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="data-karyawan" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="btn-group pull-right m-t-15">
                        <asp:Literal ID="link_back" runat="server"></asp:Literal>
                    </div>
                    <h4 class="page-title">Detail Data Karyawan</h4>
                </div>
            </div>

            <div class="row">
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="sm" runat="server" />
                    <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-sm-12">
                                <div class="form-horizontal" role="form" runat="server">
                                    <asp:Label ID="link_added" runat="server" />
                                    <div class="card-box p-b-0"  style="padding-top: 0px !important;">
                                    <div id="basicwizard" class=" pull-in">
                                    <!-- Nav tabs -->
                                         <ul class="nav nav-tabs">
                                            <li><a href="#datapribadi" aria-controls="datapribadi" role="tab" data-toggle="tab">Data Pribadi</a></li>
                                            <li><a href="#kontakdarurat" aria-controls="kontakdarurat" role="tab" data-toggle="tab">Kontak Darurat</a></li>
                                            <li><a href="#riwayatpendidikan" aria-controls="riwayatpendidikan" role="tab" data-toggle="tab">Pendidikan</a></li>
                                            <li><a href="#pengalamankerja" aria-controls="pengalamankerja" role="tab" data-toggle="tab">Pengalaman Kerja</a></li>
                                            <li><a href="#tanggungan" aria-controls="tanggungan" role="tab" data-toggle="tab">Tanggungan</a></li>
                                            <li><a href="#infopembayaran" aria-controls="infopembayaran" role="tab" data-toggle="tab">Info Pembayaran</a></li>
                                         </ul>

                                         <!-- Tab panes -->
                                         <div class="tab-content b-0 m-b-0">
                                            <div role="tabpanel" class="tab-pane m-t-10 fade" id="datapribadi">
                                            <div class="row"><br />
                                            <div class="col-lg-6">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">No. Induk Karyawan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nik" class="form-control" runat="server" placeholder="No. Induk Karyawan" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Lengkap</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_lengkap" class="form-control" runat="server" placeholder="Nama Lengkap" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Depan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_depan" class="form-control" runat="server" placeholder="Nama Depan" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Tengah</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_tengah" class="form-control" runat="server" placeholder="Nama Tengah" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Belakang</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_belakang" class="form-control" runat="server" placeholder="Nama Belakang" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Alias</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_alias" class="form-control" runat="server" placeholder="Nama Alias" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jenis Kelamin</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="jenis_kelamin" runat="server" class="form-control">
                                                                <asp:ListItem Value="L">Laki-Laki</asp:ListItem>
                                                                <asp:ListItem Value="P">Perempuan</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Tempat Lahir</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="tempat_lahir" class="form-control" runat="server" placeholder="Tempat Lahir" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label" style="padding-top: 20px;">Tanggal Lahir</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group m-t-10">
                                                                <asp:TextBox ID="tanggal_lahir" runat="server" class="form-control" placeholder="Tanggal Lahir" disabled=""/>
                                                                <span class="input-group-addon"><asp:Literal ID="old" runat="server"/></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Agama</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="agama" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Agama --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Golongan Darah</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_blood_type" runat="server" class="form-control">
                                                                <asp:ListItem Value="0">-- Pilih Golongan Darah --</asp:ListItem>
                                                                <asp:ListItem Value="1">A</asp:ListItem>
                                                                <asp:ListItem Value="2">B</asp:ListItem>
                                                                <asp:ListItem Value="3">AB</asp:ListItem>
                                                                <asp:ListItem Value="4">O</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">No. Handphone 1 (WA)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="no_hp1" class="form-control" runat="server" placeholder="No. Handphone 1 (WA)" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">No. Handphone 2</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="no_hp2" class="form-control" runat="server" placeholder="No. Handphone 2" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">No. Identitas (KTP)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="no_ktp" class="form-control" runat="server" placeholder="No. Identitas (KTP)" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Alamat (Sesuai KTP)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="alamat_ktp" class="form-control" runat="server" placeholder="Alamat (Sesuai KTP)" TextMode="MultiLine" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Alamat (Domisili)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="alamat_domisili" class="form-control" runat="server" placeholder="Alamat (Domisili)" TextMode="MultiLine" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Status Pernikahan</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="marital_status" class="form-control" runat="server">
                                                                <asp:ListItem Value="0">-- Pilih Status Pernikahan --</asp:ListItem>
                                                                <asp:ListItem Value="1">Belum Menikah</asp:ListItem>
                                                                <asp:ListItem Value="2">Menikah</asp:ListItem>
                                                                <asp:ListItem Value="3">Bercerai</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Pasangan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_pasangan" class="form-control" runat="server" placeholder="Nama Pasangan" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">SIM</label>
                                                        <div class="col-md-2">
                                                            <div class="checkbox">
                                                                <asp:CheckBox ID="checkbox_sim_a" runat="server" Text="SIM A" RepeatColumns="4" RepeatDirection="Vertikal" Enabled="false"></asp:CheckBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="checkbox">
                                                                <asp:CheckBox ID="checkbox_sim_c" runat="server" Text="SIM C" RepeatColumns="4" RepeatDirection="Vertital" Enabled="false"></asp:CheckBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Email (Kantor)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="email_kantor" class="form-control" runat="server" placeholder="Email (Kantor)" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Email (Pribadi)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="email_pribadi" class="form-control" runat="server" placeholder="Email (Pribadi)" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Pendidikan Terakhir</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="last_education" runat="server" class="form-control">
                                                                <asp:ListItem Value="">-- Pilih Pendidikan Terakhir --</asp:ListItem>
                                                                <asp:ListItem Value="1">SMA</asp:ListItem>
                                                                <asp:ListItem Value="2">Diploma</asp:ListItem>
                                                                <asp:ListItem Value="3">S1</asp:ListItem>
                                                                <asp:ListItem Value="4">S2</asp:ListItem>
                                                                <asp:ListItem Value="5">S3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jurusan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="jurusan" class="form-control" runat="server" placeholder="Jurusan" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Departemen</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_department" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Departemen --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Divisi</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_division" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Divisi --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Direct Supervisor</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_directSpv" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Direct Supervisor --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jabatan</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_position" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Jabatan --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Mulai Bekerja</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="start_of_employment" runat="server" class="form-control" placeholder="Mulai Bekerja" ReadOnly="true" disabled="" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Periode Kontrak</label>
                                                        <div class="col-md-8">
                                                            <div class="input-daterange input-group" id="date-range">
                                                                <span class="input-group-addon b-0">Dari</span>
                                                                <asp:TextBox ID="contract_start" runat="server" class="form-control" placeholder="Dari" ReadOnly="true" disabled="" />
                                                                <span class="input-group-addon b-0">Sampai</span>
                                                                <asp:TextBox ID="contract_end" runat="server" class="form-control" placeholder="Sampai" ReadOnly="true" disabled="" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Lama Bekerja</label>
                                                        <div class="col-md-8">
                                                            <table class="table table-bordered responsive" style="width: 400px">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; width: 25%;">Tahun
                                                                        </th>
                                                                        <th style="text-align: center; width: 25%;">Bulan
                                                                        </th>
                                                                        <th style="text-align: center; width: 25%;">Minggu
                                                                        </th>
                                                                        <th style="text-align: center; width: 25%;">Hari
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="lw_Year" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="lw_Month" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="lw_Week" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="lw_Day" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">CV</label>
                                                        <div class="col-md-8">
                                                            <asp:Literal ID="cv" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Foto</label>
                                                        <div class="col-md-8">
                                                            <asp:Literal ID="link_foto" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Status Karyawan</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_employee_status" class="form-control" runat="server">
                                                                <asp:ListItem Value="1" Text="Aktif" />
                                                                <asp:ListItem Value="2" Text="Tidak Aktif" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Catatan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="remarks" class="form-control" runat="server" TextMode="MultiLine" placeholder="Catatan" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end col -->
                                            </div>
                                            </div>
                                            <div role="tabpanel" class="tab-pane m-t-10 fade" id="kontakdarurat">
                                            <div class="row" id="div_kontak_darurat" runat="server">
                                            <div class="col-lg-12">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-1 control-label">&nbsp;</label>
                                                        <div class="row"><br />
                                                        <div class="col-md-8">
                                                            <table class="table table-bordered responsive" style="width: 600px">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; width: 25%;">Nama</th>
                                                                        <th style="text-align: center; width: 25%;">Alamat</th>
                                                                        <th style="text-align: center; width: 25%;">Telepon</th>
                                                                        <th style="text-align: center; width: 25%;">Hubungan</th>
                                                                    </tr>
                                                                </thead>
                                                                <asp:Literal ID="loop_kontak" runat="server"></asp:Literal>
                                                            </table>
                                                        </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            </div>
                                            <div role="tabpanel" class="tab-pane m-t-10 fade" id="riwayatpendidikan">
                                            <div class="col-md-offset-1">
                                            <h4 class="header-title m-t-30 m-b-30" id="h4_riwayat_pendidikan_formal" runat="server">Formal</h4>
                                            </div>
                                                <div class="row" id="div_riwayat_pendidikan_formal" runat="server">
                                            <div class="col-lg-12">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-1 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
                                                            <table class="table table-bordered responsive" style="width: 600px">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; width: 25%;">Pendidikan</th>
                                                                        <th style="text-align: center; width: 25%;">Institusi</th>
                                                                        <th style="text-align: center; width: 25%;">Tahun Pendidikan</th>
                                                                        <th style="text-align: center; width: 25%;">Lokasi</th>
                                                                    </tr>
                                                                </thead>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_1" runat="server" Text="SD" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_1_name" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_1_year" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_1_location" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_2" runat="server" Text="SMP" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_2_name" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_2_year" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_2_location" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_3" runat="server" Text="SMK / SMA / MI" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_3_name" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_3_year" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_3_location" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_4" runat="server" Text="UNIVERSITAS" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_4_name" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_4_year" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_4_location" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_5" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_5_name" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_5_year" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_5_location" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_6" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_6_name" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_6_year" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="edu_6_location" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                         </div>
                                            <div role="tabpanel" class="tab-pane m-t-10 fade" id="pengalamankerja">
                                            <div class="row" id="div_riwayat_pengalaman_kerja" runat="server">
                                            <div class="col-lg-12">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-1 control-label">&nbsp;</label>
                                                        <div class="row"><br />
                                                        <div class="col-md-8">
                                                            <table class="table table-bordered responsive" style="width: 600px">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; width: 25%;">Company Name</th>
                                                                        <th style="text-align: center; width: 25%;">Position</th>
                                                                        <th style="text-align: center; width: 25%;">Start Year</th>
                                                                        <th style="text-align: center; width: 25%;">End Year</th>
                                                                    </tr>
                                                                </thead>
                                                                <tr id="tr2" runat="server">
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Company_1" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Position_1" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Start_1" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="End_1" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="tr3" runat="server">
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Company_2" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Position_2" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Start_2" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="End_2" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Company_3" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Position_3" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Start_3" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="End_3" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Company_4" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Position_4" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Start_4" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="End_4" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Company_5" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Position_5" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="Start_5" runat="server" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Literal ID="End_5" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            </div>
                                            <div role="tabpanel" class="tab-pane m-t-10 fade" id="tanggungan">
                                            <div class="row" id="div_tanggungan" runat="server">
                                            <div class="col-lg-12">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-1 control-label">&nbsp;</label>
                                                        <div class="row"><br />
                                                        <div class="col-md-8">
                                                            <table class="table table-bordered responsive" style="width: 600px">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; width: 20%;">Nama</th>
                                                                        <th style="text-align: center; width: 20%;">jenis Kelamin</th>
                                                                        <th style="text-align: center; width: 20%;">Tempat Lahir</th>
                                                                        <th style="text-align: center; width: 20%;">Tanggal Lahir</th>
                                                                        <th style="text-align: center; width: 20%;">Hubungan</th>
                                                                    </tr>
                                                                </thead>
                                                                <asp:Literal ID="loop_depend" runat="server"></asp:Literal>
                                                            </table>
                                                        </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            </div>
                                            <div role="tabpanel" class="tab-pane m-t-10 fade" id="infopembayaran">
                                            <div class="row"><br />
                                            <div class="col-lg-6">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Rekening 1 (Utama)</label>
                                                        <div class="col-md-4">
                                                            <span class="font-13">Atas Nama :</span>
                                                            <asp:TextBox ID="in_the_name1" class="form-control" runat="server" placeholder="Atas Nama" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <span class="font-13">No. Rekening :</span>
                                                            <asp:TextBox ID="bank_account_1" class="form-control" runat="server" placeholder="No. Rekening" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Rekening 2</label>
                                                        <div class="col-md-4">
                                                            <span class="font-13">Atas Nama :</span>
                                                            <asp:TextBox ID="in_the_name2" class="form-control" runat="server" placeholder="Atas Nama" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <span class="font-13">No. Rekening :</span>
                                                            <asp:TextBox ID="bank_account_2" class="form-control" runat="server" placeholder="No. Rekening" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Gaji</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="current_salary" class="form-control" runat="server" placeholder="Gaji" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Gaji PPH</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="salary_pph" class="form-control" runat="server" placeholder="Gaji PPH" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Gaji BPJS</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="salary_bpjs" class="form-control" runat="server" placeholder="Gaji BPJS" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Status Pajak</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_status_pajak" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Status Pajak --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">BPJS Pribadi</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_personal_bpjs" runat="server" class="form-control" Width="100%">
                                                                <asp:ListItem Value="1">Ya</asp:ListItem>
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jumlah BPJS Pribadi</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="personal_bpjs_value" class="form-control" runat="server" placeholder="Jumlah BPJS Pribadi" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">BPJS Ditanggung Orangtua/Suami/Istri</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_bpjs_ditanggung" runat="server" class="form-control" Width="100%">
                                                                <asp:ListItem Value="1">Ya</asp:ListItem>
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">NPWP</label>
                                                        <div class="col-md-8">
                                                            <div class="col-md-3">
                                                                <div class="radio">
                                                                    <asp:RadioButton ID="radio_npwp" runat="server" GroupName="npwp" Text="NPWP"/>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="radio">
                                                                    <asp:RadioButton ID="radio_non_npwp" runat="server" GroupName="npwp" Text="Non NPWP"/>    
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="div_npwp_number" runat="server" visible="true">
                                                        <label class="col-md-4 control-label">No. NPWP</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="npwp_number" class="form-control" runat="server" ReadOnly="true" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Grup Payroll</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ReadOnly="true" disabled="" ID="ddl_payroll_group" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Grup Payroll --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            </div>
                                         </div>
                                    </div>

                                        
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </form>
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>
<ucscript:script ID="script" runat="server" />
</html>

