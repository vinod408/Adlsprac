using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Application.Models
{
    [DataContract]
    public class OneArgumentDataPoint
    {
            public OneArgumentDataPoint(double y)
            {
                 this.Y = y;
            }
            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<double> Y = null;
        }
    }
