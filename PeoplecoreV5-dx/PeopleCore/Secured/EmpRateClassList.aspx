<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="false" CodeFile="EmpRateClassList.aspx.vb" Inherits="Secured_EmpRateClassList" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                    
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control"  runat="server" />            
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>
                    </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeRateClassNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="EmployeeRateClassCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeRateClassDesc" Caption="Decription" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="CalendarYear" Caption="Calendar Days" />
                                <dx:GridViewDataTextColumn FieldName="xIsDaily" Caption="Daily" /> 
                                <dx:GridViewDataCheckColumn FieldName="IswithPayHol" Caption="With Holiday Pay" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" />
                                <dx:GridViewDataCheckColumn FieldName="IsAbsDeduct" Caption="With Abs Deduction</br> on Holiday" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />  
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" /> 
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" />                                             
                            </Columns>                            
                        </dx:ASPxGridView>    
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                    
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtEmployeeRateClassCode" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtEmployeeRateClassDesc" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Calendar year :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCalendarYear" runat="server" CssClass="required number form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Tick for Daily Paid Computation :</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkIsDaily" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Tick to pay additional holiday  :</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkIswithPayHol" runat="server" />
                </div>
            </div>
<%--            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Hourly? :</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkIsHourly" runat="server" />
                </div>
            </div>--%>
            <div class="form-group">
                <label class="col-md-4 control-label">Tick to deduct absences on holiday :</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkIsAbsDeduct" runat="server" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label"> Tich to Archive :</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkIsArchived" runat="server" />
                </div>
            </div>
            
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label">Company :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" CssClass="form-control" runat="server" />
                </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
</asp:Content>