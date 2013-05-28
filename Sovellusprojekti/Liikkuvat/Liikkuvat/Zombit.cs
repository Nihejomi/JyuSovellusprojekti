using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peli
{
    class Zombit
    {
       
        /// <summary>
        /// Arvoo zombin positionin reson avulla ja samalla arpoo zombin ominaisuudet kuten HP ja nopeus.
        /// </summary>
        /// <param name="resox">resolution x</param>
        /// <param name="resoy">resolution y</param>
        /// <returns>palauttaa arrayn {xpos, ypos, hp, nopeus}</returns>

        public int[] arvoZombi(int resox, int resoy)
        {
            Random rnd = new Random();
            int zombHPMAX = 11;
            int zombHPMIN = 1;
            int zombieSpeedMAX = 100;
            int zombieSpeedMIN = 10; 
          
            int [] ominaisuudet = new int[4] {rnd.Next(0,resox + 1), rnd.Next(0,resoy +1 ), rnd.Next(zombHPMIN, zombHPMAX), rnd.Next(zombieSpeedMIN, zombieSpeedMAX) };
            //int posx = rnd.Next(0, resox + 1);
            //int posy = rnd.Next(0, resoy + 1);
            return ominaisuudet;
        }
       

    }
}
