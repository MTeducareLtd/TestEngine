<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id='ContentPlaceHolder1_divPrint' style='width: 100%'>
            <table cellpadding='3' class='table table-striped table-bordered table-condensed'
                width='100%'>
                <tr>
                    <td class='span4' style='text-align: left'>
                        <table cellpadding='0' style='border-style: none;' class='table-hover' width='100%'>
                            <tr>
                                <td style='border-style: none; text-align: left; width: 40%;'>
                                    <span id='ContentPlaceHolder1_Label21'>Division</span>
                                </td>
                                <td style='border-style: none; text-align: left; width: 60%;'>
                                    <span id='ContentPlaceHolder1_lblDivision_Result' class='blue'>MUM-SCI-ENG</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class='span4' style='text-align: left'>
                        <table cellpadding='0' style='border-style: none;' class='table-hover' width='100%'>
                            <tr>
                                <td style='border-style: none; text-align: left; width: 40%;'>
                                    <span id='ContentPlaceHolder1_Label27'>Academic Year</span>
                                </td>
                                <td style='border-style: none; text-align: left; width: 60%;'>
                                    <span id='ContentPlaceHolder1_lblAcadYear_Result' class='blue'>2015-2016</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class='span4' style='text-align: left'>
                        <table cellpadding='0' style='border-style: none;' class='table-hover' width='100%'>
                            <tr>
                                <td style='border-style: none; text-align: left; width: 40%;'>
                                    <span id='ContentPlaceHolder1_Label46'>Centre</span>
                                </td>
                                <td style='border-style: none; text-align: left; width: 60%;'>
                                    <span id='ContentPlaceHolder1_lblCentre_Result' class='blue'>BORIVALI</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class='span4' style='text-align: left'>
                        <table cellpadding='0' style='border-style: none;' class='table-hover' width='100%'>
                            <tr>
                                <td style='border-style: none; text-align: left; width: 40%;'>
                                    <span id='ContentPlaceHolder1_Label34'>Course</span>
                                </td>
                                <td style='border-style: none; text-align: left; width: 60%;'>
                                    <span id='ContentPlaceHolder1_lblStandard_Result' class='blue'>XI-Medical</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class='span4' style='text-align: left'>
                        <table cellpadding='0' style='border-style: none;' class='table-hover' width='100%'>
                            <tr>
                                <td style='border-style: none; text-align: left; width: 40%;'>
                                    <span id='ContentPlaceHolder1_Label35'>Test Category</span>
                                </td>
                                <td style='border-style: none; text-align: left; width: 60%;'>
                                    <span id='ContentPlaceHolder1_lblTestCategory_Result' class='blue'>Objective</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class='span4' style='text-align: left'>
                    </td>
                </tr>
            </table>
        </div>
        <table id='ContentPlaceHolder1_dlGridExport' cellspacing='0' style='width: 90%; border-collapse: collapse;'>
            <tr>
                <td>
                    <table border='1' cellpadding='0' cellspacing='0' width='90%'>
                        <tr>
                            <th>
                                <b>Test Name</b>
                            </th>
                            <th align='center' style='width: 10%'>
                                Conduct No
                            </th>
                            <th align='center' style='width: 20%'>
                                Batch
                            </th>
                            <th align='center' style='width: 10%'>
                                Test Type
                            </th>
                            <th align='center' style='width: 20%'>
                                Subjects
                            </th>
                            <th style='width: 10%; text-align: center'>
                                Test Date
                            </th>
                            <th style='width: 10%; text-align: center'>
                                Test Time
                            </th>
                        </tr>
                </td>
            </tr>
            <tr>
                <td>
                    <tr>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_lblModeName_0'>101</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label14_0'>1</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label22_0'>XI-Medical-1516-BOR-01</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label33_0'>FLT</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label26_0'>Biology, Chemistry, Physics</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label25_0'>24 Apr 2015</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label24_0'>20:00-21:00</span>
                        </td>
                    </tr>
                </td>
            </tr>
            <tr>
                <td>
                    <tr>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_lblModeName_1'>201</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label14_1'>1</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label22_1'>XI-Medical-1516-BOR-01</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label33_1'>Unitwise</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label26_1'>Biology, Chemistry, Physics</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label25_1'>24 Apr 2015</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label24_1'>20:00-21:00</span>
                        </td>
                    </tr>
                </td>
            </tr>
            <tr>
                <td>
                    <tr>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_lblModeName_2'>103</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label14_2'>1</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label22_2'>XI-Medical-1516-BOR-01</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label33_2'>FLT</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label26_2'>Biology, Chemistry, Physics</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label25_2'>06 May 2015</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label24_2'>10:00-11:00</span>
                        </td>
                    </tr>
                </td>
            </tr>
            <tr>
                <td>
                    <tr>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_lblModeName_3'>104</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label14_3'>1</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label22_3'>XI-Medical-1516-BOR-01</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label33_3'>FLT</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label26_3'>Physics</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label25_3'>05 May 2015</span>
                        </td>
                        <td align='center'>
                            <span id='ContentPlaceHolder1_dlGridExport_Label24_3'>10:00-11:00</span>
                        </td>
                    </tr>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
