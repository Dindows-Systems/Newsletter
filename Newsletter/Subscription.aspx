<%@ Page language="c#" Inherits="i386.Newsletter.Subscribe" CodeFile="Subscription.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server">
	</head>
	<body class="subform">
		<form id="Form1" method="post" runat="server">
			<div align="center">
				<TABLE id="SubscriptionForm" runat="server" cellspacing="0" cellPadding="0" align="center"
					border="0">
					<tr>
						<td colspan="2" align="center" class="subscription"><asp:label id="Label_Error" runat="server" /><asp:Label ID="Label_ListName" Runat="server" cssclass="subscription" />
							Newsletter<br>
						</td>
					</tr>
					<tr>
						<td colspan="2"><strong><asp:Literal runat="server" ID="ltrlName" Text="<%$ Resources:labels, name%>" /></strong><br>
							<asp:textbox id="Name" runat="server" Width="100%"/>
							<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="Name" ForeColor="red" Text="<%$ Resources:labels, requirefield %>" /></td>
					</tr>
					<tr>
						<td colspan="2"><strong><asp:Literal runat="server" ID="ltrlEmail" Text="<%$ Resources:labels, email%>" /></strong> <asp:RegularExpressionValidator runat="server" ControlToValidate="Email" ValidationExpression=".*@.*\..*" ForeColor="red"
								display="dynamic" ID="Regularexpressionvalidator3" ErrorMessage="*" /><br>
							<asp:textbox id="Email" runat="server" Width="100%"/>
							<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="Email" ForeColor="red"  Text="<%$ Resources:labels, requirefield %>" /> 
						
						</td>
					</tr>
					<tr>
						<td colspan="2" valign="top"><asp:Literal runat="server" ID="ltrlFormat" Text="<%$ Resources:labels, format%>" />
							<asp:radiobuttonlist id="FormatList" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist>
							<div align="right"><asp:button id="Send" runat="server" Text="»" Width="50px" onclick="Send_Click"></asp:button></div>
							<asp:checkbox id="Unsubscribe" runat="server"></asp:checkbox>
						</td>
					</tr>
				</TABLE>
				<TABLE class="formdata" id="TableFriends" cellspacing="0" cellPadding="0" align="center"
					border="0" runat="server">
					<tr>
						<td colSpan="2"><STRONG>Share this with a friend</STRONG> by completing the form 
							below</td>
					</tr>
					<tr>
						<td width="50%">Name
						</td>
						<td width="50%">Email Address
						</td>
					</tr>
					<tr>
						<td width="50%"><asp:textbox id="Name1" runat="server"/>&nbsp;
						</td>
						<td width="50%"><asp:textbox id="Email1" runat="server"/></td>
					</tr>
					<tr>
						<td width="50%"><asp:textbox id="Name2" runat="server"/>&nbsp;
						</td>
						<td width="50%"><asp:textbox id="Email2" runat="server"/>&nbsp;
						</td>
					</tr>
					<tr>
						<td width="50%"><asp:textbox id="Name3" runat="server"/>&nbsp;
						</td>
						<td width="50%"><asp:textbox id="Email3" runat="server"/>&nbsp;
						</td>
					</tr>
					<tr>
						<td align="center" colSpan="3"><BR>
							<B>Send a Personal Message</B><BR>
							<asp:textbox id="Message" runat="server" Width="263px" Height="80px" TextMode="MultiLine"/>&nbsp;
							<div align="right">
								<asp:Button id="Submit" runat="server" Text="Submit" onclick="Submit_Click"></asp:Button></div>
						</td>
					</tr>
				</TABLE>
				<table cellspacing="0" cellPadding="0" align="center" border="0" runat="server" id="SubscriptionMessages">
					<tr>
						<td align="center">
							<asp:Label Runat="server" ID="Label_Unsubscribe"  Visible="False" />
							<asp:Label Runat="server" ID="Label_Subscribe"  Visible="False" />
							<br>
							<BR>
							
<script language=javascript><!--
if (parent.name!=null)
{
							document.write('<a href="#" onclick="javascript:self.close();">
							[ Close Window ]</a>');
							}
							//--></script>
						</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</html>
