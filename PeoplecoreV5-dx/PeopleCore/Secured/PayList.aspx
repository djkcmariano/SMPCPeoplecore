<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayList.aspx.vb" Inherits="Secured_PayList" %>
<%@ Register Src="~/Include/History.ascx" TagName="History" TagPrefix="uc" %>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function disableenable(chk) {
        var fval = chk.value;

        if (fval == '2') {
            document.getElementById("ctl00_cphBody_cboPYNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboPYNo").value = "";
            //document.getElementById("ctl00_cphBody_cboDTRNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = false;
            //document.getElementById("ctl00_cphBody_cboDTRNo").value = "";
            document.getElementById("ctl00_cphBody_txtStartDate").value = "";
            document.getElementById("ctl00_cphBody_txtEndDate").value = "";
            document.getElementById("ctl00_cphBody_txtIsIncludeForw").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsIncludeMass").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsIncludeOther").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").checked = false;
            document.getElementById("ctl00_cphBody_cboPayClassNo").disabled = false;
            document.getElementById("ctl00_cphBody_txtStartDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtEndDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtApplicableYear").disabled = false;
            document.getElementById("ctl00_cphBody_cboApplicableMonth").disabled = false;
            document.getElementById("ctl00_cphBody_txtPayperiod").disabled = false;
            document.getElementById("ctl00_cphBody_cboPayTypeNo").disabled = false;
            document.getElementById("ctl00_cphBody_chkIsSecondment").disabled = false;
            $('#ctl00_cphBody_cboDTRNo').removeClass("required");
        } else 
        if (fval == '1') {
            document.getElementById("ctl00_cphBody_cboPYNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboPYNo").value = "";
            document.getElementById("ctl00_cphBody_cboPYNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeForw").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsIncludeMass").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsIncludeOther").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").checked = true;
            document.getElementById("ctl00_cphBody_cboPayClassNo").disabled = true;
            document.getElementById("ctl00_cphBody_txtStartDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtEndDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtApplicableYear").disabled = true;
            document.getElementById("ctl00_cphBody_cboApplicableMonth").disabled = true;
            document.getElementById("ctl00_cphBody_txtPayperiod").disabled = true;
            document.getElementById("ctl00_cphBody_cboPayTypeNo").disabled = true;
            document.getElementById("ctl00_cphBody_chkIsSecondment").disabled = true;
            document.getElementById("ctl00_cphBody_chkIsSecondment").checked = false;
            $('#ctl00_cphBody_cboDTRNo').addClass("required");
        } else if (fval == '3') {
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboDTRNo").value = "";
            document.getElementById("ctl00_cphBody_cboPYNo").disabled = false;
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeForw").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsIncludeMass").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsIncludeOther").disabled = false;            
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").checked = true;
            document.getElementById("ctl00_cphBody_cboPayClassNo").disabled = true;
            document.getElementById("ctl00_cphBody_txtStartDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtEndDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtApplicableYear").disabled = true;
            document.getElementById("ctl00_cphBody_cboApplicableMonth").disabled = true;
            document.getElementById("ctl00_cphBody_txtPayperiod").disabled = true;
            document.getElementById("ctl00_cphBody_cboPayTypeNo").disabled = true;
        } else 
        if (fval == '') {

            document.getElementById("ctl00_cphBody_chkIsSecondment").disabled = true;
            document.getElementById("ctl00_cphBody_chkIsSecondment").checked = false;
            $('#ctl00_cphBody_cboDTRNo').removeClass("required");
        };
    };
    function disableenable_behind(fval) {
        if (fval == '2') {
            document.getElementById("ctl00_cphBody_cboPYNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboPYNo").value = "";
            //            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = false;
            //            document.getElementById("ctl00_cphBody_cboDTRNo").value = "";
            document.getElementById("ctl00_cphBody_txtStartDate").value = "";
            document.getElementById("ctl00_cphBody_txtEndDate").value = "";
            document.getElementById("ctl00_cphBody_txtIsIncludeForw").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeMass").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeOther").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").checked = false;
            document.getElementById("ctl00_cphBody_cboPayClassNo").disabled = false;
            document.getElementById("ctl00_cphBody_txtStartDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtEndDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtApplicableYear").disabled = false;
            document.getElementById("ctl00_cphBody_cboApplicableMonth").disabled = false;
            document.getElementById("ctl00_cphBody_txtPayperiod").disabled = false;
            document.getElementById("ctl00_cphBody_cboPayTypeNo").disabled = false;
            document.getElementById("ctl00_cphBody_chkIsSecondment").disabled = false;
            $('#ctl00_cphBody_cboDTRNo').removeClass("required");
        } else if (fval == '1') {
            document.getElementById("ctl00_cphBody_cboPYNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboPYNo").value = "";
            document.getElementById("ctl00_cphBody_cboPYNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeForw").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeMass").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeOther").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").checked = true;
            document.getElementById("ctl00_cphBody_cboPayClassNo").disabled = true;
            document.getElementById("ctl00_cphBody_txtStartDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtEndDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtApplicableYear").disabled = true;
            document.getElementById("ctl00_cphBody_cboApplicableMonth").disabled = true;
            document.getElementById("ctl00_cphBody_txtPayperiod").disabled = true;
            document.getElementById("ctl00_cphBody_cboPayTypeNo").disabled = true;
            document.getElementById("ctl00_cphBody_chkIsSecondment").disabled = true;
            document.getElementById("ctl00_cphBody_chkIsSecondment").checked = false;
            $('#ctl00_cphBody_cboDTRNo').addClass("required");
        } else if (fval == '3') {
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboDTRNo").value = "";
            document.getElementById("ctl00_cphBody_cboPYNo").disabled = false;
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeForw").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeMass").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeOther").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsAttendanceBase").checked = true;
            document.getElementById("ctl00_cphBody_cboPayClassNo").disabled = true;
            document.getElementById("ctl00_cphBody_txtStartDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtEndDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtApplicableYear").disabled = true;
            document.getElementById("ctl00_cphBody_cboApplicableMonth").disabled = true;
            document.getElementById("ctl00_cphBody_txtPayperiod").disabled = true;
            document.getElementById("ctl00_cphBody_cboPayTypeNo").disabled = true;
        };
    };
    function grid_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield.Set('VisibleIndex', parseInt(e.index));
        pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }

     function disableenable_second(chk) {
        var fval = chk.checked;
        var sval = document.getElementById("ctl00_cphBody_cboPaySourceNo").value;
        if (fval == true) {
            document.getElementById("ctl00_cphBody_cboDTRNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboPayClassNo").disabled = false;
            document.getElementById("ctl00_cphBody_cboPayTypeNo").disabled = false;
            document.getElementById("ctl00_cphBody_cboPYNo").value = "";
            document.getElementById("ctl00_cphBody_cboDTRNo").value = "";
            document.getElementById("ctl00_cphBody_txtStartDate").value = "";
            document.getElementById("ctl00_cphBody_txtEndDate").value = "";
            document.getElementById("ctl00_cphBody_cboPayClassNo").value = "";
            document.getElementById("ctl00_cphBody_cboPayTypeNo").value = "";
            document.getElementById("ctl00_cphBody_txtPayStartDate").value = "";
            document.getElementById("ctl00_cphBody_txtPayEndDate").value = "";
            document.getElementById("ctl00_cphBody_txtPayDate").value = "";
            document.getElementById("ctl00_cphBody_txtStartDate").disabled = true;
            document.getElementById("ctl00_cphBody_txtEndDate").disabled = true;
        } 
        else {
            if (sval == '2' || sval == '1') {
                document.getElementById("ctl00_cphBody_cboDTRNo").disabled = false;
                document.getElementById("ctl00_cphBody_txtStartDate").disabled = false;
                document.getElementById("ctl00_cphBody_txtEndDate").disabled = false;
            } else {
                document.getElementById("ctl00_cphBody_cboDTRNo").disabled = true;
            };
        };
    }

</script>
<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">                                
            <div class="col-md-2">
                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
            </div>                
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">                        
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary"/></li>
                        <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>                        
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                    <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process"  />                    
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayNo"
                            OnFillContextMenuItems="MyGridView_FillContextMenuItems">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="History" CellStyle-HorizontalAlign="Center" Width="10">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkHistory" OnClick="lnkHistory_Click" CssClass="fa fa-history" CommandArgument='<%# Eval("PayNo") & "|" & Eval("PayCode")  %>' Font-Size="Medium" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />
                                <dx:GridViewDataTextColumn FieldName="DTRCode" Caption="DTR No." />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayTypeDesc" Caption="Payroll Type" />
                                <dx:GridViewDataDateColumn FieldName="PayDate" Caption="Pay Date" />
                                <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Period" Width="5%" CellStyle-HorizontalAlign="Center" />
                                <dx:GridViewDataComboBoxColumn FieldName="MonthDesc" Caption="Month" />
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" CellStyle-HorizontalAlign="Left"/>
                                <dx:GridViewDataDateColumn FieldName="PayStartDate" Caption="Pay Start Date" Visible="false" />
                                <dx:GridViewDataDateColumn FieldName="PayEndDate" Caption="Pay End Date" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PayLastDateProcess" Caption="Date Processed" Visible="false" />
                                <dx:GridViewDataCheckColumn FieldName="Isspecialpay" Caption="Special<br />Pay"  HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false"/>
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataDateColumn FieldName="DatePosted" Caption="Date Posted" />
                                <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" />
                                <%--<dx:GridViewDataColumn Caption="Special Pay"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDTR" Enabled='<%# Bind("IsEnabledDTR") %>' CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkDTR_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Template"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkTemplate" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkTemplate_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>--%>
                                <dx:GridViewDataColumn Caption="Other<br />Income"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkOtherIncome" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkOtherIncome_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Other<br />Deduction"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkOtherDeduction" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkOtherDeduction_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Summary"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkSummary" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkSummary_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Process" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkProcess_Detail" CssClass='<%# Bind("Icon") %>' Enabled='<%# Bind("IsEnabled") %>' OnClick="lnkProcess_Detail_Click" />
                                        <uc:ConfirmBox runat="server" ID="cfProcess_Detail" TargetControlID="lnkProcess_Detail" ConfirmMessage='<%# Bind("ConfirmMessage") %>' Visible='<%# Bind("IsEnabled") %>'  /> 
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>--%>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />     
                            </Columns>   
                            <ClientSideEvents ContextMenu="grid_ContextMenu" />                   
                        </dx:ASPxGridView>
                         
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                        <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                            <Items>
                                <dx:MenuItem Text="Report" Name="Name">
                                    <Template>
                                        <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Payroll Register Report" /><br />
                                        <asp:LinkButton runat="server" ID="lnkRptSummary" OnClick="lnkRptSummary_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Payroll Summary Report" /><br />
                                        <asp:LinkButton runat="server" ID="lnkRptPayment" OnClick="lnkRptPayment_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Payroll Payment Type Report" /><br />
                                    </Template>
                                </dx:MenuItem>
                            </Items>
                            <ItemStyle Width="240px"></ItemStyle>
                        </dx:ASPxPopupMenu>
                        <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />  
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
        </div>         
         <div  class="entryPopupDetl form-horizontal">            
            <div class="form-group">
                <label class="col-md-4 control-label">Payroll No. :</label>
                <div class="col-md-6">  
                    <asp:HiddenField runat="server" ID="hifPayNo" />
                    <asp:Textbox ID="txtPayCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label">Payroll Source :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPaySourceNo" runat="server" CssClass="form-control" DataMember="EPaySource" onchange="disableenable(this);" />                        
               </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">                    
                    <asp:Checkbox ID="chkIsSecondment" runat="server" Text="&nbsp;Tick here if payroll is for on secondment Employees." onclick="disableenable_second(this);" />
                </div>
            </div> 

            <div class="form-group" style=" visibility:hidden; display:none;">
                <label class="col-md-4 control-label">Pakyawan Trans No. :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPYNo" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="cboPYNo_TextChanged" />                        
               </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">DTR No. :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDTRNo" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="cboDTRNo_TextChanged" />                        
               </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="From" ReadOnly="false" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate" />
                </div>
                <div class="col-md-3">
                    <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="To" ReadOnly="false" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator2" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate" />
                </div>                            
            </div>     
            <div class="form-group">
                <label class="col-md-4 control-label">Remarks :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control"  />                        
               </div>
            </div>        
            <div class="form-group">
                <label class="col-md-4 control-label">
                    <h5><b>Payroll Cut Off Date</b></h5>
                </label>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">                    
                    <asp:Textbox ID="txtPayStartDate" runat="server" CssClass="required form-control" placeholder="From" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtPayStartDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtPayStartDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator3" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPayStartDate" />                    
                </div>
                <div class="col-md-3">                    
                    <asp:Textbox ID="txtPayEndDate" runat="server" CssClass="required form-control" placeholder="To" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="MM/dd/yyyy" TargetControlID="txtPayEndDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtPayEndDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPayEndDate" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Pay Date :</label>
                <div class="col-md-3">                    
                    <asp:Textbox ID="txtPayDate" runat="server" CssClass="required form-control" placeholder="Date" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="txtPayDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtPayDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator5" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPayDate" />
                </div>                
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Payroll Group :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPayClassNo"  runat="server" CssClass="form-control required" />
                </div>                
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Payroll Type :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPayTypeNo" DataMember="EPayType" runat="server" CssClass="form-control required" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable Month :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable Year :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control"  ReadOnly="true" Enabled="false" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Payroll Period :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtPayperiod" runat="server" CssClass="form-control"  ReadOnly="true" Enabled="false" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Pay Schedule :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPayScheduleNo" AutoPostBack="true" OnTextChanged="cboPaySchedule_TextChanged" runat="server" CssClass="form-control required" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">
                    <h5><b>Applicable Deduction</b></h5>
                </label>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-2">                    
                    <asp:Checkbox ID="txtIsDeductTax" runat="server" Text="&nbsp;Tax" />                    
                </div>
                <div class="col-md-2">
                    <asp:Checkbox ID="txtIsDeductSSS" runat="server" Text="&nbsp;SSS" />
                </div>
                <%--<div class="col-md-2" style="Display: none">
                    <asp:Checkbox ID="txtIsPF" runat="server" Text="&nbsp;PF" />
                </div>--%>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-2">
                    <asp:Checkbox ID="txtIsDeductPH" runat="server" Text="&nbsp;PhilHealth" />
                </div>
                <div class="col-md-2">
                    <asp:Checkbox ID="txtIsDeductHDMF" runat="server" Text="&nbsp;Pag-ibig" />
                </div>      
                <%--<div class="col-md-2" style="Display: none">
                    <asp:Checkbox ID="txtIsIHP" runat="server" Text="&nbsp;HF" />
                </div>  --%>                            
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">
                    <h5><b>Payroll Components</b></h5>
                </label>
            </div>                      
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-2">                    
                    <asp:Checkbox ID="txtIsAttendanceBase" runat="server" Text="&nbsp;DTR Base" />
                </div>
                <%--<div class="col-md-2" style="visibility:hidden">                    
                    <asp:Checkbox ID="txtIsAttendanceNonBasic" runat="server" Text="&nbsp;DTR Base (Non-basic)" />
                </div>--%>
                <div class="col-md-2">                    
                    <asp:Checkbox ID="txtIsIncludeMass" runat="server" Text="&nbsp;Template" />
                </div>
                <div class="col-md-2" style="visibility:hidden">                    
                    <asp:Checkbox ID="txtIsAttendanceNonBasic" runat="server" Text="&nbsp;DTR Base (Non-basic)" />
                </div>
            </div>             
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                
                <div class="col-md-2">                    
                    <asp:Checkbox ID="txtIsIncludeForw" runat="server" Text="&nbsp;Forwarded" />
                </div>
                <div class="col-md-2">                    
                    <asp:Checkbox ID="txtIsIncludeLoan" runat="server" Text="&nbsp;Loan" />
                </div>
                <div class="col-md-2">                    
                    <asp:Checkbox ID="txtIsIncludeOther" runat="server" Text="&nbsp;Other" />
                </div>
            </div>            
            
            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label">
                    <h5><b>OTHER COMPONENTS</b></h5>
                </label>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-2">                    
                    <asp:Checkbox ID="txtIsRATA" runat="server" Text="&nbsp;RA" ToolTip="Representation Allowance" Visible="false"  />
                </div>
                <div class="col-md-2" style=" display:none;">                    
                    <asp:Checkbox ID="txtIsRice" runat="server" Text="&nbsp;Rice" Visible="false" />
                </div>
                <div class="col-md-2" style=" display:none;">                    
                    <asp:Checkbox ID="txtIsMedical" runat="server" Text="&nbsp;Medical" Visible="false" />
                </div>
                <div class="col-md-4">                    
                    <asp:Checkbox ID="txtIsLoyalty" runat="server" Text="&nbsp;Loyalty Award" Visible="false"  />
                </div>
            </div>
             </div>
            <br />          
    </fieldset>
</asp:Panel>   
    <uc:History runat="server" ID="History" />
</asp:Content>
