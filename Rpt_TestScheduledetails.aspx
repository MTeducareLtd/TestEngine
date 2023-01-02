<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Rpt_TestScheduledetails.aspx.cs" Inherits="Rpt_TestScheduledetails" %>

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
        .style1
        {
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
                <h5 class="smaller">
                    Test Schedule Report<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" />
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
                            Search Test Schedule
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
                                                                <asp:Label runat="server" ID="Label18">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
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
                                                                <%--<asp:DropDownList runat="server" ID="ddlStandard" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />--%>
                                                                <asp:ListBox runat="server" ID="ddlStandard" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />
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
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label19">Test Category</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlTestCategory" Width="215px" data-placeholder="Select Test Category"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlTestCategory_SelectedIndexChanged" />
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
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlTestType_SelectedIndexChanged" />
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
                                <td class="span6">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span4">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        OnClick="btnExport_Click" Height="25px" />
                                    &nbsp;
                                    <asp:LinkButton runat="server" ID="lnkPrint" ToolTip="Print" class="btn-small btn-warning icon-2x icon-print"
                                        Height="25px" Visible="false" />
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <%--<table cellpadding="3" class="table table-striped table-bordered table-condensed">
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
                        <td class="span4" style="text-align: left">
                        </td>
                    </tr>
                </table>--%>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Date</b> </th>
                        <th align="center" style="width: 11%">
                            Center Name
                        </th>
                        <th align="center" style="width: 11%">
                            Batch
                        </th>
                        <th align="center" style="width: 11%">
                            Test Type
                        </th>
                        <th align="center" style="width: 11%">
                            Test Name
                        </th>
                        <th align="center" style="width: 11%">
                            Subject
                        </th>
                        <th align="center" style="width: 11%">
                            Syllabus
                        </th>
                        <th align="center" style="width: 11%">
                            Marks
                        </th>
                        <th align="center" style="width: 11%">
                        Duration
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        <td style="width: 11%; text-align: left">
                            <asp:Label ID="lblcenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"centername")%>' />
                        </td>
                        <td style="width: 11%; text-align: left">
                            <asp:Label ID="lblDLBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td style="width: 11%; text-align: left">
                            <asp:Label ID="lblDLTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory_Name")%>' />
                        </td>
                        <td style="width: 11%; text-align: left">
                            <asp:Label ID="lblDLSubjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"testname")%>' />
                        </td>
                        <td style="width: 11%; text-align: left">
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="width: 11%; text-align: left">
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CHAPTERNAME")%>' />
                        </td>
                        <td style="width: 11%; text-align: left">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"marks")%>' />
                        </td>
                        <td style="width: 11%; text-align: left">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                            <%--<asp:Label ID="lblDLFromTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"FromTimeStr")%>' />
                            <asp:Label ID="lblDLToTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ToTimeStr")%>' />--%>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <div id="divPrint" runat="server" style="width: 100%; padding-bottom: 20px; padding-left: 5px;
                    padding-top: 5px; padding-right: 5px;" visible="false">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <img alt="MT Educare Ltd" class="msg-photo" src="Images/logo.jpg" />
                            </td>
                            <td align="right">
                                <h3 style="font-family: Arial">
                                    Test Schedule List</h3>
                            </td>
                        </tr>
                    </table>
                    <table style="border-collapse: collapse; border: 1px solid black; font-family: Arial;
                        font-size: 12px; width: 100%;">
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
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Repeater ID="dlGridExport" runat="server">
                    <HeaderTemplate>
                        <table id="simple-table" class="table table-striped table-bordered table-hover  Table4"
                            border="1" style="border-collapse: collapse" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        Date
                                    </th>
                                    <th>
                                        Center Name
                                    </th>
                                    <th>
                                        Batch
                                    </th>
                                    <th>
                                        Test Type
                                    </th>
                                    <th>
                                        Test Name
                                    </th>
                                    <th>
                                        Subject
                                    </th>
                                    <th>
                                        Syllabus
                                    </th>
                                    <th>
                                        Marks
                                    </th>
                                    <th>
                                        Duration
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="odd gradeX">
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                            </td>
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"centername")%>' />
                            </td>
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                            </td>
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory_Name")%>' />
                            </td>
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"testname")%>' />
                            </td>
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                            </td>
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CHAPTERNAME")%>' />
                            </td>
                            </td>
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"marks")%>' />
                            </td>
                            <td style="text-align: left; font-size: 10.0pt; font-family: Calibri;">
                                <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
</asp:Content>
