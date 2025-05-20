<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="DTRHolidayEditDaytype.aspx.vb" Inherits="Secured_DTRHolidayEditDaytype" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab" HeaderVisible="true">
    <Header>
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
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HolidayDayTypeNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                        <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group"/>
                                        <dx:GridViewBandColumn Caption="Ovt Factor Rate (Regular)" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Ovt" Caption="Ovt" PropertiesTextEdit-DisplayFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"/>
                                                <dx:GridViewDataTextColumn FieldName="Ovt8" Caption="Ovt8" PropertiesTextEdit-DisplayFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"/>
                                                <dx:GridViewDataTextColumn FieldName="NPOvt" Caption="NP" PropertiesTextEdit-DisplayFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"/>
                                                <dx:GridViewDataTextColumn FieldName="NPOvt8" Caption="NP8" PropertiesTextEdit-DisplayFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"/>
                                            </Columns>
                                        </dx:GridViewBandColumn> 
                                        <dx:GridViewBandColumn Caption="OVt Factor Rate (Rest Day)" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="OvtR" Caption="Ovt" PropertiesTextEdit-DisplayFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"/>
                                                <dx:GridViewDataTextColumn FieldName="Ovt8R" Caption="Ovt8" PropertiesTextEdit-DisplayFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"/>
                                                <dx:GridViewDataTextColumn FieldName="NPOvtR" Caption="NP" PropertiesTextEdit-DisplayFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"/>
                                                <dx:GridViewDataTextColumn FieldName="NPOvt8R" Caption="NP8" PropertiesTextEdit-DisplayFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"/>
                                            </Columns>
                                        </dx:GridViewBandColumn>   
                            
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

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
        <fieldset class="form" id="fsDetl1">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsDetl1 lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
            

           <div class="form-group">
                <label class="col-md-4 control-label has-required">Payroll Group :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayClassNo" DataMember="EPayclass"   runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboPayClassNo_SelectedIndexChanged" />                                                                                           
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Day Type (2nd) :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPDayTypeNo"   runat="server" CssClass="form-control required" />                                                                                           
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">FACTOR RATE (Regular Shift):</label>
                <div class="col-md-7">
                                                                                                           
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Ovt :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOvt" runat="server" CssClass="number form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Ovt8 :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOvt8" runat="server" CssClass="number form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">NP :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNPOvt" runat="server" CssClass="number form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">NP8 :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNPOvt8" runat="server" CssClass="number form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">FACTOR RATE (Rest Day):</label>
                <div class="col-md-7">
                                                                                                           
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Ovt :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOvtR" runat="server" CssClass="number form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Ovt8 :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOvt8R" runat="server" CssClass="number form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">NP :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNPOvtR" runat="server" CssClass="number form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">NP8 :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNPOvt8R" runat="server" CssClass="number form-control required" />
                </div>
            </div>

        </div>
        <br />
        </fieldset>
</asp:Panel>

</asp:Content>

