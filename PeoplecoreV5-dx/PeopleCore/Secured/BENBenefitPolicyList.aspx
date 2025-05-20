<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BENBenefitPolicyList.aspx.vb" Inherits="Secured_BENBenefitPolicyList" %>



<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">


<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6">
                        
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitPolicyNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="BenefitPolicyDesc" Caption="Description" />
                                    <dx:GridViewDataComboBoxColumn FieldName="BenefitTypeDesc" Caption="Benefit Type" />                                    
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" />                                    
                                    <%--<dx:GridViewDataCheckColumn FieldName="IsSuspended" Caption="Suspended?" />--%>
                                    <dx:GridViewDataComboBoxColumn FieldName="SalaryGradeDesc" Caption="Salary Grade" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="JobGradeDesc" Caption="Job Grade" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DateBaseDesc" Caption="Date Base" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DateModeDesc" Caption="Date Mode" Visible="false" />                                    
                                    <%--<dx:GridViewDataCheckColumn FieldName="IsApplyToAll" Caption="Apply to All?" />--%>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
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
 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6 panel-title">
                        <asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="BenefitPolicyDetiNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remarks" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />
                                    <dx:GridViewDataTextColumn FieldName="FromYear" Caption="From Year" />
                                    <dx:GridViewDataTextColumn FieldName="ToYear" Caption="To Year" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>   

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground">
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
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
                <div class="col-md-6">
                    <asp:TextBox ID="txtLeavePolicyCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hifBenefitPolicyNo" />
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Benefit Type :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboBenefitTypeNo" runat="server" CssClass="required form-control" DataMember="EBenefitType">
                    </asp:DropdownList>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtBenefitPolicyDesc" TextMode="MultiLine" Rows="2" runat="server" CssClass="required form-control"></asp:TextBox>
                </div>
            </div>
     
            <div class="form-group">
                <label class="col-md-4 control-label">Payroll Group :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboPayclassNo"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Employee Status :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Employee Class :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Job Grade :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboJobGradeNo" DataMember="EJobgrade" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Rank :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboRankNo" DataMember="ERank"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label">Position :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboPositionNo" DataMember="EPosition"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label">Group :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboGroupNo" DataMember="EGroup"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Date Mode :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboDateModeNo" DataMember="EDateMOde" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>                 
            <div class="form-group">
                <label class="col-md-4 control-label">Date Base :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboDateBaseNo" DataMember="EDateBase"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">No. of months in service :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtnoofmonth" runat="server" SkinID="txtDate" CssClass="number form-control"></asp:TextBox> 
      
                </div>
            </div>                  
      
            <div class="form-group">
                <label class="col-md-4 control-label">Benefit Credit :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtBenefitCreditCal" runat="server" SkinID="txtDate" CssClass="number form-control"></asp:TextBox>   
        
               </div>
            </div>                  
                 
            <br />
        </div>
        
         </fieldset>
</asp:Panel>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server"  CssClass="entryPopup2">
       <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsDetl btnSaveDetl" OnClick="btnSaveDetl_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl2 form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtBenefitPolicyDetiNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Remark :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRemark" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control"
                    ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtAmount" SkinID="txtdate" CssClass="number required form-control" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From year :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtfromYear" SkinID="txtdate" CssClass="number required form-control" runat="server" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">To year :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txttoyear" SkinID="txtdate" CssClass="number required form-control" runat="server" />                        
                </div>
            </div>
        </div>
        
         </fieldset>
</asp:Panel>
</asp:Content>
