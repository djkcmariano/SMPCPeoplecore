<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="ERDAPolicyList.aspx.vb" Inherits="Secured_ERDAPolicyList" %>

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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DAPolicyNo"
                            OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="DAPolicyCode" Caption="Code" />                                                                           
                                    <dx:GridViewDataTextColumn FieldName="DAPolicyDesc" Caption="Description" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DAPolicyTypeDesc" Caption="Nature of Offenses" /> 
                                    <dx:GridViewDataComboBoxColumn FieldName="DACaseTypeDesc" Caption="Case Type" /> 
                                    <dx:GridViewDataTextColumn FieldName="O1DATypeDesc" Caption="1st Offense" />                    
                                    <dx:GridViewDataTextColumn FieldName="O2DATypeDesc" Caption="2nd Offense" />                                                                
                                    <dx:GridViewDataTextColumn FieldName="O3DATypeDesc" Caption="3rd Offense" /> 
                                    <dx:GridViewDataTextColumn FieldName="O4DATypeDesc" Caption="4th Offense" Visible="false" /> 
                                    <dx:GridViewDataTextColumn FieldName="O5DATypeDesc" Caption="5th Offense" Visible="false" /> 
                                    <dx:GridViewDataTextColumn FieldName="O6DATypeDesc" Caption="6th Offense" Visible="false" /> 
                                    <dx:GridViewDataTextColumn FieldName="NoofDaysReset" Caption="No. Of Days Reset" Visible="false" /> 
                                    <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order No." Visible="false" />                                                                                                                                                        
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
    <asp:Panel id="pnlpopupMain" runat="server" CssClass="entryPopup" style="display:none" >
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
                    <asp:Textbox ID="txtDAPolicyNo" ReadOnly="true" runat="server" Enabled="false" CssClass="form-control" ></asp:Textbox>
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
                        <asp:Textbox ID="txtDAPolicyCode" runat="server" CssClass="required form-control" ></asp:Textbox>
                    </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                        <asp:Textbox ID="txtDAPolicyDesc" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control" ></asp:Textbox>
                    </div>
            </div> 
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Nature of Offenses :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboDAPolicyTypeNo" runat="server" DataMember="EDAPolicyType" CssClass="form-control required" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Case Type :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboDACaseTypeNo" DataMember="EDACaseType" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                    </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">1st Offense :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboO1DATypeNo" runat="server" DataMember="EDAType" CssClass="form-control" ></asp:Dropdownlist>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">2nd Offense :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboO2DATypeNo" runat="server" DataMember="EDAType" CssClass="form-control" ></asp:Dropdownlist>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">3rd Offense :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboO3DATypeNo" runat="server" DataMember="EDAType" CssClass="form-control" ></asp:Dropdownlist>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">4th Offense :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboO4DATypeNo" runat="server" DataMember="EDAType" CssClass="form-control" ></asp:Dropdownlist>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">5th Offense :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboO5DATypeNo" runat="server" DataMember="EDAType" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">6th Offense</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboO6DATypeNo" runat="server" DataMember="EDAType" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">7th Offense :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboO7DATypeNo" runat="server" DataMember="EDAType" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">8th or More Offense :</label>
                <div class="col-md-7">
                        <asp:Dropdownlist ID="cboO8DATypeNo" runat="server" DataMember="EDAType" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Order No. :</label>
                <div class="col-md-7">
                       <asp:Textbox ID="txtOrderLevel" runat="server" CssClass="form-control" ></asp:Textbox>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtOrderLevel" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Offense will reset from :</label>
                <div class="col-md-7">
                     <asp:Dropdownlist ID="cboDAResetTypeNo" runat="server" DataMember="EDAResetType" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date Interval :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboDateIntervalNo" CssClass="form-control">
                        <asp:ListItem Text="-- Select --" Value="" />
                        <asp:ListItem Text="Day" Value="1" />
                        <asp:ListItem Text="Month" Value="2" />
                    </asp:DropDownList>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">No. of days/months where the offense count will reset to zero :</label>
                <div class="col-md-3">
                       <asp:Textbox ID="txtNoOfDaysReset" runat="server" CssClass="form-control" ></asp:Textbox>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtNoOfDaysReset" />
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
            

            <br />
         </div>
         
          <!-- Footer here -->
         
    </fieldset>
</asp:Panel> 
  
</asp:Content>
