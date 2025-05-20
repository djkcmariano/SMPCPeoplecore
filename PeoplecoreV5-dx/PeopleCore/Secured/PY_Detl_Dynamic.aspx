<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PY_Detl_Dynamic.aspx.vb" Inherits="Secured_PY_Detl_Dynamic" %>

<asp:Content id="cntNo" contentplaceholderid="cphBody" runat="server">
  
  <script type="text/javascript">
      $(document).ready(function () {

      });

      function getselectedvalue_none(dval) {
          $('#' + dval).css({ 'display': 'none' });
      }

      function getselectedvalue_display(dval) {
          $('#' + dval).removeAttr("style");
      }
  </script>   

<asp:Panel runat="server" ID="Panel1">
    
</asp:Panel>
    
</asp:Content>