<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master"
    CodeFile="EmpCityList.aspx.vb" Inherits="Secured_EmpCityList" %>


<asp:Content ID="Cont1" ContentPlaceHolderID="cphbody" runat="server">
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
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false"/></li>
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="CityNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("CityNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="CityCode" Caption="Code"/>
                                    <dx:GridViewDataTextColumn FieldName="CityDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="PostalCode" Caption="Zip Code" />
                                    <dx:GridViewDataComboBoxColumn FieldName="ProvinceDesc" Caption="Province" /> 
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By"  Visible="false"/> 
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date"  Visible="false"/> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                    <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" Visible="false" />                                
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

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCityNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCityCode" runat="server"  CssClass="required form-control"/>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCityDesc" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Zip Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPostalCode" runat="server" CssClass="required form-control"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Province :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboProvinceNo"  runat="server" DataMember="EProvince" CssClass="required form-control" />
               </div>
            </div> 
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">
                Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div>            
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="txtIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>  
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
</asp:Content>
