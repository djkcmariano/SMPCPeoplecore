<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptList_EU_Designer.aspx.vb" Inherits="Secured_RptList_EU_Designer" %>

<%@ Register assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web.ClientControls" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
   
</head>


<body>
    <form id="form1" runat="server">
    <div>    
        <dx:ASPxReportDesigner 
            ID="ASPxReportDesigner1" runat="server"         
            onsavereportlayout="ASPxReportDesigner1_SaveReportLayout"
            >            
        </dx:ASPxReportDesigner>    
    </div>
    </form>
</body>
</html>