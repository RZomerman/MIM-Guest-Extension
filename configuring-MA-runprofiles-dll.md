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
- [The Active Directory MA](installing-ADMA-dll-version.md)
- [The Graph API MA](installing-GraphAPIMA-dll.md)

## Creating Run Profiles
For each of the two created MA’s
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

## Next: Running initial Synchronizations 
All the rules have been added now, and we can continue with the initial import of AAD Guest users into the metaverse and then exporting them to AD.

- [Run the synchronization rules](run-sync-rules-dll.md)