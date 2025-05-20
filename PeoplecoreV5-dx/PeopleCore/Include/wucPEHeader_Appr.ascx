<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucPEHeader_Appr.ascx.vb" Inherits="Include_wucPETemplateHeader" %>

<style type="text/css">
    .tab
    {
        width:97%; 
        font-size: 12px;   
        text-align:Left;
 
         
        margin-left:10px;     
        margin-right:0px;      
    }
    
    .txtbox
    {
        border-left: 0px solid #aaa;
        border-right: 0px solid #aaa;
        border-bottom: 1px solid #aaa;
        border-top: 0px solid #aaa;
        padding: 2px 2px 2px 2px;
        font-size:12px;
	    font-family: Tahoma, Arial, sans-serif,"Trebuchet MS"; 
        font-weight:normal;
                     
    }

</style>


                <p>
                     <div id="XXX" class="tab" style="padding-left:15px;padding-right:15px;padding-bottom:10px;">
                        <table cellpadding="0" cellspacing="2" border="0" width="98%">
                            <tr>
                                <td width="7%">
                                    <span >Employee No.</span>
                                </td>
                                <td width="1%">
                                    <span >:</span>
                                </td>
                                <td width="22%">
                                    <asp:Textbox ID="txtEmployeeCode" runat="server" width="95%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                                <td width="8%">
                                    <span >Position</span>
                                </td>
                                <td width="1%">
                                    <span >:</span>
                                </td>
                                <td width="22%">
                                    <asp:Textbox ID="txtPositionDesc" runat="server" width="95%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                                <td width="8%">
                                    <span >Applicable Year</span>
                                </td>
                                <td width="1%">
                                    <span >:</span>
                                </td>
                                <td width="15%">
                                    <asp:Textbox ID="txtApplicableYear" runat="server" width="90%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <span >Employee Name</span>
                                </td>
                                <td>
                                    <span >:</span>
                                </td>
                                <td>
                                    <asp:Textbox ID="txtFullName" runat="server" width="95%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                                <td>
                                    <span >Department</span>
                                </td>
                                <td>
                                    <span >:</span>
                                </td>
                                <td>
                                    <asp:Textbox ID="txtDepartmentDesc" runat="server" width="95%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                                <td>
                                    <span >Performance Period</span>
                                </td>
                                <td>
                                    <span >:</span>
                                </td>
                                <td>
                                    <asp:Textbox ID="txtPEPeriodDesc" runat="server" width="90%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <span >Date Hired</span>
                                </td>
                                <td>
                                    <span >:</span>
                                </td>
                                <td>
                                    <asp:Textbox ID="txtHiredDate" runat="server" width="95%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                                <td>
                                    <span >Immediate Superior</span>
                                </td>
                                <td>
                                    <span >:</span>
                                </td>
                                <td>
                                    <asp:Textbox ID="txtSuperiorName" runat="server" width="95%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                                <td>
                                    <span >Transaction No.</span>
                                </td>
                                <td>
                                    <span >:</span>
                                </td>
                                <td>
                                    <asp:Textbox ID="txtCode" runat="server" width="90%" class="txtbox" ReadOnly="true"></asp:Textbox>
                                </td>
                            </tr>

                        </table>
         
                     </div>
    
              </p>

       