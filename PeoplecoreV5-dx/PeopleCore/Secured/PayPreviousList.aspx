<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayPreviousList.aspx.vb" Inherits="Secured_PayPreviousList" %>
<%@ Register Src="../Include/wucFilterGeneric.ascx" TagName="wucFilter"   TagPrefix="uc1" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    
                </div>  
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">   
                        <li>
                            <asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" />
                        </li>                     
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li> 
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                </asp:UpdatePanel>

            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="PayPreviousNo" SkinID="grdDX">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="code" Caption="Trans. No." />      
                            <dx:GridViewDataTextColumn FieldName="FacilityDesc" Caption="Business Unit" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />                      
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" CellStyle-HorizontalAlign="Left"/>
                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                            <dx:GridViewDataCheckColumn FieldName="IsMWE" Caption="MWE"  HeaderStyle-HorizontalAlign="Center" Width="5%" />
                            <dx:GridViewDataCheckColumn FieldName="IsPreviousEmployer" Caption="Prev. Employer"  HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="TotalTaxableIncome" Caption="Taxable Income" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="TaxExemption" Caption="Tax Exemption" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="Bonus" Caption="Bonus" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="TaxWithheld" Caption="Tax Withhheld" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />  
                            <dx:GridViewDataTextColumn FieldName="DateFrom" Caption="Date From" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="DateTo" Caption="Date To" Visible="false" />  
                            <dx:GridViewDataTextColumn FieldName="TotalBasicIncome" Caption="Taxable Basic" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />         
                            <dx:GridViewDataTextColumn FieldName="RepAllow" Caption="Representation" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="TranspoAllow" Caption="Transportation" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="COLA" Caption="COLA" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="HousingAllow" Caption="Fix Housing Allowance" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="TotalOneTimeTaxableIncomeOther" Caption="Other Taxable Non Basic" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="Commission" Caption="Commission" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="Profit" Caption="Profit Sharing" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="HazardPay" Caption="Hazard Pay" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="OTPay" Caption="OT Pay" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="HolidayPay" Caption="Holiday Pay" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="NP" Caption="NP Pay" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="TotalNontaxableIncomeOther" Caption="Other Non Taxable Income" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="Deminimis" Caption="Date To" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="TotalAccumIncome" Caption="Accum/Bonus" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" Visible="false" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="MonthDesc" Caption="Applicable Month" Visible="false"/>                                                                                                                                         
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                       
                </div>                            
            </div>
        </div>
    </div>
</div>
<%--<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                
                <div class="panel-heading">
                    <div class="form-group">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-4 col-md-offset-2">
                            
                            <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                            <Content>
                                
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Filter By :</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control"  AutoPostBack="true"  OnSelectedIndexChanged="cbofilterby_SelectedIndexChanged">
                                                </asp:DropDownList>
                                             </div>
                                         </div>
                                       
		                                 <div class="form-group">
                                                <label class="col-md-4 control-label">Filter Value :</label> 
                                                <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control">
                                                        </asp:DropDownList>
                                                </div>
                                           </div>
		                                 
	                                </Content>
                                </uc:Filter>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="PayPreviousNo"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
               
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server"   
                                        Text='<%# Bind("PayPreviousNo") %>'></asp:Label>
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
                            <asp:BoundField DataField="code" HeaderText="Transaction No."  
                                HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left" SortExpression="Code" />

                                                                            
                            <asp:BoundField DataField="fullname" SortExpression="fullname" HeaderText="Employee Name"     >
                                <HeaderStyle Width="20%" HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                                                            
                            <asp:BoundField DataField="ApplicableYear" SortExpression="Applicableyear"  HeaderText="Year" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" SortExpression="Description"  HeaderText="Description" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalTaxableIncome" SortExpression="TotalTaxableIncome"  HeaderText="Taxable Income" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TaxExemption" SortExpression="TaxExemption"  HeaderText="Tax Exemption" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TaxwithHeld" SortExpression="TaxWithheld"  HeaderText="Tax WithHeld" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                                <asp:BoundField DataField="Bonus" SortExpression="Bonus"  HeaderText="Bonus" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
            
                            <asp:TemplateField HeaderText="Select"  >
                                <HeaderTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Select" style="color:white; font-size:10px;" />
                                    <asp:CheckBox ID="txtIsSelectAll" onclick ="SelectAllCheckboxes(this);"  runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="center" Width="5%" VerticalAlign="Top" />
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
                                <!-- Button here -->
                                <asp:button runat="server" cssClass="btn btn-default"  ID="lnkAdd" OnClick="lnkAdd_Click" CausesValidation="false" Text="Add" UseSubmitBehavior="false">
                                </asp:button>

                                <asp:Button ID="lnkDelete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                    cssClass="btn btn-default" OnClick="lnkDelete_Click" Text="Delete">
                                </asp:Button>               
                            </div>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div>--%>

 <asp:Button ID="Button3" runat="server" style="display:none" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose2" PopupControlID="Panel6" TargetControlID="Button3" />
    <asp:Panel id="Panel6" runat="server" CssClass="entryPopup2" style="display:none">
        <fieldset class="form" id="Fieldset2">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                    <ContentTemplate>
                        <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="cancel fa fa-times" ToolTip="Close" />
                        &nbsp;
                        <asp:LinkButton runat="server" ID="lnkSave2" CssClass="fa fa-floppy-o submit Fieldset2 lnkSave2" OnClick="lnkSave2_Click"  />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkSave2" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div  class="entryPopupDetl2 form-horizontal">
                <div class="form-group">
                    <label class="col-md-9 control-label has-space">
                    <code>File must be .csv </code></label>
                    <br />
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Filename :</label>
                    <div class="col-md-7">
                        <asp:FileUpload runat="server" ID="fuFilename" Width="100%" CssClass="required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Batch Number :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtBatchNumber" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtDescription2" runat="server" Rows="4" textmode="MultiLine" CssClass="form-control" />
                    </div>
                </div>
                <br />
            </div>
            <div class="cf popupfooter">
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>
