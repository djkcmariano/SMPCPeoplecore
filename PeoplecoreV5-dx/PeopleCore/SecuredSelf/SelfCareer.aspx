<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfCareer.aspx.vb" Inherits="SecuredSelf_SelfCareer" Theme="PCoreStyle" %>
<%@ Register Src="~/include/jobdesc.ascx" TagName="Info" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    &nbsp;
                </div>
                <div>                
                    &nbsp;                                                                                                                                                    
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grd" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="MRNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPosition" CssClass="fa fa-list" CommandArgument='<%# Bind("MRNo") %>' OnClick="lnkApply_Click" Font-Size="Medium"  />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position Title" />
                                <dx:GridViewDataTextColumn FieldName="LocationDesc" Caption="Location" />
                                <dx:GridViewDataTextColumn FieldName="departmentdesc" Caption="Department" />
                                <dx:GridViewDataTextColumn FieldName="DatePublished" Caption="Date Published" />
                                <dx:GridViewDataTextColumn FieldName="DatePublishedTo" Caption="Application<br />Deadline" />                                        
                            </Columns>                            
                        </dx:ASPxGridView>
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<uc:Info runat="server" ID="Info1" />
</asp:Content>

