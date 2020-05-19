---
title: Configuring Run Profiles for Management Agents 
description: 
author: RZomerman
ms.date: 04/15/2020
layout: LandingPage
ms.topic: landing-page
ms.service: 
ms.subservice:
---


# Setup MIM for AAD Guest Accounts - Configuring Run Profiles

## Prerequisits
This chapter will create the run profiles for the created Management Agents:

You should have completed the following steps already:

- [Windows Server 2019 preparations](prepare-server-ws-2019.md)
- [SQL Server 2016/2017 setup](install-SQL-server.md)
- [MIM Synchronization Service](install-mim-sync-service.md)
- [The MIM synchronization MA](installing-MimMa.md) 
- [The Active Directory MA](installing-ADMA.md)
- [The Graph API MA](nstalling-GraphAPIMA.md)

## Creating Run Profiles
For each of the three created MA’s
1. Right click the newly created MA and select **Configure Run Profiles**
2. Click **New Profile..** and create the following profiles by:
- Provide the **Profile Name** and click **Next**
- For the **step type** select the type and click **Next**
- On the _management agent configuration_ click **Finish**

> | Name                     |      Type                |
> |--------------------------|:-------------------------:
> | Full Import (Stage Only) | Full Import (Stage Only) |
> | Full Synchronization     | Full Synchronization     |
> | Delta Import (Stage Only)| Delta Import (Stage Only)|
> | Delta Synchronization    | Delta Synchronization    |
> | Export                   | Export                   |

3. Once all profiles are added click **OK**

## Next: Configuring Synchronization Rules in the MIM Portal 
A number of rules will have to be created through the MIM portal, this includes importing users from AAD and AD and exporting users to AD:
- [Synchronization Rule 1: Import Guest User from Azure Active Directory](rule1-import-from-aad.md)
- [Synchronization Rule 2: Import Active Directory Guest users](rule2-import-from-ad.md)
- [Synchronization Rule 3: Create Guest User account in Active Directory](rule3-export-to-ad.md)
