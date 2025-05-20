<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRPolicy_LateUnder.aspx.vb" Inherits="Secured_DTRRefPolicyLateUnder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
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
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="PayClassDTRRefNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                                
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Employee Class" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employee Status" />                                                                
                                        <dx:GridViewDataTextColumn FieldName="MinLate" Caption="Late <br /> Adjustment" /> 
                                        <dx:GridViewDataTextColumn FieldName="FractionLate" Caption="Late Fraction" /> 
                                        <dx:GridViewDataTextColumn FieldName="MinUT" Caption="Undertime <br /> Adjustment" /> 
                                        <dx:GridViewDataTextColumn FieldName="FractionUT" Caption="Undertime <br> Fraction" /> 
                                        <dx:GridViewDataTextColumn FieldName="MaxLate" Caption="Late (Cut-Off 1)" />                                                                
                                        <dx:GridViewDataTextColumn FieldName="MaxLate2" Caption="Late (Cut-Off 2)" /> 
                                        <dx:GridViewDataTextColumn FieldName="RoundOfLate" Caption="Round Off Late" /> 
                                        <dx:GridViewDataTextColumn FieldName="MaxUT" Caption="UT (Cut-Off 1)" />                                                                
                                        <dx:GridViewDataTextColumn FieldName="MaxUT2" Caption="UT (Cut-Off 2)" /> 
                                        <dx:GridViewDataTextColumn FieldName="RoundOfUT" Caption="Round Off UT" /> 
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
                    <asp:Textbox ID="txtCodeDetl" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>       
            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsApplyToAll" Text="&nbsp;Check to apply to all employees" />
                </div>
            </div>                 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Employee Classification :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control" />
                </div>
            </div>                                     
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Employee Status :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control" />
                </div>
            </div>
            <br />
            <h5><b>ADJUSTMENT</b></h5>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Late :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMinLate" runat="server" CssClass="form-control number"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Undertime :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMinUT" runat="server" CssClass="form-control number"  />
                </div>
            </div>
            <br />
            <h5><b>FRACTION</b></h5>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Late (in minutes) :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFractionLate" runat="server" CssClass="form-control number"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Undertime (in minutes) :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFractionUT" runat="server" CssClass="form-control number"  />
                </div>
            </div>
            <br />
            <h5><b>ROUNDING OF LATE</b></h5>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Cut-off 1 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMaxLate" runat="server" CssClass="form-control number"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Cut-off 2 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMaxLate2" runat="server" CssClass="form-control number"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Round Off :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRoundOfLate" runat="server" CssClass="form-control number"  /><br /><br />
                    <asp:RadioButton runat="server" ID="txtIsRoundofLateHalf" GroupName="grproundOflate" Text="&nbsp; Check to round off to half-day." /><br />
                    <asp:RadioButton runat="server" ID="txtIsRoundofLateWhole" GroupName="grproundOflate" Text="&nbsp; Check to round off to whole day." />
                </div>
            </div>
            <br />
            <h5><b>ROUNDING OF UNDERTIME</b></h5>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Cut-off 1 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMaxUT" runat="server" CssClass="form-control number"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Cut-off 2 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMaxUT2" runat="server" CssClass="form-control number"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Round Off :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRoundOfUT" runat="server" CssClass="form-control number"  /> <br /><br />
                    <asp:RadioButton runat="server" ID="txtIsRoundofUTHalf" GroupName="grproundOfUT" Text="&nbsp; Check to round off to half-day." /><br />
                    <asp:RadioButton runat="server" ID="txtIsRoundofUTWhole" GroupName="grproundOfUT" Text="&nbsp; Check to round off to whole day." />
                </div>
            </div>
            <br />
            
            
            <br />
        </div>                    
    </fieldset>
</asp:Panel>
</asp:Content>
