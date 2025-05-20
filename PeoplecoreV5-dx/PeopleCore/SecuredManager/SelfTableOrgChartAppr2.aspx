<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelfTableOrgChartAppr2.aspx.vb" Inherits="SecuredManager_SelfTableOrgChartAppr2" %>
<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/pcore/css.css" rel="stylesheet" type="text/css" />           
    <asp:Literal runat="server" ID="lTheme" />
    <style type="text/css">
        .entryPopupDetl
        {
        	height:400px;
        }            
    </style>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager runat="server" ID="ScriptManager1" />
<div>
    <div id="page-content-wrap-id" class="panel-body table-responsive">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-6">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-8">
                        <h2><asp:Label runat="server" ID="lbl" /></h2>        
                        <asp:Repeater runat="server" ID="rInfo">
                            <ItemTemplate>
                                <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("xDesc") & " " & Eval("xNo")  %>' /><br />
                            </ItemTemplate>
                        </asp:Repeater>                
                    </div>
                </div>
                                                     
                <div class="form-group">
                    <label class="col-md-2 control-label has-space">Display :</label>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="cboView"  CssClass="form-control">
                            <asp:ListItem Text="Without Incumbent" Value="1"  /> 
                            <asp:ListItem Text="With Incumbent" Value="2" />                                               
                            <%--<asp:ListItem Text="Summary View" Value="3" />--%>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label has-space">&nbsp;</label>
                    <div class="col-md-4">
                        <asp:CheckBox runat="server" ID="chkStack" Text="&nbsp;Display in stack" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label has-space">Level :</label>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="cboLevelNo"  CssClass="form-control" />
                    </div>
                </div>
            
                <div class="form-group">
                    <label class="col-md-2 control-label has-space">&nbsp;</label>
                    <div class="col-md-4">
                        <asp:Button runat="server" ID="btnRefresh" CssClass="btn btn-default" Text="Refresh" />                        
                    </div>
                </div>
            </div>                                                                      
            </div>        
        
        </div>
        <div class="row">
            <div class="table-responsive">              
            <asp:DataBoundOrganisationChart runat="server" ID="DataBoundOrganisationChart1" ChartItem-AutoGenerateFields="false" EnableSmartDraw="false" ShowFrame="false" NoDataHtml="Organizatinal chart not available." AssistantItem-AssistantField="IsAssistant" DragDrop-Enabled="false">
                <DetailedTemplate>                
                    <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>'>
                        <asp:Label runat="server" ID="lblTitle" Text='<%# Container.DataElement("Title") & "<br />" %>' style="font-size: .7em;" />                                      
                        <asp:LinkButton runat="server" ID="lnk1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' style="font-size: .7em;" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>'>                                    
                        <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />
                        <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Title") & "<br />" %>' style="font-size: .7em;" />
                        <asp:LinkButton runat="server" ID="lnk2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblName" Text='<%# Container.DataElement("Name") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblDescription" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblItemType" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' style="font-size: .7em;" />                                
                    </asp:Panel>
                    <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" PopupControlID="pMenu" TargetControlID="lnk1" Enabled='<%# Container.DataElement("without") %>' />
                    <asp:Panel ID="pMenu" runat="server" BorderWidth="1" Visible='<%# Container.DataElement("without") %>' style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:50px;position:static;">                
                        <asp:LinkButton runat="server" ID="lnkView" Text="View" OnClick="lnkView_Click" CommandArgument='<%# Container.DataElement("UniqueID") %>' CausesValidation="false" />                    
                    </asp:Panel>
                    <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="Panel2" TargetControlID="lnk2" Enabled='<%# Container.DataElement("with") %>' />
                    <asp:Panel ID="Panel2" runat="server" BorderWidth="1" Visible='<%# Container.DataElement("with") %>' style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:50px;position:static;">                
                        <asp:LinkButton runat="server" ID="LinkButton4" Text="View" OnClick="lnkView_Click" CommandArgument='<%# Container.DataElement("UniqueID") %>' CausesValidation="false" />                   
                    </asp:Panel>
                </DetailedTemplate>
                <AssistantTemplate>
                    <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>'>                                        
                        <asp:Label runat="server" ID="lblTitle" Text='<%# Container.DataElement("Title") & "<br />" %>' style="font-size: .7em;" />
                        <asp:LinkButton runat="server" ID="lnk1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' style="font-size: .7em;" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>'>                                    
                        <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />
                        <asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("Title") & "<br />" %>' style="font-size: .7em;" />
                        <asp:LinkButton runat="server" ID="lnk2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblName" Text='<%# Container.DataElement("Name") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblDescription" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblItemType" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' style="font-size: .7em;" />                                
                    </asp:Panel>
                    <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" PopupControlID="pMenu" TargetControlID="lnk1" Enabled='<%# Container.DataElement("without") %>' />
                    <asp:Panel ID="pMenu" runat="server" BorderWidth="1" Visible='<%# Container.DataElement("without") %>' style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:50px;position:static;">                
                        <asp:LinkButton runat="server" ID="lnkView" Text="View" OnClick="lnkView_Click" CommandArgument='<%# Container.DataElement("UniqueID") %>' CausesValidation="false" />                  
                    </asp:Panel>
                    <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="Panel2" TargetControlID="lnk2" Enabled='<%# Container.DataElement("with") %>' />
                    <asp:Panel ID="Panel2" runat="server" BorderWidth="1" Visible='<%# Container.DataElement("with") %>' style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:50px;position:static;">                
                        <asp:LinkButton runat="server" ID="LinkButton4" Text="View" OnClick="lnkView_Click" CommandArgument='<%# Container.DataElement("UniqueID") %>' CausesValidation="false" />                  
                    </asp:Panel>
                </AssistantTemplate>
                <StackTemplate>   
                    <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>'>                                        
                        <asp:Label runat="server" ID="lblTitle" Text='<%# Container.DataElement("Title") & "<br />" %>' style="font-size: .7em;" />
                        <asp:LinkButton runat="server" ID="lnk1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="Label1" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' style="font-size: .7em;" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>'>                                     
                        <asp:Label runat="server" ID="Label7" Text='<%# Container.DataElement("Title") & "<br />" %>' style="font-size: .7em;" />
                        <asp:LinkButton runat="server" ID="lnk2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblName" Text='<%# Container.DataElement("Name") & "<br />" %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblDescription" Text='<%# Container.DataElement("Display") %>' style="font-size: .7em;" />
                        <asp:Label runat="server" ID="lblItemType" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' style="font-size: .7em;" />                                
                    </asp:Panel>
                    <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" PopupControlID="pMenu" TargetControlID="lnk1" Enabled='<%# Container.DataElement("without") %>' />
                    <asp:Panel ID="pMenu" runat="server" BorderWidth="1" Visible='<%# Container.DataElement("without") %>' style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:50px;position:static;">                
                        <asp:LinkButton runat="server" ID="lnkView" Text="View" OnClick="lnkView_Click" CommandArgument='<%# Container.DataElement("UniqueID") %>' CausesValidation="false" />                   
                    </asp:Panel>
                    <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="Panel2" TargetControlID="lnk2" Enabled='<%# Container.DataElement("with") %>' />
                    <asp:Panel ID="Panel2" runat="server" BorderWidth="1" Visible='<%# Container.DataElement("with") %>' style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:50px;position:static;">                
                        <asp:LinkButton runat="server" ID="LinkButton4" Text="View" OnClick="lnkView_Click" CommandArgument='<%# Container.DataElement("UniqueID") %>' CausesValidation="false" />                 
                    </asp:Panel>
                </StackTemplate>
                <NavigationBarSettings ShowHidePeersButton="True" ShowNavigationButtons="True" ShowSelectButton="False" />                                                                                    
            </asp:DataBoundOrganisationChart>        
            </div>        
        </div>

        <uc:Info runat="server" ID="Info1" />

    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" placeholder="Autonumber" Enabled="false" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-required" runat="server" id="lblReportingTo">Reporting To :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtParentPlantillaCode" CssClass="form-control required" AutoPostBack="true" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Manpower Item No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtPlantillaCode" CssClass="form-control" Enabled="false" placeholder="Autonumber" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Manpower Item Type :</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cboPlantillaTypeNo" runat="server" DataMember="EPlantillaType" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label has-space">Title :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" />
                </div>
            </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Position :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboPositionNo" DataMember="EPosition" CssClass="form-control required" AutoPostBack="True" OnSelectedIndexChanged="cboPositionNo_SelectedIndexChanged" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">No. Of Box :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtNoOfBox" runat="server" SkinID="txtnumber" CssClass="number form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Minimum Salary :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtMinSalary" runat="server" SkinID="txtnumber" CssClass="number form-control" />
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Median Salary :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtMidSalary" runat="server" SkinID="txtnumber" CssClass="number form-control" />
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Maximum Salary :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtMaxSalary" runat="server" SkinID="txtnumber" CssClass="number form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Job Grade :</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cboJobGradeNo" runat="server" DataMember="EJobGrade" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsAssistant" Text="&nbsp;Tick if tag as Staff." />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Group :</label>
                    <div class="col-md-5">
                        <asp:DropDownList runat="server" ID="cboGroupNo" DataMember="EGroup" CssClass="form-control" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsGroHead" Text="&nbsp;Head?" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Division :</label>
                    <div class="col-md-5">
                        <asp:DropDownList runat="server" ID="cboDivisionNo" DataMember="EDivision" CssClass="form-control" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsDivHead" Text="&nbsp;Head?" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Department/PC :</label>
                    <div class="col-md-5">
                        <asp:DropDownList runat="server" ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsDepHead" Text="&nbsp;Head?" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Section/Patch :</label>
                    <div class="col-md-5">
                        <asp:DropDownList runat="server" ID="cboFacilityNo" DataMember="EFacility" CssClass="form-control" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsFacHead" Text="&nbsp;Head?" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Restaurant :</label>
                    <div class="col-md-5">
                        <asp:DropDownList runat="server" ID="cboSectionNo" DataMember="ESection" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboSectionNo_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsSecHead" Text="&nbsp;Head?" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Region :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboRegionNo" DataMember="ERegion" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Location :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboLocationNo" DataMember="ELocation" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Cost Center :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboCostCenterNo" DataMember="ECostCenter" CssClass="form-control" />
                    </div>                
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>                
                </div>
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
    </div>
    </div>
</div>
</form>
</body>
</html>
