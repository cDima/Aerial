#powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('.\bin\all', '..\'); }"
Copy-Item 'Aerial.scr' '.\Package\Aerial.scr';
Copy-Item 'eerial.scr' '.\Package\Aerial.scr';
Add-Type -A 'System.IO.Compression.FileSystem';
[IO.Compression.ZipFile]::CreateFromDirectory('.\bin\all', '..\');