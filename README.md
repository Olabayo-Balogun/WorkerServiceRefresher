# WorkerServiceRefresher
A .Net Core Worker Service refresher using codealong video gotten from Tim Correy on Youtube. I intend to add more comments to make it easier to understand for myself and anyone who stumbles upon it.

It runs on .Net 3.1 runtime
I created this project to check if afrotada.com (a website I created) is running.

### CREATING THE SERVICE

Step 1: Publish the application by right-clicking the project and clicking on “Publish”

Step 2: Publish the project to a folder.
NOTE: It is advisable you use the same file path as the log file.

Step 3: Register the service by opening PowerShell and running the code snippet shown below. 
NOTE: It is important you run PowerShell as an administrator.
sc.exe create WebsiteStatus binpath= C:\Users\Public\Documents\WorkerService\WebsiteStatus.exe start= auto

WHERE:	sc.exe indicates the type of program you want to run

		create is used to create the project

		WebsiteStatus is the specified name of the application (you’re advised to make sure it matches the name of your Visual Studio project but normally, it’s up to you)

		binpath= specifies the location where the application should be created

		C:\Users\Public\Documents\WorkerService\WebsiteStatus.exe specifies the location of the created application

		start= auto tells the project to automatically run.

Step 4: search for “services” in the search box in the Windows taskbar and check for the newly created service.

Step 5: Right-click the service and click “Start”.


### DELETING THE SERVICE

Step 1: Stop the service in the “Service” feature of your windows, the one you found by searching on your taskbar.

Step 2: Run the code snippet below in your PowerShell (that should be run as Administrator)
sc.exe delete WebsiteStatus


WHERE: WebsiteStatus is the name of the worker service.
