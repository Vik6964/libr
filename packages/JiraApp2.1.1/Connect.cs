using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using Atlassian.Jira;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
namespace JiraAddin
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
	public class Connect : IDTExtensibility2, IDTCommandTarget
	{
		/// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
		public Connect()
		{
		}

		/// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;
			if(connectMode == ext_ConnectMode.ext_cm_UISetup)
			{
				object []contextGUIDS = new object[] { };
				Commands2 commands = (Commands2)_applicationObject.Commands;
                string toolsMenuName = "Project and Solution Context Menus";

				//Place the command on the tools menu.
				//Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
                Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)[toolsMenuName];
                CommandBarPopup toolsPopup = (CommandBarPopup)menuBarCommandBar.Controls["Item"];

				//This try/catch block can be duplicated if you wish to add multiple commands to be handled by your Add-in,
				//  just make sure you also update the QueryStatus/Exec method to include the new command names.
				try
				{
					//Add a command to the Commands collection:
                    Command command = commands.AddNamedCommand2(_addInInstance, "Push", "Push To JIRA", "Executes the command for TestAddin", true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStyleText, vsCommandControlType.vsCommandControlTypeButton);
                    Command command2 = commands.AddNamedCommand2(_addInInstance, "Fetch", "Fetch From JIRA", "Executes the command for TestAddin", true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStyleText, vsCommandControlType.vsCommandControlTypeButton);
                    Command command3 = commands.AddNamedCommand2(_addInInstance, "All", "Fetch All From JIRA", "Executes the command for TestAddin", true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStyleText, vsCommandControlType.vsCommandControlTypeButton);

					//Add a control for the command to the Item context menu:
					if((command != null) && (toolsPopup != null))
					{
						command.AddControl(toolsPopup.CommandBar, 1);
					}
                    if ((command2 != null) && (toolsPopup != null))
                    {
                        command2.AddControl(toolsPopup.CommandBar, 1);
                    }
                    if ((command3 != null) && (toolsPopup != null))
                    {
                        command3.AddControl(toolsPopup.CommandBar, 1);
                    }
				}
				catch(System.ArgumentException)
				{
					//If we are here, then the exception is probably because a command with that name
					//  already exists. If so there is no need to recreate the command and we can 
                    //  safely ignore the exception.
				}
			}
		}

		/// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		/// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
		}

		/// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
		}
		
		/// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
		/// <param term='commandName'>The name of the command to determine state for.</param>
		/// <param term='neededText'>Text that is needed for the command.</param>
		/// <param term='status'>The state of the command in the user interface.</param>
		/// <param term='commandText'>Text requested by the neededText parameter.</param>
		/// <seealso class='Exec' />
		public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
		{
            UIHierarchy uiHierarchy = (UIHierarchy)_applicationObject.Windows.Item(Constants.vsWindowKindSolutionExplorer).Object;
			if(neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
			{
				if(commandName == "JiraAddin.Connect.Push")
				{
                    foreach (UIHierarchyItem item in (Array)uiHierarchy.SelectedItems)
                    {
                        if (item.Object is ProjectItem)
                        {
                            ProjectItem File = item.Object as ProjectItem;
                            string FileExt = File.Name.Substring(File.Name.LastIndexOf('.'));
                            if (FileExt.Equals(".feature"))
                            {
                                status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                            }
                            else
                                status = (vsCommandStatus)vsCommandStatus.vsCommandStatusInvisible;
                        }
                        else{
                            status = (vsCommandStatus)vsCommandStatus.vsCommandStatusInvisible;
                        }
                    }

					return;
				}
                if (commandName == "JiraAddin.Connect.Fetch")
                {
                    foreach (UIHierarchyItem item in (Array)uiHierarchy.SelectedItems)
                    {
                        if (item.Object is ProjectItem)
                        {
                            ProjectItem File = item.Object as ProjectItem;
                            string FileExt = File.Name.Substring(File.Name.LastIndexOf('.'));
                            if (FileExt.Equals(".feature"))
                            {
                                status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                                return;
                            }
                            else
                            {
                                status = (vsCommandStatus)vsCommandStatus.vsCommandStatusInvisible;
                                return;
                            }
                        }
                        else
                        {
                            status = (vsCommandStatus)vsCommandStatus.vsCommandStatusInvisible;
                            return;
                        }
                    }
                    return;
                }
                if (commandName == "JiraAddin.Connect.Fetch")
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }
			}
		}

		/// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
		/// <param term='commandName'>The name of the command to execute.</param>
		/// <param term='executeOption'>Describes how the command should be run.</param>
		/// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
		/// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
		/// <param term='handled'>Informs the caller if the command was handled or not.</param>
		/// <seealso class='Exec' />
		public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
		{
			handled = false;
			if(executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
			{
				if(commandName == "JiraAddin.Connect.Push")
				{
					handled = true;
                    WriteToJira();
				}
                if (commandName == "JiraAddin.Connect.Fetch")
                {
                    handled = true;
                    ReadFromJira();
                }
                if (commandName == "JiraAddin.Connect.All")
                {
                    handled = true;
                    ReadAllFromJira();
                }

			}
		}
        private void WriteToJira()
        {
            #region Code
            // string FilePath = "D:\\aDnan\\Projects\\JiraAddin\\SpecFlowTest\\SpecFlowTest\\Features\\";
            string FileName = GetFileName();
            if (string.IsNullOrEmpty(FileName))
                return;
            EnvDTE.Project project;
            //get the project
            project = _applicationObject.Solution.Projects.Item(1);
            string FilePath = project.FullName;
            FilePath = FilePath.Substring(0, FilePath.LastIndexOf('\\')) + "\\Features\\";

            try
            {
                IJiraClient jiraclient = new JiraClient(@"https://seventechnology.jira.com", "andy", "L1verp00l1");
                string contents = System.IO.File.ReadAllText(FilePath + FileName);
                int i = contents.IndexOf(":");
                int indexOfFirstLine = contents.IndexOf("\n");
                int indexOfFirstScenario = contents.IndexOf("Scenario:");
                string FeatureName = contents.Substring(i + 2, (indexOfFirstLine - (i + 2)));
                string FeatureDescription = contents.Substring(indexOfFirstLine + 1, (indexOfFirstScenario - (indexOfFirstLine + 1)));

                int count = new Regex("Scenario:").Matches(contents).Count;
                if (count > 1)
                {
                    // Create issue with subtask & empty description on jira.

                    Issue objIssue = jiraclient.CreateIssue("XPL", "Story", FeatureDescription);

                    string OnlyScenarios = contents.Substring(indexOfFirstScenario);
                    string[] Seperator = new string[] { "Scenario:" };
                    List<string> Scenarios = OnlyScenarios.Split(Seperator, StringSplitOptions.None).ToList();
                    Scenarios.RemoveAt(0);
                    foreach (string Scenario in Scenarios)
                    {
                        int indexOfStoryTag = Scenario.IndexOf("@Story");
                        string Summary = Scenario.Substring(1, indexOfStoryTag - 1);
                        Summary = Summary.Remove(Summary.IndexOf("\n"), 1);
                        string Story = Scenario.Substring(indexOfStoryTag + 7);
                        Story = Story.Remove(Story.IndexOf("\n"), 1);
                        try
                        {
                            jiraclient.CreateSubTask("XPL", objIssue.key, "Scenario", Summary, Story);
                        }
                        catch { }
                    }
                }
                else
                {
                    string Scenario = contents.Substring(indexOfFirstScenario + 9);

                    int indexOfStoryTag = Scenario.IndexOf("@Story");
                    
                    string Summary = Scenario.Substring(1, indexOfStoryTag - 1);
                    Summary = Summary.Remove(Summary.IndexOf("\n"), 1);

                    string Story = Scenario.Substring(indexOfStoryTag + 7);
                    Story = Story.Remove(Story.IndexOf("\n"), 1);
                    try
                    {
                       Issue objIssue = jiraclient.CreateIssue("XPL", "Story", Summary, Story);
                    }
                    catch { }
                    // Create issue with description as a scenario in it.
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
            #endregion
        }
        private void ReadFromJira()
        {
            #region Code
            IJiraClient jiraclient = new JiraClient(@"https://seventechnology.jira.com", "andy", "L1verp00l1");

            string FileNameWithOutExt = GetFileNameWithOutExt();
            string FileName = GetFileName();
            if(string.IsNullOrEmpty(FileName))
                return;
            // =========== GET CURRENT PATH OF SOLUTION
            //string currpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //string FilePath = "D:\\aDnan\\Projects\\JiraAddin\\SpecFlowTest\\SpecFlowTest\\Features\\";
            //string TemplatePath = "C:\\deps\\SpecFlow\\ItemTemplates\\SpecFlowFeature\\SpecFlowFeature.vstemplate";
            Issue sissue = null;
            List<Issue> issuelist = null;
            string ParentIssue = FileNameWithOutExt;
            List<Issue> Subtasks = jiraclient.GetSubTasksOfIssue("XPL", ParentIssue, "Scenario").ToList();

            issuelist = jiraclient.GetIssue("XPL", ParentIssue).ToList();
            if (issuelist.Count > 0)
                 sissue = issuelist[0];

            EnvDTE.Project project;
            EnvDTE.ProjectItem features;
            EnvDTE.ProjectItem feature;
            
            //get the project
            project = _applicationObject.Solution.Projects.Item(1);
            
            string FilePath = project.FullName;
            FilePath = FilePath.Substring(0,FilePath.LastIndexOf('\\'))+ "\\Features\\";

            string TemplatePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            TemplatePath = TemplatePath.Substring(0,TemplatePath.LastIndexOf('\\'))+ "\\Template\\SpecFlowFeature.vstemplate";

            //add the folders we want
            try
            {
                features = project.ProjectItems.AddFolder("Features");
                // add the features folder if its not there
            }
            catch
            {
                //if it is there allready we need to get it
                features = project.ProjectItems.Item("Features");
                //features = _applicationObject.Solution.FindProjectItem("Features");
            }
            try
            {
                string contents = string.Empty;
                try
                {
                    feature = features.ProjectItems.AddFromTemplate(TemplatePath, ParentIssue);
                }
                catch (COMException e)
                {
                    contents = File.ReadAllText(FilePath + FileName);
                }
                if (File.Exists(FilePath + FileName))
                {
                    if (Subtasks.Count > 0)
                    {
                        int count = 0;
                        foreach (Issue issue in Subtasks)
                        {
                            if (count == 0)
                            {
                                string Text = "Feature: " + sissue.key + "\n" + issue.fields.summary + "\n\n" + "Scenario: " + issue.fields.summary + "\n" + "@Story\n" + issue.fields.description + "\n\n";
                                File.WriteAllText(FilePath + ParentIssue+".feature", Text);
                                count++;
                            }
                            else
                            {
                                string Text = "Scenario: " + issue.fields.summary + "\n" + "@Story\n" + issue.fields.description + "\n\n";
                                File.AppendAllText(FilePath + ParentIssue+".feature", Text);
                            }
                        }
                    }
                    else
                    {
                        string Text = "Feature: " + sissue.key + "\n" + sissue.fields.summary + "\n\n" + "Scenario: " + sissue.fields.summary + "\n" + "@Story\n" + sissue.fields.description + "\n\n";
                        File.WriteAllText(FilePath + ParentIssue+".feature", Text);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
            #endregion
        }
        private void ReadAllFromJira()
        {
            #region Code
            IJiraClient jiraclient = new JiraClient(@"https://seventechnology.jira.com", "andy", "L1verp00l1");

            // =========== GET CURRENT PATH OF SOLUTION
            //string currpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //string FilePath = "D:\\aDnan\\Projects\\JiraAddin\\SpecFlowTest\\SpecFlowTest\\Features\\";
            //string TemplatePath = "C:\\deps\\SpecFlow\\ItemTemplates\\SpecFlowFeature\\SpecFlowFeature.vstemplate";


            EnvDTE.Project project;
            EnvDTE.ProjectItem features;
            EnvDTE.ProjectItem feature;

            //get the project
            project = _applicationObject.Solution.Projects.Item(1);

            string FilePath = project.FullName;
            FilePath = FilePath.Substring(0, FilePath.LastIndexOf('\\')) + "\\Features\\";

            string TemplatePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            TemplatePath = TemplatePath.Substring(0, TemplatePath.LastIndexOf('\\')) + "\\Template\\SpecFlowFeature.vstemplate";


            List<Issue> allissuelist = jiraclient.GetIssues("XPL","Story").ToList();
            
            foreach (Issue iss in allissuelist)
            {
                Issue sissue = null;
                List<Issue> issuelist = null;
                string ParentIssue = iss.key;
                List<Issue> Subtasks = jiraclient.GetSubTasksOfIssue("XPL", ParentIssue, "Scenario").ToList();

                issuelist = jiraclient.GetIssue("XPL", ParentIssue).ToList();
                if (issuelist.Count > 0)
                    sissue = issuelist[0];



                //add the folders we want
                try
                {
                    features = project.ProjectItems.AddFolder("Features");
                    // add the features folder if its not there
                }
                catch
                {
                    //if it is there allready we need to get it
                    features = project.ProjectItems.Item("Features");
                    //features = _applicationObject.Solution.FindProjectItem("Features");
                }
                try
                {
                    string contents = string.Empty;
                    try
                    {
                        feature = features.ProjectItems.AddFromTemplate(TemplatePath, ParentIssue);
                    }
                    catch (COMException e)
                    {
                        contents = File.ReadAllText(FilePath + ParentIssue + ".feature");
                    }
                    if (File.Exists(FilePath + ParentIssue + ".feature"))
                    {
                        if (Subtasks.Count > 0)
                        {
                            int count = 0;
                            foreach (Issue issue in Subtasks)
                            {
                                if (count == 0)
                                {
                                    string Text = "Feature: " + sissue.key + "\n" + issue.fields.summary + "\n\n" + "Scenario: " + issue.fields.summary + "\n" + "@Story\n" + issue.fields.description + "\n\n";
                                    File.WriteAllText(FilePath + ParentIssue + ".feature", Text);
                                    count++;
                                }
                                else
                                {
                                    string Text = "Scenario: " + issue.fields.summary + "\n" + "@Story\n" + issue.fields.description + "\n\n";
                                    File.AppendAllText(FilePath + ParentIssue + ".feature", Text);
                                }
                            }
                        }
                        else
                        {
                            string Text = "Feature: " + sissue.key + "\n" + sissue.fields.summary + "\n\n" + "Scenario: " + sissue.fields.summary + "\n" + "@Story\n" + sissue.fields.description + "\n\n";
                            File.WriteAllText(FilePath + ParentIssue + ".feature", Text);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            #endregion
        }
        private string GetFileName()
        {
            UIHierarchy uiHierarchy = (UIHierarchy)_applicationObject.Windows.Item(Constants.vsWindowKindSolutionExplorer).Object;
            foreach (UIHierarchyItem item in (Array)uiHierarchy.SelectedItems)
            {
                if (item.Object is ProjectItem)
                {
                    ProjectItem File = item.Object as ProjectItem;
                    string FileExt = File.Name.Substring(File.Name.LastIndexOf('.'));
                    if (FileExt.Equals(".feature"))
                    {
                        return File.Name;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                    return string.Empty;
            }
            return string.Empty;

        }
        private string GetFileNameWithOutExt()
        {
            UIHierarchy uiHierarchy = (UIHierarchy)_applicationObject.Windows.Item(Constants.vsWindowKindSolutionExplorer).Object;
            foreach (UIHierarchyItem item in (Array)uiHierarchy.SelectedItems)
            {
                if (item.Object is ProjectItem)
                {
                    ProjectItem File = item.Object as ProjectItem;
                    string FileExt = File.Name.Substring(File.Name.LastIndexOf('.'));
                    if (FileExt.Equals(".feature"))
                    {
                        string[] FileName = File.Name.Split('.');
                        return FileName[0];
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                return string.Empty;
            }
            return string.Empty;
        }
		private DTE2 _applicationObject;
		private AddIn _addInInstance;
	}
}