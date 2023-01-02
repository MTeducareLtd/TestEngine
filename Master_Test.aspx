<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true"
    CodeFile="Master_Test.aspx.cs" Inherits="Master_Test" %>

<%@ MasterType VirtualPath="Menu.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function openModalAuthorise() {
            $('#DivAuthorise').modal({
                backdrop: 'static'
            })

            $('#DivAuthorise').modal('show');
        }
        function showProgress() {
            var UpdateProgress1 = $get("<%= UpdateProgress1.ClientID %>");
            UpdateProgress1.style.display = "block";
        }

        function NumberCharsOnly(event) {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 08 || AsciiValue == 127)
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
                <h5 class="smaller">
                    Test<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                OnClick="BtnAdd_Click" Text="New" />
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
                                                                <asp:Label runat="server" ID="Label12">Test Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtTestName" ToolTip="Test Name" type="text" Width="205px" />
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
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" ToolTip="Division"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
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
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" ToolTip="Academic Year"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Test Mode</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTestMode" Width="215px" ToolTip="Test Mode"
                                                                    data-placeholder="Select Test Mode" CssClass="chzn-select" />
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
                                                                <asp:DropDownList runat="server" ID="ddlTestCategory" Width="215px" ToolTip="Test Category"
                                                                    data-placeholder="Select Test Category" CssClass="chzn-select" />
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
                                                                <asp:ListBox runat="server" ID="ddlTestType" Width="215px" ToolTip="Test Type" data-placeholder="Select Test Type"
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
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    OnItemCommand="dlGridDisplay_ItemCommand" runat="server" Width="100%" >
                    <HeaderTemplate>
                        <b>Test Name</b> </th>
                        <th align="left" style="width: 10%">
                            Course
                        </th>
                        <th align="left" style="width: 5%">
                            Test Mode
                        </th>
                        <th align="left" style="width: 5%">
                            Test Category
                        </th>
                        <th align="left" style="width: 5%">
                            Test Type
                        </th>
                        <th style="width: 5%; text-align: center;">
                            Max Marks
                        </th>
                        <th align="left" style="width: 15%">
                            Subjects
                        </th>
                        <th align="left" style="width: 15%">
                            Chapters
                        </th>
                        <th style="width: 5%; text-align: center;">
                            Authorised
                        </th>
                        <th style="width: 15%; text-align: center;">
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
                        <td>
                            <asp:Label ID="Label32" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapters")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AuthoriseFlag")%>' />
                        </td>
                        <td style="width: 100px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit Test" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="Edit" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"EditButtonDisplayFlag")== 1 ? true : false%>' />
                            <asp:LinkButton ID="lnkEditInfoafteras" ToolTip="Edit Test After Test Authorised"
                                class="btn-small btn-primary icon-info-sign" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                runat="server" CommandName="EditAfterAs" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"EditButtonDisplayFlag")== 1 ? false : true%>' />
                            <%--<asp:LinkButton ID="lnkEditInfo" ToolTip="Edit Test" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server" CommandName="Edit"
                                visible ="true"  />--%>
                            <asp:LinkButton ID="lnkuploadomr" ToolTip="Upload OMR Sheets" class="btn-small btn-primary icon-cloud-upload"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                CommandName="UploadOMR" />
                            <asp:LinkButton ID="lnkAuthoriseTest" ToolTip="Authorise Test" CommandName="Authorise"
                                class="btn-small btn-success icon-check" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                runat="server" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"EditButtonDisplayFlag")== 1 ? true : false%>' />
                            <asp:LinkButton ID="LinkButton1" ToolTip="Delete Test" CommandName="Delete" class="btn-small btn-inverse icon-trash"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                Visible='<%#(int)DataBinder.Eval(Container.DataItem,"EditButtonDisplayFlag")== 1 ? true : false%>' />
                    </ItemTemplate>
                </asp:DataList>
                <div id="DivResultExport" runat="server" style="display: none">
                    <asp:DataList ID="dlGridExport" runat="server" HorizontalAlign="Left" CssClass="table table-striped table-bordered table-hover"
                        Width="100%">
                        <HeaderTemplate>
                            <b>Test Name</b> </th>
                            <th align="left" style="width: 10%">
                                Course
                            </th>
                            <th align="left" style="width: 10%">
                                Test Mode
                            </th>
                            <th align="left" style="width: 10%">
                                Test Category
                            </th>
                            <th align="left" style="width: 10%">
                                Test Type
                            </th>
                            <th align="left" style="width: 10%">
                                Max Marks
                            </th>
                            <th align="left" style="width: 20%">
                                Subjects
                            </th>
                            <th align="left" style="width: 20%">
                                Chapters
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
                                <asp:Label ID="Label35" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapters")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AuthoriseFlag")%>' />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_Add" runat="server" Text="Create New Test" />
                    </h5>
                    <asp:Label ID="lblTestPKey_Hidden" runat="server" Text="" Visible="false" />
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
                                                            <asp:Label runat="server" class="red" ID="Label23">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtTestName_Add" ToolTip="Test Name" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="50" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtTestName_Add" ErrorMessage="Test Name can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Special Characters not allowed in Test Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtTestName_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label10">Test Description</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtTestDesc_Add" ToolTip="Test Description" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special Characters not allowed in Test Description !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtTestDesc_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label3" CssClass="red">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlDivision_Add" Width="215px" ToolTip="Division"
                                                                data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlDivision_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" class="red" ID="lblDivision_Add" Visible="False"></asp:Label>
                                                            <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" class="blue"></asp:Label>
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
                                                            <asp:DropDownList runat="server" ID="ddlAcadYear_Add" Width="215px" ToolTip="Academic Year"
                                                                data-placeholder="Select Acad Year" CssClass="chzn-select" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlAcadYear_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" class="red" ID="lblAcadYear_Add" Visible="False"></asp:Label>
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
                                                            <asp:DropDownList runat="server" ID="ddlStandard_Add" Width="215px" ToolTip="Course"
                                                                data-placeholder="Select Course" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" class="red" ID="lblStandard_Add" Visible="False"></asp:Label>
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
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Test Mode</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestMode_Add" Width="215px" ToolTip="Test Mode"
                                                                data-placeholder="Select Test Mode" CssClass="chzn-select" OnSelectedIndexChanged="ddlTestMode_Add_SelectedIndexChanged" />
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
                                                            <asp:DropDownList runat="server" ID="ddlTestCategory_Add" Width="215px" ToolTip="Test Category"
                                                                data-placeholder="Select Category" CssClass="chzn-select" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlTestCategory_Add_SelectedIndexChanged" />
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
                                                            <asp:DropDownList runat="server" ID="ddlTestType_Add" Width="215px" ToolTip="Test Type"
                                                                data-placeholder="Select Test Type" CssClass="chzn-select" OnSelectedIndexChanged="ddlTestType_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label1" CssClass="red">Subject(s)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:ListBox runat="server" ID="ddlSubject_Add" Width="215px" ToolTip="Subject(s)"
                                                                data-placeholder="Select Subject(s)" CssClass="chzn-select" SelectionMode="Multiple"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_Add_SelectedIndexChanged" />
                                                            <asp:ListBox runat="server" ID="ddlSubject_Add_Hidden" Width="12px" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label2" CssClass="red">Test Duration</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlHour_Add" Width="60px" ToolTip="Hr." CssClass="chzn-select" />
                                                            Hr.
                                                            <asp:DropDownList runat="server" ID="ddlMin_Add" Width="60px" ToolTip="Min." CssClass="chzn-select" />
                                                            Min.
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label9">Maximum Marks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtMaxMarks_Add" ToolTip="Maximum Marks" type="text"
                                                                Width="130px" ValidationGroup="UcValidate" MaxLength="6" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtMaxMarks_Add" ErrorMessage="Maximum Marks can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Only Numbers are allowed in Maximum Marks !!"
                                                                ValidationExpression="([0-9]|[.])*" ControlToValidate="txtMaxMarks_Add" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
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
                                                            <asp:Label runat="server" ID="Label11">Remarks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRemarks_Add" ToolTip="Remarks" type="text" Width="270px"
                                                                Height="50px" TextMode="MultiLine" MaxLength="200" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label31" Visible="false">Paper Checker Slab </asp:Label>
                                                            <asp:Label runat="server" ID="Label29">QP Set Count</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlPCSlab" Width="210px" ToolTip="Paper Checker Slab"
                                                                data-placeholder="Select Paper Checker Slab " CssClass="chzn-select" Visible="false" />
                                                            <asp:TextBox runat="server" ID="txtQPSetCount_Add" ToolTip="Question Paper Set Count"
                                                                type="text" Width="205px" ValidationGroup="UcValidate" MaxLength="6" />
                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtQPSetCount_Add" ErrorMessage="QP Set Count can't be blank !!">*</asp:RequiredFieldValidator>
                                                           --%> <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Only Numbers are allowed in QP Set Count !!"
                                                                ValidationExpression="([0-9])*" ControlToValidate="txtQPSetCount_Add" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label30">Hide Chapter Name in Test Schedule </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="ChkChapterHide" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="Row_MCQTestOptions" runat="server" visible="false">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label28">No. of Questions</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtQueCnt_Add" ToolTip="No. of Questions" type="text"
                                                                Width="130px" ValidationGroup="UcValidate" MaxLength="6" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtQueCnt_Add" ErrorMessage="No. of Questions can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Only Numbers are allowed in No. of Questions !!"
                                                                ValidationExpression="([0-9]|[.])*" ControlToValidate="txtQueCnt_Add" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13">Negative Marking</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="chkNegativeMarkingFlag" name="switch-field-1" type="checkbox"
                                                                    class="ace-switch ace-switch-2" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span12" colspan="3" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 15%;">
                                                            <asp:Label runat="server" ID="Label59">Syllabus Description</asp:Label>
                                                            &nbsp;
                                                            <span class="help-button ace-popover" runat="server" id="Span1" data-trigger="hover"
                                                                data-placement="right" data-content="Syllabus description limit to 500 characters"
                                                                title="Help">?
                                                            </span>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 85%;">
                                                            <asp:TextBox runat="server" ID="txtSyllabusDesc" ToolTip="Syllabus Description" type="text"
                                                                Width="80%" MaxLength="500" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>                                            
                                        </tr>
                                    </table>
                                    <div class="row-fluid">
                                        <div class="span8">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Assign Chapters
                                                    </h5>
                                                    <asp:CheckBox ID="chkChapterAllHidden_Sel" runat="server" Visible="False" />
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlChapter_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <HeaderTemplate>
                                                                <b>
                                                                    <asp:CheckBox ID="chkChapterAll" runat="server" AutoPostBack="True" OnCheckedChanged="All_Chapter_ChkBox_Selected" />
                                                                    <span class="lbl"></span></b></th>
                                                                <th align="left" style="width: 33%">
                                                                    Subject
                                                                </th>
                                                                <th align="left" style="width: 33%">
                                                                    Chapter Name
                                                                </th>
                                                                <th align="left" style="width: 33%">
                                                                Display Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkChapter" runat="server" />
                                                                <span class="lbl"></span>
                                                                <asp:Label ID="lblChapterCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>'
                                                                    Visible="false" />
                                                                <asp:Label ID="lblSubjectCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Code")%>'
                                                                    Visible="false" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblChapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblChapter_DisplayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_DisplayName")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="span4">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Assign Centres
                                                    </h5>
                                                    <asp:CheckBox ID="chkCentreAllHidden_Sel" runat="server" Visible="False" />
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlCentre_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <HeaderTemplate>
                                                                <b>
                                                                    <asp:CheckBox ID="chkCentreAll" runat="server" AutoPostBack="True" OnCheckedChanged="All_Centre_ChkBox_Selected" />
                                                                    <span class="lbl"></span></b></th>
                                                                <th align="left" style="width: 95%">
                                                                Center
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkCentre" runat="server" />
                                                                <span class="lbl"></span>
                                                                <asp:Label ID="lblCenterCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Code")%>'
                                                                    Visible="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCenterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Name")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
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
                            OnClick="BtnCloseAdd_Click" runat="server" Text="Close" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivEditPanelAs" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_edit" runat="server" Text="Edit Test After Authorisation" />
                    </h5>
                    <asp:Label ID="lblTestPKey_Hidden_edit" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label43">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtTestName_edit" ToolTip="Test Name" type="text"
                                                                Enabled="false" Width="205px" ValidationGroup="UcValidate" MaxLength="50" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtTestName_edit" ErrorMessage="Test Name can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Special Characters not allowed in Test Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtTestName_edit"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label44">Test Description</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtTestDesc_edit" ToolTip="Test Description" type="text"
                                                                Enabled="false" Width="205px" ValidationGroup="UcValidate" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Special Characters not allowed in Test Description !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtTestDesc_edit"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label45" CssClass="red">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlDivision_Edit" Width="215px" ToolTip="Division"
                                                                Enabled="false" data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True" />
                                                            <asp:Label runat="server" class="red" ID="lblDivision_edit" Visible="False"></asp:Label>
                                                            <asp:Label runat="server" ID="lblpkeyeditas" Visible="false" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label46" CssClass="red">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlAcadYear_Edit" Width="215px" ToolTip="Academic Year"
                                                                Enabled="false" data-placeholder="Select Acad Year" CssClass="chzn-select" AutoPostBack="True" />
                                                            <asp:Label runat="server" class="red" ID="lblAcadYear_edit" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label47" CssClass="red">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlcourse_edit" Width="215px" ToolTip="Course"
                                                                Enabled="false" data-placeholder="Select Course" CssClass="chzn-select" AutoPostBack="True" />
                                                            <asp:Label runat="server" class="red" ID="lblStandard_edit" Visible="False"></asp:Label>
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
                                                            <asp:Label runat="server" ID="Label48" CssClass="red">Test Mode</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestMode_edit" Width="215px" ToolTip="Test Mode"
                                                                Enabled="false" data-placeholder="Select Test Mode" CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label49" CssClass="red">Test Category</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestCategory_Edit" Width="215px" ToolTip="Test Category"
                                                                Enabled="false" data-placeholder="Select Category" CssClass="chzn-select" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label33" CssClass="red">Test Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTestType_Edit" Width="215px" ToolTip="Test Type"
                                                                Enabled="false" data-placeholder="Select Test Type" CssClass="chzn-select" />
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
                                                            <asp:Label runat="server" ID="Label50" CssClass="red">Subject(s)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:ListBox runat="server" ID="ddlSubject_edit" Width="215px" ToolTip="Subject(s)"
                                                                Enabled="false" data-placeholder="Select Subject(s)" CssClass="chzn-select" SelectionMode="Multiple"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_Add_SelectedIndexChanged" />
                                                            <asp:ListBox runat="server" ID="ddlSubject_edit_Hidden" Width="12px" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label34" CssClass="red">Test Duration</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlHour_edit" Width="60px" Enabled="false" ToolTip="Hr."
                                                                CssClass="chzn-select" />
                                                            Hr.
                                                            <asp:DropDownList runat="server" ID="ddlMin_edit" Width="60px" Enabled="false" ToolTip="Min."
                                                                CssClass="chzn-select" />
                                                            Min.
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label51">Maximum Marks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtMaxMarks_edit" ToolTip="Maximum Marks" type="text"
                                                                Enabled="false" Width="130px" ValidationGroup="UcValidate" MaxLength="6" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtMaxMarks_edit" ErrorMessage="Maximum Marks can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Only Numbers are allowed in Maximum Marks !!"
                                                                ValidationExpression="([0-9]|[.])*" ControlToValidate="txtMaxMarks_edit" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
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
                                                            <asp:Label runat="server" ID="Label52">Remarks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRemarks_Edit" ToolTip="Remarks" type="text" Width="270px"
                                                                Enabled="false" Height="50px" TextMode="MultiLine" MaxLength="200" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label53" Visible="false">Paper Checker Slab </asp:Label>
                                                            <asp:Label runat="server" class="red" ID="Label57">QP Set Count</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlPCSlab_edit" Width="210px" ToolTip="Paper Checker Slab"
                                                                Enabled="false" data-placeholder="Select Paper Checker Slab " CssClass="chzn-select"
                                                                Visible="false" />
                                                            <asp:TextBox runat="server" ID="txtqpsetcntedit" ToolTip="Question Paper Set Count"
                                                                Enabled="false" type="text" Width="205px" ValidationGroup="UcValidate" MaxLength="6" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label54">Hide Chapter Name in Test Schedule </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="ChkChapterHide_edit" name="switch-field-1" type="checkbox"
                                                                    class="ace-switch ace-switch-2" disabled="true" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="Row_MCQTestOptionsEdit" runat="server" visible="false">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label55">No. of Questions</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtQueCnt_edit1" ToolTip="No. of Questions" type="text"
                                                                Enabled="false" Width="130px" ValidationGroup="UcValidate" MaxLength="6" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="UcValidate"
                                                                ControlToValidate="txtQueCnt_edit1" ErrorMessage="No. of Questions can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Only Numbers are allowed in No. of Questions !!"
                                                                ValidationExpression="([0-9]|[.])*" ControlToValidate="txtQueCnt_edit1" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label56">Negative Marking</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="chkNegativeMarkingFlagedit" name="switch-field-1" type="checkbox"
                                                                    class="ace-switch ace-switch-2" disabled="true" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span12" colspan="3" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 15%;">
                                                            <asp:Label runat="server" ID="Label60">Syllabus Description</asp:Label>
                                                            &nbsp;
                                                            <span class="help-button ace-popover" runat="server" id="SyllabusDescHelpFlag" data-trigger="hover"
                                                                data-placement="right" data-content="Syllabus description limit to 500 characters"
                                                                title="Help">?
                                                            </span>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 85%;">
                                                            <asp:TextBox runat="server" ID="txtSyllabusDesc_AfterAuth" ToolTip="Syllabus Description" type="text"
                                                                Width="80%" MaxLength="500" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>                                            
                                        </tr>
                                    </table>
                                    <div class="row-fluid">
                                        <div class="span8">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Assign Chapters
                                                    </h5>
                                                    <asp:CheckBox ID="chkChapterAllHidden_Sel_edit" runat="server" Visible="False" />
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlChapter_Add_edit" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <HeaderTemplate>
                                                                <b>
                                                                    <asp:CheckBox ID="chkChapterAll_edit" runat="server" AutoPostBack="True" OnCheckedChanged="All_Chapter_ChkBox_Selected_Edit" />
                                                                    <span class="lbl"></span></b></th>
                                                                <th align="left" style="width: 33%">
                                                                    Subject
                                                                </th>
                                                                <th align="left" style="width: 33%">
                                                                    Chapter Name
                                                                </th>
                                                                <th align="left" style="width: 33%">
                                                                Display Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkChapteredit" runat="server" />
                                                                <span class="lbl"></span>
                                                                <asp:Label ID="lblChapterCode_edit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>'
                                                                    Visible="false" />
                                                                <asp:Label ID="lblSubjectCode_edit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Code")%>'
                                                                    Visible="false" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSubject_edit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblChapter_edit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblChapter_DisplayName_edit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_DisplayName")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="span4 ">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Assign Centres
                                                    </h5>
                                                    <asp:CheckBox ID="chkCentreAllHidden_Sel_edit" runat="server" Visible="False" />
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlCentre_edit" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Enabled="false" Width="100%">
                                                            <HeaderTemplate>
                                                                <b>
                                                                    <asp:CheckBox ID="chkCentreAll_edit" runat="server" AutoPostBack="True" />
                                                                    <span class="lbl"></span></b></th>
                                                                <th align="left" style="width: 95%">
                                                                Center
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkCentre_edit" runat="server" />
                                                                <span class="lbl"></span>
                                                                <asp:Label ID="lblCenterCode_edit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Code")%>'
                                                                    Visible="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCenterName_edit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Name")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label58" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btn_save_edit"
                            runat="server" Text="Save" ValidationGroup="UcValidate" OnClick="btn_save_edit_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btn_close" Visible="true"
                            runat="server" Text="Close" OnClick="btn_close_Click" />
                        <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivUploadPannel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="Label36" runat="server" Text="Upload OMR Sheets" />
                    </h5>
                    <asp:Label ID="Label37" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label40">Test Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtuploadtestname" ToolTip="Test Name" type="text"
                                                                Enabled="false" Width="205px" ValidationGroup="UcValidate" MaxLength="50" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label41">Test Description</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtuploadtestdesc" ToolTip="Test Description" type="text"
                                                                Enabled="false" Width="205px" ValidationGroup="UcValidate" MaxLength="100" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label42" CssClass="red">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" class="red" ID="lbluploaddiviosion" Visible="true"></asp:Label>
                                                            <asp:Label runat="server" ID="lbluploaddiviosion_1" Visible="false" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label61" CssClass="red">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" class="red" ID="lbluploadacadyear" Visible="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label63" CssClass="red">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label runat="server" class="red" ID="lbluploadcourse" Visible="true"></asp:Label>
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
                                                            <asp:Label runat="server" ID="Label65" CssClass="red">Test Mode</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddluploadtestmode" Width="215px" ToolTip="Test Mode"
                                                                Enabled="false" data-placeholder="Select Test Mode" CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label66" CssClass="red">Test Category</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddluploadtestcategory" Width="215px" ToolTip="Test Category"
                                                                Enabled="false" data-placeholder="Select Category" CssClass="chzn-select" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label67" CssClass="red">Test Type</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddluploadtesttype" Width="215px" ToolTip="Test Type"
                                                                Enabled="false" data-placeholder="Select Test Type" CssClass="chzn-select" />
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
                                                            <asp:Label runat="server" class="red" ID="Label39">Folder Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtuploadfoldername" ToolTip="Folder Name" type="text"
                                                                AutoPostBack="true" Width="205px" onkeypress="return NumberCharsOnly(event)"  onpaste="return false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label38">Select Files In Folder</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <input type="file" multiple="true" id="filesss" style="width: 85%" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <asp:Label runat="server" ID="lblpkeyuploadomr" Visible="false" class="blue"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <img alt="" src="WaitLoad.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveUpload"
                                    runat="server" Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveUpload_Click"
                                    OnClientClick="showProgress()" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseUpload"
                                    Visible="true" runat="server" Text="Close" OnClick="BtnCloseUpload_Click1" />
                                <div id="divwaitsave" runat="server" visible="false">
                                    <i id="waitsave" runat="server" class="icon-spinner icon-spin orange bigger-150">
                                    </i>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BtnSaveUpload" />
                                <asp:PostBackTrigger ControlID="BtnCloseUpload" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
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
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                        ToolTip="Yes" runat="server" Text="Yes" OnClick="btnDelete_Yes_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="DivAuthorise" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Authorise Test
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to authorise Test
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="lblAuthoriseTestName"
                        Text="" />. Do you want to Continue?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lblPKey_Authorise" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnAuthorise_Yes"
                        ToolTip="Yes" runat="server" Text="Yes" OnClick="btnAuthorise_Yes_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnAuthorise_No" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>
