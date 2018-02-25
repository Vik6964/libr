After pulling code from BitBuket

if you're having problems in compilation and running the project. You may need to follow these steps to run the project successfully.

1. Install the missing Nuget Packages.
   
   - Atlassion.SDK
   - JIRA RESR Client
   - XMLRPCNET

sometimes they seems installed, but does not work properly, Uninstall them and install all again in the project.



// =========================== HOW TO INSTALL PACKAGES =========================== //

 - Right click on the solution in the solution explorer and Click 'Manage Nuget Packages for Solution'

 - Select 'Online' tab in the left pane.
 - Search packages
 - Intall.
 - After installation you can see the installed packages in the solution by clicking on the 'Installed Packages' tab in the left pane.


Now the project will build successfully and you can debug the project by click on start.



// =========================== HOW TO START / RUN THE PROJECT =========================== //

you may face the problem of having visual studio Promtp window on start, saying

"A project with an Output Type of Class Library cannot be started directly."

To avoid this

 - Go to DEBUG (in top menu) -> Project Properties (Last item), as i have project name JiraAddin. so its like JiraAddin Properties
 - Select Debug from left pane.
    
   - Select radio button 'Start Debug Program' in Start Action Section and add  
     
     C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe

     in the text field.


   - In Start Options Section Put /resetaddin Yourprojectname.Connect in the text area after Command line arguments.

     ---- Like i have /resetaddin JiraAddin.Connect ---- OR ---- /resetaddin FCT.Connect ----

   - In Working Directory Text Field put 'C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\'



Save the settings. Clean and Rebuild the solution.

     and here you go....... :)


