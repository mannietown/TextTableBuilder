using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextTableBuilder
{
    /// <summary>
    /// Supports the string layout that is written to a textFile.
    /// </summary>
    public class TextDocumentFormat
    {
        /// <summary>
        /// Returns the number of characters in the TextDocumentString property of this class.
        /// </summary>
        public int DocumentLength { get { return LineCount * (LineLength+1); } }

        /// <summary>
        /// Stores the number of lines in the TextDocumentString property of this class.
        /// </summary>
        public int LineCount { get; set; }

        /// <summary>
        /// Stores the number of characters in a line in th eTextDocumentString property of this class.
        /// </summary>
        public int LineLength { get; set; }

        /// <summary>
        /// Stores the string layout that is written to a textFile.
        /// </summary>
        public StringBuilder TextDocumentString { get; set; }

        /// <summary>
        /// Returns the string layout that is written to a textFile as a multi-dimensional char array.
        /// </summary>
        public char[][] TextDocumentStringArray
        {
            get
            {
                return (from string s in TextDocumentString.ToString().Split('\n')
                        select s.ToCharArray()).ToArray();
            }
        }

        /// <summary>
        /// Writes and returns the textDocumentString format.
        /// </summary>
        /// <returns></returns>
        private StringBuilder CreateTextDocumentString()
        {
            StringBuilder textDocumentString = new StringBuilder("");
            textDocumentString.Capacity = DocumentLength;//The +1 indicates the inclusion of the new Line character

            for(int i = 0;i<LineCount;i++)
            {
                for(int j =0;j<LineLength;j++)
                {
                    textDocumentString.Append(" ");
                }
                textDocumentString.Append((i != LineCount - 1) ? "\n" : "");
            }

            return textDocumentString;
        }

        /// <summary>
        /// Constructor. Creates an instance of this class.
        /// </summary>
        /// <param name="lineCount">The value of the number of lines in the TextDocumentString property of this class.</param>
        /// <param name="lineLength">The value of the numbe of characters in the TextDocumentString property of this class. </param>
        public TextDocumentFormat(int lineCount, int lineLength)
        {
            LineCount = lineCount;
            LineLength = lineLength;
            TextDocumentString = CreateTextDocumentString();
        }

    }
}
