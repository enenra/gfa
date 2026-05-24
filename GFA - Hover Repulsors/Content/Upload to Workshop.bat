set SEInstallDir="C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers"
for %%I in (.) do set ParentDirName=%%~nxI
%SEInstallDir%\Bin64\SEWorkshopTool.exe --upload --compile --mods "%ParentDirName%" --exclude .bat .psd .md --thumb thumb.png --description description.md --message patch_notes.md --tags mod block gfa
pause