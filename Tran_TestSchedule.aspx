<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Tran_TestSchedule.aspx.cs" EnableEventValidation="false" Inherits="Tran_TestSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function openModalWarningDelete() {
            $('#DivPendingApproval').modal({
                backdrop: 'static'
            })

            $('#DivPendingApproval').modal('show');
        }


    </script>
    <style type="text/css">
        .style1 {
            width: 215px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">Manage Test Schedule<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->

            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="btnuploadviexcel"
                Width="150px" Text="Upload Via Excel" OnClick="btnuploadviexcel_Click" />
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
                        <h5>Search Options
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
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" AutoPostBack="True"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
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
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" AutoPostBack="True"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStandard" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label28">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label30">Test Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtTestName" ToolTip="Test Name" type="text" Width="205px" />
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
                                                                <asp:Label runat="server" ID="Label29">  Test Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    style="width: 205px" data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19" CssClass="red">Test Category</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTestCategory" Width="215px" data-placeholder="Select Test Category"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label20">Test Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlTestType" Width="215px" data-placeholder="Select Test Type"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <%--<asp:Panel id="pnlContents" runat = "server">
        <span style="font-size: 10pt; font-weight:bold; font-family: Arial">Hello,
            <br />
            This is <span style="color: #18B5F0">Mudassar Khan</span>.<br />
            Hoping that you are enjoying my articles!</span>
    </asp:Panel>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick = "return PrintPanel();" />--%>
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
                                <td class="span6">Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span4">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
                                    &nbsp;
                                    <asp:LinkButton runat="server" ID="lnkPrint" ToolTip="Print" class="btn-small btn-warning icon-2x icon-print"
                                        Height="25px" OnClick="lnkPrint_Click" />
                                    &nbsp;&nbsp;
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
                                        <asp:Label runat="server" ID="Label21">Division</asp:Label>
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
                                        <asp:Label runat="server" ID="Label27">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label46">Centre</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblCentre_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label34">Course</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStandard_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label35">Test Category</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblTestCategory_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left"></td>
                    </tr>
                </table>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th style="width: 10%; text-align: center">Conduct No
                        </th>
                        <th align="left" style="width: 15%">Batch
                        </th>
                        <th align="left" style="width: 10%">Test Type
                        </th>
                        <th align="left" style="width: 20%">Subjects
                        </th>
                        <th style="width: 10%; text-align: center">Max Marks
                        </th>
                        <th style="width: 10%; text-align: center">Test Date
                        </th>
                        <th style="width: 10%; text-align: center">Test Time
                        </th>
                        <th style="width: 70px; text-align: center">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblDLConductNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLSubjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLMaxMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                            <asp:Label ID="lblDLRemarks" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                            <asp:Label ID="lblDLFromTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"FromTimeStr")%>' />
                            <asp:Label ID="lblDLToTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ToTimeStr")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit Test" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="Edit" />
                            <asp:LinkButton ID="LinkButton1" ToolTip="Delete Test" CommandName="Delete" class="btn-small btn-inverse icon-trash"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server" />
                    </ItemTemplate>
                </asp:DataList>
                <div id="divPrint" runat="server" style="width: 100%; padding-bottom: 20px; padding-left: 5px; padding-top: 5px; padding-right: 5px;"
                    visible="false">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <img alt="MT Educare Ltd" class="msg-photo" src="Images/logo.jpg" />
                            </td>
                            <td align="right">
                                <h3 style="font-family: Arial">Test Schedule List</h3>
                            </td>
                        </tr>
                    </table>
                    <table style="border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12px; width: 100%;">
                        <tr>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <b>
                                    <asp:Label runat="server" ID="Label55">Division</asp:Label></b>
                            </td>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <asp:Label runat="server" ID="lblPrintTestDivision"></asp:Label>
                            </td>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <b>
                                    <asp:Label runat="server" ID="Label57">Academic Year</asp:Label></b>
                            </td>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <asp:Label runat="server" ID="lblPrintTestAcademicYear"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <b>
                                    <asp:Label runat="server" ID="Label59">Centre</asp:Label></b>
                            </td>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <asp:Label runat="server" ID="lblPrintTestCentre"></asp:Label>
                            </td>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <b>
                                    <asp:Label runat="server" ID="Label61">Course</asp:Label></b>
                            </td>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <asp:Label runat="server" ID="lblPrintTestCourse"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <b>
                                    <asp:Label runat="server" ID="Label63">Test Category</asp:Label></b>
                            </td>
                            <td style="border-collapse: collapse; border: 1px solid black;">
                                <asp:Label runat="server" ID="lblPrintTestCategory"></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" Width="100%" CssClass="gridFont"
                    ItemStyle-CssClass="gridFont" HeaderStyle-CssClass="gridFont">
                    <HeaderTemplate>
                        <th align="left" style="width: 10%; border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">Test No.
                        </th>
                        <th align="center" style="width: 15%; border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">Date
                        </th>
                        <th align="center" style="width: 20%; border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">Subject
                        </th>
                        <th align="center" style="width: 33%; border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">Syllabus
                        </th>
                        <th align="center" style="width: 12%; border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">Marks
                        </th>
                        <th style="width: 10%; text-align: center; border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">Duration
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <td align="left" style="border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td align="left" style="border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_date")%>' />
                        </td>
                        <td align="left" style="border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Display_name")%>' />
                        </td>
                        <td align="left" style="border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">
                            <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapters")%>' />
                        </td>
                        <td align="center" style="border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">
                            <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>
                        <td align="center" style="border-collapse: collapse; border: 1px solid black; font-family: Arial; font-size: 12">
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Duration")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">Test Schedule
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
                                                            <asp:Label runat="server" ID="Label5" CssClass="red">Center</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCentre_Add" Width="215px" data-placeholder="Select Center"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label23" CssClass="red">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlStandard_Add" Width="215px" data-placeholder="Select Course"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Batch</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <%--<asp:DropDownList runat="server" ID="ddlBatch_Add" Width="215px" data-placeholder="Select Batch"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_Add_SelectedIndexChanged" />--%>

                                                            <asp:ListBox runat="server" ID="lstboxbatch" data-placeholder="Select Batch(s)" Width="215px"
                                                                CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"
                                                                OnSelectedIndexChanged="lstboxbatch_SelectedIndexChanged"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label10" CssClass="red">Conduct Re Test</asp:Label>&nbsp;<span
                                                                class="help-button ace-popover" data-trigger="hover" data-placement="right" data-content="Select 'Yes' if you are scheduling a Test for a previously conducted test"
                                                                title="Conduct Test">?</span>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlReTest_Add" Width="215px" data-placeholder="Select Option"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlReTest_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label12" CssClass="red">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestName_Add" Width="215px" data-placeholder="Select Test Name"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTestName_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label7" CssClass="red">Test Category</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtTestCategory_Add" ToolTip="Test Category" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                            <asp:Label runat="server" ID="lblTestCategoryId" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label8" CssClass="red">Test Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtTestType_Add" ToolTip="Test Type" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                            <asp:Label runat="server" ID="lblTestTypeId" Visible="false"></asp:Label>
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
                                                            <asp:Label runat="server" ID="Label32" CssClass="red">Subject(s)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtSubject_Add" ToolTip="Subject(s)" type="text"
                                                                Width="205px" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td colspan="2" class="span8" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%"
                                                    id="tbHideChapter" runat="server">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 19.75%;">
                                                            <asp:Label runat="server" ID="Label31" CssClass="red">Chapters</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 80.25%;">
                                                            <asp:TextBox runat="server" ID="txtChapters_Add" ToolTip="Chapters" type="text" Width="350px"
                                                                MaxLength="200" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none; width: 128%;" class="table-hover">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label11">Remarks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRemarks_Add" ToolTip="Remarks" type="text" Width="205px"
                                                                MaxLength="200" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label64" runat="server">Teacher Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <%--<asp:DropDownList ID="ddlTeacherName_Add" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                data-placeholder="Select Teacher Name" Width="215px" />--%>

                                                            <asp:ListBox runat="server" ID="lstaddteacher" data-placeholder="Select Teacher(s)" Width="215px"
                                                                CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>


                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13" CssClass="red">Negative Marking</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtNegativeMark_Add" ToolTip="Negative Marking" type="text"
                                                                Width="205px" ReadOnly="True" />
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
                                                            <asp:Label runat="server" class="red" ID="Label9">Maximum Marks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtMaxMarks_Add" ToolTip="Maximum Marks" type="text"
                                                                Width="130px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtMaxMarks_Add" ErrorMessage="Maximum Marks can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Only Numbers are allowed in Maximum Marks !!"
                                                                ValidationExpression="([0-9]|[.])*" ControlToValidate="txtMaxMarks_Add" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-calendar"></i>
                                                            <asp:Label runat="server" ID="Label1" class="red">  Test Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">

                                                            <span>
                                                                <%--<input   id="txtTestDate_Add" runat="server"
                                                                      type="text"  style="width:205px" data-date-format="dd MM yyyy" />--%>
                                                                <%--<input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    id="Text1" placeholder="Date Search" data-placement="bottom"
                                                                    style="width: 205px" data-original-title="Date Range" />--%>
                                                                <input readonly="readonly" class="span10 date-picker" id="id_date_range_picker" runat="server"
                                                                    style="width: 205px" type="text" data-date-format="dd M yyyy" />
                                                            </span>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-time"></i>
                                                            <asp:Label runat="server" ID="Label2" class="red">Test Time</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <span>
                                                                <asp:TextBox ID="txtfromtime" Width="55px" runat="server" />
                                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="false"
                                                                    Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtfromtime" />
                                                                <asp:TextBox ID="txttotime" Width="55px" runat="server" />
                                                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="false"
                                                                    Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txttotime" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="UcValidate"
                                                                    ControlToValidate="txtfromtime" ErrorMessage="Test Start time can't be blank !!">*</asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="UcValidate"
                                                                    ControlToValidate="txttotime" ErrorMessage="Test End time can't be blank !!">*</asp:RequiredFieldValidator>
                                                            </span>
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
                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="BtnSave_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        <%-- <button id="BtnAttendanceMessage" runat="server" class="btn btn-small btn-success radius-4"
                                                                    data-rel="tooltip" data-placement="left" title="SMS"
                                                                    onserverclick="btnMesage_ManualSending_ServerClick" visible ="false">
                                                                    <i class="icon-envelope-alt"></i>
                                                                </button>--%>
                        <button id="BtnAttendanceMessage" runat="server" class="btn btn-app btn-primary btn-mini radius-4"
                            data-rel="tooltip" data-placement="left" title="SMS" onserverclick="btnMesage_ManualSending_ServerClick"
                            visible="false">
                            SMS
                        </button>
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnAddPaperChecker" runat="server"
                            Text="Add Paper Checker" OnClick="btnAddPaperChecker_Click" Width="170px" Visible="true" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivEditPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">Edit Test Schedule
                    </h5>
                    <asp:Label runat="server" ID="lblDivisionCode" Visible="False"></asp:Label>
                    <asp:Label runat="server" ID="lblTestPKey_Edit" Visible="False"></asp:Label>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelEdit" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label36">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblDivision_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label37">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblAcadYear_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label38">Center</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblCentre_Edit" class="blue"></asp:Label>
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
                                                            <asp:Label runat="server" ID="Label39">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblStandard_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label40">Batch</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblBatch_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label41">Conduct No</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblConductNo_Edit" class="blue"></asp:Label>
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
                                                            <asp:Label runat="server" ID="Label42">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblTestName_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label43">Test Category</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblTestCategory_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label44">Test Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblTestType_Edit" class="blue"></asp:Label>
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
                                                            <asp:Label runat="server" ID="Label45">Subject(s)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" ID="lblSubject_Edit" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 19.25%;">
                                                            <asp:Label runat="server" ID="Label47">Remarks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 80.75%;">
                                                            <asp:TextBox runat="server" ID="lblRemarks_Edit" ToolTip="Remarks" type="text" Width="70%" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label65" runat="server">Teacher Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <%--<asp:DropDownList ID="ddlTeacherName_Edit" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                data-placeholder="Select Teacher Name" Width="205px" />--%>

                                                            
                                                            <asp:ListBox runat="server" ID="lsteditteacher" data-placeholder="Select Teacher(s)" Width="215px"
                                                                CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>


                                                        
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
                                                            <asp:Label runat="server" class="red" ID="Label49">Maximum Marks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtMaxMarks_Edit" ToolTip="Maximum Marks" type="text"
                                                                Width="130px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="UcValidateEdit"
                                                                ControlToValidate="txtMaxMarks_Edit" ErrorMessage="Maximum Marks can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only Numbers are allowed in Maximum Marks !!"
                                                                ValidationExpression="([0-9]|[.])*" ControlToValidate="txtMaxMarks_Edit" ValidationGroup="UcValidateEdit">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-calendar"></i>
                                                            <asp:Label runat="server" ID="Label50" class="red">  Test Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span10 date-picker" id="txtTestDate_Edit" runat="server"
                                                                        style="width: 205px" type="text" data-date-format="dd M yyyy" />
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-time"></i>
                                                            <asp:Label runat="server" ID="Label51" class="red">Test Time</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <span>
                                                                <asp:TextBox ID="txtFromTime_Edit" Width="55px" runat="server" />
                                                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptAMPM="false"
                                                                    Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtFromTime_Edit" />
                                                                <asp:TextBox ID="txtToTime_Edit" Width="55px" runat="server" />
                                                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptAMPM="false"
                                                                    Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtToTime_Edit" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="UcValidateEdit"
                                                                    ControlToValidate="txtFromtime_Edit" ErrorMessage="Test Start time can't be blank !!">*</asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="UcValidateEdit"
                                                                    ControlToValidate="txtToTime_Edit" ErrorMessage="Test End time can't be blank !!">*</asp:RequiredFieldValidator>
                                                            </span>
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
                        <asp:Label runat="server" ID="Label52" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnSave_Edit" runat="server"
                            Text="Save" ValidationGroup="UcValidateEdit" OnClick="btnSave_Edit_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnClose_Edit"
                            Visible="true" runat="server" Text="Close" OnClick="btnClose_Edit_Click" />
                        <%--<button id="btnMesage_ManualSending_Edit" runat="server" class="btn btn-small btn-success radius-4"
                                                                    data-rel="tooltip" data-placement="left" title="SMS"
                                                                    onserverclick="btnMesage_ManualSending_Edit_Click" visible ="false">
                                                                    <i class="icon-envelope-alt"></i>
                                                                </button>--%>
                        <button id="btnMesage_ManualSending_Edit" runat="server" class="btn btn-app btn-primary btn-mini radius-4"
                            data-rel="tooltip" data-placement="left" title="SMS" onserverclick="btnMesage_ManualSending_Edit_Click"
                            visible="false">
                            SMS
                        </button>
                        <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidateEdit" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 20% !important; display: none;"
        role="dialog" aria-labelledby="myModalLabel" aria-hidden="true ">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Remove Test Schedule
                        <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                            Text="" />
                    </h4>
                </div>
                <div class="modal-body" style="height: 250px">
                    <!--Controls Area -->
                    <table cellpadding="0" style="border-style: none;" width="100%">
                        <tr>
                            <td style="border-style: none; text-align: left; width: 40%;">
                                <asp:Label runat="server" ID="Label48">Reason for Test Removal</asp:Label>
                            </td>
                            <td style="border-style: none; text-align: left; width: 60%;">
                                <%--  <asp:TextBox runat="server" ID="txtRemoveReason" ToolTip="Reason for Removing Test"
                                    type="text" Width="130px" />--%>
                                <asp:DropDownList runat="server" ID="ddlLect_Test_Reason" ToolTip="Select Reason"
                                    data-placeholder="Select Reason" CssClass="chzn-select" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete" ToolTip="Yes"
                        runat="server" Text="Yes" OnClick="btnDelete_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnCancel" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="DivPendingApproval" style="left: 50% !important; top: 20% !important; display: none;"
        role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="Label53" Text="This Test is already cancelled .." />
                    </h4>
                    <br />
                    <asp:Label runat="server" Font-Bold="false" ID="Label54" Text="It's waiting for admin approval status" />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnClose" ToolTip="Close" runat="server" Text="Close" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>
