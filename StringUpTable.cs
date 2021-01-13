using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace TextTableBuilder
{
    /// <summary>
    /// This class supports the drawing of a table layout to a textfile.
    /// </summary>
    public class StringUpTable
    {
        /// <summary>
        /// Stores the number columns in the table.
        /// </summary>
        public int ColumnCount { get; set; }

        /// <summary>
        /// Stores the location of textFile the table will be drawn to.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Returns the number of horizontal lines that border the table
        /// </summary>
        public int HorizontalTableBordersCount { get { return (2*RowCount) + 1; } }
        
        /// <summary>
        /// Stores the point in the textDocumentString where the drawing of the table will begin. New lines are indexed at Origin.X
        /// </summary>
        public Point Origin { get; set; }

        /// <summary>
        /// Stores the number of rows in the table. The header row is included in the column count.
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Returns the number of characters in one line of the table.
        /// </summary>
        public int TableCharactersPerLine { get { return ((ColumnCount - 1) * TableCharactersPerRow) + TableCharactersPerRow + 1; } }
        
        /// <summary>
        /// Stores the number of characters in one row of the table.
        /// </summary>
        public int TableCharactersPerRow { get; set; }
        
        /// <summary>
        /// Stores the string that is the table.
        /// </summary>
        public StringBuilder TableString { get; set; }

        /// <summary>
        /// Returns TableString as a character array. 
        /// </summary>
        public char[][] TableStringArray
        {
            get
            {
                return (from string s in TableString.ToString().Split('\n')
                        select s.ToCharArray()).ToArray();
            }
        }

        /// <summary>
        /// Stores the number of spaces between the vertical table border and the first character written to a row.
        /// </summary>
        public int RowIndentation { get; set; }

        /// <summary>
        /// Returns the number of vertical lines that border the table
        /// </summary>
        public int VerticalTableBordersCount { get { return ColumnCount + 1; } }

        /// <summary>
        /// Constructor. Creates new StringUpTable object.
        /// </summary>
        /// <param name="filePath">Provides the location of the textfile the table is written to.</param>
        /// <param name="columncount">Provides the number of columns in the table</param>
        /// <param name="rowcount">Provides the number of rows in the table, inclusive of the header row.</param>
        /// <param name="tableCharactersPerRow">Provides the point at which the drawing of the table will begin.</param>
        /// <param name="origin"></param>
        public StringUpTable(string filePath, int columncount,int rowcount, int tableCharactersPerRow, Point origin)
        {
            FilePath = filePath;
            ColumnCount = columncount;
            RowCount = rowcount;
            TableCharactersPerRow = tableCharactersPerRow;
            Origin = origin;
        }

        /// <summary>
        /// Writes the table layout to string.
        /// </summary>
        /// <returns>Returns the layout of the table.</returns>
        public StringBuilder DrawTableString()
        {
            StringBuilder tableString = new StringBuilder("");
            tableString.Capacity = GetTableStringCapacity();

            for(int i = 0;i<HorizontalTableBordersCount;i++)
            {
                for(int j = 0;j<ColumnCount;j++)
                {
                    for(int k = 0;k<TableCharactersPerRow;k++)
                    {
                        if(k == 0)
                        {
                            tableString.Append((i % 2 == 0) ? " " : "|");
                        }
                        else
                        {
                            tableString.Append((i % 2 == 0) ? "-" : " ");
                        }
                    }

                    if (j == ColumnCount - 1) { tableString.Append((i % 2 == 0) ? " " : "|"); }//Appends the space or | character at the last vertical table border
                }
                tableString.Append((i !=HorizontalTableBordersCount-1)?"\n":"");
                
            }
            return tableString;
        }

        /// <summary>
        /// Calculates and returns the number of characters in the table, inclusive of the table layout and table record content.
        /// </summary>
        /// <returns></returns>
        private int GetTableStringCapacity()
        {
            int capacity = ((TableCharactersPerLine+ 1)*HorizontalTableBordersCount)-1;
            //The +1 is the newline character
            //The -1 removes the newline character that shouldnt be at the end of the last row

            return capacity;
        }

        /// <summary>
        /// Writes the textDocumentString of the textDocumentFormat to the filePath at the creation of the StringUpTable Instance.
        /// </summary>
        /// <param name="textDocumentFormat">Represents the object containing the string that is written to the textfile.</param>
        public void WriteTextDocumentStringToFile(TextDocumentFormat textDocumentFormat)
        {
            using(FileStream fs = new FileStream(FilePath,FileMode.Open,FileAccess.Write))
            {                
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {                    
                    sw.Write(textDocumentFormat.TextDocumentString.ToString());

                }
            }
            
        }

        /// <summary>
        /// Writes the tableString of a StringUpTable to the textDocumentString of a textDocumentFormat.
        /// </summary>
        /// <param name="textDocumentFormat">Represents the object containing the string that is written to the textfile</param>
        /// <returns></returns>
        public TextDocumentFormat WriteTableToTextDocumentFormat(TextDocumentFormat textDocumentFormat)
        {
       
            char[][] charArray = (from string s in textDocumentFormat.TextDocumentString.ToString().Split('\n')
                                  select s.ToCharArray()).ToArray();

           
            int tableStringIndex = 0;
            int yBoundary = Origin.Y + HorizontalTableBordersCount;
            int xBoundary = Origin.X + TableCharactersPerLine;
            for (int y = Origin.Y;y<yBoundary;y++)
            {
                for(int x = Origin.X;x<xBoundary;x++)
                {
                    charArray[y][x] = TableString.ToString()[tableStringIndex];
                    tableStringIndex++;
                    if (tableStringIndex!=TableString.Capacity)
                    {                     
                        
                        tableStringIndex += (TableString.ToString()[tableStringIndex] == '\n') ? 1 : 0;
                        //Do not increment tablestringIndex after the last character has been written
                    }
                    else { }
                }
                
            }                       

            textDocumentFormat.TextDocumentString = ConvertToStringBuilder(charArray, textDocumentFormat.TextDocumentString.Capacity);
            return textDocumentFormat;
        }

        /// <summary>
        /// Converts a character array to a stringBuilder instance.
        /// </summary>
        /// <param name="textDocumentCharArray">The character array that is to be converted to stringbuilder instance</param>
        /// <param name="capacity">The size of the stringBuilder instance that is returned by this function</param>
        /// <returns></returns>
        public StringBuilder ConvertToStringBuilder(char[][] textDocumentCharArray, int capacity)
        {
            StringBuilder textDocumentString = new StringBuilder("");
            textDocumentString.Capacity = capacity;//All textDocumentString are square.
            for (int i = 0; i < textDocumentCharArray.Length; i++)
            {
                textDocumentString.Append(new string(textDocumentCharArray[i]));
                textDocumentString.Append((i != textDocumentCharArray.Length - 1) ? "\n" : "");
            }

            return textDocumentString;
        }
        
    }
}
