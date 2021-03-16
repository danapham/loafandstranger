using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoafAndStranger.Models
{
    public class Loaf
    {
        //Models are for storing pieces of information, not for having behavior
        public int Id { get; set; }
        public LoafSize Size { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int WeightInOunces { get; set; }
        public bool Sliced { get; set; }
    }

    //as long as it isn't used anywhere else, this can be in the same file
    public enum LoafSize
    {
        Small,
        Medium,
        Large
    }
}
