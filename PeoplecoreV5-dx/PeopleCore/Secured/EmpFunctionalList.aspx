<%@ Page Title="" Language="VB" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master"
    AutoEventWireup="false" CodeFile="EmpFunctionalList.aspx.vb" Inherits="Secured_EmpFunctionalList" %>


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

    $(window).resize(function () {
        adjustPanelEntry();

    });

    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (args, sender) {
            adjustPanelEntry();
        });

    });   
</script>

 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--<div class="panel-body">
                    <div class="table-responsive">
                        <!-- Gridview here -->
                        <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="TaskNo">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="False">
                            
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("TaskNo") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField  ShowHeader="false" >
                                    <ItemTemplate >
                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="grdEditbtn" OnClick="lnkEdit_Click" CausesValidation="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="Code" HeaderText="Reference No."  
                                    HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left" SortExpression="Code" />

                                <asp:BoundField DataField="TaskCode" HeaderText="Code" ItemStyle-HorizontalAlign="left">
                                    <HeaderStyle Width="30%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TaskDesc" HeaderText="Description" ItemStyle-HorizontalAlign="left">
                                    <HeaderStyle Width="45%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MinSalaryGradeNo" HeaderText="Minimum Salary Level"
                                    ItemStyle-HorizontalAlign="left">
                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MaxSalaryGradeNo" HeaderText="Maximum Salary Level"
                                    ItemStyle-HorizontalAlign="left">
                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Service Record Accredited">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsServiceRecord" Checked='<%# bind("IsServiceRecord") %>' runat="server" Enabled ="false"/>
                               
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Select">
                                    <HeaderStyle HorizontalAlign="right" Width="2%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="txtIsSelectAll" onclick="SelectAllCheckboxes(this);" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txtIsSelect" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NextPreviousFirstLast" />
                        </mcn:DataPagerGridView>
                    </div> 
                    <div class="row">
                        <div class="col-md-4">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="grdMain" PageSize="10">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/arrow_first.png" PreviousPageImageUrl="~/images/arrow_previous.png" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>Page
                                                <asp:Label ID="CurrentPageLabel" runat="server" Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" /> of
                                                <asp:Label ID="TotalPagesLabel" runat="server" Text="<%# Math.Ceiling (System.Convert.ToDouble(Container.TotalRowCount) / Container.PageSize) %>" /> (
                                                <asp:Label ID="TotalItemsLabel" runat="server" Text="<%# Container.TotalRowCount%>" /> records )
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    <asp:NextPreviousPagerField ButtonType="Image" LastPageImageUrl="~/images/arrow_last.png" NextPageImageUrl="~/images/arrow_next.png" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />                              
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <div class="col-md-6 col-md-offset-2">
                            <div class="btn-group pull-right">
                
                                <asp:button runat="server" cssClass="btn btn-default"  ID="lnkAdd" OnClick="lnkAdd_Click"  UseSubmitBehavior="false" CausesValidation="false" Text="Add">
                                </asp:button>
                                <asp:Button ID="lnkDelete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                        cssClass="btn btn-default"  OnClick="lnkDelete_Click" Text="Delete">
                                </asp:Button>
                            </div>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </div>
                    </div>       
                </div>--%>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TaskNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("TaskNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="TaskCode" Caption="Code"/>
                                    <dx:GridViewDataTextColumn FieldName="TaskDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="MinSalaryGradeNo" Caption="Minimum Salary Level" /> 
                                    <dx:GridViewDataTextColumn FieldName="MaxSalaryGradeNo" Caption="Maximum Salary Level" /> 
                                    <dx:GridViewDataCheckColumn FieldName="IsServiceRecord" Caption="Service Record Accredited" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder" />                                      
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
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTaskNo" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtTaskCode" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtTaskDesc" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Minimum salary level :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboMinSalaryGradeNo" runat="server" DataMember="ESalaryGrade" CssClass="form-control"  />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Maximum salary level :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboMaxSalaryGradeNo" runat="server" DataMember="ESalaryGrade" CssClass="form-control"  />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                    <asp:CheckBox  ID="chkIsServiceRecord" runat="server" /><span> if accredited in service record.</span> 
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div> 
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
</asp:Content>