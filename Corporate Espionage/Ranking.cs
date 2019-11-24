using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Corporate_Espionage {
    class Ranking {
        private string[] _allArray;
        private string[] _rankedArray;
        private string[] _storedArray;
        private double _perRanked;
        private int _numRanked;
        private int _rankCount;

        //CONSTRUCTOR
        public Ranking() {
            _perRanked = 0;
            _numRanked = 0;
        }//end constructor

        //PROPERTY
        public string[] RankedAry {
            get { return _rankedArray; }
            set { _rankedArray = value; }
        }//end property

        public string[] StoredAry {
            get { return _storedArray; }
            set { _storedArray = value; }
        }//end property

        public double PercentRank {
            get { return _perRanked; }
            set { _perRanked = value; }
        }//end property

        public int NumRanked {
            get { return _numRanked; }
            set { _numRanked = value; }
        }//end property

        public int RankCount {
            get { return _rankCount; }
            set { _rankCount = value; }
        }//end property

        public string[] AllArray {
            get { return _allArray; }
            set { _allArray = value; }
        }//end property

        //METHODS
        public void ImportFile(string path) { 
            StreamReader stream = new StreamReader(path);
            char[] splitters = { ' ', '\n', '\r' };
            RankedAry = stream.ReadToEnd().ToLower().Split(splitters, StringSplitOptions.RemoveEmptyEntries);
        }//end method

        public void ConvertArray(string data) {
            char[] splitchars = { ' ', '\t', '\n', ',', '.' };
            AllArray = data.ToLower().Split(splitchars, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> distinctstring = AllArray.Distinct();
            StoredAry = distinctstring.ToArray();
        }//end method
    }//end class
}//end namespace
