<%@ Page Language="VB" AutoEventWireup="false"   EnableEventValidation="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpList_old.aspx.vb" Inherits="Secured_EmpList" %>

<%@ Register Src="../Include/wucFilterGeneric.ascx" TagName="wucFilter"   TagPrefix="uc1" %>
<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
 
 <table width="90%" cellpadding="0" cellspacing="0" border="0" align="center">
    <tr style="vertical-align:bottom;">
        <td class="tblleftTD" >
               <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkGo_Click" style="width:200px;" CssClass="form-control" runat="server" 
               ></asp:Dropdownlist>
        </td>

        <td class="tblrightTD" style="height:40px;" >
           <%-- <asp:TextBox runat="server" ID="fltxtfilter" CssClass="form-control-search" style="width:320px;" > </asp:TextBox>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtwatermark" runat="server" 
            TargetControlID="fltxtfilter"/>
            <asp:button runat="server"  ID="lnkGo" CausesValidation="false" CssClass="form-control-search" OnClick="lnkGo_Click" Text="Filter"> </asp:button>--%>
            <uc1:wucFilter ID="wucFilter" runat="server" />   
        </td> 
   </tr>
   <tr>
        <td colspan="2" style="width:100%; text-align:center;">
                 
        <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="EmployeeNo,FullName" >
        <Columns>
            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server"   Text='<%# Bind("EmployeeNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" Visible="false"  >
                <ItemTemplate>
                                
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" Width="2%" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False"  >
                <ItemTemplate>
                    <asp:Image Width="60" Height="60" runat="server" ID="imgPic" ImageUrl='<%# Eval("EmployeeNo", "frmShowImage.ashx?tNo={0}")+"&tIndex=2"%>'  ImageAlign="Middle" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" Width="2%" />
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="false" >
                <ItemTemplate >
                                  
                        <asp:LinkButton ID="Label1" runat="server" style="background-color: #ffffff; border: solid #fff 0px; font-family: Arial, Helvetica, sans-serif;
	                    font-size: 12px;
	                    font-style: normal;
	                    line-height: normal;
	                    font-weight: bold;
	                    font-variant:small-caps; text-decoration: none; color: #656eb3;text-align :center " 
                        onmouseout="this.style.textDecorationUnderline=false;"  
                        onmouseover="this.style.textDecorationUnderline=true;"  Text='<%# Bind("Fullname") %>'  OnClick="lnkEdit_Click" ></asp:LinkButton>
                        <br />
                        <asp:Label runat="server" ID="Label16" Text="Employee # :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="lblApplicantCode" runat="server"   Text='<%# Bind("EmployeeCode") %>'></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="Label6" Text="Birthday :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="Label7" runat="server"   Text='<%# Bind("birthdate") %>'></asp:Label>
                        <br />
                         <asp:Label runat="server" ID="Label2" Text="Mobile # :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server"   Text='<%# Bind("MobileNo") %>'></asp:Label>
                        <br />
                       <asp:Label runat="server" ID="Label4" Text="Address :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label runat="server" ID="Label8" Text='<%# Bind("PresentAddress") %>' style="background-color: #ffffff; border: solid #fff 0px; font-family: Arial, Helvetica, sans-serif;
	                            font-size: 11px;
	                            font-style:normal;
	                            line-height: normal;
	                            font-weight: normal;
	                            font-variant: normal; text-decoration: none;"
                        />         

                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" Width="25%"  />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false" >
                <ItemTemplate >
                        <asp:Label runat="server" ID="Label4x" Text="Position :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="Label5x" runat="server"   Text='<%# Bind("PositionDesc") %>'></asp:Label>
                        <br />         
                        <asp:Label runat="server" ID="Label2x" Text='<%# Bind("tdateDesc") %>' style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="Label3x" runat="server"   Text='<%# Bind("tDate") %>'></asp:Label>
                        <br />
                        
                        <asp:Label runat="server" ID="Label12" Text="Date of Regularization :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="Label13" runat="server"   Text='<%# Bind("RegularizedDate") %>'></asp:Label>
                        <br />

                        <asp:Label runat="server" ID="Label5" Text="Emp. Status :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="Label9" runat="server"   Text='<%# Bind("EmployeeStatDesc") %>'></asp:Label>
                        <br />

                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" Width="25%"  />
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="false" >
                <ItemTemplate >
                                  
                        <asp:Label runat="server" ID="Label2xx" Text="Department :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="Label3xx" runat="server"   Text='<%# Bind("DepartmentDesc") %>'></asp:Label>
                        <br />
                       
                        

                        <asp:Label runat="server" ID="Label4xx" Text="Superior :" style="font-size:11px; font-variant:normal; font-weight:bold;
                        font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                        <asp:Label ID="Label10" runat="server"   Text='<%# Bind("SuperiorName") %>'></asp:Label>
                        <br />
                       
                                  
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" Width="25%"  />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false" >
                <ItemTemplate >
                                  
                        <table border="0px">
                            <tr>
                                <td style="width:75%; border:0">
                                    <asp:Label runat="server" ID="Label11x" Text="Attachment :" style="font-size:11px; font-variant:normal; font-weight:bold; font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                                </td>
                                <td style="width:25%; border:0; text-align:center">
                                    <asp:ImageButton ID="lnkCaptionx" runat="server" SkinID="grdOpenbtn" OnClick="lnkOpen_Click" CausesValidation="false" CommandArgument='<%# Bind("EmployeeNo") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td style="width:75%; border:0">
                                    <asp:Label runat="server" ID="Label11" Text="Service Record :" style="font-size:11px; font-variant:normal; font-weight:bold; font-family:Arial, Helvetica, sans-serif;color:#444444;" />&nbsp;&nbsp;
                                </td>
                                <td style="width:25%; border:0; text-align:center">
                                    <asp:ImageButton ID="lnkCaptionServiceRecord" runat="server" OnPreRender="addTrigger_PreRender" OnClick="lnkServiceRecord_Click" CausesValidation="false" ToolTip="Print Service Record" ImageUrl="~/images/printer-blue-icon.png" />
                                </td>
                            </tr>
                        </table>
                                  
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" Width="12%"  />
            </asp:TemplateField>
          </Columns>
                    
        </mcn:DataPagerGridView>   
    </td> 
</tr>
<tr style="height:5px;" >
    <td colspan="2"></td>
</tr>
<tr>
        <td class="tblleftTD">
                   
            <asp:DataPager runat="server" ID="ItemDataPager" PageSize="10" PagedControlID="grdMain">
                <Fields>
                <asp:NextPreviousPagerField ButtonType="Image" 
                    FirstPageImageUrl="~/images/arrow_first.png" 
                    PreviousPageImageUrl="~/images/arrow_previous.png"  ShowFirstPageButton="true"
                    ShowPreviousPageButton="true"
                    ShowLastPageButton="false" 
                    ShowNextPageButton ="false"
                    />
                    
                        
                <asp:TemplatePagerField>              
                    <PagerTemplate>
                    <b>
                    Page
                    <asp:Label runat="server" ID="CurrentPageLabel" 
                        Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" />
                    of
                    <asp:Label runat="server" ID="TotalPagesLabel" 
                        Text="<%# Math.Ceiling (System.Convert.ToDouble(Container.TotalRowCount) / Container.PageSize) %>" />
                    (
                    <asp:Label runat="server" ID="TotalItemsLabel" 
                        Text="<%# Container.TotalRowCount%>" />
                    records)
                        
                    </b>
                    </PagerTemplate>
                    </asp:TemplatePagerField>
                    <asp:NextPreviousPagerField ButtonType="Image" LastPageImageUrl="~/images/arrow_last.png" NextPageImageUrl="~/images/arrow_next.png"  ShowFirstPageButton="false"
                    ShowPreviousPageButton="false"
                    ShowLastPageButton="true" 
                    ShowNextPageButton ="true"
                    />

                </Fields>
            </asp:DataPager> 
        </td>
        <td class="tblrightTD">
                <asp:button runat="server" cssClass="form-control-search"  ID="lnkAdd" CausesValidation="false" UseSubmitBehavior="false" OnClick="lnkAdd_Click" Text="Add" >
                </asp:button>
 
        </td>
    </tr>

</table>
</asp:content>