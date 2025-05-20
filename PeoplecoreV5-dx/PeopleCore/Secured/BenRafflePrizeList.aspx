<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="BenRafflePrizeList.aspx.vb" Inherits="Secured_BenRafflePrizeList" EnableEventValidation="false" %>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">


<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    
                </div>
                <div>                                                
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">                                            
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                                                                                                                                                                             
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="RaffleNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="RankDesc" Caption="Prize" />
                                <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="PayIncome Type" />
                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />
                            </Columns>                            
                        </dx:ASPxGridView> 
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRafflePrizeNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Prize :</label>
                <div class="col-md-3">
                    <asp:HiddenField runat="server" ID="hifRankNo" />
                    <asp:TextBox ID="txtRankDesc" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Income Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboPayIncomeTypeNo" DataMember="EPayIncomeType" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control number"></asp:TextBox>
                </div>
            </div>

        </div>

        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
