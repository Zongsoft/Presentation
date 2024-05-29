[string]$project_presentation_core     = 'Zongsoft.Presentation/build.cake'
[string]$project_presentation_avalonia = 'Zongsoft.Presentation.Avalonia/build.cake'
[string]$project_presentation_windows  = 'Zongsoft.Presentation.Windows/build.cake'
[string]$project_presentation_plugins  = 'Zongsoft.Presentation.Plugins/build.cake'

[string]$CAKE_ARGS = '--verbosity=normal'

if(Test-Path $project_presentation_core)
{
	Write-Host "dotnet cake $project_presentation_core $CAKE_ARGS $ARGS" -ForegroundColor Magenta
	dotnet cake $project_presentation_core $CAKE_ARGS $ARGS
}

if(Test-Path $project_presentation_avalonia)
{
	Write-Host "dotnet cake $project_presentation_avalonia $CAKE_ARGS $ARGS" -ForegroundColor Magenta
	dotnet cake $project_presentation_avalonia $CAKE_ARGS $ARGS
}

if(Test-Path $project_presentation_windows)
{
	Write-Host "dotnet cake $project_presentation_windows $CAKE_ARGS $ARGS" -ForegroundColor Magenta
	dotnet cake $project_presentation_windows $CAKE_ARGS $ARGS
}

if(Test-Path $project_presentation_plugins)
{
	Write-Host "dotnet cake $project_presentation_plugins $CAKE_ARGS $ARGS" -ForegroundColor Magenta
	dotnet cake $project_presentation_plugins $CAKE_ARGS $ARGS
}
