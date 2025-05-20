<%@ Page Title=""  Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppVoluntaryEdit.aspx.vb" Inherits="Secured_AppVoluntaryEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />                
            </center>            
            <asp:Label runat="server" ID="lbl" /> 
        </Header>        
        <Content>
            <br />            
            <div class="page-content-wrap">         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                                <h4>&nbsp;</h4>                                
                            </div>
                            <div>
                                <div>                                                                                    
                                    <ul class="panel-controls">                                                                                                                        
                                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                                
                                    </ul>                                    
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                    
                                </div>                                
                            </div>                           
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <i>Voluntary Work or Involvement in Civic/ Non-Government / People / Voluntary Organization/s</i>
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="ApplicantVolunterNo" SkinID="grdDX">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ApplicantVolunterNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="Organization" Caption="Organization" />
                                            <dx:GridViewDataTextColumn FieldName="xDateFrom" Caption="From" />
                                            <dx:GridViewDataTextColumn FieldName="xDateTo" Caption="To" />
                                            <dx:GridViewDataTextColumn FieldName="NoOfHours" Caption="No. of Hours" />
                                            <dx:GridViewDataTextColumn FieldName="Position" Caption="Position / Nature of Work" />                                            
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />                                           
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
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtApplicantVolunterNo" ReadOnly="true" CssClass="form-control"  />
                </div>
            </div>     

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtApplicantVolunterCode" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"  />
                </div>
            </div>                     
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Organization :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtOrganization" CssClass="form-control required"  TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Address :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>
            <%--<div class="form-group">
                <label class="col-md-4 control-label has-space">Date From:</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtDateFrom" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="txtDateFrom" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateFrom" Display="Dynamic" />                            
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date To :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender4" TargetControlID="txtDateTo" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender4" TargetControlID="txtDateTo" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator4" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateTo" Display="Dynamic" />                            
                </div>
            </div>--%>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date From :</label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="cboFromMonth" CssClass="form-control required" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="cboFromDay" CssClass="form-control required" />                   
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFromYear" CssClass="form-control required" placeholder="Year" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtFromYear" />                               
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsPresent" Text="&nbsp;Tick if voluntary work is up to present" OnCheckedChanged="chkIsPresent_CheckedChanged" AutoPostBack="true" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date To :</label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="cboToMonth" CssClass="form-control required" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="cboToDay" CssClass="form-control required" />                    
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtToYear" CssClass="form-control required" placeholder="Year" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtToYear" />                     
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">No. of Hours :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtNoOfHours" CssClass="form-control number"  />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Position / Nature of Work :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtPosition" CssClass="form-control" TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>
        </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

