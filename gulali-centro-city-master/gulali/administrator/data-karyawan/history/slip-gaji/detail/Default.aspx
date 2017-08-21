<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_data_karyawan_history" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setting >> Data Karyawan >> History >> Slip Gaji >> Detail</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="data-karyawan" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <div class="container">

                <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <div class="btn-group pull-right m-t-15">
                            <asp:Literal ID="link_href" runat="server"></asp:Literal>
                        </div>
                        <h4 class="page-title">Slip Gaji Detail - <asp:Literal ID="nama_employee" runat="server"/></h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="clearfix">
                            <div class="pull-left">
                                <h3 class="logo invoice-logo">Slip Gaji</h3>
                            </div>
                            <div class="pull-right">
                                <h4>Password Slip Gaji
                                <br>
                                    <strong><asp:Literal ID="password_slip" runat="server"/></strong>
                                </h4>
                            </div>
                        </div>
                        <hr>






                        <asp:Panel ID="UpdatePanel1" runat="server">
                        
                        <table style="width: 100%; font-family: arial; font-size: 10px;">
                                            <tr runat="server">
                                                <td style="width: 10%;">Company</td>
                                                <td style="width: 5%;">:</td>
                                                <td style="width: 10%">Batavianet</td>
                                                <td style="width: 15%"></td>
                                                <td style="width: 10%;">Nomor</td>
                                                <td style="width: 5%">:</td>
                                                <td style="width: 30%">
                                                    <asp:Label ID="Employee_NumberID" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Bulan</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_month" runat="server" /></td>
                                                <td></td>
                                                <td>Nama </td>
                                                <td>: </td>
                                                <td>
                                                    <asp:Label ID="txt_name" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>No. Rekening</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_rekening" runat="server" /></td>
                                                <td></td>
                                                <td>NPWP</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_npwp" runat="server" /></td>
                                            </tr>
                                            <tr runat="server">
                                                <td>Tgl. Diterima</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_tgl_tanggal_terima" runat="server" /></td>
                                                <td>&nbsp;</td>
                                                <td>Status</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_status_ptkp" runat="server" /></td>
                                            </tr>
                                        </table>

                                        <table width="380" style="width: 100%; font-family: arial; font-size: 10px;">
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3"><strong><u><i>Perhitungan PPh 21</i></u></strong></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3"><strong><u><i>I. Penghasilan Teratur Periode 1 (Satu) Tahun</i></u></strong></td>
                                            </tr>
                                            <tr runat="server" id="v_gaji_dan_tunjangan">
                                                <td style="vertical-align: top;">Gaji Dan Tunjangan</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_gaji_dan_tunjangan" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_bpjs_perusahaan">
                                                <td style="width: 30%;">BPJS Kesehatan (Perusahaan)</td>
                                                <td style="width: 5%;">:</td>
                                                <td>
                                                    <asp:Label ID="txt_bpjs_kesehatan_corp" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_bpjs_perusahaan2">
                                                <td>BPJS Ketenagakerjaan (Perusahaan)</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_bpjs_ketenagakerjaan_corp" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_total_teratur">
                                                <td><strong><i>Total Penghasilan Teratur</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_penghasilan_teratur" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <strong><u><i>II. Penghasilan Tak Teratur Periode 1 (Satu) Tahun</i></u></strong>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="v_thr">
                                                <td>THR</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_thr" runat="server" />
                                                </td>
                                            </tr>
                                            <tr runat="server" id="v_total_overtime">
                                                <td>Lembur</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_overtime" runat="server" />
                                                </td>
                                            </tr>
                                            <tr runat="server" id="v_total_tambahan">
                                                <td>Bonus</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_bonus" runat="server" />
                                                </td>
                                            </tr>
                                            <tr runat="server" id="v_total_incentive">
                                                <td>Insentif</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_incentive" runat="server" />
                                                </td>
                                            </tr>
                                            <tr runat="server" id="v_total_allowance">
                                                <td>Tunjangan Lainnya</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_allowance" runat="server" />
                                                </td>
                                            </tr>
                                            <asp:Literal ID="tunjangan_tak_teratur" runat="server"></asp:Literal>
                                            <tr runat="server" id="v_bpjs_pribadi">
                                                <td>BPJS Pribadi</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_bpjs_pribadi" runat="server" />
                                                </td>
                                            </tr>
                                            <tr runat="server" id="v_total_tak_teratur">
                                                <td><strong><i>Total Penghasilan Tak Teratur</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_total_tak_teratur" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_bruto">
                                                <td><strong><i>Total Penghasilan Bruto Setahun</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_bruto" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3"><strong><u><i>III. Pengurang Penghasilan</i></u></strong></td>
                                            </tr>
                                            <tr runat="server" id="v_bpjs_karyawan">
                                                <td>BPJS Kesehatan (Karyawan)</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_kesehatan_empl" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_bpjs_karyawan2">
                                                <td>BPJS Ketenagakerjaan (Karyawan)</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_ketenagakerjaan_empl" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_total_unpaid_leave">
                                                <td>Unpaid Leave</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_unpaid_leave" runat="server" />
                                                </td>
                                            </tr>
                                            <tr runat="server" id="v_biaya_jabatan">
                                                <td>Biaya Jabatan</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_biaya_jabatan" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_pengurang_penghasilan">
                                                <td><strong><i>Total Pengurang Penghasilan</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_deduction" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_netto">
                                                <td><strong><i>Total Penghasilan Netto Setahun</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_netto" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_ptkp">
                                                <td>PTKP</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_ptkp" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_kena_pajak">
                                                <td><strong><i>Total Penghasilan Kena Pajak</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_kena_pajak" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3"><strong><u><i>IV. PPh 21</i></u></strong></td>
                                            </tr>
                                            <tr runat="server" id="v_pph_pkp">
                                                <td>Tarif PPh x Total PKP</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_pph_pkp" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_pph_pkp1">
                                                <td style="vertical-align: top;"><strong><i>PPh 21 Per Tahun</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_pph_pkp1" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_pph_sudah_dipotong">
                                                <td><strong><i>PPh 21 Yang Sudah Dipotong Di 2016</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_pph_sudah_dipotong" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_pph_periode_ini">
                                                <td><strong><i>PPh 21 Periode Ini</i></strong></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_pph_periode_ini" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <strong><u><i>Take Home Pay</i></u></strong>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="v_gapok">
                                                <td style="vertical-align: top;">Gaji Pokok </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_homepay_gapok" runat="server" />
                                                </td>
                                            </tr>
                                            <asp:Literal ID="list_tunjangan" runat="server"></asp:Literal>
                                            <tr runat="server" id="v_thr2">
                                                <td>Tunjangan Hari Raya</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_homepay_thr" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_overtime1">
                                                <td>Lembur</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_homepay_overtime" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_bonus1">
                                                <td>Bonus</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_homepay_bonus" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_incentive1">
                                                <td>Insentive</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_homepay_incentive" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_bpjs_total">
                                                <td>Potongan BPJS Total</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_homepay_bpjs_total" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_unpaidleave3">
                                                <td>Unpaid Leave</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_homepay_unpaidleave" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_pph_periode_ini2">
                                                <td>PPh 21 Bulan Berjalan</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="txt_homepay_pph_periode_ini" runat="server" /></td>
                                            </tr>
                                            <tr runat="server" id="v_take_home_pay">
                                                <td><i><strong>Take Home Pay</strong></i></td>
                                                <td>:</td>
                                                <td><i><strong>
                                                    <asp:Label ID="take_homepay" runat="server" /></strong></i></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                        </table>
                        
                        </asp:Panel>
                        



                       
                        <hr>
                        <div class="hidden-print">
                            <div class="pull-left">
                                <%--<a href="javascript:window.print()" class="btn btn-inverse waves-effect waves-light"><i class="fa fa-print"></i></a>
                                <a href="#" class="btn btn-primary waves-effect waves-light">Submit</a>--%>
                                <asp:Button  CssClass="btn btn-primary" runat="server" Text="Download" class="hov_button" OnClick="btnDownload_Click" ID="btnDownload" />
                            </div>
                        </div>
                    </div>
                </div>


                <ucfooter:footer ID="footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
