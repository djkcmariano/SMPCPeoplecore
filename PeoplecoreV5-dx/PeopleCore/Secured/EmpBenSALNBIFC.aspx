<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpBenSALNBIFC.aspx.vb" Inherits="Secured_EmpBenSALNBIFC" %>

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
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name Of Entity / Business Enterprise" Width="10%" />
                                            <%--<dx:GridViewDataTextColumn FieldName="CompanyName" Caption="Name of Firm/Company" Width="15%" />--%>
                                            <dx:GridViewDataTextColumn FieldName="Address" Caption="Business Address" Width="15%" />
                                            <dx:GridViewDataTextColumn FieldName="Nature" Caption="Nature of Business Interest and/or Financial Connection" Width="30%" />
                                            <dx:GridViewDataTextColumn FieldName="xDate" Caption="Date of Acquisition of interest or connection" Width="10%" />
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
                <label class="col-md-4 control-label has-required">Name Of Entity / Business Enterprise :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtNameBIFC" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space">Name of Firm/Company :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCompanyName" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Business Address :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtAddressBIFC" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Nature of businness interest and/or financial connection :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtNatureBIFC" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date of Acquisition of interest or connection :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtDateBIFC" CssClass="form-control required"/>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateBIFC" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateBIFC" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateBIFC" Display="Dynamic" />
                </div>
            </div>

            <br />
        
         </fieldset>
</asp:Panel>

</asp:Content>