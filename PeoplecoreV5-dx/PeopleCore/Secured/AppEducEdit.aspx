<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppEducEdit.aspx.vb" Inherits="Secured_AppEducEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />                
            </center>            
            <asp:Label runat="server" ID="lbl" /> 
        </Header>
        <Content>
            <br />
            <div class="page-content-wrap">         
                <div class="row">
                    <i>Please include Elementary , Secondary education (if applicable), College/University (if applicable), and Graduate studies (if applicable).</i>
                </div>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                                <h4>&nbsp;</h4>                                
                            </div>
                            <div>
                                <div>                                                                                    
                                    <ul class="panel-controls">                                                                                                                        
                                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                                
                                    </ul>                                    
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                    
                                </div>                                
                            </div>                           
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="ApplicantEducNo" SkinID="grdDX">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ApplicantEducNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="EducLevelDesc" Caption="Level" />
                                            <dx:GridViewDataTextColumn FieldName="SchoolDesc" Caption="School" />
                                            <dx:GridViewDataTextColumn FieldName="CourseDesc" Caption="Course" />
                                            <dx:GridViewDataTextColumn FieldName="FromDate" Caption="From" />
                                            <dx:GridViewDataTextColumn FieldName="ToDate" Caption="To" />                                            
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

    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">

            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtApplicantEducNo" ReadOnly="true" CssClass="form-control" Placeholder= "Autonumber" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtApplicantEducCode" ReadOnly="true" CssClass="form-control" Placeholder= "Autonumber" />
                </div>

            </div>           
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Level :</label>
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
            <div class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsOtherSchool" Text="&nbsp;Tick if data is not in the drop down list" AutoPostBack="true" OnCheckedChanged="txtIsOtherSchool_CheckedChanged" /><br />                    
                </div>
            </div>   
            <div class="form-group">
                <label class="col-md-4 control-label has-space">School Address :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtSchoolAddress" CssClass="form-control" TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Field of Study :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFieldOfStudy" CssClass="form-control" Visible="false" />
                    <asp:DropDownList runat="server" ID="cboFieldOfStudyNo" CssClass="form-control" DataMember="EFieldOfStudy" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required" runat="server" id="lblEduc">Basic Education / Degree / Course :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboCourseNo" CssClass="form-control required" />
                </div>
            </div> 

            <div class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label has-space"><asp:CheckBox runat="server" ID="txtIsOtherCourse" AutoPostBack="true" OnCheckedChanged="txtIsOtherCourse_CheckedChanged"/>  If others (please specify) :</label>
                <div class="col-md-7" >
                    <asp:TextBox runat="server" ID="txtOtherCourse" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Course / Major / Degree" />                    
                </div>
            </div>

            <h5><b>PERIOD OF ATTENDANCE</b></h5>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Attended From :</label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="cboFromMonth" CssClass="form-control required" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="cboFromDay" CssClass="form-control" />                   
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFromYear" CssClass="form-control required" placeholder="Year" MaxLength="4" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtFromYear" />                               
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Attended To :</label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="cboToMonth" CssClass="form-control required" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="cboToDay" CssClass="form-control" />                    
                </div>
                <div class="col-md-2">
                    <%--<asp:TextBox runat="server" ID="txtToYear" CssClass="form-control required" placeholder="Year" MaxLength="4" AutoPostBack="true" OnTextChanged="txtToYear_TextChanged" /> --%>
                    <asp:TextBox runat="server" ID="txtToYear" CssClass="form-control required" placeholder="Year" MaxLength="4" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtToYear" />                     
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Year Graduated :</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtYearGrad" CssClass="form-control number" placeholder="Year" MaxLength="4" AutoPostBack="true" OnTextChanged="txtYearGrad_TextChanged" CausesValidation="false" />            
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtYearGrad" />         
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Highest Level / Units Earned (if not graduated) :</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtUnitEarned" CssClass="form-control number" />                    
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Academic Honors Received :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboHonorNo" runat="server" DataMember="EHonor" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Scholarship :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtScholarship" CssClass="form-control" />
                </div>
            </div>

            <br />
        </div>                  
        </fieldset>
    </asp:Panel>    
</asp:Content>

