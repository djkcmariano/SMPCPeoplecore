<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmpInfo.ascx.vb" Inherits="Include_EmpInfo" %>

<style type="text/css">
    .header
    {
        background-color: #f7f7f7;
        border-top: solid 1px #c1c1c1;
        border-bottom: solid 1px #c1c1c1;
        padding:5px;
        font-weight:bold;
        margin-bottom:10px;            
    }                             
</style>

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div class="container-fluid entryPopupDetl">        
        <div class="page-content-wrap">
                <br />                   
                <div class="row">
                    <div class="col-md-2">        
                        <asp:Image runat="server" ID="imgPic" Width="100" Height="120" ImageAlign="Middle" BorderWidth="1" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="lblInfo1" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="lblInfo2" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label runat="server" ID="lblInfo3" />
                    </div>
                </div>
                <br />
                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Education</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdEduc" ClientInstanceName="grdEduc" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="EmployeeEducNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="EducLevelDesc" Caption="Educational Level" />
                                                <dx:GridViewDataTextColumn FieldName="SchoolDesc" Caption="School" />
                                                <dx:GridViewDataTextColumn FieldName="CourseDesc" Caption="Course" />
                                                <dx:GridViewDataTextColumn FieldName="FromDate" Caption="Start Date" />
                                                <dx:GridViewDataTextColumn FieldName="ToDate" Caption="End Date" />                                        
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>

                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Eligibility</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdExam" ClientInstanceName="grdExam" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="EmployeeExamNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="ExamTypeDesc" Caption="Examination Type" />
                                                <dx:GridViewDataTextColumn FieldName="ScoreRating" Caption="Rating" />
                                                <dx:GridViewDataTextColumn FieldName="xDateTaken" Caption="Date Taken" />                                            
                                                <dx:GridViewDataTextColumn FieldName="xDateExpired" Caption="Date Expired" />                                            
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>

                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Employment Record</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdExpe" ClientInstanceName="grdExpe" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="EmployeeExpeNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="Position" Caption="Position" />
                                                <dx:GridViewDataTextColumn FieldName="ExpeComp" Caption="Employer" />
                                                <dx:GridViewDataTextColumn FieldName="xFromDate" Caption="Start Date" />
                                                <dx:GridViewDataTextColumn FieldName="xToDate" Caption="End Date" />
                                                <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Monthly Salary" />
                                                <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employment Status" />
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div> 
                 
                <div class="panel panel-default">
                   <div class="panel-heading">
                        <div>
                            <h4>Job Description / Specification</h4>                                
                        </div>
                        <div> </div>                           
                    </div>
                    <div> </div>
                    <div> </div>
                    <div class="col-md-12">                                                        
                        <div class="form-horizontal">                     
                            <asp:Literal runat="server" ID="lContent" />
                            <br />
                        </div>                    
                    </div>
                </div> 

                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Training</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdEmpTrn" ClientInstanceName="grdEmpTrn" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="EmployeeNo">                                                                                   
                                            <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />
                                            <dx:GridViewDataTextColumn FieldName="IsQS" Caption="CSC QS" />
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>


                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Competencies</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdEmpComp" ClientInstanceName="grdEmpComp" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="EmployeeNo">                                                                                   
                                            <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="CompClusterDesc" Caption="Cluster" />
                                            <dx:GridViewDataTextColumn FieldName="CompCode" Caption="Code" Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="CompDesc" Caption="Competency" />
                                            <dx:GridViewDataTextColumn FieldName="CompScaleDesc" Caption="Proficiency Level" />
                                            <dx:GridViewDataTextColumn FieldName="Anchor" Caption="Indicator" />
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>


                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Performance Rating</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdEmpReview" ClientInstanceName="grdEmpReview" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="EmployeeNo">                                                                                   
                                            <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" />
                                            <dx:GridViewDataTextColumn FieldName="AveRating" Caption="Rating" />
                                            <dx:GridViewDataTextColumn FieldName="PERatingDesc" Caption="Adjectival Rating" />
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
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
