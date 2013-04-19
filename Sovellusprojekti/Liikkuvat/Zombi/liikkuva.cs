using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombi
{

    public interface liikkuva
    {
       double getvectorinpituus();
         double[] liikuta(double x, double y, double kulma);
    }
}
