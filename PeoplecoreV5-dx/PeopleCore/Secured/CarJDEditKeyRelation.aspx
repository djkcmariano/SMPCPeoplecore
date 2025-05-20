<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CarJDEditKeyRelation.aspx.vb" Inherits="Secured_CarJDEditKeyRelation" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>                       
            <asp:Label runat="server" ID="lbl" />            
        </Header>
        <Content>
            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="lnkSearch_Click" />
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>                                                        
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="JDKeyRelationNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("JDKeyRelationNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />
                                            <dx:GridViewDataTextColumn FieldName="JDKeyRelationTypeDesc" Caption="Type" Width="10%" />
                                            <dx:GridViewDataTextColumn FieldName="JDKeyRelationDesc" Caption="Description" Width="20%" />
                                            <dx:GridViewDataTextColumn FieldName="Frequency" Caption="Frequency" Width="20%" />
                                            <dx:GridViewDataTextColumn FieldName="Purpose" Caption="Purpose" HeaderStyle-HorizontalAlign="Center" />                                          
                                            <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" HeaderStyle-HorizontalAlign="Center" SelectAllCheckboxMode="Page" Width="5%" />--%>
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
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Type :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboJDKeyRelationTypeNo" CssClass="form-control">                            
                            <asp:ListItem Value="1" Text="Internal" Selected="True" />
                            <asp:ListItem Value="2" Text="External" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox id="txtJDKeyRelationDesc" runat="server" CssClass="form-control required" />
                    </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:RadioButton ID="chkIsOccasional" runat="server" Text="&nbsp;Occasional" GroupName="1" /><br />
                        <asp:RadioButton ID="chkIsFrequent" runat="server" Text="&nbsp;Frequent" GroupName="1" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Purpose :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtPurpose" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>               
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

