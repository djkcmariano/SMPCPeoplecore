<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelfEmployeeTrn.aspx.vb" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Secured_SelfEmployeeTrn" %>

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
                                    <i>(Start from the most recent L&D/training program and include only the relevant L&D/training taken for the last five (5) years for Division Chief/Executive/Managerial positions)</i>
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeTrainNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeTrainNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                                                                    
                                            <dx:GridViewDataTextColumn FieldName="TrainingTitleDesc" Caption="Program Title" />
                                            <dx:GridViewDataTextColumn FieldName="IssuedBy" Caption="Sponsored / Conducted" />   
                                            <dx:GridViewDataTextColumn FieldName="DateFrom" Caption="From" />
                                            <dx:GridViewDataTextColumn FieldName="DateTo" Caption="To" />
                                            <dx:GridViewDataTextColumn FieldName="NoOfHrs" Caption="No. of Hours" /> 
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
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
                &nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>
            <div class="entryPopupDetl form-horizontal">
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder= "Autonumber"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Title of Learning and Development Interventions/Training Programs :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtTrainingTitleDesc" CssClass="required form-control" TextMode="MultiLine" Rows="3" />                    
                    </div>
                </div>  
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Type of LD :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboSTTypeNo" DataMember="ECompTrn" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Conducted / Sponsored by :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtIssuedBy" CssClass="form-control" TextMode="MultiLine" Rows="2" />                    
                    </div>
                </div>
                <h5><b>INCLUSIVE DATES OF ATTENDANCE</b></h5>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Attended From :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateFrom" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateFrom" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateFrom" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Attended To :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtDateTo" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtDateTo" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateTo" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">No. of Hours :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtNoOfHrs" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group" style=" display:none">
                    <label class="col-md-4 control-label has-space">Venue :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtVenue" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                    </div>
                </div>
                <br />
                
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>