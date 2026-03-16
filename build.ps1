#!/usr/bin/env pwsh
# Script to build and test the Blazor app locally

Write-Host "🚀 Bracket Builder - Local Build & Test" -ForegroundColor Cyan

Write-Host "`n📁 Building solution..." -ForegroundColor Yellow
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed!" -ForegroundColor Red
    exit 1
}

Write-Host "✅ Build successful!" -ForegroundColor Green

Write-Host "`n📦 Publishing for production..." -ForegroundColor Yellow
dotnet publish BracketBuilder.Blazor/BracketBuilder.Blazor.csproj -c Release -o ./publish --no-restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Publish failed!" -ForegroundColor Red
    exit 1
}

Write-Host "✅ Publish successful!" -ForegroundColor Green

Write-Host "`n📊 Output folder contents:" -ForegroundColor Yellow
Get-ChildItem "./publish/wwwroot" -Recurse | 
    Where-Object {-not $_.PSIsContainer} | 
    Measure-Object -Property Length -Sum | 
    ForEach-Object {Write-Host "Total size: $([Math]::Round($_.Sum/1MB, 2)) MB"}

Write-Host "`n✨ To deploy:" -ForegroundColor Cyan
Write-Host "  1. Commit: git add . && git commit -m 'Ready to deploy'"
Write-Host "  2. Push: git push origin main"
Write-Host "  3. Watch: https://github.com/Revan1328/bracket-builder/actions"
Write-Host "`n🌐 Live site: https://Revan1328.github.io/bracket-builder/"
