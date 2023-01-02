<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Report_UC_SearchPanel.ascx.cs"
    Inherits="Report_UC_SearchPanel" %>
<table cellpadding="3" class="table table-striped table-bordered table-condensed">
    <tr>
        <td class="span4" style="text-align: left">
            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                <tr id="trreporttype" runat="server">
                    <td style="border-style: none; text-align: left; width: 40%;">
                        <asp:Label runat="server" ID="Label1" CssClass="red">Report Type</asp:Label>
                    </td>
                    <td style="border-style: none; text-align: left; width: 60%;">
                        <asp:DropDownList runat="server" ID="ddlReportType" Width="215px" data-placeholder="Select Report Type"
                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" />
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
                        <asp:Label runat="server" ID="Label115" CssClass="red">Division</asp:Label>
                    </td>
                    <td style="border-style: none; text-align: left; width: 60%;">
                        <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" data-placeholder="Select Division"
                            CssClass="chzn-select" />
                        <asp:Label runat="server" ID="lblHeader_Company_Code" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblHeader_User_Code" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblHeader_DBName" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblFinal_Test_Id" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblFinal_Subject_Code" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblFinal_Centre_Code" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblFinal_Batch_Code" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td class="span4" style="text-align: left">
            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                <tr>
                    <td style="border-style: none; text-align: left; width: 40%;">
                        <asp:Label runat="server" ID="Label116" CssClass="red">Academic Year</asp:Label>
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
                            CssClass="chzn-select" AutoPostBack="True" SelectionMode="Multiple" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
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
                        <asp:Label runat="server" ID="Label117" CssClass="red">Course</asp:Label>
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
                        <asp:Label runat="server" ID="Label28" >Batch</asp:Label>
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
                        <asp:Label runat="server" ID="Label19" CssClass="red">Test Category</asp:Label>
                    </td>
                    <td style="border-style: none; text-align: left; width: 60%;">
                        <asp:DropDownList runat="server" ID="ddlTestCategory" Width="215px" data-placeholder="Select Test Category"
                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTestCategory_SelectedIndexChanged" />
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
                        <asp:Label runat="server" ID="Label20" >Test Type</asp:Label>
                    </td>
                    <td style="border-style: none; text-align: left; width: 60%;">
                        <asp:ListBox runat="server" ID="ddlTestType" Width="215px" data-placeholder="Select Test Type"
                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ddlTestType_SelectedIndexChanged" />
                    </td>
                </tr>
            </table>
        </td>
        <td class="span4" style="text-align: left">
            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                <tr>
                    <td style="border-style: none; text-align: left; width: 40%;">
                        <i class="icon-calendar"></i>
                        <asp:Label runat="server" ID="Label29">  Test Period</asp:Label>
                    </td>
                    <td style="border-style: none; text-align: left; width: 60%;">
                        <input runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                            id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                            data-original-title="Date Range" style="width: 205px" />
                    </td>
                </tr>
            </table>
        </td>
        <td class="span4" style="text-align: left">
            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                <tr>
                    <td style="border-style: none; text-align: left; width: 40%;">
                        <asp:Label runat="server" ID="lbltestname">Test Name</asp:Label>
                        <asp:Label runat="server" ID="lbltestname_1"  Visible="false" CssClass="red">Test Name</asp:Label>
                        <asp:LinkButton ID="btnTestName" ToolTip="Get Test Names" class="btn-small btn-primary icon-refresh" 
                                                                    OnClick="btnTestName_Click" runat="server" />
                    </td>
                    <td style="border-style: none; text-align: left; width: 60%;">
                        <asp:ListBox runat="server" ID="ddlTestName" Width="215px" data-placeholder="Select Test Name"
                            CssClass="chzn-select" SelectionMode="Multiple" />
                             <asp:DropDownList runat="server" ID="ddltestname_1" Width="215px" data-placeholder="Select Test Name"
                            CssClass="chzn-select" AutoPostBack="True" Visible="false" />

                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr runat="server" id="Table_Row_5" visible="false">
        <td class="span4" style="text-align: left">
            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                <tr>
                    <td style="border-style: none; text-align: left; width: 40%;">
                        <asp:Label runat="server" ID="Label2">Subject</asp:Label>&nbsp;<span class="help-button ace-popover"
                            data-trigger="hover" data-placement="right" data-content="Subjectwise report option is available only for Objective Test Category"
                            title="Subject">?</span>
                    </td>
                    <td style="border-style: none; text-align: left; width: 60%;">
                        <%--<asp:DropDownList runat="server" ID="ddlSubject" Width="215px" data-placeholder="Select Subject"
                            CssClass="chzn-select" AutoPostBack="True" />--%>
                            <asp:ListBox runat="server" ID="ddlSubject" Width="215px" data-placeholder="Select Subject"
                            CssClass="chzn-select"  SelectionMode="Multiple"  />
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
