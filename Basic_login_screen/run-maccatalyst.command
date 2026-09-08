#!/bin/zsh
set -euo pipefail
cd -- "${0:A:h}/Basic_login_screen"
dotnet build Basic_login_screen.csproj -f net10.0-maccatalyst -p:RuntimeIdentifier=maccatalyst-arm64 '-t:Build;Run'
