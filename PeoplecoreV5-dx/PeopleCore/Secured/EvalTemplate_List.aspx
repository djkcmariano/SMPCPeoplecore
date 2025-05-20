<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EvalTemplate_List.aspx.vb" Inherits="Secured_EvalTemplate_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap">         
    <div class="row">   
        <%--Main--%>   
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
                        <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
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
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EvalTemplateNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="EvalTemplateCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="EvalTemplateDesc" Caption="Description" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />                                             
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Preview" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkForm" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkForm_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>  
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                                                                                                                                          
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

<div class="page-content-wrap"> 
     <div class="col-md-12 bhoechie-tab-container"> 
     <div class="panel panel-default" style="margin-bottom:0px;">
         <div class="panel-heading">
            <h4 class="panel-title">Transaction No.: <asp:Label ID="lblMain" runat="server"></asp:Label></h4>
         </div>    
         
        <div class="col-md-2 bhoechie-tab-menu">
            <div class="list-group">
                <%--Select Category--%>
                <asp:LinkButton ID="lnkCate" OnClick="lnkCate_Click" runat="server" CssClass="list-group-item active text-left" Text="Category"></asp:LinkButton>
                <%--Select Fix Rating--%>
                <asp:LinkButton ID="lnkRating" OnClick="lnkRating_Click" runat="server" CssClass="list-group-item text-left" Text="Fixed Rating"></asp:LinkButton>
                <%--Select Questionnaire--%>
                <asp:LinkButton ID="lnkItem" OnClick="lnkItem_Click" runat="server" CssClass="list-group-item text-left" Text="Questionnaire"></asp:LinkButton>
            </div>
        </div>
        <div class="col-md-10 bhoechie-tab" style=" border-left:1px solid #e5e5e5;">                  
              <br />
              <div class="page-content-wrap" id="divDim" runat="server">   
                    <%--Category Listing--%>      
                    <div class="row">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="col-md-2">

                                    </div>
                                    <div>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>                    
                                            <ul class="panel-controls">
                                              <li><asp:LinkButton runat="server" ID="lnkAddCate" OnClick="lnkAddCate_Click" Text="Add" CssClass="control-primary" /></li>
                                              <li><asp:LinkButton runat="server" ID="lnkDeleteCate" OnClick="lnkDeleteCate_Click" Text="Delete" CssClass="control-primary" /></li>
                                             <li><asp:LinkButton runat="server" ID="lnkExportCate" OnClick="lnkExportCate_Click" Text="Export" CssClass="control-primary" /></li>
                                            </ul> 
                                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteCate" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                                        </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lnkExportCate" />
                                    </Triggers>
                                </asp:UpdatePanel> 
                            </div>  
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="table-responsive">
                                            <dx:ASPxGridView ID="grdCate" ClientInstanceName="grdCate" runat="server" SkinID="grdDX" KeyFieldName="EvalTemplateCateNo">                                                                                   
                                                <Columns>
                                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                        <DataItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditCate_Click" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                    <dx:GridViewDataTextColumn FieldName="EvalTemplateCateCode" Caption="Code" />                                                                           
                                                    <dx:GridViewDataTextColumn FieldName="EvalTemplateCateDesc" Caption="Description" />                  
                                                    <dx:GridViewDataComboBoxColumn FieldName="OrderBy" Caption="Order" />                                                                                                                                                     
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                                </Columns>                            
                                            </dx:ASPxGridView>   
                                            <dx:ASPxGridViewExporter ID="grdExportCate" runat="server" GridViewID="grdCate" />                             
                                        </div>
                                    </div>                                                           
                                </div>                   
                            </div>
                    </div>
              </div> 

              <div class="page-content-wrap" id="divRating" runat="server" visible="false">  
                  <div class="row">
                      <div class="panel panel-default">
                          <div class="panel-heading">
                                <div class="col-md-2">
                                    <h4>&nbsp;</h4>                                
                                </div>
                                <div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                        <ContentTemplate>                    
                                            <ul class="panel-controls">
                                                <li><asp:LinkButton runat="server" ID="lnkAddRating" OnClick="lnkAddRating_Click" Text="Add" CssClass="control-primary" /></li>
                                                <li><asp:LinkButton runat="server" ID="lnkDeleteRating" OnClick="lnkDeleteRating_Click" Text="Delete" CssClass="control-primary" /></li>
                                                <li><asp:LinkButton runat="server" ID="lnkExportRating" OnClick="lnkExportRating_Click" Text="Export" CssClass="control-primary" /></li>
                                            </ul> 
                                            <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkDeleteRating" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportRating" />
                                        </Triggers>
                                    </asp:UpdatePanel> 
                                </div>                           
                            </div>
                            <div class="panel-body">
                                    <div class="row">
                                        <div class="table-responsive">
                                            <dx:ASPxGridView ID="grdRating" ClientInstanceName="grdRating" runat="server" SkinID="grdDXTotal" KeyFieldName="EvalTemplateRatingNo;OrderByCate"
                                            OnCustomCallback="grdRating_CustomCallback" OnCustomColumnSort="grdRating_CustomColumnSort" OnCustomColumnDisplayText="grdRating_CustomColumnDisplayText" >                                                                                   
                                                <Columns>
                                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkEditRating" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditRating_Click" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>                            
                                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                    <dx:GridViewDataTextColumn FieldName="EvalTemplateCateDesc" Caption="Category" GroupIndex="0" >
                                                           <Settings SortMode="Custom" />
					                                </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="EvalTemplateRatingDesc" Caption="Description" />                                                                         
                                                    <dx:GridViewDataTextColumn FieldName="Proficiency" Caption="Proficiency" Visible="false" CellStyle-HorizontalAlign="Left" /> 
                                                    <dx:GridViewDataTextColumn FieldName="OrderByRating" Caption="Order" CellStyle-HorizontalAlign="Left" />                                                                                                                                                                                                                                                                  
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                                </Columns>                            
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="grdExportRating" runat="server" GridViewID="grdRating" />
                                        </div>
                                    </div>                                                           
                                </div>                   
                            </div>
                    </div>
              </div>

              <div class="page-content-wrap" id="divItem" runat="server" visible="false">       
                    <%--Questionnaire Listing--%>
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="col-md-2">
                            
                                </div>
                                <div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>                    
                                            <ul class="panel-controls">
                                                <li><asp:LinkButton runat="server" ID="lnkAddItem" OnClick="lnkAddItem_Click" Text="Add" CssClass="control-primary" /></li>
                                                <li><asp:LinkButton runat="server" ID="lnkDeleteItem" OnClick="lnkDeleteItem_Click" Text="Delete" CssClass="control-primary" /></li>
                                                <li><asp:LinkButton runat="server" ID="lnkExportItem" OnClick="lnkExportItem_Click" Text="Export" CssClass="control-primary" /></li>
                                            </ul> 
                                            <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkDeleteItem" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                                         
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportItem" />
                                        </Triggers>
                                    </asp:UpdatePanel> 
                                </div>  
                             </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdItem" ClientInstanceName="grdItem" runat="server" SkinID="grdDXTotal" KeyFieldName="EvalTemplateDetlNo;OrderByCate"
                                         OnCustomCallback="grdItem_CustomCallback" OnCustomColumnSort="grdItem_CustomColumnSort" OnCustomColumnDisplayText="grdItem_CustomColumnDisplayText">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkEditItem" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditItem_Click" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="EvalTemplateCateDesc" Caption="Category" GroupIndex="0" >
                                                       <Settings SortMode="Custom" />
					                            </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrderCode" Caption="Code" />                                                                           
                                                <dx:GridViewDataTextColumn FieldName="Question" Caption="Question" /> 
                                                <dx:GridViewDataComboBoxColumn FieldName="ResponseTypeDesc" Caption="Response Type" />   
                                                <dx:GridViewDataTextColumn FieldName="OrderBy" Caption="Order" />        
                                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkDetailItem" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetailItem_Click" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>                                                                                                                                                    
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" SelectAllCheckboxMode="Page"/>
                                                
                                            </Columns>                         
                                        </dx:ASPxGridView>   
                                        <dx:ASPxGridViewExporter ID="grdExportItem" runat="server" GridViewID="grdItem" />                             
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>

                    <%--Detail Listing--%>
                    <div class="row">
                        <div class="panel panel-default"> 
                            <div class="panel-heading">
                                <div class="col-md-6">
                                    <h4 class="panel-title">Transaction No.: <asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                                </div>
                                <div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                    <ContentTemplate>                    
                                        <ul class="panel-controls">
                                            <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                        </ul> 
                                        <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                                    </Triggers>
                                    </asp:UpdatePanel> 
                                </div>                          
                            </div>                           
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdDetail" ClientInstanceName="grdDetail" runat="server" Width="100%" KeyFieldName="EvalTemplateDetlChoiceNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                        <DataItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>                            
                                                <dx:GridViewDataTextColumn FieldName="CodeDetl" Caption="Detail No." />
                                                <dx:GridViewDataTextColumn FieldName="EvalTemplateDetlChoiceDesc" Caption="Description" />                                                                           
                                                <dx:GridViewDataTextColumn FieldName="Proficiency" Caption="Proficiency" Visible="false" CellStyle-HorizontalAlign="Left" /> 
                                                <dx:GridViewDataTextColumn FieldName="OrderBy" Caption="Order" />                                                                                        
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" SelectAllCheckboxMode="Page" />
                                            </Columns>                            
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetail" />
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                   </div>
              </div> 
              <br /><br />
        </div>
        </div>   
    </div>
</div>


<%--Edit Main--%>
<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">       
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEvalTemplateNo" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
                                         
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtEvalTemplateCode" runat="server" CssClass="required form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                        <asp:TextBox ID="txtEvalTemplateDesc" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group" runat="server" id="divOpt" visible="false">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:RadioButton runat="server" ID="chkIsExam" Text="&nbsp;Examination Result" GroupName="Assessment"  /><br />
                    <asp:RadioButton runat="server" ID="chkIsPanelAssess" Text="&nbsp;Panel Assessment" GroupName="Assessment"  /><br />
                    <asp:RadioButton runat="server" ID="chkIsOnlineAssess" Text="&nbsp;Online Assessment" GroupName="Assessment"  /><br />
                    <asp:RadioButton runat="server" ID="chkIsNA" Text="&nbsp;Not applicable" GroupName="Assessment"  />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="Multiline" Rows="4" />
                </div>
            </div> 

            <div class="form-group" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass="number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div>  

	        <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />                            
                </div>
            </div>
                   
                   
        </div>                    
    </fieldset>
</asp:Panel>


<%--Edit Category--%>
<asp:Button ID="btnShowCate" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlCate" runat="server" TargetControlID="btnShowCate" PopupControlID="pnlPopupCate" CancelControlID="lnkCloseCate" BackgroundCssClass="modalBackground" />
<asp:Panel ID="pnlPopupCate" runat="server"  CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsd">
    <!-- Header here -->
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseCate" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSaveCate" CssClass="fa fa-floppy-o submit fsd btnSaveCate" OnClick="lnkSaveCate_Click"  />
        </div>
        <!-- Body here -->
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-required">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtEvalTemplateCateNo" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCodeCate" runat="server" Enabled="false" CssClass="form-control" Placeholder="Autonumber" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-required">Code :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtEvalTemplateCateCode" CssClass="form-control required" />
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtEvalTemplateCateDesc" CssClass="form-control required" />
                    </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Order :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtOrderByCate" CssClass="form-control required number" />                        
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Weight :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtWeightCate" CssClass="form-control number" />                        
                    </div>
                </div>
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>


<%--Edit Rating--%>
<asp:Button ID="btnShowRating" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlRating" runat="server" TargetControlID="btnShowRating" PopupControlID="pnlPopupRating" CancelControlID="lnkCloseRating" BackgroundCssClass="modalBackground" />
<asp:Panel ID="pnlPopupRating" runat="server"  CssClass="entryPopup" style="display:none;">
<fieldset class="form" id="Fieldset2">
<!-- Header here -->
    <div class="cf popupheader">
        <h4>&nbsp;</h4>
        <asp:Linkbutton runat="server" ID="lnkCloseRating" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
        <asp:LinkButton runat="server" ID="lnkSaveRating" CssClass="fa fa-floppy-o submit fsd btnSaveRating" OnClick="lnkSaveRating_Click"  />
    </div>
    <!-- Body here -->
    <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-required">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtEvalTemplateRatingNo" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCodeRating" runat="server" Enabled="false" CssClass="form-control" Placeholder="Autonumber" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Category :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboEvalTemplateCateRatingNo" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtEvalTemplateRatingDesc" CssClass="form-control required" TextMode="MultiLine" Rows="3" />
                </div>
            </div>      
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Proficiency :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtProficiencyRating" CssClass="form-control number" />                        
                </div>
            </div>          
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Order :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtOrderByRating" CssClass="form-control required number" />                        
                </div>
            </div>
                
            <br />
        </div>                    
    </fieldset>
</asp:Panel>

<%--Edit Item--%>
<asp:Button ID="btnShowItem" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlItem" runat="server" TargetControlID="btnShowItem" PopupControlID="pnlPopupItem" CancelControlID="lnkCloseItem" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnlPopupItem" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="Fieldset1">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseItem" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveItem" OnClick="lnkSaveItem_Click" CssClass="fa fa-floppy-o submit lnkSaveItem" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEvalTemplateDetlNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>     
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeItem" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>                     
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Category :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboEvalTemplateCateNo" CssClass="form-control" />
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtOrderCode" CssClass="form-control"  Placeholder="e.g. 1,2,3... or A,B,C..." />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Question :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtQuestion" CssClass="form-control required" TextMode="MultiLine" Rows="4" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Response Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboResponseTypeNo" CssClass="form-control required" DataMember="EResponseType" />
                </div>
            </div>   
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtHasComment" Text="&nbsp;Tick to add comment box to template" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsRequired" Text="&nbsp;Tick to make data a required field" />
                </div>
            </div>                            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Order No.  :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtOrderBy" CssClass="form-control required number" />                        
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Weight :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtWeightageItem" CssClass="form-control number" />                        
                </div>
            </div>     
            <br />
        </div>                    
    </fieldset>
</asp:Panel>


<%--Edit Details--%>
<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsDetail">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetail" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDetl" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"/>
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtEvalTemplateDetlChoiceDesc" CssClass="form-control required" TextMode="MultiLine" Rows="3" />
                </div>
            </div>  
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Proficiency :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtProficiency" CssClass="form-control number" />                        
                </div>
            </div>                                   
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Order :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtOrderByDetl" CssClass="form-control required number" />                        
                </div>
            </div>
            <br />
        </div>                    
    </fieldset>
</asp:Panel>



</asp:Content>

