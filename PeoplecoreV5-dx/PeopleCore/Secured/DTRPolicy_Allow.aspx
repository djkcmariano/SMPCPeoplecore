<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="DTRPolicy_Allow.aspx.vb" Inherits="Secured_DTRAllowPolicyList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function showhide(fval) {

        $("#divshift").hide();
        $("#divisperhr").hide();
        $("#divhrsfrom").hide();
        $("#divhrsto").hide();
        $("#divamount").hide();
        $("#divdaytype").hide();
        $("#divday").hide();
        $("#divtimeout").hide();

        if (fval == '1') {
            $("#divisperhr").show();
            $("#divhrsfrom").show();
            $("#divhrsto").show();
            $("#divamount").show();
            $("#divdaytype").show();
            $("#divday").show();
            $("#lblhrsfrom").text("Hours From :");
            $("#lblhrsto").text("Hours To :");
        }else if (fval == '3') {
            $("#divshift").show();

        } else if (fval == '4') {
            $("#divhrsfrom").show();
            $("#divhrsto").show();
            $("#divamount").show();
            $("#lblhrsfrom").text("Age From :");
            $("#lblhrsto").text("Age To :");
        }
        $("#divamount").show();
    }

    function showhidenext(chk) {
        var fval = chk.value;

        $("#divshift").hide();
//        $("#divisperhr").hide();
        $("#divhrsfrom").hide();
        $("#divhrsto").hide();
        $("#divamount").hide();
        $("#divdaytype").hide();
        $("#divday").hide();
        $("#divtimeout").hide();

        if (fval == '1') {
            $("#divisperhr").show();
            $("#divamount").show();
            $("#divdaytype").show();
            $("#divday").show();
        } else if (fval == '2' || fval == '4') {
            $("#divisperhr").show();
            $("#divamount").show();
            $("#divdaytype").show();
            $("#divday").show();
            $("#divhrsfrom").show();
            $("#divhrsto").show();
            $("#lblhrsfrom").text("Hours From :");
            $("#lblhrsto").text("Hours To :");
        } else if (fval == '3') {
            $("#divisperhr").show();
            $("#divamount").show();
            $("#divdaytype").show();
            $("#divday").show();
            $("#divhrsfrom").show();
            $("#lblhrsfrom").text("Hours :");

        }
        $("#divamount").show();
    }

    function showhidenext_behind(typeno,fval) {
      
        $("#divshift").hide();
        //        $("#divisperhr").hide();
        $("#divhrsfrom").hide();
        $("#divhrsto").hide();
        $("#divamount").hide();
        $("#divdaytype").hide();
        $("#divday").hide();
        $("#divtimeout").hide();

        if (typeno == '1' || typeno == '2' || typeno == '6' ) {
            $("#divisperhr").show();
            $("#divhrsfrom").show();
            $("#divhrsto").show();
            $("#divamount").show();
            $("#divdaytype").show();
            $("#divday").show();
        } else if (typeno == '3') {
            $("#divshift").show();
            $("#divisperhr").hide();
        } else if (typeno == '4') {
            $("#divhrsfrom").show();
            $("#divhrsto").show();
            $("#divamount").show();
            $("#lblhrsfrom").text("Age From :");
            $("#lblhrsto").text("Age To :");
            $("#divisperhr").hide();
        }

        if (fval == '1') {
            $("#divisperhr").show();
            $("#divamount").show();
            $("#divdaytype").show();
            $("#divday").show();

            $("#divhrsfrom").hide();
            $("#divhrsto").hide();

        } else if (fval == '2' || fval == '4') {
            $("#divisperhr").show();
            $("#divamount").show();
            $("#divdaytype").show();
            $("#divday").show();
            $("#divhrsfrom").show();
            $("#divhrsto").show();
            $("#lblhrsfrom").text("Hours From :");
        } else if (fval == '3') {
            $("#divisperhr").show();
            $("#divamount").show();
            $("#divdaytype").show();
            $("#divday").show();
            $("#divhrsfrom").show();
            $("#divhrsto").hide();
            $("#lblhrsfrom").text("Hours :");
        }
        $("#divamount").show();
    }


</script>
<uc:Tab runat="server" ID="Tab">
        <Content>
            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                                <%--<asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />--%>
                                &nbsp;
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                                                                
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                                      
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRAllowPolicyNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                                    
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="DTRPolicyTypeDesc" Caption="Policy Type" Visible="True" />    
                                            <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                            <dx:GridViewDataTextColumn FieldName="DTRAllowPolicyDesc" Caption="Description" />
                                            <dx:GridViewDataCheckColumn FieldName="IsApplyToAll" Caption="Apply to all" />                                                          
                                            <dx:GridViewDataTextColumn FieldName="PayScheduleDesc" Caption="Payroll Schedule" />
                                            <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" Visible="false" />  
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />                                        
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                                                              
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />

                                        </Columns>                            
                                    </dx:ASPxGridView>                                
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div> 

            <br /><br />
            <div class="page-content-wrap">            
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-6">
                                <div class="panel-title" >
                                    Transaction No.:&nbsp; <asp:Label runat="server" ID="lblDetl" />
                                </div> 
                            </div>
                            <div>                                  
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>                            
                                </ul>
                                <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="lnkDeleteDetl" />                                                                        
                            </div>                                                                                                   
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="DTRAllowPolicyDetiNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                                    
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                            <%--<dx:GridViewDataTextColumn FieldName="ShiftDesc" Caption="Shift" />    
                                            <dx:GridViewDataTextColumn FieldName="HrsFrom" Caption="Hours From" />
                                            <dx:GridViewDataTextColumn FieldName="HrsTo" Caption="Hours To" />  --%>                                                            
                                            <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />
                                            <dx:GridViewDataTextColumn FieldName="DayTypeDesc" Caption="Day Type" />  
                                            <dx:GridViewDataTextColumn FieldName="DayOffDesc" Caption="Day" />                                                         
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
    
<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">              
                <div class="form-group">
                    <label class="col-md-4 control-label">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCode"  runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" ></asp:TextBox>
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Payroll Group :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayClassNo"  runat="server"  CssClass="required form-control"></asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-sapce">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="chkIsApplyToAll" runat="server" Text="&nbsp;Tick to apply to all employee" ></asp:Checkbox>
                    </div>
                </div>  
                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Policy Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cbodtrpolicytypeNo" DataMember="EDTRPolicyType" runat="server" CssClass="required form-control"></asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtDTRAllowPolicyDesc" TextMode="MultiLine" Rows="4" runat="server" CssClass="required form-control"></asp:Textbox>
                    </div>
                </div>  
                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Payroll Schedule :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayScheduleNo" DataMember="EPaySchedule" runat="server" CssClass="required form-control"></asp:Dropdownlist>
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Income Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayIncomeTypeNo" DataMember="EPayIncomeType" runat="server"  CssClass="required form-control"></asp:Dropdownlist>
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-sapce">Current Salary :</label>
                    <div class="col-md-8">
                        <asp:Textbox ID="txtCurrentSalary" SkinID="txtdate" CssClass="number form-control" runat="server"></asp:Textbox>                                            
                        <br /><code>Equal or above this current salary are entitled for this allowance.</code>
                    </div>
                </div>
                <br />
                
              </div>     
          <!-- Footer here -->
         <br />   
        
    </fieldset>

</asp:Panel>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1"
    CancelControlID="Linkbutton1" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="LinkButton2" CssClass="fa fa-floppy-o submit Fieldset1 lnkSaveDetl" OnClick="lnkSaveDetl_Click"  />      
         </div>
         <!-- Body here -->
          <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Detail No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRAllowPolicyDetiNo" ReadOnly="true" runat="server"  CssClass="form-control" Placeholder="Autonumber" />                        
                </div>
            </div>
           <div class="form-group" id="divshift">
                <label class="col-md-4 control-label">Shift :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNo" DataMember="EShift" runat="server" CssClass="form-control" />
                </div>
            </div>
           <div class="form-group" id="divisperhr">
                <label class="col-md-4 control-label has-space">Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboPolicyDetlTypeNo" CssClass="form-control" onchange="showhidenext(this);">
                        <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                        <asp:ListItem Value="1" Text="Consider Actual hours render multiply to amount defined." />
                        <asp:ListItem Value="2" Text="Consider From and To Hours (Fixed Amount)" />
                        <asp:ListItem Value="3" Text="Consider divisible by hours defined." />
                        <asp:ListItem Value="4" Text="Consider From and To Hours (Actual hours render multiply to amount defined)" />
                    </asp:DropDownList>
                </div>
            </div>
           <div class="form-group" id="divhrsfrom">
                <label class="col-md-4 control-label" id="lblhrsfrom">Hours From :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txthrsFrom" SkinID="txtdate"  runat="server" CssClass="form-control number" />
                </div>
            </div>
           <div class="form-group" id="divhrsto">
                <label class="col-md-4 control-label" id="lblhrsto">Hours To :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtHrsTo" SkinID="txtdate"  runat="server"  CssClass="form-control number" />                    
                 </div>
            </div>
           <div class="form-group" id="divamount">
                <label class="col-md-4 control-label">Amount :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtAmount" SkinID="txtdate"  runat="server" CssClass="form-control number" />                                            
                 </div>
            </div>
           <div class="form-group" id="divdaytype">
                <label class="col-md-4 control-label">Day Type :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDayTypeNo" runat="server" CssClass="form-control"  />                        
                </div>
            </div>
           <div class="form-group" id="divday">
                <label class="col-md-4 control-label">Day :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDayno" DataMember="EDayOff" runat="server" CssClass="form-control" />                        
                </div>
            </div>
           <div class="form-group" id="divtimeout">
                <label class="col-md-4 control-label">Time Out :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtOut1" runat="server" SkinID="txtdate" CssClass="form-control" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtout1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender2"
                        ControlToValidate="txtOut1"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic" />
                </div>
            </div>
          </div>     
          <!-- Footer here -->
         <br />   
        
    </fieldset>

</asp:Panel>

</asp:Content>
