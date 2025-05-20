<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfOrgTableChartAppr.aspx.vb" Inherits="SecuredManager_SelfOrgTableChartAppr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<asp:Button ID="btn" runat="server" style="display:none" />    
<ajaxToolkit:HoverMenuExtender ID="hmeOrg" runat="server" PopupControlID="pMenu" TargetControlID="btn" BehaviorID="hmeOrgMenu" />
<asp:Panel ID="pMenu" runat="server" BorderWidth="1" style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:70px;position:static;">                
    <asp:LinkButton runat="server" ID="lnkView" Text="View" OnClick="lnkView_Click" />
</asp:Panel>

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default" id="TOcontainer">
            <div class="panel-heading">
                <div class="col-md-4">
                    <asp:Label runat="server" ID="lbl" ></asp:Label><br /><br />
                </div>
            </div>             
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <asp:HiddenField runat="server" ID="hifPlantillaCode" />
                        <asp:DataBoundOrganisationChart runat="server" ID="DataBoundOrganisationChart1" ChartItem-AutoGenerateFields="false" EnableSmartDraw="false" ShowFrame="false" NoDataHtml="Organizatinal chart not available." AssistantItem-AssistantField="IsAssistant">
                            <DetailedTemplate>
                                <%--<asp:Panel runat="server" ID="pWith" Visible="True">                                        
                                    <asp:LinkButton runat="server" ID="LinkButton1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" onmouseover="hoverMenu(this);"  />                                    
                                    <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />
                                </asp:Panel>--%>
                                <asp:Panel runat="server" ID="pWithout" Visible="true">                                    
                                    <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />
                                    <asp:LinkButton runat="server" ID="lnk" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" onmouseover="hoverMenu(this);"  />
                                    <asp:Label runat="server" ID="lblItemType" Text='<%# Container.DataElement("PlantillaTypeDesc") & "<br />" %>' style="font-size: .7em;" />
                                    <asp:Label runat="server" ID="lblName" Text='<%# Container.DataElement("Name") & "<br />" %>' style="font-size: .7em;" />
                                    <asp:Label runat="server" ID="lblDescription" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />                                
                                </asp:Panel>                                
                            </DetailedTemplate>
                            <AssistantTemplate>
                                <%--<asp:Panel runat="server" ID="pWith" Visible="true">                                        
                                    <asp:LinkButton runat="server" ID="LinkButton1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" onmouseover="hoverMenu(this);"  />                                    
                                    <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />
                                </asp:Panel>--%>
                                <asp:Panel runat="server" ID="pWithout" Visible="true">                                    
                                    <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />
                                    <asp:LinkButton runat="server" ID="lnk" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" onmouseover="hoverMenu(this);"  />
                                    <asp:Label runat="server" ID="lblItemType" Text='<%# Container.DataElement("PlantillaTypeDesc") & "<br />" %>' style="font-size: .7em;" />
                                    <asp:Label runat="server" ID="lblName" Text='<%# Container.DataElement("Name") & "<br />" %>' style="font-size: .7em;" />
                                    <asp:Label runat="server" ID="lblDescription" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />                                
                                </asp:Panel>
                            </AssistantTemplate>                                                                                    
                        </asp:DataBoundOrganisationChart>
                    </div>                    
                </div>
            </div>
        </div>
    </div>
</div>

<script language="javascript" type="text/javascript">
    var obj;
    function hoverMenu(sender) {
        obj = sender;
        var j = Sys.UI.DomElement.getLocation(obj);
        var offsets = $('#TOcontainer').offset();
        $get("<%=hifPlantillaCode.ClientID %>").value = obj.text;        
        $find("hmeOrgMenu")._onUnhover();
        $find("hmeOrgMenu")._onHover();
        $find("hmeOrgMenu")._popupBehavior.set_x((j.x + obj.offsetWidth) - offsets.left);
        $find("hmeOrgMenu")._popupBehavior.set_y(j.y);
    }
</script>
         
</asp:Content>

