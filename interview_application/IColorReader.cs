using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public interface IColorReader
    {
        void RaiseCallback(string message);
        IEnumerable<Color> LoadDataFile(string fullFilePath);
        int MatchesFound { get; }
        int ColorsFound { get; }
    }
}
