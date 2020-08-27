# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

$solutions = (
	"src/framework",
	"src/PlugIn"
)

$projects = (
	# framework
	"src/framework/Wechaty.Application",
	"src/framework/Wechaty.Application.Contracts",
	"src/framework/Wechaty.Domain.Shared",
	"src/framework/Wechaty.Plugin.Base",
	"src/framework/Wechaty.Puppet.Contracts",
	"src/framework/Wechaty.Puppet.Hostie",
	#"src/framework/Wechaty.Puppet.Mock",
	"src/framework/Wechaty.Puppet.Model",
	
	# Plugin
	"src/PlugIn/Wechaty.PlugIn.DingDong",
	"src/PlugIn/Wechaty.PlugIn.QRCodeTerminal",
	"src/PlugIn/Wechaty.PlugIn.Weather"
)