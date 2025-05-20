<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpSalaryLevel.aspx.vb" Inherits="Secured_EmpSalaryLevel" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">    
 <br />
    <div class="page-content-wrap" >         
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                    </div>
                    <div>   
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                                              
                        <ul class="panel-controls">                                    
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />
                        </ul>                                                                                                                                                     
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="SalaryGradeNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." /> 
                                    <dx:GridViewDataTextColumn FieldName="SalaryGradeCode" Caption="Code" />                                                                                
                                    <dx:GridViewDataTextColumn FieldName="SalaryGradeDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="MinimumSalaryM" Caption="Minimum Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="MidPointSalaryM" Caption="Median Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="MaximumSalaryM" Caption="Maximum Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Rata" Caption="RA" PropertiesTextEdit-DisplayFormatString="{0:N2}"  Visible="false"/>
                                    <dx:GridViewDataTextColumn FieldName="TA" Caption="TA" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                                    <dx:GridViewDataCheckColumn FieldName="IsNoOT" Caption="Not Entitled for OT" /> 
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                    <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" /> 
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                                           
                        </div>
                    </div>                                                           
                </div>                   
            </div>
        </div>
    </div>

    
       
<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlpopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" Enabled="false" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtSalaryGradeCode" runat="server" CssClass="required form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtSalaryGradeDesc" runat="server" CssClass="required form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Minimum Salary :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMinimumSalaryM" runat="server" CssClass="number form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Median Salary :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMidpointSalaryM" runat="server" CssClass="number form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Maximum Salary :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMaximumSalaryM" runat="server" CssClass="number form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">RA Amount :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRata" runat="server" CssClass="number form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">TA Amount :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTA" runat="server" CssClass="number form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="txtIsNoOT" Text="&nbsp;Check here if not entitled for overtime." />                        
                    </div>
            </div
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="txtIsArchived" Text="&nbsp;Archive" />                        
                    </div>
            </div> 
             > 

            <div class="form-group" style="visibility:hidden;">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="txtIsApplyToAll" runat="server" />  &nbsp;
                        <span >if visible to all company. </span>
                </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         </fieldset>
</asp:Panel> 
</asp:Content>

