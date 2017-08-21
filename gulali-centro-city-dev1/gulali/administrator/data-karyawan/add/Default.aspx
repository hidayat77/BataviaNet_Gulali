<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_data_karyawan_add" %>

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
    <title>Gulali HRIS - Data Karyawan - Add</title>
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
                    <h4 class="page-title">Add Data Karyawan</h4>
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
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div id="basicwizard" class="pull-in">
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
                                        <div class="row">
                                            <br />
                                            <div class="col-lg-6">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">No. Induk Karyawan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nik" class="form-control" runat="server" placeholder="No. Induk Karyawan"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Lengkap</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_lengkap" class="form-control" runat="server" placeholder="Nama Lengkap" disabled=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Depan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_depan" class="form-control" runat="server" placeholder="Nama Depan" onkeyup="javascript:Convert()" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Tengah</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_tengah" class="form-control" runat="server" placeholder="Nama Tengah" onkeyup="javascript:Convert()" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Belakang</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_belakang" class="form-control" runat="server" placeholder="Nama Belakang" onkeyup="javascript:Convert()" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Alias</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_alias" class="form-control" runat="server" placeholder="Nama Alias"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jenis Kelamin</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="jenis_kelamin" runat="server" class="form-control">
                                                                <asp:ListItem Value="L">Laki-Laki</asp:ListItem>
                                                                <asp:ListItem Value="P">Perempuan</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Tempat Lahir</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="tempat_lahir" class="form-control" runat="server" placeholder="Tempat Lahir"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Tanggal Lahir</label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="tanggal_lahir" runat="server" class="form-control" placeholder="Tanggal Lahir" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Agama</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="agama" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Agama --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Golongan Darah</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_blood_type" runat="server" class="form-control">
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
                                                            <asp:TextBox ID="no_hp1" class="form-control" runat="server" placeholder="No. Handphone 1 (WA)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">No. Handphone 2</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="no_hp2" class="form-control" runat="server" placeholder="No. Handphone 2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">No. Identitas (KTP)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="no_ktp" class="form-control" runat="server" placeholder="No. Identitas (KTP)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Alamat (Sesuai KTP)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="alamat_ktp" class="form-control" runat="server" placeholder="Alamat (Sesuai KTP)" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Alamat (Domisili)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="alamat_domisili" class="form-control" runat="server" placeholder="Alamat (Domisili)" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Status Pernikahan</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="marital_status" class="form-control" runat="server">
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
                                                            <asp:TextBox ID="nama_pasangan" class="form-control" runat="server" placeholder="Nama Pasangan"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">SIM</label>
                                                        <div class="col-md-2">
                                                            <div class="checkbox">
                                                                <asp:CheckBox ID="checkbox_sim_a" runat="server" />
                                                                <label for="control-label">
                                                                    SIM A
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="checkbox">
                                                                <asp:CheckBox ID="checkbox_sim_c" runat="server" />
                                                                <label for="control-label">
                                                                    SIM C
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Email (Kantor)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="email_kantor" class="form-control" runat="server" placeholder="Email (Kantor)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Email (Pribadi)</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="email_pribadi" class="form-control" runat="server" placeholder="Email (Pribadi)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Pendidikan Terakhir</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="last_education" runat="server" class="form-control">
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
                                                            <asp:TextBox ID="jurusan" class="form-control" runat="server" placeholder="Jurusan"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Departemen</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_department" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged">
                                                                <asp:ListItem Value="" Text="-- Pilih Departemen --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Divisi</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_division" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged">
                                                                <asp:ListItem Value="" Text="-- Pilih Divisi --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Direct Supervisor</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_directSpv" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Direct Supervisor --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jabatan</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_position" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Jabatan --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Mulai Bekerja</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="start_of_employment" runat="server" class="form-control" placeholder="Mulai Bekerja" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Periode Kontrak</label>
                                                        <div class="col-md-8">
                                                            <div class="input-daterange input-group" id="date-range">
                                                                <span class="input-group-addon b-0">Dari</span>
                                                                <asp:TextBox ID="contract_start" runat="server" class="form-control" placeholder="Dari" />
                                                                <span class="input-group-addon b-0">Sampai</span>
                                                                <asp:TextBox ID="contract_end" runat="server" class="form-control" placeholder="Sampai" />
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">CV</label>
                                                        <div class="col-md-8">
                                                            <asp:FileUpload ID="FileUpload_cv" runat="server"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Foto</label>
                                                        <div class="col-md-8">
                                                            <asp:FileUpload ID="Employee_Photo" runat="server"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Status Karyawan</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_employee_status" class="form-control" runat="server">
                                                                <asp:ListItem Value="1" Text="Aktif" />
                                                                <asp:ListItem Value="2" Text="Tidak Aktif" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Catatan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="remarks" class="form-control" runat="server" TextMode="MultiLine" placeholder="Catatan"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane m-t-10 fade" id="kontakdarurat">
                                        <div class="row">
                                            <br />
                                            <div class="col-lg-12">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-1 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <span class="font-13">Nama</span>
                                                                        <asp:TextBox ID="Emergency_name1" class="form-control" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <span class="font-13">Alamat :</span>
                                                                        <asp:TextBox ID="Emergency_address1" class="form-control" runat="server" placeholder="Alamat" autocomplete="off" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <span class="font-13">Telepon :</span>
                                                                        <asp:TextBox ID="Emergency_phone1" class="form-control" runat="server" placeholder="Telepon" autocomplete="off" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <span class="font-13">Hubungan :</span>
                                                                        <asp:TextBox ID="Emergency_relation1" class="form-control" runat="server" placeholder="Hubungan" autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Emergency_name2" class="form-control" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <asp:TextBox ID="Emergency_address2" class="form-control" runat="server" placeholder="Alamat" autocomplete="off" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <asp:TextBox ID="Emergency_phone2" class="form-control" runat="server" placeholder="Telepon" autocomplete="off" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <asp:TextBox ID="Emergency_relation2" class="form-control" runat="server" placeholder="Hubungan" autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane m-t-10 fade" id="riwayatpendidikan">
                                        <div class="col-md-offset-1">
                                            <h4>Formal</h4>
                                        </div>
                                        <div class="row">
                                            <br />
                                            <div class="col-lg-12">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-1 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <span class="font-13">Pendidikan :</span>
                                                                        <asp:TextBox ID="TextBox3" class="form-control" Width="85%" runat="server" Text="SD / MI" ReadOnly="true" disabled="" />
                                                                    </td>
                                                                    <td>
                                                                        <span class="font-13">Institusi :</span>
                                                                        <asp:TextBox ID="edu_name1" class="form-control" Width="85%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <span class="font-13">Tahun Pendidikan :</span>
                                                                        <asp:TextBox ID="edu_year1" class="form-control" Width="85%" runat="server" placeholder="Tahun Pendidikan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <span class="font-13">Lokasi :</span>
                                                                        <asp:TextBox ID="edu_location1" class="form-control" TextMode="MultiLine" runat="server" placeholder="Lokasi" autocomplete="off" Style="min-height: 10px" Rows="1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox27" class="form-control" Width="85%" runat="server" Text="SMP / MTs" ReadOnly="true" disabled="" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_name2" class="form-control" Width="85%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_year2" class="form-control" Width="85%" runat="server" placeholder="Tahun Pendidikan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_location2" class="form-control" TextMode="MultiLine" runat="server" placeholder="Lokasi" autocomplete="off" Style="min-height: 10px" Rows="1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox30" class="form-control" Width="85%" runat="server" Text="SMA / SMK / MAN" ReadOnly="true" disabled="" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_name3" class="form-control" Width="85%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_year3" class="form-control" Width="85%" runat="server" placeholder="Tahun Pendidikan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_location3" class="form-control" TextMode="MultiLine" runat="server" placeholder="Lokasi" autocomplete="off" Style="min-height: 10px" Rows="1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox34" class="form-control" Width="85%" runat="server" Text="Universitas" ReadOnly="true" disabled="" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_name4" class="form-control" Width="85%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_year4" class="form-control" Width="85%" runat="server" placeholder="Tahun Pendidikan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_location4" class="form-control" TextMode="MultiLine" runat="server" placeholder="Lokasi" autocomplete="off" Style="min-height: 10px" Rows="1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_5" class="form-control" Width="85%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_name5" class="form-control" Width="85%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_year5" class="form-control" Width="85%" runat="server" placeholder="Tahun Pendidikan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_location5" class="form-control" TextMode="MultiLine" runat="server" placeholder="Lokasi" autocomplete="off" Style="min-height: 10px" Rows="1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_6" class="form-control" Width="85%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_name6" class="form-control" Width="85%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_year6" class="form-control" Width="85%" runat="server" placeholder="Tahun Pendidikan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="edu_location6" class="form-control" TextMode="MultiLine" runat="server" placeholder="Lokasi" autocomplete="off" Style="min-height: 10px" Rows="1" />
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
                                        <div class="row">
                                            <br />
                                            <div class="col-lg-12">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-1 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <span class="font-13">Nama Perusahaan :</span>
                                                                        <asp:TextBox ID="work_exp_name1" class="form-control" Width="90%" runat="server" placeholder="Nama Perusahaan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <span class="font-13">Jabatan :</span>
                                                                        <asp:TextBox ID="work_exp_position1" class="form-control" Width="90%" runat="server" placeholder="Jabatan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <span class="font-13">Tahun Masuk :</span>
                                                                        <asp:TextBox ID="work_exp_start1" class="form-control" Width="90%" runat="server" placeholder="Tahun Masuk" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <span class="font-13">Tahun Keluar :</span>
                                                                        <asp:TextBox ID="work_exp_end1" class="form-control" Width="90%" runat="server" placeholder="Tahun Keluar" autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_name2" class="form-control" Width="90%" runat="server" placeholder="Nama Perusahaan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_position2" class="form-control" Width="90%" runat="server" placeholder="Jabatan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_start2" class="form-control" Width="90%" runat="server" placeholder="Tahun Masuk" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_end2" class="form-control" Width="90%" runat="server" placeholder="Tahun Keluar" autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_name3" class="form-control" Width="90%" runat="server" placeholder="Nama Perusahaan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_position3" class="form-control" Width="90%" runat="server" placeholder="Jabatan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_start3" class="form-control" Width="90%" runat="server" placeholder="Tahun Masuk" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_end3" class="form-control" Width="90%" runat="server" placeholder="Tahun Keluar" autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_name4" class="form-control" Width="90%" runat="server" placeholder="Nama Perusahaan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_position4" class="form-control" Width="90%" runat="server" placeholder="Jabatan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_start4" class="form-control" Width="90%" runat="server" placeholder="Tahun Masuk" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_end4" class="form-control" Width="90%" runat="server" placeholder="Tahun Keluar" autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_name5" class="form-control" Width="90%" runat="server" placeholder="Nama Perusahaan" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_position5" class="form-control" Width="90%" runat="server" placeholder="Positon" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_start5" class="form-control" Width="90%" runat="server" placeholder="Tahun Masuk" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="work_exp_end5" class="form-control" Width="90%" runat="server" placeholder="Tahun Keluar" autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane m-t-10 fade" id="tanggungan">
                                        <div class="row">
                                            <br />
                                            <div class="col-lg-12">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-1 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_name1" class="form-control" Width="75%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="dep_gender1" runat="server" class="form-control" Width="100%">
                                                                            <asp:ListItem Value="L">Laki-Laki</asp:ListItem>
                                                                            <asp:ListItem Value="P">Perempuan</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_placeofbirth1" class="form-control" Width="70%" runat="server" placeholder="Tempat Lahir"
                                                                            autocomplete="off" Style="margin-left: 5%;" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_dateofbirth1" runat="server" class="form-control" Width="75%" placeholder="Tanggal Lahir" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_relationship1" class="form-control" Width="75%" runat="server" placeholder="Hubungan"
                                                                            autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_name2" class="form-control" Width="75%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="dep_gender2" runat="server" class="form-control" Width="100%">
                                                                            <asp:ListItem Value="L">Laki-Laki</asp:ListItem>
                                                                            <asp:ListItem Value="P">Perempuan</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_placeofbirth2" class="form-control" Width="70%" runat="server" placeholder="Tempat Lahir"
                                                                            autocomplete="off" Style="margin-left: 5%;" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_dateofbirth2" class="form-control" Width="75%" runat="server" placeholder="Tanggal Lahir" SkinID="tgl" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_relationship2" class="form-control" Width="75%" runat="server" placeholder="Hubungan"
                                                                            autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_name3" class="form-control" Width="75%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="dep_gender3" runat="server" class="form-control" Width="100%">
                                                                            <asp:ListItem Value="L">Laki-Laki</asp:ListItem>
                                                                            <asp:ListItem Value="P">Perempuan</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_placeofbirth3" class="form-control" Width="70%" runat="server" placeholder="Tempat Lahir"
                                                                            autocomplete="off" Style="margin-left: 5%;" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_dateofbirth3" class="form-control" Width="75%" runat="server" placeholder="Tanggal Lahir"
                                                                            SkinID="tgl" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_relationship3" class="form-control" Width="75%" runat="server" placeholder="Hubungan"
                                                                            autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_name4" class="form-control" Width="75%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="dep_gender4" runat="server" class="form-control" Width="100%">
                                                                            <asp:ListItem Value="L">Laki-Laki</asp:ListItem>
                                                                            <asp:ListItem Value="P">Perempuan</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_placeofbirth4" class="form-control" Width="70%" runat="server" placeholder="Tempat Lahir"
                                                                            autocomplete="off" Style="margin-left: 5%;" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_dateofbirth4" class="form-control" Width="75%" runat="server" placeholder="Tanggal Lahir"
                                                                            SkinID="tgl" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_relationship4" class="form-control" Width="75%" runat="server" placeholder="Hubungan"
                                                                            autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_name5" class="form-control" Width="75%" runat="server" placeholder="Nama" autocomplete="off" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="dep_gender5" runat="server" class="form-control" Width="100%">
                                                                            <asp:ListItem Value="L">Laki-Laki</asp:ListItem>
                                                                            <asp:ListItem Value="P">Perempuan</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_placeofbirth5" class="form-control" Width="70%" runat="server" placeholder="Tempat Lahir"
                                                                            autocomplete="off" Style="margin-left: 5%;" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_dateofbirth5" class="form-control" Width="75%" runat="server" placeholder="Tanggal Lahir"
                                                                            SkinID="tgl" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="dep_relationship5" class="form-control" Width="75%" runat="server" placeholder="Hubungan"
                                                                            autocomplete="off" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane m-t-10 fade"id="infopembayaran">
                                        <div class="row">
                                            <br />
                                            <div class="col-lg-6">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Rekening 1 (Utama)</label>
                                                        <div class="col-md-4">
                                                            <span class="font-13">Atas Nama :</span>
                                                            <asp:TextBox ID="in_the_name1" class="form-control" runat="server" placeholder="Atas Nama"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <span class="font-13">No. Rekening :</span>
                                                            <asp:TextBox ID="bank_account_1" class="form-control" runat="server" placeholder="No. Rekening"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Rekening 2</label>
                                                        <div class="col-md-4">
                                                            <span class="font-13">Atas Nama :</span>
                                                            <asp:TextBox ID="in_the_name2" class="form-control" runat="server" placeholder="Atas Nama"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <span class="font-13">No. Rekening :</span>
                                                            <asp:TextBox ID="bank_account_2" class="form-control" runat="server" placeholder="No. Rekening"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Gaji</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="current_salary" class="form-control" runat="server" onkeyup="javascript:this.value=MoneyFormat(this.value);" placeholder="Gaji"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Gaji PPH</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="salary_pph" class="form-control" runat="server" onkeyup="javascript:this.value=MoneyFormat(this.value);" placeholder="Gaji PPH"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Gaji BPJS</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="salary_bpjs" class="form-control" runat="server" onkeyup="javascript:this.value=MoneyFormat(this.value);" placeholder="Gaji BPJS"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Status Pajak</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_status_pajak" class="form-control" runat="server">
                                                                <asp:ListItem Value="" Text="-- Pilih Status Pajak --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">BPJS Pribadi</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_personal_bpjs" runat="server" class="form-control" Width="100%">
                                                                <asp:ListItem Value="1">Ya</asp:ListItem>
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jumlah BPJS Pribadi</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="personal_bpjs_value" class="form-control" runat="server" placeholder="Jumlah BPJS Pribadi"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">BPJS Ditanggung Orangtua/Suami/Istri</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_bpjs_ditanggung" runat="server" class="form-control" Width="100%">
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
                                                                    <asp:RadioButton ID="radio_npwp" runat="server" GroupName="npwp" Text="NPWP" OnCheckedChanged="radio_1" AutoPostBack="true" Checked="true" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="radio">
                                                                    <asp:RadioButton ID="radio_non_npwp" runat="server" GroupName="npwp" Text="Non NPWP" OnCheckedChanged="radio_2" AutoPostBack="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="div_npwp_number" runat="server" visible="true">
                                                        <label class="col-md-4 control-label">No. NPWP</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="npwp_number" class="form-control" runat="server" onkeyup="javascript:this.value=NPWPFormat(this.value);" required></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Grup Payroll</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddl_payroll_group" class="form-control" runat="server">
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
                        <div class="form-group">
                            <label class="col-md-2 control-label">&nbsp;</label>
                            <div class="col-md-8">
                                <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="BtnSubmit" AccessKey="s" />
                                <asp:Button ID="Back" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Batal" name="cancel" OnClick="BtnCancel" AccessKey="s" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Submit" />
                <asp:PostBackTrigger ControlID="Back" />
                <asp:AsyncPostBackTrigger ControlID="nama_depan" EventName="TextChanged" />
            </Triggers>
            </asp:UpdatePanel>
            </form>
        </div>
        <ucfooter:footer ID="footer" runat="server" />
        </div>
    </div>
</body>

<script>
    //jQuery('#tanggal_lahir').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    //jQuery('#start_of_employment').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    //jQuery('#contract_start').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    //jQuery('#contract_end').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    //jQuery('#dep_dateofbirth1').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //jQuery('#dep_dateofbirth2').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //jQuery('#dep_dateofbirth3').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //jQuery('#dep_dateofbirth4').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //jQuery('#dep_dateofbirth5').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    function Convert() {
        var Namadepan = jQuery("#nama_depan").val();
        var Namatengah = jQuery("#nama_tengah").val();
        var Namabelakang = jQuery("#nama_belakang").val();
        var NamaLengkap = Namadepan + " " + Namatengah + " " + Namabelakang;

        //var kilometers = meters / 1000;
        jQuery("#nama_lengkap").val(NamaLengkap);
    }

    function pageLoad() {
        $('#tanggal_lahir').unbind();
        $('#tanggal_lahir').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#start_of_employment').unbind();
        $('#start_of_employment').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#contract_start').unbind();
        $('#contract_start').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#contract_end').unbind();
        $('#contract_end').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth1').unbind();
        $('#dep_dateofbirth1').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth2').unbind();
        $('#dep_dateofbirth2').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth3').unbind();
        $('#dep_dateofbirth3').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth4').unbind();
        $('#dep_dateofbirth4').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth5').unbind();
        $('#dep_dateofbirth5').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });
    }
    <%--function HideLabel() {
        document.getElementById('<%= note.ClientID %>').style.display = "none";
        document.getElementById('<%= note.ClientID %>').appendChild = "";
    }
    setTimeout("HideLabel();", 30000);--%>
</script>
<ucscript:script ID="script" runat="server" />
</html>

