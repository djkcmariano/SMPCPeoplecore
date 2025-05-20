<%@ Control Language="VB" AutoEventWireup="false" CodeFile="JobProfile.ascx.vb" Inherits="Include_JobProfile" %>


<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div class="container-fluid entryPopupDetl">        
        <div class="page-content-wrap">
                <br />                   
                <div class="row">
                    <div class="form-group">
                        
                        <div class="col-md-11">
                            <label>Position :</label>&nbsp;&nbsp;
                            <asp:Label ID="lblPositionDesc" runat="server" ></asp:Label> 
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-11">
                            <label>Reporting To :</label>&nbsp;&nbsp;
                            <asp:Label ID="lblReportingTo" runat="server"></asp:Label> 
                        </div>
                    </div>
                    <div class="form-group">
                        
                        <div class="col-md-11">
                            <label>Supervises :</label>
                            <asp:Label ID="lblSupervises" runat="server" ></asp:Label> 
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-11">
                            <label>Coordinates With :</label>
                            <asp:Label ID="lblCoordinate" runat="server"></asp:Label> 
                        </div>
                    </div>
                </div>
                <br />

                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Educational Requirements</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdEduc" ClientInstanceName="grdEduc" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="JDEducNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="EducTypeDesc" Caption="Educational Type" />
                                                <dx:GridViewDataTextColumn FieldName="EducLevelDesc" Caption="Educational Level" />
                                                <dx:GridViewDataTextColumn FieldName="IsGraduatedDesc" Caption="Graduated" />                                            
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div> 
                
                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Experience Requirements</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdExpe" ClientInstanceName="grdExpe" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="JDExpeNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="ExpeTypeDesc" Caption="Experience Type" />
                                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />                                            
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>
                
                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Training Requirements</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdTrn" ClientInstanceName="grdTrn" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="JDTrnNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />
                                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="No. Of Hours" />
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>
                
                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Eligibility Requirements</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="gridElig" ClientInstanceName="gridElig" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="JDEligNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="ExamTypeDesc" Caption="Examination Type" />
                                                <dx:GridViewDataTextColumn FieldName="AverageRate" Caption="Rating" />
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>
                
                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Skills and Competency Requirements</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdComp" ClientInstanceName="grdComp" runat="server" SkinID="grdDX" Width="100%" KeyFieldName="JDCompNo">                                                                                   
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="CompTypeDesc" Caption="Competency Type" />
                                                <dx:GridViewDataTextColumn FieldName="CompDesc" Caption="Competency" />
                                                <dx:GridViewDataTextColumn FieldName="CompScaleDesc" Caption="Proficiency Level" />
                                            </Columns>                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>

        </div>
        </div>
    </fieldset>
</asp:Panel>
