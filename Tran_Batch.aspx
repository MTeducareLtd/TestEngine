﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Tran_Batch.aspx.cs" Inherits="Tran_Batch" %>

<%@ MasterType VirtualPath="Menu.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Manage Batch<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
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
                                                                <asp:Label runat="server" ID="Label12">Batch Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtBatchName" ToolTip="Batch Name" type="text" Width="130px" MaxLength="50" />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special Characters not allowed in Batch Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtBatchName"
                                                                ValidationGroup="UcValidateSearch">&nbsp</asp:RegularExpressionValidator>
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
                                                                <asp:Label runat="server" ID="Label15">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="142px" ToolTip="Division"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="142px" ToolTip="Academic Year"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17">Standard</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlStandard" Width="142px" ToolTip="Standard" data-placeholder="Select Standard"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="False" />
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
                                                                <asp:Label runat="server" ID="Label18">Centre</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCentre" Width="142px" ToolTip="Test Mode" data-placeholder="Select Centre"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <%--<table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label19">Test Category</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:DropDownList runat="server" ID="ddlTestCategory" Width="142px" ToolTip="Test Category" />
                                                    </td>
                                                </tr>
                                            </table>--%>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <%--<table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label20">Test Type</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:DropDownList runat="server" ID="ddlTestType" Width="142px" ToolTip="Test Type" />
                                                    </td>
                                                </tr>
                                            </table>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" OnClick="BtnSearch_Click"/>
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
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
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" ForeColor="White" runat="server"
                                        Text="Export" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label10">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDivision_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label11">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlGridDisplay"  CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Centre Name</b> </th>
                        <th align="left" style="width: 10%">
                            Standard
                        </th>
                        <th align="left" style="width: 15%">
                            Batch Name
                        </th>
                        <th align="left" style="width: 10%">
                            Batch Short Name
                        </th>
                        <th align="left" style="width: 10%; text-align: center;">
                            Batch Strength
                        </th>
                        <th align="left" style="width: 25%">
                            Product(s)
                        </th>
                        <th style="width: 50px; text-align: center;">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchShortName")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblStrength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxCapacity")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Products")%>' />
                        </td>
                        <td  style="width: 50px; text-align: center;">
                            <asp:LinkButton ID="lnkManageBatch" ToolTip="Manage Batch" class="btn-small btn-primary icon-user"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server" CommandName ="Manage" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" ItemStyle-BackColor="Silver"
                    HorizontalAlign="Left" HeaderStyle-BackColor="Gray" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Centre Name</b> </th>
                        <th align="left" style="width: 10%">
                            Standard
                        </th>
                        <th align="left" style="width: 15%">
                            Batch Name
                        </th>
                        <th align="left" style="width: 10%">
                            Batch Short Name
                        </th>
                        <th align="left" style="width: 10%; text-align: center;">
                            Max Batch Strength
                        </th>
                        <th align="left" style="width: 25%">
                            Product(s)
                        </th> 
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCentre1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblStandard1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBatch1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBatchShortName1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchShortName")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblStrength1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxCapacity")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblProduct1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Products")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        Manage Batch
                    </h5>
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
                                                            <asp:Label runat="server" ID="Label1">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblDivision_Add" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label3">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblAcadYear_Add" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label5">Centre</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblCentre_Add" class="blue"></asp:Label>
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
                                                            <asp:Label runat="server" ID="Label7">Standard</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblStandard_Add" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13">Batch Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblBatchName_Add" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label19">Batch Short Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="lblBatchShortName_Add" Width ="60px" class="ace-tooltip" title="Batch Short Name" data-placement="bottom"></asp:TextBox>
                                                            <button id="btnSaveBatchShortName" runat="server" class="btn btn-small btn-success radius-4" data-rel="tooltip"
                                                                                    data-placement="right" title="Save" onserverclick="btnSaveBatchShortName_ServerClick">
                                                                                    <i class="icon-save"></i>
                                                                                </button>
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
                                                            <asp:Label runat="server" ID="Label21">Product(s)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblProducts_Add" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label23">Subject(s)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblSubjects_Add" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label25">Max Batch Strength</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblMaxBatchStrength_Add" class="blue"></asp:Label>
                                                            <asp:Label runat="server" ID="lblPKey_Add" class="blue" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="ResultRow">
                                            <td colspan="3" class="span12" style="text-align: left">
                                                <asp:UpdatePanel ID="UpdatePanelStudList" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="tabbable">
                                                            <ul class="nav nav-tabs" id="myTab">
                                                                <li class="active"><a data-toggle="tab" href="#CurrentStudent">Students assigned to
                                                                    Current Batch <span class="badge badge-success">
                                                                        <asp:Label runat="server" ID="lblCurrentRecCnt">0</asp:Label></span></a></li>
                                                                <li><a data-toggle="tab" href="#PendingStudent">Students not assigned to Current Batch
                                                                    <span class="badge badge-important">
                                                                        <asp:Label runat="server" ID="lblPendingRecCnt">0</asp:Label></span></a></li>
                                                            </ul>
                                                            <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                                                                <div id="CurrentStudent" class="tab-pane in active">
                                                                    <asp:UpdatePanel ID="UpdatePanel_SelectedStudent" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div class="well well-small">
                                                                                <button id="btnStud_RemoveFromBatch" runat="server" class="btn btn-small btn-inverse radius-4" data-rel="tooltip"
                                                                                    data-placement="right" title="Remove selected Student(s) from Current Batch" onserverclick="btnStud_RemoveFromBatch_ServerClick">
                                                                                    <i class="icon-share-alt"></i>
                                                                                </button>
                                                                                <button id="btnStud_AssignRollNo" runat="server" class="btn btn-small btn-inverse radius-4" data-rel="tooltip"
                                                                                    data-placement="right" title="Assign Roll Numbers" onserverclick="btnStud_AssignRollNo_ServerClick">
                                                                                    <i class="icon-asterisk"></i>
                                                                                </button>
                                                                                <button id="btnStud_EditRollNo" runat="server" visible ="true" class="btn btn-small btn-inverse radius-4" data-rel="tooltip"
                                                                                    data-placement="right" title="Edit Roll Numbers" onserverclick="btnStud_EditRollNo_ServerClick"> 
                                                                                    <i class="icon-edit"></i>
                                                                                </button>
                                                                                <button id="btnStud_SaveRollNo" runat="server" visible ="false" class="btn btn-small btn-success radius-4" data-rel="tooltip"
                                                                                    data-placement="right" title="Save Roll Numbers" onserverclick="btnStud_SaveRollNo_ServerClick"> 
                                                                                    <i class="icon-save"></i>
                                                                                </button>
                                                                                <asp:CheckBox ID="chkStudentAllHidden_Sel" runat="server" Visible="False" />
                                                                            </div>
                                                                            <asp:DataList ID="dlGridDisplay_Selected" CssClass="table table-striped table-bordered table-hover"
                                                                                runat="server" Width="100%">
                                                                                <HeaderTemplate>
                                                                                    <b>
                                                                                        <asp:CheckBox ID="chkStudentAll" runat="server" AutoPostBack="True" OnCheckedChanged="All_Student_ChkBox_Selected_Sel" />
                                                                                        <span class="lbl"></span></b></th>
                                                                                    <th style="width: 15%; text-align: left;">
                                                                                        Roll No
                                                                                    </th>
                                                                                    <th style="width: 40%; text-align: left;">
                                                                                        Student Name
                                                                                    </th>
                                                                                    <th style="width: 25%; text-align: left;">
                                                                                        Product
                                                                                    </th>
                                                                                    <th style="width: 15%; text-align: center;">
                                                                                        Other Batch Count&nbsp;<span class="help-button ace-popover" data-trigger="hover"
                                                                                            data-placement="left" data-content="Number of Academic Batch(es) assigned to the student other than Current Batch for Current Standard"
                                                                                            title="Other Batch Count">?</span>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkStudent" runat="server" />
                                                                                    <span class="lbl"></span></td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lblStudentRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                                        <asp:TextBox ID="txtStudentRollNo" Width="85%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' Visible ="false" onkeypress="return NumberOnly()" MaxLength="10" />
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                                                        <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>'
                                                                                            Visible="False" />
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StreamName")%>' />
                                                                                    </td>
                                                                                    <td style="text-align: center;">
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OtherBatchCount")%>' />
                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div id="PendingStudent" class="tab-pane">
                                                                    <asp:UpdatePanel ID="UpdatePanel_PendingStudent" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div class="well well-small">
                                                                                <button id="btnStud_AddToBatch" runat="server" class="btn btn-small btn-inverse radius-4"
                                                                                    data-rel="tooltip" data-placement="right" title="Add selected Student(s) to Current Batch" onserverclick="btnStud_AddToBatch_ServerClick">
                                                                                    <i class="icon-reply"></i>
                                                                                </button>
                                                                                <asp:CheckBox ID="chkStudentAllHidden" runat="server" Visible="False" />
                                                                            </div>
                                                                            <asp:DataList ID="dlGridDisplay_Pending" CssClass="table table-striped table-bordered table-hover"
                                                                                runat="server" Width="100%">
                                                                                <HeaderTemplate>
                                                                                    <b>
                                                                                        <asp:CheckBox ID="chkStudentAll" runat="server" AutoPostBack="True" OnCheckedChanged="All_Student_ChkBox_Selected" />
                                                                                        <span class="lbl"></span></b></th>
                                                                                    <th style="width: 45%; text-align: left;">
                                                                                        Student Name
                                                                                    </th>
                                                                                    <th style="width: 35%; text-align: left;">
                                                                                        Product
                                                                                    </th>
                                                                                    <th style="width: 15%; text-align: center;">
                                                                                        Other Batch Count&nbsp;<span class="help-button ace-popover" data-trigger="hover"
                                                                                            data-placement="left" data-content="Number of Academic Batch(es) assigned to the student other than Current Batch for Current Standard"
                                                                                            title="Other Batch Count">?</span>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkStudent" runat="server" />
                                                                                    <span class="lbl"></span></td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                                                        <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' Visible="False" />
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StreamName")%>' />
                                                                                    </td>
                                                                                    <td style="text-align: center;">
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OtherBatchCount")%>' />

                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
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
                        Delete Test
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to delete Test
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />. Do you want to Continue ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete" ToolTip="Yes"
                        runat="server" Text="Yes" />
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

