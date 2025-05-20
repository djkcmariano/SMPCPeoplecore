<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BenBenefitHMOPolicy.aspx.vb" Inherits="Secured_BenBenefitHMOPolicy" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<br />

<div class="page-content-wrap">         
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
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Are you sure you want to archive selected item(s)?"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitHMOPolicyNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Referrence No." />
                                <dx:GridViewDataComboBoxColumn FieldName="BenefitHMOPlanTypeDesc" Caption="HMO Plan Bracket" />
                                <dx:GridViewDataComboBoxColumn FieldName="CivilStatDesc" Caption="Civil Status" /> 
                                <dx:GridViewDataComboBoxColumn FieldName="RelationshipDesc" Caption="Relationship" />
                                <dx:GridViewDataTextColumn FieldName="OrderNo" Caption="Order Level" />
                                <dx:GridViewDataTextColumn FieldName="FromAge" Caption="From Age" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="ToAge" Caption="To Age" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataCheckColumn FieldName="IsDisabled" Caption="Family member is incapacitated/PWD" />   
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />                                     
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

    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none" >
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        `
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">HMO Plan Bracket :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboBenefitHMOPlanTypeNo" DataMember="EBenefitHMOPlanType" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Civil Status :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboCivilStatNo" DataMember="ECivilStat" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Relationship :</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cboRelationshipNo" runat="server" DataMember="ERelationship" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Order Level :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtOrderNo" runat="server" CssClass="form-control number required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">From Age :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtFromAge" runat="server" CssClass="form-control number required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">To Age :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtToAge" runat="server" CssClass="form-control number required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsDisabled" Text="&nbsp;Tick to tag family member as incapacitated/PWD" />
                    </div>
                </div>
                  
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>                       
                               
            </div>                    
        </fieldset>
    </asp:Panel>
    
    
</asp:Content>

