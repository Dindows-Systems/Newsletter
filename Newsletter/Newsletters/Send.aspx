<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Send.aspx.cs" Inherits="Newsletters_Send" Title="View Newsletter" %>

<%@ Register Src="../Controls/ViewNewsletter.ascx" TagName="ViewNewsletter" TagPrefix="uc1" %>
<%@ Register TagPrefix="eo" Namespace="EO.Web" Assembly="EO.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <h2>
        Send Newsletter</h2>
    <eo:ProgressBar runat="server" ID="ProgressBar1" ShowPercentage="True" IndicatorImage="00060304"
        BackgroundImageRight="00060103" ControlSkinID="None" BackgroundImage="00060101"
        IndicatorIncrement="7" BackgroundImageLeft="00060102" Width="100%" StartTaskButton="btnStart"
        StopTaskButton="btnStop" ClientSideOnValueChanged="OnProgress" OnRunTask="ProgressBar1_RunTask"
        Height="61px"></eo:ProgressBar>
    <div id="divStatus">
    </div>
    <asp:Label runat="server" ID="lblErrorMsg" />
    <div align="right">
        <asp:Button ID="btnStart" runat="server" Text="Send" />
        &nbsp;
        <asp:Button ID="btnStop" runat="server" Text="Abort" />
    </div>
    <div align="left">
        <asp:LinkButton ID="lnkSendTest" runat="server" Text="Send Test" OnClick="lnkSendTest_Click" />
    </div>
    <p>
        <uc1:ViewNewsletter ID="ViewNewsletter1" runat="server" />
    </p>
</asp:Content>
