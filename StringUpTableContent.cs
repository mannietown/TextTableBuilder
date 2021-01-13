using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextTableBuilder
{
    /// <summary>
    /// Supports the writing of records to a StringUpTable TableString instance.
    /// </summary>
    public class StringUpTableContent
    {
        /// <summary>
        /// Stores the StringUpTable instance associated with the table records.
        /// </summary>
        public StringUpTable ContentTable { get; set; }

        /// <summary>
        /// Constructor. Creates an instance of the StringUpTableContent.
        /// </summary>
        /// <param name="stringUpTable">The value of the ContentTable property of the StringUpTableContent instance.</param>
        /// <param name="indentation">The value of the number of spaces before the record is written to a row.</param>
        public StringUpTableContent(StringUpTable stringUpTable, int indentation)
        {
            ContentTable = stringUpTable;
            ContentTable.RowIndentation = indentation;
        }

        /// <summary>
        /// Adds and array of records to the designated row.
        /// </summary>
        /// <param name="rowIndex">The index of the row to which the records are to be added.</param>
        /// <param name="rowContent">An array of records to be added to the TableString property of the ContentTable. Size must equal column count.</param>
        public void AddRowContent(int rowIndex,params string[] rowContent)
        {
            char[][] tableStringArray = ContentTable.TableStringArray;
            int rowIndexLine = 2 * (rowIndex) + 1;

            //Write to designated row
            for(int i = 0;i<ContentTable.ColumnCount;i++)
            {
                int indentation = ContentTable.RowIndentation + (i * (ContentTable.TableCharactersPerRow));
                for (int j = 0; j < rowContent[i].Length; j++)
                {
                    tableStringArray[rowIndexLine][indentation + j] = rowContent[i][j];
                }
            }

            //Write new TableString
            ContentTable.TableString = ContentTable.ConvertToStringBuilder(tableStringArray, ContentTable.TableString.Capacity);
        }
    }   
}
