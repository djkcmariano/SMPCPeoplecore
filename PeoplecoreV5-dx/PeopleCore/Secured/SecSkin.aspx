<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SecSkin.aspx.vb" Inherits="Secured_SecSkin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type='text/css'>
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
     <uc:Tab runat="server" ID="Tab">
        <Content>

        <asp:Panel id="pnlPopupMain" runat="server">
                    <br /><br />              
                  <fieldset class="form" id="fsMain">
                      <div  class="form-horizontal">                                                                                                    
                        <div class="form-group">  
                            <label class="col-md-4 control-label has-space" style=" font-size:1.25em"><b>Peoplecore Logo</b></label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Type :</label>
                            <div class="col-md-4">                                
                                <asp:DropDownList runat="server" ID="cboLogo" CssClass="form-control">
                                    <asp:ListItem Text="Dark Logo" Value="1" />
                                    <asp:ListItem Text="Light Logo" Value="2" />
                                </asp:DropDownList>
                            </div>                           
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtPeoplecoreBG" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender23"  
                                    TargetControlID="txtPeoplecoreBG"                                    
                                    Enabled="True"
                                    SampleControlID="Label23" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label23" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                        </div> 
                        <div class="form-group">  
                            <label class="col-md-4 control-label has-space" style=" font-size:1.25em"><b>Banner</b></label>
                        </div>                       
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtBannerColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender0"  
                                    TargetControlID="txtBannerColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label0" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label0" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Icon Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtBannerIconColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender18"  
                                    TargetControlID="txtBannerIconColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label18" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label18" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hover Icon Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtBannerIconHoverColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender19"  
                                    TargetControlID="txtBannerIconHoverColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label19" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label19" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hover Icon Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtBannerIconHoverBGColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender20"  
                                    TargetControlID="txtBannerIconHoverBGColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label20" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label20" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Active Icon Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtBannerIconActiveBGColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender21"  
                                    TargetControlID="txtBannerIconActiveBGColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label21" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label21" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Active Icon Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtBannerIconActiveColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender22"  
                                    TargetControlID="txtBannerIconActiveColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label22" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label22" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                        </div>
                        <div class="form-group">  
                            <label class="col-md-4 control-label has-space" style=" font-size:1.25em"><b>Menu Item</b></label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Component Font Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtComponentFontColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender12"  
                                    TargetControlID="txtComponentFontColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label12" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label12" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Component Separator Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtComponentSepColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender13"  
                                    TargetControlID="txtComponentSepColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label13" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label13" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender1"  
                                    TargetControlID="txtMenuColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label1" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label1" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Font Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuItemFontColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender2"  
                                    TargetControlID="txtMenuItemFontColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label2" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label2" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Separator Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuItemSepColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender3"  
                                    TargetControlID="txtMenuItemSepColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label3" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label3" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Icon Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuItemIconColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender4"  
                                    TargetControlID="txtMenuItemIconColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label4" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label4" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>                         
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hover Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuHoverColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender5"  
                                    TargetControlID="txtMenuHoverColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label5" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label5" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hover Font Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuItemFontHoverColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender6"  
                                    TargetControlID="txtMenuItemFontHoverColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label6" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label6" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hover Icon Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuItemIconHoverColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender7"  
                                    TargetControlID="txtMenuItemIconHoverColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label7" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label7" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Active Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuActiveColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender8"  
                                    TargetControlID="txtMenuActiveColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label8" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label8" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Active Font Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuItemFontActiveColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender9"  
                                    TargetControlID="txtMenuItemFontActiveColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label9" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label9" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Active Icon Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuItemIconActiveColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender10"  
                                    TargetControlID="txtMenuItemIconActiveColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label10" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label10" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Caret Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuItemCaretColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender11"  
                                    TargetControlID="txtMenuItemCaretColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label11" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label11" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">  
                            <label class="col-md-4 control-label has-space" style=" font-size:1.25em"><b>Menu Sub Item</b></label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuSubColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender14"  
                                    TargetControlID="txtMenuSubColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label14" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label14" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Separator Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuSubSepColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender15"  
                                    TargetControlID="txtMenuSubSepColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label15" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label15" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hover Background Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuSubHoverColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender16"  
                                    TargetControlID="txtMenuSubHoverColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label16" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label16" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hover Font Color :</label>
                            <div class="col-md-4">                                
                                <asp:TextBox runat="server" ID="txtMenuSubHoverFontColor" CssClass="form-control" AutoComplete="off" />
                                <ajaxToolkit:ColorPickerExtender 
                                    ID="ColorPickerExtender17"  
                                    TargetControlID="txtMenuSubHoverFontColor"                                    
                                    Enabled="True"
                                    SampleControlID="Label17" 
                                    runat="server">
                                </ajaxToolkit:ColorPickerExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="Label17" Text="&nbsp;" CssClass="form-control" Width="25" />
                            </div>
                         </div>
                         <div class="form-group">            
                            <div class="col-md-4 col-md-offset-4">
                                <div class="pull-left">
                                    <asp:Button ID="lnkSave" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary submit fsMain lnkSave" ToolTip="Click here to save changes" OnClick="lnkSave_Click"></asp:Button>
                                    <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" Visible="false" />
                                </div>
                            </div>
                         </div>
                          <br /><br />  

                        </div>  
                    </fieldset>
                </asp:Panel>

        </Content>
    </uc:Tab>                
</asp:Content>
