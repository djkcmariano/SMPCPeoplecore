<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="SelfAppRandomAnsApprList_QS.aspx.vb" Inherits="SecuredManager_SelfAppRandomAnsApprList_QS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">            
    .gv th
    {
        border-bottom: solid 1px #000;
        border-left: none 0 #fff;
        border-right: none 0 #fff;
        border-top: none 0 #fff;
        padding:10px;
        text-align:left;      
    }

    .gv, .gv tr, .gv td
    {       
        border: none 0 #fff;    
    }

    .gv td
    {
        padding: 5px    
    }               
                              
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<br />
<h4><asp:Label runat="server" ID="lbl"></asp:Label></h4>
<br />
<div class="page-content-wrap" >         

<asp:Literal runat="server" ID="lContent" />  

<div class="row">
<div class="panel panel-default">
    <div class="panel-heading">
        <div class="form-horizontal">  
            <div class="col-md-12">
                Qualification Standard
            </div>
        </div>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="table-responsive">
                <mcn:DataPagerGridView ID="grdQS" runat="server" AllowPaging="false">
                <Columns>
                    <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" HeaderText="Type">
                        <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="10%" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Title") %>' ID="lblType" />                                       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="30%" />
                        <ItemTemplate>
                            <asp:HiddenField runat="server" Value='<%# Bind("QSTypeNo") %>' ID="hifQSTypeNo" />
                            <asp:HiddenField runat="server" Value='<%# Bind("ID") %>' ID="hifQSNo" />
                            <asp:HiddenField runat="server" Value='<%# Bind("ApplicantQSNo") %>' ID="hifApplicantQSNo" />
                            <asp:Label runat="server" Text='<%# Bind("Value") %>' ID="lblValue" />                                       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                        <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="20%" />
                        <ItemTemplate>
                            <asp:RadioButtonList runat="server" ID="rbl" Font-Bold="false" Font-Size="X-Small" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                <asp:ListItem Text="&nbsp;Not Complied&nbsp;" Value="0" />
                                <asp:ListItem Text="&nbsp;Complied&nbsp;" Value="1" />
                            </asp:RadioButtonList>

                            <asp:HiddenField runat="server" Value='<%# Bind("IsPass") %>' ID="hifIsPass" />
                            <asp:HiddenField runat="server" Value='<%# Bind("IsComplied") %>' ID="hifIsComplied" />
                            <%--<asp:CheckBox runat="server" ID="chk" Visible='<%# Bind("IsEnabled") %>' Checked='<%# Bind("IsPass") %>'  />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtRemarks" Text='<%# Bind("Remarks") %>' CssClass="form-control" Width="100%" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Value">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtValue" Text='<%# Bind("NumericValue") %>' CssClass="form-control number" Width="100%" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="10%" />
                    </asp:TemplateField>                                
                </Columns>
                </mcn:DataPagerGridView>
            </div>
        </div>
        <div class="row">
            <div class="col-md-10 col-md-offset-2">                
                <div class="pull-right">                                            
                    <asp:Button ID="btnUpdateQS" Text="Update" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnUpdateQS_Click" ToolTip="Click here to update" ></asp:Button>                                            
                </div>                                        
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label runat="server" Font-Size="XX-Small" Font-Bold="True" ID="lblEduc" ForeColor="Red">Education</asp:Label>
                    <asp:GridView runat="server" ID="grdEducTaken" AutoGenerateColumns="false" BorderWidth="0" Width="100%" Font-Size="XX-Small" CssClass="gv">
                        <Columns>                                
                            <asp:BoundField DataField="SchoolDesc" ItemStyle-BorderWidth="0" HeaderText="School" HeaderStyle-HorizontalAlign="Left"  />
                            <asp:BoundField DataField="CourseDesc" ItemStyle-BorderWidth="0" HeaderText="Course" HeaderStyle-HorizontalAlign="Left"  />
                            <asp:TemplateField ItemStyle-BorderWidth="0" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# "(" & Eval("FromDate") & " - " & Eval("ToDate") & ")" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Label runat="server" Font-Size="XX-Small" Font-Bold="True" ID="lblExam" ForeColor="Red">Eligibility</asp:Label>
                    <asp:GridView runat="server" ID="grdExamTaken" AutoGenerateColumns="false" BorderWidth="0" Width="100%" Font-Size="XX-Small" CssClass="gv">
                        <Columns>                                
                            <asp:BoundField DataField="ExamTypeDesc" ItemStyle-BorderWidth="0" HeaderText="Examination Title" HeaderStyle-HorizontalAlign="Left"  />
                            <asp:BoundField DataField="DateTaken" ItemStyle-BorderWidth="0" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"  />                                                              
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Label runat="server" Font-Size="XX-Small" Font-Bold="True" ID="lblExpe" ForeColor="Red">Work Experience</asp:Label>
                    <asp:GridView runat="server" ID="grdExpeTaken" AutoGenerateColumns="false" BorderWidth="0" Width="100%" Font-Size="XX-Small" CssClass="gv">
                        <Columns>                                
                            <asp:BoundField DataField="ExpeComp" ItemStyle-BorderWidth="0" HeaderText="Agency/Company" HeaderStyle-HorizontalAlign="Left"  />
                            <asp:BoundField DataField="Position" ItemStyle-BorderWidth="0" HeaderText="Position" HeaderStyle-HorizontalAlign="Left"  />                                                              
                            <asp:TemplateField ItemStyle-BorderWidth="0" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# "(" & Eval("FromDate") & " - " & Eval("ToDate") & ")" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Label runat="server" Font-Size="XX-Small" Font-Bold="True" ID="lblTrn" ForeColor="Red">Training Programs</asp:Label>
                    <asp:GridView runat="server" ID="grdTrainingTaken" AutoGenerateColumns="false" BorderWidth="0" Width="100%" Font-Size="XX-Small" CssClass="gv">
                        <Columns>                                
                            <asp:BoundField DataField="TrainingTitleDesc" ItemStyle-BorderWidth="0" HeaderText="Training Program" HeaderStyle-HorizontalAlign="Left"  />                                
                            <asp:TemplateField ItemStyle-BorderWidth="0" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# "(" & Eval("FromDate") & " - " & Eval("ToDate") & ")" %>' />
                                    </ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                    </asp:GridView>
                </div>
            </div>                         
        </div>                                                                               
    </div>
</div>                                                                                                                                                                                                                                           
</div>

<div class="row">
<div class="panel panel-default">
    <div class="panel-heading">
        <div class="form-horizontal">  
            <div class="col-md-12">
                Competency
            </div>
        </div>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="table-responsive">
                <mcn:DataPagerGridView ID="grdComp" runat="server" AllowPaging="false" AutoGenerateColumns="true" SkinID="grdAuto" />                                
            </div>
        </div>                                                                        
    </div>
</div>                                                                                                                                                                                                                                           
</div>

<div class="row">
<div class="panel panel-default">
    <div class="panel-heading">
        <div class="form-horizontal">  
            <div class="col-md-12">
                Job Description                
            </div>
        </div>
    </div>
    <div class="panel-body">              
        <div class="row">
            <div class="table-responsive">
                <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="ApplicantResponsibilityNo">
                    <Columns>                                                                                                                
                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                            <ItemTemplate>
                                <asp:Label ID="lblApplicantResponsibilityNo" runat="server"   Text='<%# Bind("ApplicantResponsibilityNo") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Description" HeaderText="Duties and Responsibilities" SortExpression="Description" Visible="true">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>  
                        <asp:TemplateField HeaderText="Relevant Exp. Credited"  >
                            <ItemTemplate>
                                <asp:Textbox ID="lblExp_Credited" runat="server"   Text='<%# Bind("Exp_Credited") %>' CssClass="form-control" Width="100%"></asp:Textbox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Office/Department"  >
                            <ItemTemplate>
                                <asp:Textbox ID="lblDepartmentdesc" runat="server"   Text='<%# Bind("DepartmentDesc") %>' CssClass="form-control" Width="100%"></asp:Textbox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Position"  >
                            <ItemTemplate>
                                <asp:Textbox ID="lblPositionDesc" runat="server"   Text='<%# Bind("PositionDesc") %>' CssClass="form-control" Width="100%"></asp:Textbox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Presented"  >
                            <ItemTemplate>
                                <asp:Textbox ID="lblDocPresented" runat="server"   Text='<%# Bind("DocPresented") %>' CssClass="form-control" Width="100%"></asp:Textbox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period From"  >
                            <ItemTemplate>
                                <asp:Textbox ID="lblPeriodFrom" runat="server"   Text='<%# Bind("PeriodFrom") %>' CssClass="form-control" Width="100%"></asp:Textbox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period To"  >
                            <ItemTemplate>
                                <asp:Textbox ID="lblPeriodTo" runat="server"   Text='<%# Bind("PeriodTo") %>' CssClass="form-control" Width="100%"></asp:Textbox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Percentage Weight"  >
                            <ItemTemplate>
                                <asp:Textbox ID="lblWeighted" runat="server"   Text='<%# Bind("Weighted") %>' CssClass="form-control" Width="100%"></asp:Textbox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Equivalent Experience in years/months/days"  >
                            <ItemTemplate>
                                <asp:Textbox ID="lblExp" runat="server"   Text='<%# Bind("Exp") %>' CssClass="form-control" Width="100%"></asp:Textbox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>                                                                                                                                                                            
                </Columns>                                
                </mcn:DataPagerGridView>                               
            </div>
        </div> 
        <div class="row">
            <div class="col-md-4">
                <!-- Paging here -->
                <asp:DataPager ID="dpMain" runat="server" PagedControlID="grdMain" PageSize="10">
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
                    <!-- Button here btn-group -->
                <div class="pull-right">                                            
                    <asp:Button ID="btnSave" Text="Update" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="Click here to update" ></asp:Button>                                            
                </div>                                        
            </div>
        </div>                                                                    
    </div>
</div>                                                                                                                                                                                                                                           
</div>

</div>
</asp:Content>
