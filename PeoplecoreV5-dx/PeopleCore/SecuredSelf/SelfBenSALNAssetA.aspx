<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfBenSALNAssetA.aspx.vb" Inherits="SecuredSelf_SelfBenSALNAssetA" %>

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
                                            <dx:GridViewDataTextColumn FieldName="Nature" Caption="Description" Width="15%" />
                                            <dx:GridViewDataTextColumn FieldName="Kind" Caption="Kind" Width="10%" />
                                            <dx:GridViewDataTextColumn FieldName="Location" Caption="Exact Location" Width="10%" />
                                            <dx:GridViewDataTextColumn FieldName="Assessed" Caption="Assessed Value" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="7%" />
                                            <dx:GridViewDataTextColumn FieldName="CurrentFair" Caption="Current Fair Market Value" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="7%" />
                                            <dx:GridViewDataTextColumn FieldName="Year" Caption="Year Acquired" Width="5%" />
                                            <dx:GridViewDataTextColumn FieldName="Mode" Caption="Mode of Acquisition" Width="10%" />
                                            <dx:GridViewDataTextColumn FieldName="Acquisition1" Caption="Acquisition Cost" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="7%" />
<%--                                            <dx:GridViewDataTextColumn FieldName="Acquisition2" Caption="Improvments" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="7%" />--%>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />

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
                <label class="col-md-4 control-label has-required">Description of Property :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtNature" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Kind :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtKind" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Exact Location :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtLocation" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Assessed Value :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtAssessed" CssClass="form-control required number" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Current Fair Market Value :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCurrentFair" CssClass="form-control required number" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Year Acquired :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtYear" CssClass="form-control required" MaxLength="4" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Mode of Acquistion :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtMode" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

<%--            <div class="form-group">  
                <h5 class="col-md-10">
                    <label class="control-label">ACQUISITION &nbsp;&nbsp;COST</label>
                </h5>
            </div>--%>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Acquisition Cost :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtAcquisition1" CssClass="form-control required number" />
                </div>
            </div>

            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space">Improvements :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtAcquisition2" CssClass="form-control" />
                </div>
            </div>

            <br />
        
         </fieldset>
</asp:Panel>

</asp:Content>