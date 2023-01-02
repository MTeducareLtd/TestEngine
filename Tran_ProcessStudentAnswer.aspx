<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Tran_ProcessStudentAnswer.aspx.cs" Inherits="Tran_ProcessStudentAnswer"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function showProgress() {
            var UpdateProgress1 = $get("<%= UpdateProgress1.ClientID %>");
            UpdateProgress1.style.display = "block";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Process Student Answers<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
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
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadyear" Width="215px" data-placeholder="Select Acad Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlStandard" Width="215px" ToolTip="Course" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Test Category</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTestCategory" Width="215px" data-placeholder="Select Test Category"
                                                                    CssClass="chzn-select" OnSelectedIndexChanged="ddlTestCategory_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none; margin-bottom: 0px;" class="table-hover"
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19" CssClass="red">Test Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlTestType" Width="215px" ToolTip="Test Type" data-placeholder="Select Test Type"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label20">Test Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtTestName" ToolTip="Test Name" type="text" Width="205px"
                                                                    OnTextChanged="txtTestName_TextChanged" />
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
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29" CssClass="red"> Upload Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    style="width: 205px" id="dtrange" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
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
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" Height="25px" ToolTip="Export"
                                        class="btn-small btn-danger icon-2x icon-download-alt" runat="server" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>File Name</b> </th>
                        <th align="left" style="width: 15%">
                            Course
                        </th>
                        <th align="left" style="width: 15%">
                            Test Type
                        </th>
                        <th align="left" style="width: 15%">
                            Test Name
                        </th>
                        <th style="width: 15%; text-align: center">
                            Upload Date
                        </th>
                        <th style="width: 8%; text-align: center">
                            Correct Record Count
                        </th>
                        <th style="width: 8%; text-align: center">
                            Warning Record Count
                        </th>
                        <th style="width: 80px; text-align: center">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Import_FileName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Import_Run_Date")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Record_Cnt")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Warning_Record_Cnt")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="View File" class="btn-small btn-warning icon-download"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="ViewFile" />
                            <asp:LinkButton ID="lnkViewResult" ToolTip="View File" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="ViewResult" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" HorizontalAlign="Left"
                    CssClass="table table-striped table-bordered table-hover" Width="100%">
                    <HeaderTemplate>
                        <b>File Name</b> </th>
                        <th align="left" style="width: 9%">
                            Course
                        </th>
                        <th align="left" style="width: 8%">
                            Test Type
                        </th>
                        <th align="left" style="width: 11%">
                            Test Name
                        </th>
                        <th style="width: 8%; text-align: center">
                            Upload Date
                        </th>
                        <th style="width: 8%; text-align: center">
                            Correct Record Count
                        </th>
                        <th style="width: 8%; text-align: center">
                        Warning Record Count
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Import_FileName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Import_Run_Date")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Record_Cnt")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Warning_Record_Cnt")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div class="row-fluid">
            <div id="DivAddPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Upload Student Answers
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelAdd" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision_Add" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_Add_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label4" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear_Add" Width="215px" data-placeholder="Select Acad Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcadYear_Add_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label5" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStandard_Add" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_Add_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label23" CssClass="red">Test Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTestType_Add" Width="215px" data-placeholder="Select Test Type"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTestType_Add_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Test Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTestName_Add" Width="215px" data-placeholder="Select Test Name"
                                                                    CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlTestName_Add_SelectedIndexChanged" />
                                                                <i class="icon-bell-alt red icon-animated-bell icon-only" visible="false" runat="server"
                                                                    id="icon_NegativeMarking_Add"></i>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label13" CssClass="red">Conduct No</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlConductNo_Add" Width="215px" data-placeholder="Select Conduct No"
                                                                    CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlConductNo_Add_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label11" CssClass="red">No. of Questions</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtQueCount_Add" ToolTip="No. of Questions" type="text"
                                                                    Width="205px" ReadOnly="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">QP Set Count</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtQPSetCount_Add" ToolTip="QP Set Count" type="text"
                                                                    Width="205px" ReadOnly="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">  Import Date</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtImportDate_Add" ToolTip="Import Date" type="text"
                                                                    Width="205px" ReadOnly="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left; vertical-align: middle;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label7" CssClass="red">ID Column</asp:Label>&nbsp;<span
                                                                    class="help-button ace-popover" data-trigger="hover" data-placement="right" data-content="Enter name of the column in CSV file where Student ID is stored"
                                                                    title="ID Column">?</span>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtStudentIDColumn_Add" ToolTip="Student ID Column"
                                                                    type="text" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td colspan="2" class="span8" style="text-align: left; vertical-align: middle;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 19.75%;">
                                                                <asp:Label runat="server" ID="Label10" CssClass="red">Select File</asp:Label>&nbsp;<span
                                                                    class="help-button ace-popover" data-trigger="hover" data-placement="right" data-content="Select a CSV files that contains Student Results using Browse button"
                                                                    title="Select File">?</span>
                                                                <asp:TextBox runat="server" ID="txtFileName_Add" Width="30px" Visible="false" />
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 80.25%;">
                                                                <div class="row-fluid">
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="70%" ViewStateMode="Enabled" />
                                                                    <asp:Button class="btn btn-app btn-info btn-mini radius-4" ID="btnUpload" runat="server"
                                                                        Text="Upload" OnClick="BtnUpload_Click" OnClientClick="showProgress()" />
                                                                    <asp:TextBox runat="server" ID="txtUploadedFileName" Width="30px" Visible="false" />
                                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelAdd">
                                                                        <ProgressTemplate>
                                                                            <div class="modal">
                                                                                <div class="center">
                                                                                    <img alt="" src="WaitLoad.gif" />
                                                                                </div>
                                                                            </div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:ListBox runat="server" ID="lstPresentSBEntryCode_Add" Width="172px" Visible="false" />
                                                    <asp:ListBox runat="server" ID="lstPresentSBEntryCodeCentre_Add" Width="172px" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr runat="server" id="ResultRow">
                                                <td colspan="3" class="span12" style="text-align: left">
                                                    <div class="tabbable">
                                                        <ul class="nav nav-tabs" id="myTab">
                                                            <li class="active"><a data-toggle="tab" href="#AnswerKey">Correct Records <span class="badge badge-success">
                                                                <asp:Label runat="server" ID="lblSuccessRecCnt">0</asp:Label></span></a></li>
                                                            <li><a data-toggle="tab" href="#StudentResult">Records with Warning <span class="badge badge-warning">
                                                                <asp:Label runat="server" ID="lblWarnRecCnt">0</asp:Label></span></a></li>
                                                            <li><a data-toggle="tab" href="#ErrorRecords">Records with Error <span class="badge badge-important">
                                                                <asp:Label runat="server" ID="lblErrorRecCnt">0</asp:Label></span></a></li>
                                                        </ul>
                                                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                                                            <div id="AnswerKey" class="tab-pane in active">
                                                                <asp:DataList ID="dlCorrectResult" CssClass="table table-striped table-bordered table-hover"
                                                                    runat="server" Width="100%">
                                                                    <HeaderTemplate>
                                                                        <left><b>Row No</b></left>
                                                                        </th>
                                                                        <th style="width: 10%; text-align: left;">
                                                                            Roll No
                                                                        </th>
                                                                        <th style="width: 25%; text-align: left;">
                                                                            Centre Name
                                                                        </th>
                                                                        <th style="width: 10%; text-align: left;">
                                                                            Test Number
                                                                        </th>
                                                                        <th style="width: 10%; text-align: center;">
                                                                            QP Set No
                                                                        </th>
                                                                        <th style="width: 10%; text-align: center;">
                                                                            Status
                                                                        </th>
                                                                        <th style="width: 25%; text-align: left;">
                                                                        Remarks
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ExcelRowNo")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblCentreName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreName")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblTestNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestNumber")%>' />
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblSetNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Set_Number")%>' />
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks")%>' />
                                                                            <asp:Label ID="lblCentreCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CentreCode")%>' />
                                                                            <asp:Label ID="lblSBEntryCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />
                                                                            <asp:Label ID="lblAnswerKey" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Answer_Key")%>' />
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                            <div id="StudentResult" class="tab-pane">
                                                                <asp:DataList ID="dlWarningResult" CssClass="table table-striped table-bordered table-hover"
                                                                    runat="server" Width="100%">
                                                                    <HeaderTemplate>
                                                                        <left><b>Row No</b></left>
                                                                        </th>
                                                                        <th style="width: 10%; text-align: left;">
                                                                            Roll No
                                                                        </th>
                                                                        <th style="width: 25%; text-align: left;">
                                                                            Centre Name
                                                                        </th>
                                                                        <th style="width: 10%; text-align: left;">
                                                                            Test Number
                                                                        </th>
                                                                        <th style="width: 10%; text-align: center;">
                                                                            QP Set No
                                                                        </th>
                                                                        <th style="width: 10%; text-align: center;">
                                                                            Status
                                                                        </th>
                                                                        <th style="width: 25%; text-align: left;">
                                                                        Remarks
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ExcelRowNo")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblCentreName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreName")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblTestNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestNumber")%>' />
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblSetNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Set_Number")%>' />
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks")%>' />
                                                                            <asp:Label ID="lblCentreCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CentreCode")%>' />
                                                                            <asp:Label ID="lblSBEntryCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />
                                                                            <asp:Label ID="lblAnswerKey" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Answer_Key")%>' />
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                            <div id="ErrorRecords" class="tab-pane">
                                                                <asp:DataList ID="dlErrorResult" CssClass="table table-striped table-bordered table-hover"
                                                                    runat="server" Width="100%">
                                                                    <HeaderTemplate>
                                                                        <left><b>Row No</b></left>
                                                                        </th>
                                                                        <th style="width: 10%; text-align: left;">
                                                                            Roll No
                                                                        </th>
                                                                        <th style="width: 25%; text-align: left;">
                                                                            Centre Name
                                                                        </th>
                                                                        <th style="width: 10%; text-align: left;">
                                                                            Test Number
                                                                        </th>
                                                                        <th style="width: 10%; text-align: center;">
                                                                            QP Set No
                                                                        </th>
                                                                        <th style="width: 10%; text-align: center;">
                                                                            Status
                                                                        </th>
                                                                        <th style="width: 25%; text-align: left;">
                                                                        Remarks
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ExcelRowNo")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblCentreName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreName")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblTestNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestNumber")%>' />
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblSetNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Set_Number")%>' />
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status")%>' />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks")%>' />
                                                                            <asp:Label ID="lblCentreCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CentreCode")%>' />
                                                                            <asp:Label ID="lblSBEntryCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />
                                                                            <asp:Label ID="lblAnswerKey" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Answer_Key")%>' />
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                                Text="Save" Visible="False" OnClick="BtnSave_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivEditPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            View Upload Result
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                    <tr>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label9">Division</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_Division" class="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label14">Academic Year</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_AcadYear" class="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label22">Course</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_Standard" class="blue"></asp:Label>
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
                                                        <asp:Label runat="server" ID="Label26">Test Type</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_TestType" class="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label28">Test Name</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_TestName" class="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label30">Conduct No</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_ConductNo" class="blue"></asp:Label>
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
                                                        <asp:Label runat="server" ID="Label31">No. of Questions</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_QueNo" class="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label32">QP Set Count</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_QPSetCount" class="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <i class="icon-calendar"></i>
                                                        <asp:Label runat="server" ID="Label33">  Import Date</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_ImportDate" class="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span4" style="text-align: left; vertical-align: middle;">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label34">ID Column</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblResult_IDColumn" class="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" class="span8" style="text-align: left; vertical-align: middle;">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 19.75%;">
                                                        <asp:UpdatePanel ID="uplpnlReprocess" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label runat="server" ID="Label35">File Name</asp:Label>&nbsp;
                                                                <asp:LinkButton ID="btnResult_Reprocess1" ToolTip="Reprocess" class="btn-small btn-primary icon-refresh"
                                                                    runat="server" OnClick="btnResult_Reprocess1_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 80.25%;">
                                                        <asp:Label runat="server" ID="lblResult_FileName" class="blue"></asp:Label>
                                                        <asp:Label runat="server" ID="lblResult_PKey" Visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblResult_RunPKey" Visible="false"></asp:Label>
                                                        <asp:UpdateProgress ID="UpdateProgress12" runat="server" AssociatedUpdatePanelID="uplpnlReprocess">
                                                            <ProgressTemplate>
                                                                <img alt="progress" src="WaitLoad.gif" />                                                                
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:ListBox runat="server" ID="ListBox1" Width="172px" Visible="false" />
                                            <asp:ListBox runat="server" ID="ListBox2" Width="172px" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr1">
                                        <td colspan="3" class="span12" style="text-align: left">
                                            <div class="tabbable">
                                                <ul class="nav nav-tabs" id="Ul1">
                                                    <li class="active"><a data-toggle="tab" href="#Result_Correct">Correct Records <span
                                                        class="badge badge-success">
                                                        <asp:Label runat="server" ID="lblResult_Success">0</asp:Label></span></a></li>
                                                    <li><a data-toggle="tab" href="#Result_Warning">Records with Warning <span class="badge badge-warning">
                                                        <asp:Label runat="server" ID="Label37">0</asp:Label></span></a></li>
                                                </ul>
                                                <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                                                    <div id="Result_Correct" class="tab-pane in active">
                                                        <asp:DataList ID="DLResult_Correct" CssClass="table table-striped table-bordered table-hover"
                                                            runat="server" Width="100%">
                                                            <HeaderTemplate>
                                                                Roll No </th>
                                                                <th style="width: 15%; text-align: left;">
                                                                    Centre Name
                                                                </th>
                                                                <th style="width: 29%; text-align: left;">
                                                                    Student Name
                                                                </th>
                                                                <th style="width: 8%; text-align: center;">
                                                                    QP Set No
                                                                </th>
                                                                <th style="width: 8%; text-align: center;">
                                                                    Attempt Count
                                                                </th>
                                                                <th style="width: 8%; text-align: center;">
                                                                    Correct Count
                                                                </th>
                                                                <th style="width: 8%; text-align: center;">
                                                                    Un-Correct Count
                                                                </th>
                                                                <th style="width: 8%; text-align: center;">
                                                                    Obtd Marks
                                                                </th>
                                                                <th style="width: 8%; text-align: center;">
                                                                Max Marks
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDLRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lblDLCentreName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lblDLStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblDLTestNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MCQ_Set_Number")%>' />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblSetNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MCQ_Attempt_Count")%>' />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MCQ_Correct_Count")%>' />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="Label38" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MCQ_Wrong_Count")%>' />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="Label39" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ObtdMarks")%>' />
                                                                    <td style="text-align: center;">
                                                                        <asp:Label ID="Label40" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                    <div id="Result_Warning" class="tab-pane">
                                                        <asp:DataList ID="DLResult_Warning" CssClass="table table-striped table-bordered table-hover"
                                                            runat="server" Width="100%">
                                                            <HeaderTemplate>
                                                                <left><b>Row No</b></left>
                                                                </th>
                                                                <th style="width: 10%; text-align: left;">
                                                                    Roll No
                                                                </th>
                                                                <th style="width: 25%; text-align: left;">
                                                                    Centre Name
                                                                </th>
                                                                <th style="width: 10%; text-align: left;">
                                                                    Test Number
                                                                </th>
                                                                <th style="width: 10%; text-align: center;">
                                                                    QP Set No
                                                                </th>
                                                                <th style="width: 10%; text-align: center;">
                                                                    Status
                                                                </th>
                                                                <th style="width: 25%; text-align: left;">
                                                                Remarks
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ExcelRowNo")%>' />
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lblRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lblCentreName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreName")%>' />
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lblTestNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestNumber")%>' />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblSetNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Set_Number")%>' />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status")%>' />
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks")%>' />
                                                                    <asp:Label ID="lblCentreCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CentreCode")%>' />
                                                                    <asp:Label ID="lblSBEntryCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />
                                                                    <asp:Label ID="lblAnswerKey" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Answer_Key")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <%-- </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnResult_Reprocess1" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                            </div>
                        </div>
                        <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                            <!--Button Area -->
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnResult_Close"
                                Visible="true" runat="server" Text="Close" OnClick="btnResult_Close_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 40% !important;
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
