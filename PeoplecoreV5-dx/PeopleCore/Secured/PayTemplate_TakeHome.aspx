<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayTemplate_TakeHome.aspx.vb" Inherits="Secured_PayTemplate_TakeHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" menustyle="TabRef" ID="Tab">
    <Content>
    <br />
    <div class="page-content-wrap">            
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        &nbsp;
                    </div>
                    <div>                                  
                        <ul class="panel-controls">
                       
                        </ul>

                    
                    </div>                                                                                                   
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayClassNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                                
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="PayClassCode" Caption="Code" />                                                                           
                                    <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Description" />  
                                    <dx:GridViewDataTextColumn FieldName="MaxAmtAccumulatedExemp" Caption="Accumulated Exemption" />                               
                                    <dx:GridViewBandColumn Caption="Take Home Pay" HeaderStyle-HorizontalAlign="Center">
                                        <Columns> 
                                            <dx:GridViewDataTextColumn FieldName="MinNetPayForDedu" Caption="Min. Take Home Pay" />  
                                            <dx:GridViewDataCheckColumn FieldName="IsMinNetPayInPercent" Caption="Percentage Base"/> 
                                         </Columns>
                                    </dx:GridViewBandColumn> 
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />                            
                                </Columns>                            
                            </dx:ASPxGridView>                        
                        </div>
                    </div>                                                           
                </div>                   
            </div>
        </div>
    </div> 
        </Content>
</uc:Tab>  
<br /><br />

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPayClassCode" runat="server" Enabled="false" CssClass="form-control required" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-3 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPayClassDesc" runat="server" Enabled="false" CssClass="form-control required" />
                </div>
            </div>
            
            <br />
           <h5><b>CURRENCY</b></h5>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Currency :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboCurrencyNo" DataMember="ECurrency" runat="server" CssClass="form-control" />
                </div>                
            </div>

            <h5><b>TAKE HOME PAY</b></h5>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Min. Take Home Pay :</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtMinNetPayForDedu" runat="server" CssClass="form-control number" />               
                </div>
                <div class="col-md-3">
                    <asp:CheckBox runat="server" ID="chkIsMinNetPayInPercent" Text="&nbsp;Tick if mininum take home pay is percentage base" />
                </div>                
            </div>
            
             <h5><b>ACCUMULATED INCOME</b></h5>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Max. Accumulated Exemption :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtMaxAmtAccumulatedExemp" runat="server" CssClass="form-control number" />               
                </div>                
            </div>
            <h5><b>TAX BASIS</b></h5>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">&nbsp;</label>
                <div class="col-md-5">
                    <asp:Radiobutton runat="server" ID="chkIsMonthlyTax" GroupName="grpTax" Text="&nbsp;&nbsp;Tick if tax procedure is on monthly basis" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-3 control-label has-space">&nbsp;</label>
                <div class="col-md-5">
                    <asp:Radiobutton runat="server" ID="chkIsAnnualizedTax" GroupName="grpTax" Text="&nbsp;&nbsp;Annualize Tax (Include MWE Income) " />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-3 control-label has-space">&nbsp;</label>
                <div class="col-md-5">
                    <asp:Radiobutton runat="server" ID="chkIsProrateTax" GroupName="grpTax" Text="&nbsp;&nbsp;Annualize Tax (Exclude MWE Income) " />
                </div>
            </div>    
                           
            <br /><br />
            <br /><br />

        </div>                    
    </fieldset>
</asp:Panel>

</asp:Content>

