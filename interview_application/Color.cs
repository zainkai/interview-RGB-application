using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public class Color
    {
        #region Members

        public const int MAX_VALUE = 255;
        public const int MIN_VALUE = 0;

        #endregion

        #region Constructors

        public Color() { }

        public Color(int red, int green, int blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Join(",", this.Red, this.Green, this.Blue);
        }

        #endregion

        #region Properties

        public int Red { get; set; }

        public int Green { get; set; }

        public int Blue { get; set; }

        public bool IsValid
        {
            get
            {
                bool result = true;

                result = result && this.Red.IsBetween(MIN_VALUE, MAX_VALUE);
                result = result && this.Green.IsBetween(MIN_VALUE, MAX_VALUE);
                result = result && this.Blue.IsBetween(MIN_VALUE, MAX_VALUE);

                return result;
            }
        }

        #endregion
    }
}
