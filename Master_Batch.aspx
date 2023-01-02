<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Master_Batch.aspx.cs" Inherits="Master_Batch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
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
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Batch<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" 
                onclick="BtnShowSearchPanel_Click" />
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
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" 
                                                                    AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlAcadYear_SelectedIndexChanged" />
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
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" 
                                    onclick="BtnSearch_Click"/>
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click"  />
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
                            Max Batch Strength
                        </th>
                        <th align="left" style="width: 25%">
                            Product(s)
                        </th>
                        <th style="width: 100px; text-align: center;">
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
                        <td  style="width: 100px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit Batch" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server" CommandName="Edit" />
                            <asp:LinkButton ID="lnkDelInfo" ToolTip="Create New Batch" CommandName="Replicate" class="btn-small btn-inverse icon-camera"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server" />
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
                            Batch Strength
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
                        Create New Batch(es)
                    </h5>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label3">Division</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="ddlDivision_Add" ToolTip="Division" data-placeholder="Select Division"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label4">Academic Year</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="ddlAcadYear_Add" ToolTip="Academic Year" data-placeholder="Select Acad Year" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged"
                                                                CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label5">Standard</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="ddlStandard_Add" ToolTip="Standard" data-placeholder="Select Standard"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label6">Product(s)</asp:Label>&nbsp;<span class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Students who have taken admission for these Product(s) will be available for the new Academic Batch(es)"
                                                                title="Product(s)">?</span>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:ListBox runat="server" ID="ddlProduct_Add" ToolTip="Product(s)" SelectionMode="Multiple"
                                                                data-placeholder="Select Product(s)" CssClass="chzn-select" AutoPostBack="True" class="span12" OnSelectedIndexChanged="ddlProduct_Add_SelectedIndexChanged"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label7">Subject(s)</asp:Label>&nbsp;<span class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Subject(s) selected will be available for Academic activity"
                                                                title="Subject(s)">?</span>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:ListBox runat="server" ID="ddlSubject_Add" ToolTip="Subject(s)" SelectionMode="Multiple"
                                                                data-placeholder="Select Subject(s)" CssClass="chzn-select" AutoPostBack="True" class="span12" OnSelectedIndexChanged="ddlSubject_Add_SelectedIndexChanged"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label1">Centre(s)</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:ListBox runat="server" ID="ddlCentre_Add" ToolTip="Select Centre(s)" SelectionMode="Multiple"
                                                                data-placeholder="Select Centre(s)" CssClass="chzn-select" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label2">Max Batch Strength</asp:Label>&nbsp;<span class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Maximum number of students that are allowed in the Batch"
                                                                title="Max Batch Strength">?</span>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:TextBox runat="server" ID="txtBatchStrength_Add" ToolTip="Max Batch Strength" type="text"
                                                                Width="130px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtBatchStrength_Add" ErrorMessage="Maximum Marks can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Only Numbers are allowed in Max Batch Strength field !!"
                                                                ValidationExpression="([0-9])*" ControlToValidate="txtBatchStrength_Add" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server" OnClick="BtnSaveAdd_Click"
                            Text="Save" ValidationGroup="UcValidate" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivEditPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label runat="server" ID="lblEditBatchDetails_Header">Edit Batch Details</asp:Label>
                    </h5>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelEdit" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label9">Division</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="lblDivision_Edit" class="blue"></asp:Label>
                                                            <asp:Label runat="server" ID="lblPKey_Edit" visible ="false" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label13">Academic Year</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="lblAcadYear_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label14">Standard</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="lblStandard_Edit" class="blue"></asp:Label>
                                                            <asp:Label runat="server" ID="lblStandardCode_Edit" class="blue" Visible ="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label21">Centre</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="lblCentre_Edit" class="blue"></asp:Label>
                                                            <asp:Label runat="server" ID="lblCentreCode_Edit" Visible ="false" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label24">Batch Name</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="lblBatchName_Edit" class="blue"></asp:Label>
                                                            &nbsp;|&nbsp;<asp:TextBox runat="server" ID="txtBatchShortName_Edit" Width ="60px" class="ace-tooltip" title="Batch Short Name" data-placement="right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label26">Current Batch Strength</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="lblCurBatchStrength_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label19">Product(s)</asp:Label>&nbsp;<span class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Students who have taken admission for these Product(s) will be available for the new Academic Batch(es)"
                                                                title="Product(s)">?</span>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:ListBox runat="server" ID="ddlProduct_Edit" ToolTip="Product(s)" SelectionMode="Multiple"
                                                                data-placeholder="Select Product(s)" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_Edit_SelectedIndexChanged"/>
                                                            <asp:Label runat="server" ID="lblProduct_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label20">Subject(s)</asp:Label>&nbsp;<span class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Subject(s) selected will be available for Academic activity"
                                                                title="Subject(s)">?</span>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:ListBox runat="server" ID="ddlSubject_Edit" ToolTip="Subject(s)" SelectionMode="Multiple"
                                                                data-placeholder="Select Subject(s)" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_Edit_SelectedIndexChanged" />
                                                            <asp:Label runat="server" ID="lblSubject_Edit" class="blue"></asp:Label>
                                                            <asp:ListBox runat="server" ID="ddlSubject_Edit_Hidden" Visible ="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label22">Max Batch Strength</asp:Label>&nbsp;<span class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Maximum number of students that are allowed in the Batch"
                                                                title="Max Batch Strength">?</span>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:TextBox runat="server" ID="txtBatchStrength_Edit" ToolTip="Max Batch Strength" type="text"
                                                                Width="130px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="UcValidateEdit"
                                                                ControlToValidate="txtBatchStrength_Edit" ErrorMessage="Max Batch Strength can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Only Numbers are allowed in Max Batch Strength field !!"
                                                                ValidationExpression="([0-9])*" ControlToValidate="txtBatchStrength_Edit" ValidationGroup="UcValidateEdit">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="lblBolckBatch">Block Batch&nbsp;</asp:Label>
                                                                <span runat ="server" id="BlockBatchHelp" class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Batch will be marked as Blocked and will not be visible at Centre"
                                                                title="Block Batch">?</span>
                                                             <asp:Label runat="server" ID="lblNewBatchCount" Visible ="false">New Batch Count</asp:Label>&nbsp;
                                                                <span runat ="server" id="NewBatchCountHelp" class="help-button ace-popover"
                                                                data-trigger="hover" data-placement="right" data-content="Total number of new Batches required in current Centre"
                                                                title="New Batch Count" Visible ="false">?</span>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <label>
                                                                <input runat="server" id="ChkActive" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                            <asp:TextBox runat="server" ID="txtNewBatchCount_Edit" ToolTip="New Batch Count" type="text"
                                                                Width="130px" Visible="False" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="UcValidateReplicate"
                                                                ControlToValidate="txtNewBatchCount_Edit" ErrorMessage="New Batch Count can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Only Numbers are allowed in New Batch Count field !!"
                                                                ValidationExpression="([0-9])*" ControlToValidate="txtNewBatchCount_Edit" ValidationGroup="UcValidateReplicate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblErrorBatchEdit" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                            Text="Save" ValidationGroup="UcValidateEdit" OnClick="BtnSaveEdit_Click" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveReplicate" runat="server"
                            Text="Save" ValidationGroup="UcValidateReplicate" Visible ="false" OnClick="BtnSaveReplicate_Click"/>
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseEdit" Visible="true" OnClick="BtnCloseEdit_Click"
                            runat="server" Text="Close" />
                        <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidateEdit" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidateReplicate" runat="server" />
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

