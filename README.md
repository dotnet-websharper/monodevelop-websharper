# monodevelop.websharper

[WebSharper][ws] add-in for MonoDevelop and XamarinStudio.

*Status*: available for testing.

## Install

MonoDevelop or XamarinStudio 4.2.2+ with F# language binding installed is required.

Go to Add-in Manager, and manually add a new repository:

    https://raw.githubusercontent.com/intellifactory/monodevelop.websharper/master/repository/
    
Upon refreshing, WebSharper should be an installable add-in in the "Web Development" category.

You may need to restart the IDE after installing.

## Use

New Project and New Solution dialogs should have a WebSharper section with various templates.

## Build

You need MonoDevelop/XamarinStudio 4.2.2+, Mono 3.2+, and latest F# 3.1. 

To refresh `repository/` index, do:

    make release
    
To install into the local IDE for testing do:

    make install

[ws]: http://websharper.com
