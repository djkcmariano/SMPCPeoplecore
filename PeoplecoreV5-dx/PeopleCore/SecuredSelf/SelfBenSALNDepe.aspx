<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfBenSALNDepe.aspx.vb" Inherits="SecuredSelf_SelfBenSALNDepe" %>

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
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="7%">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("TransNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." Width="5%" />
                                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name" />
                                            <dx:GridViewDataTextColumn FieldName="DateOfBirth" Caption="Date of Birth" />
                                            <dx:GridViewDataTextColumn FieldName="Age" Caption="Age" />
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
                <label class="col-md-4 control-label has-required">Family Name :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtDepeLName" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">First Name :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtDepeFName" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Middle Name :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtDepeMName" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date of Birth :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtDateOfBirth" CssClass="form-control required"/>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateOfBirth" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateOfBirth" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateOfBirth" Display="Dynamic" />
                </div>
            </div>


        <br />
        
         </fieldset>
</asp:Panel>


</asp:Content>