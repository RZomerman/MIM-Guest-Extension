---
title: Extension DLL for MIM to support AAD Guests -> AD
description: 
author: RZomerman
ms.date: 04/15/2020
layout: LandingPage
ms.topic: landing-page
ms.service: 
ms.subservice:
---


# Setup MIM for AAD Guest Accounts - Extension DLL

A custom DLL extension has been written to take care of a few alterations during the flow of attributes.The DLL Extension will be used for the AAD Import MA (AADMA) as well as the global options for provisioning (creating the user in AD).

## Installing the DLL
Copy the AAD-B2B-Guest-MIM-Extensions.dll file to the extensions folder:
C:\Program Files\Microsoft Forefront Identity Manager\2010\Synchronization Service\Extensions

1. In the Synchronization Service Manager program, click **Tools** and click **Options**
2. Select **Enable metaverse rules extension** and click **Browse**
3. Select the custom DLL: **AAD-B2B-Guest-MIM-Extensions.dll**
4. Select **Enable Provisioning Rules Extension**
5. Click **OK** to close

## Information about the DLL
The DLL has 2 cs extensions, an MaExtension (for the AADMA) and an MvExtsion.cs.

### MaExtension.cs 
In the MaExtension.cs we have created a case **"cd.user:id->mv.person:accountname"**: which is equal to the extensionRule specified on the AAD attribute import flows. This rule takes the “id” of the AAD user (which is a GUID) and takes only the first 20 characters of that id to set that as the accountName (which later will be used as the sAMAccountName). As the sAMAccountName has a limitation of a maximum of 20 characters and must be unique, this is the easiest way to set that.

### MvExtension
The MvExtension.cs is for exporting the metaverse user objects to Active Directory and is activated when the “Export” run profile on the ADMA is run.
The first thing that will be done is creating the distinguishedName of the user. This is required to determine the location of the user object in AD. For this, the following items will be combined: “CN=” + uid + ou – where uid and ou are retrieved from the metaverse. Therefore on the AAD import we set a static value for OU=.

The accountName in the Metaverse will be set as the sAMAccountName and in order to avoid users to forcing them to change passwords on login, the pwdLastSet will be set to -1 (which is interpreted as current time)

Next, as users are created and a password for the user will not have to be known by the user a random password (32 characters) will be created and set to the user. For this the GenerateRandomString function is available in the Extension.

## Next: Installing and configuring the MA's
- [The Active Directory MA with DLL](installing-ADMA-dll-version.md)
- [The Graph API MA with DLL](installing-GraphAPIMA-dll.md)
- [Configuring Run Profiles](configuring-MA-runprofiles-dll.md)
