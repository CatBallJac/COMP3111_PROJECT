<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account Application.aspx.cs" Inherits="HKeInvestWebApplication.Account_Application" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Account Application</h2>
    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="text-danger" EnableClientScript="false"/>
        <div class="form-group">
        <h3>1. Account Type</h3>
            <asp:DropDownList ID="AccountType" runat="server" AutoPostBack="True">
                <asp:ListItem>---Please select an account type---</asp:ListItem>
                <asp:ListItem>Individual</asp:ListItem>
                <asp:ListItem>Joint Tenants with Rights of Survivorship</asp:ListItem>
                <asp:ListItem>Joint Tenants in Common</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="AccountType" InitialValue="---Please select an account type---" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Account type is required."></asp:RequiredFieldValidator>
            
        </div>
        
    <div class="form-group" id="clientInfo">
        <h3>2. Client Information</h3>
        <div class="form-group">
            <asp:Button ID="coclient" runat="server" Text="Add a co-client" CssClass="btn button-default" Height="34px" OnClick="coclient_Click" />
        </div>
        <div class="form-group">
        
        <asp:Label runat="server" Text="Title"  ClientIDMode="Inherit" CssClass="label-control col-md-2" ></asp:Label>
            <div>
                <asp:DropDownList ID="title" runat="server" AutoPostBack="True">
                    <asp:ListItem>---Select a title---</asp:ListItem>
                    <asp:ListItem>Mr.</asp:ListItem>
                    <asp:ListItem>Mrs.</asp:ListItem>
                    <asp:ListItem>Ms.</asp:ListItem>
                    <asp:ListItem>Dr.</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ControlToValidate="title" InitialValue="---Select a title---" runat="server" ErrorMessage="Title is required." Text="*" Display="Dynamic" EnableClientScript="false"></asp:RequiredFieldValidator>
                </div>
        
        <asp:Label runat="server" Text="First Name" AssociatedControlID="FirstName" ClientIDMode="Inherit" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="FirstName" runat="server" CssClass="form-control" MaxLength="35"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="FirstName" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="First Name is required."></asp:RequiredFieldValidator>
        </div>
        <asp:Label runat="server" Text="Last Name" Font-Overline="False" AssociatedControlID="LastName" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="LastName" runat="server" CssClass="form-control" MaxLength="35"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="LastName" CssClass="text-danger" Text="*" EnableClientScript="false" Display="Dynamic" runat="server" ErrorMessage="Last Name is required."></asp:RequiredFieldValidator>
</div>
            </div>
   

        <div class="form-group">
        <asp:Label  runat="server" Text="Date Of Birth" AssociatedControlID="DateOfBirth" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
        <asp:TextBox ID="DateOfBirth" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="DateOfBirth" CssClass="text-danger" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Date of Birth is required."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="DateOfBirth" CssClass="text-danger" ValidationExpression="^(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" Display="Dynamic" Text="*" EnableClientScript="false" runat="server" ErrorMessage="Date of Birth is not validate."></asp:RegularExpressionValidator>
        </div>
        <asp:Label runat="server" Text="Email" AssociatedControlID="Email" CssClass="label-control col-md-2"></asp:Label >
        <div class="col-md-4">
        <asp:TextBox ID="Email" runat="server" TextMode="Email" CssClass="form-control" MaxLength="30"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="Email" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Email address is required." Text="*"></asp:RequiredFieldValidator>
    </div>
        </div>

        <div class="form-group">
            <h6>Home address</h6>
            <div>
            <asp:Label runat="server" Text="Building (if any)" AssociatedControlID="buildingAddress" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
            <asp:TextBox ID="buildingAddress" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
               
                </div>
                </div>
            <div>
            <asp:Label  runat="server" Text="Street " AssociatedControlID="streetAddress" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="streetAddress" runat="server" CssClass="form-control" MaxLength="35"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="streetAddress" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Street address is required." Text="*"></asp:RequiredFieldValidator>
     
            </div>
                </div>
            <div>
            <asp:Label  runat="server" Text="District " AssociatedControlID="districtAddress" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="districtAddress" runat="server" CssClass="form-control" MaxLength="19"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="districtAddress" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="District address is required." Text="*"></asp:RequiredFieldValidator>
            </div>
                </div>
        </div>

        <div class="form-group">
            <asp:Label  runat="server" Text="Home phone " AssociatedControlID="homePhone" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="homePhone" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="homePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Home phone is required." Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="homePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" ValidationExpression="^[0-9]{8}$" runat="server" ErrorMessage="The format for home phone is wrong." Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="Home fax" AssociatedControlID="homeFax" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="homeFax" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="homeFax" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Home fax is required." Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="homeFax" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" ValidationExpression="^[0-9]{8}$" runat="server" ErrorMessage="The format for home fax is wrong." Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label  runat="server" Text="Business phone" AssociatedControlID="businessPhone" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="businessPhone" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="businessPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Business phone is required." Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="businessPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ValidationExpression="^[0-9]{8}$" ErrorMessage="The format for business phone is wrong." Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label  runat="server" Text="Mobile Phone" AssociatedControlID="businessPhone" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="mobilePhone" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="mobilePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Mobile phone is required."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="mobilePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ValidationExpression="^[0-9]{8}$" ErrorMessage="The format for mobile phone is wrong."></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label  runat="server" Text="Country of citizenship" CssClass="control-label col-md-2" AssociatedControlID="countryCitizenship"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="countryCitizenship" runat="server" CssClass="form-control" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="countryCitizenship" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Country of citizenship is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label  runat="server" Text="Country of legal residence" CssClass="control-label col-md-2" AssociatedControlID="countryResidence"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="countryResidence" runat="server" CssClass="form-control" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="countryResidence" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Country of residence is required."></asp:RequiredFieldValidator>
            </div>
        </div>


    <div class="form-group">
        <asp:Label runat="server" Text="HKID/Passport#" AssociatedControlID="HKID" CssClass="label-control col-md-2"></asp:Label>
        <div class="col-md-4">
            <asp:TextBox ID="HKID" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="HKID" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="A HKID or Passport number is required."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="HKID" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ValidationExpression="^[0-9a-zA-Z]{8}$" runat="server" ErrorMessage="The format for HKID/Passport is wrong."></asp:RegularExpressionValidator>
        </div>
    </div>

        <div class="form-group">
            <asp:Label  runat="server" Text="Passport country of issue" AssociatedControlID="countryPassport" CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="countryPassport" runat="server" CssClass="form-control" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="countryPassport" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Passport country of issue is required."></asp:RequiredFieldValidator>
            </div>
        </div>

    

    
        </div>

    <div class="form-group">
        <h3>3. Employment Infomation</h3>
        <div class="form-group">
        <asp:Label runat="server" Text="Employment status" AssociatedControlID="employmentStatus"  CssClass="control-label col-md-2"></asp:Label>
            <div>
                <asp:DropDownList ID="employmentStatus" runat="server" OnSelectedIndexChanged="employmentStatus_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem>---Select a status---</asp:ListItem>
                    <asp:ListItem>Employed</asp:ListItem>
                    <asp:ListItem>Self-employed</asp:ListItem>
                    <asp:ListItem>Retired</asp:ListItem>
                    <asp:ListItem>Student</asp:ListItem>
                    <asp:ListItem>Not employed</asp:ListItem>
                    <asp:ListItem>Homemaker</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ControlToValidate="employmentStatus" InitialValue="---Select a status---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Employment status is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Specific occupation" AssociatedControlID="occupation"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="occupation" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="occupationRequired" ControlToValidate="occupation" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Specific occupation is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Year with employer" AssociatedControlID="year"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="year" runat="server" CssClass="form-control" MaxLength="2" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="yearRequired" ControlToValidate="year" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Year with employer is required."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="yearExpression" ControlToValidate="year" ValidationExpression="^\d+$" runat="server" ErrorMessage="Year with employer should contain only digits." CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Employer Name" AssociatedControlID="employerName"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="employerName" runat="server" CssClass="form-control" MaxLength="25" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="employerNameRequired" ControlToValidate="employerName" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Employer Name is required."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Employer Phone" AssociatedControlID="employerPhone"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="employerPhone" runat="server" CssClass="form-control" MaxLength="8" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="employerPhoneRequired" ControlToValidate="employerPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Business phone is required." Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="employerPhoneExpression" ControlToValidate="employerPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ValidationExpression="^[0-9]{8}$" ErrorMessage="The format for business phone is wrong." Text="*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Nature of business" AssociatedControlID="businessNature"  CssClass="control-label col-md-2"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="businessNature" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="businessNatureRequired" ControlToValidate="businessNature" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Business nature is required."></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div class="form-group">
        <h3>4.  Disclosures and Regulatory Information </h3>
        <div class="form-group">
            <asp:Label AssociatedControlID="isEmployedRegistered"  runat="server" Text="Are you employed by a registered securities broker/dealer, investment advisor, bank or other financial institution?"></asp:Label>
            <asp:DropDownList ID="isEmployedRegistered" runat="server">
                <asp:ListItem>---Please select yes/no---</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes (you must submit a compliance letter with this application)</asp:ListItem>

            </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="isEmployedRegistered" InitialValue="---Please select yes/no---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="If you are employed by a registered is required."></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:Label AssociatedControlID="isDirector"  runat="server" Text="Are you a director, 10% shareholder or policy-making officer of a publicly traded company? "></asp:Label>
            <asp:DropDownList ID="isDirector" runat="server">
                <asp:ListItem>---Please select yes/no---</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="isDirector" InitialValue="---Please select yes/no---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="If you are a director or officer is required."></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
        <asp:Label AssociatedControlID="DepositDescription"  runat="server" Text="Describe the primary source of funds deposited to this account:  "></asp:Label>
        
        <asp:DropDownList ID="DepositDescription" runat="server" OnSelectedIndexChanged="DepositDescription_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem>---Please select a source---</asp:ListItem>
            <asp:ListItem>salary/wages/savings</asp:ListItem>
            <asp:ListItem>investment/capital gains</asp:ListItem>
            <asp:ListItem>family/relatives/inheritance</asp:ListItem>
            <asp:ListItem>Other (describe below)</asp:ListItem>
        </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="DepositDescription" InitialValue="---Please select a source---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Primary source is required."></asp:RequiredFieldValidator>
        <div>
        <asp:TextBox ID="other" runat="server" CssClass="form-control" MaxLength="30" Visible="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="otherValidator" ControlToValidate="other" Enabled="false" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" ErrorMessage="Deposit description is required."></asp:RequiredFieldValidator>
            
        </div>
            </div>

    </div>
    <div class="form-group">
        <asp:Label ID="CoClientLabel" runat="server" Visible="false" Text="#Co-client Information#" Font-Size="Large"></asp:Label>
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
                <asp:RequiredFieldValidator ID="FirstNameCoClientRequired" ControlToValidate="FirstNameCoClient" Enabled="false" CssClass="text-danger" Text="*" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="First Name of co-client is required."></asp:RequiredFieldValidator>
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
            <asp:Label ID="homeAddressCoClientLabel" runat="server" Visible="false" Text="Home Address" Font-Size="Medium"></asp:Label>
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
            <asp:Label ID="countryResidenceCoClientLabel"  Visible="false" runat="server" Text="Country of legal residence" CssClass="control-label col-md-2" AssociatedControlID="countryResidenceCoClient"></asp:Label>
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
        <asp:Label ID="EmploymentCoClientInfo" runat="server" Visible="false" Text="Co-client employment information" Font-Size="Medium"></asp:Label>
         <div class="form-group">
        <asp:Label ID="employmentStatusCoClientLabel" Visible="false" runat="server" Text="Employment status" AssociatedControlID="employmentStatusCoClient"  CssClass="control-label col-md-2"></asp:Label>
            <div>
                <asp:DropDownList ID="employmentStatusCoClient" runat="server" Visible="false"  AutoPostBack="true" OnSelectedIndexChanged="employmentStatusCoClient_SelectedIndexChanged">
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
                <asp:RegularExpressionValidator ID="yearCoClientExpression" ControlToValidate="yearCoClient" Enabled="false" ValidationExpression="^\d+$" runat="server" ErrorMessage="Year with employer of co-client should contain only digits." CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*"></asp:RegularExpressionValidator>
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
        <asp:Label ID="DisclosureLabel" runat="server" Visible="false" Text="Disclosures and Regulatory Information" Font-Size="Medium"></asp:Label>
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

    <div class="form-group">
        <h3>5. Investment Profile</h3>
        <div class="form-group">
        <asp:Label AssociatedControlID="InvestObject"  runat="server" Text="Investment objective* for this account: "></asp:Label>
        <asp:DropDownList ID="InvestObject" runat="server">
            <asp:ListItem>---Please select an investment objective---</asp:ListItem>
            <asp:ListItem>capital preservation</asp:ListItem>
            <asp:ListItem>income</asp:ListItem>
            <asp:ListItem>growth</asp:ListItem>
            <asp:ListItem>speculation</asp:ListItem>
        </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="InvestObject"  InitialValue="---Please select an investment objective---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Investment objective is required."></asp:RequiredFieldValidator>
            </div>
        <div class="form-group">
        <asp:Label AssociatedControlID="knowledge"  runat="server" Text="Investment knowledge"></asp:Label>
        <asp:DropDownList ID="knowledge" runat="server">
            <asp:ListItem>---Select knowledge level---</asp:ListItem>
            <asp:ListItem>none</asp:ListItem>
            <asp:ListItem>limited</asp:ListItem>
            <asp:ListItem>good</asp:ListItem>
            <asp:ListItem>extensive</asp:ListItem>
        </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="knowledge" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" InitialValue="---Select knowledge level---" runat="server" ErrorMessage="Knowledge level is required."></asp:RequiredFieldValidator>
            </div>
        <div class="form-group">
        <asp:Label AssociatedControlID="experience"  runat="server" Text="Investment experience"></asp:Label>
        <asp:DropDownList ID="experience" runat="server">
            <asp:ListItem>---Select an experience level---</asp:ListItem>
            <asp:ListItem>none</asp:ListItem>
            <asp:ListItem>limited</asp:ListItem>
            <asp:ListItem>good</asp:ListItem>
            <asp:ListItem>extensive</asp:ListItem>
        </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="experience" InitialValue="---Select an experience level---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Experience level is required."></asp:RequiredFieldValidator>
            </div>
        <div class="form-group">
        <asp:Label AssociatedControlID="annualIncome"  runat="server" Text="Annual Income"></asp:Label>
        <asp:DropDownList ID="annualIncome" runat="server">
            <asp:ListItem>---Select an annual income level---</asp:ListItem>
            <asp:ListItem>under HK$20,000</asp:ListItem>
            <asp:ListItem>HK$20,001 - HK$200,000</asp:ListItem>
            <asp:ListItem>HK$200,001 - HK$2,000,000</asp:ListItem>
            <asp:ListItem>more than HK$2,000,000</asp:ListItem>
        </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="annualIncome" InitialValue="---Select an annual income level---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Annual income level is required."></asp:RequiredFieldValidator>
            </div>
        <div class="form-group">
        <asp:Label AssociatedControlID="netWorth"  runat="server" Text="Approximate liquid net worth (cash and securities): "></asp:Label>
        <asp:DropDownList ID="netWorth" runat="server">
            <asp:ListItem>---Select a net worth range---</asp:ListItem>
            <asp:ListItem>under HK$100,000</asp:ListItem>
            <asp:ListItem>HK$100,001 - HK$1,000,000</asp:ListItem>
            <asp:ListItem>HK$1,000,001 - HK$10,000,000</asp:ListItem>
            <asp:ListItem>more than HK$10,000,000 </asp:ListItem>
        </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="netWorth" InitialValue="---Select a net worth range---" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Net worth range is required."></asp:RequiredFieldValidator>
            </div>
        <p>* Investment objective definitions Capital preservation: The objective of a capital preservation strategy is to protect your initial investment by choosing investments that minimize the potential of any loss of principal. The long-term risk of capital preservation is that the returns may not be adequate to offset inflation. Income: The objective of an income strategy is to provide current income rather than long-term growth of principal. Growth: The objective of a growth strategy is to increase the value of your investment over time while recognizing a high likelihood of volatility. Speculation: The objective of a speculation strategy is to assume a higher risk of loss in anticipation of potentially higher than average gain by taking advantage of expected price changes. </p>
    </div>

    <div class="form-group">
        <h3>6  Account Feature</h3>
        <p>Earn Income on Your Cash Balance The Free Credit Balance in your HKeInvest account is not interest bearing. However, you may choose to earn interest on the Free Credit Balance in your account by having daily automatic investment in or redemption of (“sweep’) shares of our sweep fund (“Fund”). (The Free Credit Balance is the sum of the cash balances in your Cash Account and Margin Account (if applicable) offset by any debit balances and/or cash retained as collateral in the accounts.) On any Exchange Business Day, the Free Credit Balance will be automatically invested in shares of the Fund on the following Exchange Business Day. Shares of the Fund will be automatically redeemed to satisfy a debit balance in your HKeInvest account, to provide necessary cash collateral in your Margin Account (if applicable) or to the extent necessary to settle securities transactions. </p>
        <asp:CheckBox ID="ifSweep" runat="server" Text="Yes, sweep my Free Credit Balance into the Fund. "/>
    </div>
    <div class="form-group">
        <h3>7  Initial Account Deposit </h3>
        <p>A HK$20,000 minimum deposit is required to open an account. Check one or more:</p>

        <div class="form-group">
        <div>
        <asp:CheckBox ID="chequeCheck" AutoPostBack="true" runat="server" Text=" A cheque for HK$ ______________ made payable to Hong Kong Electronic Investments LLC is enclosed." OnCheckedChanged="chequeCheck_CheckedChanged"/>
            </div>
        <div class="col-md-4">
        <asp:TextBox ID="chequeText" runat="server" CssClass="form-control" MaxLength="6" Visible="false"></asp:TextBox>
           <asp:RequiredFieldValidator ID="chequeTextValidatorRequired" ControlToValidate="chequeText" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ErrorMessage="Amount of cheque is required." Text="*"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="chequeTextValidatorExpression" ControlToValidate="chequeText" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" runat="server" ValidationExpression="^[1-9]\d*$" ErrorMessage="The format for the amount of cheque is wrong." Text="*"></asp:RegularExpressionValidator>
        </div>
        </div>
        <div class="form-group">
        <div>
        <asp:CheckBox ID="transferCheck" AutoPostBack="true" runat="server" Text=" A completed Account Transfer Form for HK$ ______________ is attached." OnCheckedChanged="transferCheck_CheckedChanged"/>
        </div>
        <div class="col-md-4">
        <asp:TextBox ID="transferText" runat="server" CssClass="form-control" MaxLength="6" Visible="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="transferTextValidatorRequired" Enabled="false" ControlToValidate="transferText" CssClass="text-danger" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="Amount of transfer is required."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="transferTextValidatorExpression" Enabled="false" ControlToValidate="transferText" CssClass="text-danger" ValidationExpression="^[1-9]\d*$" Display="Dynamic" EnableClientScript="false" Text="*" runat="server" ErrorMessage="The amount of transfer should be digits."></asp:RegularExpressionValidator>
        </div>
        </div>
  


    

 </div>
        <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
        <asp:Button ID="Register" runat="server" Text="Register" CssClass="btn button-default" Height="34px" OnClick="Register_Click" />
        </div>
      
    </div>
        
</div>
</asp:Content>
