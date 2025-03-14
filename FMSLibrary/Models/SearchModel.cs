using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSLibrary.Models
{
    public  class SearchModel
    {
        public List<Flight> Flights { get; set; }= new List<Flight>();
        public List<Fare> Fares { get; set; }=new List<Fare>();
    }
}
