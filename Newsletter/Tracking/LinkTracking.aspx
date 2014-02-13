<%@ Page language="c#" Inherits="LinkTracking" CodeFile="LinkTracking.aspx.cs"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server" />

	<body topmargin=0 leftmargin=0 class="formdata">
		<form id="Form1" method="post" runat="server">
		<table align="center"><tr><td class="formdata">
			<strong>Link Tracker</strong>
			<br>
			Link to track:<asp:TextBox id="Url" runat="server" Width="400px"/>
			<asp:Button ID="Add" Runat="server" Text="Generate"></asp:Button>
			<asp:LinkButton ID="Reset" Runat="server">Reset Clicks</asp:LinkButton>
			<div align=center>
			<asp:datagrid id="DataGrid1" Width="700px" runat="server" AutoGenerateColumns="False" AllowSorting="True"
				PageSize="20" AllowPaging="True" >
				<EditItemStyle VerticalAlign="Top"></EditItemStyle>
				<ItemStyle VerticalAlign="Middle" CssClass="dgitem" HorizontalAlign="Center"></ItemStyle>
				<AlternatingItemStyle CssClass="dgalt" Height="30px" VerticalAlign="Middle"></AlternatingItemStyle>
				<HeaderStyle CssClass="dgheader" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
				<PagerStyle Mode="NumericPages" Position="Top" HorizontalAlign="Right" CssClass="toolbar"></PagerStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="Url">
						<HeaderTemplate>
							Link
							<asp:LinkButton CommandName="UrlUp" Runat="server" ID="Linkbutton1">
								<img src="../images/buttons/up.gif" border="0" width="12" height="10" /></asp:LinkButton>
							<asp:LinkButton CommandName="UrlDn" Runat="server" ID="Linkbutton2">
								<img src="../images/buttons/dn.gif" border="0" width="12" height="10" /></asp:LinkButton>
						</HeaderTemplate>
						<ItemTemplate>
							<a href='<%# DataBinder.Eval(Container.DataItem, "URL") %>' target=_blank>
							<%# DataBinder.Eval(Container.DataItem, "URL") %>
							</a>
						</ItemTemplate>
					</asp:TemplateColumn> 
					<asp:TemplateColumn >
						<HeaderTemplate>
							Tracking Link
						</HeaderTemplate>
						<ItemTemplate>
							<asp:PlaceHolder Runat=server ID="LinkBox" />
							<input type=text name='clip<%# DataBinder.Eval(Container.DataItem, "ID") %>' value='<%=i386.Newsletter.App.ConfigSettings.ApplicationURL%>/tracking/url.aspx?l=<%# DataBinder.Eval(Container.DataItem, "ID") %>&amp;url=<%# DataBinder.Eval(Container.DataItem, "URL") %>'/>
							<a href="#" onclick="sendToclipboard(clip<%# DataBinder.Eval(Container.DataItem, "ID") %>.value);">[Copy to Clickboard]</a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Clicks">
						<HeaderTemplate>
							Clicks
							<asp:LinkButton CommandName="ClicksUp" Runat="server" ID="Linkbutton3">
								<img src="../images/buttons/up.gif" border="0" width="12" height="10" /></asp:LinkButton>
							<asp:LinkButton CommandName="ClicksDn" Runat="server" ID="Linkbutton4">
								<img src="../images/buttons/dn.gif" border="0" width="12" height="10" /></asp:LinkButton>
						</HeaderTemplate>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "Clicks") %>
							</a>
						</ItemTemplate>
					</asp:TemplateColumn>

					<asp:TemplateColumn>
						<HeaderTemplate>
							<asp:Literal ID="ltrlDelete" runat="server" Text="<%$ Resources:labels, Delete%>" />
						</HeaderTemplate>
						<ItemTemplate>
							<asp:TextBox Runat=server ID="RecId" Visible=False  Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>' />
							<asp:Imagebutton Runat="server" id="Delete" ImageUrl="~/images/buttons/delete.gif" CommandName="Delete" />
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid></div></td></tr></table>
		</form>
	</body>
</html>
