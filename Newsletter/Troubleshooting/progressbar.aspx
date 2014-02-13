<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="progressbar.aspx.cs" Inherits="Troubleshooting_progressbar" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%@ Register TagPrefix="eo" NameSpace="EO.Web" Assembly="EO.Web" %>
<script type="text/javascript">
function OnProgress(progressBar)
{
	var extraData = progressBar.getExtraData();
	if (extraData)
	{
		var div = document.getElementById("divStatus");
		div.innerHTML = extraData;
	}
}
</script>
<p>
	EO.Web ProgressBar can be associated with a server side long running task and 
	updates the UI when the server side task progresses.
</p>
<p>
	In order to associate a server side task with a ProgressBar, run the task 
	inside the ProgressBar's <b>RunTask</b> event and start the task either by 
	setting <b>StartTaskButton</b> or calling <b>startTask</b> client side method 
	on the client side ProgressBar object.
</p>
<p>
	Inside <b>RunTask</b>, you should frequently call <b>ProgressTaskEventArgs.UpdateProgress</b>
	to report the current progress of your task. The ProgressBar control transfers 
	this information to the client to update the progress.
</p>
<p>
	The following sample demonstrates how to use this feature. It performs a long 
	server side task that does nothing but calling System.Threading.Thread.Sleep. 
	You can switch to the code tab to view the related source code.
</p>
<p>
	For more information, please visit online documentation EO.Web.ProgressBar 
	-&gt; Moving ProgressBar (Server Side).
</p>
<eo:ProgressBar runat="server" id="ProgressBar1" ShowPercentage="True" IndicatorImage="00060104"
	BackgroundImageRight="00060103" ControlSkinID="None" BackgroundImage="00060101" IndicatorIncrement="7"
	BackgroundImageLeft="00060102" Width="300px" StartTaskButton="btnStart" StopTaskButton="btnStop"
	ClientSideOnValueChanged="OnProgress"></eo:ProgressBar>
<br>
<asp:LinkButton id="btnStart" runat="server">Start</asp:LinkButton>
&nbsp;
<asp:LinkButton id="btnStop" runat="server">Stop</asp:LinkButton>
<div id="divStatus"></div>

</asp:Content>

