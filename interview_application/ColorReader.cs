using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public class ColorReader : IColorReader
    {
        #region Members

        public delegate void CallBackHandler(string message);
        public event CallBackHandler CallBack = null;

        private List<Color> _loadedList = null;

        #endregion

        #region Implementation

        /// <summary>
        /// Raises the CallBack event. All communication with the UI should be handled through this method.
        /// </summary>
        /// <param name="message">The string to be raised as a status update to the UI</param>
        public void RaiseCallback(string message)
        {
            CallBack.Invoke(message);//appends string\n to txtOuput box
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the specified csv data file.
        /// Callback to display the R,G,B value of every color found where the R value is greater than both the G and B values.
        /// Data rows in the csv may be malformed, contain non-parsable data, or otherwise be unusable.
        /// The data will contain row headers to specify which column contains which color.
        /// The possible headers are: "Red", "Green", and "Blue" (but without the quotation marks)
        /// </summary>
        /// <param name="fullFilePath">The full path to the data file in question</param>
        public IEnumerable<Color> LoadDataFile(string fullFilePath)
        {
            //variables
            int my_int_red = 0;
            int my_int_green = 0;
            int my_int_blue = 0;
            int values_length = 0;
            //need to use object
            List<Color> list_colors_obj = new List<Color>();//allocating a list for _loadedlist
            bool is_valid_red, is_valid_green, is_valid_blue;
            var reader = new StreamReader(File.OpenRead(fullFilePath));//open file
            string line;
            string[] values;


            //read file till the end
            while (!reader.EndOfStream)
            {
                //read line then put into string array to be sorted
                line = reader.ReadLine();
                values = line.Split(',');

                values_length = values.Count();//count starts from 1 instead of 0
                //checks if line has only 3 values
                if((values_length == 3) == true){
                    //check if RGB values are integers
                    is_valid_red = int.TryParse(values[0], out my_int_red);//red
                    is_valid_green = int.TryParse(values[1], out my_int_green);//green
                    is_valid_blue = int.TryParse(values[2], out my_int_blue);//blue
                }
                else
                {
                    is_valid_red = false;
                    is_valid_green = false;
                    is_valid_blue = false;
                }

                if (is_valid_red == true && is_valid_green == true && is_valid_blue == true)
                {
                    //constructs a object with RGB values
                    Color temp = new Color(my_int_red, my_int_green, my_int_blue);

                    //check if RGB values are between 0 and 255
                    if(temp.IsValid == true)
                    {
                        list_colors_obj.Add(temp);
                    }
                    else
                    {
                        temp = null;//there is no destructor for colors class
                    }  
                }
            }
            return list_colors_obj;
            //throw new NotImplementedException();
        }


        /// <summary>
        /// Returns the total number of rows that were successfully parsed by the LoadDataFile method.
        /// </summary>
        public int MatchesFound
        {
            get
            {
                return _loadedList.Count();//simple count elements of list
                //throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Returns the total number of rows where Red was the most significant color in all successfully parsed results.
        /// </summary>
        public int ColorsFound
        {
            get
            {
                int red_rows = 0;//numbers of rows with red being dominant over green and blue.
                int red, green, blue;//temporary holders for object RGB values
                
                //iterating through list of objects
                foreach(Color temp in _loadedList)
                {
                    //filled temporary RGB holder values
                    red = temp.Red;
                    green = temp.Green;
                    blue = temp.Blue;

                    //checking if red if dominant
                    if(red > green && red > blue)
                    {
                        red_rows++;
                    }
                }
                //throw new NotImplementedException();
                return red_rows;
            }
        }

        #endregion

        #region Methods

        public void StartProcessing(string filePath)
        {
            this.RaiseCallback("Loading data file...");

            _loadedList = this.LoadDataFile(filePath).ToList();

            this.RaiseCallback("Processing complete.");
        }

        #endregion
    }
}
