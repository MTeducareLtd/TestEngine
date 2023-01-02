<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Master_Classroom.aspx.cs" Inherits="Master_Classroom" %>
<%@ MasterType VirtualPath="Menu.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function openModalAuthorise() {
            $('#DivAuthorise').modal({
                backdrop: 'static'
            })

            $('#DivAuthorise').modal('show');
        }

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Premises<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click"/>
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="New" OnClick="BtnAdd_Click" />
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <!--/page-header-->
        <div class="row-fluid">
            <!-- -->
            <!-- PAGE CONTENT BEGINS HERE -->
            <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="alert alert-block alert-success" id="Msg_Success" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-ok"></i></strong>
                            <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                    <div class="alert alert-error" id="Msg_Error" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-remove"></i>Error!</strong>
                            <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="DivSearchPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Search Options
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12">Premises Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtClassroomName" ToolTip="Classroom Name" type="text" Width="130px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15">Country</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCountry" Width="142px" ToolTip="Country"
                                                                    data-placeholder="Select Country" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16">State</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlState" Width="142px" ToolTip="State"
                                                                    data-placeholder="Select State" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17">City</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCity" Width="142px" ToolTip="Standard" data-placeholder="Select City"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18">Location</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlLocation" Width="142px" ToolTip="Location"
                                                                    data-placeholder="Select Location" CssClass="chzn-select" SelectionMode="Multiple" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label11">Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlPremisesType" Width="142px" ToolTip="Premises Type" data-placeholder="Select Type"
                                                                    CssClass="chzn-select" AutoPostBack="True"  />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Premises Name</b> </th>
                        <th align="left" style="width: 15%">
                            Location
                        </th>
                        <th align="left" style="width: 10%">
                            Premises Size
                        </th>
                        <th align="left" style="width: 10%">
                            Premises Type
                        </th>
                        <th align="left" style="width: 30%">
                            Address
                        </th>
                        <th style="width: 80px; text-align: center;">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Premises_LName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Location_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Area_inSQFeet")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PremisesType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Premises_Address")%>' />
                        </td>
                        <td style="width: 80px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit Premises" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Premises_Code")%>' runat="server"
                                CommandName="Edit" />
                            <asp:LinkButton ID="LinkButton1" ToolTip="Bolck Premises" CommandName="Delete" class="btn-small btn-inverse icon-trash"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Premises_Code")%>' runat="server"
                                 />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" ItemStyle-BackColor="Silver"
                    HorizontalAlign="Left" HeaderStyle-BackColor="Gray" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Premises Name</b> </th>
                        <th align="left" style="width: 10%">
                            Location
                        </th>
                        <th align="left" style="width: 10%">
                            Premises Size
                        </th>
                        <th align="left" style="width: 10%">
                            Premises Type
                        </th>
                        <th align="left" style="width: 30%">
                            Address
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Premises_LName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Location_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Area_inSQFeet")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PremisesType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Premises_Address")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_Add" runat="server" Text="Create New Premises" />
                    </h5>
                    <asp:Label ID="lblTestPKey_Hidden" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label23">Premises Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtPremisesName_Add" ToolTip="Premises Name" type="text"
                                                                Width="130px" ValidationGroup="UcValidate" MaxLength="50" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtPremisesName_Add" ErrorMessage="Premises Name can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Special Characters not allowed in Premises Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtPremisesName_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label10">Shortname</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtPremisesShortName_Add" ToolTip="Shortname" type="text"
                                                                Width="130px" ValidationGroup="UcValidate" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special Characters not allowed in Premises Short Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtPremisesShortName_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label30">Company</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCompany_Add" Width="142px" ToolTip="Company"
                                                                data-placeholder="Select Company" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" ID="lblCompany_Add" Visible="False"></asp:Label>
                                                            <asp:Label runat="server" ID="lblCompanyCode_Add" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label3">Country</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCountry_Add" Width="142px" ToolTip="Country"
                                                                data-placeholder="Select Country" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" ID="lblCountry_Add" Visible="False"></asp:Label>
                                                            <asp:Label runat="server" ID="lblPKey_PremisesCode" Visible="false" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label4">State</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlState_Add" Width="142px" ToolTip="State"
                                                                data-placeholder="Select State" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlState_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" ID="lblState_Add" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label5">City</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCity_Add" Width="142px" ToolTip="City"
                                                                data-placeholder="Select City" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" ID="lblCity_Add" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label6">Location</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlLocation_Add" Width="142px" ToolTip="Location"
                                                                data-placeholder="Select Location" CssClass="chzn-select" OnSelectedIndexChanged="ddlLocation_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label1">Premises Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlPremisesType_Add" Width="142px" ToolTip="Premises Type"
                                                                data-placeholder="Select Premises Type" CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13">Active Premises</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox"
                                                                    class="ace-switch ace-switch-2" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label2">Size (in Sq. feet)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtPremisesSize_Add" ToolTip="Premises Size" type="text"
                                                                Width="130px" ValidationGroup="UcValidate" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Special Characters not allowed in Premises Size !!"
                                                                ValidationExpression="([0-9]|[.]|[ ])*" ControlToValidate="txtPremisesSize_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td colspan="2" class="span8" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 19.75%;">
                                                            <asp:Label runat="server" ID="Label40">Address</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 80.25%;">
                                                            <asp:TextBox runat="server" ID="txtAddress_Add" ToolTip="Address" type="text" Width="350px"
                                                                MaxLength="250" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div id ="DivClassRoomAdd" runat ="server" class="table-header" visible ="false" >
                                <table width="100%">
                                    <tr>
                                        <td class="span10">
                                            Total No of Rooms in this Premises:
                                            <asp:Label runat="server" ID="lblRoomCnt_InPremises" Text="0" />
                                        </td>
                                        <td style="text-align: right" class="span2">
                                            <asp:LinkButton runat="server" ID="btnNewRoom" ToolTip="New Room" class="btn-small btn-danger icon-2x icon-plus-sign"
                                                Height="25px" OnClick="btnNewRoom_Click"/>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:DataList ID="dlClassRoom" CssClass="table table-striped table-bordered table-hover" 
                                runat="server" Width="100%"  visible ="false" OnItemCommand="dlClassRoom_ItemCommand">
                                <HeaderTemplate>
                                    <b>Room Name</b> </th>
                                    <th align="left" style="width: 15%">
                                        Room Type
                                    </th>
                                    <th align="left" style="width: 10%">
                                        Room Size
                                    </th>
                                    <th align="left" style="width: 10%">
                                        Primary Owner
                                    </th>
                                    <th align="left" style="width: 10%">
                                        Status
                                    </th>
                                    <th style="width: 50px; text-align: center;">
                                    Action
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Classroom_LName")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ClassRoomType_Name")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Area_inSQFeet")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Primary_Centre_Name")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label41" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RoomStatus")%>' />
                                    </td>
                                    <td style="width: 50px; text-align: center;">
                                        <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit Premises" class="btn-small btn-primary icon-info-sign"
                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Classroom_Code")%>' runat="server"
                                            CommandName="Edit" />
                                        <%--<asp:LinkButton ID="LinkButton1" ToolTip="Bolck Premises" CommandName="Delete" class="btn-small btn-inverse icon-trash"
                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Classroom_Code")%>' runat="server" />--%>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="BtnSave_Click"/>
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server" OnClick="BtnSaveEdit_Click"
                            Text="Save" ValidationGroup="UcValidate" Visible ="false"  />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>

                    

                </div>
            </div>
        </div>
        <div id="DivAddPanel_Classroom" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_Room_Add" runat="server" Text="Create New Room" />
                    </h5>
                    <asp:Label ID="Label19" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label26">Premises Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" class="blue" ID="lblPremisesName_InClassroom"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label27">Premises Shortname</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" class="blue" ID="lblPremisesShortName_InClassroom"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <asp:Label runat="server" Visible ="false"  ID="lblPKey_ClassroomCode"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label37">Room Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlClassroomType_Add" Width="142px" ToolTip="Room Type"
                                                                data-placeholder="Select Room Type" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlClassroomType_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label20">Room Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtClassroomName_Add" ToolTip="Room Name" type="text"
                                                                Width="130px" ValidationGroup="UcValidate_P" MaxLength="50" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="UcValidate_P"
                                                                ControlToValidate="txtClassroomName_Add" ErrorMessage="Room Name can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Special Characters not allowed in Room Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtClassroomName_Add"
                                                                ValidationGroup="UcValidate_P">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label25">Shortname</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtClassroom_ShortName_Add" ToolTip="Shortname" type="text"
                                                                Width="130px" ValidationGroup="UcValidate_P" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Special Characters not allowed in Room Short Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtClassroom_ShortName_Add"
                                                                ValidationGroup="UcValidate_P">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            
                                        </tr>
                                        
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label39">Length (in feet)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtLength_Add" ToolTip="Classroom Length" type="text"
                                                                Width="130px" ValidationGroup="UcValidate" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Special Characters not allowed in Classroom Length !!"
                                                                ValidationExpression="([0-9]|[.]|[ ])*" ControlToValidate="txtLength_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label7">Width (in feet)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtWidth_Add" ToolTip="Classroom Width" type="text"
                                                                Width="130px" ValidationGroup="UcValidate" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Special Characters not allowed in Classroom Width !!"
                                                                ValidationExpression="([0-9]|[.]|[ ])*" ControlToValidate="txtWidth_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label8">Height (in feet)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtHeight_Add" ToolTip="Classroom Height" type="text"
                                                                Width="130px" ValidationGroup="UcValidate" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Special Characters not allowed in Classroom Height !!"
                                                                ValidationExpression="([0-9]|[.]|[ ])*" ControlToValidate="txtHeight_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label38">Active Room</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="Checkbox1" name="switch-field-1" type="checkbox"
                                                                    class="ace-switch ace-switch-2" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="row-fluid">
                                        <div class="span6">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Room Capacity
                                                    </h5>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlCapacity_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%" OnItemDataBound="dlCapacity_Add_ItemDataBound">
                                                            <HeaderTemplate>
                                                                <b>Activity</b>
                                                                </th>
                                                                <th align="left" style="width: 30%">
                                                                    Capacity
                                                                </th>
                                                                <th align="left" style="width: 30%">
                                                                    Unit
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                     <asp:Label ID="lblDL_Activity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_Name")%>' />
                                                                     <asp:Label ID="lblDL_Activity_Id" runat="server" Visible ="false"  Text='<%#DataBinder.Eval(Container.DataItem,"Activity_ID")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDL_Capacity" runat="server" Width ="50px" onkeypress="return NumberOnly()" Text='<%#DataBinder.Eval(Container.DataItem,"Capacity")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlDLUOM" runat="server" CssClass="chzn-select" Width ="100px" 
                                                                        Visible="true"/>
                                                                    <asp:Label ID="lblDLUOM" runat="server" Visible ="false"  Text='<%#DataBinder.Eval(Container.DataItem,"UOM")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="span6">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Assign Centres
                                                    </h5>
                                                    <asp:CheckBox ID="chkCentreAllHidden_Sel" runat="server" Visible="False" />
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlCentre_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <HeaderTemplate>
                                                                <b>
                                                                    <asp:CheckBox ID="chkCentreAll" runat="server" AutoPostBack="True" OnCheckedChanged="All_Centre_ChkBox_Selected" />
                                                                    <span class="lbl"></span></b></th>
                                                                <th align="left" style="width: 30%">
                                                                    Division
                                                                </th>
                                                                <th align="left" style="width: 45%">
                                                                Centre
                                                                </th>
                                                                <th align="left" style="width: 15%">
                                                                Primary Owner
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkCentre" runat="server" OnCheckedChanged="chkCentre_Checked" AutoPostBack="True" />
                                                                <span class="lbl"></span>
                                                                <asp:Label ID="lblCenterCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Code")%>'
                                                                    Visible="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Name")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCenterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Name")%>' />
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <input runat="server" id="chkDL_PrimaryFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2" visible ="false"  />
                                                                        <span class="lbl"></span>
                                                                    </label>
                                                                 
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label42" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnSave_Classroom" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="btnSave_Classroom_Click" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnSave_Classroom_Edit" runat="server"
                            Text="Save" ValidationGroup="UcValidate" Visible ="false" OnClick="btnSave_Classroom_Edit_Click"  />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnClose_Classroom" Visible="true"
                            runat="server" Text="Close" />
                        <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Block Classroom
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to block classroom 
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />. Do you want to Continue ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                        ToolTip="Yes" runat="server" Text="Yes" OnClick="btnDelete_Yes_Click"/>
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>

