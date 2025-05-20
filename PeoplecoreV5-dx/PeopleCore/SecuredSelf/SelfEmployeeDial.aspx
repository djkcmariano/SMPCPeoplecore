<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelfEmployeeDial.aspx.vb" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Secured_SelfEmployeeDial" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:TabSelf runat="server" ID="TabSelf">
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
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeDialectNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeDialectNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                            
                                            <dx:GridViewDataTextColumn FieldName="DialectDesc" Caption="Language / Dialect" />
                                            <dx:GridViewDataTextColumn FieldName="WritingDesc" Caption="Writing" />
                                            <dx:GridViewDataTextColumn FieldName="ReadingDesc" Caption="Reading" />
                                            <dx:GridViewDataTextColumn FieldName="SpeakingDesc" Caption="Speaking" />                                                                                       
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
    </uc:TabSelf>
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
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-required" runat="server" id="lblDialect">Language / Dialect :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboDialectNo" DataMember="EDialect" runat="server" CssClass="form-control required" />
                    </div>
            </div>           
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><asp:CheckBox runat="server" ID="txtIsOtherDial" AutoPostBack="true" OnCheckedChanged="txtIsOtherDial_CheckedChanged" />  If others (please specify) :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtOtherDial" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Language / Dialect" />                    
                </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Writing Proficiency Level :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboWritingProfLevelNo" DataMember="EDialectProf" runat="server" CssClass="form-control" />
                    </div>
            </div>      
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Reading Proficiency Level :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboReadingProfLevelNo" DataMember="EDialectProf" runat="server" CssClass="form-control" />
                    </div>
            </div>          
            <div class="form-group">
                <label class="col-md-4 control-label has-space has-space">Speaking Proficiency Level :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboSpeakingProfLevelNo" DataMember="EDialectProf" runat="server" CssClass="form-control" />
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control" />            
                </div>
            </div>                  
        </fieldset>
    </asp:Panel> 
</asp:content> 