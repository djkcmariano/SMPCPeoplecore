<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpPlantillaChart.aspx.vb" Inherits="Secured_EmpPlantillaChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
    .small-font
    {
        font-size:.7em;                                    
    }
    .smaller-font
    {
        font-size:.6em;                                    
    }
    .stack
    {
        border: 1px solid #e5e5e5;            
    }        
    </style> 
    <link rel='stylesheet' type='text/css' id='theme' href='../css/theme-light.css' />
</head>
<body>
    <form id="form1" runat="server">
    
        <div class="row">
            <div class="panel panel-default">            
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                        
                            <asp:DataBoundOrganisationChart runat="server" ID="DataBoundOrganisationChart1" ChartItem-AutoGenerateFields="false" EnableSmartDraw="false" ShowFrame="false" NoDataHtml="Organizational chart not available." AssistantItem-AssistantField="IsAssistant">
                            <DetailedTemplate>
                                <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>'>
                                    <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />                                
                                </asp:Panel>                                
                                <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>'>                                    
                                    <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />                                                                    
                                    <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label3" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small-font" />                                
                                </asp:Panel>
                                <asp:Label runat="server" ID="Label4" Text='<%# Container.DataElement("PlantillaGovTypeDesc") %>' CssClass="smaller-font" />
                                <%--<asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />--%>
                                <asp:HiddenField runat="server" ID="hifIsMirror" Value='<%# Container.DataElement("IsMirror")  %>' />
                            </DetailedTemplate>
                            <AssistantTemplate>
                                <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>'>
                                    <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />                                
                                </asp:Panel>                                
                                <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>'>                                    
                                    <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />                                                                    
                                    <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label3" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small-font" />                                
                                </asp:Panel>
                                <asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaGovTypeDesc") %>' CssClass="smaller-font" />
                                <%--<asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />--%>
                            </AssistantTemplate>
                            <StackTemplate>
                                <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>'>
                                    <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />                                
                                </asp:Panel>                                
                                <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>'>                                    
                                    <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />                                                                    
                                    <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />
                                    <asp:Label runat="server" ID="Label3" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small-font" />                                
                                </asp:Panel>                                
                                <%--<asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />--%>
                            </StackTemplate>
                            <%--<StackItem ShowBackgroundImage="false"></StackItem>                                                                               
                            <ChartItem BackgroundImageStyle="Square" ItemStyle-BorderWidth="1" ></ChartItem>  --%>                                              
                        </asp:DataBoundOrganisationChart>
                        
                        </div>                    
                    </div>
                </div>
            </div>
        </div>
    
       
    </form>
</body>
</html>