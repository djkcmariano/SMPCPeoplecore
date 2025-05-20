<%@ Page Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="frmEmailTemplateList.aspx.vb" Inherits="Secured_frmEmailTemplateList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">


<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3>&nbsp;</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmailTempNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="EmailTempCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="EmailTempDesc" Caption="Decription" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="EmailTempSubj" Caption="Subject" />    
                                <dx:GridViewDataTextColumn FieldName="EmailAddress" Caption="Email Recipients" Width="25%" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />                                                
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
       
            <div class="form-group">
                <label class="col-md-4 control-label">Reference no. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" />
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmailTempCode" runat="server"  CssClass="required form-control" />
                    
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmailTempDesc" runat="server"  CssClass="required form-control" />                        
                   
                </div>
            </div>
           <div class="form-group" style="display:block;">
                <label class="col-md-4 control-label">Email to :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmailAddress" TextMode="MultiLine" Rows="3" runat="server"  CssClass="form-control" 
                        ></asp:Textbox>
                    
            
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Subject :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmailTempSubj" runat="server" CssClass="required form-control" 
                        ></asp:Textbox>
                   
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Body :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmailTempMsg" TextMode="MultiLine" Rows="10"  runat="server" CssClass="required form-control"
                        ></asp:Textbox>
                   
                </div>
            </div>
            <br />
           </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
</asp:Content>

