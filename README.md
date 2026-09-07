# SDC440_Moblie_App_NET_MAUI

A .NET MAUI app that displays entered text and clears it with the Reset button.

## Run on Mac

Requires .NET 10, the MAUI workload, and Xcode selected as the active developer directory.

From this repository's root directory:

```sh
dotnet build Add_UI_Elements_and_Handle_Events/Add_UI_Elements_and_Handle_Events.csproj -f net10.0-maccatalyst
dotnet build Add_UI_Elements_and_Handle_Events/Add_UI_Elements_and_Handle_Events.csproj -f net10.0-maccatalyst -t:Run
```

Save changes and close the running app before running both commands again. The first command builds the app bundle; the second launches it. Running only the second command after cleaning can fail because the app bundle has been removed.

Enter text and click **Display Text** to show it below the buttons. Click **Reset** to clear both the input and output.

The Mac build removes Finder information and resource-fork metadata from the generated app bundle before code signing, preventing the signing error “resource fork, Finder information, or similar detritus not allowed.”
