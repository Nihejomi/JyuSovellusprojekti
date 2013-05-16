using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Undying
{

    public interface liikkuva
    {
        Vector act(Vector playerPos);
    }
}
