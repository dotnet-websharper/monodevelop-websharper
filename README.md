# monodevelop.websharper

[![Build Status](https://travis-ci.org/intellifactory/monodevelop.websharper.svg?branch=master)](https://travis-ci.org/intellifactory/monodevelop.websharper)

[WebSharper][ws] add-in for MonoDevelop and XamarinStudio.

## Install

On MonoDevelop or XamarinStudio 5.0+:

* Go to **Tools** > **Add-in Manager**.
* Under the **Gallery** tab, open the **Web Development** category.
* Select **WebSharper**, and on the right panel click the **Install...** button.

Make sure that you have the F# Language Binding add-in installed. It comes bundled with Xamarin Studio, but you need to install it in MonoDevelop (from the **Language bindings** category in the Add-in Manager).

## Use

New Project and New Solution dialogs should have a WebSharper section with various [templates](http://websharper.com/docs/templates).

## Build

You need MonoDevelop/XamarinStudio 5.0+, Mono 3.4.0+ and latest F#.

To refresh `repository/` index, do:

    make release
    
To install into the local IDE for testing do:

    make install

[ws]: http://websharper.com
