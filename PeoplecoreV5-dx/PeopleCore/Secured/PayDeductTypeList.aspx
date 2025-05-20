<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayDeductTypeList.aspx.vb" Inherits="Secured_PayDeductTypeList" %>



<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible ="false"/></li>
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayDeductTypeNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="PayDeductTypeCode" Caption="Code" />
                                    <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Description" />
                                    <dx:GridViewDataCheckColumn FieldName="IsTaxExempt" Caption="Tax Exempt" ReadOnly="true" />
                                    <dx:GridViewDataCheckColumn FieldName="IsLoan" Caption="Loan" ReadOnly="true" />
                                    <dx:GridViewDataCheckColumn FieldName="IsForPF" Caption="Provident Fund" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsOnline" Caption="Online" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsNotForhomepay" Caption="Exclude from Retention" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DeduOrder" Caption="Order No." />
                                    <dx:GridViewDataTextColumn FieldName="EntityCode" Caption="Entity Code" />
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
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPaydeducttypeNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDeductTypeCode" runat="server"  CssClass="required form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDeductTypeDesc" runat="server"  CssClass="required form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Entity Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEntityCode" runat="server" CssClass="required form-control" ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Order No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDeduOrder" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtDeduOrder" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsnotforHomepay"  runat="server" Text="&nbsp;Tick to exclude from retention"/>
                </div>
            </div>
            
            <div id="Div1" class="form-group" runat="server" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsForPF"  runat="server" Text="&nbsp;Tick here for provident fund"/>
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsOnline"  runat="server" Text="&nbsp;Tick to view online"/>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-8">
                    <asp:CheckBox ID="txtIsTaxExempt"  runat="server" Text="&nbsp;Tick if tax exempt" AutoPostBack="true" OnCheckedChanged="txtIsExempts_OnCheckedChanged" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Type of Tax Exempt :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboTaxExemptNo" CssClass="form-control">
                        <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                        <asp:ListItem Value="1" Text="SSS" />
                        <asp:ListItem Value="2" Text="PHIC" />
                        <asp:ListItem Value="3" Text="Pag-ibig" />
                        <asp:ListItem Value="4" Text="Union Dues" />
                        <asp:ListItem Value="5" Text="Absent, Late & Undertime" />
                        <asp:ListItem Value="6" Text="PF" />
                    </asp:DropDownList>
                </div>
            </div>
            

            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsLoan"  runat="server" Text="&nbsp;Tick if loan" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged"/>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Type of Loan :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboLoanTypeNo" CssClass="form-control">
                        <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                        <asp:ListItem Value="1" Text="SSS Salary Loan" />
                        <asp:ListItem Value="2" Text="SSS Calamity Loan" />
                        <asp:ListItem Value="3" Text="HDMF Salary Loan" />
                        <asp:ListItem Value="4" Text="HDMF Calamity Loan" />
                        <asp:ListItem Value="5" Text="Other Loan" />
                    </asp:DropDownList>
                </div>
            </div>
            
            <div class="form-group" style="display:none";>
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
        </div>
        
        <br />
         </fieldset>
</asp:Panel>
</asp:Content>
