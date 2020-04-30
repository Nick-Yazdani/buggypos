using System;
using System.Collections.Generic;

namespace DuplicateCode
{
    class Category
    {
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }

        public Category(string name)
        {
            Name = name;
            Tasks = new List<Task>();
        }
    }

    class Task
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool Highlighted { get; set; }
        public string Due { get; set; }

        /*public Task(string name, int priority, bool highlighted, string due)
        {
            Name = name;
            Priority = priority;
            Highlighted = highlighted;
            Due = due;
        }*/

        public Task(string name, bool highlighted)
        {
            Name = name;
            Highlighted = highlighted;
        }
    }
    class DuplicateCode
    {
        static void InitCategories(List<Category> categories)
        {
            Console.WriteLine("Please enter three categories to begin with: ");
            int i = 3;
            while(i > 0)
            {
                Console.Write(">> ");
                string categoryName = Console.ReadLine();

                if(categoryName == "")
                {
                    Console.WriteLine("Field cannot be left empty");
                    continue;
                }

                Category category = new Category(categoryName);
                categories.Add(category);
                i--;
            }
        }
        static void DrawCategories(List<Category> categories)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(new string(' ', 12) + "CATEGORIES");
            Console.WriteLine(new string(' ', 10) + new string('-', 94));
            Console.Write("{0, 10}|", "item #");

            foreach(var category in categories)
            {
                Console.Write("{0, 30}|", category.Name);
            }

            Console.WriteLine();
            Console.WriteLine(new string(' ', 10) + new string('-', 94));
        }

        static int Choose()
        {
            Console.WriteLine("Enter your menu choice: ");
            Console.WriteLine("1. Add a category");
            Console.WriteLine("2. Delete an existing category");
            Console.WriteLine("3. Add a task to a category");
            Console.WriteLine("4. Delete an existing task");
            Console.WriteLine("5. Change task priority");
            Console.WriteLine("6. Move a task from one category to another");
            Console.WriteLine("7. Highlight a task");
            Console.WriteLine("8. Quit");

            int choice = Convert.ToInt32(Console.ReadLine());

            return choice;
        }

        static void DeleteCategory(List<Category> categories)
        {
            Console.WriteLine("Which category would you like to delete?");

            string categoryToDelete = Console.ReadLine().ToLower();

            foreach(var category in categories)
            {
                if(categoryToDelete == category.Name.ToLower())
                {
                    categories.Remove(category);
                    break;
                }
            }
        }

        static void AddCategory(List<Category> categories)
        {
            if(categories.Count == 3)
            {
                Console.WriteLine("Sorry, there are already three categories.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Please enter the name of the category you would like to add: ");
            string categoryName = Console.ReadLine();
            Category category = new Category(categoryName);
            categories.Add(category);
        }

        static void AddTask(List<Category> categories)
        {
            Console.WriteLine("Please enter the name of the category you would like to add the task to: ");
            string categoryName = Console.ReadLine().ToLower();

            Console.WriteLine("Please enter the name of the task you would like to add: ");
            string taskName = Console.ReadLine();
            Task task = new Task(taskName, false);

            foreach (var category in categories)
            {
                if (categoryName == category.Name.ToLower())
                {
                    category.Tasks.Add(task);
                    break;
                }
            }
        }

        static void DeleteTask(List<Category> categories)
        {
            Console.WriteLine("Please enter the name of the category you would like to delete the task from: ");
            string categoryName = Console.ReadLine().ToLower();

            Console.WriteLine("Please enter the name of the task you would like to delete: ");
            string taskName = Console.ReadLine().ToLower();

            foreach (var category in categories)
            {
                if (categoryName == category.Name.ToLower())
                {
                    foreach(var task in category.Tasks)
                    {
                        if(taskName == task.Name.ToLower())
                        {
                            category.Tasks.Remove(task);
                            return;
                        }
                    }
                }
            }
        }

        static void DrawTasks(List<Category> categories)
        {
            /*Console.Write("{0,10}|", i);

            if (tasksIndividual.Length > i)
            {
                Console.Write("{0,30}|", tasksIndividual[i]);
            }
            else
            {
                Console.Write("{0,30}|", "N/A");
            }*/

            foreach(var category in categories)
            {
                if(category.Tasks.Count > 0)
                {
                    foreach(var task in category.Tasks)
                    {
                        Console.Write("{0,30}|", task.Name);
                    }
                }

                else
                {
                    Console.Write("{0,30}|", "N/A");
                }
            }
            
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            string[] tasksIndividual = new string[0], tasksWork = new string[0], tasksFamilly = new string[0];

            List<Category> categories = new List<Category>();

            InitCategories(categories);

            while (true)
            {
                int choice = Choose();

                switch(choice)
                {
                    case 1:
                        AddCategory(categories);
                        break;

                    case 2:
                        DeleteCategory(categories);
                        break;
                    case 3:
                        AddTask(categories);
                        break;
                    case 4:
                        DeleteTask(categories);
                        break;
                }


                Console.Clear();
                int max = tasksIndividual.Length > tasksWork.Length ? tasksIndividual.Length : tasksWork.Length;
                max = max > tasksFamilly.Length ? max : tasksFamilly.Length;

                DrawCategories(categories);

                DrawTasks(categories);

                /*

                for (int i = 0; i < max; i++)
                {
                    Console.Write("{0,10}|", i);

                    if (tasksIndividual.Length > i)
                    {
                        Console.Write("{0,30}|", tasksIndividual[i]);
                    }
                    else
                    {
                        Console.Write("{0,30}|", "N/A");
                    }

                    if (tasksWork.Length > i)
                    {
                        Console.Write("{0,30}|", tasksWork[i]);
                    }
                    else
                    {
                        Console.Write("{0,30}|", "N/A");
                    }

                    if (tasksFamilly.Length > i)
                    {
                        Console.Write("{0,30}|", tasksFamilly[i]);
                    }
                    else
                    {
                        Console.Write("{0,30}|", "N/A");
                    }
                    Console.WriteLine();
                }

                Console.ResetColor(); Console.WriteLine("\nWhich category do you want to place a new task? Type \'Personal\', \'Work\', or \'Family\'");
                Console.Write(">> "); string listName = Console.ReadLine().ToLower();
                Console.WriteLine("Describe your task below (max. 30 symbols)."); Console.Write(">> ");
                string task = Console.ReadLine(); if (task.Length > 30) task = task.Substring(0, 30);

                string[] goalsIndividualNew = null;
                if (listName == "personal")
                {
                    goalsIndividualNew = new string[tasksIndividual.Length + 1];
                    for (int j = 0; j < tasksIndividual.Length; j++) goalsIndividualNew[j] = tasksIndividual[j];
                    goalsIndividualNew[goalsIndividualNew.Length - 1] = task; tasksIndividual = goalsIndividualNew;
                }
                else if (listName == "work")
                {
                    string[] goalsWorkNew = new string[tasksWork.Length + 1];
                    for (int j = 0; j < tasksWork.Length; j++) { goalsWorkNew[j] = tasksWork[j]; }
                    goalsWorkNew[goalsWorkNew.Length - 1] = task; tasksWork = goalsWorkNew;
                }
                else if (listName == "family")
                {
                    string[] goalsFamillyNew = new string[tasksFamilly.Length + 1];
                    for (int j = 0; j < tasksFamilly.Length; j++)
                    {
                        goalsFamillyNew[j] = tasksFamilly[j];
                    }
                    goalsFamillyNew[goalsFamillyNew.Length - 1] = task;
                    tasksFamilly = goalsFamillyNew;
                    
                }
                */
            }
        }
    }
}
