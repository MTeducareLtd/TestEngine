<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Tran_Test_RemoveApproval.aspx.cs" Inherits="Tran_Test_RemoveApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function openModalRemoveTestAprove() {
        $('#DivRemoveTestAprove').modal({
            backdrop: 'static'
        })

        $('#DivRemoveTestAprove').modal('show');
    }

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
                    Approve Test Removal<span class="divider"></span></h5>
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
        <div class="row-fluid">
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
                                                                    data-placeholder="Select Division" CssClass="chzn-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"/>
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
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged"/>
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
                                                                <%--<asp:DropDownList runat="server" ID="ddlStandard" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" />--%>
                                                                    <asp:ListBox runat="server" ID="ddlStandard" Width="215px" data-placeholder="Select Course"
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
                                                                <asp:Label runat="server" ID="Label30">Test Name</asp:Label>
                                                            </td>
                                                       <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtTestName" ToolTip="Test Name" type="text" Width="205px" />
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
                                                                <%--<asp:DropDownList runat="server" ID="ddlTestCategory" Width="215px" data-placeholder="Select Test Category"
                                                                    CssClass="chzn-select" />--%>
                                                                    <asp:ListBox runat="server" ID="ddlTestCategory" Width="215px" data-placeholder="Select Test Category"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
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
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                      <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29">  Test Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker" id="id_date_range_picker_1"
                                                                   style="width:205px"     placeholder="Date Search" data-placement="bottom" data-original-title="Date Range"/>
                                                            </td>
                                                        </tr>  
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        
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
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper" visible ="false">
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
                                        Height="25px" onclick="btnExport_Click" />
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
                        <th style="width: 10%; text-align: center">
                            Center Name
                        </th>
                        <th style="width: 10%; text-align: center">
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
                        <th style="width: 10%; text-align: center">
                            Max Marks
                        </th>
                        <th style="width: 10%; text-align: center">
                            Request Date
                        </th>
                        <th style="width: 10%; text-align: center">
                            Reson
                        </th>
                        <th style="width: 70px; text-align: center">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblDLCenterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
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
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Request_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Request_Reason")%>' />
                            <asp:Label ID="lblDLFromTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"FromTimeStr")%>' />
                            <asp:Label ID="lblDLToTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ToTimeStr")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Aprove Request" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="Aprove" />
                            
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" CssClass="table table-striped table-bordered table-hover"
                    Width="100%">
                    <HeaderTemplate>
                    <b>Test Name</b> </th>
                        <th style="width: 10%; text-align: center">
                            Center Name
                        </th>
                        <th style="width: 10%; text-align: center">
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
                        <th style="width: 10%; text-align: center">
                            Max Marks
                        </th>
                        <th style="width: 10%; text-align: center">
                            Request Date
                        </th>
                        <th style="width: 10%; text-align: center">
                            Reson
                        </th>
                       
                        <th style="width: 13%; text-align: center">
                        Test Time
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style="width: 10%; text-align: center">
                            <asp:Label ID="lblDLCenterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>

                        <td style="text-align: center">
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Request_Date")%>' />
                        </td>
                         <td style="text-align: center">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Request_Reason")%>' />
                            </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        
    </div>

    <div class="modal fade" id="DivRemoveTestAprove" style="left: 50% !important; top: 20% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Approve Request <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />
                    </h4>
                </div>
                <div class="modal-header">
                    <!--Controls Area -->
                    <table cellpadding="7" style="border-style: none;" width="100%">
                        <tr>
                            <td style="border-style: none; text-align: left; width: 40%;">
                                <asp:Label runat="server" ID="Label1">Select Action</asp:Label>
                            </td>
                            <td style="border-style: none; text-align: left; width: 60%;">
                                <asp:DropDownList runat="server" ID="ddlAction" Width="142px" AutoPostBack="false"
                                 data-placeholder="Select Action" CssClass="chzn-select" />
                            </td>
                        </tr>
                        <tr>

                            <td style="border-style: none; text-align: left; width: 40%;">
                                <asp:Label runat="server" ID="Label48">Reason for Approve Request</asp:Label>
                            </td>
                            <td style="border-style: none; text-align: left; width: 60%;">
                                <asp:TextBox runat="server" ID="txtRemoveReason" ToolTip="Reason for Removing Test" type="text"
                                                                Width="130px" />
                            </td>

                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnReqApr" ToolTip="Yes"
                        runat="server" Text="Yes" OnClick="btnReqApr_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnCancel" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>

