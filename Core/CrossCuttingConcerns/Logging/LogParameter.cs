using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string Name { get; set; }//methot ismi
        public object Value { get; set; }//değeri
        public string Type { get; set; }//tipi 
    }
}
