<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientManageAccountAndClientInfo.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.ClientManageAccountAndClientInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label AssociatedControlID="currentAN" CssClass="control-label col-md-2" Text="Account Number: " runat="server" />
                <div class="col-md-4">
                    <asp:Label ID="currentAN" runat="server" />
                </div>
            </div>
            <asp:Label ID="result" runat="server" Font-Size="Large" BackColor="Red" />
            <div class="form-group">
                <h3>1. Account Type</h3>
                <div class="col-md-offset-1">
                    <asp:Label CssClass="control-label col-md-1" Text="Account type: " runat="server" AssociatedControlID="accountType" />
                    <div class="col-md-4">
                        <asp:Label ID="accountType" runat="server" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <h3>2. Client Information</h3>
                <div class="col-md-offset-1">
                    <div class="form-group">
                        <h4>Primary Account Holder</h4>
                        <div class="form-group">
                            <asp:Label CssClass="control-label col-md-1" Text="Title: " runat="server" AssociatedControlID="pTitle" />
                            <div class="col-md-4">
                                <asp:DropDownList ID="pTitle" runat="server">
                                    <asp:ListItem>Mr.</asp:ListItem>
                                    <asp:ListItem>Mrs.</asp:ListItem>
                                    <asp:ListItem>Ms.</asp:ListItem>
                                    <asp:ListItem>Dr.</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
            	        <div class="form-group">
		                    <asp:Label runat="server" Text="First name:" CssClass="label-control col-md-2" AssociatedControlID="pFirstName"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="pFirstName" runat="server" CssClass="form-control" MaxLength="35" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pFirstName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="First name is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
	
		                    <asp:Label runat="server" AssociatedControlID="pLastName" Text="Last name:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="pLastName" runat="server"  CssClass="form-control" MaxLength="35" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pLastName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Last name is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="pLastName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Last name should contain at least one alphabet" ForeColor="Red" ValidationExpression="^.*[a-zA-Z]+.*$"></asp:RegularExpressionValidator>
		                    </div>
                        </div>
                        <div class="form-group">
		                    <asp:Label AssociatedControlID="pDateOfBirth" runat="server" Text="Date of birth (dd/mm/yyyy):" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
                                <asp:Label ID="pDateOfBirth" runat="server"></asp:Label>
		                    </div>
	
		                    <asp:Label runat="server" AssociatedControlID="pEmail" Text="Email address:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="pEmail" runat="server"  CssClass="form-control" MaxLength="30" TextMode="Email" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="pEmail" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Email address is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
	                    </div>
                        <div class="form-group">
                            <asp:Label runat="server" Text="Home address:" CssClass="label-control col-md-2" Font-Size="Medium"></asp:Label>
                        </div>
	                    <div class="form-group">
		                    <asp:Label runat="server" AssociatedControlID="pBuilding" Text="Building (if any):" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="pBuilding" runat="server"  CssClass="form-control" MaxLength="50" ValidationGroup="Page"></asp:TextBox>
		                    </div>
	
		                    <asp:Label runat="server" AssociatedControlID="pStreet" Text="Street:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="pStreet" runat="server"  CssClass="form-control" MaxLength="35" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="pStreet" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Street is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
                        </div>
                        <div class="form-group">
		                    <asp:Label runat="server" AssociatedControlID="pDistrict" Text="District:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="pDistrict" runat="server"  CssClass="form-control" MaxLength="19" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="pDistrict" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="District is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
	                    </div>
                        <div class="form-group">
	        	            <asp:Label ID="Label7" runat="server" AssociatedControlID="pHomePhone" Text="Home phone:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="pHomePhone" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
	                            <asp:CustomValidator ID="pHomePhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="pPhoneValidate_ServerValidate"></asp:CustomValidator>
	        	                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="pHomePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Home phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	            </div>
	
	        	            <asp:Label ID="Label8" runat="server" AssociatedControlID="pHomeFax" Text="Home fax:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="pHomeFax" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="pHomeFax" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Home fax is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	            </div>
	                    </div>
                        <div class="form-group">
	        	            <asp:Label ID="Label9" runat="server" AssociatedControlID="pBusinessPhone" Text="Business phone:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="pBusinessPhone" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
                                <asp:CustomValidator ID="pBusinessPhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="pPhoneValidate_ServerValidate"></asp:CustomValidator>
                	            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="pBusinessPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Business phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
                	        </div>
	
		                    <asp:Label ID="Label10" runat="server" AssociatedControlID="pMobilePhone" Text="Mobile phone:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="pMobilePhone" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
                	            <asp:CustomValidator ID="pMobilePhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="pPhoneValidate_ServerValidate"></asp:CustomValidator>
	        	                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="pMobilePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Mobile phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	            </div>
	                    </div>
                        <div class="form-group">
	        	            <asp:Label ID="Label11" runat="server" AssociatedControlID="pCitizenship" Text="Country of citizenship:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="pCitizenship" runat="server"  CssClass="form-control" MaxLength="70" ValidationGroup="Page"></asp:TextBox>
                	            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="pCitizenship" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country of citizenship is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
	
	        	            <asp:Label ID="Label12" runat="server" AssociatedControlID="pLegalResidence" Text="Country of legal residence:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="pLegalResidence" runat="server"  CssClass="form-control" MaxLength="70" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="pLegalResidence" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country of legal residence is required" ForeColor="Red"></asp:RequiredFieldValidator>
	        	            </div>
	                    </div>
                        <div class="form-group">
                            <asp:CheckBox AutoPostBack="true" ID="pHKIDUsed" runat="server" Text="Do you use HKID for this application?" OnCheckedChanged="pHKIDUsed_CheckedChanged" />
                        </div>
	                    <div class="form-group">
	        	            <asp:Label ID="Label13" runat="server" AssociatedControlID="pHKID" Text="HKID/passport number:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	            	            <asp:TextBox ID="pHKID" runat="server"  CssClass="form-control" MaxLength="8" ValidationGroup="Page"></asp:TextBox>
                	            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="pHKID" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="HKID/passport number is required" ForeColor="Red"></asp:RequiredFieldValidator>
	        	                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="pHKID" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of HKID is invalid" ForeColor="Red" ValidationExpression="^[0-9A-Z]{8}$"></asp:RegularExpressionValidator>
	        	                <asp:CustomValidator ID="pHKIDValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="pHKIDValidate_ServerValidate"></asp:CustomValidator>
	        	            </div>
	
		                    <asp:Label ID="Label14" runat="server" AssociatedControlID="pPassportCountry" Text="Passport country of issue:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="pPassportCountry" runat="server"  CssClass="form-control" MaxLength="70" ValidationGroup="Page"></asp:TextBox>
	                            <asp:CustomValidator ID="pPassportCountryOfIssueValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="pPassportCountryOfIssueValidate_ServerValidate" ErrorMessage="Passport country of issue is required."></asp:CustomValidator>
		                    </div>
	                    </div>
                    </div>
                    <div class="form-group" id="coClientInfo" runat="server" visible="false">
                        <h4>Co-Account Holder</h4>
                        <div class="form-group">
                            <asp:Label Text="Title: " runat="server" AssociatedControlID="cTitle" />
                            <div class="col-md-4">
                                <asp:DropDownList ID="cTitle" runat="server">
                                    <asp:ListItem>Mr.</asp:ListItem>
                                    <asp:ListItem>Mrs.</asp:ListItem>
                                    <asp:ListItem>Ms.</asp:ListItem>
                                    <asp:ListItem>Dr.</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
		                    <asp:Label runat="server" Text="First name:" CssClass="label-control col-md-2" AssociatedControlID="cFirstName"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="cFirstName" runat="server" CssClass="form-control" MaxLength="35" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cFirstName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="First name is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
	
		                    <asp:Label runat="server" AssociatedControlID="cLastName" Text="Last name:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="cLastName" runat="server"  CssClass="form-control" MaxLength="35" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="cLastName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Last name is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="cLastName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Last name should contain at least one alphabet" ForeColor="Red" ValidationExpression="^.*[a-zA-Z]+.*$"></asp:RegularExpressionValidator>
		                    </div>
                        </div>
                        <div class="form-group">
		                    <asp:Label AssociatedControlID="cDateOfBirth" runat="server" Text="Date of birth (dd/mm/yyyy):" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
                                <asp:Label ID="cDateOfBirth" runat="server"></asp:Label>
		                    </div>
	
		                    <asp:Label runat="server" AssociatedControlID="cEmail" Text="Email address:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="cEmail" runat="server"  CssClass="form-control" MaxLength="30" TextMode="Email" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cEmail" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Email address is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
	                    </div>
                        <div class="form-group">
                            <asp:Label runat="server" Text="Home address:" CssClass="label-control col-md-2" Font-Size="Medium"></asp:Label>
                        </div>
                        <div class="form-group">
		                    <asp:Label runat="server" AssociatedControlID="cBuilding" Text="Building (if any):" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="cBuilding" runat="server"  CssClass="form-control" MaxLength="50" ValidationGroup="Page"></asp:TextBox>
		                    </div>
	
		                    <asp:Label runat="server" AssociatedControlID="cStreet" Text="Street:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="cStreet" runat="server"  CssClass="form-control" MaxLength="35" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cStreet" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Street is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
                        </div>
                        <div class="form-group">
		                    <asp:Label runat="server" AssociatedControlID="cDistrict" Text="District:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="cDistrict" runat="server"  CssClass="form-control" MaxLength="19" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="cDistrict" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="District is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
	                    </div>
                        <div class="form-group">
	        	            <asp:Label ID="Label2" runat="server" AssociatedControlID="cHomePhone" Text="Home phone:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="cHomePhone" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
	                            <asp:CustomValidator ID="cHomePhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="cPhoneValidate_ServerValidate"></asp:CustomValidator>
	        	                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="cHomePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Home phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	            </div>
	
	        	            <asp:Label ID="Label3" runat="server" AssociatedControlID="cHomeFax" Text="Home fax:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="cHomeFax" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="cHomeFax" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Home fax is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	            </div>
	                    </div>
                        <div class="form-group">
	        	            <asp:Label ID="Label4" runat="server" AssociatedControlID="cBusinessPhone" Text="Business phone:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="cBusinessPhone" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
                                <asp:CustomValidator ID="cBusinessPhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="cPhoneValidate_ServerValidate"></asp:CustomValidator>
                	            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="cBusinessPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Business phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
                	        </div>
	
		                    <asp:Label ID="Label5" runat="server" AssociatedControlID="cMobilePhone" Text="Mobile phone:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="cMobilePhone" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
                	            <asp:CustomValidator ID="cMobilePhoneValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="cPhoneValidate_ServerValidate"></asp:CustomValidator>
	        	                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="cMobilePhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the Mobile phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
	        	            </div>
	                    </div>
                        <div class="form-group">
	        	            <asp:Label ID="Label6" runat="server" AssociatedControlID="cCitizenship" Text="Country of citizenship:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="cCitizenship" runat="server"  CssClass="form-control" MaxLength="70" ValidationGroup="Page"></asp:TextBox>
                	            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="cCitizenship" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country of citizenship is required" ForeColor="Red"></asp:RequiredFieldValidator>
		                    </div>
	
	        	            <asp:Label ID="Label15" runat="server" AssociatedControlID="cLegalResidence" Text="Country of legal residence:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	        	                <asp:TextBox ID="cLegalResidence" runat="server"  CssClass="form-control" MaxLength="70" ValidationGroup="Page"></asp:TextBox>
	                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="cLegalResidence" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country of legal residence is required" ForeColor="Red"></asp:RequiredFieldValidator>
	        	            </div>
	                    </div>
                        <div class="form-group">
                            <asp:CheckBox AutoPostBack="true" ID="cHKIDUsed" runat="server" Text="Do you use HKID for this application?" OnCheckedChanged="cHKIDUsed_CheckedChanged" />
                        </div>
	                    <div class="form-group">
	        	            <asp:Label ID="Label16" runat="server" AssociatedControlID="cHKID" Text="HKID/passport number:" CssClass="label-control col-md-2"></asp:Label>
	        	            <div class="col-md-4">
	            	            <asp:TextBox ID="cHKID" runat="server"  CssClass="form-control" MaxLength="8" ValidationGroup="Page"></asp:TextBox>
                	            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="cHKID" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="HKID/passport number is required" ForeColor="Red"></asp:RequiredFieldValidator>
	        	                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="cHKID" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of HKID is invalid" ForeColor="Red" ValidationExpression="^[0-9A-Z]{8}$"></asp:RegularExpressionValidator>
	        	                <asp:CustomValidator ID="cHKIDValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="cHKIDValidate_ServerValidate"></asp:CustomValidator>
	        	            </div>
	
		                    <asp:Label ID="Label17" runat="server" AssociatedControlID="cPassportCountry" Text="Passport country of issue:" CssClass="label-control col-md-2"></asp:Label>
		                    <div class="col-md-4">
		                        <asp:TextBox ID="cPassportCountry" runat="server"  CssClass="form-control" MaxLength="70" ValidationGroup="Page"></asp:TextBox>
	                            <asp:CustomValidator ID="cPassportCountryValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="cPassportCountryOfIssueValidate_ServerValidate" ErrorMessage="Passport country of issue is required."></asp:CustomValidator>
		                    </div>
	                    </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <h3>3. Employment Information</h3>
                <div class="col-md-offset-1">
                    <div class="form-group">
                        <h4>Primary Account Holder</h4>
                        <div class="form-group">
                    <asp:Label ID="Label18" AssociatedControlID="pEmploymentStatus" runat="server" Text="Employment status:" CssClass="label-control col-md-2"></asp:Label>

                    <asp:DropDownList ID="pEmploymentStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="pEmploymentStatus_SelectedIndexChanged">
                        <asp:ListItem Value="Employed">Employed</asp:ListItem>
                        <asp:ListItem Value="Self-employed">Self-employed</asp:ListItem>
                        <asp:ListItem Value="Retired">Retired</asp:ListItem>
                        <asp:ListItem Value="Student">Student</asp:ListItem>
                        <asp:ListItem Value="Not employed">Not employed</asp:ListItem>
                        <asp:ListItem Value="Homemaker">Homemaker</asp:ListItem>
                    </asp:DropDownList>

                </div>
                        <div class="form-group">
                    <asp:Label ID="Label34" runat="server" AssociatedControlID="pOccupation" Text="Specific occupation:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="pOccupation" runat="server"  CssClass="form-control" MaxLength="20" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="pOccupationValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Specific occupation is required" ForeColor="Red" OnServerValidate="pOccupationValidator_ServerValidate"></asp:CustomValidator>
		            </div>
	
		            <asp:Label ID="Label35" runat="server" AssociatedControlID="pYears" Text="Years with employer:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="pYears" runat="server"  CssClass="form-control" MaxLength="2" TextMode="Number" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="pYearsValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Years with employer is required" ForeColor="Red" OnServerValidate="pYearsValidator_ServerValidate"></asp:CustomValidator>
		                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="pYears" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the years with employer is invalid" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$"></asp:RegularExpressionValidator>
		            </div>
                </div>
                        <div class="form-group">
                    <asp:Label ID="Label36" runat="server" AssociatedControlID="pEmployerName" Text="Employer name:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="pEmployerName" runat="server"  CssClass="form-control" MaxLength="25" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="pEmployerNameValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Employer name is required" ForeColor="Red" OnServerValidate="pEmployerNameValidator_ServerValidate"></asp:CustomValidator>
	            	</div>
	
		            <asp:Label ID="Label37" runat="server" AssociatedControlID="pEmployerPhone" Text="Employer phone:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="pEmployerPhone" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="pEmployerPhoneValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Employer phone is required" ForeColor="Red" OnServerValidate="pEmployerPhoneValidator_ServerValidate"></asp:CustomValidator>
		                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="pEmployerPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the employer phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
		            </div>
                </div>
                        <div class="form-group">
                    <asp:Label ID="Label38" runat="server" AssociatedControlID="pNatureOfBusiness" Text="Nature of business:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="pNatureOfBusiness" runat="server"  CssClass="form-control" MaxLength="20" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="pNatureOfBusinessValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Nature of business is required" ForeColor="Red" OnServerValidate="pNatureOfBusinessValidator_ServerValidate"></asp:CustomValidator>
		            </div>
                </div>
                    </div>
                    <div class="form-group" id="coClientEmploymentInfo" runat="server" visible="false">
                        <h4>Co-Account Holder</h4>
                        <div class="form-group">
                    <asp:Label ID="Label19" AssociatedControlID="cEmploymentStatus" runat="server" Text="Employment status:" CssClass="label-control col-md-2"></asp:Label>

                    <asp:DropDownList ID="cEmploymentStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cEmploymentStatus_SelectedIndexChanged">
                        <asp:ListItem Value="Employed">Employed</asp:ListItem>
                        <asp:ListItem Value="Self-employed">Self-employed</asp:ListItem>
                        <asp:ListItem Value="Retired">Retired</asp:ListItem>
                        <asp:ListItem Value="Student">Student</asp:ListItem>
                        <asp:ListItem Value="Not employed">Not employed</asp:ListItem>
                        <asp:ListItem Value="Homemaker">Homemaker</asp:ListItem>
                    </asp:DropDownList>

                </div>
                        <div class="form-group">
                    <asp:Label ID="Label20" runat="server" AssociatedControlID="cOccupation" Text="Specific occupation:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="cOccupation" runat="server"  CssClass="form-control" MaxLength="20" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="cOccupationValidate" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Specific occupation is required" ForeColor="Red" OnServerValidate="cOccupationValidator_ServerValidate"></asp:CustomValidator>
		            </div>
	
		            <asp:Label ID="Label21" runat="server" AssociatedControlID="cYears" Text="Years with employer:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="cYears" runat="server"  CssClass="form-control" MaxLength="2" TextMode="Number" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="cYearsValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Years with employer is required" ForeColor="Red" OnServerValidate="cYearsValidator_ServerValidate"></asp:CustomValidator>
		                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="cYears" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the years with employer is invalid" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$"></asp:RegularExpressionValidator>
		            </div>
                </div>
                        <div class="form-group">
                    <asp:Label ID="Label22" runat="server" AssociatedControlID="cEmployerName" Text="Employer name:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="cEmployerName" runat="server"  CssClass="form-control" MaxLength="25" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="cEmployerNameValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Employer name is required" ForeColor="Red" OnServerValidate="cEmployerNameValidator_ServerValidate"></asp:CustomValidator>
	            	</div>
	
		            <asp:Label ID="Label23" runat="server" AssociatedControlID="cEmployerPhone" Text="Employer phone:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="cEmployerPhone" runat="server"  CssClass="form-control" MaxLength="8" TextMode="Phone" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="cEmployerPhoneValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Employer phone is required" ForeColor="Red" OnServerValidate="cEmployerPhoneValidator_ServerValidate"></asp:CustomValidator>
		                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="cEmployerPhone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the employer phone is invalid" ForeColor="Red" ValidationExpression="^[0-9]{8}$"></asp:RegularExpressionValidator>
		            </div>
                </div>
                        <div class="form-group">
                    <asp:Label ID="Label24" runat="server" AssociatedControlID="cNatureOfBusiness" Text="Nature of business:" CssClass="label-control col-md-2"></asp:Label>
		            <div class="col-md-4">
		                <asp:TextBox ID="cNatureOfBusiness" runat="server"  CssClass="form-control" MaxLength="20" ValidationGroup="Page"></asp:TextBox>
	                    <asp:CustomValidator ID="cNatureOfBusinessValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Nature of business is required" ForeColor="Red" OnServerValidate="cNatureOfBusinessValidator_ServerValidate"></asp:CustomValidator>
		            </div>
                </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <h3>4. Disclosure and Regulatory Information</h3>
                <div class="col-md-offset-1">
                    <div class="form-group">
                        <h4>Primary Account Holder</h4>
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
                    <div class="form-group" id="coClientDisclosureInfo" runat="server" visible="false">
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
                        <p>Describe the primary source of funds deposited to this account:</p>
                        <asp:RadioButtonList ID="primarySource" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="primarySource_SelectedIndexChanged">
                        <asp:ListItem Value="salary/wages/savings">salary/wages/savings</asp:ListItem>
                        <asp:ListItem Value="investment/capital gains">investment/capital gains</asp:ListItem>
                        <asp:ListItem Value="family/relatives/inheritance">family/relatives/inheritance </asp:ListItem>
                        <asp:ListItem Value="Other">Other (describe below)</asp:ListItem>
                    </asp:RadioButtonList>
                        <asp:TextBox ID="other" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:CustomValidator runat="server" ErrorMessage="The description of primary source is required" ID="primarySourceValidate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="primarySourceValidate_ServerValidate"></asp:CustomValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <h3>5. Investment Profile</h3>
                <div class="col-md-offset-1">
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
            </div>
            <div class="form-group">
                <h3>6. Account Feature</h3>
                <div class="col-md-offset-1">
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
                    <asp:RadioButtonList ID="part6" runat="server">
                        <asp:ListItem Value="Yes">Yes, sweep my Free Credit Balance into the Fund</asp:ListItem>
                        <asp:ListItem Value="No">No</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group">
                <h3>7. Account Balance</h3>
                <div class="col-md-offset-1">
                    <asp:Label CssClass="control-label col-md-1" Text="balance: " AssociatedControlID="balance" runat="server" />
                    <div class="col-md-4">
                        <asp:Label ID="balance" runat="server" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <h3>8. Login account username</h3>
                <div class="col-md-offset-1">
                    <asp:Label CssClass="control-label col-md-1" Text="User Name: " runat="server" />
                    <div class="col-md-4">
                        <asp:Label ID="userName" runat="server" />
                    </div>
                </div>
            </div>
            <div>
                <asp:Button Text="Update" CssClass="btn button-default" Height="34px" OnClick="Update_Click" runat="server" />
            </div>
        </div>
</asp:Content>
