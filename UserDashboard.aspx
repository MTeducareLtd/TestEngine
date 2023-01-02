<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="UserDashboard.aspx.cs" Inherits="UserDashboard" %>

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
                    Dashboard<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search" class="middle">
            <asp:Label ID="lblReportPeriod" runat="server"></asp:Label>
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <asp:Panel ID="pnlTemp" runat="server" Visible="false">
            <div class="space-6">
            </div>
            <asp:UpdatePanel ID="UpdatePanel_CentreDashboard" runat="server">
                <ContentTemplate>
                    <div class="row-fluid span10">
                        &nbsp;&nbsp;
                        <button id="btn_PreviousCentre" runat="server" class="btn btn-mini btn-grey radius-4"
                            data-rel="tooltip" data-placement="top" title="previous centre" onserverclick="btn_PreviousCentre_ServerClick">
                            <i class="icon-chevron-left icon-2x icon-only"></i>
                        </button>
                        <button id="btn_NextCentre" runat="server" class="btn btn-mini btn-grey radius-4"
                            data-rel="tooltip" data-placement="top" title="next centre" onserverclick="btn_NextCentre_ServerClick">
                            <i class="icon-chevron-right icon-2x icon-only"></i>
                        </button>
                        <small>Centre:
                            <asp:Label ID="lblCentreDashboard_CentreName" runat="server"></asp:Label></small>
                        <asp:Label ID="lblCentreDashboard_CentreNumber" runat="server" Visible="false"></asp:Label></small>
                    </div>
                    <div class="space-6">
                    </div>
                    <div class="row-fluid">
                        <div class="span12 infobox-container">
                            <div class="infobox infobox-green">
                                <div class="infobox-icon">
                                    <i class="icon-calendar"></i>
                                </div>
                                <div class="infobox-data">
                                    <span class="infobox-data-number">
                                        <asp:Label ID="lblCentreDashboard_TestCount" runat="server"></asp:Label></span>
                                    <span class="infobox-content">tests scheduled</span>
                                </div>
                                <div class="badge badge-success">
                                    <asp:Label ID="lblCentreDashboard_ReTestCount" runat="server"></asp:Label>
                                    re-tests</div>
                            </div>
                            <div class="infobox infobox-blue">
                                <div class="infobox-icon">
                                    <i class="icon-user"></i>
                                </div>
                                <div class="infobox-data">
                                    <span class="infobox-data-number">
                                        <asp:Label ID="lblCentreDashboard_AttendPending" runat="server"></asp:Label></span>
                                    <span class="infobox-content">pending attendance</span>
                                </div>
                                <div class="badge badge-info">
                                    avg TAT:
                                    <asp:Label ID="lblCentreDashboard_AttendTAT" runat="server"></asp:Label>&nbsp;days</div>
                            </div>
                            <div class="infobox infobox-orange">
                                <div class="infobox-icon">
                                    <i class="icon-book"></i>
                                </div>
                                <div class="infobox-data">
                                    <span class="infobox-data-number">0</span> <span class="infobox-content">pending answersheet</span>
                                </div>
                                <div class="badge badge-warning">
                                    avg TAT: 0 days</div>
                            </div>
                            <div class="infobox infobox-red">
                                <div class="infobox-icon">
                                    <i class="icon-check"></i>
                                </div>
                                <div class="infobox-data">
                                    <span class="infobox-data-number">
                                        <asp:Label ID="lblCentreDashboard_MarkPending" runat="server"></asp:Label></span>
                                    <span class="infobox-content">pending marks</span>
                                </div>
                                <div class="badge badge-important">
                                    avg TAT:
                                    <asp:Label ID="lblCentreDashboard_MarkTAT" runat="server"></asp:Label>&nbsp;days</div>
                            </div>
                            <div class="infobox infobox-pink">
                                <div class="infobox-icon">
                                    <i class="icon-home"></i>
                                </div>
                                <div class="infobox-data">
                                    <span class="infobox-data-number">*</span> <span class="infobox-content">centre rank</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="hr hr32 hr-dotted">
            </div>
        </asp:Panel>
        <div class="row-fluid" runat="server" Visible="false">
            <div class="span6">
                <div class="widget-box">
                    <div class="widget-header">
                        <h4 class="lighter">
                            <i class="icon-bullhorn green"></i>Centrewise Test Attendance Summary</h4>
                        <div class="widget-toolbar">
                            <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main no-padding" style="height: 240px; overflow-y: scroll; overflow-x: none;">
                            <asp:DataList ID="dlGrid_CentreAbsent" CssClass="table table-bordered table-striped"
                                runat="server" Width="100%">
                                <HeaderTemplate>
                                    <b><span class="green" style="vertical-align: middle; text-align: left"><i class="icon-caret-right blue">
                                    </i>Centre</span></b> </th>
                                    <th class="green" style="width: 15%; text-align: center; vertical-align: middle;">
                                        Tests Conducted
                                    </th>
                                    <th class="green" style="width: 15%; text-align: center; vertical-align: middle;">
                                        Students Appeared
                                    </th>
                                    <th class="green" style="width: 15%; text-align: center; vertical-align: middle;">
                                        Students Absent
                                    </th>
                                    <th class="green" style="width: 15%; text-align: center; vertical-align: middle;">
                                    Absent %
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' />
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <asp:Label ID="lblTestCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Count")%>' />
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <asp:Label ID="lblBatchStrength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Student_Count")%>' />
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <asp:Label ID="lblAbsentCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Absent_Count")%>' />
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <asp:Label ID="lblAbsentPercent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Absent_Percent")%>' />
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <!--/widget-main-->
                    </div>
                    <!--/widget-body-->
                </div>
                <!--/widget-box-->
            </div>
            <div class="span6">
                <div class="widget-box">
                    <div class="widget-header">
                        <h4 class="lighter">
                            <i class="icon-user orange"></i>Studentwise Test Attendance Summary</h4>
                        <div class="widget-toolbar">
                            <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main no-padding" style="height: 240px; overflow-y: scroll; overflow-x: none;">
                            <asp:DataList ID="dlGrid_StudentAbsent" CssClass="table table-bordered table-striped"
                                runat="server" Width="100%">
                                <HeaderTemplate>
                                    <b><span class="orange" style="vertical-align: middle; text-align: left"><i class="icon-caret-right blue">
                                    </i>Student & Centre</span></b> </th>
                                    <th class="orange" style="width: 15%; text-align: center; vertical-align: middle;">
                                        Tests Conducted
                                    </th>
                                    <th class="orange" style="width: 15%; text-align: center; vertical-align: middle;">
                                        Tests Absent
                                    </th>
                                    <th class="orange" style="width: 15%; text-align: center; vertical-align: middle;">
                                    Absent %
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Student_Name")%>' /><br />
                                    <small>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTestCount" class="grey" runat="server"
                                        Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Name")%>' /></small> </td>
                                    <td style="width: 15%; text-align: center;">
                                        <asp:Label ID="lblBatchStrength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Count")%>' />
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <asp:Label ID="lblAbsentCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Absent_Count")%>' />
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <asp:Label ID="lblAbsentPercent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Absent_Percent")%>' />
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <!--/widget-main-->
                    </div>
                    <!--/widget-body-->
                </div>
                <!--/widget-box-->
            </div>
        </div>
        <div class="hr hr32 hr-dotted" runat="server" Visible="false">
        </div>
    </div>
    <!--/#page-content-->
</asp:Content>
