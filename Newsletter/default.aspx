<%@ Page Language="c#" Inherits="i386.Newsletter.Login" CodeFile="default.aspx.cs"
    MasterPageFile="~/MasterPage.master" Title="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="loginbox">
        <img src="images/buttons/newsletters.gif" align="left"/><h2>
            Newsletter Sign-in</h2>
        <dl>
            <dt>
                <asp:Literal runat="server" ID="ltrlUserName" Text="<%$ Resources:labels, username%>" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="UserID"
                    Text="*" />
            </dt>
            <dd>
                <asp:TextBox ID="UserID" runat="server" Width="90%" />
            </dd>
            <dt>
                <asp:Literal runat="server" ID="ltrlPwd" Text="<%$ Resources:labels, password%>" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="Password"
                    Text="*" />
            </dt>
            <dd>
                <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="90%" />
            </dd>
        </dl>
        <p align="right">
            <asp:Button ID="Signin" runat="server" Text="&raquo; Sign-in" OnClick="Signin_Click"
                Width="100" /></p>
    </div>
    <input name="ForwardUrl" type="hidden" value="MainMenu.aspx" id="ForwardUrl" runat="server">
</asp:Content>
