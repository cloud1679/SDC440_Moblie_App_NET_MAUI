# SDC440_Moblie_App_NET_MAUI

A .NET MAUI app that displays entered text and clears it with the Reset button.

## Run on Mac

Requires .NET 10, the MAUI workload, and Xcode selected as the active developer directory.

From this repository's root directory:

```sh
dotnet build Add_UI_Elements_and_Handle_Events/Add_UI_Elements_and_Handle_Events.csproj -f net10.0-maccatalyst -t:Run
```

Enter text and click **Display Text** to show it below the buttons. Click **Reset** to clear both the input and output.

The Mac build removes Finder information and resource-fork metadata from the generated app bundle before code signing, preventing the signing error “resource fork, Finder information, or similar detritus not allowed.”
