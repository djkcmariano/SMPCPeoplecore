<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="TrnCalendarList.aspx.vb" Inherits="Secured_TrnCalendarList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
    <script type="text/javascript">
   

    $(window).resize(function () {
        adjustPanelEntry();

    });

    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (args, sender) {
            adjustPanelEntry();
        });

    });

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
</script>

<style type="text/css">
    td.column_style_left
    {
        border-left: 1px solid #9E9DAA;
    }    
    td.column_style_right
    {
        border-right: 1px solid #9E9DAA;
    }  
    .dg
    {
        background-color:Darkgray;
        border:#ffffff solid 1px;
        }
    .lg
    {
        background-color:#eaeaea;
        border:Lightgray solid 1px;
    }
    .wh
    {
        background-color:White;
        border:Lightgray solid 1px;
    }
    .cg
    {
        background-color:#e3e3ff;
        border:Lightgray solid 1px;
    }
    
  
</style>

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-5">
                        <h4><asp:Label ID="lblMonth" runat="server" CssClass="control-label"></asp:Label></h4>
                    </div>
                    <div>
                        <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                            <Content>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Title :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cboTrnTitleNo" CssClass="form-control" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Month :</label> 
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cboMonthNo" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Year :</label> 
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtYear" CssClass="form-control">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </Content>
                        </uc:Filter>
                    </div>                           
                </div>
                <div class="panel-body">
                    <div  class="form-horizontal">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-9">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grdCal" SkinID="grdCalendar" runat="server" DataKeyNames="EmployeeNo,Date1,date2,date3,date4,date5,date6,date7" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id"  Visible="False"  >
                
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server"   Text='<%# Bind("EmployeeNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField >
                                                    <HeaderTemplate>
 
                                                        <asp:LinkButton runat="server" ID="lnkPrevious" OnClick="lnkPrevious_Click" Text="<<" ></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                        Sunday
                                                    </HeaderTemplate>
                
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:50%; text-align:left;">
                                                                    <asp:Label ID="lblMon" runat="server"  Text='<%# Bind("Day1") %>'></asp:Label>
                                                                </td>
                                                                <td style="width:50%; text-align:right; padding-right:5px;">
                                                                    <asp:CheckBox runat="server" ID="chkMon" Visible="false" onclick="showChk(this);" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;padding:0px 10px 10px 10px">
                                                                    &nbsp;<asp:LinkButton runat="server" ID="lblshift1" tooltip='<%# Bind("Date1") %>' Text='<%# Bind("TrnTitleDesc1") %>'></asp:LinkButton>
                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;">
                                
                               
                                                                </td>
                                                            </tr>
                        
                                                        </table>
                    

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Height="80px" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="14.3%"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Monday" >
                
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:50%; text-align:left;">
                                                                    <asp:Label ID="lblTue" runat="server"   Text='<%# Bind("Day2") %>'></asp:Label>
                                                                </td>
                                                                <td style="width:50%; text-align:right; padding-right:5px;">
                                                                    <asp:CheckBox runat="server" ID="chkTue" Visible="false" onclick="showChk(this);" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;padding:0px 10px 10px 10px">
                                                                    &nbsp;<asp:LinkButton runat="server" ID="lblshift2" tooltip='<%# Bind("Date2") %>' Text='<%# Bind("TrnTitleDesc2") %>'></asp:LinkButton>
                               
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;">
                               
                                                                </td>
                                                            </tr>
                       
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Height="80px" VerticalAlign="Top"  />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="14.3%"  />
                                                </asp:TemplateField>                                                             
                                                <asp:TemplateField HeaderText="Tuesday" >
                
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:50%; text-align:left;">
                                                                    <asp:Label ID="lblWed" runat="server"   Text='<%# Bind("Day3") %>'></asp:Label>
                                                                </td>
                                                                <td style="width:50%; text-align:right; padding-right:5px;">
                                                                    <asp:CheckBox runat="server" ID="chkWed" Visible="false" onclick="showChk(this);" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;padding:0px 10px 10px 10px">
                                                                    &nbsp;<asp:LinkButton runat="server" ID="lblshift3" tooltip='<%# Bind("Date3") %>' Text='<%# Bind("TrnTitleDesc3") %>'></asp:LinkButton>
                               
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;">
                               
                                                                </td>
                                                            </tr>
                       
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Height="80px" VerticalAlign="Top"  />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="14.3%" />
                                                </asp:TemplateField>  
                                                <asp:TemplateField HeaderText="Wednesday" >
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:50%; text-align:left;">
                                                                    <asp:Label ID="lblThu" runat="server"   Text='<%# Bind("Day4") %>'></asp:Label>
                                                                </td>
                                                                <td style="width:50%; text-align:right; padding-right:5px;">
                                                                    <asp:CheckBox runat="server" ID="chkThu" Visible="false" onclick="showChk(this);" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;padding:0px 10px 10px 10px">
                                                                    &nbsp;<asp:LinkButton runat="server" ID="lblshift4" tooltip='<%# Bind("Date4") %>' Text='<%# Bind("TrnTitleDesc4") %>'></asp:LinkButton>
                               
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;">
                                
                                                                </td>
                                                            </tr>
                        
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"  Height="80px" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="14.3%"/>
                                                </asp:TemplateField>   
                                                <asp:TemplateField HeaderText="Thursday" >
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:50%; text-align:left;">
                                                                    <asp:Label ID="lblFri" runat="server"   Text='<%# Bind("Day5") %>'></asp:Label>
                                                                </td>
                                                                <td style="width:50%; text-align:right; padding-right:5px;">
                                                                    <asp:CheckBox runat="server" ID="chkFri" Visible="false"  onclick="showChk(this);"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;padding:0px 10px 10px 10px">
                                                                    &nbsp;<asp:LinkButton runat="server" ID="lblshift5" tooltip='<%# Bind("Date5") %>' Text='<%# Bind("TrnTitleDesc5") %>'></asp:LinkButton>
                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;">
                               
                                                                </td>
                                                            </tr>
                       
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Height="80px" VerticalAlign="Top"  />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="14.3%" />
                                                </asp:TemplateField>  
            
                                                <asp:TemplateField HeaderText="Friday" >
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:50%; text-align:left;">
                                                                    <asp:Label ID="lblSat" runat="server"   Text='<%# Bind("Day6") %>'></asp:Label>
                                                                </td>
                                                                <td style="width:50%; text-align:right; padding-right:5px;">
                                                                    <asp:CheckBox runat="server" ID="chkSat" Visible="false" onclick="showChk(this);" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;padding:0px 10px 10px 10px">
                                                                    &nbsp;<asp:LinkButton runat="server" ID="lblshift6" tooltip='<%# Bind("Date6") %>' Text='<%# Bind("TrnTitleDesc6") %>'></asp:LinkButton>
                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;">
                                
                                                                </td>
                                                            </tr>
                        
                                                        </table>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Height="80px" VerticalAlign="Top"  />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="14.3%" />
                                                </asp:TemplateField>  
                                                    <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Saturday
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton runat="server" ID="lnkNext" OnClick="lnkNext_Click" Text=">>" ></asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:50%; text-align:left;">
                                                                    <asp:Label ID="lblSun" runat="server"   Text='<%# Bind("Day7") %>'></asp:Label>
                                                                </td>
                                                                <td style="width:50%; text-align:right; padding-right:5px;">
                                                                    <asp:CheckBox runat="server" ID="chkSun" Visible="false" onclick="showChk(this);" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;padding:0px 10px 10px 10px">
                                                                    &nbsp;<asp:LinkButton runat="server" ID="lblshift7" tooltip='<%# Bind("Date7") %>' Text='<%# Bind("TrnTitleDesc7") %>'></asp:LinkButton>
                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width:100%; text-align:left;">
                                
                                                                </td>
                                                            </tr>
                       
                                                        </table>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Height="80px" VerticalAlign="Top"  />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="14.3%" />
                                                </asp:TemplateField>  
                                                            
                                            </Columns>
                                            <PagerSettings Mode="NextPreviousFirstLast" />
                                        </mcn:DataPagerGridView>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label">Legend :</label>
                                    <p style="display:block;vertical-align:Top">
                                        <div style="border:#444444 solid 7px ;width:5px;position:absolute;display:block;"></div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:RadioButton ID="rdoTrnTitleNo0" GroupName="TrnTitle" runat="server" />&nbsp;<span>ALL</span>
                                    </p>
                                    <p style="display:block;vertical-align:Top">
                                        <div style="border:#9a9cff solid 7px ;width:5px;position:absolute;display:block;"></div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:RadioButton ID="rdoTrnTitleNo1" GroupName="TrnTitle" runat="server" />&nbsp;<span>Targeted Participants</span>
                                    </p>
                                    <p style="display:block;">
                                        <div style="border:#5484ed solid 7px ;width:5px;position:absolute;display:block;"></div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:RadioButton ID="rdoTrnTitleNo2" GroupName="TrnTitle" runat="server" />&nbsp;<span>Open for Public</span>
                                    </p>
                                    <p style="display:block;">
                                        <div style="border:#46d6db solid 7px ;width:5px;position:absolute;display:block;"></div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:RadioButton ID="rdoTrnTitleNo3" GroupName="TrnTitle" runat="server" />&nbsp;<span>By Invitation</span>
                                    </p>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>
 
</asp:content>
