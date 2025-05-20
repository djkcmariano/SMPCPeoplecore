<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayAlphaList_Upload.aspx.vb" Inherits="Secured_PayAlphaList_Upload" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">


<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }
</script>

<div class="page-content-wrap"> 
    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExport" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BatchFileNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="BatchFileNo" Caption="Batch No." CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataTextColumn FieldName="Filename" Caption="Filename" />
                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Uploaded By" />
                                <dx:GridViewDataTextColumn FieldName="EncodedDate" Caption="Date Uploaded" />
                                <dx:GridViewDataTextColumn FieldName="tCount" Caption="Uploaded" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
                                    <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn> 
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6 panel-title">
                        Batch No. :&nbsp;<asp:Label ID="lbl" runat="server"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkExportD" OnClick="lnkExportD_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExportD" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="BatchFileErrorNo" Width="100%">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="ErrorMessage" Caption="Error Message" />
                            </Columns>
                            <SettingsSearchPanel Visible="true" />  
                            <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7">
                                <PageSizeItemSettings Visible="true" Items="10, 20, 50, 100" />        
                            </SettingsPager>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportD" runat="server" GridViewID="grdDetl" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Button ID="btnShow" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />
                        &nbsp;
                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group">
                    <p class="col-md-12 control-label-left  has-space"><code><i class="fa fa-info-circle fa-lg"></i> File must be CSV (Comma delimited) with following columns and header: employeeno,	applicableyear,	datefrom,	dateto,	Description,	TotalBasicIncome,	TotalOneTimeTaxableIncomeOther,	TotalNonTaxableIncomeOther,	TaxExemption,	TaxwithHeld,	bonus,	December Adjustment? (1=YES and 0=NO),	With Previous Employer(1=YES and 0=NO),	EMPLOYER Name,	Address	,Tin No.,	Exclude In Alpha(1=yes and 0=no),	hazard Pay,	commission,	directors fee,	overtime,	profit,	repre,	cola,	housing,	transpo	,13th month(initial release),	deminimis,	NP,	holiday, pay,	MWE (1=YES and 0=NO). </code></p>
                    <br />
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Batch No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtBatchFileNo" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Filename :</label>
                    <div class="col-md-7">
                        <asp:FileUpload runat="server" ID="fuFilename" Width="100%" CssClass="required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtDescription" runat="server" Rows="4" textmode="MultiLine" CssClass="form-control required" />
                    </div>
                </div>

                <br />
            </div>
            <!-- Footer here -->
        </fieldset>
    </asp:Panel>
   

      
</asp:Content>

