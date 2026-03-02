# StoreLogo.png が無い場合に 50x50 のプレースホルダー画像を生成する
$dir = $PSScriptRoot
$path = Join-Path $dir "StoreLogo.png"
if (Test-Path $path) { Write-Host "StoreLogo.png already exists."; exit 0 }
Add-Type -AssemblyName System.Drawing
$b = New-Object System.Drawing.Bitmap(50, 50)
$g = [System.Drawing.Graphics]::FromImage($b)
$g.Clear([System.Drawing.Color]::FromArgb(100, 100, 120))
$g.Dispose()
$b.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
$b.Dispose()
Write-Host "Created StoreLogo.png"
