﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Marks { get; set; }
}



namespace StudentMgtSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            // Read the input data from a file as one string.
            string strdata = System.IO.File.ReadAllText(@"H:\StudentMgtSystem\StudentDataInput.txt");
            // Getting each line as a rowdata to finally spilt them into columns
            string[] rowdata = strdata.Split("\r\n\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


            //Delit Characters can be changed as per the input requirement
            //char[] delimiterChars = { ',', '.', ':', '\t' };
            char[] delimiterChars = { ',', '\t' };

            // Saving each row as a student record by grepping the student data like FirstName, LastName and Marks.
            // Scope is there to add more fields to the class.
            List<Student> studentlist = new List<Student>();

            // Printing the input data to the console:
            System.Console.WriteLine("***************************** INPUT DATA ******************************");
            foreach (string studentrecord in rowdata)
            {
                System.Console.WriteLine(studentrecord);

                //Spilts each rowdata as per the delimiter provided and saves them to the student object
                Student studentobject = new Student();
                string[] splitdata = studentrecord.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                studentobject.FirstName = splitdata[0].Replace(" ", string.Empty);
                studentobject.LastName = splitdata[1].Replace(" ", string.Empty);
                studentobject.Marks = int.Parse(splitdata[2].Replace(" ", string.Empty));
                studentlist.Add(studentobject);
            }
            System.Console.WriteLine("***********************************************************************\n\n");


            //The sorted data will be copied to the new var list 
            var sortedlist = studentlist.OrderByDescending(s => s.Marks).ThenBy(s1 => s1.FirstName);

            //Writing the sported output data to both console and output file.
            System.Console.WriteLine("**************************** SORTED DATA ******************************");

            // Finally writing the sorted data to the output file.
            // NOTE: It is always advisable to not to use FileStream for text files because it writes bytes, but StreamWriter
            // encodes the output as text.
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"H:\StudentMgtSystem\StudentDataInput-graded.txt"))
            {
                foreach (Student s in sortedlist)
                {
                    string sortedrecord = s.FirstName + ", " + s.LastName + ", " + s.Marks;
                    System.Console.WriteLine(sortedrecord);
                    file.WriteLine(sortedrecord);
                }
            }
            System.Console.WriteLine("***********************************************************************\n\n");

            //Waiting for the user input to exit
            System.Console.WriteLine("The same sorted data is also copied to the file:");
            System.Console.WriteLine("---->H:\\StudentMgtSystem\\StudentDataInput-graded.txt<----\n\n");
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();


            /*          Sample code tried initially to solve the problem
                        // Example #1
                        // Read the file as one string.
                        string text = System.IO.File.ReadAllText(@"H:\StudentMgtSystem\Sort.txt");

                        // Display the file contents to the console. Variable text is a string.
                        System.Console.WriteLine("Contents of Sort.txt = {0}", text);

                        // Read each line of the file into a string array. Each element
                        // of the array is one line of the file.
                        string[] lines = System.IO.File.ReadAllLines(@"H:\StudentMgtSystem\Sort.txt");

            
                        // Display the file contents by using a foreach loop.
                        System.Console.WriteLine("Contents of Sort.txt = ");
                        foreach (string line in lines)
                        {
                            // Use a tab to indent each line of the file.
                            Console.WriteLine("\t" + line);
                        }
            
                        //Example #2
                        int counter = 0;
                        string line;

                        // Read the file and display it line by line.
                        System.IO.StreamReader file =
                           new System.IO.StreamReader("H:\\StudentMgtSystem\\Sort.txt");

                        List<int> list = new List<int>();

                        while ((line = file.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            list.Add(int.Parse(line));
                            counter++;
                        }

                        int[] arr = list.ToArray();

                        Array.Sort(arr);

                        foreach (int item in arr)
                        {
                            Console.WriteLine(item);
                        }

                        file.Close();
            
            */
        }
    }
}