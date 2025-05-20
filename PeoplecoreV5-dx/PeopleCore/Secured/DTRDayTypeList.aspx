<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRDayTypeList.aspx.vb" Inherits="Secured_DTRDayTypeList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                                        
                    <h4>&nbsp;</h4>
                </div>
                <div>                                                                                                                                                                                                                         
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayClassNo">                                                                                   
                            <Columns>                                                                   
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                <dx:GridViewDataTextColumn FieldName="PayClassCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Description" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                            </Columns>                            
                        </dx:ASPxGridView>                                
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div> 
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">                                        
                    <div class="panel-title">
                        <asp:Label runat="server" ID="lbl" Text="Reference No. :" />
                    </div>                    
                </div>
                <div> 
                    <div>    
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkGenerate_Click" Text="Generate Day Type" CssClass="control-primary" /></li> 
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Visible="false" Text="Add" CssClass="control-primary" /></li>                                                    
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
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="DayTypeNo" Width="100%">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="DayTypeCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="DayTypeDesc" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="Ovt" Caption="Ovt" />
                                <dx:GridViewDataTextColumn FieldName="Ovt8" Caption="Ovt8" />
                                <dx:GridViewDataTextColumn FieldName="NP" Caption="NP" />
                                <dx:GridViewDataTextColumn FieldName="NPOvt" Caption="NPOvt" />
                                <dx:GridViewDataTextColumn FieldName="NPOvt8" Caption="NPOvt8" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />                               
                            </Columns>                            
                        </dx:ASPxGridView>    
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdDetl" />                              
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div> 


 <asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShowDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" >
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsMain btnSaveDetl" OnClick="btnSaveDetl_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
       
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDayTypeNo" ReadOnly="true" runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Day Type Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDayTypeCode" runat="server" CssClass="required form-control" 
                        ></asp:TextBox>
               </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Day Type Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDayTypeDesc" runat="server" CssClass="required form-control" 
                        ></asp:TextBox>
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Ovt :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOvt" SkinID="txtdate"  runat="server" CssClass="number form-control" 
                        ></asp:Textbox>
                                                        
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Ovt 8 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOVt8" SkinID="txtdate"  runat="server" CssClass="number form-control" 
                        ></asp:Textbox>
                                                          
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">NP :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtNP" SkinID="txtdate"  runat="server" CssClass="number form-control" 
                        ></asp:Textbox>
                                           
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">NP Ovt :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtNPOvt" SkinID="txtdate"  runat="server" CssClass="number form-control" 
                        ></asp:Textbox>
                        
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">NP Ovt8 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtNPOvt8" SkinID="txtdate"  runat="server" CssClass="number form-control"
                        ></asp:Textbox>
                                                             
                </div>
            </div>
            <br />
           </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
    
</asp:Content>