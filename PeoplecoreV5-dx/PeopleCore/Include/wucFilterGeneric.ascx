<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucFilterGeneric.ascx.vb" Inherits="Include_wucFilterGeneric" %>
<div class="input-group">
    <asp:TextBox runat="server" ID="txtfilter" CssClass="form-control" placeholder="Search here..."> </asp:TextBox>
    <div  class="input-group-btn">
        <asp:button runat="server"  ID="lnkGo"  CausesValidation="false" CssClass="btn btn-default" Text="Go!"></asp:button>
        <a href="#popup" class="btn btn-default">&nbsp;<span class="caret"></span></a>                            
    </div>
</div>


    <div id="popup">
	    <div class="popup-container">
            <div class="form-group">
                <label class="col-md-4 control-label">Filter By :</label>
                <div class="col-md-8">
                    <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control"  AutoPostBack="true"  OnSelectedIndexChanged="cbofilterby_SelectedIndexChanged">
                    </asp:DropDownList>
                 </div>
             </div>
             <br />
		     <div class="form-group">
                    <label class="col-md-4 control-label">Filter Value :</label> 
                    <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control">
                            </asp:DropDownList>
                    </div>
               </div>
		     <br />
		    <div class="form-group">
                <label class="col-md-4 control-label"></label>
                           
                <div class="col-md-8">
                    <asp:button runat="server"  ID="lnkCriteria" CausesValidation="false" 
                    CssClass="form-control-criteria" Text="Submit" > </asp:button>
                                    
                </div>
                </div>
		    <a class="popup-close" href="#closed">X</a>
        </div>
</div>  
