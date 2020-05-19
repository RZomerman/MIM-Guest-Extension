# @author: Fabien Duchene
# @mail: fabien.duchene1 **at** googlemail.com
############
# PARAMETERS
############
$params_ComputerName = "."      # "." is the current computer
$params_delayBetweenExecs = 3600    #delay between each execution, in seconds
$params_numOfExecs = 0          #Number of executions 0 for infinite
$params_runProfilesOrder = 
@(
#   @{
#        type="Forefront Identity Management (FIM)";
#        name="MIMMA";
#       profilesToRun=@("Full Import";"Full Synchronization");
#    };
    @{
        type="Graph (Microsoft)";
        name="AADMA";
        profilesToRun=@("Full Import (Stage Only)";"Full Synchronization");
    };
    @{
        type="Active Directory";
        name="ADMA";
        profilesToRun=@("Full Import (Stage Only)";"Full Synchronization";"Export");
    };

);

############
# FUNCTIONS
############
$line = "-----------------------------"
function Write-Output-Banner([string]$msg) {
    Write-Output $line,("- "+$msg),$line
}

############
# DATAS
############
$MAs = @(get-wmiobject -class "MIIS_ManagementAgent" -namespace "root\MicrosoftIdentityIntegrationServer" -computername $params_ComputerName)
$numOfExecDone = 0

############
# PROGRAM
############
do {
    Write-Output-Banner("Execution #:"+(++$numOfExecDone))
    foreach($MATypeNRun in $params_runProfilesOrder) {
        $found = $false;
        foreach($MA in $MAS) {  
            if(!$found) {
            if($MA.name.Equals($MATypeNRun.name)) {
                $found=$true;
                Write-Output-Banner("MA: "+$MA.name)
                foreach($profileName in $MATypeNRun.profilesToRun) {
                    Write-Output (" "+$profileName),"  -> starting"
                    $datetimeBefore = Get-Date;
                    Write-Output ("starting" + $profileName)
                    $result = $MA.Execute($profileName);
                    $datetimeAfter = Get-Date;
                    $duration = $datetimeAfter - $datetimeBefore;
                    if("success".Equals($result.ReturnValue)){
                        $msg = "done. Duration: "+$duration.Hours +":"+$duration.Minutes+":"+$duration.Seconds
                    } else { $msg = "Error: "+$result }
                    Write-Output ("  -> "+$msg)
                }
            }
        }
    }
    if(!$found) { Write-Output ("Not found MA type :"+$MATypeNRun.name); }
}
$continue = ($params_numOfExecs -EQ 0) -OR ($numOfExecDone -lt $params_numOfExecs)
    if($continue) { 
        Write-Output-Banner("Sleeping "+$params_delayBetweenExecs+" seconds")
        Start-Sleep -s $params_delayBetweenExecs
    }
} while(
    $continue
    )
