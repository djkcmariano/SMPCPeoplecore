<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSProjectList_Class.aspx.vb" Inherits="Secured_BSProjectList_Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab" HeaderVisible="true">
<Content>
<br />
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <h3>&nbsp;</h3>
                </div>
                <div>
                    &nbsp;                  
                </div> 
                <div>                                                
                    <ul class="panel-controls"> 
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>                                                                                                                                                     
                </div>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSProjectClassNo">                                                                                   
                        <Columns>    
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BSProjectClassNo") %>' OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                                                    
                            <dx:GridViewDataTextColumn FieldName="PayClassCode" Caption="Payroll Group Code" />
                            <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Billing/Payroll Group"/>
                            <dx:GridViewDataTextColumn FieldName="Discount" Caption="Miscelaneous" />                                                        

                                                    
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Rate Sheet" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                                        
                            <dx:GridViewDataTextColumn FieldName="EncodeNo" Caption="Cost Center" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="PayClassNo" Caption="PayClass Id" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="ProjectNo" Caption="Project Id" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />                      
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                    <div class="panel-title">
                        <asp:Label runat="server" ID="lbl" />
                    </div>
                </div>
                <div>                    
                    <ul class="panel-controls">                            
                        <li><asp:LinkButton runat="server" ID="lnkAddD" OnClick="lnkAddD_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDeletD" OnClick="lnkDeleteD_Click" Text="Delete" CssClass="control-primary" /></li>
                    </ul>                       
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                 
                </div> 
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="BSProjectClassRateNo" Width="100%">
                        <Columns>      
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BSProjectClassRateNo") %>' OnClick="lnkEditD_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                      
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                            <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />
                            <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" /> 
                             
                            <dx:GridViewBandColumn Caption="Employee Rate" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Monthly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="CurrentSalaryD" Caption="Daily" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="CurrentSalaryH" Caption="Hourly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </dx:GridViewBandColumn> 
                            <dx:GridViewBandColumn Caption="Billing Rate" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="BillingRate" Caption="Monthly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="BillingRateD" Caption="Daily" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="BillingRateH" Caption="Hourly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </dx:GridViewBandColumn>  
                            <dx:GridViewBandColumn Caption="OT Rate" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="OTRate" Caption="Monthly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="OTRateD" Caption="Daily" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="OTRateH" Caption="Hourly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Rate Sheet Detail" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetailsR" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetailsR_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>   
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>
<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlDetl" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupDetl" TargetControlID="btnShowDetl" />
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>
                
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Billing/Payroll Group :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayClassNo" runat="server" CssClass="form-control" DataMember="EPayClass" />                    
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Billing Formula Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboBSProjectPayTypeNo" runat="server" CssClass="form-control" DataMember="BBSPRojectPayType" />                    
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Miscellaneous :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtDiscount" />
                </div>
            </div>
                           
        </div>
        <br />
        </fieldset>
    </asp:Panel>

<asp:Button ID="btnShowRate" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlRate" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose" PopupControlID="pnlPopupRate" TargetControlID="btnShowRate" />
<asp:Panel id="pnlPopupRate" runat="server" CssClass="entryPopup2" style="display:none;">
       <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsDetl lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtBSProjectClassRateNo" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
           </div>  
           <div class="form-group">
                <label class="col-md-4 control-label has-space">Position :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPositionNo" runat="server" CssClass="form-control" DataMember="EPosition" />                    
                </div>
            </div>  
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Department :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboDepartmentNo" runat="server" CssClass="form-control" DataMember="EDepartment" />                    
                </div>
            </div>   
             
            <div class="form-group">
                <label class="col-md-4 control-label has-space">EMPLOYEE RATE :</label>
                <div class="col-md-7">
                    
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Monthly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCurrentSalary" runat="server" CssClass="form-control number" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Daily :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCurrentSalaryD" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Hourly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCurrentSalaryH" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">BILLING RATE :</label>
                <div class="col-md-7">
                    
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Monthly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBillingRate" runat="server" CssClass="form-control number" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Daily :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBillingRateD" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Hourly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBillingRateH" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">OT RATE :</label>
                <div class="col-md-7">
                    
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Monthly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOTRate" runat="server" CssClass="form-control number" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Daily :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOTRateD" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Hourly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOTRateH" runat="server" CssClass="form-control number" />
                </div>
            </div>
            
                                         
            <br /><br />
        </div>        
        </fieldset>
</asp:Panel>

</Content>
</uc:Tab>
<br /><br />
</asp:Content>