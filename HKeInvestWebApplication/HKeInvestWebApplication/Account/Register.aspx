<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HKeInvestWebApplication.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new client account</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" EnableClientScript="false"  ValidationGroup="Page"/>
        <div class="form-group">
        <asp:Label runat="server" Text="First Name" AssociatedControlID="FirstName" ClientIDMode="Inherit" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="FirstName" runat="server" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="FirstName" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="First Name is required." ValidationGroup="Page"></asp:RequiredFieldValidator>
        </div>
        <asp:Label runat="server" Text="Last Name" Font-Overline="False" AssociatedControlID="LastName" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="LastName" runat="server" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="LastName" CssClass="text-danger" Text="*" EnableClientScript="false" Display="Dynamic" runat="server" ErrorMessage="Last Name is required." ValidationGroup="Page"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="lastName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The last name must contain at least one alpahbet" ForeColor="Red" ValidationExpression="^.*[a-zA-Z]+.*$">*</asp:RegularExpressionValidator>
</div>
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="Account#" AssociatedControlID="AccountNumber" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="AccountNumber" runat="server" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="AccountNumber" CssClass="text-danger" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Account Number is required." ValidationGroup="Page"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="AccountNumber" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the account number  is invalid" ForeColor="Red" ValidationExpression="^[A-Z]{2}[0-9]{8}$">*</asp:RegularExpressionValidator>
        <asp:CustomValidator ID="cvAccountNumber" ValidateEmptyText="true" ControlToValidate="AccountNumber" CssClass="text-danger" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="The account number does not match the client's last name." OnServerValidate="cvAccountNumber_ServerValidate" ValidationGroup="Page"></asp:CustomValidator>
        </div>
        <asp:Label runat="server" Text="HKID/Passport#" AssociatedControlID="HKID" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="HKID" runat="server" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="HKID" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="A HKID or Passport number is required." ValidationGroup="Page"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="HKID" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of HKID is invalid" ForeColor="Red" ValidationExpression="^[0-9A-Z]{8}$">*</asp:RegularExpressionValidator>
    </div>
        </div>

    <div class="form-group">
        <asp:Label  runat="server" Text="Date Of Birth" AssociatedControlID="DateOfBirth" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="DateOfBirth" runat="server" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="DateOfBirth" CssClass="text-danger" Text="*" EnableClientScript="false" Dysplay="Dynamic" runat="server" ErrorMessage="Date of Birth is required." ValidationGroup="Page"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="DateOfBirth" CssClass="text-danger" ValidationExpression="^(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Date of Birth is not validate." ValidationGroup="Page"></asp:RegularExpressionValidator>
        </div>
        <asp:Label runat="server" Text="Email" AssociatedControlID="Email" CssClass="label-control col-md-2"></asp:Label >
        <div class="col-md-4">
        <asp:TextBox ID="Email" runat="server" TextMode="Email" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="Email" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Email address is required." Text="*" ValidationGroup="Page"></asp:RequiredFieldValidator>
    </div>
        </div>

    <div class="form-group">
        <asp:Label  runat="server" Text="User Name" AssociatedControlID="UserName" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="UserName" runat="server" MaxLength="10" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="UserName" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*"  runat="server" ErrorMessage="User Name is required."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="UserName" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ValidationExpression="^.{6,10}$" runat="server" ErrorMessage="User Name must be between 6 and 10 characters." ValidationGroup="Page"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ControlToValidate="UserName" CssClass="text-danger" EnableClientScript="false" ValidationExpression="^[a-zA-Z0-9]+$" Text="*" Display="Dynamic" runat="server" ErrorMessage="User Name must contain only letters and digits." ValidationGroup="Page"></asp:RegularExpressionValidator>
    </div>
        </div>

    <div class="form-group">
        <asp:Label  runat="server" Text="Password" AssociatedControlID="Password" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="Password" runat="server" TextMode="Password" MaxLength="15" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="Password" CssClass="text-danger" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Password isrequired."  ValidationGroup="Page"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="Password"  CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ValidationExpression="^.{8,15}$" runat="server" ErrorMessage="Password must be between 8 and 15 characters." ValidationGroup="Page"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ControlToValidate="Password" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ValidationExpression="^(.*?[^0-9a-zA-Z]){2}.*$" runat="server" ErrorMessage="Password must contain at least 2 nonalphanumeric characters."  ValidationGroup="Page"></asp:RegularExpressionValidator>
        </div>
        <asp:Label  runat="server" Text="Confirm Password" AssociatedControlID="ConfirmPassword" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" ValidationGroup="Page"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" Text="*" Display="Dynamic" EnableClientScript="false" CssClass="text-danger" runat="server" ErrorMessage="Confirm Password is required."  ValidationGroup="Page"></asp:RequiredFieldValidator>
        <asp:CompareValidator ControlToValidate="ConfirmPassword" ControlToCompare="Password" ValidateEmptyText="false" CssClass="text-danger" EnableClientScript="false" Text="*" Display="Dynamic" runat="server" ErrorMessage="Password and Confirm Password do not match." ValidationGroup="Page"></asp:CompareValidator>
    </div>
    </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button Height="34px" runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" ValidationGroup="Page"/>
            </div>
        </div>
    </div>
</asp:Content>
