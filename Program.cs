using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextTableBuilder;
using System.Diagnostics;
using System.Drawing;

namespace StringUpTable_Executable
{
    /// <summary>
    /// This class is an example code for creating tables, writing records to them and writing the resulting stringBuilder instance to a textfile.
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {
            //Create an instance of the textDocumentFormat. This example creates a textString of 150 line of 150 characters
            TextDocumentFormat textDocumentFormat = new TextDocumentFormat(150, 150);
            //Create an instance of the StringUpTable. The table to be draw here has 5 columns, 4 rows and 20 character per row. It will be drawn from the point (x = 0, y =15)
            //The path of the textFile should alread exist, change this in your code to an empty text file on your computer.
            StringUpTable table = new StringUpTable("C://Users//emmax//Desktop//Programming//TextTableBuilder//Table.txt", 5, 4, 20,new Point(0,15));           
            table.TableString = table.DrawTableString();//Draw the table
            
            //Write an instance of the content to be written to the table.
            StringUpTableContent stringUpContent = new StringUpTableContent(table,3);
            stringUpContent.AddRowContent(0, "Part_Number", "Description", "Quantity", "Unit_Price", "Total");
            stringUpContent.AddRowContent(1, "MAX232", "Conversion", "15", "$0.3", "$4.5");

            //Write the table to the string that will be written to the textFile.
            textDocumentFormat = table.WriteTableToTextDocumentFormat(textDocumentFormat);            
            table.WriteTextDocumentStringToFile(textDocumentFormat);//Write to the textFile.

            Process.Start(table.FilePath);//Run the textFile to see results.

        

            Console.ReadLine();
        }
    }
}
