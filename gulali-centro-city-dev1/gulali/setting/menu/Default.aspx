<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Setting_Menu" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Setting >> Menu</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" runat="server" />
    </header>

    <form id="form" runat="server">
        <div class="wrapper">
            <div class="container">

                <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <div class="btn-group pull-right m-t-15">
                            <a href="/gulali/setting/" class="btn btn-custom dropdown-toggle waves-effect waves-light">Kembali <span class="m-l-5"><i class="fa fa-backward"></i></span></a>
                        </div>
                        <h4 class="page-title">Menu</h4>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12">
                        <div class="card-box table-responsive">
                            <div id="datatable-buttons_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">

                                <div style="padding: 10px !important; float: right;">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>
                                            <td style="padding-right: 10px;">
                                                <a class="btn btn-primary" href="add/">Tambah</a>
                                            </td>
                                            <td style="padding-right: 10px;">
                                                <asp:TextBox ID="search_text" class="form-control" runat="server" AutoPostBack="True" placeholder="Cari" OnTextChanged="btnSearch_Click" />
                                            </td>

                                            <td style="text-align: center; padding-right: 20px;">
                                                <asp:Button class="btn" ID="previous2" runat="server" OnClick="previous2_Click" Text="<<" />
                                                <asp:Button class="btn" ID="previous" runat="server" OnClick="previous_Click" Text="<" />
                                            </td>
                                            <td style="text-align: center;">page
                                                    <%--<input  onKeyPress="checkEnter(event,this.value,'','20','');" name="pagenum" type="text" id="pagenum" size="4" value="1" style="text-align:center;"/>--%>
                                                <asp:TextBox ID="pagenum" runat="server" AutoPostBack="True" Width="25px" OnTextChanged="page_enter" />
                                                of
                                                    <asp:Literal ID="count_page" runat="server" />
                                            </td>
                                            <td style="text-align: center; padding-left: 20px;">
                                                <asp:Button class="btn" ID="next" runat="server" OnClick="next_Click" Text=">" />
                                                <asp:Button class="btn" ID="next2" runat="server" OnClick="next2_Click" Text=">>" />
                                            </td>
                                            <td style="text-align: center;" id="td_hide" runat="server" visible="false">showing
                                                        <asp:Literal ID="pagesum" Text="10" runat="server"></asp:Literal>
                                                per page
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <table id="datatable-editable" class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid" aria-describedby="datatable-buttons_info">
                                    <thead>
                                        <tr role="row">
                                            <th class="sorting_asc" tabindex="0" aria-controls="datatable-buttons" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Name: activate to sort column descending" style="width: 181px;">Name</th>
                                            <th class="sorting" tabindex="0" aria-controls="datatable-buttons" rowspan="1" colspan="1" aria-label="Position: activate to sort column ascending" style="width: 295px;">Position</th>
                                            <th class="sorting" tabindex="0" aria-controls="datatable-buttons" rowspan="1" colspan="1" aria-label="Office: activate to sort column ascending" style="width: 132px;">Office</th>
                                            <th class="sorting" tabindex="0" aria-controls="datatable-buttons" rowspan="1" colspan="1" aria-label="Age: activate to sort column ascending" style="width: 65px;">Age</th>
                                            <th class="sorting" tabindex="0" aria-controls="datatable-buttons" rowspan="1" colspan="1" aria-label="Start date: activate to sort column ascending" style="width: 128px;">Start date</th>
                                            <th class="sorting" tabindex="0" aria-controls="datatable-buttons" rowspan="1" colspan="1" aria-label="Salary: activate to sort column ascending" style="width: 102px;">Salary</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <tr role="row" class="odd">
                                            <td class="sorting_1" tabindex="0">Airi Satou</td>
                                            <td>Accountant</td>
                                            <td>Tokyo</td>
                                            <td>33</td>
                                            <td>2008/11/28</td>
                                            <td class="actions" style="text-align: center;"><a href="#" class="on-default edit-row"><i class="fa fa-pencil"></i></a>
                                                <a href="#" class="on-default remove-row"><i class="fa fa-trash-o"></i></a></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- end col -->
                </div>
                <!-- end row -->
            <ucfooter:footer id="footer" runat="server" />
            </div>
            <!-- end container -->
        </div>
    </form>
</body>
</html>
