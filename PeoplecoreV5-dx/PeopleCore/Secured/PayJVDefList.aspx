<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayJVDefList.aspx.vb" Inherits="Secured_PayJVDefList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<%--<uc:JVTab runat="server" ID="JVTab" />--%>
<uc:FormTab runat="server" ID="FormTab" />

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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="JVDefNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="AccntCode" Caption="Account Code" />
                                    <dx:GridViewDataTextColumn FieldName="AccntDesc" Caption="Account Description" />
                                    <dx:GridViewDataCheckColumn FieldName="IsFixed" Caption="Fixed" />
                                    <dx:GridViewDataCheckColumn FieldName="IsEmployee" Caption="Per Employee" />
                                    <dx:GridViewDataTextColumn FieldName="DRCRDesc" Caption="Debit/Credit" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />
                                    <dx:GridViewDataComboBoxColumn FieldName="JobGradeDesc" Caption="Jobgrade" />
                                    <dx:GridViewDataTextColumn FieldName="GroupByDesc" Caption="Group By" />
                                    <dx:GridViewDataColumn Caption="Details"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list control-primary" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
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


 <div class="page-content-wrap">         
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">

                <div class="panel-heading">
                    <h4 class="panel-title">Reference No. : &nbsp;<asp:Label ID="lblIncome" runat="server"></asp:Label></h4>
                </div>
                <div class="panel-body">
                    <br />
                    <div class="page-content-wrap">         
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="col-md-6">
                                                
                                            </div>
                                            <div>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                    <ContentTemplate>
                                                        <ul class="panel-controls">
                                                            <li><asp:LinkButton runat="server" ID="lnkAddIncome" OnClick="lnkAddIncome_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                                            <li><asp:LinkButton runat="server" ID="lnkDeleteIncome" OnClick="lnkDeleteIncome_Click" Text="Delete" CssClass="control-primary" /></li>
                                                            <li><asp:LinkButton runat="server" ID="lnkExportIncome" OnClick="lnkExportIncome_Click" Text="Export" CssClass="control-primary" Visible="false" /></li>
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
                                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                                                <DataItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditIncome_Click" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." Visible="false"  />
                                                            <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select"  Width="2%" SelectAllCheckboxMode="Page"/> 
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
                                            <div class="col-md-6">
                                                
                                            </div>
                                            <div>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                    <ContentTemplate>
                                                        <ul class="panel-controls">
                                                            <li><asp:LinkButton runat="server" ID="lnkAddDeduct" OnClick="lnkAddDeduct_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                                            <li><asp:LinkButton runat="server" ID="lnkDeleteDeduct" OnClick="lnkDeleteDeduct_Click" Text="Delete" CssClass="control-primary" /></li>
                                                            <li><asp:LinkButton runat="server" ID="lnkExportDeduct" OnClick="lnkExportDeduct_Click" Text="Export" CssClass="control-primary" Visible="false" /></li>
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
                                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                                                <DataItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDeduct_Click" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." Visible="false"  />
                                                            <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Deduction Type" />
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%" SelectAllCheckboxMode="Page" /> 
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
 
                </div> 
            </div> 
        </div>
    </div>
</div>  


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
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
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

</asp:content>
