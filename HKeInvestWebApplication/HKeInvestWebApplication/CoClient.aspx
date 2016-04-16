<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoClient.aspx.cs" Inherits="HKeInvestWebApplication.CoClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <h4>Co-client Information</h4>
        <div class="form-group">
            <asp:Label ID="titleCoClientLabel" Visible="false" runat="server" Text="Title"  ClientIDMode="Inherit" CssClass="label-control col-md-2" ></asp:Label>
            <div>
                <asp:DropDownList ID="titleCoClient" Visible="false" runat="server" AutoPostBack="True">
                    <asp:ListItem>---Select a title---</asp:ListItem>
                    <asp:ListItem>Mr.</asp:ListItem>
                    <asp:ListItem>Mrs.</asp:ListItem>
                    <asp:ListItem>Ms.</asp:ListItem>
                    <asp:ListItem>Dr.</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="titleCoClientRequired" Enabled="false" ControlToValidate="titleCoClient" InitialValue="---Select a title---" runat="server" ErrorMessage="Title of Co-client is required." Text="*" Display="Dynamic" EnableClientScript="false"></asp:RequiredFieldValidator>
            </div>
            <asp:Label ID="FirstNameCoClientLabel" runat="server" Visible="false" Text="First Name" AssociatedControlID="FirstNameCoClient" ClientIDMode="Inherit" CssClass="label-control col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="FirstNameCoClient" runat="server" Visible="false" CssClass="form-control" MaxLength="35"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FirstNameRequired" ControlToValidate="FirstNameCoClient" Enabled="false" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="First Name of co-client is required."></asp:RequiredFieldValidator>
            </div>
            <asp:Label ID="LastNameCoClientLabel" runat="server" Visible="false" Text="Last Name" Font-Overline="False" AssociatedControlID="LastNameCoClient" CssClass="label-control col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="LastNameCoClient" runat="server" Visible="false" CssClass="form-control" MaxLength="35"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LastNameCoClientRequired" Enabled="false" ControlToValidate="LastNameCoClient" CssClass="text-danger" Text="*" EnableClientScript="false" Display="Dynamic" runat="server" ErrorMessage="Last Name of co-client is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="DateOfBirthCoClientLabel" Visible="false"  runat="server" Text="Date Of Birth" AssociatedControlID="DateOfBirthCoClient" CssClass="label-control col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="DateOfBirthCoClient" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DateOfBirthCoClientRequired" ControlToValidate="DateOfBirthCoClient" Enabled="false" CssClass="text-danger" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Date of Birth of the co-client is required."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="DateOfBirthCoClientExpression" ControlToValidate="DateOfBirthCoClient" Enabled="false" CssClass="text-danger" ValidationExpression="^(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Date of Birth of co-client is not validate."></asp:RegularExpressionValidator>
            </div>
            <asp:Label ID="EmailCoClientLabel" Visible="false" runat="server" Text="Email" AssociatedControlID="EmailCoClient" CssClass="label-control col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="EmailCoClient" Visible="false" runat="server" TextMode="Email" CssClass="form-control" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailCoClientRequired" Enabled="false" ControlToValidate="EmailCoClient" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Email address of co-client is required." Text="*"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <h6>Home address</h6>
            <div>
                <asp:Label ID="buildingAddressCoClientLabel" Visible="false" runat="server" Text="Building (if any)" AssociatedControlID="buildingAddressCoClient" CssClass="control-label col-md-2"></asp:Label>
                <div class="col-md-4">
                    <asp:TextBox ID="buildingAddressCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
            </div>
            <div>
                <asp:Label ID="streetAddressCoClientLabel" Visible="false"  runat="server" Text="Street " AssociatedControlID="streetAddressCoClient" CssClass="control-label col-md-2"></asp:Label>
                <div class="col-md-4">
                    <asp:TextBox ID="streetAddressCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="35"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="streetAddressCoClientRequired" Enabled="false" ControlToValidate="streetAddressCoClient" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Street address of co-client is required." Text="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div>
                <asp:Label ID="districtAddressCoClientLabel" Visible="false"  runat="server" Text="District " AssociatedControlID="districtAddressCoClient" CssClass="control-label col-md-2"></asp:Label>
                <div class="col-md-4">
                    <asp:TextBox ID="districtAddressCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="19"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="districtAddressCoClientRequired" ControlToValidate="districtAddressCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="District address of co-client is required." Text="*"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="homePhoneCoClientLabel" Visible="false"  runat="server" Text="Home phone " AssociatedControlID="homePhoneCoClient" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="homePhoneCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="homePhoneCoClientRequired" Enabled="false" ControlToValidate="homePhoneCoClient" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Home phone of co-client is required." Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="homePhoneCoClientExpression" ControlToValidate="homePhoneCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" ValidationExpression="^[0-9]{8}$" runat="server" ErrorMessage="The format for home phone of co-client is wrong." Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="homeFaxCoClientLabel" Visible="false" runat="server" Text="Home fax" AssociatedControlID="homeFaxCoClient" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="homeFaxCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="homeFaxCoClientRequired" Enabled="false" ControlToValidate="homeFaxCoClient" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Home fax of co-client is required." Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="homeFaxCoClientExpression" ControlToValidate="homeFaxCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" ValidationExpression="^[0-9]{8}$" runat="server" ErrorMessage="The format for home fax of co-client is wrong." Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="businessPhoneCoClientLabel" Visible="false"  runat="server" Text="Business phone" AssociatedControlID="businessPhoneCoClient" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="businessPhoneCoClient" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="businessPhoneCoClientRequired" Enabled="false" ControlToValidate="businessPhoneCoClient" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Business phone of co-client is required." Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="businessPhoneCoClientExpression" Enabled="false" ControlToValidate="businessPhoneCoClient" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ValidationExpression="^[0-9]{8}$" ErrorMessage="The format for business phone of co-client is wrong." Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="mobilePhoneCoClientLabel" Visible="false"  runat="server" Text="Mobile Phone" AssociatedControlID="businessPhone" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="mobilePhoneCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="mobilePhoneCoClientRequired" ControlToValidate="mobilePhoneCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Mobile phone of co-client is required."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="mobilePhoneCoClientExpression" ControlToValidate="mobilePhoneCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ValidationExpression="^[0-9]{8}$" ErrorMessage="The format for mobile phone of co-client is wrong."></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="countryCitizenshipCoClientLabel" Visible="false" runat="server" Text="Country of citizenship" CssClass="control-label col-md-2" AssociatedControlID="countryCitizenshipCoClient"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="countryCitizenshipCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ID="countryCitizenshipCoClientRequired" ControlToValidate="countryCitizenshipCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Country of citizenship of co-client is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="countryResidenceCoClientLabel" Visible="false" runat="server" Text="Country of legal residence" CssClass="control-label col-md-2" AssociatedControlID="ccountryResidenceCoClient"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="countryResidenceCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ID="countryResidenceCoClientRequired" ControlToValidate="countryResidenceCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Country of residence of co-client is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
        <asp:Label ID="HKIDCoClientLabel" Visible="false" runat="server" Text="HKID/Passport#" AssociatedControlID="HKIDCoClient" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
            <asp:TextBox ID="HKIDCoClient" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="HKIDCoClientRequired" Enabled="false" ControlToValidate="HKIDCoClient" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="A HKID or Passport number of co-client is required."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="HKIDCoClientExpression" Enabled="false" ControlToValidate="HKIDCoClient" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ValidationExpression="^[0-9a-zA-Z]{8}$" runat="server" ErrorMessage="The format for HKID/Passport of co-client is wrong."></asp:RegularExpressionValidator>
        </div>
    </div>
        <div class="form-group">
            <asp:Label ID="countryPassportCoClientLabel" Visible="false"  runat="server" Text="Passport country of issue" AssociatedControlID="countryPassportCoClient" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="countryPassportCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ID="countryPassportCoClientRequired" ControlToValidate="countryPassportCoClient" Visible="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Passport country of issue of co-client is required."></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="form-group">
        <h4>Employment Information of Co-client</h4>
         <div class="form-group">
        <asp:Label ID="employmentStatusCoClientLabel" Visible="false" runat="server" Text="Employment status" AssociatedControlID="employmentStatusCoClient"  CssClass="control-label col-md-2"></asp:Label>
            <div>
                <asp:DropDownList ID="employmentStatusCoClient" runat="server"  AutoPostBack="true">
                    <asp:ListItem>---Select a status---</asp:ListItem>
                    <asp:ListItem>Employed</asp:ListItem>
                    <asp:ListItem>Self-employed</asp:ListItem>
                    <asp:ListItem>Retired</asp:ListItem>
                    <asp:ListItem>Student</asp:ListItem>
                    <asp:ListItem>Not employed</asp:ListItem>
                    <asp:ListItem>Homemaker</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="employmentStatusCoClientRequired" ControlToValidate="employmentStatusCoClient" Enabled="false" InitialValue="---Select a status---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Employment status of co-client is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="occupationCoClientLabel" Visible="false" runat="server" Text="Specific occupation" AssociatedControlID="occupationCoClient"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="occupationCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="occupationCoClientRequired" ControlToValidate="occupationCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Specific occupation of co-client is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="yearCoClientLabel" Visible="false" runat="server" Text="Year with employer" AssociatedControlID="yearCoClient"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="yearCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="2" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="yearCoClientRequired" ControlToValidate="yearCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Year with employer of co-client is required."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="yearCoClientExpression" ControlToValidate="yearCoClient" Enabled="false" ValidationExpression="^[0-9]$" runat="server" ErrorMessage="Year with employer of co-client should contain only digits." CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="employerNameCoClientLabel" Visible="false" runat="server" Text="Employer Name" AssociatedControlID="employerNameCoClient"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="employerNameCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="25" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="employerNameCoClientRequired" ControlToValidate="employerNameCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Employer Name of co-client is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="employerPhoneCoClientLabel" Visible="false" runat="server" Text="Employer Phone" AssociatedControlID="employerPhoneCoClient"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="employerPhoneCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="8" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="employerPhoneCoClientRequired" ControlToValidate="employerPhoneCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Business phone of co-client is required." Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="employerPhoneCoClientExpression" ControlToValidate="employerPhoneCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ValidationExpression="^[0-9]{8}$" ErrorMessage="The format for business phone of co-client is wrong." Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="businessNatureCoClientLabel" Visible="false" runat="server" Text="Nature of business" AssociatedControlID="businessNatureCoClient"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="businessNatureCoClient" Visible="false" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="businessNatureCoClientRequired" ControlToValidate="businessNatureCoClient" Enabled="false" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Business nature of co-client is required."></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="form-group">
        <h4>Disclosures and Regulatory Information</h4>
        <div class="form-group">
            <asp:Label ID="isEmployedRegisteredCoClientLabel" Visible="false" AssociatedControlID="isEmployedRegisteredCoClient"  runat="server" Text="Are you employed by a registered securities broker/dealer, investment advisor, bank or other financial institution?"></asp:Label>
            <asp:DropDownList ID="isEmployedRegisteredCoClient" Visible="false" runat="server">
                <asp:ListItem>---Please select yes/no---</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes (you must submit a compliance letter with this application)</asp:ListItem>

            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="isEmployedRegisteredCoClientRequired" ControlToValidate="isEmployedRegisteredCoClient" Enabled="false" InitialValue="---Please select yes/no---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="If you are employed by a registered of co-client is required."></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="isDirectorCoClientLabel" AssociatedControlID="isDirectorCoClient" Visible="false"  runat="server" Text="Are you a director, 10% shareholder or policy-making officer of a publicly traded company? "></asp:Label>
            <asp:DropDownList ID="isDirectorCoClient" Visible="false" runat="server">
                <asp:ListItem>---Please select yes/no---</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="isDirectorCoClientRequired" ControlToValidate="isDirectorCoClient" Enabled="false" InitialValue="---Please select yes/no---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="If you are a director or officer of co-client is required."></asp:RequiredFieldValidator>
        </div>
    </div>
</asp:Content>
