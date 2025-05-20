<%@ Control Language="VB" AutoEventWireup="false" CodeFile="jobdesc.ascx.vb" Inherits="include_jobdesc" %>

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
    .cbo1
     {
        width:200px;
    }
        
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

<script type="text/javascript">
    
    var checker = document.getElementById('<%=chkAgree.ClientID%>');
    var sendbtn = document.getElementById('<%=lnkSave.ClientID%>');
    checker.onchange = function () {
        sendbtn.disabled = !!this.checked;
    };
  
</script>        

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation"  BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" OnClick="lnkClose_Click" />
            <%--<asp:Linkbutton runat="server" ID="lnkApply" CssClass="fa fa-edit" ToolTip="Apply" />--%>
        </div>
        <div class="container-fluid entryPopupDetl">        
            <div class="page-content-wrap">              
                <div class="col-md-12">                                                        
                    <div class="form-horizontal">                                           
                        <asp:Literal runat="server" ID="lContent" />                        
                        <asp:HiddenField runat="server" ID="hifMRNo" />
                        <b>Education</b>
                        <asp:GridView runat="server" ID="grdEduc" AutoGenerateColumns="false" BorderWidth="0" Width="100%" EmptyDataText="No record found" ShowHeader="false">
                            <Columns>                               
                                <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" HeaderText="Description">
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="70%" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" Value='<%# Bind("QSTypeNo") %>' ID="hifQSTypeNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ID") %>' ID="hifQSNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ApplicantQSNo") %>' ID="hifApplicantQSNo" />
                                        <asp:Label runat="server" Text='<%# Bind("Value") %>' ID="lblValue" />                                       
                                    </ItemTemplate>
                                    <ItemStyle Width="70%" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" >
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="30%" />
                                    <ItemTemplate>
                                        <asp:RadioButtonList runat="server" ID="rbl" Font-Bold="false" Font-Size="X-Small" RepeatDirection="Horizontal" RepeatLayout="Flow" Visible='<%# Bind("IsEnabled") %>'>
                                            <asp:ListItem Text="&nbsp;Not Complied&nbsp;" Value="0" />
                                            <asp:ListItem Text="&nbsp;Complied&nbsp;" Value="1" />
                                        </asp:RadioButtonList>
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsPass") %>' ID="hifIsPass" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsComplied") %>' ID="hifIsComplied" />                                       
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>--%>                                
                            </Columns>
                        </asp:GridView><br />
                        <b>Work Experience</b>
                        <asp:GridView runat="server" ID="grdExpe" AutoGenerateColumns="false" BorderWidth="0" Width="100%" EmptyDataText="No record found" ShowHeader="false">
                            <Columns>                            
                                <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" HeaderText="Description">
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="70%" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" Value='<%# Bind("QSTypeNo") %>' ID="hifQSTypeNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ID") %>' ID="hifQSNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ApplicantQSNo") %>' ID="hifApplicantQSNo" />
                                        <asp:Label runat="server" Text='<%# Bind("Value") %>' ID="lblValue" />                                       
                                    </ItemTemplate>
                                    <ItemStyle Width="70%" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="30%" />
                                    <ItemTemplate>
                                        <asp:RadioButtonList runat="server" ID="rbl" Font-Bold="false" Font-Size="X-Small" RepeatDirection="Horizontal" RepeatLayout="Flow" Visible='<%# Bind("IsEnabled") %>'>
                                            <asp:ListItem Text="&nbsp;Not Complied&nbsp;" Value="0" />
                                            <asp:ListItem Text="&nbsp;Complied&nbsp;" Value="1" />
                                        </asp:RadioButtonList>
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsPass") %>' ID="hifIsPass" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsComplied") %>' ID="hifIsComplied" />                                        
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>--%>                                
                            </Columns>
                        </asp:GridView><br />
                        <b>Training</b>
                        <asp:GridView runat="server" ID="grdTraining" AutoGenerateColumns="false" BorderWidth="0" Width="100%" EmptyDataText="No record found" ShowHeader="false">
                            <Columns>                              
                                <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" HeaderText="Description">
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="70%" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" Value='<%# Bind("QSTypeNo") %>' ID="hifQSTypeNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ID") %>' ID="hifQSNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ApplicantQSNo") %>' ID="hifApplicantQSNo" />
                                        <asp:Label runat="server" Text='<%# Bind("Value") %>' ID="lblValue" />                                       
                                    </ItemTemplate>
                                    <ItemStyle Width="70%" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="30%" />
                                    <ItemTemplate>
                                        <asp:RadioButtonList runat="server" ID="rbl" Font-Bold="false" Font-Size="X-Small" RepeatDirection="Horizontal" RepeatLayout="Flow" Visible='<%# Bind("IsEnabled") %>'>
                                            <asp:ListItem Text="&nbsp;Not Complied&nbsp;" Value="0" />
                                            <asp:ListItem Text="&nbsp;Complied&nbsp;" Value="1" />
                                        </asp:RadioButtonList>
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsPass") %>' ID="hifIsPass" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsComplied") %>' ID="hifIsComplied" />                                        
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>--%>                                
                            </Columns>
                        </asp:GridView><br />
                        <b>Eligibility</b>
                        <asp:GridView runat="server" ID="grdExam" AutoGenerateColumns="false" BorderWidth="0" Width="100%" EmptyDataText="No record found" ShowHeader="false">
                            <Columns>                              
                                <asp:TemplateField ItemStyle-BorderWidth="0" HeaderText="Description">
                                    <HeaderStyle HorizontalAlign="Center" Width="70%" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" Value='<%# Bind("QSTypeNo") %>' ID="hifQSTypeNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ID") %>' ID="hifQSNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ApplicantQSNo") %>' ID="hifApplicantQSNo" />
                                        <asp:Label runat="server" Text='<%# Bind("Value") %>' ID="lblValue" />                                       
                                    </ItemTemplate>
                                    <ItemStyle Width="70%" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="30%" />
                                    <ItemTemplate>
                                        <asp:RadioButtonList runat="server" ID="rbl" Font-Bold="false" Font-Size="X-Small" RepeatDirection="Horizontal" RepeatLayout="Flow" Visible='<%# Bind("IsEnabled") %>'>
                                            <asp:ListItem Text="&nbsp;Not Complied&nbsp;" Value="0" />
                                            <asp:ListItem Text="&nbsp;Complied&nbsp;" Value="1" />
                                        </asp:RadioButtonList>
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsPass") %>' ID="hifIsPass" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsComplied") %>' ID="hifIsComplied" />                                        
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>--%>                                
                            </Columns>
                        </asp:GridView><br />
                        
                        <asp:GridView runat="server" ID="grdQS" AutoGenerateColumns="false" BorderWidth="0" Width="100%" Visible="false">
                            <Columns>                               
                                <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" HeaderText="Description">
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="70%" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" Value='<%# Bind("QSTypeNo") %>' ID="hifQSTypeNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ID") %>' ID="hifQSNo" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("ApplicantQSNo") %>' ID="hifApplicantQSNo" />
                                        <asp:Label runat="server" Text='<%# Bind("Value") %>' ID="lblValue" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="30%" />
                                    <ItemTemplate>
                                        <asp:RadioButtonList runat="server" ID="rbl" Font-Bold="false" Font-Size="X-Small" RepeatDirection="Horizontal" RepeatLayout="Flow" Visible='<%# Bind("IsEnabled") %>'>
                                            <asp:ListItem Text="&nbsp;Not Complied&nbsp;" Value="0" />
                                            <asp:ListItem Text="&nbsp;Complied&nbsp;" Value="1" />
                                        </asp:RadioButtonList>
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsPass") %>' ID="hifIsPass" />
                                        <asp:HiddenField runat="server" Value='<%# Bind("IsComplied") %>' ID="hifIsComplied" />                                        
                                    </ItemTemplate>
                                </asp:TemplateField>--%>                                
                            </Columns>
                        </asp:GridView>
                        <br />                                                
                        <div class="form-group" runat="server" id="divComp"><div class="row"><div class="col-md-12 header">Competencies</div></div></div>
                        <asp:GridView runat="server" ID="grdComp" BorderWidth="0" Width="100%" CssClass="gv" HeaderStyle-HorizontalAlign="Center">
                            <Columns>                                                                                   
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:PlaceHolder runat="server" ID="ph" Visible="false">
                        
                        <div class="form-group">
                            <label class="col-md-7">How did you know about this career opportunity :</label>
                            <div class="col-md-5">                                
                                <asp:DropDownList runat="server" ID="cboVacancySourceNo" DataMember="EVacancySource" CssClass="form-control" />
                            </div>                            
                        </div>
                        <br />
                        <div class="form-group"> 
                            <%--<label class="col-md-"></label>--%>                               
                            <div class="col-md-12">                    
                                <asp:Label runat="server" ID="lblDisc" CssClass="form-control" Width="100%" Height="100%" style=" text-align:justify" />
                            </div>
                        </div>                        
                        <div class="form-group">                            
                            <div class="col-md-11">                                
                                <asp:CheckBox runat="server" ID="chkAgree" Text="&nbsp;I agree"  />
                            </div>                            
                        </div>
                        </asp:PlaceHolder>
                        <div style="text-align:center">
                            <asp:Button runat="server" ID="lnkSave" CssClass="btn btn-primary" Text="Apply" OnClick="lnkApply_Click" />
                        </div>
                        <br /><br />
                    </div>                    
                </div>
            </div>
        </div>
    </fieldset>
</asp:Panel>