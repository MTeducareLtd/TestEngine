<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_Marksheet_Print.aspx.cs" Inherits="Report_Marksheet_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Marksheet</title>
    <style type="text/css">
        @page Section1
        {
            size: 8.27in 11.69in;
            margin: .2in .2in .2in .2in;
            mso-header-margin: 0in;
            mso-footer-margin: 0in;
            mso-paper-source: 0;
        }
        
        
        
        body
        {
            /* this affects the margin on the content before sending to printer */
            margin: .2in .2in .2in .2in;
            background: #FFFFFF;
            font-family: Tahoma;
            font-size: 10pt;
        }
        
        
        #wrap
        {
            width: 1003px;
            height: 100%;
            margin: auto;
        }
        
        .logo
        {
            width: 999px;
            height: 150px;
            float: left;
        }
        
        .fee_receipt
        {
            width: 1003px;
            height: 30px;
            float: left;
            text-align: center;
        }
        
        .fee_receipt span
        {
            width: auto;
            height: 25px;
            border: #6d9ab7 1px solid;
            padding: 2px 10px;
            line-height: 25px;
            color: #6d9ab7;
            font-weight: bold;
            font-size: 18px;
        }
        
        .tbl
        {
            height: 25px;
            text-align: center;
            font-size: 14px;
            color: #000000;
            line-height: 25px;
        }
        
        .tbl1
        {
            height: 25px;
            text-align: left;
            font-size: 14px;
            color: #000000;
            line-height: 25px;
        }
        
        label
        {
            width: 2px;
            height: 2px;
            color: #6d9ab7;
            text-align: center;
        }
        
        .left
        {
            width: 600px;
            float: left;
            height: 120px;
        }
        
        .right
        {
            width: 403px;
            float: left;
            height: 120px;
        }
        
        .txt
        {
            width: 400px;
            height: 25px;
            line-height: 25px;
            font-size: 12px;
            color: #6d9ab7;
            text-align: center;
        }
        
        .sign
        {
            margin: 10px 160px;
            width: 80px;
            height: 80px;
            float: left;
            border: 1px solid #6d9ab7;
        }
        
        .last
        {
            width: 1003px;
            height: 60px;
            float: left;
            font-size: 12px;
            font-weight: bold;
            color: #333333;
        }
        
        .table
        {
            width: 1003px;
            height: auto;
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Section1">
        <div class="table">
            <asp:DataList ID="dlGridSummaryReport" runat="server" Width="100%" 
                BorderStyle="Solid" BorderWidth="1px">
                <HeaderTemplate>
                    <b>Test Date</b> </th>
                    <th style="width: 10%; text-align: left">
                        Test Name
                    </th>
                    <th style="width: 30%; text-align: left">
                        Subject
                    </th>
                    <th style="width: 10%; text-align: center">
                        Score
                    </th>
                    <th style="width: 10%; text-align: center">
                        Out of
                    </th>
                    <th style="width: 10%; text-align: center">
                        Percent
                    </th>
                    <th style="width: 10%; text-align: center">
                        Centre Rank
                    </th>
                    <th style="width: 10%; text-align: center">
                    Overall Rank
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDLTestDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Date")%>' />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblDLTestName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Test_Name")%>' />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblDLSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblDLMarksObtd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Obtd_Marks")%>' />
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblDLMarksOutOf" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OutOf_Marks")%>' />
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblDLPercent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Percent_Marks")%>' />
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblDLCentreRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreRank")%>' />
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblDLOvarllRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OverallRank")%>' />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    </form>
</body>
</html>
