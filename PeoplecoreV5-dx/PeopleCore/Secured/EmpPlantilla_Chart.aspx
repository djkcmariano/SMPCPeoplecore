<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpPlantilla_Chart.aspx.vb" Inherits="Secured_EmpPlantilla_Chart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body 
        {
            font-family: Arial;
            font-size:.7em;            
        }
        .small_font
        {
            font-size:.7em;
        }
        .to_container
        {
            padding: 10px;
        }
        .stack
        {
            border: 1px solid #e5e5e5;            
        }
    </style>
    <script type="text/javascript" src="../js/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../js/html2canvas.js"></script>
    <script type="text/javascript" src="../js/canvas2image.js"></script>        
</head>
<body>
    <form id="form1" runat="server">            
    <br />
    <asp:HiddenField ID="hfImageData" runat="server" />    
    <table border="0">
        <tr>
            <td>Display :</td>
            <td>
                <asp:DropDownList runat="server" ID="cboView"  CssClass="form-control">
                    <asp:ListItem Text="W / O Incumbent" Value="0" Selected="True"  /> 
                    <asp:ListItem Text="W / Incumbent" Value="1" />                                               
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Level :</td>
            <td><asp:DropDownList runat="server" ID="cboLevelNo" Width="200px" AutoPostBack="true" /></td>
        </tr>
        <tr>
            <td>Start With :</td>
            <td><asp:DropDownList runat="server" ID="cboStartWith" Width="200px" AutoPostBack="true" /></td>
        </tr>        
        <tr>
            <td colspan="2">
                <asp:CheckBox runat="server" ID="chkStack" Text="&nbsp;Stack last level" AutoPostBack="true" />        
            </td>            
        </tr>
    </table>
    <script type="text/javascript">

        

        function screenshot() {
            html2canvas(document.body, {
                onrendered: function (canvas) {
                    //document.body.appendChild(canvas);
                    //var dataUrl = canvas.toDataURL();
                    //document.write('<img src="' + dataUrl + '"/>');

                    //img = canvas.toDataURL("image/jpeg");
                    //download(img, "chart.jpg", "image/jpeg");

                    if (canvas.msToBlob) { //for IE
                        var blob = canvas.msToBlob();
                        window.navigator.msSaveBlob(blob, 'chart.png');
                    } else {
                        //other browsers
                        Canvas2Image.saveAsPNG(canvas);
                        //img = canvas.toDataURL("image/png");
                        //download(img, "chart.png", "image/png")
                    }                    
                }
            });
        }
    </script> 
           
    <asp:DataBoundOrganisationChart runat="server" ID="DataBoundOrganisationChart1" ChartItem-AutoGenerateFields="false" EnableSmartDraw="false" ShowFrame="false" NoDataHtml="Organizational chart not available." AssistantItem-AssistantField="IsAssistant">
        <DetailedTemplate>
            <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("wo") %>' CssClass="to_container">
                <asp:LinkButton runat="server" ID="lnk1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small_font" />                
                <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Display") %>' CssClass="small_font" />                                
            </asp:Panel>
            <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("w") %>' CssClass="to_container">
                <asp:Image Width="60" Height="70" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left;" />                                    
                <asp:LinkButton runat="server" ID="lnk2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small_font" />
                <asp:Label runat="server" ID="Label3" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small_font"  />
                <asp:Label runat="server" ID="Label4" Text='<%# Container.DataElement("Display") %>' CssClass="small_font" />                
            </asp:Panel>
        </DetailedTemplate>        
        <AssistantTemplate>
            <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("wo") %>' CssClass="to_container">
                <asp:LinkButton runat="server" ID="lnk1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small_font" />                
                <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Display") %>' CssClass="small_font" />                                
            </asp:Panel>
            <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("w") %>' CssClass="to_container">
                <asp:Image Width="60" Height="70" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left;" />                                    
                <asp:LinkButton runat="server" ID="lnk2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small_font" />
                <asp:Label runat="server" ID="Label3" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small_font"  />
                <asp:Label runat="server" ID="Label4" Text='<%# Container.DataElement("Display") %>' CssClass="small_font" />                
            </asp:Panel>
        </AssistantTemplate>
        <StackTemplate>
            <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("wo") %>' CssClass="to_container">
                <asp:LinkButton runat="server" ID="lnk1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small_font" />                
                <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Display") %>' CssClass="small_font" />                                
            </asp:Panel>
            <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("w") %>' CssClass="to_container">                
                <asp:LinkButton runat="server" ID="lnk2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small_font" />
                <asp:Label runat="server" ID="Label3" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small_font"  />
                <asp:Label runat="server" ID="Label4" Text='<%# Container.DataElement("Display") %>' CssClass="small_font" />                
            </asp:Panel>            
        </StackTemplate>
    </asp:DataBoundOrganisationChart>
       
    </form>
</body>
</html>
