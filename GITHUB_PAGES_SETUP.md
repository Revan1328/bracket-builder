# Bracket Builder - GitHub Pages Deployment Guide

This Blazor WebAssembly application is configured to deploy to GitHub Pages. Follow these steps to get it running.

## Prerequisites

- .NET 10.0 SDK or later
- Git
- GitHub repository

## Local Setup

1. **Clone the repository**
```bash
git clone https://github.com/Revan1328/bracket-builder.git
cd bracket-builder
```

2. **Run locally (development)**
```bash
dotnet watch run --project BracketBuilder.Blazor/BracketBuilder.Blazor.csproj
```

The app will be available at `https://localhost:7XXX`

3. **Build for production**
```bash
dotnet publish BracketBuilder.Blazor/BracketBuilder.Blazor.csproj -c Release -o ./publish
```

## GitHub Pages Setup

### Automatic Deployment (Recommended)

The repository includes a GitHub Actions workflow (`.github/workflows/deploy.yml`) that automatically:

1. Builds the Blazor WebAssembly project
2. Publishes it with the correct base path for GitHub Pages
3. Deploys to the `gh-pages` branch

**Steps:**

1. **Enable GitHub Pages:**
   - Go to your repository Settings
   - Navigate to Pages (left sidebar under Code and automation)
   - Set Source to "Deploy from a branch"
   - Select `gh-pages` branch and `root` folder
   - Click Save

2. **Push to main:**
   ```bash
   git add .
   git commit -m "Enable GitHub Pages deployment"
   git push origin main
   ```

3. **Workflow will automatically run:**
   - Check the Actions tab to monitor the deployment
   - Once complete, your site will be available at: `https://Revan1328.github.io/bracket-builder/`

### Manual Deployment (Alternative)

If you prefer to deploy manually:

1. Build and publish:
```bash
dotnet publish BracketBuilder.Blazor/BracketBuilder.Blazor.csproj -c Release -o ./publish
```

2. Create or update the `gh-pages` branch:
```bash
git subtree push --prefix publish/wwwroot origin gh-pages
```

3. Configure GitHub Pages (same as above)

## Configuration

- **Base Path:** The app is configured to run at `/bracket-builder/` (set in `BracketBuilder.Blazor.csproj` via `StaticWebAssetBasePath`)
- **Index Fallback:** `index.html` is copied to `404.html` for SPA routing support
- **.nojekyll:** Prevents Jekyll processing on GitHub Pages

## Features

- **Bracket Simulation:** Generate NCAA March Madness tournament brackets
- **Statistical Models:** Uses seed-based probability models for match predictions
- **Real-time Updates:** Displays round results with color-coded upsets
- **Responsive Design:** Works on desktop and mobile devices

## Troubleshooting

### App not loading at correct path
- Verify `StaticWebAssetBasePath` in `.csproj` matches your deployment path
- Check browser console for 404 errors on asset loads

### Styles not loading
- Clear browser cache and do a hard refresh (Ctrl+Shift+R or Cmd+Shift+R)
- Verify Bootstrap CDN is accessible

### Deploy action failing
- Check GitHub Actions logs for build errors
- Ensure .NET 10.0 is available on the runner
- Verify the project file references are correct

## Performance Notes

- The app uses `PublishTrimmed=true` to reduce bundle size
- `InvariantGlobalization=true` optimizes for English-only usage
- Blazor WASM startup time varies based on internet connection

## Support

For issues or questions:
1. Check the [Blazor documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
2. Review [GitHub Pages documentation](https://docs.github.com/en/pages)
3. Open an issue on the repository

---

**Live Demo:** https://Revan1328.github.io/bracket-builder/
