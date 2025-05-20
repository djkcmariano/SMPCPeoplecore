<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DTRPolicy_Application.aspx.vb" Inherits="Secured_DTRApplicationPolicy" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
    <Content>
        <br />
        <div class="page-content-wrap">         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">                                
                        <h3 class="panel-title">&nbsp;</h3>
                         <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                    
                            <ul class="panel-controls">
                        
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" /></li>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                    
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRApplicationPolicyNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("DTRApplicationPolicyNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" />                                                                          
                                        <dx:GridViewDataComboBoxColumn FieldName="ApproverCodeDesc" Caption="Application" />
                                        <dx:GridViewDataTextColumn FieldName="ModeOfApplication" Caption="Mode of application" />  
                                        <dx:GridViewDataComboBoxColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />    
                                        <dx:GridViewDataTextColumn FieldName="Days" Caption="No. Of days" />
                                        <dx:GridViewDataTextColumn FieldName="DaysMsg" Caption="Msg Display" />               
                                        <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Minimum Hours" />
                                        <dx:GridViewDataTextColumn FieldName="HrsMsg" Caption="Minimum Hours Msg" Visible="false" />  
                                        <dx:GridViewDataTextColumn FieldName="MinLengthService" Caption="Length of Service" />
                                        <dx:GridViewDataTextColumn FieldName="MinLengthServiceMsg" Caption="Length of Service Msg" Visible="false" />                                                               
                                        <dx:GridViewDataTextColumn FieldName="DaysPrior" Caption="Day(s) Prior" Visible="false" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="DaysPriorMsg" Caption="Day(s) Prior Msg" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="DaysAfter" Caption="Day(s) Beyond" Visible="false" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="DaysAfterMsg" Caption="Day(s) Beyond Msg" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoder" />  
                                        <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" Visible="false" /> 
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
    </Content>
</uc:Tab> 
<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4></h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDTRApplicationPolicyNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" ></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Payroll Group :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayclassNo" DataMember="EPayClass" runat="server" CssClass="form-control"
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Application :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboApproverCodeNo" DataMember="EApproverCodeL" runat="server" OnSelectedIndexChanged="cboApproverCodeNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control required"
                        ></asp:Dropdownlist>
               </div>
            </div>
            
            <asp:PlaceHolder runat="server" ID="phleave" Visible="false">
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Leave Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboLeavetypeNo" DataMember="ELeaveType" runat="server" CssClass="form-control"
                            ></asp:Dropdownlist>
                   </div>
                </div>
                <br />
                <h5><b>Filed Hours</b></h5> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-space" runat="server" id="lblminhrs">Minimum Hrs :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtHrs" SkinID="txtdate" runat="server" CssClass="form-control number" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space"> Message to display :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtHrsMsg" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control" 
                            ></asp:Textbox>
                    </div>
                </div>
                <br />
                <h5><b>Length of Service</b></h5> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Minimum Length of Service (month/s) :</label>
                    <div class="col-md-7">
                         <asp:TextBox ID="txtMinLengthService" SkinID="txtdate" runat="server" CssClass="form-control number" ></asp:TextBox>                       
                   </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Message to display :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtMinLengthServiceMsg" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control" 
                            ></asp:Textbox>
                    </div>
                </div>

            </asp:PlaceHolder>
            <br />
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Mode of Application :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboDTRApplicationPolicyModeNo" runat="server" OnSelectedIndexChanged="cboApproverCodeNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control required" >
                        <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                        <asp:ListItem Value="1" Text="Days Prior" />
                        <asp:ListItem Value="2" Text="Days Beyond" />
                        <asp:ListItem Value="3" Text="After the Cut-Off" />
                    </asp:Dropdownlist>
               </div>
            </div>

            <br />
           
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Minimum length of period (in days) of application  :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDays" SkinID="txtdate" runat="server" CssClass="form-control number" ></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Message to display :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDaysMsg" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>


            <asp:PlaceHolder runat="server" ID="PlaceHolder1" Visible="false">
            <br />
            <h5><b>Day(s) Prior</b></h5> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Minimum length of period (in days) of application prior to effective date :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDaysPrior" SkinID="txtdate" runat="server" CssClass="form-control number" ></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Message to display :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDaysPriorMsg" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>
            <br />
            <h5><b>Day(s) Beyond</b></h5> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Maximum length of period (in days) of application beyond the effective date :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDaysAfter" SkinID="txtdate" runat="server" CssClass="form-control number" ></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"> Message to display :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDaysAfterMsg" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>
            </asp:PlaceHolder>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div> 
            <br />
        </div>
        
         </fieldset>
</asp:Panel>

</asp:Content>

