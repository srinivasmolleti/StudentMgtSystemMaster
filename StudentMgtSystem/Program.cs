using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//Student class is to load the student details in the obejct.
//Provison to add more fields in the future.
public class Student
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Marks { get; set; }
}



namespace StudentMgtSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            // Read the input data from a file as one string.
            System.Console.WriteLine("*************************************************************************");
            System.Console.WriteLine("***   Welcome To The Transmax Applicatio Student Information System   ***");
            System.Console.WriteLine("***   Enter the input file Path:                                      ***");
            System.Console.WriteLine("***   Example:H:\\StudentMgtSystem\\StudentData.txt                     ***");
            System.Console.WriteLine("*************************************************************************\n");
            
            string inputfilePath = System.Console.ReadLine();

            //Creating the output file path using the input file path.          
            string fileName = Path.GetFileName(inputfilePath);
            string fileExt = Path.GetExtension(inputfilePath);
            string fileNameWoExt = Path.GetFileNameWithoutExtension(inputfilePath);
            string dirPath = Path.GetDirectoryName(inputfilePath);
            string outputfilePath = dirPath + "\\" + fileNameWoExt + "-graded" + fileExt;

            string strdata = System.IO.File.ReadAllText(inputfilePath);
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

                studentobject.LastName = splitdata[0].Replace(" ", string.Empty);
                studentobject.FirstName = splitdata[1].Replace(" ", string.Empty);
                studentobject.Marks = int.Parse(splitdata[2].Replace(" ", string.Empty));
                studentlist.Add(studentobject);
            }
            System.Console.WriteLine("***********************************************************************\n\n");


            //The sorted data will be copied to the new var list 
            var sortedlist = studentlist.OrderByDescending(s => s.Marks).ThenBy(s1 => s1.LastName).ThenBy(s2 => s2.FirstName);

            //Writing the sported output data to both console and output file.
            System.Console.WriteLine("**************************** SORTED DATA ******************************");

            // Finally writing the sorted data to the output file.
            // NOTE: It is always advisable to not to use FileStream for text files because it writes bytes, but StreamWriter
            // encodes the output as text..
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(outputfilePath))
            {
                foreach (Student s in sortedlist)
                {
                    string sortedrecord = s.LastName + ", " + s.FirstName + ", " + s.Marks;
                    System.Console.WriteLine(sortedrecord);
                    file.WriteLine(sortedrecord);
                }
            }
            System.Console.WriteLine("***********************************************************************\n\n");

            //Waiting for the user input to exit
            System.Console.WriteLine("The same sorted data is also copied to the file:");
            System.Console.WriteLine("---->"+outputfilePath+"<----\n\n");
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
            
                        //Example #3, the most effective code:
                        string[] rows = File.ReadAllLines(inputfilePath);
                        var data = rows.Skip(1);
                        var sorted = data.Select(row => new
                        {
                            FirstName = row.Split(',')[0],
                            LastName = row.Split(',')[1],
                            Grade = Int32.Parse(row.Split(',')[2]),
                            Row = row
                        })
                                    .OrderByDescending(x => x.Grade)
                                    .ThenBy(x => x.FirstName)
                                    .ThenBy(x => x.LastName)
                                    .Select(x => x.Row);
                        File.WriteAllLines(outputfilePath, rows.Take(1).Concat(sorted));             
            */
        }
    }
}