<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Config_Mode.aspx.cs" Inherits="Config_Mode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <div id="breadcrumbs" class ="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Configuration<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Test Mode<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
        </div>
        <!--#nav-search-->
    </div>

        <div id="page-content" class="clearfix">
            <!--/page-header-->
            <div class="row-fluid">
                <!-- -->
                <!-- PAGE CONTENT BEGINS HERE -->
                <div class="alert alert-block alert-success" id="MsgSuccess" visible="false" runat="server">
                    <button type="button" class="close" data-dismiss="alert">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <strong><i class="icon-ok"></i></strong>
                        <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="alert alert-error" id="errormsg" visible="false" runat="server">
                    <button type="button" class="close" data-dismiss="alert">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <strong><i class="icon-remove"></i>Error!</strong>
                        <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div id="DivSearchPanel" runat="server">
                    <div class="widget-box">
                        <div class="widget-header widget-header-small header-color-dark">
                            <h5>
                                Search Options
                            </h5>
                            
                        </div>
                        <div class="widget-body">
                            <div class="widget-body-inner">
                                <div class="widget-main padding-3">
                                    <table width="100%" cellpadding="6">
                                        <tr>
                                            <td class="span2" style="text-align: left">
                                                <asp:Label runat="server" ID="lblServicedby" Text="Mode Name" />
                                            </td>
                                            <td class="span2" style="text-align: left">
                                                <asp:TextBox ID="txtsrchDivName" runat="server" Style="width: 150px" placeholder="Mode Name"
                                                    MaxLength="6" ToolTip="Mode Name" />
                                            </td>
                                            <td class="span8">
                                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div id="DivResultPanel" runat ="server" class="dataTables_wrapper" >
                    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                    <div class="widget-box">
                        <div class="table-header">
                            <table width="100%">
                                <tr>
                                    <td class="span10">
                                        Total No of Records:
                                        <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                    </td>
                                    <td style="text-align: right" class="span2">
                                        <asp:LinkButton ID="HLExport" Font-Underline="true" ForeColor="White" runat="server"
                                            Text="Export" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <asp:DataList ID="dlGridDisplay" onrowcommand="grdBookDetails_RowCommand" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                        <HeaderTemplate>
                            <b>Test Mode</b> </th>
                            <th align="left" style="width: 20%">
                                Status
                            </th>
                            <th align="right" style="width: 20%">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ModeName")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status")%>' />
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit Mode" 
                                    class="btn-small btn-primary icon-info-sign" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ModeCode")%>' runat="server" />
                                <asp:LinkButton ID="LinkButton1" ToolTip="Delete Mode"  CommandName ="Delete"
                                    class="btn-small btn-inverse icon-trash" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ModeCode")%>'  runat="server" />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:DataList ID="dlGridExport" Visible="false" runat="server" ItemStyle-BackColor="Silver"
                        HorizontalAlign="Left" HeaderStyle-BackColor="Gray" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%">
                        <HeaderTemplate>
                            <b>Division Code</b> </th>
                            <th align="left" style="background-color: Gray;">
                            Division Name
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbDivCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Code")%>' />
                            </td>
                            <td style="background-color: Silver;">
                                <asp:Label ID="lbDivName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Name")%>' />
                        </ItemTemplate>
                    </asp:DataList>
                    </ContentTemplate> 
                    </asp:UpdatePanel> 

                </div>
            </div>
            <div id="DivAddPanel" runat ="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Add Test Mode
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main padding-3">
                                <table cellpadding="6" style="border-style: none none solid none; width: 100%; padding: 6px;
                                    border-bottom-width: 0px;">
                                    <tr>
                                        <td class="span2" style="text-align: left">
                                            <asp:Label runat="server" ID="Label23">Mode Name</asp:Label>
                                        </td>
                                        <td class="span2" style="text-align: left">
                                            <asp:TextBox runat="server" ID="txtModeName" ToolTip="Mode name" type="text" />
                                        </td>
                                        <td class="span2" style="text-align: left">
                                            <asp:Label runat="server" ID="Label2">Active</asp:Label>
                                        </td>
                                        <td class="span2" style="text-align: left">
                                            <label>
                                                <input runat="server" id="ChkActive" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2" />
                                                <span class="lbl"></span>
                                            </label>
                                        </td>
                                        <td class="span2" style="text-align: left">
                                            <asp:Label runat="server" ID="lbldivCode" Text="Division Code" Visible="false" />
                                        </td>
                                        <td class="span2" style="text-align: left">
                                            <asp:Label runat="server" ID="txtdivCode" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class= "widget-main alert-block alert-success  alert- " style="text-align: center;">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                                Text="Save" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        </div>
                    </div>
                    
                </div>

            </div>
            
        </div>
        <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Delete Test Mode
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to delete Test Mode 
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />.  Do you want to Continue ? 
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4"  ID="btnDelete"
                        ToolTip="Yes" runat="server" Text="Yes" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnCancel" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>

