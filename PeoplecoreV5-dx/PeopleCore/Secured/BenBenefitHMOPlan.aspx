<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BenBenefitHMOPlan.aspx.vb" Inherits="Secured_BenBenefitHMOPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<br />
<div class="page-content-wrap">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdHMOType" ClientInstanceName="grdHMOType" runat="server" SkinID="grdDX" KeyFieldName="BenefitHMOPlanTypeNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="BenefitHMOPlanTypeCode" Caption="HMO Plan Group Code" />
                                <dx:GridViewDataTextColumn FieldName="BenefitHMOPlanTypeDesc" Caption="HMO Plan Group Description" /> 
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                            </Columns>                            
                        </dx:ASPxGridView>
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
                <div class="col-md-6">
                    <h4><asp:Label runat="server" ID="lbl" /></h4>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitHMOPlanNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Referrence No." />
                                <dx:GridViewDataTextColumn FieldName="BenefitHMOPlanCode" Caption="Code" />
                                <%--<dx:GridViewDataTextColumn FieldName="BenefitHMOPlanDesc" Caption="Description" />--%>
                                <dx:GridViewDataComboBoxColumn FieldName="BenefitHMOTypeDesc" Caption="HMO Plan Type" /> 
                                <dx:GridViewDataTextColumn FieldName="RoomTypeDesc" Caption="Room Type" />
                                <dx:GridViewDataTextColumn FieldName="MBL" Caption="MBL" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="AddPremiumCost" Caption="Upgrade Cost" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="PercentCost" Caption="Subsidy Cost" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="PHFee" Caption="PhilHealth Fee" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <%--<dx:GridViewDataCheckColumn FieldName="IsPrincipalPlan" Caption="Principal Plan" />--%>
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder" Visible="false" />                                                          
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
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
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none" >
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        `
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Code :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtBenefitHMOPlanCode" runat="server" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtBenefitHMOPlanDesc" runat="server" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">HMO Plan Type :</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cboBenefitHMOTypeNo" runat="server" DataMember="EBenefitHMOType" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Room Type :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboRoomTypeNo" CssClass="form-control required">
                            <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                            <asp:ListItem Value="1" Text="REGULAR PRIVATE" />
                            <asp:ListItem Value="2" Text="SEMI-PRIVATE" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group" style="visibility:hidden; position:absolute;">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:RadioButton runat="server" ID="chkIsPrincipalPlan" GroupName="Plan" Text="&nbsp;Principal Plan" OnCheckedChanged="rdo_CheckedChanged" AutoPostBack="true" /><br />
                        <asp:RadioButton runat="server" ID="chkIsDependentPlan" GroupName="Plan" Text="&nbsp;Dependent Plan" OnCheckedChanged="rdo_CheckedChanged" AutoPostBack="true" />
                    </div>
                </div>

                <div class="form-group" style="visibility:hidden; position:absolute;">
                    <label class="col-md-4 control-label has-space">No. of Dependent :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtNoOfDependent" runat="server" CssClass="form-control number" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Maximum Benefit Limit :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtMBL" runat="server" CssClass="form-control required number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Subsidy Cost :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtPercentCost" runat="server" CssClass="form-control number" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Upgrade Cost :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtAddPremiumCost" runat="server" CssClass="form-control number" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">PhilHealth Fee :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtPHFee" runat="server" CssClass="form-control number" />
                    </div>
                </div>

                <div class="form-group" style="visibility:hidden; position:absolute;" >
                    <label class="col-md-4 control-label has-space">Order Level :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtOrderLevel" runat="server" CssClass="form-control number" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">No. Of Payments :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtNoOfPayments" runat="server" CssClass="form-control number required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Payroll Schedule :</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cboPayScheduleNo" runat="server" DataMember="EPaySchedule" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Applicable Year :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control number required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                    </div>
                </div>
                
                <div class="form-group" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
                </div>  
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>                       
                               
            </div>                    
        </fieldset>
    </asp:Panel>
    
    
</asp:Content>

