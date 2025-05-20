<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CliClinicFindingList.aspx.vb" Inherits="Secured_CliClinicFindingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />            
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
                    
                            </div>
                            <div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>                    
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
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
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ClinicHisFindingNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>                            
                                            <dx:GridViewDataTextColumn FieldName="ClinicHisFindingCode" Caption="Trans. No." />
                                            <dx:GridViewDataTextColumn FieldName="TestDate" Caption="Test Date" /> 
                                            <dx:GridViewDataTextColumn FieldName="TestConductedDesc" Caption="Lab/Diag Test" />                                                                           
                                            <dx:GridViewDataTextColumn FieldName="TestResultDesc" Caption="Test Result" />
                                            <dx:GridViewDataTextColumn FieldName="Findings" Caption="Findings" />                    
                                            <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />                                                                                                                                                        
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>                            
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
            </div>
        </div>

            <asp:Button ID="Button1" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtClinicHisFindingCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Test Date :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control required"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTestDate" Format="MM/dd/yyyy" />  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTestDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  />
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtTestDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" />                                                                           
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Lab/Diag Test :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboTestConductedNo" DataMember="ETestConducted" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="TestConductedNo_ValueChanged" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Test Result :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboTestResultNo" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Findings :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtFindings" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Remarks :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>
                    </div>                    
                </fieldset>
            </asp:Panel>

        </Content>
    </uc:Tab>
</asp:Content>

