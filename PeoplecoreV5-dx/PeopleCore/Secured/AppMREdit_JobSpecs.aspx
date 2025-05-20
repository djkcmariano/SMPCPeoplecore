<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="AppMREdit_JobSpecs.aspx.vb" Inherits="Secured_AppMREdit_JobSpecs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc:Tab runat="server" ID="Tab">
        <Header>                   
            <asp:Label runat="server" ID="lbl" />                                    
        </Header>
        <Content>
            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-6">
                                <h4 class="panel-title">Education</h4>
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAddEduc" OnClick="lnkAddEduc_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteEduc" OnClick="lnkDeleteEduc_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteEduc" TargetControlID="lnkDeleteEduc" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdEduc" ClientInstanceName="grdEduc" runat="server" SkinID="grdDX" KeyFieldName="MREducNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("MREducNo") %>' OnClick="lnkEditEduc_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="EducTypeDesc" Caption="Educational Type" />
                                            <dx:GridViewDataCheckColumn FieldName="IsQS" Caption="CSC QS" />                                        
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>                            
                                    </dx:ASPxGridView>                                
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div>
            
            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-6">
                                <h4 class="panel-title">Work Experience</h4>
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAddExpe" OnClick="lnkAddExpe_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteExpe" OnClick="lnkDeleteExpe_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteExpe" TargetControlID="lnkDeleteExpe" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdExpe" ClientInstanceName="grdExpe" runat="server" SkinID="grdDX" KeyFieldName="MRExpeNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("MRExpeNo") %>' OnClick="lnkEditExpe_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="ExpeTypeDesc" Caption="Experience Type" />
                                            <dx:GridViewDataCheckColumn FieldName="IsQS" Caption="CSC QS" />                                        
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>                            
                                    </dx:ASPxGridView>                                
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div>

            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-6">
                                <h4 class="panel-title">Training</h4>
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAddTrn" OnClick="lnkAddTrn_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteTrn" OnClick="lnkDeleteTrn_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteTrn" TargetControlID="lnkDeleteTrn" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdTrn" ClientInstanceName="grdTrn" runat="server" SkinID="grdDX" KeyFieldName="MRTrnNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("MRTrnNo") %>' OnClick="lnkEditTrn_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />
                                            <dx:GridViewDataCheckColumn FieldName="IsQS" Caption="CSC QS" />                                        
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>                            
                                    </dx:ASPxGridView>                                
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div>
                                    
            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-6">
                                <h4 class="panel-title">Eligibility</h4>
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAddElig" OnClick="lnkAddElig_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteElig" OnClick="lnkDeleteElig_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteElig" TargetControlID="lnkDeleteElig" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdElig" ClientInstanceName="grdElig" runat="server" SkinID="grdDX" KeyFieldName="MREligNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("MREligNo") %>' OnClick="lnkEditElig_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="ExamTypeDesc" Caption="Eligibility Type" />
                                            <dx:GridViewDataCheckColumn FieldName="IsQS" Caption="QS?" />                                        
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>                            
                                    </dx:ASPxGridView>                                
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div>                        
            
            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-6">
                                <h4 class="panel-title">Skills and Competency</h4>
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                    
                                    <li><asp:LinkButton runat="server" ID="lnkAddComp" OnClick="lnkAddComp_Click" Text="Add" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteComp" OnClick="lnkDeleteComp_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteComp" TargetControlID="lnkDeleteComp" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdComp" ClientInstanceName="grdComp" runat="server" SkinID="grdDX" KeyFieldName="MRCompNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("MRCompNo") %>' OnClick="lnkEditComp_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="CompDesc" Caption="Competency" />
                                            <dx:GridViewDataCheckColumn FieldName="IsQS" Caption="QS?" />                                        
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>                            
                                    </dx:ASPxGridView>                                
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div>  

            <asp:Button ID="btnShowEduc" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlEduc" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlpopupEduc" TargetControlID="btnShowEduc"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupEduc" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsMain">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveEduc" CssClass="fa fa-floppy-o submit fsMain btnSaveEduc" OnClick="btnSaveEduc_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMREducNo" runat="server" ReadOnly="true" Enabled="false" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMREducCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Description :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtDescriptionEduc" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Educational Level :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboEducLevelNo" DataMember="EEducLevel" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Educational Type :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboEducTypeNo" DataMember="EEducType" runat="server" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Course :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboCourseNo" DataMember="ECourse" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Area of Specialization :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboFieldOfStudyNo" DataMember="EFieldOfStudy" runat="server" CssClass="form-control" />
                            </div>
                        </div>                                                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsGraduated" runat="server" Text="&nbsp;Tick if graduated is required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSEduc" runat="server" Text="&nbsp;Tick if for Qualification Standard" />
                            </div>
                        </div>                                            
                    </div>
                    <br />
                </fieldset>
            </asp:Panel>

            <asp:Button ID="btnShowExpe" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlExpe" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClosed" PopupControlID="pnlpopupExpe" TargetControlID="btnShowExpe"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupExpe" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsExpe">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveExpe" CssClass="fa fa-floppy-o submit fsExpe btnSaveExpe" OnClick="btnSaveExpe_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRExpeNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRExpeCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Description :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtDescriptionExpe" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Experience Type :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboExpeTypeNo" DataMember="EExpeType" runat="server" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">No. of year/s :</label>
                            <div class="col-md-3">                        
                                <asp:TextBox ID="txtNoOfYear" runat="server" CssClass="form-control" MaxLength="5"  />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtNoOfYear" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSExpe" runat="server" Text="&nbsp;Tick if for Qualification Standard"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                     </fieldset>
            </asp:Panel>

            <asp:Button ID="btnShowComp" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlComp" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClosedComp" PopupControlID="pnlpopupComp" TargetControlID="btnShowComp"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupComp" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsComp">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClosedComp" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveComp" CssClass="fa fa-floppy-o submit fsComp btnSaveComp" OnClick="btnSaveComp_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRCompNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRCompCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Competency Type :</label>
                            <div class="col-md-7">                        
                                <asp:DropdownList ID="cboCompTypeNo" DataMember="ECompType" runat="server" OnSelectedIndexChanged="cboCompTypeNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Competency :</label>
                            <div class="col-md-7">                                       
                                <asp:DropdownList ID="cboCompNo" runat="server" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Proficiency Level :</label>
                            <div class="col-md-7">                                       
                                <asp:DropdownList ID="cboCompScaleNo" runat="server" CssClass="form-control required" DataMember="ECompScale" OnSelectedIndexChanged="cboCompScaleNo_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Indicator :</label>
                            <div class="col-md-7">                                       
                                <asp:Textbox ID="txtAnchor" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div>
                        <div class="form-group" runat="server" visible="false">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSComp" runat="server" Text="&nbsp;Tick if Skills and Competency Requirement is for QS"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                     </fieldset>
            </asp:Panel>

            <asp:Button ID="btnShowElig" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlElig" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClosedElig" PopupControlID="pnlpopupElig" TargetControlID="btnShowElig"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupElig" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsElig">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClosedElig" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveElig" CssClass="fa fa-floppy-o submit fsElig btnSaveElig" OnClick="btnSaveElig_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMREligNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMREligCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Description :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtDescriptionElig" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Eligibility Type :</label>
                            <div class="col-md-7">
                                <asp:DropdownList ID="cboExamTypeNo" DataMember="EExamType" runat="server" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Rating :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtAverageRate" runat="server" SkinID="txtdate" CssClass="form-control" MaxLength="7" />  
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtAverageRate" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSElig" runat="server" Text="&nbsp;Tick if for Qualification Standard" ></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                </fieldset>
            </asp:Panel>

            <asp:Button ID="btnShowTrn" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlTrn" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClosedd" PopupControlID="pnlpopupTrn" TargetControlID="btnShowTrn"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupTrn" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsTrn">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClosedd" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveTrn" CssClass="fa fa-floppy-o submit fsTrn btnSaveTrn" OnClick="btnSaveTrn_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRTrnNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRTrnCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Description :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtDescriptionTrn" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Training Type :</label>
                            <div class="col-md-7"> 
                                <asp:Dropdownlist ID="cboTrnTitleNo" DataMember="ETrnTitle" runat="server" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">No. of Hours :</label>
                            <div class="col-md-3">                        
                                <asp:TextBox ID="txtNoOfHours" runat="server" CssClass="form-control" MaxLength="7" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtNoOfHours" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSTrn" runat="server" Text="&nbsp;Tick if for Qualification Standard" ></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                     </fieldset>
            </asp:Panel>
                
        </Content>
    </uc:Tab>    
</asp:Content>

