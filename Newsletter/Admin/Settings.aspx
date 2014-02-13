<%@ Page Language="c#" Inherits="i386.Newsletter.Config" CodeFile="Settings.aspx.cs"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Global Settings</h2>
    <asp:ImageButton ID="Save" runat="server" ImageUrl="~/images/buttons/save.gif" OnClick="Save_Click1" />
    <dl>
        <dt>Company Name</dt>
        <dd>
            <asp:TextBox ID="CompanyName" runat="server" Width="90%" />
        </dd>
        <dt>SMTP Mail Server</dt>
        <dd>
            <asp:TextBox ID="SMTPServer" runat="server" Width="90%" />
        </dd>
        <dt>Application URL</dt>
        <dd>
            <asp:TextBox ID="ApplicationURL" runat="server" Width="90%" />
        </dd>
        <dt>Subscribe Subject*</dt>
        <dd>
            <asp:TextBox ID="SubscribeSubject" runat="server" Width="90%" />
        </dd>
        <dt>Subscribe Body*</dt>
        <dd>
            <asp:TextBox ID="SubscribeBody" runat="server" Height="144px" Width="90%" TextMode="MultiLine" />
        </dd>
         <dt>Unsubscribe Subject*</dt>
        <dd>
            <asp:TextBox ID="UnsubscribeSubject" runat="server" Width="90%" />
        </dd>
        <dt>Unsubscribe Body*</dt>
        <dd>
            <asp:TextBox ID="UnsubscribeBody" runat="server" Height="144px" Width="90%" TextMode="MultiLine" />
        </dd>
    </dl>
    * Current not used in this version!
</asp:Content>
