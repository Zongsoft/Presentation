#!/bin/sh

set -ex

CAKE_ARGS="--verbosity=normal"

Project_Presentation_Core="Zongsoft.Presentation/build.cake"
Project_Presentation_Avalonia="Zongsoft.Presentation.Avalonia/build.cake"
Project_Presentation_Windows="Zongsoft.Presentation.Windows/build.cake"
Project_Presentation_Plugins="Zongsoft.Presentation.Plugins/build.cake"

if [ -f "$Project_Presentation_Core" ]; then
	dotnet cake $Project_Presentation_Core $CAKE_ARGS "$@"
fi

if [ -f "$Project_Presentation_Avalonia" ]; then
	dotnet cake $Project_Presentation_Avalonia $CAKE_ARGS "$@"
fi

if [ -f "$Project_Presentation_Windows" ]; then
	dotnet cake $Project_Presentation_Windows $CAKE_ARGS "$@"
fi

if [ -f "$Project_Presentation_Plugins" ]; then
	dotnet cake $Project_Presentation_Plugins $CAKE_ARGS "$@"
fi
