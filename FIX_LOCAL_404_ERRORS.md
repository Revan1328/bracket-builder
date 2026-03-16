# 🔧 Fixed: 404 Errors for Static Files

## Problem
You were seeing 404 errors for `app.css` and `blazor.webassembly.js` when running locally.

## Root Cause
The `StaticWebAssetBasePath=/bracket-builder/` in the `.csproj` file was only meant for **production builds** (GitHub Pages), not for local development. This caused the dev server to look for static files at the wrong path.

## Solution Applied
I've made two changes to fix this:

### 1. Update `.csproj` - Base Path Only for Release Builds
```xml
<!-- Only use the base path for production builds -->
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
    <StaticWebAssetBasePath>/bracket-builder/</StaticWebAssetBasePath>
</PropertyGroup>
```

**What this does:**
- Local development (Debug): Base path = `/` ✅ (finds `app.css` and other assets correctly)
- GitHub Pages (Release): Base path = `/bracket-builder/` ✅ (works on GitHub Pages)

### 2. Update `index.html` - Smart Base Href
```html
<base href="/" />

<script>
    // Override for GitHub Pages deployment
    if (window.location.hostname === 'revan1328.github.io') {
        document.querySelector('base').href = '/bracket-builder/';
    }
</script>
```

**What this does:**
- Local dev or localhost: Uses `base href="/"`
- GitHub Pages (revan1328.github.io): Uses `base href="/bracket-builder/"`

## Now You Can:

### ✅ Run Locally
```bash
dotnet watch run --project BracketBuilder.Blazor/BracketBuilder.Blazor.csproj
```
- No 404 errors
- Hot reload works
- Assets load correctly from `/app.css`, `/_framework/blazor.webassembly.js`, etc.

### ✅ Deploy to GitHub Pages
```bash
# Publish for production
dotnet publish BracketBuilder.Blazor/BracketBuilder.Blazor.csproj -c Release

# Deploy
git add . && git commit -m "Deploy" && git push origin main
```
- Works correctly at `/bracket-builder/` subdirectory

## Verify It Works

1. **Stop any running dev server** (Ctrl+C if running)
2. **Rebuild the project:**
   ```bash
   dotnet watch run --project BracketBuilder.Blazor/BracketBuilder.Blazor.csproj
   ```
3. **Check the Network tab** (F12 → Network):
   - `app.css` should be `200 OK`
   - `_framework/blazor.webassembly.js` should be `200 OK`
   - No more 404s ✅

4. **Test the app** - Click "Generate Bracket" and it should work!

## Why This Works

| Environment | Base Path | Source |
|-------------|-----------|--------|
| **Local Dev** | `/` | index.html default |
| **GitHub Pages** | `/bracket-builder/` | Script checks hostname |
| **Release Build** | `/bracket-builder/` | StaticWebAssetBasePath |

The three-layer approach ensures it works everywhere! 🎉

---

**If you still see 404s:**
1. Clear browser cache (Ctrl+Shift+Delete)
2. Stop the dev server and restart it
3. Hard refresh (Ctrl+Shift+R)

Everything should work now! 🚀
