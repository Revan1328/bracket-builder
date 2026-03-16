# Bracket Builder - Blazor WebAssembly Version

This is a Blazor WebAssembly conversion of the Bracket Builder console application. It runs entirely in the browser with no backend server required.

## What's New

✨ **Web-based UI** - Run in your browser instead of console  
⚡ **Client-side execution** - All processing happens on your machine  
☁️ **GitHub Pages hosting** - Deployed automatically on every push to `main`  
📊 **Same statistics engine** - Uses the original seed-based probability model

## Local Development

### Prerequisites
- .NET 8.0 SDK or later: [Download here](https://dotnet.microsoft.com/download)

### Build and Run

```bash
cd BracketBuilder.Blazor
dotnet watch run
```

Open `https://localhost:5001` in your browser.

## Deployment to GitHub Pages

The app is automatically deployed when you push to the `main` branch using GitHub Actions.

### First-Time Setup

1. **Enable GitHub Pages in your repository settings:**
   - Go to Settings → Pages
   - Set source to "Deploy from a branch"
   - Select `gh-pages` branch (created automatically by the workflow)
   - Save

2. **Update the base path (if using a non-root URL):**
   If your repo is not at the root of your domain (e.g., `github.com/username/bracket-builder`), update the base href in [wwwroot/index.html](BracketBuilder.Blazor/wwwroot/index.html):
   ```html
   <base href="/bracket-builder/" />
   ```

3. **Push to main:**
   ```bash
   git add .
   git commit -m "Add Blazor WebAssembly conversion"
   git push origin main
   ```

### View Your App

Once the workflow completes (~2-3 minutes), visit:
- `https://yourusername.github.io/bracket-builder` (if repo is not your username)
- Or your custom domain if configured

## Architecture

- **BracketBuilder.Blazor/** - Main web project
  - `Services/BracketBuilderService.cs` - Business logic (ported from console app)
  - `Components/Pages/Home.razor` - Main UI page
  - `wwwroot/` - Static assets

## Key Changes from Console App

| Feature | Console | Web |
|---------|---------|-----|
| Output | Console.WriteLine | Event callbacks |
| Display | Text-based | HTML/Bootstrap UI |
| Colors | Console colors | CSS colors |
| Threading | Synchronous | Async/background tasks |

## Customization

To modify the teams list or statistics, edit:
- `Services/BracketBuilderService.cs` - Contains all team data and stat maps

## Troubleshooting

**App not loading?**
- Check browser console (F12) for errors
- Verify the base href matches your deployment path

**Workflow failing?**
- Check GitHub Actions tab in your repo for error logs
- Ensure `.github/workflows/deploy.yml` exists

## License

Same as the original Bracket Builder project.
