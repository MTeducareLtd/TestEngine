<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Get_Test_Details_LMS.aspx.cs" Inherits="Get_Test_Details_LMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                    Test<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd" />
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
                        </tr>
                    </table>
                </div>
            </div>
            <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                runat="server" Width="100%">
                <HeaderTemplate>
               
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="TestAssignStartDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestAssignStartDate")%>' />
                    <asp:Label ID="TestAssignEndDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestAssignEndDate")%>' />
                    <asp:Label ID="TestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestName")%>' />
                    <asp:Label ID="TestId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestId")%>' />
                    <asp:Label ID="TotalQuestion" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalQuestion")%>' />
                    <asp:Label ID="Score" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Score")%>' />
                    <asp:Label ID="OutOf" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OutOf")%>' />
                    <asp:Label ID="CenterCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CenterCode")%>' />
                    <asp:Label ID="BatchCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchCode")%>' />
                    <asp:Label ID="ProductCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' />
                    <asp:Label ID="SkipQuestionCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SkipQuestionCount")%>' />
                    <asp:Label ID="RightAnswerCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RightAnswerCount")%>' />
                    <asp:Label ID="InCorrectAnswerCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InCorrectAnswerCount")%>' />
                    <asp:Label ID="TestCompletedDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestCompletedDate")%>' />
                    <asp:Label ID="CenterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CenterName")%>' />
                    <asp:Label ID="BatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                    <asp:Label ID="ProductName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductName")%>' />
                    <asp:Label ID="ExamMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ExamMode")%>' />
                    <asp:Label ID="ExamType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ExamType")%>' />
                    <asp:Label ID="Attendance" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendance")%>' />
                    <asp:Label ID="Subjects" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subjects")%>' />
                    <asp:Label ID="Syllabus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Syllabus")%>' />
                    <asp:Label ID="Correct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Correct")%>' />
                    <asp:Label ID="InCorrect" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InCorrect")%>' />
                    <asp:Label ID="UnAnswered" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UnAnswered")%>' />
                    <asp:Label ID="OverAllRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OverAllRank")%>' />
                    <asp:Label ID="CenterRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CenterRank")%>' />
                    </td>
                </ItemTemplate>
            </asp:DataList>
        </div>
</asp:Content>
