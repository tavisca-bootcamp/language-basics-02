using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
{
    class CustomTime
    {
        public TimeSpan timeSpan;
        public string time;
        public string readAs;

        public CustomTime(string time, string read_as)
        {
            this.time = time;
            this.readAs = read_as;
            TimeSpan.TryParse(time, out this.timeSpan);
            
        }
    }
}
