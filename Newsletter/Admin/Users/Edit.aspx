<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Admin_Users_Edit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>Edit User</h2>
<asp:datalist id="DataList1" runat="server" OnDeleteCommand="DataList1_DeleteCommand" OnItemCreated="DataList1_ItemCreated" OnUpdateCommand="DataList1_UpdateCommand">
					<EditItemTemplate>
						<table class="frame" cellspacing="0" cellPadding="5" width="800" border="0">
							<tr>
								<td class="toolbar" colSpan="2">
									<asp:imagebutton id="Update" runat="server" ImageUrl="~/images/buttons/save.gif" CommandName="Update" />
									<asp:imagebutton id="Delete" runat="server" CommandName="Delete" imageurl="~/images/buttons/delete.gif" /></td>
							</tr>
							<tr>
								<td class="formtitle" valign="top" width="100">
								    <asp:Literal ID="ltrlName" runat="server" Text="<%$ Resources:labels, name%>" />
								</td>
								<td class="formdata" width="700">
									<asp:TextBox id=Name runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' width="100%">
									</asp:TextBox><BR>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator2" runat="server" ControlToValidate="Name" ErrorMessage="* Please provide name for the List" /></td>
							</tr>
							<tr valign="top">
								<td class="formtitle" width="100">
								    <asp:Literal ID="ltrlEmail" runat="server" Text="<%$ Resources:labels, email%>" /></td>
								<td class="formdata" width="700">
									<asp:TextBox id=Email runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %>' width="100%">
									</asp:TextBox><BR>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator3" runat="server" ControlToValidate="Email" ErrorMessage="* Please provide a valid email address" /><BR>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator1" runat="server" ControlToValidate="Email" ErrorMessage="* Please use a valid email address"
										 display="dynamic"  ValidationExpression=".*@.*\..*" /><BR>
								</td>
							</tr>
							<tr valign="top">
								<td class="formtitle" width="100"><asp:Literal ID="ltrlUserName" runat="server" Text="<%$ Resources:labels, username%>" /></td>
								<td class="formdata" width="700">
									<asp:TextBox id="UserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName") %>' width="100%">
									</asp:TextBox><BR>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator4" runat="server" ControlToValidate="UserName" ErrorMessage="* Please provide a username"
										 />
								</td>
							</tr>
							<tr valign="top">
								<td class="formtitle" width="100">
								<asp:Literal ID="ltrlPassword" runat="server" Text="<%$ Resources:labels, Password%>" /></td>
								<td class="formdata" width="700">
									<asp:TextBox id="Password" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Password") %>' width="100%">
									</asp:TextBox><BR>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator1" runat="server" ControlToValidate="Password" ErrorMessage="* Please provide a password"  />
								</td>
							</tr>
							<tr valign="top">
								<td class="formtitle" width="100"><asp:Literal ID="ltrlLists" runat="server" Text="<%$ Resources:labels, lists%>" /></td>
								<td class="formdata" width="700">
									<P>
										<asp:ListBox id="Listbox1" Width="336px" Rows="5" SelectionMode="Multiple" Runat="server"></asp:ListBox><BR>
										<asp:RequiredFieldValidator id="Val2" runat="server" ControlToValidate="Listbox1" ErrorMessage="* Please select a Distribution List"
											 /></P>
									
								</td>
							</tr>
							<tr valign="top">
								<td class="formtitle" width="100"><asp:Literal ID="ltrlRoles" runat="server" Text="<%$ Resources:labels, role%>" /></td>
								<td class="formdata" width="700">
									<asp:DropDownList id="ListOfRoles" runat="server">
										<asp:ListItem Value="User">User</asp:ListItem>
										<asp:ListItem Value="Admin">Admin</asp:ListItem>										
									</asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="formtitle" valign="top" width="100"><asp:Literal ID="ltrlDesc" runat="server" Text="<%$ Resources:labels, description%>" /></td>
								<td class="formdata" width="700">
									<asp:TextBox id=Description runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' width="100%" Height="79px" TextMode="MultiLine">
									</asp:TextBox><BR>
								</td>
							</tr>
																		<tr>
							<td class="formdata" colspan=2> <asp:Literal ID="ltrlDateCreated" runat="server" Text="<%$ Resources:labels, DateCreated%>" /><%# DataBinder.Eval(Container.DataItem, "Created") %> by <%# DataBinder.Eval(Container.DataItem, "CreatedBy") %></td>
							</tr>
						</table>						
					</EditItemTemplate>
				</asp:datalist>
</asp:Content>

