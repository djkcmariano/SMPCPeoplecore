<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="CliDoctorList.aspx.vb" Inherits="Secured_CliDoctorList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }

    </script>
<div class="page-content-wrap">         
    <div class="row">
        <div class="col-md-12">
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
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul> 
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                    </asp:UpdatePanel> 
                </div>                          
                </div>
                <div class="panel-body">
                    <%--<div class="row">
                        <div class="table-responsive">
                           <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="DoctorNo" >
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEdit_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("DoctorNo") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Reference No."     >
                                            <HeaderStyle Width="8%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
        
                                        <asp:BoundField DataField="fullname" HeaderText="Name" ItemStyle-HorizontalAlign="left" SortExpression ="fullname">
                                            <HeaderStyle Width="20%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Home Address" ItemStyle-HorizontalAlign="left">
                                            <HeaderStyle Width="20%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                              <asp:BoundField DataField="ContactNo" HeaderText="Contact No." ItemStyle-HorizontalAlign="left">
                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                                <asp:BoundField DataField="CelNo" HeaderText="Mobile No." ItemStyle-HorizontalAlign="left">
                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                             <asp:BoundField DataField="OfficeAdd" HeaderText="Office Address" ItemStyle-HorizontalAlign="left">
                                            <HeaderStyle Width="20%" HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </mcn:DataPagerGridView>
                        </div>
                    </div>--%>
                    <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DoctorNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="Address" Caption="Home Address" /> 
                                <dx:GridViewDataTextColumn FieldName="ContactNo" Caption="Contact No." />                    
                                <dx:GridViewDataTextColumn FieldName="CelNo" Caption="Mobile No." />                                                                
                                <dx:GridViewDataTextColumn FieldName="OfficeAdd" Caption="Office Address" />                                                                                                                                                        
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder"/>                       
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
				                </dx:GridViewCommandColumn> 
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                       
                </div>
                   
            </div>
            </div>
       </div>
 </div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Last Name :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtLastName" runat="server" CssClass="form-control required" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">First Name :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtFirstName" runat="server" CssClass="form-control required" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Middle Name :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtMiddleName" runat="server" CssClass="form-control required" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Home Address :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Contact No. :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtContactNo" runat="server" CssClass="form-control" SkinID="txtdate" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Mobile No. :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtCelNo" runat="server" CssClass="form-control" SkinID="txtdate" ></asp:Textbox>
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-4 control-label has-space">Office Address :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtOfficeAdd" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
             </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                </div>
            </div> 
       
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
