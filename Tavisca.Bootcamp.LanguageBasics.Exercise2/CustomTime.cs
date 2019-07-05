using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
{
    class CustomTime
    {
        public TimeSpan t_span;
        public string time;
        public string read_as;

        public CustomTime(string time, string read_as)
        {
            this.time = time;
            this.read_as = read_as;
            TimeSpan.TryParse(time, out this.t_span);
            
        }
    }
}
