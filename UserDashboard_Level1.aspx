<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="UserDashboard_Level1.aspx.cs" Inherits="UserDashboard_Level1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    <asp:Label ID="lblHead_PageName" runat="server" Text="" /><span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4" runat="server" ID="BtnClose" OnClick="BtnClose_Click"
                Text="Close" />
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
            <div id="DivResult_TodaysTest" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount_TodaysTest" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport_TodaysTest" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_TodaysTest_Click"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGrid_TodaysTest" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Division</b> </th>
                        <th style="width: 8%; text-align: left">
                            Acad Year
                        </th>
                        <th style="width: 5%; text-align: left">
                            Course
                        </th>
                        <th style="width: 8%; text-align: left">
                            Center
                        </th>
                        <th style="width: 5%; text-align: left">
                            Batch
                        </th>
                        <th style="width: 8%; text-align: left">
                            Test Name
                        </th>
                        <th style="width: 5%; text-align: center">
                            Conduct No
                        </th>
                        <th align="left" style="width: 8%">
                            Test Category
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
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year")%>' />
                        </td>
                        <td style="width: 5%; text-align: left">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                        </td>
                        <td style="width: 5%; text-align: left">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style="width: 5%; text-align: center">
                            <asp:Label ID="lblDLConductNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLSubjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLMaxMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivResult_AttendAuthorise" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lblTotalCount_AttendAuthorise" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport_AttendAuthorise" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_AttendAuthorise_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGrid_AttendAuthorise" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Division</b> </th>
                        <th style="width: 8%; text-align: left">
                            Acad Year
                        </th>
                        <th style="width: 5%; text-align: left">
                            Course
                        </th>
                        <th style="width: 8%; text-align: left">
                            Center
                        </th>
                        <th style="width: 5%; text-align: left">
                            Batch
                        </th>
                        <th style="width: 8%; text-align: left">
                            Test Name
                        </th>
                        <th style="width: 5%; text-align: center">
                            Conduct No
                        </th>
                        <th align="left" style="width: 8%">
                            Test Category
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
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year")%>' />
                        </td>
                        <td style="width: 5%; text-align: left">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                        </td>
                        <td style="width: 5%; text-align: left">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style="width: 5%; text-align: center">
                            <asp:Label ID="lblDLConductNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLSubjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLMaxMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivResult_MarksAuthorise" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lblTotalCount_MarksAuthorise" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport_MarksAuthorise" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_MarksAuthorise_Click"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGrid_MarksAuthorise" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Division</b> </th>
                        <th style="width: 8%; text-align: left">
                            Acad Year
                        </th>
                        <th style="width: 5%; text-align: left">
                            Course
                        </th>
                        <th style="width: 8%; text-align: left">
                            Center
                        </th>
                        <th style="width: 5%; text-align: left">
                            Batch
                        </th>
                        <th style="width: 8%; text-align: left">
                            Test Name
                        </th>
                        <th style="width: 5%; text-align: center">
                            Conduct No
                        </th>
                        <th align="left" style="width: 8%">
                            Test Category
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
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year")%>' />
                        </td>
                        <td style="width: 5%; text-align: left">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                        </td>
                        <td style="width: 5%; text-align: left">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style="width: 5%; text-align: center">
                            <asp:Label ID="lblDLConductNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Conduct_No")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCategory_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLSubjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLMaxMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxMarks")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestTimeStr")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>

            <div id="DivResult_TestCancellation" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lblTestCancellation" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="lnkTestCancellation" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" onclick="lnkTestCancellation_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <asp:DataList ID="dlTestCancellation" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b> Division</b> </th>
                        <th style="width: 8%; text-align: left">
                            Acad Year
                        </th>
                        <th style="width: 5%; text-align: left">
                            Course
                        </th>
                        <th style="width: 10%; text-align: center">
                            Center Name
                        </th>
                        
                        
                        <th>Test Name </th>
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
                        
                    </HeaderTemplate>
                    <ItemTemplate>

                    <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Name")%>' />
                        </td>
                        <td style="width: 8%; text-align: left">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year")%>' />
                        </td>
                        <td style="width: 5%; text-align: left">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Standard_Name")%>' />
                        </td>

                        <td>
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
                            
                       
                        
                            
                    </ItemTemplate>
                </asp:DataList>
        </div>

            <div id="DivResult_ReTest" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lblReTestCount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="lnkReTest" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" onclick="lnkReTest_Click"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <asp:DataList ID="dlReTest" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b> Division</b> </th>
                        <th style="width: 8%; text-align: left">
                            Acad Year
                        </th>                       
                        <th style="width: 10%; text-align: center">
                            Center Name
                        </th>
                         <th style="width: 10%; text-align: left">
                            Course
                        </th>                        
                        <th>Test Name </th>
                        <th style="width: 10%; text-align: center">
                            Actual Test Date
                        </th>
                        <th align="left" style="width: 10%">
                            Batch
                        </th>
                        <th align="left" style="width: 10%">
                            Test Type
                        </th>
                        <th align="left" style="width: 10%;text-align: center">
                            ReTest Date
                        </th>
                        <th style="width: 20%; text-align: center">
                            Student Name
                        </th>                        
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCourseName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                        </td>
                        <td style=" text-align: center">
                            <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                        </td>
                        <td style=" text-align: center">
                            <asp:Label ID="lblDLActualTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ActualTestDate")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDLTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType_Name")%>' />
                        </td>
                        <td style=" text-align: center">
                            <asp:Label ID="lblDLReTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ReTestDate")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblDLStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                            
                    </ItemTemplate>
                </asp:DataList>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 20% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Delete Test Schedule
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
