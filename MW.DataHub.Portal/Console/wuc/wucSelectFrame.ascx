<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucSelectFrame.ascx.cs"
    Inherits="MW.DataHub.Portal.Console.wuc.wucSelectFrame" %>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/css/Common.css" />

<script type="text/javascript">
var ID=<%=ID %>;
$(document).ready(function(){
    $( "#tabs" ).tabs();
    if(ID<=0)
    {
         $( "#tabs" ).tabs( "option", "disabled", [1,2,3,4] );
         $("#tabs li:eq(0)").click(function(){
             tabsSelect(this);
         });
    }
    else{
         $("#tabs li").click(function(){
             tabsSelect(this);
         });
    }
    
    
})
function tabsSelect(e)
{
    switch($(e).text())
    {
        case "Project":
            $("iframe[id*='iframe1']").attr("src","../../Console/ProjectProfile.aspx?ProjectID="+ID);
        break;
        case "Task":
            $("iframe[id*='iframe1']").attr("src","../../Console/TasksManager.aspx?ProjectID="+ID);
        break;
        case "Changing":
            $("iframe[id*='iframe1']").attr("src","../../Console/ChangeManager.aspx?ProjectID="+ID);
        break;
        case "Log":
            $("iframe[id*='iframe1']").attr("src","../../Console/ProjectLog.aspx?ProjectID="+ID);
        break;
        case "Notification":
            $("iframe[id*='iframe1']").attr("src","../../Console/ProjectNotification.aspx?ProjectID="+ID);
        break;
    }
}
function urlChange1(url)
{
     $("iframe[id*='iframe1']").attr("src",url);
}
</script>
<style type="text/css">
    </style>
<div id="tabs">
  <ul style="height:35px;">
    <li><a href="#fragment-1" style="height:15px;" ><span  style="font-size:14px;font-family:Arial">Project</span></a></li>
    <li><a href="#fragment-1" style="height:15px;" ><span style="font-size:14px;font-family:Arial">Task</span></a></li>
    <li><a href="#fragment-1" style="height:15px;" ><span style="font-size:14px;font-family:Arial">Changing</span></a></li>
    <li><a href="#fragment-1" style="height:15px;" ><span style="font-size:14px;font-family:Arial">Log</span></a></li>
    <li><a href="#fragment-1" style="height:15px;" ><span style="font-size:14px;font-family:Arial">Notification</span></a></li>
  </ul>
  <div id="fragment-1">
  <div>
     <iframe id="iframe1" frameborder="0" enableviewstate="false"  width="100%" height="600"
            src="../../Console/ProjectProfile.aspx?ProjectID=<%=ID %>" name="Iframe30"></iframe>
            </div>
  </div>
  </div>


