<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRPolicy_EmpAllow.aspx.vb" Inherits="Secured_DTRPolicy_EmpAllow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">

    function disableenable(chk) {
        var fval = chk.checked;

        if (chk.checked) {

            document.getElementById("ctl00_cphBody_txtIsIncludeRegOT").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeRegHol").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeSpeHol").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeRD").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_txtIsIncludeRegOT").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeRegOT").checked = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeRegHol").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeRegHol").checked = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeSpeHol").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeSpeHol").checked = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeRD").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeRD").checked = false;

     
        }
    }

    function disableenable_behind(val) {
        var fval = val;

        if (fval=="true") {

            document.getElementById("ctl00_cphBody_txtIsIncludeRegOT").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeRegHol").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeSpeHol").disabled = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeRD").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_txtIsIncludeRegOT").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeRegOT").checked = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeRegHol").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeRegHol").checked = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeSpeHol").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeSpeHol").checked = false;
            document.getElementById("ctl00_cphBody_txtIsIncludeRD").disabled = true;
            document.getElementById("ctl00_cphBody_txtIsIncludeRD").checked = false;


        }
    }

</script>

<br />
<uc:Tab runat="server" ID="Tab">
    <Content>
        <br />
        <div class="page-content-wrap">            
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-6">
                            <div class="panel-title" >
                                Payroll Group
                            </div> 
                        </div>
                        <div>                                  
                    
                        </div>                                                                                                   
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayClassNo">                                                                                   
                                    <Columns>
                
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                        <dx:GridViewDataTextColumn FieldName="PayClassCode" Caption="Code" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Description" />                                
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
        <br /><br />
        <div class="page-content-wrap">            
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-6">
                            <div class="panel-title" >
                                Reference No.:&nbsp; <asp:Label runat="server" ID="lblDetl" />
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
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="PayClassDTRRefEmpAllowNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                                
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />     
                                        <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />       
                                        <dx:GridViewDataCheckColumn FieldName="IsAtleastWithDTR" Caption="At least one DTR only." HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                        <dx:GridViewDataCheckColumn FieldName="IsIncludeBonus" Caption="Include in 13month/Bonus" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                        <dx:GridViewDataCheckColumn FieldName="IsDTRBase" Caption="DTR Base" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                        <dx:GridViewDataCheckColumn FieldName="IsIncludeRegOT" Caption="Include Regular OT" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                        <dx:GridViewDataCheckColumn FieldName="IsIncludeRegHol" Caption="Include Regular Holiday" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                        <dx:GridViewDataCheckColumn FieldName="IsIncludeSpeHol" Caption="Include Special Holiday" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                        <dx:GridViewDataCheckColumn FieldName="IsIncludeRD" Caption="Include Rest Day" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />

                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />                            
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

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsDetail">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetail" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>        
           
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Income Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist runat="server" ID="cboPayIncomeTypeNo"  DataMember="EPayIncomeType"  CssClass="form-control required"/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsOT" Text="&nbsp;Tick to include allowance in hourly rate computation of overtime/NP/Leave/Holiday Pay" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsAtleastWithDTR" Text="&nbsp;Tick to enable allowance to those who have at least 1 day DTR" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsIncludeBonus" Text="&nbsp;Tick to include allowance in bonus/13th month" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsDTRBase" Text="&nbsp;Tick to prorate allowance based on DTR" onclick="disableenable(this);" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsIncludeRegOT" Text="&nbsp;Tick to include Regular Overtime" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsIncludeRD" Text="&nbsp;Tick to include Rest Day Overtime" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsIncludeRegHol" Text="&nbsp;Tick to include Regular Holiday" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsIncludeSpeHol" Text="&nbsp;Tick to include Special Holiday" />
                </div>
            </div>
            <br />
        </div>                    
    </fieldset>
</asp:Panel>
</asp:Content>
