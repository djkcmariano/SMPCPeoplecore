<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmpStandardHeader_EI_Clearance.aspx.vb" Inherits="SecuredSelf_SelfEmpStandardHeader_EI_Clearance" %>

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
                        <ul class="panel-controls">
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeEIClearanceNo">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="fullname" Caption="Name of Superior" />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeClearanceTypeDesc" Caption="Clearance Type" />
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />
                                    <dx:GridViewDataCheckColumn FieldName="IsCleared" Caption="Status" />
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>


