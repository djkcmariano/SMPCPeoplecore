<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SelfDTRLogList_Deti.aspx.vb" Inherits="SecuredSelf_SelfDTRLogList_Deti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }
</script>

<div class="row">
    <%--<h3>Payroll Details</h3>--%>
    <div class="panel panel-default" style="margin-bottom:10px;">
        <div class="table-responsive">
            <table class="table table-condensed"> 
                <tbody> 
                <tr> 
                    <td style="width:15%;text-align:left;"><strong>Employee No.</strong></td> 
                    <td style="width:35%;"><asp:label ID="lblEmployeeCode" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:15%;text-align:left;"><strong>Employee Name</strong></td> 
                    <td style="width:35%;"><asp:label ID="lblFullName" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                <tr> 
                    <td style="text-align:left;"><strong>DTR Date</strong></td> 
                    <td ><asp:label ID="lblLeaveType" runat="server" class="col-md-12 control-label" /></td>
                   
                    
                </tr> 
               
                </tbody> 
            </table> 
        </div>
    </div>
</div>    
 
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading"> 
                <div class="col-md-2">
                </div>                                
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls"> 
                        <li><asp:LinkButton runat="server" ID="lnkBack" OnClick="lnkBack_Click" Text="Close" CssClass="control-primary" /></li>
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
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRLogDetlNo" OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." HeaderStyle-Wrap="true" />
                                <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project" HeaderStyle-Wrap="true" /> 
                                <dx:GridViewDataTextColumn FieldName="In1" Caption="In" HeaderStyle-Wrap="true" />
                                <dx:GridViewDataTextColumn FieldName="Out1" Caption="Out" HeaderStyle-Wrap="true" />
                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hours Worked" HeaderStyle-Wrap="true" />
                                <dx:GridViewDataTextColumn FieldName="Task" Caption="Task"  PropertiesTextEdit-EncodeHtml="false" HeaderStyle-Wrap="true" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Comments" HeaderStyle-Wrap="true"  PropertiesTextEdit-EncodeHtml="false"/>
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


<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4></h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRLogDetlNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"   Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Project :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboProjectNo" DataMember="EProject" CssClass="form-control" runat="server" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Hours Work :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtHrs" SkinID="txtdate" runat="server" CssClass="form-control"
                        ></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">In :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIn11" runat="server" CssClass="form-control" Placeholder="In" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1x" runat="server"
                        TargetControlID="txtIn11" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                        ControlExtender="MaskedEditExtender1x"
                        ControlToValidate="txtIn11"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
                </div>
                <label class="col-md-1 control-label">Out :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOut11" runat="server" CssClass="form-control" Placeholder="Out" ></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2x" runat="server"
                        TargetControlID="txtOut11" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server"
                        ControlExtender="MaskedEditExtender2x"
                        ControlToValidate="txtOut11"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Task :</label>
                <div class="col-md-6">
                    <%--<asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" 
                        ></asp:Textbox>--%>
                    <dx:ASPxHtmlEditor ID="txtTask" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Comments :</label>
                <div class="col-md-6">
                    <%--<asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" 
                        ></asp:Textbox>--%>
                    <dx:ASPxHtmlEditor ID="txtRemarks" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                </div>
            </div>
            <div class="form-group">
                <br />
            </div>

        </div>
        
         </fieldset>
</asp:Panel>
 

</asp:Content>

