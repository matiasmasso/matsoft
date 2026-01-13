To update Matsched:

On the server:

launch Administrator Command Prompt (bottom toolbar search -> Cmd -> rightclick -> Run as administrator)

stop service:
	net stop matschedservice

uninstall service:
	cd C:\public\matsoft\matsched
	installutil -u matschedservice.exe

copy new files:
	copy all files from developer machine at C:\Users\matia\source\Workspaces\MMO\MatSchedService\MatSchedService\bin\Release\*.*
	paste them all in server C:\public\matsoft\matsched

reinstall service:
	installutil matschedservice.exe

restart service
	net start matschedservice (log on as Local System Account)
