<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRShiftSched.aspx.vb" Inherits="Secured_DTRShiftSched" %>




<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
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

 
    function GetSelectedAll(lnk) {


        var oItem = lnk.children;
        var theBox = (lnk.type == "checkbox") ? lnk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("chkIsSelect") > 0 &&
                    elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
    }

    function GetSelectedRow(lnk) {

        var ishide;
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex + 1;
        var grid = document.getElementById("<%= grdMain.ClientID %>");
        var count = grid.rows.length

        for (i = 2; i <= 9; i++) {
            if (i == rowIndex) {
                if (document.getElementById('ctl00_cphBody_grdMain_ctl0' + i + '_chkIsSelect') == ishide) {
                }
                else {
                    if (document.getElementById('ctl00_cphBody_grdMain_ctl0' + i + '_chkIsSelect').checked == true) {
                        for (j = 1; j <= 35; j++) {
                            if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + j) == ishide) {
                            }
                            else {
                                if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + j).checked == true) {
                                    //alert("Value of I:" + i + " Value of J:" + j);
                                    document.getElementById('ctl00_cphBody_grdMain_ctl0' + i + '_txtIsEdit' + j).checked = 1;
                                }
                            }
                        }
                    }
                    else {
                        for (j = 1; j <= 35; j++) {
                            if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + j) == ishide) {
                            }
                            else {
                                if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + j).checked == true) {
                                    //alert("Value of I:" + i + " Value of J:" + j);
                                    document.getElementById('ctl00_cphBody_grdMain_ctl0' + i + '_txtIsEdit' + j).checked = 0;
                                }
                            }
                        }
                    }
                }
            }
        }


        for (i = 10; i <= count; i++) {
            if (i == rowIndex) {
                if (document.getElementById('ctl00_cphBody_grdMain_ctl' + i + '_chkIsSelect') == ishide) {
                }
                else {
                    if (document.getElementById('ctl00_cphBody_grdMain_ctl' + i + '_chkIsSelect').checked == true) {
                        for (j = 1; j <= 35; j++) {
                            if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + j) == ishide) {
                            }
                            else {
                                if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + j).checked == true) {
                                    //alert("Value of I:" + i + " Value of J:" + j);
                                    document.getElementById('ctl00_cphBody_grdMain_ctl' + i + '_txtIsEdit' + j).checked = 1;
                                }
                            }
                        }
                    }
                    else {
                        for (j = 1; j <= 35; j++) {
                            if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + j) == ishide) {
                            }
                            else {
                                if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + j).checked == true) {
                                    //alert("Value of I:" + i + " Value of J:" + j);
                                    document.getElementById('ctl00_cphBody_grdMain_ctl' + i + '_txtIsEdit' + j).checked = 0;
                                }
                            }
                        }
                    }
                }
            }
        }


        return false;

    }

    function GetSelectedDate(lnk) {

        var ishide;
        var oItem = lnk.children;
        var theBox = (lnk.type == "checkbox") ? lnk : lnk.children.item[0];
        var columnIndex = theBox.id
        var grid = document.getElementById("<%= grdMain.ClientID %>");
        var count = grid.rows.length

        for (i = 1; i <= 35; i++) {
            if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + i) == ishide) {
            }
            else {
                if (('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + i) == columnIndex) {
                    if (document.getElementById('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + i).checked == true) {
                        for (j = 2; j <= 9; j++) {
                            if (document.getElementById('ctl00_cphBody_grdMain_ctl0' + j + '_chkIsSelect') == ishide) {
                            }
                            else {
                                if (document.getElementById('ctl00_cphBody_grdMain_ctl0' + j + '_chkIsSelect').checked == true) {
                                    document.getElementById('ctl00_cphBody_grdMain_ctl0' + j + '_txtIsEdit' + i).checked = 1;
                                }
                            }
                        }

                        for (j = 10; j <= count; j++) {
                            if (document.getElementById('ctl00_cphBody_grdMain_ctl' + j + '_chkIsSelect') == ishide) {
                            }
                            else {
                                if (document.getElementById('ctl00_cphBody_grdMain_ctl' + j + '_chkIsSelect').checked == true) {
                                    document.getElementById('ctl00_cphBody_grdMain_ctl' + j + '_txtIsEdit' + i).checked = 1;
                                }
                            }
                        }
                    }
                    else {
                        for (j = 2; j <= 9; j++) {
                            if (document.getElementById('ctl00_cphBody_grdMain_ctl0' + j + '_chkIsSelect') == ishide) {
                            }
                            else {
                                if (document.getElementById('ctl00_cphBody_grdMain_ctl0' + j + '_chkIsSelect').checked == true) {
                                    document.getElementById('ctl00_cphBody_grdMain_ctl0' + j + '_txtIsEdit' + i).checked = 0;
                                }
                            }
                        }

                        for (j = 10; j <= count; j++) {
                            if (document.getElementById('ctl00_cphBody_grdMain_ctl' + j + '_chkIsSelect') == ishide) {
                            }
                            else {
                                if (document.getElementById('ctl00_cphBody_grdMain_ctl' + j + '_chkIsSelect').checked == true) {
                                    document.getElementById('ctl00_cphBody_grdMain_ctl' + j + '_txtIsEdit' + i).checked = 0;
                                }
                            }
                        }
                    }
                }
            }
        }


        return false;

    }


    function GetSelectedAllDate(lnk) {


        var oItem = lnk.children;
        var theBox = (lnk.type == "checkbox") ? lnk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;
        //                var str = elm[18].id;
        //                elm[13].click(); elm[14].click(); elm[15].click(); elm[16].click(); elm[17].click(); elm[18].click(); elm[19].click(); elm[20].click(); elm[21].click();
        //                elm[22].click(); elm[23].click(); elm[24].click(); elm[25].click(); elm[26].click(); elm[27].click(); elm[28].click(); elm[29].click(); elm[30].click(); elm[31].click();
        //                elm[32].click(); elm[33].click(); elm[34].click(); elm[35].click(); elm[36].click(); elm[37].click(); elm[38].click(); elm[39].click(); elm[40].click(); elm[41].click();
        //                elm[42].click(); elm[43].click(); elm[44].click(); elm[45].click(); elm[46].click();
        for (jj = 1; jj < 36; jj++) {
            for (ii = 0; ii < elm.length; ii++) {
                if (('ctl00_cphBody_grdMain_ctl01_txtIsSelect' + jj) == elm[ii].id) {
                    if (elm[ii].checked != xState)
                        elm[ii].click();
                }
            }
        }
        return false;
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
</style>


<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                 <div class="col-md-6">
                     <h4 class="panel-title">
                         Filter
                     </h4>
                 </div>
                 <div >
                    <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                        <Content>
	                        <div class="form-group">
                                <label class="col-md-4 control-label">Department :</label>
                                <div class="col-md-8">
                                    <asp:DropdownList runat="server" CssClass="form-control" ID="cboDepartmentNo" >
                                    </asp:DropdownList> 
                                </div>
                            </div>
	                        <div class="form-group">
                                <label class="col-md-4 control-label">Section :</label>
                                <div class="col-md-8">
                                    <asp:DropdownList runat="server" CssClass="form-control" ID="cboSectionNo" >
                                    </asp:DropdownList> 
                                </div>
                            </div>
	                        <div class="form-group">
                                <label class="col-md-4 control-label">Unit :</label>
                                <div class="col-md-8">
                                    <asp:DropdownList runat="server" CssClass="form-control" ID="cboUnitNo" >
                                    </asp:DropdownList>
                                </div>
                            </div>
	                        <div class="form-group">
                                <label class="col-md-4 control-label">Position :</label>
                                <div class="col-md-8">
                                    <asp:DropdownList runat="server" CssClass="form-control" ID="cboPositionNo">
                                    </asp:DropdownList> 
                                </div>
                             </div>
                            </Content>
                    </uc:Filter>
                </div>
            </div>
            <div>
                <div class="panel-body">
                    <div class="col-md-2">
                        <label class="col-md-4 control-label">Year</label>
                        <div class="col-md-8">
                            <asp:DropdownList runat="server" CssClass="form-control" ID="cboApplicableYear" AutoPostBack="true" OnTextChanged="txtFilter_Changed" >
                            </asp:DropdownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label class="col-md-4 control-label right">Month</label>
                        <div class="col-md-8">
                            <asp:DropdownList runat="server" CssClass="form-control" ID="cboApplicableMonth" AutoPostBack="true" OnTextChanged="txtFilter_Changed" >
                            </asp:DropdownList> 
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="col-md-8">
                            <asp:CheckBox ID="txtWeek1" runat="server" Text="&nbsp Week 1 &nbsp;" AutoPostBack="true" OnCheckedChanged="txtFilter_Changed"  />
                            <asp:CheckBox ID="txtWeek2" runat="server" Text="&nbsp Week 2 &nbsp;" AutoPostBack="true" OnCheckedChanged="txtFilter_Changed"  />
                            <asp:CheckBox ID="txtWeek3" runat="server" Text="&nbsp Week 3 &nbsp;" AutoPostBack="true" OnCheckedChanged="txtFilter_Changed"  />
                            <asp:CheckBox ID="txtWeek4" runat="server" Text="&nbsp Week 4 &nbsp;" AutoPostBack="true" OnCheckedChanged="txtFilter_Changed"  />
                            <asp:CheckBox ID="txtWeek5" runat="server" Text="&nbsp Week 5 &nbsp;" AutoPostBack="true" OnCheckedChanged="txtFilter_Changed"  />
                        </div>
                        <label class="col-md-2 control-label">Page Size</label>
                        <div class="col-md-2">
                            <asp:DropdownList runat="server" CssClass="form-control" ID="cboPageSize" AutoPostBack="true" OnTextChanged="txtFilter_Changed" >
                            </asp:DropdownList>
                        </div>
                    </div>

                    
                </div>
            </div>
        </div>
    </div>
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <div class="col-md-6">
                            <asp:Label runat="server" ID="lblRemark" CssClass="panel-title"></asp:Label>
                        </div>
                        <div class="pull-right">          
                            <ul class="panel-controls">                                                        
                                <li><asp:LinkButton runat="server" ID="lnkUpdateShift" OnClick="lnkUpdateShift_Click" Text="Update Shift" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkUpdateRD" OnClick="lnkUpdateRD_Click" Text="Update RD" CssClass="control-primary" /></li>
                            </ul>
                        </div>
                </div>
                <div class="panel-body">

                    <div class="table-responsive">
                        <!-- Gridview here -->
                        <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="EmployeeNo,fullName">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeNo" runat="server"   Text='<%# Bind("EmployeeNo") %>'></asp:Label>
                                            <asp:Label ID="lblSectionNo" runat="server"   Text='<%# Bind("SectionNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                       <HeaderTemplate>
                                            <center>
                                            <asp:Label ID="lblhdSelect" runat="server" Text="Select Emp." style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="chkIsSelectAll" onclick ="GetSelectedAll(this);" runat="server" />
                                            </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsSelect" onclick ="GetSelectedRow(this);" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="column_style_left"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                            <center>
                                            <asp:Label ID="lblhdName" runat="server" Text="Select Date" style="color:#7092be; font-size: 12px" />
                                            <br />
                                            <asp:CheckBox ID="txtIsSelectAll" onclick ="GetSelectedAllDate(this);" runat="server" />                                                                    
                                            </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFullName" runat="server"   Text='<%# Bind("FullName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate1" runat="server"   Text='<%# Bind("Date1") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >  
                                            <asp:Label ID="lblShift1" runat="server"   Text='<%# Bind("Shift1") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                        <center>
                                            <asp:Label ID="lblhd1" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode1" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect1" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"   />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit1" runat="server" Enabled='<%# Bind("IsEnabled1") %>' /><br />
                                            <asp:LinkButton  ID="lnk1" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode1") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift1") %>' ToolTip='<%# Bind("date1") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk1" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode1") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date1") %>'  CommandArgument='<%# Bind("DayOffStr1") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"  VerticalAlign="Top" BackColor="#F7F6F7" CssClass="column_style_left"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate2" runat="server"   Text='<%# Bind("Date2") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift2" runat="server"   Text='<%# Bind("Shift2") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd2" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode2" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect2" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"  />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit2" runat="server" Enabled='<%# Bind("IsEnabled2") %>'  /><br />
                                            <asp:LinkButton  ID="lnk2" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode2") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift2") %>' ToolTip='<%# Bind("date2") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk2" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode2") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date2") %>'  CommandArgument='<%# Bind("DayOffStr2") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate3" runat="server"   Text='<%# Bind("Date3") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift3" runat="server"   Text='<%# Bind("Shift3") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField runat="server">
                                       <HeaderTemplate runat="server">
                                       <center>
                                            <asp:Label ID="lblhd3" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode3" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect3" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"  />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit3" runat="server" Enabled='<%# Bind("IsEnabled3") %>'  /><br />
                                            <asp:LinkButton  ID="lnk3" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode3") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift3") %>' ToolTip='<%# Bind("date3") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk3" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode3") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date3") %>'  CommandArgument='<%# Bind("DayOffStr3") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate4" runat="server"   Text='<%# Bind("Date4") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift4" runat="server"   Text='<%# Bind("Shift4") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                            <center>
                                                <asp:Label ID="lblhd4" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                                <asp:Label ID="lblDayCode4" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                                <asp:CheckBox ID="txtIsSelect4" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"   />
                                            </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit4" runat="server" Enabled='<%# Bind("IsEnabled4") %>'  /><br />
                                            <asp:LinkButton  ID="lnk4" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode4") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift4") %>' ToolTip='<%# Bind("date4") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk4" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode4") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date4") %>'  CommandArgument='<%# Bind("DayOffStr4") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate5" runat="server"   Text='<%# Bind("Date5") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift5" runat="server"   Text='<%# Bind("Shift5") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                            <center>
                                                <asp:Label ID="lblhd5" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                                <asp:Label ID="lblDayCode5" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                                <asp:CheckBox ID="txtIsSelect5" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"   />
                                            </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit5" runat="server" Enabled='<%# Bind("IsEnabled5") %>'  /><br />
                                            <asp:LinkButton  ID="lnk5" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode5") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift5") %>' ToolTip='<%# Bind("date5") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk5" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode5") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date5") %>'  CommandArgument='<%# Bind("DayOffStr5") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate6" runat="server"   Text='<%# Bind("Date6") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift6" runat="server"   Text='<%# Bind("Shift6") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd6" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode6" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect6" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"  />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit6" runat="server" Enabled='<%# Bind("IsEnabled6") %>'  /><br />
                                            <asp:LinkButton  ID="lnk6" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode6") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift6") %>' ToolTip='<%# Bind("date6") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk6" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode6") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date6") %>'  CommandArgument='<%# Bind("DayOffStr6") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate7" runat="server"   Text='<%# Bind("Date7") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift7" runat="server"   Text='<%# Bind("Shift7") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd7" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode7" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect7" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"  />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit7" runat="server" Enabled='<%# Bind("IsEnabled7") %>'  /><br />
                                            <asp:LinkButton  ID="lnk7" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode7") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift7") %>' ToolTip='<%# Bind("date7") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk7" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode7") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date7") %>'  CommandArgument='<%# Bind("DayOffStr7") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate8" runat="server"   Text='<%# Bind("Date8") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift8" runat="server"   Text='<%# Bind("Shift8") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd8" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode8" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect8" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"  />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit8" runat="server" Enabled='<%# Bind("IsEnabled8") %>'  /><br />
                                            <asp:LinkButton  ID="lnk8" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode8") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift8") %>' ToolTip='<%# Bind("date8") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk8" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode8") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date8") %>'  CommandArgument='<%# Bind("DayOffStr8") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="column_style_left"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate9" runat="server"   Text='<%# Bind("Date9") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift9" runat="server"   Text='<%# Bind("Shift9") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                        
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd9" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode9" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect9" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"  />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit9" runat="server" Enabled='<%# Bind("IsEnabled9") %>'  /><br />
                                            <asp:LinkButton  ID="lnk9" CssClass="lnktextsmall-Color" style="font-size:11px;"  runat="server"  Text='<%# Bind("ShiftCode9") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift9") %>' ToolTip='<%# Bind("date9") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk9" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode9") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date9") %>'  CommandArgument='<%# Bind("DayOffStr9") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate10" runat="server"   Text='<%# Bind("Date10") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift10" runat="server"   Text='<%# Bind("Shift10") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd10" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode10" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect10" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit10" runat="server" Enabled='<%# Bind("IsEnabled10") %>'  /><br />
                                            <asp:LinkButton  ID="lnk10" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode10") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift10") %>' ToolTip='<%# Bind("date10") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk10" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode10") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date10") %>'  CommandArgument='<%# Bind("DayOffStr10") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate11" runat="server"   Text='<%# Bind("Date11") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift11" runat="server"   Text='<%# Bind("Shift11") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd11" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode11" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect11" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server"   />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit11" runat="server" Enabled='<%# Bind("IsEnabled11") %>'  /><br />
                                            <asp:LinkButton  ID="lnk11" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode11") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift11") %>' ToolTip='<%# Bind("date11") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk11" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode11") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date11") %>'  CommandArgument='<%# Bind("DayOffStr11") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate12" runat="server"   Text='<%# Bind("Date12") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift12" runat="server"   Text='<%# Bind("Shift12") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd12" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode12" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect12" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit12" runat="server" Enabled='<%# Bind("IsEnabled12") %>'  /><br />
                                            <asp:LinkButton  ID="lnk12" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode12") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift12") %>' ToolTip='<%# Bind("date12") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk12" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode12") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date12") %>'  CommandArgument='<%# Bind("DayOffStr12") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate13" runat="server"   Text='<%# Bind("Date13") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift13" runat="server"   Text='<%# Bind("Shift13") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd13" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode13" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect13" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit13" runat="server" Enabled='<%# Bind("IsEnabled13") %>'  /><br />
                                            <asp:LinkButton  ID="lnk13" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode13") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift13") %>' ToolTip='<%# Bind("date13") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk13" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode13") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date13") %>'  CommandArgument='<%# Bind("DayOffStr13") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate14" runat="server"   Text='<%# Bind("Date14") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift14" runat="server"   Text='<%# Bind("Shift14") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd14" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode14" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect14" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit14" runat="server" Enabled='<%# Bind("IsEnabled14") %>'  /><br />
                                            <asp:LinkButton  ID="lnk14" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode14") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift14") %>' ToolTip='<%# Bind("date14") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk14" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode14") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date14") %>'  CommandArgument='<%# Bind("DayOffStr14") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate15" runat="server"   Text='<%# Bind("Date15") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift15" runat="server"   Text='<%# Bind("Shift15") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd15" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode15" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect15" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit15" runat="server" Enabled='<%# Bind("IsEnabled15") %>'  /><br />
                                            <asp:LinkButton  ID="lnk15" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode15") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift15") %>' ToolTip='<%# Bind("date15") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk15" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode15") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date15") %>'  CommandArgument='<%# Bind("DayOffStr15") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" CssClass="column_style_left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate16" runat="server"   Text='<%# Bind("Date16") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift16" runat="server"   Text='<%# Bind("Shift16") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd16" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode16" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect16" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit16" runat="server" Enabled='<%# Bind("IsEnabled16") %>'  /><br />
                                            <asp:LinkButton  ID="lnk16" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode16") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift16") %>' ToolTip='<%# Bind("date16") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk16" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode16") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date16") %>'  CommandArgument='<%# Bind("DayOffStr16") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate17" runat="server"   Text='<%# Bind("Date17") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift17" runat="server"   Text='<%# Bind("Shift17") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd17" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode17" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect17" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit17" runat="server" Enabled='<%# Bind("IsEnabled17") %>'  /><br />
                                            <asp:LinkButton  ID="lnk17" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode17") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift17") %>' ToolTip='<%# Bind("date17") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk17" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode17") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date17") %>'  CommandArgument='<%# Bind("DayOffStr17") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                        <asp:Label ID="lblDate18" runat="server"   Text='<%# Bind("Date18") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift18" runat="server"   Text='<%# Bind("Shift18") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd18" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode18" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect18" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit18" runat="server" Enabled='<%# Bind("IsEnabled18") %>'  /><br />
                                            <asp:LinkButton  ID="lnk18" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode18") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift18") %>' ToolTip='<%# Bind("date18") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk18" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode18") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date18") %>'  CommandArgument='<%# Bind("DayOffStr18") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate19" runat="server"   Text='<%# Bind("Date19") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift19" runat="server"   Text='<%# Bind("Shift19") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd19" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode19" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect19" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit19" runat="server" Enabled='<%# Bind("IsEnabled19") %>'  /><br />
                                            <asp:LinkButton  ID="lnk19" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode19") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift19") %>' ToolTip='<%# Bind("date19") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk19" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode19") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date19") %>'  CommandArgument='<%# Bind("DayOffStr19") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                        <asp:Label ID="lblDate20" runat="server"   Text='<%# Bind("Date20") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift20" runat="server"   Text='<%# Bind("Shift20") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd20" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode20" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect20" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit20" runat="server" Enabled='<%# Bind("IsEnabled20") %>'  /><br />
                                            <asp:LinkButton  ID="lnk20" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode20") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift20") %>' ToolTip='<%# Bind("date20") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk20" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode20") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date20") %>'  CommandArgument='<%# Bind("DayOffStr20") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate21" runat="server"   Text='<%# Bind("Date21") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift21" runat="server"   Text='<%# Bind("Shift21") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd21" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode21" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect21" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit21" runat="server"  Enabled='<%# Bind("IsEnabled21") %>' /><br />
                                            <asp:LinkButton  ID="lnk21" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode21") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift21") %>' ToolTip='<%# Bind("date21") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk21" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode21") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date21") %>'  CommandArgument='<%# Bind("DayOffStr21") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate22" runat="server"   Text='<%# Bind("Date22") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift22" runat="server"   Text='<%# Bind("Shift22") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd22" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode22" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect22" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit22" runat="server" Enabled='<%# Bind("IsEnabled22") %>'  /><br />
                                            <asp:LinkButton  ID="lnk22" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode22") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift22") %>' ToolTip='<%# Bind("date22") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk22" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode22") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date22") %>'  CommandArgument='<%# Bind("DayOffStr22") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="column_style_left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate23" runat="server"   Text='<%# Bind("Date23") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift23" runat="server"   Text='<%# Bind("Shift23") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd23" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode23" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect23" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit23" runat="server" Enabled='<%# Bind("IsEnabled23") %>'  /><br />
                                            <asp:LinkButton  ID="lnk23" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode23") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift23") %>' ToolTip='<%# Bind("date23") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk23" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode23") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date23") %>'  CommandArgument='<%# Bind("DayOffStr23") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate24" runat="server"   Text='<%# Bind("Date24") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift24" runat="server"   Text='<%# Bind("Shift24") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd24" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode24" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect24" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit24" runat="server" Enabled='<%# Bind("IsEnabled24") %>'  /><br />
                                            <asp:LinkButton  ID="lnk24" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode24") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift24") %>' ToolTip='<%# Bind("date24") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk24" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode24") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date24") %>'  CommandArgument='<%# Bind("DayOffStr24") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate25" runat="server"   Text='<%# Bind("Date25") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift25" runat="server"   Text='<%# Bind("Shift25") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                        
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd25" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode25" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect25" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit25" runat="server" Enabled='<%# Bind("IsEnabled25") %>'  /><br />
                                            <asp:LinkButton  ID="lnk25" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode25") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift25") %>' ToolTip='<%# Bind("date25") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk25" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode25") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date25") %>'  CommandArgument='<%# Bind("DayOffStr25") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate26" runat="server"   Text='<%# Bind("Date26") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift26" runat="server"   Text='<%# Bind("Shift26") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                         
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd26" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode26" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect26" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit26" runat="server" Enabled='<%# Bind("IsEnabled26") %>'  /><br />
                                            <asp:LinkButton  ID="lnk26" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode26") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift26") %>' ToolTip='<%# Bind("date26") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk26" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode26") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date26") %>'  CommandArgument='<%# Bind("DayOffStr26") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate27" runat="server"   Text='<%# Bind("Date27") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift27" runat="server"   Text='<%# Bind("Shift27") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd27" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode27" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect27" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit27" runat="server" Enabled='<%# Bind("IsEnabled27") %>'  /><br />
                                            <asp:LinkButton  ID="lnk27" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode27") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift27") %>' ToolTip='<%# Bind("date27") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk27" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode27") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date27") %>'  CommandArgument='<%# Bind("DayOffStr27") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate28" runat="server"   Text='<%# Bind("Date28") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift28" runat="server"   Text='<%# Bind("Shift28") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd28" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode28" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect28" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit28" runat="server" Enabled='<%# Bind("IsEnabled28") %>'  /><br />
                                            <asp:LinkButton  ID="lnk28" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode28") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift28") %>' ToolTip='<%# Bind("date28") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk28" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode28") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date28") %>'  CommandArgument='<%# Bind("DayOffStr28") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate29" runat="server"   Text='<%# Bind("Date29") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift29" runat="server"   Text='<%# Bind("Shift29") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd29" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode29" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect29" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit29" runat="server" Enabled='<%# Bind("IsEnabled29") %>'  /><br />
                                            <asp:LinkButton  ID="lnk29" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode29") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift29") %>' ToolTip='<%# Bind("date29") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk29" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode29") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date29") %>'  CommandArgument='<%# Bind("DayOffStr29") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" CssClass="column_style_left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate30" runat="server"   Text='<%# Bind("Date30") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift30" runat="server"   Text='<%# Bind("Shift30") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd30" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode30" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect30" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit30" runat="server" Enabled='<%# Bind("IsEnabled30") %>'  /><br />
                                            <asp:LinkButton  ID="lnk30" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode30") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift30") %>' ToolTip='<%# Bind("date30") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk30" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode30") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date30") %>'  CommandArgument='<%# Bind("DayOffStr30") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate31" runat="server"   Text='<%# Bind("Date31") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift31" runat="server"   Text='<%# Bind("Shift31") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd31" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode31" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect31" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit31" runat="server" Enabled='<%# Bind("IsEnabled31") %>'  /><br />
                                            <asp:LinkButton  ID="lnk31" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode31") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift31") %>' ToolTip='<%# Bind("date31") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk31" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode31") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date31") %>'  CommandArgument='<%# Bind("DayOffStr31") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                       
                                   <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate32" runat="server"   Text='<%# Bind("Date32") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift32" runat="server"   Text='<%# Bind("Shift32") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd32" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode32" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect32" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit32" runat="server" Enabled='<%# Bind("IsEnabled32") %>'  /><br />
                                            <asp:LinkButton  ID="lnk32" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode32") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift32") %>' ToolTip='<%# Bind("date32") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk32" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode32") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date32") %>'  CommandArgument='<%# Bind("DayOffStr32") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                        
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate33" runat="server"   Text='<%# Bind("Date33") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift33" runat="server"   Text='<%# Bind("Shift33") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd33" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode33" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect33" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit33" runat="server" Enabled='<%# Bind("IsEnabled33") %>'  /><br />
                                            <asp:LinkButton  ID="lnk33" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode33") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift33") %>' ToolTip='<%# Bind("date33") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk33" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode33") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date33") %>'  CommandArgument='<%# Bind("DayOffStr33") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                        
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate34" runat="server"   Text='<%# Bind("Date34") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift34" runat="server"   Text='<%# Bind("Shift34") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd34" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode34" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect34" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit34" runat="server" Enabled='<%# Bind("IsEnabled34") %>'  /><br />
                                            <asp:LinkButton  ID="lnk34" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode34") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift34") %>' ToolTip='<%# Bind("date34") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk34" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode34") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date34") %>'  CommandArgument='<%# Bind("DayOffStr34") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                        
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblDate35" runat="server"   Text='<%# Bind("Date35") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate >
                                            <asp:Label ID="lblShift35" runat="server"   Text='<%# Bind("Shift35") %>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                       <HeaderTemplate>
                                       <center>
                                            <asp:Label ID="lblhd35" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:Label ID="lblDayCode35" runat="server" style="color:#7092be; font-size: 12px" /><br />
                                            <asp:CheckBox ID="txtIsSelect35" onclick ="GetSelectedDate(this);" VerticalAlign="Top" runat="server" />
                                        </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsEdit35" runat="server" Enabled='<%# Bind("IsEnabled35") %>'  /><br />
                                            <asp:LinkButton  ID="lnk35" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("ShiftCode35") %>' OnClick="lnkEditShift_Click" CausesValidation="false" TabIndex ='<%# Bind("Shift35") %>' ToolTip='<%# Bind("date35") %>'></asp:LinkButton>
                                            <asp:LinkButton  ID="Dlnk35" CssClass="lnktextsmall-Color" style="font-size:11px;" runat="server"  Text='<%# Bind("DayTypeCode35") %>'  OnClick="lnkEditDayOff_Click" CausesValidation="false" ToolTip='<%# Bind("date35") %>'  CommandArgument='<%# Bind("DayOffStr35") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#F7F6F7" CssClass="column_style_right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                                    </asp:TemplateField>
                        
                                </Columns>
                            <PagerSettings Mode="NextPreviousFirstLast" />
                        </mcn:DataPagerGridView> 
                    </div> 
                <div class="row">
                        <div class="col-md-4">
                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="grdMain">
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
                            
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div>
  <script type="text/javascript">


      function ViewCrew(fval) {
          var isCrew = fval;
          if (isCrew == "True") {
              $('#divTime').removeAttr("style");
              $('#divShift').css({ 'display': 'none' });
    
              jQuery('#' + '<%=(cboShiftNo.ClientID)%>').rules("add", { required: false });
              jQuery('#' + '<%=(txtIn1.ClientID)%>').rules("add", { required: true });
              jQuery('#' + '<%=(txtOut1.ClientID)%>').rules("add", { required: true });
          } else {
              $('#divTime').css({ 'display': 'none' });
              $('#divShift').removeAttr("style");

              jQuery('#' + '<%=(cboShiftNo.ClientID)%>').rules("add", { required: true });
              jQuery('#' + '<%=(txtIn1.ClientID)%>').rules("add", { required: false });
              jQuery('#' + '<%=(txtOut1.ClientID)%>').rules("add", { required: false });

          }

      }
</script>
<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Change Shift</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRShiftNo" runat="server" ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" id="tcName" runat="server" >
                <label class="col-md-4 control-label has-space">Name of Employee :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtEmployeeNo" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" runat="server" id="tcdate" >
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="required form-control" Placeholder="From"></asp:TextBox> 
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateFrom" Format="MM/dd/yyyy" />                 
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtDateFrom" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="RangeValidator3" />                                                                           
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="required form-control" Placeholder="To"></asp:TextBox> 
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDateTo" Format="MM/dd/yyyy" />  
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtDateTo" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtDateTo" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator4" />                                                                           
                </div>
            </div>
            <div class="form-group" id="divShift" >
                <label class="col-md-4 control-label has-required">Shift :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNo" runat="server" CssClass="form-control required"  ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group" id="divTime" style="display:none;">
                <label class="col-md-4 control-label has-required">Shift Time In :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtIn1" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                        TargetControlID="txtIn1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                        ControlExtender="MaskedEditExtender4x"
                        ControlToValidate="txtIn1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
                </div>
             
                <label class="col-md-1 control-label">Out:</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOut1" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtOut1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtOut1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Charged To :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboCostCenterNo" DataMember="ECostCenterL" runat="server" CssClass="form-control" ></asp:Dropdownlist>
               </div>
            </div>
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Reason :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtReason" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>
            <br />
           </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>




<asp:Button ID="btnShowDayOff" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDayOff" runat="server" TargetControlID="btnShowDayOff" PopupControlID="pnlPopupDayOff"
    CancelControlID="imgCloseDayOff" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDayOff" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="fsMainDayOff">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Change Day Off</h4>
                <asp:Linkbutton runat="server" ID="imgCloseDayOff" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveDayOff" CssClass="fa fa-floppy-o submit fsMainDayOff lnkSaveDayOff" OnClick="lnkSaveDayOff_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                 <asp:Textbox ID="txtDTRDayoffNo" style="visibility:hidden;" runat="server" ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtDTRDayoffTransNo"  runat="server" Enabled="false" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtDayOffEmployeeNo" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtDayOffFullName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDayOffDateFrom" runat="server" CssClass="required form-control" style="display:inline-block;" placeholder="From" Enabled="false"></asp:TextBox> 
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDayOffDateFrom"  Format="MM/dd/yyyy" />  
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDayOffDateFrom" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDayOffDateFrom" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" />                                                                           
               </div>
               <div class="col-md-3">
                    <asp:TextBox ID="txtDayOffDateTo" runat="server" CssClass="required form-control" style="display:inline-block;" placeholder="To" Enabled="false"></asp:TextBox> 
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDayOffDateTo" Format="MM/dd/yyyy" />                   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDayOffDateTo" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />             
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDayOffDateTo" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />             
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="RangeValidator2" />                                                                           
                </div>                            
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Day Off 1 :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDayOffNo" DataMember="EDayOff" CssClass="form-control required" runat="server" ></asp:Dropdownlist>
                </div>
                <div class="col-md-2" style="visibility:hidden;">
                    <asp:CheckBox runat="server" ID="txtIsSixDays" Text="&nbsp; Six Day" style="color:Red;" />
                </div>
            </div>
         
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Day Off 2 :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDayOffNo2" DataMember="EDayOff" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Day Off 3 :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDayOffNo3" DataMember="EDayOff" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                </div>
            </div>
         
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reason :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDayOffReason" TextMode="MultiLine" Rows="3"  CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>
         
         </div>
          <!-- Footer here -->
         <br />   
        
    </fieldset>

</asp:Panel>

</asp:Content>

