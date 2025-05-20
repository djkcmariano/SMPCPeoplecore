<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OrgTableOrgDeti2.aspx.vb" Inherits="Secured_OrgTableOrgDeti2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    body 
    {
        font-family: Arial;
        font-size:.7em;           
    }   
    .to_container img
    {
        width:60px;
        height:70px;       
    }                
</style>       
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager runat="server" ID="ScriptManager1" />
<asp:UpdatePanel runat="server" ID="UpdatePanel1">
<ContentTemplate>
<div id="page-content-wrap-id" class="panel-body table-responsive">
    <div class="page-content-wrap">               
        <div class="row text-center">        
            <h2><asp:Label runat="server" ID="lbl" /></h2>
        </div>
        <div class="row">
            <asp:DataBoundOrganisationChart runat="server" ID="DataBoundOrganisationChart1" ChartItem-AutoGenerateFields="false" EnableSmartDraw="false" ShowFrame="false" NoDataHtml="Organizational chart not available." AssistantItem-AssistantField="IsAssistant">
                <DetailedTemplate>
                    <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>' CssClass="to_container">
                        <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                        
                        <asp:Label runat="server" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' />
                    </asp:Panel>                                
                    <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>' CssClass="to_container">                                    
                        <asp:Image runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />                                                                    
                        <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                
                        <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />                                
                        <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small-font" />                                                                                
                    </asp:Panel>
                    <%--<asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />--%>
                    <asp:Label runat="server" ID="Label4" Text='<%# Container.DataElement("PlantillaGovTypeDesc") %>' CssClass="smaller-font" />
                </DetailedTemplate>
                <AssistantTemplate>
                    <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>' CssClass="to_container">                                
                        <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                
                        <asp:Label ID="Label3" runat="server" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' />
                    </asp:Panel>                                
                    <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>' CssClass="to_container">                                    
                        <asp:Image runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />                                    
                        <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                
                        <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />                                
                        <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small-font" />                                                                                
                    </asp:Panel>
                    <%--<asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />--%>
                    <asp:Label runat="server" ID="Label7" Text='<%# Container.DataElement("PlantillaGovTypeDesc") %>' CssClass="smaller-font" />
                </AssistantTemplate>
                <StackTemplate>
                    <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>' CssClass="to_container">
                        <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                    
                        <asp:Label ID="Label3" runat="server" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' />
                    </asp:Panel>                                
                    <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>' CssClass="to_container">                                                                        
                        <asp:Image runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />                                    
                        <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                    
                        <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" />                                
                        <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Name") & "<br />" %>' CssClass="small-font" />
                    </asp:Panel>                                
                    <asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />
                </StackTemplate>
                <StackItem ShowBackgroundImage="false">                               
                </StackItem>                                                                               
            </asp:DataBoundOrganisationChart>
            <br /><br />        
        </div>
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</form>
</body>
</html>
