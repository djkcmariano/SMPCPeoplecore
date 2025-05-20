<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PEForm.ascx.vb" Inherits="Include_PEForm" %>
<%@ Register Src="~/Include/ChatBox.ascx" TagName="ChatBox" TagPrefix="uc" %>
<%@ Register Src="~/Include/PEReviewTab.ascx" TagName="PEReviewTab" TagPrefix="uc" %>
<%@ Register Src="~/Include/ConfirmBox.ascx" TagName="ConfirmBox" TagPrefix="uc" %>


<style type="text/css">

@media (min-width: 768px) {
  .dl-horizontal dt { text-align: left; }
}

.container {
  background-color: white;
  float: left;
  padding: 5px;
  margin:0px;
}

.container-border 
{
  padding: 5px;
  margin:0px;
}

#container1 {
  width: 100%;
}
 
</style>

<script type="text/javascript">
    function computerating() {

        var weight = document.getElementById("ctl00_cphBody_PEForm1_txtGWeighted").value;
        var proficiency = document.getElementById("ctl00_cphBody_PEForm1_txtGProficiency").value;
        if ((weight > 100) || (proficiency > 100)) {
            document.getElementById("ctl00_cphBody_PEForm1_txtGRating").value = "Invalid value.";
        }
        else {
            weight = weight / 100;
            document.getElementById("ctl00_cphBody_PEForm1_txtGRating").value = Math.round((Number(weight * proficiency)) * 100) / 100;
        }

    }
</script>

<div class="row" style="margin-bottom:5px;">
    <div class="col-md-10">
        <h5 style="padding-top:5px;" class="text-primary">Review No. <asp:label ID="lblCode" runat="server" /></h5>
    </div>
    <div class="col-md-2">
        <div class="btn-group pull-right" id="DivSettings" runat="server">
          <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown">&nbsp;<span class="fa fa-cog" style="padding-top:5px;"></span></button>
          <ul class="dropdown-menu">
            <li><asp:LinkButton runat="server" ID="lnkCycle" OnClick="lnkCycle_Click" >Settings</asp:LinkButton></li>
            <li><asp:LinkButton runat="server" ID="lnkEval" OnClick="lnkCycle_Click" Visible="false">Evaluator</asp:LinkButton></li>
          </ul>
        </div>
    </div>
</div>


<div class="row">
  <div class="panel panel-default" style="margin-bottom:0px;">
    <div class="panel-heading" style="padding:5px;">
            <div class="panel-title">
                <asp:Image runat="server" ID="imgPhoto" width="50" height="50" CssClass="img-circle" style="border: 2px solid white; padding:0px;margin:0px" />&nbsp;&nbsp;
                <a role="button" data-toggle="collapse" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                    <asp:label ID="lblName" runat="server" style="color:#069; cursor:pointer;" />
                </a>
            </div>            
<%--            <ul class="panel-controls">                        
                <li>
                <asp:LinkButton runat="server" ID="lnkInfo" CssClass="fa fa-info-circle fa-2x" style="text-decoration: none;"
                role="button" data-toggle="collapse" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne" /> 
                </li>
            </ul>--%>
    </div>
    <div id="collapseOne" class="panel-collapse collapse">
      <div id="container1" class="container">
          <asp:Repeater runat="server" ID="rRef">        
                <ItemTemplate>   
                   <div class="col-md-12 container-border">
                        <div class="col-md-6">
                            <asp:label ID="lblDisplay1" runat="server" Text='<%# Bind("Display1") %>' />
                        </div>
                        <div class="col-md-6">
                            <asp:label ID="lblDisplay2" runat="server" Text='<%# Bind("Display2") %>' />
                        </div>
                    </div> 
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
  </div>
</div>

<div class="panel panel-default">
<asp:Panel runat="server" ID="pHeader" />




<uc:PEReviewTab runat="server" ID="PEReviewTab">
<Content>
<br />
            <asp:Panel id="pnlNewItem" runat="server" Visible="false">
                <div class="pull-right"> 
                    <asp:Button runat="server"  ID="lnkAdd" CssClass="btn btn-success" Text="Add New Item" OnClick="lnkAdd_Click" />
                    <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary" ValidationGroup="EvalValidationGroup" Text="Save" OnClick="lnkSave_Click" />
                </div>
            </asp:Panel>

            <uc:ChatBox runat="server" ID="ChatBox1">
            </uc:ChatBox>

            <div class="row">
                <asp:Panel runat="server" ID="pCate" />
            </div>

            <!-- KRA here -->
            <div class="row">
                <asp:Panel runat="server" ID="pForm" />
            </div>   
            
            <!-- ACI Goals -->  
            <asp:Panel id="pnlGoals" runat="server" Visible="false">
                <div class="panel-body" style="padding-top:10px;padding-bottom:10px;">
                        <div class="pull-right"> 
                                <asp:Button runat="server"  ID="lnkAddGoals" CssClass="btn btn-default" Text="Add Goals" OnClick="lnkAddGoals_Click" />
                                <asp:Button runat="server"  ID="lnkPreviewGoals" CssClass="btn btn-default" Text="Preview" OnClick="lnkPreviewGoals_Click" OnPreRender="lnkPrint_PreRender"  />
                        </div>
                 </div> 
                <div class="row">
                    <div class="page-content-wrap"> 
                        <div class="panel-body">
                            <mcn:DataPagerGridView ID="grdMain" SkinID="grdMBO" runat="server" DataKeyNames="PEStandardRevNo" >
                                        <Columns>
                                                <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server"   Text='<%# Bind("PEStandardRevNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            
                                                <asp:BoundField DataField="OrderLevelItem" HeaderText="#" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center"  >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="Goals"  >
                                                    <ItemTemplate>
                                                        <div style="vertical-align:middle;">
                                                            <div class="col-md-6">
                                                                <h5 class="title"><asp:LinkButton runat="server" ID="lnkEditGoals" OnClick="lnkEditGoals_Click" Text='<%# Bind("PEStandardDesc") %>' CommandArgument='<%# Bind("PEStandardRevNo") %>' /></h5><p class="summary"><asp:Label ID="lblPEStandard" runat="server" Text='<%# Bind("Standard") %>' /></p>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <dl><dt>Category</dt><dd><asp:Label ID="Label1" runat="server" Text='<%# Bind("PEReviewDimDesc") %>' /></dd></dl>
                                                                <dl><dt>Status</dt><dd><asp:Label ID="Label2" runat="server" Text='<%# Bind("PEStandardStatDesc") %>' /></dd></dl>
                                                                <dl><dt>Priority</dt><dd><asp:Label ID="Label8" runat="server" Text='<%# Bind("PEPriorityDesc") %>' /></dd></dl>
                                                                <dl><dt><asp:Label ID="Label9" runat="server" Text='<%# Bind("DueDate") %>' /></dt></dl>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <dl><dt>Weight</dt><dd><asp:Label ID="Label3" runat="server" Text='<%# Bind("Weighted") %>' />%</dd></dl>
                                                                <dl><dt>Completion Percentage</dt><dd><asp:Label ID="Label4" runat="server" Text='<%# Bind("Proficiency") %>' />%</dd></dl>
                                                                <dl><dt>Rating</dt><dd><asp:Label ID="Label11" runat="server" Text='<%# Bind("Rating") %>' /></dd></dl>
                                                            </div>
                                                            <div class="col-md-2" >
                                                                <asp:LinkButton runat="server" ID="lnkTask" CssClass="list-group-item btn-xs" OnClick="lnkTask_Click" CommandArgument='<%# Bind("PEStandardRevNo") %>' >
                                                                    <i class="fa fa-tasks fa-fw"></i> Tasks<span class="pull-right text-muted"><em><asp:Label ID="Label7" runat="server" Text='<%# Bind("CountTasks") %>' /></em></span>
                                                                </asp:LinkButton> 
                                                                 <asp:LinkButton runat="server" ID="lnkFiles" CssClass="list-group-item btn-xs" OnClick="lnkFiles_Click" CommandArgument='<%# Bind("PEStandardRevNo") %>' >
                                                                    <i class="fa fa-paperclip fa-fw"></i> Attached<span class="pull-right text-muted"><em><asp:Label ID="Label10" runat="server" Text='<%# Bind("CountFiles") %>' /></em></span>
                                                                </asp:LinkButton> 
                                                                <asp:LinkButton runat="server" ID="lnkDeleteGoals" CssClass="list-group-item btn-xs"  OnClick="lnkDeleteGoals_Click" CommandArgument='<%# Bind("PEStandardRevNo") %>'  Visible='<%# Bind("IsEnabled") %>' >
                                                                    <i class="fa fa-trash-o fa-fw"></i> Delete<span class="pull-right text-muted"><em></em></span>
                                                                </asp:LinkButton> 
                                                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDeleteGoals" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  Visible='<%# Bind("IsEnabled") %>'  /> 
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Select" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" Enabled='<%# Bind("IsEnabled") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                                                </asp:TemplateField>
                                            </Columns>
                                    </mcn:DataPagerGridView>

                                    <div class="panel-footer" style="padding-top:15px;padding-bottom:0px;">
                                        <dl class="row">
                                          <dt class="col-md-2">Total Weight</dt>
                                          <dd class="col-md-4"><asp:Label ID="lblTotalWeight" runat="server" />%</dd>
                                          <dt class="col-md-2">Total Rating</dt>
                                          <dd class="col-md-4"><asp:Label ID="lblTotalRating" runat="server" /></dd>
                                        </dl>
                                    </div>
                            </div>
                            
                        </div>
                </div>


<%--                    <div class="panel-body">
                        <div class="jumbotron">
                            <h4>Summary</h4>
                            <div class="container-fluid">
                                <dl class="row">
                                  <dt class="col-md-1">Weight</dt>
                                  <dd class="col-md-11">100%</dd>

                                  <dt class="col-md-1">Rating</dt>
                                  <dd class="col-md-11">98</dd>
                               </dl>
                            </div>
                        </div>
                    </div>--%>

            </asp:Panel> 

            <!-- ACI Task --> 
            <asp:Panel id="pnlTask" runat="server" Visible="false">
            <div class="panel-body">
                <h3 class="page-header" style="margin-top:0px;margin-bottom:10px;">
                    <asp:Linkbutton ID="lnkGoals" runat="server" Font-Underline="false" OnClick="lnkGoals_Click" >
                    <i class="fa fa-arrow-circle-o-left fa-lg"></i> <asp:Label ID="lblGoalName" runat="server" />
                    </asp:Linkbutton>
                </h3>
               <h2 >Tasks 
               <div class="pull-right"> 
                    <asp:Button runat="server"  ID="lnkAddTask" CssClass="btn btn-default" Text="Add Task" OnClick="lnkAddTask_Click" /> 
                </div>
               </h2>
                <%--  <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                <br />--%>
            </div>
            
            <div class="row">
                <div class="page-content-wrap"> 
                    <div class="panel-body">
                        <mcn:DataPagerGridView ID="grdTask" SkinID="grdMBO" runat="server" DataKeyNames="PEStandardRevTaskNo" >
                                    <Columns>
                                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server"   Text='<%# Bind("PEStandardRevTaskNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="OrderLevel" HeaderText="#" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Tasks"  >
                                                <ItemTemplate>
                                                    <div style="vertical-align:middle;">
                                                        <div class="col-md-6">
                                                            <h5 class="title"><asp:LinkButton runat="server" ID="lnkEditTask" OnClick="lnkEditTask_Click" Text='<%# Bind("PEStandardRevTaskDesc") %>' CommandArgument='<%# Bind("PEStandardRevTaskNo") %>' /></h5><p class="summary"><asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>' /></p>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <dl><dt>Status</dt><dd><asp:Label ID="Label2" runat="server" Text='<%# Bind("PEStandardStatDesc") %>' /></dd></dl>
                                                            <dl><dt>Priority</dt><dd><asp:Label ID="Label8" runat="server" Text='<%# Bind("PEPriorityDesc") %>' /></dd></dl>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <dl><dt>Completion Percentage</dt><dd><asp:Label ID="Label4" runat="server" Text='<%# Bind("Proficiency") %>' />%</dd></dl>
                                                            <dl><dt><asp:Label ID="Label9" runat="server" Text='<%# Bind("DueDate") %>' /></dt></dl>
                                                        </div>
                                                         <div class="col-md-2" >
                                                            <asp:LinkButton runat="server" ID="lnkDeleteTask" CssClass="list-group-item btn-xs"  OnClick="lnkDeleteTask_Click" CommandArgument='<%# Bind("PEStandardRevTaskNo") %>' Visible='<%# Bind("IsEnabled") %>' >
                                                                <i class="fa fa-trash-o fa-fw"></i> Delete<span class="pull-right text-muted"><em></em></span>
                                                            </asp:LinkButton> 
                                                            <uc:ConfirmBox runat="server" ID="cfbDeleteTask" TargetControlID="lnkDeleteTask" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" Visible='<%# Bind("IsEnabled") %>'  /> 
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Select" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Enabled='<%# Bind("IsEnabled") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                </mcn:DataPagerGridView>
                        </div>
                    </div>
            </div>
            </asp:Panel>

            <!-- ACI Files --> 
            <asp:Panel id="pnlFiles" runat="server" Visible="false">
            <div class="panel-body">
                <h3 class="page-header" style="margin-top:0px;margin-bottom:10px;">
                    <asp:Linkbutton ID="lnkGoals1" runat="server" Font-Underline="false" OnClick="lnkGoals_Click" >
                    <i class="fa fa-arrow-circle-o-left fa-lg"></i> <asp:Label ID="lblGoalName1" runat="server" />
                    </asp:Linkbutton>
                </h3>
               <h2 >Attachments
               <div class="pull-right"> 
                    <asp:Button runat="server"  ID="lnkAddFiles" CssClass="btn btn-default" Text="Add Files" OnClick="lnkAddFiles_Click" /> 
                </div>
               </h2>
                <%--  <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                <br />--%>
            </div>
            
            <div class="row">
                <div class="page-content-wrap"> 
                    <div class="panel-body">
                        <mcn:DataPagerGridView ID="grdFiles" SkinID="grdMBO" runat="server" DataKeyNames="PEStandardRevFilesNo" >
                                    <Columns>
                                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server"   Text='<%# Bind("PEStandardRevFilesNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="PEStandardRevFilesNo" HeaderText="#" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Files"  >
                                                <ItemTemplate>
                                                    <div style="vertical-align:middle;">
                                                        <div class="col-md-4">
                                                            <h5 class="title"><asp:LinkButton runat="server" ID="lnkEditFiles" OnClick="lnkEditFiles_Click" Text='<%# Bind("PEStandardRevFilesDesc") %>' CommandArgument='<%# Bind("PEStandardRevFilesNo") %>' /></h5><p class="summary"><asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>' /></p>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <dl><dt>Uploaded By</dt><dd><asp:Label ID="Label2" runat="server" Text='<%# Bind("EncodeByName") %>' /></dd></dl>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <dl><dt>Date Uploaded</dt><dd><asp:Label ID="Label5" runat="server" Text='<%# Bind("EncodeDate") %>' /></dd></dl>
                                                        </div>
                                                         <div class="col-md-2 pull-right" >
                                                            <asp:LinkButton runat="server" ID="lnkDownload" OnClick="lnkDownload_Click" CssClass="list-group-item btn-xs" OnPreRender="addTrigger_PreRender" Enabled='<%# Bind("IsDownload") %>' CommandArgument='<%# Bind("PEStandardRevFilesNo") %>' >
                                                                <i class="fa fa-download fa-fw"></i> Download<span class="pull-right text-muted"><em></em></span>
                                                            </asp:LinkButton> 
                                                            <asp:LinkButton runat="server" ID="lnkDeleteFiles" CssClass="list-group-item btn-xs"  OnClick="lnkDeleteFiles_Click" CommandArgument='<%# Bind("PEStandardRevFilesNo") %>' Visible='<%# Bind("IsEnabled") %>' >
                                                                <i class="fa fa-trash-o fa-fw"></i> Delete<span class="pull-right text-muted"><em></em></span>
                                                            </asp:LinkButton> 
                                                            <uc:ConfirmBox runat="server" ID="cfbDeleteFiles" TargetControlID="lnkDeleteFiles" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" Visible='<%# Bind("IsEnabled") %>'  /> 
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Select" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Enabled='<%# Bind("IsEnabled") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                </mcn:DataPagerGridView>
                        </div>
                    </div>
            </div>
            </asp:Panel>          

    </Content>
</uc:PEReviewTab>

</div>


<asp:Panel ID="pConfirmBox" runat="server" Style="display: none">
<div class="message-box animated fadeIn open">
    <div class="mb-container">
        <div class="mb-middle">
            <div class="mb-title">
                <span class="fa fa-question"></span>
                <div class="pull-left"> 
                    Confirm
                </div>
            </div>
            <div class="mb-content">    
                <div class="pull-left"> 
                    <asp:Label runat="server" ID="lblMessage" Text="Do you want to proceed?" />
                </div>
            </div>
            <div class="mb-footer">
                <div class="pull-right">                                                
                    <asp:Button runat="server" CssClass="btn btn-success btn-lg" ID="btnYes" Text="Yes" />
                    <asp:Button runat="server" CssClass="btn btn-default btn-lg" ID="btnNo" Text="Cancel" />                    
                </div>
            </div>                    
        </div>
    </div>
</div>
</asp:Panel>

<asp:Button ID="btnShowItem" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlItem" runat="server" TargetControlID="btnShowItem" PopupControlID="pnlPopupItem" CancelControlID="lnkCloseItem" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupItem" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsItem">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseItem" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveItem" OnClick="lnkSaveItem_Click"  CssClass="fa fa-floppy-o submit fsItem lnkSaveItem" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardRevNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeItem" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Dimension :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPEReviewDimNo" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Order No. :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOrderLevelItem"  runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtOrderLevelItem" />
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardCode" runat="server" CssClass="form-control" Placeholder="e.g. 1,2,3... or a,b,c..."></asp:Textbox>
                 </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardDesc" runat="server" CssClass="form-control" TextMode="Multiline" Rows="2"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Standard Question :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtStandard" runat="server" CssClass="form-control required" TextMode="Multiline" Rows="2"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsRequired" Text="&nbsp;Please check here to make item require." />
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkHasComment" Text="&nbsp;Please check here to add comment box." />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Response Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboResponseTypeNo" DataMember="EResponseType" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>





<asp:Button ID="btnShowChoices" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlChoices" runat="server" TargetControlID="btnShowChoices" PopupControlID="pnlPopupChoices" CancelControlID="lnkCloseChoices" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupChoices" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsChoices">
        <div class="cf popupheader">
            <h4><asp:Label runat="server" ID="lblChoices" /></h4>
            <asp:Linkbutton runat="server" ID="lnkCloseChoices" CssClass="fa fa-times" ToolTip="Close"/>
            &nbsp;<asp:Linkbutton runat="server" ID="lnkRefreshChoices" OnClick="lnkRefreshChoices_Click"  CssClass="fa fa-floppy-o" ToolTip="Save" />
        </div>
        <div  class="entryPopupDetl2 form-horizontal">
                <div class="panel panel-default">                   
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <mcn:DataPagerGridView ID="grdChoices" runat="server" OnPageIndexChanging="grdChoices_PageIndexChanging" DatakeyNames="PEStandardRevNo,PEStandardDetiRevNo,PEStandardObjRevNo">
                                    <Columns>
                                                        
                                        <asp:TemplateField ShowHeader="false" Visible="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditChoices" runat="server" CausesValidation="false" CssClass="cancel" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("PEStandardDetiRevNo") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDetlNo" runat="server"   Text='<%# Bind("PEStandardDetiRevNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>     
                                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrder" runat="server"   Text='<%# Bind("OrderLevel") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CodeDeti" HeaderText="Order No." >
                                            <HeaderStyle Width="10%"  HorizontalAlign  ="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>  
                                                                  
                                        <asp:TemplateField HeaderText="Rating" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" Visible="false" >
                                            <ItemTemplate >                       
                                                <asp:DropDownList CssClass="form-control" ID="cboPERatingNo"   Text='<%# Bind("PERatingNo") %>' AppendDataBoundItems="True"  runat="server" DataSourceID="ObjectDataSource2" DataTextField="tDesc" DataValueField="tNo">
                                                </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Lookup_PERating" TypeName="clsLookup"></asp:ObjectDataSource>                            
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="30%"/>
                                        </asp:TemplateField> 
                                        
                                        <asp:TemplateField HeaderText="Proficiency" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                            <ItemTemplate >                       
                                                <asp:Textbox CssClass="form-control" ID="txtProfeciency" Text='<%# Bind("Profeciency") %>' runat="server"></asp:Textbox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" TargetControlID="txtProfeciency" ValidChars="-." />                                   
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                        </asp:TemplateField>   

                                        <asp:TemplateField HeaderText="Indicator" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                            <ItemTemplate >                  
                                                <asp:Textbox CssClass="form-control" ID="txtAnchor" Text='<%# Bind("Anchor") %>' runat="server"></asp:Textbox>                                 
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="30%"/>
                                        </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Select"  >
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:Label ID="Label6" runat="server" Text="Select"/>
                                                    &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtIsSelectd" onclick ="chkselectdeti(this);"  runat="server" Visible="false" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtdIsSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                                                                                                                                               
                                                                                        
                                    </Columns>
                                </mcn:DataPagerGridView>
                            </div>
                        </div>
                    
                        <div class="row">
                            <div class="col-md-6" style="padding-top:5px;">
                                <!-- Paging here -->
                                <asp:DataPager ID="dpChoices" runat="server" PagedControlID="grdChoices" PageSize="10">
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
                            <div class="col-md-6">
                                <!-- Button here btn-group -->
                                <div class="pull-right">
                                    <asp:Button ID="lnkSaveChoices" Text="Submit" runat="server" CausesValidation="false" CssClass="btn btn-success" OnClick="lnkSaveChoices_Click" ToolTip="Click here to add" ></asp:Button>
                                    <asp:Button ID="lnkDeleteChoices" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="lnkDeleteChoices_Click" ToolTip="Click here to delete" ></asp:Button>                       
                                </div>
                                <%--<uc:ConfirmBox ID="ConfirmBox4" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDeleteChoices" />--%>
                            </div>
                        </div> 
                      
                    </div>
                                                 
                </div>
        </div>
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowSetting" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlSetting" runat="server" TargetControlID="btnShowSetting" PopupControlID="pnlPopupSetting" CancelControlID="lnkCloseSetting" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupSetting" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsSetting">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseSetting" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveSetting" OnClick="lnkSaveSetting_Click"  CssClass="fa fa-floppy-o submit fsSetting lnkSaveSetting" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Evaluator :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPEEvaluatorNo" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Performance Cycle :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPECycleNo" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            
            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


<asp:Button ID="btnShowCate" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlCate" runat="server" TargetControlID="btnShowCate" PopupControlID="pnlPopupCate" CancelControlID="lnkCloseCate" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupCate" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsCate">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseCate" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveCate"   CssClass="fa fa-floppy-o submit fsCate lnkSaveCate" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowDim" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDim" runat="server" TargetControlID="btnShowDim" PopupControlID="pnlPopupDim" CancelControlID="lnkCloseDim" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDim" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsDim">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseDim" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDim"  CssClass="fa fa-floppy-o submit fsDim lnkSaveDim" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            

            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowGoals" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlGoals" runat="server" TargetControlID="btnShowGoals" PopupControlID="pnlPopupGoals" CancelControlID="lnkCloseGoals" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupGoals" runat="server" CssClass="entryPopup3" style="display:none">
    <fieldset class="form" id="fsGoals">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseGoals" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveGoals" OnClick="lnkSaveGoals_Click" CssClass="fa fa-floppy-o submit fsGoals lnkSaveGoals" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl3 form-horizontal">
            
            <div class="form-group" style="display:none;">
                <label class="col-md-3 control-label has-space">Item No. :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtGPEStandardRevNo" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" Placeholder="Autonumber"  ></asp:TextBox> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Item No. :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtGOrderLevel" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" Placeholder="Autonumber"  ></asp:TextBox> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-required">Goal Name :</label>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtGPEStandardDesc" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Description :</label>
                <div class="col-md-8">
                    <dx:ASPxHtmlEditor ID="txtGStandard" runat="server"  Width="100%" Height="150px" SkinID="HtmlEditorPlane" />
                </div>
            </div>   
            
            <div class="form-group" >
                <label class="col-md-3 control-label has-space">Category :</label>
                <div class="col-md-8">
                    <asp:Dropdownlist ID="cboGPEReviewDimNo" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-3 control-label has-required">
                Start Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtGStartDate" runat="server" CssClass="required form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                        TargetControlID="txtGStartDate"
                        Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtGStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                    <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtGStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender1"
                        TargetControlID="RangeValidator4" />
                </div>
                <label class="col-md-3 control-label has-required">Target Completion Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtGEndDate" runat="server" CssClass="required form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtGEndDate"
                        Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtGEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                    <asp:RangeValidator
                        ID="RangeValidator1"
                        runat="server"
                        ControlToValidate="txtGEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator1" />
                </div>
            </div>
            
           <div class="form-group">
                <label class="col-md-3 control-label has-required">Weight (%) :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtGWeighted" runat="server" CssClass="required form-control" MaxLength="3" onkeyup="computerating();" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtGWeighted" /> 
                </div>
                <label class="col-md-3 control-label has-space">Actual Completion Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtGActualDate" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                        TargetControlID="txtGActualDate"
                        Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtGActualDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                    <asp:RangeValidator
                        ID="RangeValidator5"
                        runat="server"
                        ControlToValidate="txtGActualDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender5"
                        TargetControlID="RangeValidator5" />
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-3 control-label has-space">Priority :</label>
                <div class="col-md-8">
                    <asp:Dropdownlist ID="cboGPEPriorityNo" DataMember="EPEPriority" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-required">Completion Percentage :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtGProficiency" runat="server" CssClass="required form-control" MaxLength="3" onkeyup="computerating();" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtGProficiency" /> 

                    <%--<asp:DropDownList runat="server" ID="cboGRating" CssClass="required form-control">
                        <asp:ListItem Text="0" Value="0" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="75" Value="75" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>--%>
                </div>
                <label class="col-md-3 control-label has-space">Rating :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtGRating" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false" />
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-3 control-label has-required">Status :</label>
                <div class="col-md-8">
                    <asp:Dropdownlist ID="cboGPEStandardStatNo" DataMember="EPEStandardStat" runat="server" CssClass="required form-control" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Comments :</label>
                <div class="col-md-8">
                    <dx:ASPxHtmlEditor ID="txtGComments" runat="server"  Width="100%" Height="150px" SkinID="HtmlEditorPlane" />
                </div>
            </div>


            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>





<asp:Button ID="btnShowTask" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlTask" runat="server" TargetControlID="btnShowTask" PopupControlID="pnlPopupTask" CancelControlID="lnkCloseTask" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupTask" runat="server" CssClass="entryPopup3" style="display:none">
    <fieldset class="form" id="fsTask">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseTask" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveTask" OnClick="lnkSaveTask_Click" CssClass="fa fa-floppy-o submit fsTask lnkSaveTask" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl3 form-horizontal">
            
            <div class="form-group" style="display:none;">
                <label class="col-md-3 control-label has-space">Item No. :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtPEStandardRevTaskNo" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" Placeholder="Autonumber"  ></asp:TextBox> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Item No. :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtTOrderLevel" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" Placeholder="Autonumber"  ></asp:TextBox> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-required">Task Name :</label>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtPEStandardRevTaskDesc" CssClass="form-control required"  TextMode="MultiLine" Rows="2" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Description :</label>
                <div class="col-md-8">
                    <dx:ASPxHtmlEditor ID="txtTRemarks" runat="server"  Width="100%" Height="150px" SkinID="HtmlEditorPlane" CssClass="required" />
                </div>
            </div>   

            <div class="form-group">
                <label class="col-md-3 control-label has-required">Start Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTStartDate" runat="server" CssClass="form-control required"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="txtTStartDate"
                        Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtTStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                    <asp:RangeValidator
                        ID="RangeValidator2"
                        runat="server"
                        ControlToValidate="txtTStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender3"
                        TargetControlID="RangeValidator2" />
                </div>
                <label class="col-md-3 control-label has-required">Target Completion Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTEndDate" runat="server" CssClass="form-control required"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtTEndDate"
                        Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtTEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                    <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtTEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender4"
                        TargetControlID="RangeValidator3" />
                </div>
            </div>

            <div class="form-group" style="display:none">
                <label class="col-md-3 control-label has-space">Weight (%) :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTWeighted" runat="server" CssClass="form-control"  ></asp:TextBox> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtTWeighted" /> 
                </div>
                <label class="col-md-3 control-label has-space">Actual Completion Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTActualDate" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                        TargetControlID="txtTActualDate"
                        Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                        TargetControlID="txtTActualDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                    <asp:RangeValidator
                        ID="RangeValidator6"
                        runat="server"
                        ControlToValidate="txtTActualDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender6"
                        TargetControlID="RangeValidator6" />
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-3 control-label has-space">Priority :</label>
                <div class="col-md-8">
                    <asp:Dropdownlist ID="cboTPEPriorityNo" DataMember="EPEPriority" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-required">Completion Percentage :</label>
                <div class="col-md-8">
                    <asp:DropDownList runat="server" ID="cboTProficiency" CssClass="required form-control">
                        <asp:ListItem Text="0" Value="0" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="75" Value="75" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                </div>
            </div>
            
            <div class="form-group" >
                <label class="col-md-3 control-label has-required">Status :</label>
                <div class="col-md-8">
                    <asp:Dropdownlist ID="cboTPEStandardStatNo" DataMember="EPEStandardStat" runat="server" CssClass="required form-control" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Comments :</label>
                <div class="col-md-8">
                    <dx:ASPxHtmlEditor ID="txtTComments" runat="server"  Width="100%" Height="150px" SkinID="HtmlEditorPlane" />
                </div>
            </div>

            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowFiles" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlFiles" runat="server" TargetControlID="btnShowFiles" PopupControlID="pnlPopupFiles" CancelControlID="lnkCloseFiles" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupFiles" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsFiles">
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <asp:Linkbutton runat="server" ID="lnkCloseFiles" CssClass="fa fa-times" ToolTip="Close" />&nbsp;
                        <asp:Linkbutton runat="server" ID="lnkSaveFiles" OnClick="lnkSaveFiles_Click" CssClass="fa fa-floppy-o submit fsMain lnkSaveFiles" ToolTip="Save" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkSaveFiles" />
                    </Triggers>
                </asp:UpdatePanel>   
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPEStandardRevFilesNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"/>
                    </div>
                </div>                        

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPEStandardRevFilesDesc" runat="server" CssClass="required form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                </div>  

                <div class="form-group" style="visibility:hidden;position:absolute;">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                </div> 

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Upload File :</label>
                    <div class="col-md-7">
                        <asp:FileUpload runat="server" ID="fuDoc" Width="100%" CssClass="required" />
                    </div>
                </div>  


            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>