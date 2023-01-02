<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Master_QPSet.aspx.cs" Inherits="Master_QPSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function openModalDivOverride() {
            $('#DivOverrid').modal({
                backdrop: 'static'
            })

            $('#DivOverrid').modal('show');
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
                <h5 class="smaller">QP Sets<span class="divider"></span></h5>
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
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
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
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19">Test Type</asp:Label>
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
                                                                <asp:TextBox runat="server" ID="txtTestName" ToolTip="Test Name" type="text" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                <td class="span10">Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" Height="25px" ToolTip="Export"
                                        class="btn-small btn-danger icon-2x icon-download-alt" runat="server" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th align="left" style="width: 10%">Course
                        </th>
                        <th align="left" style="width: 10%">Test Mode
                        </th>
                        <th align="left" style="width: 10%">Test Category
                        </th>
                        <th align="left" style="width: 10%">Test Type
                        </th>
                        <th style="width: 10%; text-align: center;">Max Marks
                        </th>
                        <th align="left" style="width: 20%">Subjects
                        </th>
                        <th style="width: 10%; text-align: center;">Authorised
                        </th>
                        <th style="width: 50px; text-align: center;">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestMode_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td style="width: 10%; text-align: center;">
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AuthoriseFlag")%>' />
                        </td>
                        <td style="width: 50px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Manage" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="Manage" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" HorizontalAlign="Left"
                    CssClass="table table-striped table-bordered table-hover" Width="100%">
                    <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th align="left" style="width: 10%">Course
                        </th>
                        <th align="left" style="width: 10%">Test Mode
                        </th>
                        <th align="left" style="width: 10%">Test Category
                        </th>
                        <th align="left" style="width: 10%">Test Type
                        </th>
                        <th align="left" style="width: 10%">Max Marks
                        </th>
                        <th align="left" style="width: 20%">Subjects
                        </th>
                        <th align="left" style="width: 10%">
                        Authorised
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestMode_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AuthoriseFlag")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">Add QP Set
                    </h5>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <%--<asp:UpdatePanel runat="server" ID="updGridQuestion">
                                                    <ContentTemplate>--%>
                            <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                <tr>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label3">Division</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblDivision_Add"></asp:Label>
                                                    <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" class="blue"></asp:Label>
                                                    <asp:Label runat="server" ID="lblDivCode" Visible="false" class="blue"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label4">Academic Year</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblAcadYear_Add"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label5">Course</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblStandard_Add"></asp:Label>
                                                    <asp:Label runat="server" class="blue" ID="lblStandardCode" Visible="false"></asp:Label>
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
                                                    <asp:Label runat="server" ID="Label6">Test Category</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblTestCategory_Add"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label7">Test Type</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblTestType_Add"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label8">Test Name</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblTestName_Add"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="span4" style="text-align: left; vertical-align: middle;">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%; vertical-align: middle;">
                                                    <asp:Label runat="server" ID="Label1">Subject(s)</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" class="blue" ID="lblSubject_Add"></asp:Label>
                                                    <asp:Label runat="server" class="blue" ID="lbltestneagtivemarkingflag" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%; vertical-align: middle;">
                                                    <asp:Label runat="server" ID="Label9">QP Set No</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:DropDownList runat="server" ID="ddlQPSetNo" Width="72px" CssClass="chzn-select"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlQPSetNo_SelectedIndexChanged" />
                                                    <asp:Button class="btn btn-app btn-info btn-mini radius-4" ID="btnShowQPSetDetails"
                                                        runat="server" Text="Show" OnClick="btnShowQPSetDetails_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left; vertical-align: middle;">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%" runat="server" id="tblUploadFile">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlDiffLevel_Hidden" Width="72px" Visible="false" />
                                                    <asp:DropDownList runat="server" ID="ddlSubject_Hidden" Width="72px" Visible="false" />
                                                    <asp:UpdateProgress ID="updProgress" DynamicLayout="false" runat="server">
                                                        <ProgressTemplate>
                                                            <img alt="progress" src="Images/loading.gif" />
                                                            Processing...
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:Label runat="server" ID="Label2">Select File :</asp:Label>
                                                    <span class="help-button ace-popover" runat="server" id="SMSHelpFlag" data-trigger="hover"
                                                        data-placement="left" data-content="Select a CSV file that contain QP Result using browse button."
                                                        title="Help">?</span>
                                                    <asp:FileUpload ID="FFLExcel" runat="server" Width="172px" />
                                                    <button id="BtnUploadExcel" class="btn btn-info btn-purple" data-rel="tooltip" data-placement="left"
                                                        title="Upload" runat="server" onserverclick="BtnUploadExcel_ServerClick">
                                                        <i class="icon-cloud-upload"></i>
                                                    </button>
                                                    &nbsp;&nbsp;
                                                    <button id="BtnDownloadExcel" class="btn btn-info btn-pink" data-rel="tooltip" runat="server"
                                                        data-placement="right" title="Download" onserverclick="BtnDownloadExcel_ServerClick">
                                                        <i class="icon-cloud-download"></i>
                                                    </button>
                                                    <asp:Label ID="lblfilepath" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="span4" style="text-align: left; vertical-align: middle;">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%; vertical-align: middle;">
                                                    <asp:Label runat="server" ID="lblassesmenttestcpde">Assesment Test Code</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:TextBox runat="server" ID="txtassesmenttestcode" ToolTip="Assesment Engine Test Code"
                                                        type="text" Width="150" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left" id="tdonline" runat="server" visible="false">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: center; width: 100%;">
                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnsavveonlinetest"
                                                        runat="server" Text="Process Online Test Details" Width="215px" OnClick="btnsavveonlinetest_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left; vertical-align: middle;"></td>
                                </tr>
                                <tr runat="server" id="QuestionGrid">
                                    <td colspan="3" class="span12" style="text-align: left">
                                        <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                                        <asp:UpdatePanel ID="updatepanneladd" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div style="overflow-x: scroll; overflow-y: scroll; width: 1250px; height: 300px">
                                                    <asp:DataList ID="dlQuestion" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" OnItemDataBound="dlQuestion_ItemDataBound" OnItemCommand="dlQuestion_ItemCommand">
                                                        <HeaderTemplate>
                                                            <center>
                                                            </center>
                                                            </th>
                                                            <th style="width: 5%; text-align: left;">Que No
                                                            </th>
                                                            <th style="width: 5%; text-align: left;">Que Type
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Answer Key
                                                            </th>
                                                            <th style="width: 80px; text-align: center;">Difficulty Level
                                                            </th>
                                                            <th style="width: 8%; text-align: center;">Correct Marks
                                                            </th>
                                                            <th style="width: 8%; text-align: center;">Wrong Marks (-ve)
                                                            </th>
                                                            <th style="width: 5%; text-align: left;">Subject
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Ref.Course
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Ref.Subject
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Chapter
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Topic
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">SubTopic
                                                            </th>
                                                            <th style="width: 5%; text-align: left;">Module
                                                            </th>

                                                            <th style="width: 5%; text-align: left;">Question Rule
                                                            </th>
                                                            <th style="width: 8%; text-align: center;"></th>
                                                            <th style="width: 10%; text-align: center;">
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <center>
                                                                <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Question_No")%>' runat="server"
                                                                    Visible="True" CommandName="Edit" Height="25px" />
                                                            </center>
                                                            </td>
                                                            <td style="width: 5%; text-align: left;">
                                                                <asp:Label ID="lblQueNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Question_No")%>' />
                                                            </td>
                                                            <td style="width: 5%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLQueType" runat="server" Width="5%" CssClass="chzn-select"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblquetypeid" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Question_Type_Id")%>' />
                                                                <asp:Label ID="lblquetypename" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Question_Type_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:TextBox ID="txtDLAnswerKey" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Ans_Key")%>'
                                                                    Width="75%" />
                                                                <asp:Label ID="lblDLAnswerKey_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Ans_Key")%>' />
                                                            </td>
                                                            <td style="width: 80px; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLDiffLevel" runat="server" Width="70px" Visible="false"
                                                                    CssClass="chzn-select" />
                                                                <asp:Label ID="lblDLDiffLevel" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"DiffLevel_Id")%>' />
                                                                <asp:Label ID="lblDLDiffLevel_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"DiffLevel_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: center;">
                                                                <asp:TextBox ID="txtDLCorrectMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Marks")%>'
                                                                    Width="75%" onkeypress="return NumberOnly()" Visible="false" />
                                                                <asp:Label ID="lblDLCorrectMarks_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Marks")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: center;">
                                                                <asp:TextBox ID="txtDLWrongMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Wrong_Marks")%>'
                                                                    Width="75%" onkeypress="return NumberOnly()" Visible="false" />
                                                                <asp:Label ID="lblDLWrongMarks" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Wrong_Marks")%>' />
                                                            </td>
                                                            <td style="width: 5%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLSubject" runat="server" Width="5%" CssClass="chzn-select"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLSubject_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Code")%>' />
                                                                <asp:Label ID="lblDLSubject_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLRefCouse" runat="server" Width="8%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlDLRefCouse_SelectedIndexChanged" AutoPostBack="True"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLRefCourse_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"RefCourse_Code")%>' />
                                                                <asp:Label ID="lblDLRefCourse_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"RefCourse_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLRefSubject" runat="server" Width="8%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlRefSubject_SelectedIndexChanged" AutoPostBack="True"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLRefSubject_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"RefSubject_Code")%>' />
                                                                <asp:Label ID="lblDLRefSubject_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"RefSubject_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLChapter" runat="server" Width="8%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlChapter_SelectedIndexChanged" AutoPostBack="true"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLChapter_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>' />
                                                                <asp:Label ID="lblDLChapter_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLTopic" runat="server" Width="8%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlDLTopic_SelectedIndexChanged" AutoPostBack="true"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLTopic_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_Code")%>' />
                                                                <asp:Label ID="lblDLTopic_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLSubTopic" runat="server" Width="8%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlDLSubTopic_SelectedIndexChanged" AutoPostBack="true"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLSubTopic_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SubTopic_Code")%>' />
                                                                <asp:Label ID="lblDLSubTopic_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"SubTopic_Name")%>' />
                                                            </td>
                                                            <td style="width: 5%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLModul" runat="server" Width="5%" CssClass="chzn-select"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLModule_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Module_Code")%>' />
                                                                <asp:Label ID="lblDLModule_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Module_Name")%>' />
                                                            </td>

                                                            <td style="width: 5%; text-align: left;">
                                                                <asp:DropDownList ID="ddlquestionrule" runat="server" Width="5%" CssClass="chzn-select"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblruleid" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Rule_Id")%>' />
                                                                <asp:Label ID="lblrulename" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Rule_Name")%>' />
                                                            </td>

                                                            <td style="width: 8%; text-align: center;">
                                                                <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                                                    runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Question_No")%>'
                                                                    Visible="False" />
                                                            </td>

                                                            <td style="width: 10%; text-align: center;">
                                                                <asp:Label ID="lblResult" runat="server" Text="" />
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%" Visible="false">
                                                        <HeaderTemplate>
                                                            <center>
                                                            </center>
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Que No
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Que Type
                                                            </th>
                                                            <th style="width: 10%; text-align: left;">Answer Key
                                                            </th>
                                                            <th style="width: 120px; text-align: center;">Difficulty Level
                                                            </th>
                                                            <th style="width: 10%; text-align: center;">Correct Marks
                                                            </th>
                                                            <th style="width: 10%; text-align: center;">Wrong Marks (-ve)
                                                            </th>
                                                            <th style="width: 13%; text-align: left;">Subject
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Ref.Course
                                                            </th>
                                                            <th style="width: 8%; text-align: left;">Ref.Subject
                                                            </th>
                                                            <th style="width: 13%; text-align: left;">Chapter
                                                            </th>
                                                            <th style="width: 13%; text-align: left;">Topic
                                                            </th>
                                                            <th style="width: 13%; text-align: left;">SubTopic
                                                            </th>
                                                            <th style="width: 13%; text-align: left;">Module
                                                            </th>
                                                            <th style="width: 13%; text-align: left;">Question Rule
                                                            </th>
                                                            <th style="width: 8%; text-align: center;">
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <center>
                                                                <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Question_No")%>' runat="server"
                                                                    Visible="True" CommandName="Edit" Height="25px" />
                                                            </center>
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:Label ID="lblQueNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Question_No")%>' />
                                                            </td>
                                                            <td style="width: 13%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLQueType" runat="server" Width="10%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="True"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblquetypeid" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Question_Type_Id")%>' />
                                                                <asp:Label ID="lblquetypename" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Question_Type_Name")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left;">
                                                                <asp:TextBox ID="txtDLAnswerKey" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Ans_Key")%>'
                                                                    Width="85%" />
                                                                <asp:Label ID="lblDLAnswerKey_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Ans_Key")%>' />
                                                            </td>
                                                            <td style="width: 120px; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLDiffLevel" runat="server" Width="100px" Visible="false"
                                                                    CssClass="chzn-select" />
                                                                <asp:Label ID="lblDLDiffLevel" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"DiffLevel_Id")%>' />
                                                                <asp:Label ID="lblDLDiffLevel_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"DiffLevel_Name")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: center;">
                                                                <asp:TextBox ID="txtDLCorrectMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Marks")%>'
                                                                    Width="85%" onkeypress="return NumberOnly()" Visible="false" />
                                                                <asp:Label ID="lblDLCorrectMarks_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Correct_Marks")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: center;">
                                                                <asp:TextBox ID="txtDLWrongMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Wrong_Marks")%>'
                                                                    Width="85%" onkeypress="return NumberOnly()" Visible="false" />
                                                                <asp:Label ID="lblDLWrongMarks" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Wrong_Marks")%>' />
                                                            </td>
                                                            <td style="width: 13%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLSubject" runat="server" Width="10%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="True"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLSubject_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Code")%>' />
                                                                <asp:Label ID="lblDLSubject_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLRefCouse" runat="server" Width="8%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlDLRefCouse_SelectedIndexChanged" AutoPostBack="True"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLRefCourse_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"RefCourse_Code")%>' />
                                                                <asp:Label ID="lblDLRefCourse_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"RefCourse_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLRefSubject" runat="server" Width="8%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlRefSubject_SelectedIndexChanged" AutoPostBack="True"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLRefSubject_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"RefSubject_Code")%>' />
                                                                <asp:Label ID="lblDLRefSubject_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"RefSubject_Name")%>' />
                                                            </td>
                                                            <td style="width: 13%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLChapter" runat="server" Width="10%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlChapter_SelectedIndexChanged" AutoPostBack="true"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLChapter_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>' />
                                                                <asp:Label ID="lblDLChapter_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                                            </td>
                                                            <td style="width: 13%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLTopic" runat="server" Width="10%" CssClass="chzn-select"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLTopic_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_Code")%>' />
                                                                <asp:Label ID="lblDLTopic_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_Name")%>' />
                                                            </td>
                                                            <td style="width: 13%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLSubTopic" runat="server" Width="8%" CssClass="chzn-select"
                                                                    OnSelectedIndexChanged="ddlDLSubTopic_SelectedIndexChanged" AutoPostBack="true"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLSubTopic_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SubTopic_Code")%>' />
                                                                <asp:Label ID="lblDLSubTopic_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"SubTopic_Name")%>' />
                                                            </td>
                                                            <td style="width: 13%; text-align: left;">
                                                                <asp:DropDownList ID="ddlDLModul" runat="server" Width="8%" CssClass="chzn-select"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblDLModule_Code" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Module_Code")%>' />
                                                                <asp:Label ID="lblDLModule_Name" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Module_Name")%>' />
                                                            </td>

                                                            <td style="width: 13%; text-align: left;">
                                                                <asp:DropDownList ID="ddlquestionrule" runat="server" Width="5%" CssClass="chzn-select"
                                                                    Visible="false" />
                                                                <asp:Label ID="lblruleid" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Rule_Id")%>' />
                                                                <asp:Label ID="lblrulename" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Rule_Name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: center;">
                                                                <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                                                    runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Question_No")%>'
                                                                    Visible="False" />
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                            OnClick="BtnSave_Click" Text="Save" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important; display: none;"
        role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Delete Test
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
    <div class="modal fade" id="DivOverrid" style="left: 50% !important; top: 20% !important; display: none;"
        role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">QP Set Warning
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    <table cellpadding="0" style="border-style: none;" width="100%">
                        <tr>
                            <td style="border-style: none; text-align: left; width: 40%;">
                                <asp:Label runat="server" ID="Label48">Are you sure want to override existing QP</asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="Label11" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="Button1" ToolTip="Yes"
                        runat="server" Text="Yes" OnClick="btnDivOverYes_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="Button2" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>
