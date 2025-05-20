<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppMRAnnualList.aspx.vb" Inherits="Secured_AppMRAnnualList" Theme="PCoreStyle" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                                                
                    <ul class="panel-controls">                                                     
                        <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDisapproved" OnClick="lnkDisapproved_Click" Text="Disapprove" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkSubmit" OnClick="lnkSubmit_Click" Text="Submit for Review" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li> 
                    </ul>
                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkApproved" ConfirmMessage="Approved transaction can not be modified. Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkDisapproved" ConfirmMessage="Disapprove transaction can not be modified. Proceed?"  />

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
                        <dx:ASPxGridView ID="grdDept" ClientInstanceName="grdDept" runat="server" SkinID="grdDX" KeyFieldName="MRAnnualMainNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataColumn FieldName="ApplicableYear" Caption="Applicable Year"/>
                                <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Dept/Office/Region" />
                                <dx:GridViewDataTextColumn FieldName="GroupDesc" Caption="Group/Branch" />
                                <dx:GridViewDataDateColumn FieldName="RequestedDate" Caption="Date Submitted" />
                                <dx:GridViewDataDateColumn FieldName="FullName" Caption="Submitted By" />
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Status" Width="12%" />             
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />                                                        
                            </Columns>                            
                        </dx:ASPxGridView>                                
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                   <asp:Dropdownlist ID="cboTab2No" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch2_Click" CssClass="form-control" runat="server" />
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>                                                
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>                                                       
                            </ul>
                            <uc:ConfirmBox runat="server" ID="ConfirmBox4" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be recovered, Proceed?" /> 
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="MRAnnualNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("MRAnnualNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position Title" />
                                <dx:GridViewDataComboBoxColumn FieldName="JobGradeDesc" Caption="Job Level" />
                                <dx:GridViewDataComboBoxColumn FieldName="PlantillaDesc" Caption="Plantilla No." />
                                <dx:GridViewDataComboBoxColumn FieldName="FullName" Caption="Incumbent" />
                                <dx:GridViewDataComboBoxColumn FieldName="MRTypeDesc" Caption="F/CV/AV" />
                                <dx:GridViewDataComboBoxColumn FieldName="QuarterDesc" Caption="RTF/RTH Date" />
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

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;" >
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label  has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtMRAnnualNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                    <asp:HiddenField runat="server" ID="hifRequestedByNo" />
                 </div>
            </div>


            <div class="form-group">
                <label class="col-md-4 control-label has-space">AMP Submission Date :</label>
                <div class="col-md-3">
                        <asp:TextBox ID="txtRequestedDate" runat="server" CssClass="form-control" SkinID="txtdate" ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                            Format="MM/dd/yyyy" TargetControlID="txtRequestedDate" />
                        <asp:RangeValidator ID="RangeValidator3" runat="server" 
                            ControlToValidate="txtRequestedDate" Display="None" 
                            ErrorMessage="&lt;b&gt;Please enter valid entry&lt;/b&gt;" 
                            MaximumValue="3000-12-31" MinimumValue="1900-01-01" Type="Date" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                            AcceptNegative="Left" ClearTextOnInvalid="true" DisplayMoney="Left" 
                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" 
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtRequestedDate" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" 
                            runat="Server" TargetControlID="RangeValidator3" />
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Plantilla No. :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPlantillaNo" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cboPlantillaNo_SelectedIndexChanged" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Position Title :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPositionNo" DataMember="EPosition" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Plantilla No. Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPlantillaGovTypeNo" DataMember="EPlantillaGovType" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Incumbent :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" />
                    <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">F / CV / AV :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboMRTypeNo" DataMember="EMRType" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Job Level :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboJobGradeNo" runat="server" DataMember="EJobGrade" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Facility :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboFacilityNo" DataMember="EFacility" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Unit :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboUnitNo" DataMember="EUnit" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Dept/Office/Region :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Group/Branch :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboGroupNo" DataMember="EGroup" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Division :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboDivisionNo" DataMember="EDivision" CssClass="form-control" />
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-4 control-label has-space">Section/Unit :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboSectionNo" DataMember="ESection" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Cost Center :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboCostCenterNo" DataMember="ECostCenter" CssClass="form-control" />
                </div>                
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Location :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboLocationNo" DataMember="ELocation" CssClass="form-control" />
                </div>                
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable Year :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtApplicableYear" CssClass="form-control number" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">RTF/RTH Date :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboQuarterNo" DataMember="EQuarter" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Mode of filling :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboMRReasonNo" DataMember="EMRReason" CssClass="form-control" />
                </div>
            </div>

            <br />
            <br />
                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>
</asp:Content>

