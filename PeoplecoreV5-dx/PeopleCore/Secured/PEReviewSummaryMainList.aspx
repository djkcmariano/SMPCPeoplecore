<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEReviewSummaryMainList.aspx.vb" Inherits="Secured_PEReviewSummaryMainList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function disableenable(chk) {
        if (chk.checked) {
            //$("#lblpereviewno").addClass('col-md-4 control-label has-space');
            //$("#cboPEReviewMainNo").addClass('form-control');
            document.getElementById("ctl00_cphBody_lblpereviewno").className = "col-md-4 control-label has-space";
            document.getElementById("ctl00_cphBody_cboPEReviewMainNo").className = "form-control";
            document.getElementById("ctl00_cphBody_cboPEReviewMainNo").disabled = true;
            document.getElementById("ctl00_cphBody_cboPEReviewMainNo").value = "";
        } else {
            //$("#lblpereviewno").addClass('col-md-4 control-label has-required');
            document.getElementById("ctl00_cphBody_lblpereviewno").className = "col-md-4 control-label has-required";
            document.getElementById("ctl00_cphBody_cboPEReviewMainNo").className = "form-control required";
            document.getElementById("ctl00_cphBody_cboPEReviewMainNo").disabled = false;
        }
    }
</script>


<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">                                
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>                
                 <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">                        
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be recovered, Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                           
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEReviewSummaryMainNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" CommandArgument='<%# Bind("PEReviewSummaryMainNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Summary No." />                                                                                                                                          
                                <dx:GridViewDataTextColumn FieldName="SourceType" Caption="Source Type" Visible="False"/>                                                     
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year"  CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataTextColumn FieldName="PEPeriodDesc" Caption="Period Type" Visible="False" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="PENormsDesc" Caption="Performance Norms" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PEReviewMainCode" Caption="PE Review No." Visible="false" />   
                                
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" Visible="False" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" Visible="False" />  
                                <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" Visible="False" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Posted Date" Visible="False" />
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />
                                <dx:GridViewDataTextColumn FieldName="LastDateProcess" Caption="Date Processed"/> 

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkView_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" />     
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
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopup" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEReviewSummaryMainNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">PE Summary No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Applicable Year :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtApplicableyear" runat="server" CssClass="form-control required" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Performance Period Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPEPeriodNo" runat="server" DataMember="EPEPeriod" CssClass="form-control required"></asp:Dropdownlist>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control required" TextMode="MultiLine" ></asp:Textbox>
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-4 control-label has-space">Performance Norms :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPENormsNo" runat="server" DataMember="EPENorms" CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsManual" onclick="disableenable(this);"  Text="&nbsp; Please check here if employee rating is uploaded." />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required" runat="server" id="lblpereviewno">PE Review No. :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPEReviewMainNo" runat="server" DataMember="EPEReviewMainL" CssClass="form-control required"></asp:Dropdownlist>
                </div>
            </div>

             <br />       
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


</asp:content>
