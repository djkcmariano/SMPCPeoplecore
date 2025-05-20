<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfTableOrgChartAppr.aspx.vb" Inherits="SecuredManager_SelfTableOrgChartAppr" %>
<%@ Register Src="~/Include/EmpInfo.ascx" TagName="EmpInfo" TagPrefix="uc" %>
<%@ Register Src="~/Include/JobProfile.ascx" TagName="JobProfile" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<asp:HiddenField runat="server" ID="hifPlantillaCode" />
<asp:Button ID="btn" runat="server" style="display:none" />    
<ajaxToolkit:HoverMenuExtender ID="hmeOrg" runat="server" PopupControlID="pMenu" TargetControlID="btn" BehaviorID="hmeOrgMenu" />
<asp:Panel ID="pMenu" runat="server" BorderWidth="1" style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:70px;position:static;">                        
    <%--<asp:LinkButton runat="server" ID="lnkAdd" Text="Add" OnClick="lnkAdd_Click" /><div></div>
    <asp:LinkButton runat="server" ID="lnkEdit" Text="Edit" OnClick="lnkEdit_Click" /><asp:Literal runat="server" ID="brEdit" />
    <asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" OnClick="lnkDelete_Click" /><asp:Literal runat="server" ID="brDelete" />--%>
    <asp:LinkButton runat="server" ID="lnkView" Text="View" OnClick="lnkView_Click" />    
</asp:Panel>

<asp:Button ID="btn2" runat="server" style="display:none" />    
<ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" PopupControlID="Panel2" TargetControlID="btn2" BehaviorID="hmeOrgMenu2" />
<asp:Panel ID="Panel2" runat="server" BorderWidth="1" style="background-color:#fff;padding:1px 5px 1px 3px;display:none;text-align:left;padding:5px;min-width:70px;position:static;">
    <%--<asp:HiddenField runat="server" ID="hifPlantillaCode" />--%>
    <asp:LinkButton runat="server" ID="lnkHRAN" Text="H R A N" OnClick="lnkHRAN_Click" /><br />
    <asp:LinkButton runat="server" ID="lnkMR" Text="M R" OnClick="lnkMR_Click" />
</asp:Panel>
<div id="TOcontainer">
<uc:Tab runat="server" ID="Tab">
<Header>        
    <div class="row">
    <div class="form-horizontal">            
        <div class="form-group">
            <label class="col-md-3 control-label has-space">Display</label>
            <div class="col-md-9">
                <asp:DropDownList runat="server" ID="cboView"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbo_SelectedIndexChange">
                    <asp:ListItem Text="W/O Incumbent" Value="0"  /> 
                    <asp:ListItem Text="With Incumbent" Value="1" />                                               
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label has-space">Level</label>
            <div class="col-md-9">
                <asp:DropDownList runat="server" ID="cboLevelNo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbo_SelectedIndexChange" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label has-space">Start</label>
            <div class="col-md-9">
                <asp:DropDownList runat="server" ID="cboStartWith"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbo_SelectedIndexChange" />
            </div>
        </div>
        <%--<div class="form-group">
            <label class="col-md-3 control-label has-space">&nbsp;</label>
            <div class="col-md-9">
                <asp:CheckBox runat="server" ID="chkStack" Text="&nbsp;Stack last level" AutoPostBack="true" OnCheckedChanged="cbo_SelectedIndexChange" />
            </div>
        </div>--%> 
        <div class="form-group">
            <label class="col-md-3 control-label has-space">&nbsp;</label>
            <div class="col-md-9">
                <asp:CheckBox runat="server" ID="chkStack" Text="&nbsp;Stack last level" AutoPostBack="true" OnCheckedChanged="cbo_SelectedIndexChange" /><br />
                <asp:CheckBox runat="server" ID="chkIsMirrorView" Text="&nbsp;Mirrored" AutoPostBack="true" OnCheckedChanged="cbo_SelectedIndexChange" /><br />
                <asp:CheckBox runat="server" ID="chkIsBridgeView" Text="&nbsp;Bridged" AutoPostBack="true" OnCheckedChanged="cbo_SelectedIndexChange" /><br />
                <asp:CheckBox runat="server" ID="chkIsClusterView" Text="&nbsp;Clustered" AutoPostBack="true" OnCheckedChanged="cbo_SelectedIndexChange" /><br />
                <asp:CheckBox runat="server" ID="chkIsBudgetSource" Text="&nbsp;Budget Source" AutoPostBack="true" OnCheckedChanged="cbo_SelectedIndexChange" /><br />
                <asp:CheckBox runat="server" ID="chkIsReassignment" Text="&nbsp;Reassignment" AutoPostBack="true" OnCheckedChanged="cbo_SelectedIndexChange" />
            </div>
        </div>           
    </div>
    </div>
    <hr />
    <b>SUMMARY</b><br />
    <asp:Repeater runat="server" ID="rInfo" >
        <ItemTemplate>                    
            <span class="small-font">&#9658;&nbsp;</span><asp:Label runat="server" ID="lbl" Text='<%# Eval("xDesc") & " " & Eval("xNo")  %>' CssClass="small-font" /><br />
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <b>VACANT</b><br />
    <asp:Repeater runat="server" ID="rVacant" >
        <ItemTemplate>                    
            <span class="small-font">&#9658;&nbsp;</span><asp:Label runat="server" ID="lbl" Text='<%# Eval("xDesc") & " " & Eval("xNo")  %>' CssClass="small-font" /><br />
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <b>OCCUPIED</b><br />
    <asp:Repeater runat="server" ID="rOccupied" >
        <ItemTemplate>
            <span class="small-font">&#9658;&nbsp;</span><asp:Label runat="server" ID="lbl" Text='<%# Eval("xDesc") & " " & Eval("xNo")  %>' CssClass="small-font" /><br />
        </ItemTemplate>            
    </asp:Repeater> 
    <%--<br />    
    <b>CANDIDATE/S FOR RETIREMENT</b><br />
    <asp:Repeater runat="server" ID="rRetirement" >
        <ItemTemplate>
            <span class="small-font">&#9658;&nbsp;</span><asp:Label runat="server" ID="lbl" Text='<%# Eval("xDesc") & " " & Eval("xNo")  %>' CssClass="small-font" /><br />
        </ItemTemplate>            
    </asp:Repeater>--%>
    <%--<b>SUMMARY</b><br />
    <asp:Repeater runat="server" ID="rInfo" >
        <ItemTemplate>                    
            <span class="small-font">&#9658;&nbsp;</span><asp:Label runat="server" ID="lbl" Text='<%# Eval("xDesc") & " " & Eval("xNo")  %>' CssClass="small-font" /><br />
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <b>VACANT</b><br />
    <asp:Repeater runat="server" ID="rVacant" >
        <ItemTemplate>                    
            <span class="small-font">&#9658;&nbsp;</span><asp:Label runat="server" ID="lbl" Text='<%# Eval("xDesc") & " " & Eval("xNo")  %>' CssClass="small-font" /><br />
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <b>OCCUPIED</b><br />
    <asp:Repeater runat="server" ID="rOccupied" >
        <ItemTemplate>
            <span class="small-font">&#9658;&nbsp;</span><asp:Label runat="server" ID="lbl" Text='<%# Eval("xDesc") & " " & Eval("xNo")  %>' CssClass="small-font" /><br />
        </ItemTemplate>            
    </asp:Repeater> --%>               
</Header>
<Content>
<div class="page-content-wrap" >         
    <div class="row">
    <br />
    <div class="panel panel-default">                    
        <div class="panel-heading">
            <div class="col-md-2">
                <asp:LinkButton runat="server" ID="lnkGenerate" Text="Generate AMP" CssClass="control-primary" OnClick="lnkGenerate_Click" />
                <uc:ConfirmBox runat="server" ID="cfbGenerate" TargetControlID="lnkGenerate" ConfirmMessage="Vacant position/s will be generated/included in the Annual Manpower Plan."  />
            </div>
        </div>            
        <div class="panel-body">                                
            <div class="row">
                <div class="table-responsive">                    
                    <asp:DataBoundOrganisationChart runat="server" ID="DataBoundOrganisationChart1" ChartItem-AutoGenerateFields="false" EnableSmartDraw="false" ShowFrame="false" NoDataHtml="Organizational chart not available." AssistantItem-AssistantField="IsAssistant">
                        <DetailedTemplate>
                            <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>'>
                                <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />
                                <asp:LinkButton runat="server" ID="LinkButton1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" onmouseover="hoverMenu(this);" />
                            </asp:Panel>                                
                            <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>'>                                    
                                <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />                                                                    
                                <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                
                                <asp:LinkButton runat="server" ID="LinkButton3" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" onmouseover="hoverMenu(this);" />
                                <asp:LinkButton runat="server" ID="lnkName" Text='<%# Container.DataElement("Name") & "<br />" %>' CommandArgument='<%# Container.DataElement("EmployeeNo") %>' OnClick="lnkName_Click" CssClass="small-font" onmouseover="hoverMenu2(this);" ToolTip='<%# Container.DataElement("UniqueID") %>' />                                
                            </asp:Panel>
                            <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("PlantillaGovTypeDesc") %>' CssClass="smaller-font" />
                            <%--<asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />--%>
                        </DetailedTemplate>
                        <AssistantTemplate>
                            <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>'>                                
                                <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                
                                <asp:LinkButton runat="server" ID="LinkButton1" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" onmouseover="hoverMenu(this);" />
                            </asp:Panel>                                
                            <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>'>                                    
                                <asp:Image Width="80" Height="90" runat="server" ID="img" ImageUrl='<%# "frmShowImage.ashx?tNo=" & Container.DataElement("EmployeeNo") & "&tIndex=2" %>' style="float:left; padding:10px" />                                    
                                <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />
                                <asp:LinkButton runat="server" ID="LinkButton4" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" onmouseover="hoverMenu(this);" />
                                <asp:LinkButton runat="server" ID="lnkName" Text='<%# Container.DataElement("Name") & "<br />" %>' CommandArgument='<%# Container.DataElement("EmployeeNo") %>' OnClick="lnkName_Click" CssClass="small-font" onmouseover="hoverMenu2(this);" ToolTip='<%# Container.DataElement("UniqueID") %>' />                                                                    
                            </asp:Panel>
                            <asp:Label runat="server" ID="Label2" Text='<%# Container.DataElement("PlantillaGovTypeDesc") %>' CssClass="smaller-font" />
                            <%--<asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />--%>
                        </AssistantTemplate>
                        <StackTemplate>
                            <asp:Panel runat="server" ID="pWith" Visible='<%# Container.DataElement("without") %>' CssClass="stack">
                                <asp:Label runat="server" ID="Label5" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                    
                                <asp:LinkButton runat="server" ID="LinkButton4" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" onmouseover="hoverMenu(this);" />
                            </asp:Panel>                                
                            <asp:Panel runat="server" ID="pWithout" Visible='<%# Container.DataElement("with") %>' CssClass="stack">                                                                        
                                <asp:Label runat="server" ID="lblPosition" Text='<%# Container.DataElement("Display") %>' CssClass="small-font" />                                    
                                <asp:LinkButton runat="server" ID="LinkButton2" Text='<%# Container.DataElement("UniqueID") & "<br />" %>' CssClass="small-font" onmouseover="hoverMenu(this);" />
                                <asp:LinkButton runat="server" ID="lnkName" Text='<%# Container.DataElement("Name") & "<br />" %>' CommandArgument='<%# Container.DataElement("EmployeeNo") %>' OnClick="lnkName_Click" CssClass="small-font" onmouseover="hoverMenu2(this);" ToolTip='<%# Container.DataElement("UniqueID") %>' />                                                                    
                            </asp:Panel>                                
                            <%--<asp:Label runat="server" ID="Label6" Text='<%# Container.DataElement("PlantillaTypeDesc") %>' CssClass="small-font" />--%>
                        </StackTemplate>
                        <StackItem ShowBackgroundImage="false">                               
                        </StackItem>                                                                               
                    </asp:DataBoundOrganisationChart>
                    <br /><br />
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

    var obj2;
    function hoverMenu2(sender) {
        obj2 = sender;
        var j = Sys.UI.DomElement.getLocation(obj2);
        var offsets = $('#TOcontainer').offset();
        $get("<%=hifPlantillaCode.ClientID %>").value = obj2.title;
        if (obj2.text == "VACANT") {
            $find("hmeOrgMenu2")._onUnhover();
            $find("hmeOrgMenu2")._onHover();
            $find("hmeOrgMenu2")._popupBehavior.set_x((j.x + obj2.offsetWidth) - offsets.left);
            $find("hmeOrgMenu2")._popupBehavior.set_y(j.y);
        }
    }
</script>
</Content>
</uc:Tab>
</div>

<uc:EmpInfo runat="server" ID="EmpInfo1" />
<uc:JobProfile runat="server" ID="JobProfile1" />

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup2" style="display:none;">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>                                            
        <div class="entryPopupDetl2 form-horizontal">                                          
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">TO Title :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">TO Action Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboTableOrgActionTypeNo" DataMember="ETableOrgActionType" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space" runat="server" id="lbldate">Effective Date :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtEffectiveDate" runat="server" CssClass="form-control" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtEffectiveDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtEffectiveDate" Display="Dynamic" />
                </div>
            </div>
                                                
            <div class="form-group">
                <label class="col-md-4 control-label has-required" runat="server" id="lblReportingTo">Reporting To :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtParentPlantillaCode" CssClass="form-control required" ReadOnly="true" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space" runat="server" id="lblPlantillaCode">Plantilla No. :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPlantillaCode" CssClass="number form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Plantilla No. Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPlantillaTypeNo" DataMember="EPlantillaType" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Position :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPositionNo" DataMember="EPosition" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsMirror" Text="&nbsp;Check here if Plantilla No. is binded position" />
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space" runat="server" id="Label1">Mirror Plantilla No. :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPreviousPlantillaCode" CssClass="form-control" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Binded Positions Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPlantillaGovTypeNo" DataMember="EPlantillaGovType" CssClass="form-control" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Binded Positions :</label>
                <div class="col-md-6">
                    <dx:ASPxListBox runat="server" Width="100%" SelectionMode="CheckColumn" ID="ListBox1" />                                            
                </div>
            </div>                                    
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Functional Title :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboTaskNo" DataMember="ETask" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">No. of Box :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtNoOfBox" CssClass="form-control" Enabled="false" Text="1" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Job Level :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboSalaryGradeNo" runat="server" DataMember="ESalaryGrade" CssClass="form-control" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Job Family :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboOccupationalGroupNo" DataMember="EOccupationalGroup" CssClass="form-control" />
                </div>                
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Job Level :</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cboMinSalaryGradeNo" runat="server" DataMember="ESalaryGrade" CssClass="form-control" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="cboMaxSalaryGradeNo" runat="server" DataMember="ESalaryGrade" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Minimum Salary :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMinSalary" runat="server" CssClass="number form-control" Enabled="false" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Median Salary :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMidSalary" runat="server" CssClass="number form-control" Enabled="false" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Maximum Salary :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMaxSalary" runat="server" CssClass="number form-control" Enabled="false" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsManpowerPool" Text="&nbsp;Set Plantilla No. as head of manpower pool."  />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsAssistant" Text="&nbsp;Please check here to tag as Staff" />
                </div>
            </div> 
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Job Grade :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboJobGradeNo" runat="server" DataMember="EJobGrade" CssClass="form-control" />
                </div>
            </div>                                               
            <div class="form-group" visible="false">
                <label class="col-md-4 control-label has-space">Facility :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboFacilityNo" DataMember="EFacility" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsFacHead" Text="&nbsp;Head?" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Division :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboDivisionNo" DataMember="EDivision" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsDivHead" Text="&nbsp;Head?" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Department :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsDepHead" Text="&nbsp;Head?" />
                </div>
            </div>
            <div class="form-group" visible="false">
                <label class="col-md-4 control-label has-space">Section :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboSectionNo" DataMember="ESection" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <asp:CheckBox runat="server" ID="chkIsSecHead" Text="&nbsp;Head?" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Unit :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboUnitNo" DataMember="EUnit" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsUniHead" Text="&nbsp;Head?" />
                </div>
            </div>
            
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Group :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboGroupNo" DataMember="EGroup" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsGroHead" Text="&nbsp;Head?" />
                </div>
            </div>
                                                                                         
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Cost Center :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboCostCenterNo" DataMember="ECostCenter" CssClass="form-control" />
                </div>                
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Location :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboLocationNo" DataMember="ELocation" CssClass="form-control" />
                </div>                
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Area :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboAreaTypeNo" DataMember="EAreaType" CssClass="form-control" />
                </div>                
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Colatilla :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control required" TextMode="MultiLine" Rows="4" />
                </div>                
            </div>
            <br />
        </div>                
    </fieldset>
</asp:Panel>           
</asp:Content>

