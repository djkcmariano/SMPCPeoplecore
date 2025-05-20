<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEMeritDetiList.aspx.vb" Inherits="Secured_PEMeritDetiList" %>


<asp:Content id="Content2" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-4">
                        
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkPostHRAN" OnClick="lnkPostHRAN_Click" Text="Post to HRAN" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" Visible="false" /></li>                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Exclude" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be excluded in increase/bonus. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkPostHRAN" ConfirmMessage="Are you sure you want to post to HRAN?"  />
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXTotal" KeyFieldName="PEMeritDetiNo" >                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No."  Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="JobGradeDesc" Caption="Job Grade" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />                                
                                    <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Business Unit" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Old Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}"/>
                                    <dx:GridViewDataTextColumn FieldName="AveRating" Caption="Rating" />
                                    <dx:GridViewBandColumn Caption="Merit Value" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="PercentIncrease" Caption="Percent" />
                                            <dx:GridViewDataTextColumn FieldName="MeritAmount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}"/>
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="Adjustment" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="PercentIncreaseAdj" Caption="Percent" />
                                            <dx:GridViewDataTextColumn FieldName="AmountIncreaseAdj" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}"/>
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewDataTextColumn FieldName="MinSalary" Caption="Entry Rate" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MedSalary" Caption="Median" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                                    <dx:GridViewBandColumn Caption="New Salary" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="AliceBlue">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="PercentIncreaseFinal" Caption="Percent" HeaderStyle-BackColor="AliceBlue" />
                                            <dx:GridViewDataTextColumn FieldName="NewSalary" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-BackColor="AliceBlue" />
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Exc." Width="2%" />
                                </Columns>  
                                <Settings ShowGroupFooter="VisibleIfExpanded" ShowFooter="true" />  
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="FullName" SummaryType="Count" />
                                    <dx:ASPxSummaryItem FieldName="CurrentSalary" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="AveRating" SummaryType="Average"/>
                                    <dx:ASPxSummaryItem FieldName="PercentIncrease" SummaryType="Average"/>
                                    <dx:ASPxSummaryItem FieldName="MeritAmount" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="PercentIncreaseAdj" SummaryType="Average"/>
                                    <dx:ASPxSummaryItem FieldName="AmountIncreaseAdj" SummaryType="Custom" />
                                    <dx:ASPxSummaryItem FieldName="PercentIncreaseFinal" SummaryType="Average" />
                                    <dx:ASPxSummaryItem FieldName="NewSalary" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="NewSalary" SummaryType="Custom" />
                                </TotalSummary>
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                    <%--<dx:ASPxSummaryItem FieldName="MeritAmount" ShowInGroupFooterColumn="MeritAmount" SummaryType="Sum" />--%>
                                </GroupSummary>
                                               
                            </dx:ASPxGridView>
                            

                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>

 </div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlMain" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupMain" TargetControlID="btnShowMain">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup"  style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEMeritDetiNo" CssClass="form-control" runat="server" ReadOnly="true" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" CssClass="form-control" runat="server" ReadOnly="true" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:Checkbox ID="txtIsExcluded" runat="server" Text="&nbsp; Please check here if employee is excluded in merit."></asp:Checkbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Employee Name :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFullName" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Percent Adjustment :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtPercentIncreaseAdj" runat="server" CssClass="form-control" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtPercentIncreaseAdj" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Amount Adjustment :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtAmountIncreaseAdj" runat="server" CssClass="form-control" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtAmountIncreaseAdj" />
                </div>
            </div>

            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
   

</asp:Content>