<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="HKeInvestWebApplication.RegistrationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" EnableViewState="True">
    <asp:ValidationSummary CssClass="text-danger" DisplayMode="BulletList" EnableClientScript="false" ShowSummary="true" runat="server" />
    <h4>Create a new login account</h4>
    <div class="form-horizontal">
    <div class="form-group">
        <asp:Label runat="server" Text="First Name" AssociatedControlID="FirstName" ClientIDMode="Inherit" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="FirstName" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="FirstName" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="First Name is required."></asp:RequiredFieldValidator>
        </div>
        <asp:Label runat="server" Text="Last Name" Font-Overline="False" AssociatedControlID="LastName" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="LastName" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="LastName" CssClass="text-danger" Text="*" EnableClientScript="false" Display="Dynamic" runat="server" ErrorMessage="Last Name is required."></asp:RequiredFieldValidator>
</div>
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="Account#" AssociatedControlID="AccountNumber" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="AccountNumber" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="AccountNumber" CssClass="text-danger" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Account Number is required."></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="cvAccountNumber" ValidateEmptyText="true" ControlToValidate="AccountNumber" CssClass="text-danger" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="The account number does not match the client's last name." OnServerValidate="cvAccountNumber_ServerValidate"></asp:CustomValidator>
        </div>
        <asp:Label runat="server" Text="HKID/Passport#" AssociatedControlID="HKID" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="HKID" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="HKID" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="A HKID or Passport number is required."></asp:RequiredFieldValidator>
    </div>
        </div>

    <div class="form-group">
        <asp:Label  runat="server" Text="Date Of Birth" AssociatedControlID="DateOfBirth" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="DateOfBirth" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="DateOfBirth" CssClass="text-danger" Text="*" EnableClientScript="false" Dysplay="Dynamic" runat="server" ErrorMessage="Date of Birth is required."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="DateOfBirth" CssClass="text-danger" ValidationExpression="dd/mm/yyyy" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Date of Birth is not validate."></asp:RegularExpressionValidator>
        </div>
        <asp:Label runat="server" Text="Email" AssociatedControlID="Email" CssClass="label-control col-md-2"></asp:Label >
        <div class="col-md-4">
        <asp:TextBox ID="Email" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="Email" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Email address is required." Text="*"></asp:RequiredFieldValidator>
    </div>
        </div>

    <div class="form-group">
        <asp:Label  runat="server" Text="User Name" AssociatedControlID="UserName" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="UserName" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="UserName" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*"  runat="server" ErrorMessage="User Name is required."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="UserName" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ValidationExpression="^.{6,}$" runat="server" ErrorMessage="User Name must contain at least 6 characters."></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ControlToValidate="UserName" CssClass="text-danger" EnableClientScript="false" ValidationExpression="^[a-zA-Z0-9]+$" Text="*" Display="Dynamic" runat="server" ErrorMessage="User Name must contain only letters and digits."></asp:RegularExpressionValidator>
    </div>
        </div>

    <div class="form-group">
        <asp:Label  runat="server" Text="Password" AssociatedControlID="Password" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="Password" runat="server" TextMode="Password" MaxLength="15" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="Password" CssClass="text-danger" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Password isrequired."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="Password"  CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ValidationExpression="^.{8,}$" runat="server" ErrorMessage="Password must contain at least 8 characters."></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ControlToValidate="Password" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ValidationExpression="^[^(a-zA-Z0-9)]{2,}$" runat="server" ErrorMessage="Password must contain at least 2 nonalphanumeric characters."></asp:RegularExpressionValidator>
        </div>
        <asp:Label  runat="server" Text="Confirm Password" AssociatedControlID="ConfirmPassword" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" Text="*" Display="Dynamic" EnableClientScript="false" CssClass="text-danger" runat="server" ErrorMessage="Confirm Password is required."></asp:RequiredFieldValidator>
        <asp:CompareValidator ControlToValidate="ConfirmPassword" ControlToCompare="Password" ValidateEmptyText="false" CssClass="text-danger" EnableClientScript="false" Text="*" Display="Dynamic" runat="server" ErrorMessage="Password and Confirm Password do not match."></asp:CompareValidator>
    </div>
    </div>

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
        <asp:Button ID="Register" runat="server" Text="Register" CssClass="btn button-default" Height="34px" />
        </div>
      
    </div>

 </div>
</asp:Content>
