This is a method of backdooring keepass via a malicious plugin. 
The idea was a cross platform way of maintaining persistance.
Works on both Linux and Windows. 
First version just embedded MSF shellcode inside. 
This new version skips that and utilizes built in functionality. 

Build with visual studio. If you want 'plgx' output which is keepass's
internal plugin format, run 
`KeePass.exe --plgx-create` in the directory of the solution file. 
For more info, see https://keepass.info/help/v2_dev/plg_index.html
Also see the 'Builder Exe' folder.

To load a plugin, you can use either the 'Plugins' folder in the 
installation folder, or the user's profile folder under 'keepass\plugins'.