<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="false" CodeFile="EmpProvinceList.aspx.vb" Inherits="Secured_EmpProvinceList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
<script type="text/javascript">
    function SelectAllCheckboxes(spanChk) {

        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtIsSelect") > 0 &&
            elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }     
</script>

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" /> 
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>    
                                <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>                                                
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible ="false"/></li>
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ProvinceNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ProvinceNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="ProvinceCode" Caption="Code"/>
                                    <dx:GridViewDataTextColumn FieldName="ProvinceDesc" Caption="Description" />
                                    <dx:GridViewDataComboBoxColumn FieldName="RegionDesc" Caption="Region" />  
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                    <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company"/>                             
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
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtProvinceNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtProvinceCode" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtProvinceDesc" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Region :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboRegionNo" DataMember="ERegion" runat="server" CssClass="required form-control" />
                </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>
            <br />
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">
                Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass="number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div> 
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
</asp:Content>