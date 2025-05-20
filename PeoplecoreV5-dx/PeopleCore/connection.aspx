<%@ Page Title="" Language="VB" MasterPageFile="~/masterpage/masterblank.master" AutoEventWireup="false" CodeFile="connection.aspx.vb" Inherits="connection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" contentplaceholderid="cphBody" Runat="Server">

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">  
            <div class="panel-heading">
                <h4 class="panel-title">Database Connection</h4>                   
            </div>             
             <div class="panel-body">
                <fieldset class="form" id="fsMain"> 
                    <div  class="form-horizontal">                                 
                        <div class="form-group">
                            <label class="col-md-5 control-label">Server</label>
                            <div class="col-md-3">                                            
                                <asp:TextBox runat="server" ID="txtServer" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Database</label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtDatabase" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Username</label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" />
                            </div>
                        </div>                                    
                        <div class="form-group">
                            <label class="col-md-5 control-label">Password</label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label"></label>
                            <div class="col-md-3">
                                <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btn btn-primary submit fsMain btnSave" Text="Submit" />                                            
                            </div>
                        </div>
                    </div>                    
                </fieldset>
            </div>                                                            
        </div>                        
    </div>
</div>
</asp:Content>

