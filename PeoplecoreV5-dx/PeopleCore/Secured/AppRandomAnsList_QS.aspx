<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="AppRandomAnsList_QS.aspx.vb" Inherits="Secured_AppRandomAnsList_QS" %>
<%@ Register Src="~/Include/FileUpload.ascx" TagName="FileUpload" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    form-horizontal .control-label2{    
    text-align:left;    
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

<div class="panel panel-default">      
    <div class="panel-heading">
        <h4 class="panel-title">
            <asp:Label runat="server" ID="lbl" />
        </h4>
        <ul class="panel-controls">                                        
            <li><asp:LinkButton runat="server" ID="lnkAttachment" OnClick="lnkAttachment_Click" Text="File Cabinet" CssClass="control-primary" /></li>                
        </ul>
    </div>
    <div class="panel-body">             
       <div class="panel panel-default">
          <%--QS--%>
              <div>
              <div class="panel-heading">
                 <h4 class="panel-title"><a data-toggle="collapse" href="#divQS">Qualification Standards of Position to be Filled</a></h4>
              </div>      
              <div id="divQS" class="panel-body collapse in">
              <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-md-2 control-label2">Position Title :</label>
                    <div class="col-md-10">
                        <asp:Label runat="server" ID="lblPositionDesc" Text='<%# Bind("Description") %>' />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label2">Education :</label>
                    <div class="col-md-10">
                        <asp:Repeater runat="server" ID="rEduc">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Label1" Text='<%# Bind("Description") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label2">Experience :</label>
                    <div class="col-md-10">
                        <asp:Repeater runat="server" ID="rExpe">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Label1" Text='<%# Bind("ExpeTypeDesc") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label2">Training :</label>
                    <div class="col-md-10">
                        <asp:Repeater runat="server" ID="rTrn">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Label1" Text='<%# Bind("Description") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label2">Eligibility :</label>
                    <div class="col-md-10">
                        <asp:Repeater runat="server" ID="rExam">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Label1" Text='<%# Bind("ExamTypeDesc") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div> 
                </div>
          </div>        
          </div>
          <%--QS--%>

          <%--Education--%>          
          <div>
          <div class="panel-heading">
             <h4 class="panel-title"><a data-toggle="collapse" href="#divEduc">Education</a></h4>                                
             <ul class="panel-controls">                                        
                <li><asp:LinkButton runat="server" ID="lnkEducList" OnClick="lnkEducList_Click" Text="Candidate Entry" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkEducAdd" OnClick="lnkEducAdd_Click" Text="Add" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkEducDelete" OnClick="lnkEducDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkEducDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
             </ul>                             
          </div>      
          <div id="divEduc" class="panel-body collapse in">             
            <div class="table-responsive">
                <dx:ASPxGridView ID="grdAssessEduc" ClientInstanceName="grdMain" runat="server" KeyFieldName="AssessEducNo" Width="100%">
                    <Columns>
                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                            <DataItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkEudcEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("AssessEducNo") %>' OnClick="lnkEducEdit_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>                            
                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />
                        <dx:GridViewDataTextColumn FieldName="EducLevelDesc" Caption="Level" />
                        <dx:GridViewDataTextColumn FieldName="SchoolDesc" Caption="School" />
                        <dx:GridViewDataTextColumn FieldName="CourseDesc" Caption="Course" />                            
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
                    </Columns>                            
                </dx:ASPxGridView>                                   
            </div>
          </div>

          <asp:Button ID="Button1" runat="server" style="display:none" />
          <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
          <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
                    </div>
                    <div class="container-fluid entryPopupDetl">        
                        <div class="row">
                            <div class="page-content-wrap">         
                                <div class="row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">                                                                                                                                                                   
                                            <div>
                                                <ul class="panel-controls">                                        
                                                    <li><asp:LinkButton runat="server" ID="lnkEducCredit" OnClick="lnkEducValidate_Click" Text="Credit" CssClass="control-primary" /></li>                
                                                </ul>                                                
                                            </div>                                                                      
                                        </div>                            
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <dx:ASPxGridView ID="grdEduc" ClientInstanceName="grdEduc" runat="server" KeyFieldName="Code" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                            <dx:GridViewDataTextColumn FieldName="EducLevelDesc" Caption="Level" />
                                                            <dx:GridViewDataTextColumn FieldName="SchoolDesc" Caption="School" />
                                                            <dx:GridViewDataTextColumn FieldName="CourseDesc" Caption="Course" />
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />                                                                        
                                                    </dx:ASPxGridView>                                
                                                </div>
                                            </div>                                                           
                                        </div>                   
                                    </div>
                                </div>
                            </div>
                            </div>        
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button7" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender7" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="Panel7" TargetControlID="Button7" />
            <asp:Panel ID="Panel7" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="Fieldset5">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="lnkEducSave" CssClass="fa fa-floppy-o submit Fieldset6" OnClick="lnkEducSave_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtAssessEducCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber"></asp:TextBox>
                            </div>
                        </div>                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Level :</label>
                            <div class="col-md-7">
                                <asp:DropDownList runat="server" ID="cboEducLevelNo" DataMember="EEducLevel" CssClass="form-control required" OnSelectedIndexChanged="cboEducLevelNo_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required" runat="server" id="lblSchool">Name of School :</label>
                            <div class="col-md-7">
                                <asp:DropDownList runat="server" ID="cboSchoolNo" CssClass="form-control" DataMember="ESchool" AutoPostBack="true" OnSelectedIndexChanged="cboSchoolNo_SelectedIndexChanged" />
                                <asp:TextBox runat="server" ID="txtOtherSchool" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="divSchoolCampus" visible="false">
                            <label class="col-md-4 control-label has-required" runat="server" id="Label1">Campus / Branch :</label>
                            <div class="col-md-7">
                                <asp:DropDownList runat="server" ID="cboSchoolDetiNo" CssClass="form-control" />                    
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-required" runat="server" id="lblEduc">Basic Education / Degree / Course :</label>
                            <div class="col-md-7">
                                <asp:DropDownList runat="server" ID="cboCourseNo" CssClass="form-control required" />
                            </div>
                        </div>                                                                    
                    </div>
                    <br />
                </fieldset>
            </asp:Panel>

          </div>
          <%--Education--%>

          <%--Experience--%>
          <div>
          <div class="panel-heading">
             <h4 class="panel-title"><a data-toggle="collapse" href="#divExpe">Relevant Experience</a></h4>                                
             <ul class="panel-controls">                                        
                <li><asp:LinkButton runat="server" ID="lnkExpeList" OnClick="lnkExpeList_Click" Text="Candidate Entry" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkExpeAdd" OnClick="lnkExpeAdd_Click" Text="Add" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkExpeDelete" OnClick="lnkExpeDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                <uc:ConfirmBox runat="server" ID="ConfirmBox4" TargetControlID="lnkExpeDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
             </ul>                             
          </div>      
          <div id="divExpe" class="panel-body collapse in">             
            <div class="table-responsive">
                <dx:ASPxGridView ID="grdAssessExpe" ClientInstanceName="grdAssessExpe" runat="server" KeyFieldName="AssessExpeNo" Width="100%">
                    <Columns>
                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                            <DataItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkExpeEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("AssessExpeNo") %>' OnClick="lnkExpeEdit_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />                        
                        <dx:GridViewDataTextColumn FieldName="JDKRACritDesc" Caption="Duties and Responsibilities" GroupIndex="0" />
                        <dx:GridViewDataTextColumn FieldName="Exp_Credited" Caption="Relevant Experience Credited" />
                        <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Office/Department" />
                        <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                        <dx:GridViewDataTextColumn FieldName="DocPresented" Caption="Document Presented" />                        
                        <dx:GridViewBandColumn Caption="Period Covered" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="FromDate" Caption="From" />
                                <dx:GridViewDataTextColumn FieldName="ToDate" Caption="To" />
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataTextColumn FieldName="NoOfDays" Caption="Total<br />Days" />
                        <dx:GridViewDataTextColumn FieldName="EquivalentExperience" Caption="Equivalent<br />Experience"  PropertiesTextEdit-DisplayFormatString="{0:N4}" />
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />                        
                    </Columns>                                        
                    <GroupSummary>
                        <dx:ASPxSummaryItem SummaryType="None" Visible="false" />                        
                    </GroupSummary>                                                                                     
                </dx:ASPxGridView>                                   
            </div>
          </div>

          <asp:Button ID="Button4" runat="server" style="display:none" />
          <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Button4" PopupControlID="Panel4" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel4" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="Fieldset3">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="Linkbutton5" CssClass="fa fa-times" ToolTip="Close" />
                    </div>
                    <div class="container-fluid entryPopupDetl">        
                        <div class="row">
                            <div class="page-content-wrap">         
                                <div class="row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">                                                                                                                                                                   
                                            <div class="col-md-6">
                                                <label class="has-required">Duties and Responsibilities of the Position to be Filled</label><br />
                                                <asp:DropDownList runat="server" ID="cboJDKRACritSumNo" CssClass="form-control required"  />
                                            </div>
                                            <div class="col-md-6">
                                                <ul class="panel-controls">                                                                                        
                                                    <li><asp:LinkButton runat="server" ID="lnkExpeValidate" OnClick="lnkExpeValidate_Click" Text="Credit" CssClass="control-primary submit Fieldset3" /></li>
                                                </ul>                                                
                                            </div>                                                                      
                                        </div>                            
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <dx:ASPxGridView ID="grdExpe" ClientInstanceName="grdExpe" runat="server" KeyFieldName="Code" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                            <dx:GridViewDataTextColumn FieldName="ExpeComp" Caption="Office/Department" />
                                                            <dx:GridViewDataTextColumn FieldName="Position" Caption="Position" />
                                                            <dx:GridViewDataTextColumn FieldName="xFromDate" Caption="From" />
                                                            <dx:GridViewDataTextColumn FieldName="xToDate" Caption="To" />
                                                            <dx:GridViewDataTextColumn FieldName="Duties" Caption="Duties" />                                                            
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />                                                                        
                                                    </dx:ASPxGridView>                                
                                                </div>
                                            </div>                                                           
                                        </div>                   
                                    </div>
                                </div>
                            </div>
                            </div>        
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button10" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender10" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="Panel10" TargetControlID="Button10" />
            <asp:Panel ID="Panel10" runat="server" CssClass="entryPopup">
                <fieldset class="form" id="Fieldset10">
                <!-- Header here -->
                <div class="cf popupheader">
                    <asp:Linkbutton runat="server" ID="Linkbutton8" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="lnkExpeSave" CssClass="fa fa-floppy-o submit Fieldset10" OnClick="lnkExpeSave_Click"  />      
                </div>
                <!-- Body here -->
                <div class="entryPopupDetl form-horizontal">                        
                    <div class="form-group">
                        <label class="col-md-4 control-label">Transaction No. :</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtAssessExpeCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Duties and Responsibilities of the Position to be Filled :</label>
                        <div class="col-md-7">
                            <asp:DropDownList runat="server" ID="cboJDKRACritNo" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Relevant Experience Credited :</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtExp_Credited" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Office / Department :</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtDepartmentDesc" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Position :</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtPositionDesc" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Document Presented :</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtDocPresented" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">From Date :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtExpeFromDate" CssClass="form-control " />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender5" TargetControlID="txtExpeFromDate" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender5" TargetControlID="txtExpeFromDate" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator5" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtExpeFromDate" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">To Date :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtExpeToDate" CssClass="form-control" />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender6" TargetControlID="txtExpeToDate" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender6" TargetControlID="txtExpeToDate" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator6" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtExpeToDate" Display="Dynamic" />
                        </div>
                    </div>
                </div>
                <br />
                </fieldset>
            </asp:Panel>

          </div>
          <%--Experience--%>
          
          <%--Training--%>
          <div>
          <div class="panel-heading">
             <h4 class="panel-title"><a data-toggle="collapse" href="#divTrn">Training</a></h4>                                
             <ul class="panel-controls">                                        
                <li><asp:LinkButton runat="server" ID="lnkTrnList" OnClick="lnkTrnList_Click" Text="Candidate Entry" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkTrnAdd" OnClick="lnkTrnAdd_Click" Text="Add" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkTrnDelete" OnClick="lnkTrnDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkTrnDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
             </ul>                             
          </div>      
          <div id="divTrn" class="panel-body collapse in">
              <div class="table-responsive">
                <dx:ASPxGridView ID="grdAssessTrn" ClientInstanceName="grdAssessTrn" runat="server" KeyFieldName="AssessTrainNo" Width="100%">
                    <Columns>
                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                            <DataItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkTrnEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("AssessTrainNo") %>' OnClick="lnkTrnEdit_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />
                        <dx:GridViewDataTextColumn FieldName="TrainingTitleDesc" Caption="Training Title" />
                        <dx:GridViewDataTextColumn FieldName="NoOfHrs" Caption="No Of Hours" Width="10%" />
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
                    </Columns>                            
                </dx:ASPxGridView>                                   
              </div>                       
          </div>

          <asp:Button ID="Button3" runat="server" style="display:none" />
          <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button3" PopupControlID="Panel3" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
          <asp:Panel id="Panel3" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="Fieldset2">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="Linkbutton4" CssClass="fa fa-times" ToolTip="Close" />
                    </div>
                    <div class="container-fluid entryPopupDetl">        
                        <div class="row">
                            <div class="page-content-wrap">         
                                <div class="row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">                                                                                                                                                                   
                                            <div>
                                                <ul class="panel-controls">                                        
                                                    <li><asp:LinkButton runat="server" ID="lnkTrnValidate" OnClick="lnkTrnValidate_Click" Text="Credit" CssClass="control-primary" /></li>                
                                                </ul>                                                
                                            </div>                                                                      
                                        </div>                            
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <dx:ASPxGridView ID="grdTrn" ClientInstanceName="grdTrn" runat="server" KeyFieldName="Code" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                            <dx:GridViewDataTextColumn FieldName="TrainingTitleDesc" Caption="Training Title" />
                                                            <dx:GridViewDataTextColumn FieldName="NoOfHrs" Caption="No Of Hours" />
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />                                                                        
                                                    </dx:ASPxGridView>                                
                                                </div>
                                            </div>                                                           
                                        </div>                   
                                    </div>
                                </div>
                            </div>
                            </div>        
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button8" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender8" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="Panel8" TargetControlID="Button8" />
            <asp:Panel ID="Panel8" runat="server" CssClass="entryPopup">
                    <fieldset class="form" id="Fieldset8">
                    <!-- Header here -->
                        <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="Linkbutton2" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="lnkTrnSave" CssClass="fa fa-floppy-o submit Fieldset6" OnClick="lnkTrnSave_Click"  />      
                        </div>
                        <!-- Body here -->
                        <div  class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtAccessTrainCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Training Title :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtTrainingTitleDesc" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">From Date :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control " />
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtDateFrom" Format="MM/dd/yyyy" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="txtDateFrom" Mask="99/99/9999" MaskType="Date" />
                                <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateFrom" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">To Date :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control" />
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender4" TargetControlID="txtDateTo" Format="MM/dd/yyyy" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender4" TargetControlID="txtDateTo" Mask="99/99/9999" MaskType="Date" />
                                <asp:CompareValidator runat="server" ID="CompareValidator4" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateTo" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">No Of Hours :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtNoOfHrs" runat="server" CssClass="form-control number" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label">Training Type :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtSTTypeNo" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <br />
                </fieldset>
            </asp:Panel>

          </div>
          <%--Experience--%>

          <%--Eligibility--%>
          <div>
          <div class="panel-heading">
             <h4 class="panel-title"><a data-toggle="collapse" href="#divExam">Eligibility</a></h4>                                
             <ul class="panel-controls">                                        
                <li><asp:LinkButton runat="server" ID="lnkExamList" OnClick="lnkExamList_Click" Text="Candidate Entry" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkExamAdd" OnClick="lnkExamAdd_Click" Text="Add" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkExamDelete" OnClick="lnkExamDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkExamDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
             </ul>                             
          </div>      
          <div id="divExam" class="panel-body collapse in">
             <div class="table-responsive">
                <dx:ASPxGridView ID="grdAssessExam" ClientInstanceName="grdAssessExam" runat="server" KeyFieldName="AssessExamNo" Width="100%">
                    <Columns>
                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                            <DataItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkExamEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("AssessExamNo") %>' OnClick="lnkExamEdit_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />
                        <dx:GridViewDataTextColumn FieldName="ExamTypeDesc" Caption="Exam Type" />
                        <dx:GridViewDataTextColumn FieldName="DateTaken" Caption="Date Taken" />
                        <dx:GridViewDataTextColumn FieldName="ScoreRating" Caption="Rating" />                            
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
                    </Columns>                            
                </dx:ASPxGridView>                                   
            </div>
          </div>

          <asp:Button ID="Button2" runat="server" style="display:none" />
          <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="Fieldset1">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="Linkbutton7" CssClass="fa fa-times" ToolTip="Close" />
                    </div>
                    <div class="container-fluid entryPopupDetl">        
                        <div class="row">
                            <div class="page-content-wrap">         
                                <div class="row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">                                                                                                                                                                   
                                            <div>
                                                <ul class="panel-controls">                                        
                                                    <li><asp:LinkButton runat="server" ID="lnkExamCredit" OnClick="lnkExamValidate_Click" Text="Credit" CssClass="control-primary" /></li>                
                                                </ul>                                                
                                            </div>                                                                      
                                        </div>                            
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <dx:ASPxGridView ID="grdExam" ClientInstanceName="grdExam" runat="server" KeyFieldName="Code" Width="100%">
                                                        <Columns>                                                            
                                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                            <dx:GridViewDataTextColumn FieldName="ExamTypeDesc" Caption="Exam Type" />
                                                            <dx:GridViewDataTextColumn FieldName="xDateTaken" Caption="Date Taken" />
                                                            <dx:GridViewDataTextColumn FieldName="ScoreRating" Caption="Rating" />
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />                                                                        
                                                    </dx:ASPxGridView>                                
                                                </div>
                                            </div>                                                           
                                        </div>                   
                                    </div>
                                </div>
                            </div>
                            </div>        
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button9" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="Panel9" TargetControlID="Button9" />
            <asp:Panel ID="Panel9" runat="server" CssClass="entryPopup">
                    <fieldset class="form" id="Fieldset9">
                    <!-- Header here -->
                        <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="Linkbutton3" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="lnkExamSave" CssClass="fa fa-floppy-o submit Fieldset6" OnClick="lnkExamSave_Click"  />      
                        </div>
                        <!-- Body here -->
                        <div  class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtAccessExamCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Examination Title :</label>
                            <div class="col-md-7">
                                <asp:DropDownList runat="server" ID="cboExamTypeNo" DataMember="EExamType" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Date Taken :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtDateTaken" CssClass="form-control " />
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateTaken" Format="MM/dd/yyyy" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateTaken" Mask="99/99/9999" MaskType="Date" />
                                <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateTaken" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Expiry Date :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtExpiryDate" CssClass="form-control" />
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtExpiryDate" Format="MM/dd/yyyy" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtExpiryDate" Mask="99/99/9999" MaskType="Date" />
                                <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtExpiryDate" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Rating :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtRatingDesc" runat="server" CssClass="form-control" />
                            </div>
                        </div>                       
                    </div>
                    <br />
                </fieldset>
            </asp:Panel>

          </div>
          <%--Eligibility--%>

          <%--Performance--%>
          <div>
          <div class="panel-heading">
             <h4 class="panel-title"><a data-toggle="collapse" href="#divPerf">Performance</a></h4>                                
             <ul class="panel-controls">                                        
                <li><asp:LinkButton runat="server" ID="lnkPerfList" OnClick="lnkPerfList_Click" Text="Candidate Entry" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkPerfAdd" OnClick="lnkPerfAdd_Click" Text="Add" CssClass="control-primary" /></li>
                <li><asp:LinkButton runat="server" ID="lnkPerfDelete" OnClick="lnkPerfDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                <uc:ConfirmBox runat="server" ID="ConfirmBox5" TargetControlID="lnkPerfDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
             </ul>                             
          </div>      
          <div id="divPerf" class="panel-body collapse in">             
            <div class="table-responsive">
                <dx:ASPxGridView ID="grdAssessPerf" ClientInstanceName="grdAssessPerf" runat="server" KeyFieldName="AssessPerfNo" Width="100%">
                    <Columns>                 
                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                            <DataItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkPerfEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("AssessPerfNo") %>' OnClick="lnkPerfEdit_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>                              
                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />                        
                        <dx:GridViewDataTextColumn FieldName="Ratings" Caption="Actual Rating" Width="20%" />
                        <dx:GridViewDataTextColumn FieldName="RatingsEquiv" Caption="Rating" Width="20%" />                        
                        <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />                                                
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
                    </Columns>                                        
                    <GroupSummary>
                        <dx:ASPxSummaryItem SummaryType="None" Visible="false" />                        
                    </GroupSummary>                                                                                     
                </dx:ASPxGridView>                                   
            </div>
          </div>

          <asp:Button ID="Button5" runat="server" style="display:none" />
          <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="Button5" PopupControlID="Panel5" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
          <asp:Panel id="Panel5" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="Fieldset4">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="Linkbutton6" CssClass="fa fa-times" ToolTip="Close" />
                    </div>
                    <div class="container-fluid entryPopupDetl">        
                        <div class="row">
                            <div class="page-content-wrap">         
                                <div class="row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">                                                                                                                                                                                                               
                                            <div>
                                                <ul class="panel-controls">                                                                                        
                                                    <li><asp:LinkButton runat="server" ID="lnkPerfValidate" OnClick="lnkPerfValidate_Click" Text="Credit" CssClass="control-primary" /></li>
                                                </ul>                                                
                                            </div>                                                                      
                                        </div>                            
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <dx:ASPxGridView ID="grdExpePerf" ClientInstanceName="grdExpe" runat="server" KeyFieldName="Code" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                            <dx:GridViewDataTextColumn FieldName="ExpeComp" Caption="Office/Department" />
                                                            <dx:GridViewDataTextColumn FieldName="Position" Caption="Position" />
                                                            <dx:GridViewDataTextColumn FieldName="xFromDate" Caption="From" />
                                                            <dx:GridViewDataTextColumn FieldName="xToDate" Caption="To" />
                                                            <dx:GridViewDataTextColumn FieldName="Ratings" Caption="Performance<br />Rating" />                                                            
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />                                                                        
                                                    </dx:ASPxGridView>                                
                                                </div>
                                            </div>                                                           
                                        </div>                   
                                    </div>
                                </div>
                            </div>
                        </div>        
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button6" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="Panel6" TargetControlID="Button6" />
            <asp:Panel ID="Panel6" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="Fieldset6">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="lnkPerfSave" CssClass="fa fa-floppy-o submit Fieldset6" OnClick="lnkPerfSave_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtAccessPerfCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber"></asp:TextBox>
                            </div>
                        </div>                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Actual Rating :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtRatings" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Equivalent Rating :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtRatingsEquiv" CssClass="form-control required number" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Remarks :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                            </div>
                        </div>                                                                    
                    </div>
                    <br />
                </fieldset>
            </asp:Panel>
          
          </div>
          <%--Performance--%>

          <%--Competency--%>
          <div>
          <div class="panel-heading">
             <h4 class="panel-title"><a data-toggle="collapse" href="#divComp">Competency</a></h4>                                                                         
          </div>      
          <div id="divComp" class="panel-body collapse in">             
            <div class="table-responsive">
                <dx:ASPxGridView ID="grdAssessComp" ClientInstanceName="grdAssessComp" runat="server" KeyFieldName="AssessCompNo" Width="100%">
                    <Columns>                 
                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                            <DataItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkCompEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Eval("AssessCompNo") & "|" & Eval("CompNo") & "|" & Eval("xStandard") & "|" & Eval("xSelf") & "|" & Eval("xAssess")  & "|" & Eval("CompDesc") & "|" & Eval("CompScaleNo") & "|" & Eval("CompScaleSelfNo")   %>' OnClick="lnkCompEdit_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>                              
                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />                        
                        <dx:GridViewDataTextColumn FieldName="CompDesc" Caption="Competency" />
                        <dx:GridViewDataTextColumn FieldName="xStandard" Caption="Min. Req." Width="10%" />                        
                        <dx:GridViewDataTextColumn FieldName="xSelf" Caption="Self Assess" Width="10%" />
                        <dx:GridViewDataTextColumn FieldName="xAssess" Caption="Assessment" Width="10%" />                                                
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
                    </Columns>                                        
                    <GroupSummary>
                        <dx:ASPxSummaryItem SummaryType="None" Visible="false" />                        
                    </GroupSummary>                                                                                     
                </dx:ASPxGridView>                                   
            </div>
          </div>

          <%--<asp:Button ID="Button11" runat="server" style="display:none" />
          <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender11" runat="server" TargetControlID="Button11" PopupControlID="Panel11" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
          <asp:Panel id="Panel11" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="Panel11">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="Linkbutton12" CssClass="fa fa-times" ToolTip="Close" />
                    </div>
                    <div class="container-fluid entryPopupDetl">        
                        <div class="row">
                            <div class="page-content-wrap">         
                                <div class="row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">                                                                                                                                                                                                               
                                            <div>
                                                <ul class="panel-controls">                                                                                        
                                                    <li><asp:LinkButton runat="server" ID="LinkButton13" OnClick="lnkPerfValidate_Click" Text="Credit" CssClass="control-primary" /></li>
                                                </ul>                                                
                                            </div>                                                                      
                                        </div>                            
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grdExpe" runat="server" KeyFieldName="Code" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                            <dx:GridViewDataTextColumn FieldName="ExpeComp" Caption="Office/Department" />
                                                            <dx:GridViewDataTextColumn FieldName="Position" Caption="Position" />
                                                            <dx:GridViewDataTextColumn FieldName="xFromDate" Caption="From" />
                                                            <dx:GridViewDataTextColumn FieldName="xToDate" Caption="To" />
                                                            <dx:GridViewDataTextColumn FieldName="Ratings" Caption="Performance<br />Rating" />                                                            
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />                                                                        
                                                    </dx:ASPxGridView>                                
                                                </div>
                                            </div>                                                           
                                        </div>                   
                                    </div>
                                </div>
                            </div>
                        </div>        
                    </div>
                </fieldset>
            </asp:Panel>--%>

            <asp:Button ID="Button12" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender12" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="Panel12" TargetControlID="Button12" />
            <asp:Panel ID="Panel12" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="Fieldset12">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="Linkbutton14" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="lnkCompSave" CssClass="fa fa-floppy-o submit Fieldset6" OnClick="lnkCompSave_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtAccessCompCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control" placeholder="Autonumber"></asp:TextBox>
                            </div>
                        </div>                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Competency :</label>
                            <div class="col-md-7">
                                <asp:HiddenField runat="server" ID="hifCompNo" />
                                <asp:TextBox runat="server" ID="txtCompDesc" CssClass="form-control" />
                            </div>
                        </div>                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Min. Req. :</label>
                            <div class="col-md-7">                                
                                <asp:HiddenField runat="server" ID="hifCompScaleNo" />
                                <asp:TextBox runat="server" ID="txtCompScaleDesc" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Self Assessment :</label>
                            <div class="col-md-7">                             
                                <asp:HiddenField runat="server" ID="hifCompScaleSelfNo" />   
                                <asp:TextBox runat="server" ID="txtCompScaleSelfDesc" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Assessment :</label>
                            <div class="col-md-7">
                                <asp:HiddenField runat="server" ID="hifCompScaleAssessNo" />   
                                <asp:DropDownList runat="server" ID="cboCompScaleAssessNo" CssClass="form-control required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Remarks :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtAssessCompRemarks" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                            </div>
                        </div>                                                                                        
                    <br />
                </fieldset>
            </asp:Panel>
          
          </div>
          <%--Competency--%>
                                              
       </div>                  
    </div>
</div>

<uc:FileUpload runat="server" ID="FileUpload" />
</asp:Content>


