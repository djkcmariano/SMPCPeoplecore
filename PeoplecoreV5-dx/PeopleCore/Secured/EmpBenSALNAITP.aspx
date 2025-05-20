<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpBenSALNAITP.aspx.vb" Inherits="Secured_EmpBenSALNAITP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc:Tab runat="server" ID="Tab" HeaderVisible="true">
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
                            &nbsp;
                            </div>
                            <div>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkNotApplicable" OnClick="lnkNotApplicable_Click" Text="NOT APPLICABLE" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    <uc:ConfirmBox runat="server" ID="cfbNA" TargetControlID="lnkNotApplicable" ConfirmMessage="I have nothing to declare in this portion?"  />
                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TransNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="7%" Visible="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("TransNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." Width="5%" />
                                            <dx:GridViewDataTextColumn FieldName="Nature" Caption="Nature" Width="50%" />
                                            <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="20%" />
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" Visible="false" />

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

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style=" display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtTransNo" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Nature :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtNatureAITP" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtAmountAITP" CssClass="form-control required number" />
                </div>
            </div>

            <br />
        
         </fieldset>
</asp:Panel>

</asp:Content>