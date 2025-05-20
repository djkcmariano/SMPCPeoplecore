<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmployeeDepe.aspx.vb" Inherits="Secured_EmpDepeList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:TabSelf runat="server" ID="TabSelf" HeaderVisible="true">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />
            </center>
            <asp:Label runat="server" ID="lbl" />
        </Header>
        <Content>
            <br />
            <div class="page-content-wrap" >
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                            &nbsp;
                            </div>
                            <div>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeDepeNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeDepeNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="code" Caption="Record No." />
                                            <dx:GridViewDataTextColumn FieldName="pFullname" Caption="Name" />
                                            <dx:GridViewDataTextColumn FieldName="RelationshipDesc" Caption="Relationship" />
                                            <dx:GridViewDataTextColumn FieldName="BirthDate" Caption="Birth Date" />
                                            <dx:GridViewDataTextColumn FieldName="CivilStatDesc" Caption="Civil Status" />
                                            <dx:GridViewDataTextColumn FieldName="PhoneNo" Caption="Contact No." />
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
    </uc:TabSelf>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
        <fieldset class="form" id="fsMain">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
                &nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>
            <div class="entryPopupDetl form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:HiddenField runat="server" ID="hifEmployeeDepeNo" />
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder= "Autonumber"/>
                    </div>
                </div>
                <h5> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <b>NOTE : &nbsp; For immediate family member only : </b></h5>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Relationship :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboRelationShipNo" DataMember="ERelationship" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Surname :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">First Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Middle Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Maiden Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtMaidenName" CssClass="form-control"  />                    
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Name Extension :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboEmployeeExtNo" DataMember="EEmployeeExt" CssClass="form-control" />
                    </div>
                    <div style="color:Red;">*<em style="color:Black;">(JR.,SR.)</em></div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Date of Birth :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender
                         runat="server" ID="CalendarExtender1" TargetControlID="txtBirthDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtBirthDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtBirthDate" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Civil Status :</label>
                    <div class="col-md-7">
                        <asp:DropDownList runat="server" ID="cboCivilStatNo" DataMember="ECivilStat" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Contact No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Home Address :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtHomeAddress" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Employer / Business Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtEmployerName" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Business Address :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtEmployerAdd" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Occupation / Position :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtOccupation" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                </div>                                
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Employer Phone No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtEmployerTelNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label> 
                    <%--Assign As Dependent :--%>
                    <div class="col-md-7">
                        <%--<asp:RadioButtonList runat="server" ID="rblIsDependent" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100">
                            <asp:ListItem  Value="1" Text="Yes" />
                            <asp:ListItem  Value="0" Text="No" />
                        </asp:RadioButtonList>--%>
                        <asp:CheckBox runat="server" ID="chkIsDependent" Text="&nbsp;Assign as dependent" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <%--Assign As Beneficiary :--%>
                    <div class="col-md-7">
                        <%--<asp:RadioButtonList runat="server" ID="rblIsBeneficiary" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100">
                            <asp:ListItem  Value="1" Text="Yes" />
                            <asp:ListItem  Value="0" Text="No" />
                        </asp:RadioButtonList>--%>
                        <asp:CheckBox runat="server" ID="chkIsBeneficiary" Text="&nbsp;Assign as beneficiary" />

                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <%--Has Own Insurance :--%>
                    <div class="col-md-7">
                        <%--<asp:RadioButtonList runat="server" ID="rblIsInsurance" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100">
                            <asp:ListItem  Value="1" Text="Yes" />
                            <asp:ListItem  Value="0" Text="No" />
                        </asp:RadioButtonList>--%>
                        <asp:CheckBox runat="server" ID="chkIsWithHMO" Text="&nbsp;Assign as HMO/MIS dependent" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <%--Has Own Insurance :--%>
                    <div class="col-md-7">
                        <%--<asp:RadioButtonList runat="server" ID="rblIsInsurance" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100">
                            <asp:ListItem  Value="1" Text="Yes" />
                            <asp:ListItem  Value="0" Text="No" />
                        </asp:RadioButtonList>--%>
                        <asp:CheckBox runat="server" ID="chkIsInsurance" Text="&nbsp;Family member has own insurance" />
                    </div>
                </div>
                <br />
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>