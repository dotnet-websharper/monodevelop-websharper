#!/bin/bash -e
# installs Mono Framework and Xamarin Studio dependencies

MONO_VERSION="3.4.0"
XAMARIN_VERSION="5.0.0.878-0"

MONO_PATH=$(which mono || true)
if [ -x "$MONO_PATH" ]; then
	echo "===> found Mono Framework"
	mono --version
else
	echo "===> installing Mono Framework $MONO_VERSION"
	curl -OL "http://download.mono-project.com/archive/$MONO_VERSION/macos-10-x86/MonoFramework-MDK-$MONO_VERSION.macos10.xamarin.x86.pkg"
	sudo installer -pkg "MonoFramework-MDK-${MONO_VERSION}.macos10.xamarin.x86.pkg" -target /
    mono --version
fi

if [ -d "/Applications/Xamarin Studio.app" ]; then
	echo "===> found Xamarin Studio"
else
	wget "http://download.xamarin.com/studio/Mac/XamarinStudio-$XAMARIN_VERSION.dmg"
	hdiutil mount XamarinStudio-$XAMARIN_VERSION.dmg
	sudo cp -R "/Volumes/Xamarin Studio/Xamarin Studio.app" /Applications	
fi

