<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="AppServiceTemplate.aspx.vb" Inherits="Secured_AppServiceTemplate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    &nbsp;
                </div>
                <div>                                                
                    <ul class="panel-controls">                                    
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>                                                                                                                                                     
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grd" runat="server">
                            <Columns>                                
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate >                                        
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ApplicantCoreTemplateNo") %>' OnClick="lnkEdit_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Code" HeaderText="Transaction No.">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ApplicantCoreCateDesc" HeaderText="Category">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                </asp:BoundField>                                                                                                                                                                                                                                                   
                                <asp:BoundField DataField="Messages" HeaderText="Template Message">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="60%" />
                                </asp:BoundField>                            
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                </asp:TemplateField>                                       
                            </Columns>
                            <PagerSettings Mode="NextPreviousFirstLast" />
                        </mcn:DataPagerGridView>                                                    
                    </div>
                </div>                                                           
            </div>                   
        </div>        
    </div>
</asp:Content>
