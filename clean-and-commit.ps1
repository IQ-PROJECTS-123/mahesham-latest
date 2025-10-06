# -------------------------------
# clean-and-commit.ps1
# -------------------------------
# 1. Close Visual Studio processes
Write-Host "Closing Visual Studio processes..."
Get-Process devenv,VBCSCompiler -ErrorAction SilentlyContinue | Stop-Process -Force

# 2. Delete .vs, bin, obj folders
$foldersToDelete = ".vs","bin","obj"
foreach ($folder in $foldersToDelete) {
    if (Test-Path $folder) {
        Write-Host "Deleting $folder..."
        Remove-Item -Recurse -Force $folder
    }
}

# 3. Ensure .gitignore is in place
$gitignorePath = ".gitignore"
if (-Not (Test-Path $gitignorePath)) {
    Write-Host "Creating .gitignore..."
    @"
.vs/
bin/
obj/
packages/
*.user
*.suo
*.log
*.cache
*.vsidx
"@ | Out-File -Encoding UTF8 $gitignorePath
} else {
    Write-Host ".gitignore already exists."
}

# 4. Stage .gitignore first
git add .gitignore
git commit -m "Fix .gitignore" -q 2>$null

# 5. Stage and commit rest of the project
git add .
git commit -m "Initial clean project commit"

Write-Host "✅ Cleanup and commit complete!"
