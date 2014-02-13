<%@ Page language="c#" %>
<script runat=server>
		private void Page_Load(object sender, System.EventArgs e)
		{
			//first check to see if any exceptions occurred
			string ProcessName = Request.QueryString["process"];
			if( Session[ProcessName + "_Exception" ] != null )
			{
				phException.Visible = true;
				phStatus.Visible = false;
				//if an exception occurred, display it.
				Exception ex = (Exception)Session[ProcessName + "_Exception" ];
				litException.Text =  "<pre>" + ex.ToString() + "</pre>";
			}
		}
</script>

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">

			<SCRIPT language="javascript">
function Update( statusLine, statusWidth, percentComplete ){	
	currentItem.innerText = statusLine;
	amountComplete.innerText = percentComplete;
	if( statusWidth > 0 ){
		statusBar.width = statusWidth;
		statusBar.style.visibility='visible';
	}
	if (percentComplete=='100%')
	{
		//parent.opener.document.location.reload();
		setTimeout('parent.close()',2000);
	}
}
			</SCRIPT>
	</head>
	<body>
		<div align="center">
			<asp:PlaceHolder ID="phException" Visible="False" Runat="server">Exception occurred: 
			<asp:Literal id="litException" Runat="server"></asp:Literal></asp:PlaceHolder>
			<SPAN id="currentItem" align="center"></SPAN>
			<asp:PlaceHolder ID="phStatus" Runat="server">
				<TABLE class="StatusHolder" width="493" align="center" border="0">
					<TR>
						<td>
							<TABLE class="Status" id="statusBar" style="VISIBILITY: hidden" height="20" cellspacing="0"
								cellPadding="0" width="0" border="0">
								<TR>
									<td>&nbsp;</td>
								</tr>
							</TABLE>
						</td>
					</tr>
				</TABLE>
			</asp:PlaceHolder>
			<SPAN id="amountComplete" />
		</div>
	</body>
</html>
