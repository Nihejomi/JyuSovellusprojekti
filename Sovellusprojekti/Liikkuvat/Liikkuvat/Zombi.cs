using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liikkuvat
{
    class Zombi
    {
        private double posX;
        private double posY;
        //states:(idle,wander,hunt,charge)(dead,crippled,damaged,fine)
        //statistics:(tough,aware,aggro,danger)

        public Zombi(double x, double y)
        {
            setPosition(x,y);
        }

        private void setPosition(double x, double y)
        {
            posX = x;
            posY = y;
        }

        public double getDistance(double x, double y)
        {
            return Math.Sqrt((Math.Pow(posX - x, 2)) + (Math.Pow(posY - y, 2)));
        }
    }
}
