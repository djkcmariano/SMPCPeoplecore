<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucMR_ScreeningProcess.ascx.vb" Inherits="Include_Tab" %>

<script type="text/javascript">
    $(window).resize(function () {
        if ($(window).width() < 768) {
            $('#responsive').removeClass('list-group-horizontal');
            $('#responsive').addClass('list-group');
        } else {
            $('#responsive').addClass('list-group-horizontal');
            $('#responsive').removeClass('list-group');
        }
    });
</script>

<style type="text/css">
 
/* List-group-horizontal*/ 
.list-group-horizontal .list-group-item { 
    display: inline-block; 
} 
.list-group-horizontal .list-group-item { 
    margin-bottom: 0; 
    margin-left:-4px; 
    margin-right: 0; 
} 
.list-group-horizontal .list-group-item:first-child { 
    border-top-right-radius:0; 
    border-bottom-left-radius:4px; 
} 

.list-group-horizontal .list-group-item:last-child { 
    border-top-right-radius:4px;  
    border-bottom-left-radius:0; 
} 

</style>


<div class="page-content-wrap">         
    <div class="list-group-horizontal" id="responsive">
        <asp:PlaceHolder runat="server" ID="PlaceHolder1" />
    </div>
    <%--<hr style="margin-top:0px;"/>--%>
</div>
