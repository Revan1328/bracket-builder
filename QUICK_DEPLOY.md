# Quick Deploy Checklist

## Pre-Deployment (One Time)

- [ ] Go to https://github.com/Revan1328/bracket-builder/settings/pages
- [ ] Set Source to "Deploy from a branch"
- [ ] Select `gh-pages` branch and `root` folder
- [ ] Click Save

## To Deploy

```bash
git add .
git commit -m "Your message here"
git push origin main
```

Then watch: https://github.com/Revan1328/bracket-builder/actions

## Check Your Live Site

🌐 https://Revan1328.github.io/bracket-builder/

---

That's it! The GitHub Actions workflow will handle everything automatically.
