---
title: Scheduling Autorun of MIM Synchronization Rules
description: 
author: RZomerman
ms.date: 04/15/2020
layout: LandingPage
ms.topic: landing-page
ms.service: 
ms.subservice:
---


# Setup MIM for AAD Guest Accounts - Scheduling the autoruns

## Prerequisits

This chapter will help you setup a schedule for automatically running the synchronization profiles. 

You should have completed the following steps already:

- [Windows Server 2019 preparations](prepare-server-ws-2019.md)
- [SQL Server 2016/2017 setup](install-SQL-server.md)
- [MIM Synchronization Service](install-mim-sync-service.md)
- [The MIM synchronization MA](installing-MimMa.md) 

* For option 1
- [SharePoint](prepare-server-sharepoint.md)
- [MIM Service and Portal](install-mim-service-portal.md)
- [The Active Directory MA](installing-ADMA.md)
- [The Graph API MA](installing-AADMA.md)
- [Configuring Run Profiles](configuring-MA-runprofiles.md)
- [Synchronization Rule 1: Import Guest User from Azure Active Directory](rule1-import-from-aad.md)
- [Synchronization Rule 2: Import Active Directory Guest users](rule2-import-from-ad.md)
- [Synchronization Rule 3: Create Guest User account in Active Directory](rule3-export-to-ad.md)

* For option 2
- [The Extension DLL](extension-dll.md)
- [The Active Directory MA with DLL](installing-ADMA-dll-version.md)
- [The Graph API MA with DLL](installing-GraphAPIMA-dll.md)
- [Configuring Run Profiles](configuring-MA-runprofiles-dll.md)

## PowerShell script to run the MA's

It is possible to automate the schedule for the Import of Guest accounts. There are many ways to schedule the run profiles and in this chapter we will describe the PowerShell script which can be found on: https://social.technet.microsoft.com/Forums/en-US/c7e204be-05b4-40e1-bf95-e0191a76ece3/using-powershell-to-start-run-profiles?forum=ilm2

A copy of the (already adjusted) script can be found in the github repository:

- [runMimSync.ps1](runMimSync.ps1)

1. Download and save the file (for example in C:\Scripts) in a PowerShell format (for example runMIMSync.ps1)
2. Adjust the script if required (when names of the Management agents are not "AADMA", "ADMA")
3. Open powershell in administrative mode on the server and type **Set-ExecutionPolicy Unrestricted**
4. Type **Y** to confirm
5. Open a new PowerShell window (non-admin) and run the script by executing **\scripts\runMIMSync.ps1**
6. Validate that the script works (press ctrl-c to quit during the timeout value if needed)

### Scheduling the script
1. Go to Start and type Task Scheduler and open this program
2. Select **Create a basic task** on the action menu
3. For the name type _MIM Scheduled Sync_ and click **Next**
4. Select **When the computer starts** and click **Next**
5. For the Program/Script type **PowerShell.exe**
6. In the Add Arguments (optional) field, type:** -ExecutionPolicy Bypass c:\scripts\runMIMSync.ps1**
7. Select **Open properties…** and click **Finish**
8. On the properties field, select **Change User or Group**
9. Make sure the Location shows the `<servername>`
10. Type `MIMSyncUser` for the user
11.	Select **Run whether user is logged on or not**
12.	Select **Triggers** tab and there select **Edit** for the **At Startup** entry
13.	Under **Advanced Settings** select **Delay Task for** and set it to **15 minutes** and click **OK**
14.	Click **OK** and on the user account pop-up type the password for `<MIMSyncUser>`
15.	Restart the computer or browse to Task Scheduler Library, select the created schedule and click **Run**
_Note that the delay will be imposed_
16.	Validate the script execution by monitoring the Operations tab in the Synchronization Service Manager
