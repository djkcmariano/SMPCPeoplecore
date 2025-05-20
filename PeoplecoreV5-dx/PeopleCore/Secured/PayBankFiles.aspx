<%@ Page Language="VB" AutoEventWireup="false"  Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayBankFiles.aspx.vb" Inherits="Secured_PayBankFiles" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }

</script>
<br />

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-3">
                     <asp:Dropdownlist ID="cboCompNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearchGrp_Click" CssClass="form-control" runat="server"/>
                     <asp:Dropdownlist ID="cboGrpNo" AutoPostBack="true" CssClass="form-control" runat="server"/>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label has-required">Pay End Date :</label>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtPayDate" CssClass="form-control required" AutoPostBack="true" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtPayDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtPayDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtPayDate" Display="Dynamic" />
                    </div>
                </div>
                <br/>
                <div class="form-group">
                    <label class="col-md-2 control-label has-required">Credit Date :</label>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtCreditDate" CssClass="form-control required" AutoPostBack="true" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtCreditDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtCreditDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtCreditDate" Display="Dynamic" />
                    </div>
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" SkinID="grdDX" runat="server" KeyFieldName="Code"  >                                                                                   
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption = "Bank No"/> 
                                    <dx:GridViewDataTextColumn FieldName="Bank" Caption="Bank" />                                            
                                    <dx:GridViewDataTextColumn FieldName="PayGrp" Caption="Payroll Group" />
                                    <dx:GridViewDataTextColumn FieldName="FileType" Caption="File Type" />
                                    <dx:GridViewDataTextColumn FieldName="FileName" Caption="File Name" />
                                    <dx:GridViewDataTextColumn FieldName="Count" Caption="Emp Count" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="2%">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" OnPreRender="lnk_View" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                                                                                                 
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                        </div>
                    </div>                                               
                </div> 
                <div class="col-md-2">
                    <asp:Button runat="server" ID="lnkView" OnClick="lnkView_Click" CssClass="btn btn-default submit fsMain" Text="View" />  
                </div>  
            </div>
        </div>
    </div>
</div>
</asp:Content>
