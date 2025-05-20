<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="SecUnpostTransaction.aspx.vb" Inherits="Secured_SecUnpostTransaction" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
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
                                                            
                            </ul>        
                            
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="UnpostNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="MenuTitle" Caption="Menu" />
                                        <dx:GridViewDataTextColumn FieldName="TransactionCode" Caption="Unposted Trans. No." />
                                        <dx:GridViewDataTextColumn FieldName="PostedByName" Caption="Posted By" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Date Posted" Visible="false" /> 
                                        <dx:GridViewDataTextColumn FieldName="UnpostedByName" Caption="Unposted By" />
                                        <dx:GridViewDataTextColumn FieldName="UnpostedDate" Caption="Date Unposted" /> 
                                        <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" />
                                                                                  
                                    </Columns>                            
                                </dx:ASPxGridView>   
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>      
     
    </Content>  
</uc:Tab>    


<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="Button1" PopupControlID="pnlPopup" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none" >
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal"> 
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtUnpostNo" CssClass="form-control" runat="server" Placeholder="Autonumber" ></asp:Textbox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Menu :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboModuleNo" CssClass="form-control required" AutoPostBack="true"  OnSelectedIndexChanged="lnkFilter_Click" />
                    </div>
                </div>    

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboTransactionNo" CssClass="form-control required" AutoPostBack="true"  OnSelectedIndexChanged="lnkTransaction_Click" />
                    </div>
                </div> 

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Posted By :</label>
                    <div class="col-md-7">
                        <asp:HiddenField runat="server" ID="hifPostedByNo" />
                        <asp:TextBox ID="txtPostedByName"  runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Date Posted :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtDatePosted"  runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Reason of Unposting :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtReasons"  runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="4" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">ADMIN AUTHENTICATION</label>
                    <div class="col-md-7">
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Username :</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtAdminName"  runat="server" CssClass="form-control required" AutoCompleteType="Disabled" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Password :</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtAdminPassword" TextMode="Password"  runat="server" CssClass="form-control required" AutoCompleteType="Disabled" onfocus="this.removeAttribute('readonly');"></asp:TextBox>
                    </div>
                </div>

                <br />
        </div>                    
    </fieldset>
</asp:Panel>


                 
</asp:Content>