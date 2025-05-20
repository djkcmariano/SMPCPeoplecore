<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppExamEdit.aspx.vb" Inherits="Secured_AppExamEdit" %>

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
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="ApplicantExamNo" SkinID="grdDX">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ApplicantExamNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="ExamTypeDesc" Caption="Examination Title" />
                                            <dx:GridViewDataTextColumn FieldName="ScoreRating" Caption="Rating" />
                                            <dx:GridViewDataTextColumn FieldName="xDateTaken" Caption="Date of Examination" />
                                            <dx:GridViewDataTextColumn FieldName="xDateExpired" Caption="Expiry Date" />                                            
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
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">

                    <div class="form-group" style="display:none;">
                        <label class="col-md-4 control-label has-space">Transaction No. :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtApplicantExamNo" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"  />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Transaction No. :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtApplicantExamCode" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"  />
                        </div>
                    </div>            

                    <div class="form-group">
                        <label class="col-md-4 control-label has-required" runat="server" id="Label1">Examination Title :</label>
                        <div class="col-md-7">
                            <asp:DropDownList runat="server" ID="cboExamTypeNo" DataMember="EExamType" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboExamTypeNo_SelectedIndexChanged" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label has-space" runat="server" id="lblOtherExam"></label>
                        <div class="col-md-7">
                            <asp:CheckBox runat="server" ID="txtIsOtherExam" AutoPostBack="true" Text="&nbsp;Tick if data is not in the drop down list" OnCheckedChanged="txtIsOtherExam_CheckedChanged" /><br />
                            <asp:TextBox runat="server" ID="txtOtherExam" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Examination Title" />                    
                        </div>
                    </div>   
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Rating (if applicable) :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtScoreRating" CssClass="form-control" Visible="false"  />
                            <asp:TextBox runat="server" ID="txtScoreRatingDesc" CssClass="form-control"  />                    
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Date of Examination / Conferment :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDateTaken" CssClass="form-control required"  />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateTaken" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateTaken" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateTaken" Display="Dynamic" />
                        </div>
                    </div>                        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Place of Examination / Conferment :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtVenue" CssClass="form-control" TextMode="MultiLine" Rows="2" />                    
                        </div>
                    </div>            
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space" runat="server" id="lblLicense">Certificate / License No. :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtLicenseNo" CssClass="form-control" />                    
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Date of Validity :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDateReleased" CssClass="form-control"  />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtDateReleased" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtDateReleased" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateReleased" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space" runat="server" id="lblExpiry">Expiry Date :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDateExpired" CssClass="form-control"  />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtDateExpired" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="txtDateExpired" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateExpired" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Remarks :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" TextMode="MultiLine" Rows="2" />                    
                        </div>
                    </div>

                </div>                    
                </fieldset>
            </asp:Panel>

</asp:Content>

