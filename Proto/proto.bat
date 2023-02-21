@echo on

set out_path=.\proto2cs
set protogen_path=.\publish
set unityPath=.\..\Assets\GameScript\Runtime\JIT\Proto

rem 查找文件  
for /f "delims=" %%i in ('dir /b ".\*.proto"') do echo %%i  

rem 删除文件

del .\proto2cs\*.cs
del *.cs

cd %protogen_path%
del *.cs
del *.proto
del *.protodesc
del *.txt


cd ..
copy *.proto %protogen_path%

cd %protogen_path%
for /f "delims=" %%i in ('dir /b/a "*.proto"') do protogen --csharp_out=.\   %%i  

rem 复制
cd ..
copy %protogen_path%\*.cs %out_path%

cd %protogen_path%
del *.cs
del *.proto


cd..

del %unityPath%\*.cs
del *.cs
copy .\proto2cs\*.cs  %unityPath%

rem pause

for /l %%i in (5,-1,0) do (
cls
echo. Automatic shutdown timing %%i...
ping 127.1 -n 2 >nul
)
exit
