<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpRateSheet_ProjectOutput.aspx.vb" Inherits="Secured_EmpRateSheet_ProjectOutput" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab" HeaderVisible="true">
<Content>
<br />
<script type="text/javascript">

    function disableenable(chk) {
        var fval = chk.value;

        if (fval == '1') {
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").value = "";

            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").disabled = false;

            
        } else if (fval == '2') {
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").value = "";

            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").disabled = false;
        } else if (fval == '3') {
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").disabled = false;
        }  else if (fval == '4') {
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").disabled = false;
        };
    };

    function disableenable_behind(fval) {
     
        if (fval == '1') {
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").value = "";

            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").disabled = false;


        } else if (fval == '2') {
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").value = "";

            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").disabled = false;
        } else if (fval == '3') {
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").disabled = false;
        } else if (fval == '4') {
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtBillingRateHrs").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalaryHrs").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtCurrentSalary").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtBillingRate").disabled = false;
        };
    };


</script>

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <h3>&nbsp;</h3>
                </div>
                <div>
                    &nbsp;                  
                </div> 
                <div>                                                
                    <ul class="panel-controls"> 
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>                                                                                                                                                     
                </div>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSProjectClassRatePYNo">                                                                                   
                        <Columns>    
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BSProjectClassRatePYNo") %>' OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Billing / Payroll Group"/> 
                            <dx:GridViewDataComboBoxColumn FieldName="PYActivityTypeDesc" Caption="Activity Type"/> 
                            <dx:GridViewDataComboBoxColumn FieldName="PYActivityTypeDetiDesc" Caption="Sub Activity Type"/> 
                            <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position"/>                        
                            <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />
                            <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Employee Rate" />                                                        
                            <dx:GridViewDataTextColumn FieldName="BillingRate" Caption="Billing Rate" />   
                            <dx:GridViewDataTextColumn FieldName="DayTypeDesc" Caption="Day Code" />                        
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlDetl" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupDetl" TargetControlID="btnShowDetl" />
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>
                
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Billing/Payroll Group :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayClassNo" runat="server" CssClass="form-control" DataMember="EPayClass" AutoPostBack="true" OnSelectedIndexChanged="cboPayClass_SelectedIndexChanged" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Activity Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPYActivityTypeNo" runat="server" CssClass="form-control" DataMember="EPYActivityType" AutoPostBack="true" OnSelectedIndexChanged="cboActivityType_SelectedIndexChanged" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Sub Activity Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPYActivityTypeDetiNo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboActivityTypeDeti_SelectedIndexChanged"/>                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Position :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPositionNo" runat="server" CssClass="form-control" DataMember="EPosition" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Department :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboDepartmentNo" runat="server" CssClass="form-control" DataMember="EDepartment" />                    
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Location :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboLocationNo" runat="server" CssClass="form-control" DataMember="ELocation" />                    
                </div>
            </div>
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Mode Of Payment :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPYModePaymentDesc" runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
          
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label has-space">RATE PER OUPUT :</label>
                <div class="col-md-7">
                   
                </div>
            </div>
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Employee Rate :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCurrentSalary" runat="server" CssClass="form-control number"></asp:TextBox>
          
                </div>
            </div>
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Billing Rate :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtBillingRate" runat="server" CssClass="form-control number"></asp:TextBox>
                 
                </div>
            </div>
      
            <br />
         
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">RATE PER HOUR :</label>
                <div class="col-md-7">
                   
                </div>
            </div>
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Employee Rate :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCurrentSalaryHrs" runat="server" CssClass="form-control number"></asp:TextBox>
              
                </div>
            </div>
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Billing Rate :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtBillingRateHrs" runat="server" CssClass="form-control number"></asp:TextBox>
                   
                </div>
            </div>
          
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Day Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboDayTypeNo" runat="server" CssClass="form-control" />                    
                </div>
            </div>
                           
        </div>
        <br />
        </fieldset>
    </asp:Panel>

</Content>
</uc:Tab>
<br /><br />
</asp:Content>