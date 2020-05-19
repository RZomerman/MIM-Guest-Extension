---
title: Installing MIM MA 
description: 
author: RZomerman
ms.date: 04/15/2020
layout: LandingPage
ms.topic: landing-page
ms.service: 
ms.subservice:
---


# Setup MIM for AAD Guest Accounts - Architecture

In order to allow Kerberos authentication to AD backend services an account will need to exist in AD. As guest users in Azure AD do not replicate back to Active Directory domains through Azure AD Connect, another connector will need to be created.
After installation and configuration, MIM will read all “Guest” users from Azure Active Directory, import them into it’s metaverse and create users in Active Directory.
For this the following attribute flows will be configured:

![AAD Guests MIM Rule Architecture](./images/1.Architecture.png)

Each user object in Azure AD has an “id” this is a guid that represents the objectID. This is being used as the ‘mv:uid’, the ‘mv:cn’ and partially used (only the first 20 characters) to create the ‘mv:accountname’. The other attributes on ‘Guest’ accounts are userPrincipalName, ‘displayName’, ‘surname’, ‘mail’, ‘givenName’ and ‘userType’. These are copied into the metaverse attributes. 

Join rules are based on aad:id<->mv:uid, meaning these attributes are used to determine if an inbound object can be matched to an existing object in the metaverse or that a new object has to be created.

To be able to determine if an account has already been created on Active Directory, an import from AD into the metaverse will also be required. The equivalent attributes to the metaverse attributes are imported from AD and matched to the objects in the metaverse. The import join rule is based on objectSID (as this is unique).

On export to AD, the attributes are mapped 1:1, with the exception of 3 initial outbound flow attributes: ad:dn, ad:userAccountControl, dn:unicodePWD. These are only activated on the initial creation of the object and are used to: 
> - ad:dn -> set the distinguishedName of the object (and thus OU location)
> - ad:userAccountControl -> enforce smartcard logon only (to avoid users logging in with username/password)
> - ad:unicodePWD -> set a password for the user

Once users are created, their objects can be used in Kerberos Constraint Delegation (with Protocol Transition) architectures. For example, by using the Azure AD Application Proxy, guest users can now login to IIS (Kerberos) based websites on-premises.

For more information, see https://docs.microsoft.com/en-us/azure/active-directory/manage-apps/application-proxy-configure-single-sign-on-with-kcd
