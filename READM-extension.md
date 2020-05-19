# MIM-Guest-Extension

As an organization that uses Azure Active Directory (Azure AD) B2B collaboration capabilities to invite guest users from partner organizations to your Azure AD, you can now provide these B2B users access to on-premises apps. These on-premises apps can use SAML-based authentication or Integrated Windows Authentication (IWA) with Kerberos constrained delegation (KCD).
On-premises Windows-Integrated Authentication or Kerberos-based applications, published via the Azure AD application proxy or other gateway mechanisms requires each user to have their own AD DS account, for identification and delegation purposes.

This repo will provide the required Extension DLL for Microsoft Identity Manager (MIM) 2016 Synchronization Service to synchronize Azure AD Guest users to your on-premises Active Directory Domain Services.

If you accept all defaults, copy the 2 files from the bin folder to "c:\program files\Microsoft Identity Manager\2010\Synchronization Service\Extensions" and configure MIM according to the guide.

The source code is avaiable to adjust / enhance the flows

A custom DLL has been created to support a few attribute flows that cannot be direct. 
The DLL Extension must be used for the AAD Import MA (AADMA) as well as the global options for provisioning. The DLL therefore has 2 cs extensions, an MaExtension (for the AADMA) and an MvExtsion.cs
MaExtension 
In the MaExtension.cs we have created a case "cd.user:id->mv.person:accountname": which is equal to the extensionRule specified on the AAD attribute import flows. This rule takes the “id” of the AAD user (which is a GUID) and takes only the first 20 characters of that id to set that as the accountName (which later will be used as the sAMAccountName). As the sAMAccountName has a limitation of a maximum of 20 characters and must be unique, this is the easiest way to set that. 
MvExtension
The MvExtension.cs is for exporting the metaverse user objects to Active Directory and is activated when the “Export” run profile on the ADMA is run. 
The first thing that will be done is creating the distinguishedName of the user. This is required to determine the location of the user object in AD. For this, the following items will be combined: “CN=” + uid + ou – where uid and ou are retrieved from the metaverse. Therefore on the AAD import we set a static value for OU=. 
The accountName in the Metaverse will be set as the sAMAccountName and in order to avoid users to forcing them to change passwords on login, the pwdLastSet will be set to -1 (which is interpreted as current time)
Next, as users are created and a password for the user will not have to be known by the user a random password (32 characters) will be created and set to the user. For this the GenerateRandomString function is available in the Extension.
