<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayIncomeTypeList.aspx.vb" Inherits="Secured_PayIncomeTypeList" %>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function disableenable(chk,index) {
        if (chk.checked && index==0) {
            document.getElementById("ctl00_cphBody_txtthreshold").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsChargeToAccum").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsChargeToTaxable").disabled = false;
        } else if (chk.checked && index == 1) {
            document.getElementById("ctl00_cphBody_txtthreshold").disabled = true;
            document.getElementById("ctl00_cphBody_txtthreshold").value = "";
            document.getElementById("ctl00_cphBody_txtIsChargeToAccum").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsChargeToTaxable").disabled = true;
        } else {
            document.getElementById("ctl00_cphBody_txtthreshold").disabled = true;
            document.getElementById("ctl00_cphBody_txtthreshold").value = "";
            document.getElementById("ctl00_cphBody_txtIsChargeToAccum").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsChargeToTaxable").disabled = true;
        };
    };


</script>

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                       <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control"  runat="server" />             
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>                                      
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayIncomeTypeNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="PayIncomeTypeCode" Caption="Code" />
                                    <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Description" />
                                    <dx:GridViewDataCheckColumn FieldName="IsBasic" Caption="Basic" ReadOnly="true" />
                                    <dx:GridViewDataCheckColumn FieldName="IsTaxable" Caption="Taxable" ReadOnly="true" />
                                    <dx:GridViewDataCheckColumn FieldName="IsAccum" Caption="Accum" ReadOnly="true" />
                                    <dx:GridViewDataCheckColumn FieldName="IsFBT" Caption="FBT" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsNonTaxable" Caption="Non-Taxable" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsDiminimis" Caption="Deminimis" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsPaidbyER" Caption="Tax Paid by ER" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsAddTakehomepay" Caption="Exclude from Retention" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsAllowance" Caption="Allowance" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="IncoOrder" Caption="Order No." />
                                    <dx:GridViewDataTextColumn FieldName="EntityCode" Caption="Entity Code" />
                                    <dx:GridViewDataTextColumn FieldName="PaySchedDesc" Caption="Credit Schedule" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                    <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />     
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
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
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayIncomeTypeNo" ReadOnly="true" runat="server"  CssClass="form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server"  CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayIncomeTypeCode" runat="server" CssClass="required form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayIncomeTypeDesc" runat="server" CssClass="required form-control"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Entity Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEntityCode" runat="server" CssClass="form-control required" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Order No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtIncoOrder" runat="server" CssClass="number form-control" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtIncoOrder" />
               </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsBasic" runat="server" Text="&nbsp;Tick if basic income" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsAllowance" runat="server" Text="&nbsp;Tick if allowance" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIspaidByER"  runat="server" Text="&nbsp;Tick if tax is paid by the employer"/>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-8">
                    <asp:CheckBox ID="txtIsAddTakehomepay"  runat="server" Text="&nbsp;Tick to exclude income from retention" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-8">
                    <asp:CheckBox ID="txtIsExcludeContri"  runat="server" Text="&nbsp;Tick to exclude income from SSS premium computation" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-8">
                    <asp:CheckBox ID="txtIsUnreg"  runat="server" Text="&nbsp;Tick to unregister" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Type of Compensation :</label>
                <div class="col-md-7">
                    <asp:RadioButton ID="txtIsNonTaxable" GroupName="IncomeType" Text="&nbsp; Non-Taxable" runat="server" onclick="disableenable(this,4)"/><br />
                    <asp:RadioButton ID="txtIsTaxable" GroupName="IncomeType" Text="&nbsp; Taxable" runat="server" onclick="disableenable(this,3)" /><br />
                    <asp:RadioButton ID="txtIsAccum" GroupName="IncomeType" Text="&nbsp; Accumulated" onclick="disableenable(this,1)" runat="server" /><br />
                    <asp:RadioButton ID="txtIsFBT" GroupName="IncomeType" Text="&nbsp; Fringe Benefits Tax (FBT)" runat="server" onclick="disableenable(this,2)" /><br />
                    <asp:RadioButton ID="txtIsDiminimis" GroupName="IncomeType"  Text="&nbsp; Deminimis" runat="server" onclick="disableenable(this,0)"/>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Deminimis Threshold (Annual) :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtthreshold" runat="server" CssClass="form-control" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtthreshold" />
                    <asp:RangeValidator runat="server" ID="rv1" ControlToValidate="txtthreshold" MinimumValue="" MaximumValue="99999999" Type="Double" ErrorMessage="Value is out of Range." SetFocusOnError="true"></asp:RangeValidator>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-8">
                    <asp:Radiobutton ID="txtIsChargeToAccum" GroupName="grpDeminimis"  runat="server" Text="&nbsp;Excess amount from threshold will forwarded to accumulated income" /><br />
                    <asp:Radiobutton ID="txtIsChargeToTaxable" GroupName="grpDeminimis"  runat="server" Text="&nbsp;Excess amount from threshold will forwarded to taxable income" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Threshold (Per-payroll) :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtThresholdMonthly" runat="server" CssClass="form-control" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtThresholdMonthly" />
                    <asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="txtThresholdMonthly" MinimumValue="" MaximumValue="99999999" Type="Double" ErrorMessage="Value is out of Range." SetFocusOnError="true"></asp:RangeValidator>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Threshold charged to Income Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboChargeToIncomeTypeNo" runat="server" CssClass=" number form-control" DataMember="EPayIncomeType" >
                        </asp:Dropdownlist>
                    </div>
             </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Pay Credit Schedule :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPaySchedNo" runat="server" CssClass=" number form-control" DataMember="EPaySched" >
                        </asp:Dropdownlist>
                    </div>
             </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Company Name :</label>
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
             <br />
             <br />

        </div>
        

         </fieldset>
</asp:Panel>
</asp:content>