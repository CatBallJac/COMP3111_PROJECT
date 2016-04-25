<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account Application.aspx.cs" Inherits="HKeInvestWebApplication.Account_Application" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><strong>Account Application Form</strong></h2>
    <a href="Code_File/Application Form.pdf" target="_blank"><span class="auto-style2">Click here to get a pdf of the application form</span></a>
    <style>a:visited{color: gray}a:before{color: blue}</style>
    <hr /><asp:Label ID="applyResult" runat="server" />
    <div class="form-horizontal">
        <hr />
        <h3>1. Account Type</h3>
        <div class="form-group">
            <div class="form-group">
                <asp:DropDownList CssClass="col-md-offset-1" ID="AccountType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AccountType_SelectedIndexChanged">
                    <asp:ListItem Value="0">---Please Select An Account Type---</asp:ListItem>
                    <asp:ListItem Value="individual">Individual</asp:ListItem>
                    <asp:ListItem Value="survivor">Joint Tenants with Rights of Survivorship</asp:ListItem>
                    <asp:ListItem Value="common"> Joint Tenants in Common</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="AccountTypeValidate" runat="server" ControlToValidate="AccountType" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Account type must be selected" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
        </div>
        <hr />
        <h3>2. Client Information</h3>
        <div id="PrimaryClient" class="form-group">
            <div class="form-group col-md-offset-2">
                <h4 class="col-md-offset-1">Primary Account Holder:</h4>
                <asp:DropDownList ID="title" runat="server" CssClass="col-md-offset-1">
                    <asp:ListItem Value="0">---Please Select Your Title---</asp:ListItem>
                    <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                    <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                    <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
                    <asp:ListItem Value="Dr.">Dr.</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="title" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Your title must be selected" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>

	        <div class="form-group">
		        <asp:Label ID="Label1" runat="server" Text="First name:" CssClass="control-label col-md-2" AssociatedControlID="firstName"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="firstName" runat="server" Wrap="False" CssClass="form-input" MaxLength="35"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="firstName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="First name is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
	
		        <asp:Label ID="Label2" runat="server" AssociatedControlID="lastName" Text="Last name:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="lastName" runat="server" Wrap="False" CssClass="form-input" MaxLength="35"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lastName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Last name is required" ForeColor="Red"></asp:RequiredFieldValidator>
		            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="lastName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Last name should contain at least one alphabet" ForeColor="Red" ValidationExpression="^.*[a-zA-Z]+.*$"></asp:RegularExpressionValidator>
		        </div>
	        </div>

            <div class="form-group">
		        <asp:Label ID="Label15" AssociatedControlID="dateOfBirth" runat="server" Text="Date of birth (dd/mm/yyyy):" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="dateOfBirth" runat="server" Wrap="False" CssClass="form-input" MaxLength="10"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dateOfBirth" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Date of birth is required" ForeColor="Red"></asp:RequiredFieldValidator>
		            <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="dateOfBirth" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Date of birth is invalid" ForeColor="Red" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
		            <asp:CustomValidator ID="dateOfBirthValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="dateOfBirthValidate_ServerValidate"></asp:CustomValidator>
		        </div>
	
		        <asp:Label ID="Label3" runat="server" AssociatedControlID="email" Text="Email address:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="email" runat="server" Wrap="False" CssClass="form-input" MaxLength="30" TextMode="Email"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="email" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Email address is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
	        </div>

	        <div class="form-group">
                <asp:Label ID="Label16" runat="server" Text="Home address:" CssClass="control-label col-md-2" Font-Size="Medium"></asp:Label>
            </div>

	        <div class="form-group">
		        <asp:Label ID="Label4" runat="server" AssociatedControlID="building" Text="Building (if any):" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="building" runat="server" Wrap="False" CssClass="form-input" MaxLength="50"></asp:TextBox>
		        </div>
	
		        <asp:Label ID="Label5" runat="server" AssociatedControlID="street" Text="Street:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="street" runat="server" Wrap="False" CssClass="form-input" MaxLength="35"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="street" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Street is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
            </div>

	        <div class="form-group">
		        <asp:Label ID="Label6" runat="server" AssociatedControlID="district" Text="District:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="district" runat="server" Wrap="False" CssClass="form-input" MaxLength="19"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="district" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="District is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
	        </div>

	        <div class="form-group">
	        	<asp:Label ID="Label7" runat="server" AssociatedControlID="homePhone" Text="Home phone:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	    <asp:TextBox ID="homePhone" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
	                <asp:CustomValidator ID="homePhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="PhoneValidate_ServerValidate"></asp:CustomValidator>
	        	    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="homePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Home phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	</div>
	
	        	<asp:Label ID="Label8" runat="server" AssociatedControlID="homeFax" Text="Home fax:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	    <asp:TextBox ID="homeFax" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
	                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="homeFax" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Home fax is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	</div>
	        </div>

	        <div class="form-group">
	        	<asp:Label ID="Label9" runat="server" AssociatedControlID="businessPhone" Text="Business phone:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	    <asp:TextBox ID="businessPhone" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
                	
	                <asp:CustomValidator ID="businessPhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="PhoneValidate_ServerValidate"></asp:CustomValidator>
                	
	        	    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="businessPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Business phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
                	
	        	</div>
	
		        <asp:Label ID="Label10" runat="server" AssociatedControlID="mobilePhone" Text="Mobile phone:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	    <asp:TextBox ID="mobilePhone" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
                	<asp:CustomValidator ID="mobilePhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="PhoneValidate_ServerValidate"></asp:CustomValidator>
	        	    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="mobilePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Mobile phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	</div>
	        </div>

	        <div class="form-group">
	        	<asp:Label ID="Label11" runat="server" AssociatedControlID="citizenship" Text="Country of citizenship:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	    <asp:TextBox ID="citizenship" runat="server" Wrap="False" CssClass="form-input" MaxLength="70"></asp:TextBox>
                	<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="citizenship" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country of citizenship is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
	
	        	<asp:Label ID="Label12" runat="server" AssociatedControlID="legalResidence" Text="Country of legal residence:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	    <asp:TextBox ID="legalResidence" runat="server" Wrap="False" CssClass="form-input" MaxLength="70"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="legalResidence" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country of legal residence is required" ForeColor="Red"></asp:RequiredFieldValidator>
	        	</div>
	        </div>

            <div class="form-group">

                <asp:CheckBox AutoPostBack="true" ID="HKIDUsed" runat="server" Text="Do you use HKID for this application?" CssClass="col-md-offset-1" OnCheckedChanged="HKIDUsed_CheckedChanged" />

            </div>

	        <div class="form-group">
	        	<asp:Label ID="Label13" runat="server" AssociatedControlID="HKID" Text="HKID/passport number:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	            	<asp:TextBox ID="HKID" runat="server" Wrap="False" CssClass="form-input" MaxLength="8"></asp:TextBox>
                	<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="HKID" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="HKID/passport number is required" ForeColor="Red"></asp:RequiredFieldValidator>
	        	    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="HKID" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of HKID is invalid" ForeColor="Red" ValidationExpression="^[0-9A-Z]{8}$"></asp:RegularExpressionValidator>
	        	    <asp:CustomValidator ID="HKIDValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="HKIDValidate_ServerValidate"></asp:CustomValidator>
	        	</div>
	
		        <asp:Label ID="Label14" runat="server" AssociatedControlID="passportCountry" Text="Passport country of issue:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="passportCountry" runat="server" Wrap="False" CssClass="form-input" MaxLength="70"></asp:TextBox>
	                <asp:CustomValidator ID="passportCountryOfIssueValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="passportCountryOfIssueValidate_ServerValidate" ErrorMessage="Passport country of issue is required."></asp:CustomValidator>
		        </div>
	        </div>
        </div>

        <div id="CoClient" class="form-group" runat="server" visible="false">
            <div class="form-group col-md-offset-2">
                <h4 class="col-md-offset-1">Co-Account Holder:</h4>
                <asp:DropDownList CssClass="col-md-offset-1" ID="title2" runat="server">
                    <asp:ListItem Value="0">---Please Select Your Title---</asp:ListItem>
                    <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                    <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                    <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
                    <asp:ListItem Value="Dr.">Dr.</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="title2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Your title must be selected" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>

	        <div class="form-group">
		        <asp:Label ID="Label18" runat="server" Text="First name:" CssClass="control-label col-md-2" AssociatedControlID="firstName2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="firstName2" runat="server" Wrap="False" CssClass="form-input" MaxLength="35"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="firstName2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="First name is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
	
		        <asp:Label ID="Label19" runat="server" AssociatedControlID="lastName2" Text="Last name:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="lastName2" runat="server" Wrap="False" CssClass="form-input" MaxLength="35"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="lastName2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Last name is required" ForeColor="Red"></asp:RequiredFieldValidator>
		            <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="lastName2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Last name should contain at least one alphabet" ForeColor="Red" ValidationExpression="^.*[a-zA-Z]+.*$"></asp:RegularExpressionValidator>
		        </div>
	        </div>

            <div class="form-group">
		        <asp:Label ID="Label20" AssociatedControlID="dateOfBirth2" runat="server" Text="Date of birth (dd/mm/yyyy):" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="dateOfBirth2" runat="server" Wrap="False" CssClass="form-input" MaxLength="10"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="dateOfBirth2" CssClass="text-danger" Display="Dynamic" ForeColor="Red" EnableClientScript="False" ErrorMessage="Date of birth is required"></asp:RequiredFieldValidator>
		            <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="dateOfBirth2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Date of birth is invalid" ForeColor="Red" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
		            <asp:CustomValidator ID="dateOfBirth2Validate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="dateOfBirth2Validate_ServerValidate"></asp:CustomValidator>
		        </div>
	
		        <asp:Label ID="Label21" runat="server" AssociatedControlID="email2" Text="Email address:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="email2" runat="server" Wrap="False" CssClass="form-input" MaxLength="30" TextMode="Email"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="email2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Email address is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
	        </div>

	        <div class="form-group">
                <asp:Label ID="Label22" runat="server" Text="Home address:" CssClass="control-label col-md-2" Font-Size="Medium"></asp:Label>
            </div>

	        <div class="form-group">
		        <asp:Label ID="Label23" runat="server" AssociatedControlID="building2" Text="Building (if any):" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="building2" runat="server" Wrap="False" CssClass="form-input" MaxLength="50"></asp:TextBox>
		        </div>
	
		        <asp:Label ID="Label24" runat="server" AssociatedControlID="street2" Text="Street:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		            <asp:TextBox ID="street2" runat="server" Wrap="False" CssClass="form-input" MaxLength="35"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="street2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Street is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
            </div>

	        <div class="form-group">
		        <asp:Label ID="Label25" runat="server" AssociatedControlID="district2" Text="District:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		        <asp:TextBox ID="district2" runat="server" Wrap="False" CssClass="form-input" MaxLength="19"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="district2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="District is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
	        </div>

	        <div class="form-group">
	        	<asp:Label ID="Label26" runat="server" AssociatedControlID="homePhone2" Text="Home phone:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	<asp:TextBox ID="homePhone2" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
	                <asp:CustomValidator ID="homePhoneValidate2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="phoneValidator2_ServerValidate"></asp:CustomValidator>
	        	    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="homePhone2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of home phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	</div>
	
	        	<asp:Label ID="Label27" runat="server" AssociatedControlID="homeFax2" Text="Home fax:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	<asp:TextBox ID="homeFax2" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
	                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="homeFax2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of home fax is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	</div>
	        </div>

	        <div class="form-group">
	        	<asp:Label ID="Label28" runat="server" AssociatedControlID="businessPhone2" Text="Business phone:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	<asp:TextBox ID="businessPhone2" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
                	<asp:CustomValidator ID="businessPhoneValidate2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="phoneValidator2_ServerValidate"></asp:CustomValidator>
	        	    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="businessPhone2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of business phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	</div>
	
		        <asp:Label ID="Label29" runat="server" AssociatedControlID="mobilePhone2" Text="Mobile phone:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	<asp:TextBox ID="mobilePhone2" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
                	<asp:CustomValidator ID="mobilePhoneValidate2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="phoneValidator2_ServerValidate"></asp:CustomValidator>
	        	    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="mobilePhone2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of mobile phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	</div>
	        </div>

	        <div class="form-group">
	        	<asp:Label ID="Label30" runat="server" AssociatedControlID="citizenship2" Text="Country of citizenship:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	<asp:TextBox ID="citizenship2" runat="server" Wrap="False" CssClass="form-input" MaxLength="70"></asp:TextBox>
                	<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="citizenship2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country of citizenship is required" ForeColor="Red"></asp:RequiredFieldValidator>
		        </div>
	
	        	<asp:Label ID="Label31" runat="server" AssociatedControlID="legalResidence2" Text="Country of legal residence:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	        	<asp:TextBox ID="legalResidence2" runat="server" Wrap="False" CssClass="form-input" MaxLength="70"></asp:TextBox>
	                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="legalResidence2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country of legal residence is required" ForeColor="Red"></asp:RequiredFieldValidator>
	        	</div>
	        </div>

            <div class="form-group">
                <asp:CheckBox ID="HKIDUsed2" runat="server" Text="Do you use HKID for this application?" CssClass="col-md-offset-1" AutoPostBack="True" OnCheckedChanged="HKIDUsed2_CheckedChanged" />
            </div>

	        <div class="form-group">
	        	<asp:Label ID="Label32" runat="server" AssociatedControlID="HKID2" Text="HKID/passport number:" CssClass="control-label col-md-2"></asp:Label>
	        	<div class="col-md-4">
	            	<asp:TextBox ID="HKID2" runat="server" Wrap="False" CssClass="form-input" MaxLength="8"></asp:TextBox>
                	<asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="HKID2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="HKID/passport number is required" ForeColor="Red"></asp:RequiredFieldValidator>
	        	    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of HKID is invalid" ForeColor="Red" ValidationExpression="^[0-9A-Z]{8}$"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="HKID2Validate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="HKID2Validate_ServerValidate"></asp:CustomValidator>
	        	</div>
	
		        <asp:Label ID="Label33" runat="server" AssociatedControlID="passportCountry2" Text="Passport country of issue:" CssClass="control-label col-md-2"></asp:Label>
		        <div class="col-md-4">
		        <asp:TextBox ID="passportCountry2" runat="server" Wrap="False" CssClass="form-input" MaxLength="70"></asp:TextBox>
	                <asp:CustomValidator ID="passportCountryOfIssue2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Passport country of issue is required" ForeColor="Red" OnServerValidate="passportCountryOfIssue2_ServerValidate"></asp:CustomValidator>
		        </div>
	        </div>
        </div>


            <hr />
            <h3>3. Employment Information</h3>
            <div class="form-group" id="PrimaryClientEmploymentInfo">
                <h4 class="col-md-offset-1">Primary Acccount Holder</h4>
                <div class="form-group">
                    <asp:Label ID="Label17" AssociatedControlID="employmentStatus" runat="server" Text="Employment status:" CssClass="control-label col-md-2"></asp:Label>

                    <asp:DropDownList ID="employmentStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="employmentStatus_SelectedIndexChanged">
                        <asp:ListItem Value="Employed">Employed</asp:ListItem>
                        <asp:ListItem Value="Self-employed">Self-employed</asp:ListItem>
                        <asp:ListItem Value="Retired">Retired</asp:ListItem>
                        <asp:ListItem Value="Student">Student</asp:ListItem>
                        <asp:ListItem Value="Not employed">Not employed</asp:ListItem>
                        <asp:ListItem Value="Homemaker">Homemaker</asp:ListItem>
                    </asp:DropDownList>

                </div>

                <div class="form-group">
                    <asp:Label ID="Label34" runat="server" AssociatedControlID="occupation" Text="Specific occupation:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="occupation" runat="server" Wrap="False" CssClass="form-input" MaxLength="20"></asp:TextBox>
	                    <asp:CustomValidator ID="occupationValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Specific occupation is required" ForeColor="Red" OnServerValidate="occupationValidator_ServerValidate"></asp:CustomValidator>
		            </div>
	
		            <asp:Label ID="Label35" runat="server" AssociatedControlID="years" Text="Years with employer:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="years" runat="server" Wrap="False" CssClass="form-input" MaxLength="2" TextMode="Number"></asp:TextBox>
	                    <asp:CustomValidator ID="yearsValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Years with employer is required" ForeColor="Red" OnServerValidate="yearsValidator_ServerValidate"></asp:CustomValidator>
		                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="years" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the years with employer is invalid" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$"></asp:RegularExpressionValidator>
		            </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label36" runat="server" AssociatedControlID="employerName" Text="Employer name:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="employerName" runat="server" Wrap="False" CssClass="form-input" MaxLength="25"></asp:TextBox>
	                    <asp:CustomValidator ID="employerNameValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Employer name is required" ForeColor="Red" OnServerValidate="employerNameValidator_ServerValidate"></asp:CustomValidator>
	            	</div>
	
		            <asp:Label ID="Label37" runat="server" AssociatedControlID="employerPhone" Text="Employer phone:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="employerPhone" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
	                    <asp:CustomValidator ID="employerPhoneValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Employer phone is required" ForeColor="Red" OnServerValidate="employerPhoneValidator_ServerValidate"></asp:CustomValidator>
		                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="employerPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the employer phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
		            </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label38" runat="server" AssociatedControlID="natureOfBusiness" Text="Nature of business:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="natureOfBusiness" runat="server" Wrap="False" CssClass="form-input" MaxLength="20"></asp:TextBox>
	                    <asp:CustomValidator ID="natureOfBusinessValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Nature of business is required" ForeColor="Red" OnServerValidate="natureOfBusinessValidator_ServerValidate"></asp:CustomValidator>
		            </div>
                </div>
            </div>


            <div class="form-group" id="CoClientEmploymentInfo" runat="server" visible="false">
                <h4 class="col-md-offset-1">Co-Acccount Holder</h4>
                <div class="form-group">
                    <asp:Label ID="Label39" AssociatedControlID="employmentStatus2" runat="server" Text="Employment status:" CssClass="control-label col-md-2"></asp:Label>

                    <asp:DropDownList ID="employmentStatus2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="employmentStatus2_SelectedIndexChanged">
                        <asp:ListItem Value="Employed">Employed</asp:ListItem>
                        <asp:ListItem Value="Self-employed">Self-employed</asp:ListItem>
                        <asp:ListItem Value="Retired">Retired</asp:ListItem>
                        <asp:ListItem Value="Student">Student</asp:ListItem>
                        <asp:ListItem Value="Not employed">Not employed</asp:ListItem>
                        <asp:ListItem Value="Homemaker">Homemaker</asp:ListItem>
                    </asp:DropDownList>

                </div>

                <div class="form-group">
                    <asp:Label ID="Label40" runat="server" AssociatedControlID="occupation2" Text="Specific occupation:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="occupation2" runat="server" Wrap="False" CssClass="form-input" MaxLength="20"></asp:TextBox>
	                    <asp:CustomValidator ID="occupationValidator2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Specific occupation is required" ForeColor="Red" OnServerValidate="occupationValidator2_ServerValidate"></asp:CustomValidator>
		            </div>
	
		            <asp:Label ID="Label41" runat="server" AssociatedControlID="years2" Text="Years with employer:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="years2" runat="server" Wrap="False" CssClass="form-input" MaxLength="2" TextMode="Number"></asp:TextBox>
	                    <asp:CustomValidator ID="yearsValidator2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Years with employer is required" ForeColor="Red" OnServerValidate="yearsValidator2_ServerValidate"></asp:CustomValidator>
		                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="years2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the years with employer is invalid" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$"></asp:RegularExpressionValidator>
		            </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label42" runat="server" AssociatedControlID="employerName2" Text="Employer name:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="employerName2" runat="server" Wrap="False" CssClass="form-input" MaxLength="25"></asp:TextBox>
	                    <asp:CustomValidator ID="employerNameValidator2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Employer name is required" ForeColor="Red" OnServerValidate="employerNameValidator2_ServerValidate"></asp:CustomValidator>
	            	</div>
	
		            <asp:Label ID="Label43" runat="server" AssociatedControlID="employerPhone2" Text="Employer phone:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="employerPhone2" runat="server" Wrap="False" CssClass="form-input" MaxLength="8" TextMode="Phone"></asp:TextBox>
	                    <asp:CustomValidator ID="employerPhoneValidator2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Employer phone is required" ForeColor="Red" OnServerValidate="employerPhoneValidator2_ServerValidate"></asp:CustomValidator>
		                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="employerPhone2" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the employer phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$"></asp:RegularExpressionValidator>
		            </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label44" runat="server" AssociatedControlID="natureOfBusiness2" Text="Nature of business:" CssClass="control-label col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="natureOfBusiness2" runat="server" Wrap="False" CssClass="form-input" MaxLength="20"></asp:TextBox>
	                    <asp:CustomValidator ID="natureOfBusinessValidator2" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Nature of business is required" ForeColor="Red" OnServerValidate="natureOfBusinessValidator2_ServerValidate"></asp:CustomValidator>
		            </div>
                </div>
            </div>
        

        <hr />
        <h3>4. Disclosures and Regulatory Information</h3>
        <asp:CustomValidator ID="part4Validate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Please select one option for every question in part 4" ForeColor="Red" OnServerValidate="part4Validate_ServerValidate"></asp:CustomValidator>
        <br />
        <div class="form-group">
            <div class="form-group col-md-offset-2">
                <h4>Primary Account Holder
                </h4>
                <div class="form-group">
                    <p>Are you employed by a registered securities broker/dealer, investment advisor, bank or other financial institution?</p>
                    <asp:RadioButtonList ID="part4PrimaryQ1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="No">No</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes (you must submit a compliance letter with this application)</asp:ListItem>
                    </asp:RadioButtonList>

                </div>
                <div class="form-group">
                    <p>Are you a director, 10% shareholder or policy-making officer of a publicly traded company?</p>
                    <asp:RadioButtonList ID="part4PrimaryQ2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="No">No</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    </asp:RadioButtonList>

                </div>
            </div>

            <div class="form-group" id="CoClientPart4" runat="server" visible="false">
                <h4>Co-Account Holder</h4>
                <div class="form-group">
                    <p>Are you employed by a registered securities broker/dealer,investment advisor, bank or other financial institution?</p>
                    <asp:RadioButtonList ID="part4CoQ1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="No">No</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes (you must submit a compliance letter with this application)</asp:ListItem>
                    </asp:RadioButtonList>

                </div>
                <div class="form-group">
                    <p>Are you a director, 10% shareholder or policy-making officer ofa publicly traded company?</p>
                    <asp:RadioButtonList ID="part4CoQ2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="No">No</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="form-group">
                
                    <p>Describe the primary source of funds deposited to this account: </p>
                    <asp:RadioButtonList ID="primarySource" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="primarySource_SelectedIndexChanged">
                        <asp:ListItem Value="salary/wages/savings">salary/wages/savings</asp:ListItem>
                        <asp:ListItem Value="investment/capital gains">investment/capital gains</asp:ListItem>
                        <asp:ListItem Value="family/relatives/inheritance">family/relatives/inheritance </asp:ListItem>
                        <asp:ListItem Value="Other">Other (describe below)</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:TextBox ID="Part4Other" runat="server" Wrap="False" CssClass="form-input" MaxLength="30"></asp:TextBox>
                
                    <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter the description in the textbox" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                
            </div>
        </div>

        <hr />
        <h3>5. Investment Profile</h3><br />
        <asp:CustomValidator ID="part5Validator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="part5Validator_ServerValidate" ErrorMessage="Please select an option for every question in part 5"></asp:CustomValidator>
        <div class="form-group">
            <div class="form-group">
                <p>Investment objective* for this account:</p>
                <asp:RadioButtonList ID="investmentObjective" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="capital preservation">capital preservation</asp:ListItem>
                        <asp:ListItem Value="income"> income</asp:ListItem>
                        <asp:ListItem Value="growth"> growth </asp:ListItem>
                        <asp:ListItem Value="speculation">speculation </asp:ListItem>
                </asp:RadioButtonList>
            </div>
            
            <div class="form-group">
                <p>Investment knowledge:</p>
                <asp:RadioButtonList ID="investmentKnowledge" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="none"> none </asp:ListItem>
                        <asp:ListItem Value="limited"> limited</asp:ListItem>
                        <asp:ListItem Value="good"> good </asp:ListItem>
                        <asp:ListItem Value="extensive"> extensive </asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div class="form-group">
                <p>Investment experience:</p>
                <asp:RadioButtonList ID="investmentExperience" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="none"> none </asp:ListItem>
                        <asp:ListItem Value="limited"> limited</asp:ListItem>
                        <asp:ListItem Value="good"> good </asp:ListItem>
                        <asp:ListItem Value="extensive"> extensive </asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div class="form-group">
                <p>Annual income:</p>
                <asp:RadioButtonList ID="annualIncome" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="under HK$20,000"> under HK$20,000 </asp:ListItem>
                        <asp:ListItem Value="HK$20,001 - HK$200,000"> HK$20,001 - HK$200,000 </asp:ListItem>
                        <asp:ListItem Value="HK$200,001 - HK$2,000,000"> HK$200,001 - HK$2,000,000 </asp:ListItem>
                        <asp:ListItem Value="more than HK$2,000,000"> more than HK$2,000,000 </asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div class="form-group">
                <p>Approximate liquid net worth (cash and securities):</p>
                <asp:RadioButtonList ID="liquidNetWorth" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="under HK$100,000"> under HK$100,000 </asp:ListItem>
                        <asp:ListItem Value="HK$100,001 - HK$1,000,000"> HK$100,001 - HK$1,000,000  </asp:ListItem>
                        <asp:ListItem Value="HK$1,000,001 - HK$10,000,000">  HK$1,000,001 - HK$10,000,000  </asp:ListItem>
                        <asp:ListItem Value="more than HK$10,000,000"> more than HK$10,000,000  </asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div class="form-group">
                <p>* Investment objective definitions</p>
                <p>Capital preservation: The objective of a capital preservation strategy is to protect your initial investment by choosing investments that minimize thepotential of any loss of principal. The long-term risk of capital preservation is that the returns may not be adequate to offset inflation.</p>
                <p>Income: The objective of an income strategy is to provide current income rather than long-term growth of principal.</p>
                <p>Growth: The objective of a growth strategy is to increase the value of your investment over time while recognizing a high likelihood of volatility.</p>
                <p>Speculation: The objective of a speculation strategy is to assume a higher risk of loss in anticipation of potentially higher than average gain by takingadvantage of expected price changes. </p>
            </div>
        </div>

        <hr />
        <h3>6. Account Feature</h3>
        <div class="form-group">
            <div class="form-group">
                <h4>Earn Income on Your Cash Balance</h4>
                <p>The Free Credit Balance in your HKeInvest account is not interest bearing. However, you may choose to earn
                interest on the Free Credit Balance in your account by having daily automatic investment in or redemption of
                (“sweep’) shares of our sweep fund (“Fund”). (The Free Credit Balance is the sum of the cash balances in your
                Cash Account and Margin Account (if applicable) offset by any debit balances and/or cash retained as
                collateral in the accounts.)</p>
                <p>On any Exchange Business Day, the Free Credit Balance will be automatically invested in shares of the Fund
                on the following Exchange Business Day. Shares of the Fund will be automatically redeemed to satisfy a debit
                balance in your HKeInvest account, to provide necessary cash collateral in your Margin Account (if
                applicable) or to the extent necessary to settle securities transactions. </p>
            </div>
            <asp:CheckBox ID="part6" runat="server" Text="Yes, sweep my Free Credit Balance into the Fund."/>
        </div>

        <hr />
        <h3>7. Initial Account Deposit</h3>
        <div class="form-group">
            <h4>A HK$20,000 minimum deposit is requireed to open an account</h4>
            <p>Check one or more:</p>
            <p><asp:CustomValidator ID="part7Validator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="part7Validator_ServerValidate"></asp:CustomValidator>
            </p>
            <p><asp:CheckBox ID="cheque" runat="server"/>
                A cheque for HK$ <asp:TextBox ID="chequeAmount" runat="server" MaxLength="10"></asp:TextBox> made payable to Hong Kong Electronic Investments LLC is enclosed.
            </p>
            <p>
                <asp:CheckBox ID="transfer" runat="server" />
                A completed Account Transfer Form for HK$ <asp:TextBox ID="transferAmount" runat="server" MaxLength="10"></asp:TextBox> is attached. 
            </p>
            
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn button-default" Height="34px" OnClick="Submit_Click" />
            </div>
        </div> 
    </div>
</asp:Content>
