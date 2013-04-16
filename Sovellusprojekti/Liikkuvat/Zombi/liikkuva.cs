using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombi
{

    public interface liikkuva
    {
        int getvectorinpituus();
         int[] liikuta(int x, int y, double kulma);
    }
}
