<%@ Page Language="c#" Inherits="i386.Newsletter.Batch" CodeFile="Batch.aspx.cs"  MasterPageFile="~/MasterPage.master" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
		<h2>Batch Process</h2>
			<asp:RadioButtonList id="Action" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Value="Import" Selected="True">Import into lists</asp:ListItem>
								<asp:ListItem Value="Remove">Remove from lists</asp:ListItem>
							</asp:RadioButtonList>
				<table class="frame" cellspacing="0" cellPadding="5" width="800" border="0">
					<tr valign="top">
						<td class="formtitle" colSpan="3">
						</td>
					</tr>
					<TR>
						<td class="formdata" width="300">Newsletter Lists:<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="* Please select a list"
								ControlToValidate="Listbox1" /><br>
							<asp:listbox id="Listbox1" SelectionMode="Multiple" Width="336px" Rows="5" size="30" Runat="server"></asp:listbox></td>
						<td class="formdata" colspan="2">Field Arrangement
							<asp:radiobuttonlist id="Layout" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Value="1">Email,Name</asp:ListItem>
								<asp:ListItem Value="2" Selected="True">Name,Email</asp:ListItem>
								<asp:ListItem Value="3">Email</asp:ListItem>
							</asp:radiobuttonlist>Seperator
							<asp:radiobuttonlist id="SepartorChar" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Value=";" Selected="True">Semi-colon (csv)</asp:ListItem>
								<asp:ListItem Value="	">Tab</asp:ListItem>
								<asp:ListItem Value=",">Comma</asp:ListItem>
								
							</asp:radiobuttonlist>
							<div align="right"><asp:button id="Button1" runat="server" Text="Validate Addresses" onclick="Button1_Click"></asp:button></div>
						</td>
					</tr>
					<tr>
						<td class="formdata" colSpan="3">
							<P>Email Addresses:
								<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="* There must  email addresses below in the box below!"
									ControlToValidate="emails" />
								<br>
								<asp:textbox id="emails" Width="100%" cols=100 Rows=15 Runat="server" Height="200px" TextMode="MultiLine"/><BR>
								<EM>Cut'n'Paste Email addresses into the box.</EM></P>
						</td>
					</tr>
					<tr>
						<td class="formtitle" style="WIDTH: 259px"><span class="texterror"><strong>Invalid Email 
									Addresses:
									<asp:label id="Label_Invalid" Runat="server"></asp:label></strong></span><br>
							<asp:listbox id="ListInvalid" Width="300px" Runat="server" Visible="False"></asp:listbox>
						</td>
						<td class="formtitle">Valid Email Addresses:
							<asp:label id="Label_Valid" Runat="server"></asp:label><BR>
							<asp:ListBox ID="ListValid" Runat="server" Width="300px" Visible="False" />
						</td>
						<td class="formtitle" valign="bottom" align="right"><asp:button id="Progress" runat="server" Text="Process Now" Visible="False" onclick="AddEmails_Click"></asp:button></td>
					</tr>
				</table>
				</asp:content>
