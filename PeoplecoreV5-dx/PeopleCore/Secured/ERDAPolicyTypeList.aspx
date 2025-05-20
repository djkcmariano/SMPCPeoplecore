<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="ERDAPolicyTypeList.aspx.vb" Inherits="Secured_ERDAPolicyTypeList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
 <script type="text/javascript">
     function cbCheckAll_CheckedChanged(s, e) {
         grdMain.PerformCallback(s.GetChecked().toString());
     }

    </script>
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <%--<asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkGo_Click" style="width:200px;" CssClass="form-control" runat="server" 
                                ></asp:Dropdownlist>--%>
                                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkGo_Click" CssClass="form-control" runat="server" />

                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                    
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul> 
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DAPolicyTypeNo"
                            OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                            
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="DAPolicyTypeCode" Caption="Code" />
                                    <dx:GridViewDataTextColumn FieldName="DAPolicyTypeDesc" Caption="Description" />
                                    <dx:GridViewDataCheckColumn FieldName="NoOfMonth" Caption="Month(s) of Prescription" Visible="false" />                                                                                                              
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder"/>                       
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
</div>
   
    <asp:Button ID="btnShowMain" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlpopupMain"
        CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlpopupMain" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDAPolicyTypeNo" ReadOnly="true" runat="server" Enabled="false" CssClass="form-control" Placeholder = "Autonumber"></asp:Textbox>
                </div>
            </div> 


           <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" Enabled="false" CssClass="form-control" Placeholder="Autonumber" ></asp:Textbox>
                    </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDAPolicyTypeCode" runat="server" CssClass="required form-control" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDAPolicyTypeDesc" runat="server" CssClass="required form-control" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Order No. :</label>
                <div class="col-md-3">
                       <asp:Textbox ID="txtOrderLevel" runat="server" CssClass="form-control" ></asp:Textbox>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtOrderLevel" />
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Months of Prescription :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtNoOfMonth" runat="server" CssClass="number form-control" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label has-space">Entire employment? :</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkIsEntireEmployment" runat="server" />
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
             </div>  
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="chkIsOnline" runat="server" Text="&nbsp;Tick if online" />                                                
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="chkIsArchived" runat="server" Text="&nbsp;Archive" />                                                
                 </div>
            </div>
                      
         </div>
          <!-- Footer here -->
         
    </fieldset>
</asp:Panel> 
  
</asp:Content>
