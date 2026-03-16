# Bracket Builder - GitHub Pages Setup Complete ✅

Your Blazor WebAssembly application is now configured for GitHub Pages deployment!

## What Was Done

### 1. **Project Configuration** 
   - Updated `BracketBuilder.Blazor.csproj` to include `StaticWebAssetBasePath=/bracket-builder/`
   - This ensures all assets are served correctly from the GitHub Pages subdirectory

### 2. **Base Path Configuration**
   - Updated `wwwroot/index.html` with `<base href="/bracket-builder/" />`
   - This allows the Blazor router to work correctly on GitHub Pages

### 3. **Build Workflow**
   - Updated `.github/workflows/deploy.yml` to use .NET 10.0
   - Workflow now:
     - Builds the Blazor WebAssembly app
     - Creates 404.html for SPA routing support
     - Deploys to the `gh-pages` branch automatically on push to main
     - Uses the `.nojekyll` file to prevent Jekyll processing

### 4. **Documentation**
   - Created `GITHUB_PAGES_SETUP.md` with complete deployment guide

## Next Steps to Deploy

### ✅ Step 1: Enable GitHub Pages
1. Go to your repository: https://github.com/Revan1328/bracket-builder
2. Click **Settings** → **Pages**
3. Under "Build and deployment":
   - Set **Source** to "Deploy from a branch"
   - Select `gh-pages` branch
   - Select `root` folder
4. Click **Save**

### ✅ Step 2: Push Changes
```bash
git add .
git commit -m "Configure GitHub Pages deployment for Blazor WASM"
git push origin main
```

### ✅ Step 3: Monitor Deployment
1. Go to the **Actions** tab in your repository
2. Watch the "Deploy to GitHub Pages" workflow
3. Once successful (green checkmark), your site is live!

## Your Live Site
Once deployed, your app will be available at:
```
https://Revan1328.github.io/bracket-builder/
```

## How It Works

### Automatic Workflow
Every time you push to `main`:
1. GitHub Actions builds the project
2. Publishes to the `gh-pages` branch
3. GitHub Pages serves the `gh-pages` branch at your site URL

### Local Testing
To test locally before deploying:
```bash
# Run in development mode
dotnet watch run --project BracketBuilder.Blazor/BracketBuilder.Blazor.csproj

# Or publish and test the production build
dotnet publish BracketBuilder.Blazor/BracketBuilder.Blazor.csproj -c Release -o ./publish
```

## Features of Your App

✨ **March Madness Bracket Simulation**
- Generate random tournament brackets
- 64 teams across 4 regions
- 6 rounds of play
- Statistical probability models

🎨 **Interactive UI**
- Blazor WebAssembly (client-side only, no server needed)
- Bootstrap 5 styling
- Real-time bracket output
- Upset highlighting in red
- Loading states with spinner

⚡ **Performance**
- Trimmed for smaller bundle size
- No server required after deployment
- Pure client-side execution

## Troubleshooting

### Issue: App loads but styles are missing
- Clear browser cache (Ctrl+Shift+Delete)
- Hard refresh (Ctrl+Shift+R)
- Check browser console for 404 errors

### Issue: Pages not routing correctly
- Verify `404.html` exists in gh-pages branch
- Verify `.nojekyll` file exists
- Check base href in index.html matches deployment path

### Issue: GitHub Actions workflow failing
- Check Actions tab for error messages
- Ensure all commits are pushed
- Verify .NET 10.0 is available

## Files Modified

```
BracketBuilder.Blazor/
  ├── BracketBuilder.Blazor.csproj (updated: added StaticWebAssetBasePath)
  ├── wwwroot/
  │   └── index.html (updated: base href to /bracket-builder/)
  └── Components/
      └── _Imports.razor (updated: added Layout namespace)

.github/
  └── workflows/
      └── deploy.yml (updated: .NET 8.0 → 10.0)

GITHUB_PAGES_SETUP.md (created: deployment guide)
```

## Additional Resources

- [Blazor WASM Hosting on GitHub Pages](https://learn.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/webassembly)
- [GitHub Pages Documentation](https://docs.github.com/en/pages)
- [GitHub Actions Documentation](https://docs.github.com/en/actions)

---

**You're all set!** Push your changes and watch your app deploy to GitHub Pages automatically. 🚀
