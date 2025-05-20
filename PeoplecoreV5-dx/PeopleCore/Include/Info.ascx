<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Info.ascx.vb" Inherits="Include_Info" %>


<asp:Button ID="Button2" runat="server" style="display:none;" />
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
                    <div class="col-md-10">
                        <asp:Label runat="server" ID="lblInfo" />
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
                                        <mcn:DataPagerGridView ID="grdEduc" runat="server" AllowSorting="true" AllowPaging="false">
                                        <Columns>
                                            <asp:BoundField DataField="EducLevelDesc" HeaderText="Educational Level" SortExpression="EducLevelDesc">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SchoolDesc" HeaderText="School" SortExpression="SchoolDesc">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CourseDesc" HeaderText="Course" SortExpression="CourseDesc">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign ="Left" Width="25%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FromDate" HeaderText="From" SortExpression="FromDate">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:BoundField>                
                                            <asp:BoundField DataField="ToDate" HeaderText="To" SortExpression="ToDate">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:BoundField>
                                        </Columns>
                                        </mcn:DataPagerGridView>                                
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
                                        <mcn:DataPagerGridView ID="grdExam" runat="server" AllowSorting="true" AllowPaging="false">
                                        <Columns>
                                            <asp:BoundField DataField="ExamTypeDesc" HeaderText="Examination Type" SortExpression="ExamTypeDesc">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="40%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ScoreRating" HeaderText="Rating" SortExpression="ScoreRating">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="xDateTaken" HeaderText="Date Of Exam" SortExpression="DateTaken">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign ="Left" Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="xDateExpired" HeaderText="Expiry Date" SortExpression="DateExpired">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:BoundField>                            
                                        </Columns>
                                        </mcn:DataPagerGridView>                                
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
                                    <h4>Work Experience</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grdExpe" runat="server" AllowSorting="true" AllowPaging="false">
                                        <Columns>
                                            <asp:BoundField DataField="xFromDate" HeaderText="From" SortExpression="FromDate">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="xToDate" HeaderText="To" SortExpression="ToDate">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ExpeComp" HeaderText="Employer" SortExpression="ExpeComp">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign ="Left" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CurrentSalary" HeaderText="Monthly Salary" SortExpression="CurrentSalary">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign ="Left" Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmployeeStatDesc" HeaderText="Employment Status" SortExpression="EmployeeStatDesc">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign ="Left" Width="10%" />
                                            </asp:BoundField>                            
                                        </Columns>
                                        </mcn:DataPagerGridView>                                
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
                                    <h4>Training</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grdTrain" runat="server" AllowSorting="true" AllowPaging="false">
                                        <Columns>
                                            <asp:BoundField DataField="TrainingTitleDesc" HeaderText="Program Title" SortExpression="TrainingTitleDesc">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IssuedBy" HeaderText="Sponsored / Conducted" SortExpression="IssuedBy">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="xFromDate" HeaderText="From" SortExpression="xFromDate">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="xToDate" HeaderText="To" SortExpression="xToDate">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign ="Left" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NoOfHrs" HeaderText="No. of Hours" SortExpression="NoOfHrs">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign ="Left" Width="10%" />
                                            </asp:BoundField>                            
                                        </Columns>
                                        </mcn:DataPagerGridView>                                
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
