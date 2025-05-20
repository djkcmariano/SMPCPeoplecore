<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppRefEdit.aspx.vb" Inherits="Secured_AppRefEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />                
            </center>            
            <asp:Label runat="server" ID="lbl" /> 
        </Header>        
        <Content>
            <br />
            <div class="page-content-wrap">         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                                <h4>&nbsp;</h4>                                
                            </div>
                            <div>
                                <div>                                                                                    
                                    <ul class="panel-controls">                                                                                                                        
                                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                                
                                    </ul>                                    
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                    
                                </div>                                
                            </div>                           
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="ApplicantRefeNo" SkinID="grdDX">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ApplicantRefeNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <%--<dx:GridViewDataTextColumn FieldName="RelationRefeType" Caption="Relationship" />--%>
                                            <dx:GridViewDataTextColumn FieldName="Fullname" Caption="Name" />
                                            <dx:GridViewDataTextColumn FieldName="Occupation" Caption="Occupation / Position" />
                                            <dx:GridViewDataTextColumn FieldName="PhoneNo" Caption="Contact No." />                                            
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
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtApplicantRefeNo" ReadOnly="true" CssClass="form-control"  />
                    </div>
                </div>          
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtApplicantRefeCode" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"  />
                    </div>
                </div>           
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Relationship :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtRelationRefeType" CssClass="form-control" />
                    </div>
                </div>             
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Surname :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">First Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Middle Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtMiddlename" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Contact No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control number" />                        
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">E-mail :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />                        
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Occupation / Position :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtOccupation" CssClass="form-control" TextMode="MultiLine" Rows="2" />                        
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Employer / Company :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtCompany" CssClass="form-control" TextMode="MultiLine" Rows="2" />                        
                    </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Address :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" TextMode="MultiLine" Rows="2" />                        
                    </div>
                </div>
                <!--<div class="form-group">
                    <label class="col-md-4 control-label">Business Address :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtBusinessAddress" CssClass="form-control" TextMode="MultiLine" Rows="4" />                        
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">Business Contact No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtBusinessPhoneNo" CssClass="form-control" />                        
                    </div>
                </div>-->
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Years Known :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtAcquaintanceYear" CssClass="form-control number" SkinID="txtdate" />                        
                    </div>
                </div>                
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>
