Connect-AzAccount 

$emailaddress="delton.fernandes@british-business-bank.co.uk"
$UserId=(Get-AzAdUser -Mail $emailAddress).id
$RoleDefinitionId=(Get-AzRoleDefinition -name "Azure Service Bus Data Owner").id

Remove-AzResourceGroup -Name delton-rg
New-AzResourceGroup -Name delton-rg -Location "Uk South"
Cd "C:\Users\Delton.Fernandes\OneDrive - British Business Bank plc\Documents\GitHub\Quick-Scripts\CoreHrFunctionApp"
New-AzResourceGroupDeployment -Name "corehrarmtemplate" -ResourceGroup delton-rg -TemplateFile "./azuredeploy.json" -userid $UserId -roledefinitionid $RoleDefinitionId