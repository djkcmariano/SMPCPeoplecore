<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpPlantillaList.aspx.vb" Inherits="Secured_EmpPlantillaList" Theme="PCoreStyle" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<br />
<div class="page-content-wrap">
                 
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                    
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />                
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">                            
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkFilter" OnClick="lnkFilter_Click" Text="Org Chart" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                    </asp:UpdatePanel> 
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PlantillaNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("PlantillaNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="PlantillaCode" Caption="Plantilla No." />                                
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />
                                <dx:GridViewDataTextColumn FieldName="Fullname" Caption="Name of Incumbent" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="ParentPlantillaCode" Caption="Reporting To" />
                                <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Sector" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" />
                                <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" />                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="History" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkHistory" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkHistory_Click" OnPreRender="lnk_PreRender" CommandArgument='<%# Bind("PlantillaCode") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Position<br/>Chart" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPositionChart" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkPositionChart_Click" OnPreRender="lnk_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Hierarchy<br/>Chart" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkHierarchyChart" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkHierarchyChart_Click" OnPreRender="lnk_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="J D" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-print" Font-Size="Medium" OnClick="lnkJD_Click" OnPreRender="lnk_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6 panel-title">
                        <asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="HRANNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataDateColumn FieldName="Effectivity" Caption="Effectivity" />
                                    <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                                </Columns>                            
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>            
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
            <asp:Linkbutton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" OnClick="lnkSave_Click" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Title :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space" runat="server" id="lblReportingTo">Reporting To :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtParentPlantillaCode" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required" runat="server" id="lblPlantillaCode">Plantilla No. :</label>
                <div class="col-md-6">
                    <asp:HiddenField runat="server" ID="hifPlantillaNo" />
                    <asp:TextBox runat="server" ID="txtPlantillaCode" CssClass="number form-control required" AutoPostBack="true" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Plantilla No. Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPlantillaTypeNo" DataMember="EPlantillaType" CssClass="form-control" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Position :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPositionNo" DataMember="EPosition" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsMirror" Text="&nbsp;Tick if Plantilla No. is a binded position" OnCheckedChanged="chkIsMirror_CheckedChanged" AutoPostBack="true" />
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space" runat="server" id="Label1">Mirror Plantilla No. :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPreviousPlantillaCode" CssClass="form-control" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space" runat="server" id="lblbptype">Binded Positions Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPlantillaGovTypeNo" DataMember="EPlantillaGovType" CssClass="form-control" Enabled="false" />
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
                <label class="col-md-4 control-label has-required">Job Level :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboSalaryGradeNo" runat="server" DataMember="ESalaryGrade" CssClass="form-control required" />
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
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Minimum Salary :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMinSalary" runat="server" CssClass="number form-control" Enabled="false" />
                </div>
            </div> 
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Median Salary :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMidSalary" runat="server" CssClass="number form-control" Enabled="false" />
                </div>
            </div> 
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Maximum Salary :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMaxSalary" runat="server" CssClass="number form-control" Enabled="false" />
                </div>
            </div>

            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Job Grade :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboJobGradeNo" runat="server" DataMember="EJobGrade" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsManpowerPool" Text="&nbsp;Tick to set Plantilla No. as head of manpower pool."  />
                </div>                
            </div>                       
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsAssistant" Text="&nbsp;Tick to tag as Staff" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsHeadStaff" Text="&nbsp;Tick to tag direct report" />
                </div>
            </div> 
            <div class="form-group" visible="false">
                <label class="col-md-4 control-label has-space">Facility :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboFacilityNo" DataMember="EFacility" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsFacHead" Text="&nbsp;Head?" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Unit :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboUnitNo" DataMember="EUnit" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsUniHead" Text="&nbsp;Head?" />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Department :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsDepHead" Text="&nbsp;Head?" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Group :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboGroupNo" DataMember="EGroup" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsGroHead" Text="&nbsp;Head?" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Division :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboDivisionNo" DataMember="EDivision" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsDivHead" Text="&nbsp;Head?" />
                </div>
            </div>
            <div class="form-group" visible="false">
                <label class="col-md-4 control-label has-space">Section/Unit :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboSectionNo" DataMember="ESection" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsSecHead" Text="&nbsp;Head?" />
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

            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Area :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboAreaTypeNo" DataMember="EAreaType" CssClass="form-control" />
                </div>                
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                </div>                
            </div>
            <br />
            <br />            
        </div>                    
    </fieldset>
</asp:Panel>
<asp:UpdatePanel runat="server" ID="UpdatePanel1">
<ContentTemplate>
<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel2" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="Fieldset1">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkGenerate" CssClass="fa fa-sitemap" ToolTip="Generate" OnClick="lnkGenerate_Click" />
        </div>                                            
        <div class="entryPopupDetl2 form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label">Filter By :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboFilterBy1"  CssClass="form-control" OnSelectedIndexChanged="cboFilterBy_SelectedIndexChanged" AutoPostBack="true" />
                </div>                
            </div>
		    <div class="form-group">                                
                <label class="col-md-4 control-label">Filter Value :</label> 
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboFilterValue1" CssClass="form-control" />
                </div>                                                
            </div>
             <%--<div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Display :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboView"  CssClass="form-control">
                        <asp:ListItem Text="W / O Incumbent" Value="0" Selected="True"  /> 
                        <asp:ListItem Text="W / Incumbent" Value="1" />                                               
                    </asp:DropDownList>
                </div>
            </div>--%>
            <br />
        </div>                    
    </fieldset>
</asp:Panel>


<asp:Button ID="Button3" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button3" PopupControlID="Panel3" CancelControlID="Linkbutton2" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel3" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="Fieldset2">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="Linkbutton2" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="Linkbutton3" CssClass="fa fa-sitemap" ToolTip="Generate" OnClick="lnkGenerate_Click" />
        </div>                                            
        <div class="entryPopupDetl2 form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label">Plantilla No./Title :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPlantillaCodeView" CssClass="form-control" onblur="ResetPlantilla()" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifPlantillaCode"/>
                    <ajaxToolkit:AutoCompleteExtender ID="acePlantilla" runat="server"  
                        TargetControlID="txtPlantillaCodeView" MinimumPrefixLength="2"
                        CompletionInterval="500" ServiceMethod="PopulatePlantilla" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" />
                        <script type="text/javascript">

                            function ResetPlantilla() {
                                if (document.getElementById('<%= hifPlantillaCode.ClientID %>').value == "") {
                                }
                            }

                            function getRecord(source, eventArgs) {
                                document.getElementById('<%= hifPlantillaCode.ClientID %>').value = eventArgs.get_value();
                            }                               	

                        </script>
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Display :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboView"  CssClass="form-control">
                        <asp:ListItem Text="W/O Incumbent" Value="0"  /> 
                        <asp:ListItem Text="With Incumbent" Value="1" />                                               
                    </asp:DropDownList>
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Level :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboLevelNo" CssClass="form-control" />
                </div>                
            </div>
            <%--<div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Plantilla No. Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPlantillaTypeNo" CssClass="form-control">
                        <asp:ListItem Value="0" Text="PERMANENT/TEMPORARY" />
                        <asp:ListItem Value="1" Text="POOL" />
                    </asp:DropDownList>
                </div>                
            </div>--%>            
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkStack" Text="&nbsp;Stack last level" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsMirrorView" Text="&nbsp;Include Mirror" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsBridge" Text="&nbsp;Include Bridge" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsCluster" Text="&nbsp;Include Cluster" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsBudgetSource" Text="&nbsp;Include Budget Source" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsReassignment" Text="&nbsp;Include Reassignment" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Staff Color :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboStaffColor"  CssClass="form-control" />
                </div>
            </div>
            <br />
        </div>                    
    </fieldset>
</asp:Panel>

</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkGenerate" />
    <asp:PostBackTrigger ControlID="LinkButton3" />
</Triggers>
</asp:UpdatePanel>    
</asp:Content>

