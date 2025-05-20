<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpSchool.aspx.vb" Inherits="Secured_EmpSchool" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }
</script>

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
                        <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false"/></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul> 
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                    <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />
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
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="tableNo"
                    OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("tableNo") %>' OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                            <dx:GridViewDataTextColumn FieldName="tableCode" Caption="Code" />                                                                           
                            <dx:GridViewDataTextColumn FieldName="tableDesc" Caption="Description" /> 
                            <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                            <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                            <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                            <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                            <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />  
                            <%--<dx:GridViewDataTextColumn FieldName="PayLocDesc" Caption="Company Name" Visible="false" />--%>
                            <dx:GridViewDataCheckColumn FieldName="IsFixed" Caption="Standard" Visible="false" Width="4%" />
                            <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Visible="false" />
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					            <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                    </dx:ASPxCheckBox>
                                </HeaderTemplate>
				            </dx:GridViewCommandColumn>                          
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                </div>
            </div>                                                           
        </div>                   
    </div>
</div>

<br /><br />
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-7">
                    <div class="row">
                        <h4 class="panel-title">Reference No.: <asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Dropdownlist ID="cboTabDetiNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                        </div>
                    </div>                    
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>                            
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ContentTemplate>                    
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%" KeyFieldName="SchoolDetiNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Detail No." />                            
                            <dx:GridViewDataComboBoxColumn FieldName="SchoolDetiDesc" Caption="Campus / Branch" />                            
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" Width="5%" /> 
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>


<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none" >
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="btnSave" 
            OnClick="btnSave_Click" CssClass="fa fa-floppy-o submit btnSave fsMain" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTableCode" runat="server" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTableDesc" runat="server" CssClass="form-control required" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference Document :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDocRef" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Effective Date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEffectiveDate" runat="server" CssClass="form-control" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtEffectiveDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtEffectiveDate" Display="Dynamic" />
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                </div>
           </div>
           <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">
                Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div>                      
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                </div>
            </div>                                   
        </div>                    
    </fieldset>
</asp:Panel>  

<asp:Button ID="btnShowD" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShowDetl" runat="server" TargetControlID="btnShowD" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsDetl btnSaveDetl" OnClick="btnSaveDetl_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDeti" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>                        

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Campus / Branch :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtSchoolDetiDesc" runat="server" CssClass="required form-control" />                    
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtRemarksDeti" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchivedDeti" Text="&nbsp;Archive" />
                </div>
            </div>

            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
      
</asp:Content>
