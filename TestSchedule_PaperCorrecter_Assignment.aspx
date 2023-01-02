<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="TestSchedule_PaperCorrecter_Assignment.aspx.cs" Inherits="TestSchedule_PaperCorrecter_Assignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                    Update Test Schedule and Assign Paper Corrector<span class="divider"></span></h5>
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
                                <td class="span6">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span4">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
                                    &nbsp;
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
                        <td class="span4" style="text-align: left">
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="up" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                            <HeaderTemplate>
                                <b>Test Name</b> </th>
                                <th style="width: 3%; text-align: center">
                                    Conduct No
                                </th>
                                <th align="left" style="width: 10%">
                                    Batch
                                </th>
                                <th align="left" style="width: 5%">
                                    Test Type
                                </th>
                                <th align="left" style="width: 15%">
                                    Subjects
                                </th>
                                <th style="width: 5%; text-align: center">
                                    Max Marks
                                </th>
                                <th style="width: 10%; text-align: center">
                                    Test Date
                                </th>
                                <th style="width: 15%; text-align: center">
                                    Test Time
                                </th>
                                <th style="width: 12%; text-align: center">
                                    Paper Checker
                                </th>
                                <th style="width: 6%; text-align: center">
                                    Paper Checker Slab
                                </th>
                                <th style="width: 6%; text-align: center">
                                    Action
                                </th>
                                <th style="width: 6%; text-align: center">
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
                                    <asp:Label ID="lblTestTimeStr" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                                    <asp:Label ID="lblDLFromTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"FromTimeStr")%>' />
                                    <asp:Label ID="lblDLToTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ToTimeStr")%>' />
                                    <asp:TextBox ID="txtfromtime" Width="55px" runat="server" Visible="false" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="false"
                                        Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtfromtime" />
                                    <asp:TextBox ID="txttotime" Width="55px" runat="server" Visible="false" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="false"
                                        Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txttotime" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="UcValidate"
                                        ControlToValidate="txtfromtime" ErrorMessage="Test Start time can't be blank !!">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="UcValidate"
                                        ControlToValidate="txttotime" ErrorMessage="Test End time can't be blank !!">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblpartnername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerName")%>' />
                                    <asp:Label ID="lblpartnercode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' />
                                    <asp:DropDownList runat="server" ID="ddlpartner" ToolTip="Paper Checker" data-placeholder="Select Paper Checker "
                                        CssClass="chzn-select" Visible="false" />
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblpcslab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Slab_Name")%>' />
                                    <asp:Label ID="lblpcslabid" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PCSlab_Id")%>' />
                                    <asp:Label ID="lblbagpkey" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"TestBagPKey")%>' />
                                    <asp:DropDownList runat="server" ID="ddlPCSlab" Width="75px" ToolTip="Paper Checker Slab"
                                        data-placeholder="Select Paper Checker Slab " CssClass="chzn-select" Visible="false" />
                                </td>
                                <td style="text-align: center;">
                                    <%--<asp:LinkButton ID="lnkEditInfo" ToolTip="Edit Test" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="Edit" />
                            <asp:LinkButton ID="LinkButton1" ToolTip="Delete Test" CommandName="Delete" class="btn-small btn-inverse icon-trash"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server" />--%>
                                    <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>'
                                        CommandName="Edit" Height="25px" />
                                    <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                        runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 30px; text-align: center; vertical-align: middle;">
                                    <a id="lbl_DLError" runat="server" title="Error" data-rel="tooltip" href="#">
                                        <asp:Panel ID="icon_Error" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                    
                </asp:UpdatePanel>
                <asp:DataList ID="dlgridexport" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th style="width: 5%; text-align: center">
                            Conduct No
                        </th>
                        <th align="left" style="width: 10%">
                            Batch
                        </th>
                        <th align="left" style="width: 10%">
                            Test Type
                        </th>
                        <th align="left" style="width: 15%">
                            Subjects
                        </th>
                        <th style="width: 5%; text-align: center">
                            Max Marks
                        </th>
                        <th style="width: 10%; text-align: center">
                            Test Date
                        </th>
                        <th style="width: 10%; text-align: center">
                            Test Time
                        </th>
                        <th style="width: 12%; text-align: center">
                            Paper Checker
                        </th>
                        <th style="width: 12%; text-align: center">
                        Paper Checker Slab
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
                        <td style="text-align: center">
                        </td>
                        <td style="text-align: center">
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>
