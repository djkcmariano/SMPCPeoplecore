<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CliClinicPastList.aspx.vb" Inherits="Secured_CliClinicPastList" %>

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
                            <div class="col-md-3">
                                <h4 class="panel-title">Infectious Disease</h4>
                            </div>
                            <div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>                    
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAddID" OnClick="lnkAddID_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDeleteID" OnClick="lnkDeleteID_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExportID" OnClick="lnkExportID_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul> 
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteID" TargetControlID="lnkDeleteID" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExportID" />
                                </Triggers>
                                </asp:UpdatePanel> 
                            </div>           
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMainID" ClientInstanceName="grdMainID" runat="server" SkinID="grdDX" KeyFieldName="ClinicPastIDNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditID" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditID_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                    <dx:GridViewDataTextColumn FieldName="ClinicPastIDCode" Caption="Trans. No." />
                                    <dx:GridViewDataComboBoxColumn FieldName="ClinicIDDesc" Caption="Infectious Disease" />                                                                           
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />                                                                                                                                                
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                </Columns>                            
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExportID" runat="server" GridViewID="grdMainID" />
                            </div>
                        </div>                                        
                    </div>                   
                </div>                    
            </div>
        <div class="page-content-wrap">         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-3">
                                <h4 class="panel-title">Operations & Injuries</h4>
                            </div>
                            <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>                    
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAddOI" OnClick="lnkAddOI_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDeleteOI" OnClick="lnkDeleteOI_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExportOI" OnClick="lnkExportOI_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul> 
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteOI" TargetControlID="lnkDeleteOI" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExportOI" />
                                </Triggers>
                                </asp:UpdatePanel> 
                            </div>              
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMainOI" ClientInstanceName="grdMainOI" runat="server" SkinID="grdDX" KeyFieldName="ClinicPastOINo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditOI" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditOI_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                    <dx:GridViewDataTextColumn FieldName="ClinicPastOICode" Caption="Trans. No." />
                                    <dx:GridViewDataTextColumn FieldName="NatureOfInjury" Caption="Nature of Injury" />  
                                    <dx:GridViewDataTextColumn FieldName="NatureOfOperation" Caption="Nature of Operation" />                                              
                                    <dx:GridViewDataTextColumn FieldName="OIDate" Caption="Date" />   
                                    <dx:GridViewDataTextColumn FieldName="OperativeDiagnosis" Caption="Operative Diagnosis" />                                                                                                                                              
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                </Columns>                            
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExportOI" runat="server" GridViewID="grdMainOI" />
                            </div>
                        </div>                                       
                    </div>                   
                </div>                    
            </div>
        <div class="page-content-wrap">         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-3">
                                <h4 class="panel-title">Previous Hospitalizations</h4>
                            </div>
                            <div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                <ContentTemplate>                    
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAddPH" OnClick="lnkAddPH_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDeletePH" OnClick="lnkDeletePH_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExportPH" OnClick="lnkExportPH_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul> 
                                    <uc:ConfirmBox runat="server" ID="cfbDeletePH" TargetControlID="lnkDeletePH" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExportPH" />
                                </Triggers>
                                </asp:UpdatePanel> 
                            </div>              
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMainPH" ClientInstanceName="grdMainPH" runat="server" SkinID="grdDX" KeyFieldName="ClinicPastPHNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditPH" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditPH_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                    <dx:GridViewDataTextColumn FieldName="ClinicPastPHCode" Caption="Trans. No." />
                                    <dx:GridViewDataTextColumn FieldName="NatureOfConfinement" Caption="Nature of Confinement" />  
                                    <dx:GridViewDataTextColumn FieldName="Hospital" Caption="Name & Address Hospital" />                                                                          
                                    <dx:GridViewDataTextColumn FieldName="DateOfConfinement" Caption="Date of Confinement" />                                                                                                                                                
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                </Columns>                            
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExportPH" runat="server" GridViewID="grdMainPH" />
                            </div>
                        </div>                                         
                    </div>                   
                </div>                    
            </div>
        </div>
        <div class="page-content-wrap">         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-3">
                                <h4 class="panel-title">Systems Disease</h4>
                            </div>
                            <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                <ContentTemplate>                    
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAddSD" OnClick="lnkAddSD_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDeleteSD" OnClick="lnkDeleteSD_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExportSD" OnClick="lnkExportSD_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul> 
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteSD" TargetControlID="lnkDeleteSD" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExportSD" />
                                </Triggers>
                                </asp:UpdatePanel> 
                            </div>             
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMainSD" ClientInstanceName="grdMainSD" runat="server" SkinID="grdDX" KeyFieldName="ClinicPastSDNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditSD" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditSD_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                    <dx:GridViewDataTextColumn FieldName="ClinicPastSDCode" Caption="Trans. No." />
                                    <dx:GridViewDataComboBoxColumn FieldName="ClinicSDTypeDesc" Caption="System Disease Type" />
                                    <dx:GridViewDataComboBoxColumn FieldName="ClinicSDDesc" Caption="System Disease" />  
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />                                                                                                                                                                                                                        
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                </Columns>                            
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExportSD" runat="server" GridViewID="grdMainSD" />
                            </div>
                        </div>                                      
                    </div>                   
                </div>                    
            </div>
        </div>

            <asp:Button ID="btnID" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMainID" runat="server" TargetControlID="btnID" PopupControlID="Panel1" CancelControlID="lnkCloseID" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMainID">                    
                    <div class="cf popupheader">
                        <h4>Infectious Disease &nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkCloseID" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveID" OnClick="lnkSaveID_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtClinicPastIDCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Infectious Disease :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboClinicIDNo" DataMember="EClinicID" runat="server" CssClass="form-control required" />
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

            <asp:Button ID="btnOI" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMainOI" runat="server" TargetControlID="btnOI" PopupControlID="Panel2" CancelControlID="lnkCloseOI" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMainOI">                    
                    <div class="cf popupheader">
                        <h4>Operations & Injuries &nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkCloseOI" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveOI" OnClick="lnkSaveOI_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtClinicPastOICode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Nature of Injury :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtNatureOfInjury" TextMode="MultiLine" Rows="3" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Nature of Operation :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtNatureOfOperation" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Date :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtOIDate" runat="server" CssClass="form-control"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtOIDate" Format="MM/dd/yyyy" />  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtOIDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtOIDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator2" />                                                                           
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Operative Diagnosis :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtOperativeDiagnosis" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-6">
                                <asp:CheckBox runat="server" ID="chkIsWorkRelated" Text="&nbsp;Work related" />
                            </div>
                        </div>

                    </div>                    
                </fieldset>
            </asp:Panel>

            <asp:Button ID="btnPH" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMainPH" runat="server" TargetControlID="btnPH" PopupControlID="Panel3" CancelControlID="lnkClosePH" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel3" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMainPH">                    
                    <div class="cf popupheader">
                        <h4>Previous Hospitalizations &nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClosePH" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSavePH" OnClick="lnkSavePH_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtClinicPastPHCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>

                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Nature Of Confinement :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtNatureOfConfinement" TextMode="MultiLine" Rows="3" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Name & Address of Hospital :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtHospital" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Date Of Confinement :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtDateOfConfinement" runat="server" CssClass="form-control"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateOfConfinement" Format="MM/dd/yyyy" />       
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDateOfConfinement" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDateOfConfinement" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" />                                                                           
                            </div>
                        </div>


                    </div>                    
                </fieldset>
            </asp:Panel>

            <asp:Button ID="btnSD" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMainSD" runat="server" TargetControlID="btnSD" PopupControlID="Panel4" CancelControlID="lnkCloseSD" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel4" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMainSD">                    
                    <div class="cf popupheader">
                        <h4>Systems Disease &nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkCloseSD" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveSD" OnClick="lnkSaveSD_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtClinicPastSDCode" ReadOnly="true" runat="server" CssClass="form-control"  Placeholder="Autonumber"/>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">System Disease Type :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboClinicSDTypeNo" DataMember="EClinicSDType" runat="server" CssClass="form-control required"  AutoPostBack="true" OnSelectedIndexChanged="ClinicSDNo_ValueChanged" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">System Disease :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboClinicSDNo" runat="server" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Remarks :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRemarksSD" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>
                        
                    </div>                    
                </fieldset>
            </asp:Panel>

        </Content>
    </uc:Tab>
</asp:Content>

