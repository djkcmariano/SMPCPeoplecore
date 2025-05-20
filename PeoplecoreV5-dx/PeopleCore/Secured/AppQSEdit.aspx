<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="AppQSEdit.aspx.vb" Inherits="Secured_AppQSEdit" %>


<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />                
            </center>            
            <asp:Label runat="server" ID="lbl" /> 
        </Header>
        <Content>
            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                                &nbsp;
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ApplicantQSNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ApplicantQSNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                            
                                            <dx:GridViewDataTextColumn FieldName="QSTypeDesc" Caption="Category" />
                                            <dx:GridViewDataTextColumn FieldName="xDesc" Caption="Qualification Standard" />                                            
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
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
    <asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">    
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCode" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"  />
                </div>
            </div>                                                   
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Category :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboQSTypeNo" DataMember="EQSType" CssClass="form-control" AutoPostBack="true" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Qualification Standard :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboQSNo" CssClass="form-control" />
                </div>
            </div>            
            <br />                      
        </div>                    
    </fieldset>
</asp:Panel>
</asp:content> 