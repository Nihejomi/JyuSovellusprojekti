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
        Vector getPosition();
        Vector possibleMove(Vector target);
        void move(Vector target);
        int getKaantovuoro();
        double getDistance(Vector target);
        void die();
        bool isDead();
    }
}
