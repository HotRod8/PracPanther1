using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP_Library.Models;
using PP_Library.Services;

namespace PP_CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Project> work = new List<Project>();
            ClientMenu();
            ProjectMenu(work);
        }

        static void ClientMenu()
        {
            while (true)
            {
                Console.WriteLine("C. Create a Client");
                Console.WriteLine("R. List Clients");
                Console.WriteLine("U. Update a Client");
                Console.WriteLine("D. Delete a Client");
                Console.WriteLine("Q. Quit");

                var choice = Console.ReadLine() ?? string.Empty;

                if (choice.Equals("C", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Create stuff for all required Client properties.
                    Console.WriteLine("Id: ");
                    var Id = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Name: ");
                    var name = Console.ReadLine();

                    Console.WriteLine("Notes: ");
                    var notes = Console.ReadLine();

                    Console.WriteLine("Start Date: ");
                    var open = DateTime.Today;

                    ProjLinker.Current.Add(
                        new Client
                        {
                            Id = Id,
                            Name = name ?? "John/Jane Doe",
                            Notes = notes,
                            IsActive = true,
                            OpenDate = open
                        }
                    );

                }
                else if (choice.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Read stuff
                    ProjLinker.Current.Read();
                }

                else if (choice.Equals("U", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Update stuff
                    //About the same as delete
                    ProjLinker.Current.Read();
                    Console.WriteLine("Which of these above accounts do you wish to update?");
                    var updateChoice = int.Parse(Console.ReadLine() ?? "0");
                    ProjLinker.Current.Edit(updateChoice);
                }
                else if (choice.Equals("D", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Delete stuff
                    Console.WriteLine("Which Client should be deleted?");
                    ProjLinker.Current.Read();
                    var deleteChoice = int.Parse(Console.ReadLine() ?? "0");
                    ProjLinker.Current.Delete(deleteChoice);
                }
                else if (choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase)) { break; }
                else { Console.WriteLine("Sorry, that functionality isn't supported"); }
            }
            //End of Client Menu
        }

        static void ProjectMenu(List<Project> work)
        {
            //var myProjLinker = ProjLinker.Current
            while (true)
            {
                Console.WriteLine("C. Create a Project");
                Console.WriteLine("R. List Projects");
                Console.WriteLine("U. Update a Project");
                Console.WriteLine("D. Delete a Project");
                Console.WriteLine("Q. Quit");

                var choice = Console.ReadLine() ?? string.Empty;

                if (choice.Equals("C", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Create stuff
                    Console.WriteLine("Your Account Id: ");
                    var myclient = ProjLinker.Current.Get(int.Parse(Console.ReadLine() ?? "0"));
                 
                    Console.WriteLine("Project Id: ");
                    int id = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Short Name: ");
                    string shname = Console.ReadLine() ?? string.Empty;

                    Console.WriteLine("Long Name: ");
                    string lgname = Console.ReadLine() ?? string.Empty;

                    //Option 1
                    //var myProject = new Project { Id = Id, Name = name };

                    //Option 2 - more convenient
                    work.Add(
                        new Project
                        {
                            Id = id,
                            //Aggregate Property
                            ClientId = myclient.Id,
                            ShortName = shname ?? "John/Jane Doe",
                            LongName = lgname ?? "Roderick/Rosalina Shaw",
                            IsActive = true,
                            OpenDate = DateTime.Today
                        }
                    );
                    if (myclient.Projects == null) { myclient.Projects = new List<Project>(); }
                    myclient.Projects.Add(work.FirstOrDefault(e => e.Id == id));

                }
                else if (choice.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Read stuff
                    foreach(var project in work)
                    { Console.WriteLine(project.ToString()); }
                    //work.ForEach(Console.WriteLine);
                }

                else if (choice.Equals("U", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Update stuff
                    //About the same as delete
                    foreach (var project in work)
                    { Console.WriteLine(project.ToString()); }
                    Console.WriteLine("Which of these above accounts do you wish to update?");
                    var updateChoice = int.Parse(Console.ReadLine() ?? "0");

                    var ProjectToUpdate = work.FirstOrDefault(x => x.Id == updateChoice);
                    if (ProjectToUpdate != null)
                    {
                        Console.WriteLine("What is the Project's updated short name?");
                        ProjectToUpdate.ShortName = Console.ReadLine() ?? "John/Jane Doe";
                        Console.WriteLine("What is the Project's updated long name?");
                        ProjectToUpdate.LongName = Console.ReadLine() ?? "Roderick/Rosalina Shaw";
                    }
                }
                else if (choice.Equals("D", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Delete stuff
                    Console.WriteLine("Which Project should be deleted?");
                    foreach (var project in work)
                    { Console.WriteLine(project.ToString()); }
                    var deleteChoice = int.Parse(Console.ReadLine() ?? "0");

                    var ProjectToRemove = work.FirstOrDefault(x => x.Id == deleteChoice);
                    if (ProjectToRemove != null)
                    {
                        work.Remove(ProjectToRemove);
                    }
                }
                else if (choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase)) { break; }
                else { Console.WriteLine("Sorry, that functionality isn't supported"); }
            }
            //End of Project Menu
        }
    }

}
