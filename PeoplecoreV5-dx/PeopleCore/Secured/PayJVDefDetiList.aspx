<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayJVDefDetiList.aspx.vb" Inherits="Secured_PayJVDefList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<%--<uc:JVTab runat="server" ID="JVTab" />--%>
<uc:FormTab runat="server" ID="FormTab" />

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="JVDefDetiNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="JVTypeDesc" Caption="Type" />
                                    <dx:GridViewDataTextColumn FieldName="EntityCode" Caption="Entity Code" />
                                    <dx:GridViewDataTextColumn FieldName="Description" Caption="Income/Deduction Name" />
                                    <dx:GridViewDataTextColumn FieldName="JVDefLCode" Caption="Account Code" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="JVDefLDesc" Caption="Account Description" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DRCRDesc" Caption="Debit/Credit" />
                                    <dx:GridViewDataComboBoxColumn FieldName="GroupByDesc" Caption="Group By" />
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>



 <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
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
                                   
                <div class="form-group" style="display:none;" runat="server">
                    <label class="col-md-4 control-label has-space">Income Type :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPayIncomeTypeNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>  

                <div class="form-group" style="display:none;" runat="server">
                    <label class="col-md-4 control-label has-space">Deduction Type :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPayDeductTypeNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Income/Deduction Name :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtDescription" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>  

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Account Name :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboJVDefNo" DataMember="EJVDefL" CssClass="form-control" runat="server"></asp:DropdownList>
                    </div>
                </div>

                <br />
            </div>                    
        </fieldset>
    </asp:Panel>


</asp:content>
