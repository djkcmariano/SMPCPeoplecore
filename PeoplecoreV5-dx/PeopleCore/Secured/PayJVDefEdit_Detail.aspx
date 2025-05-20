<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayJVDefEdit_Detail.aspx.vb" Inherits="Secured_PayJVDefEdit_Detail" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Panel id="Panel2" runat="server">
                <fieldset class="form" id="fsHeader">
                    <div  class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12">
                                <label style="font-size:smaller;">Trans. No. :</label>&nbsp;
                                <asp:Label runat="server" ID="lblCode" CssClass="control-label" Font-Size="Smaller" />
                            </div>
                            <div class="col-md-12">
                                <label style="font-size:smaller;">Description :</label>&nbsp;
                                <asp:Label runat="server" ID="lblDescription" CssClass="control-label" Font-Size="Smaller" />
                            </div>
                            <div class="col-md-12">
                                <label style="font-size:smaller;">Debit/Credit :</label>&nbsp;
                                <asp:Label runat="server" ID="lbldebit_credit" CssClass="control-label" Font-Size="Smaller" />
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

        </Header>        
        <Content>
        <br />
        <div class="page-content-wrap">         
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-5">
                                <h4 class="panel-title">Income Type</h4>
                            </div>
                            <div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <ul class="panel-controls">
                                            <li><asp:LinkButton runat="server" ID="lnkAddIncome" OnClick="lnkAddIncome_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                            <li><asp:LinkButton runat="server" ID="lnkDeleteIncome" OnClick="lnkDeleteIncome_Click" Text="Delete" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkExportIncome" OnClick="lnkExportIncome_Click" Text="Export" CssClass="control-primary" /></li>
                                        </ul>
                                        <uc:ConfirmBox runat="server" ID="cfbDeleteIncome" TargetControlID="lnkDeleteIncome" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lnkExportIncome" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>   
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                   <dx:ASPxGridView ID="grdIncome" ClientInstanceName="grdIncome" runat="server" KeyFieldName="JVDefDetiNo" Width="100%">                                                                                   
                                        <Columns>                            
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditIncome_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                            <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
                                        </Columns> 
                                        <SettingsPager Mode="ShowAllRecords" />                       
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExportIncome" runat="server" GridViewID="grdIncome" />    
                                </div>
                            </div>
                        </div>
                    </div> 
                </div>

                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-5">
                                <h4 class="panel-title">Deduction Type</h4>
                            </div>
                            <div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <ul class="panel-controls">
                                            <li><asp:LinkButton runat="server" ID="lnkAddDeduct" OnClick="lnkAddDeduct_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                            <li><asp:LinkButton runat="server" ID="lnkDeleteDeduct" OnClick="lnkDeleteDeduct_Click" Text="Delete" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkExportDeduct" OnClick="lnkExportDeduct_Click" Text="Export" CssClass="control-primary" /></li>
                                        </ul>
                                        <uc:ConfirmBox runat="server" ID="cfbDeleteDeduct" TargetControlID="lnkDeleteDeduct" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lnkExportDeduct" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>  
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                   <dx:ASPxGridView ID="grdDeduct" ClientInstanceName="grdDeduct" runat="server" KeyFieldName="JVDefDetiNo" Width="100%">                                                                                   
                                        <Columns>                            
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDeduct_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                            <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Deduction Type" />
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
                                        </Columns>
                                        <SettingsPager Mode="ShowAllRecords" />                             
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExportDeduct" runat="server" GridViewID="grdDeduct" />    
                                </div>
                            </div>
                        </div>
                    </div> 
                </div>


            </div>
        </div>                                
    </Content>        
    </uc:Tab>

    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">    
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Detail No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtJVDefDetiNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Detail No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        

                <div class="form-group" style="display:none;" id="divincome" runat="server">
                    <label class="col-md-4 control-label has-required">Income Type :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboPayIncomeTypeNo" CssClass="form-control" runat="server"></asp:DropdownList>
                    </div>
                </div>  

                <div class="form-group" style="display:none;" id="divdeduct" runat="server">
                    <label class="col-md-4 control-label has-required">Deduction Type :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboPayDeductTypeNo" CssClass="form-control" runat="server"></asp:DropdownList>
                    </div>
                </div>

                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

