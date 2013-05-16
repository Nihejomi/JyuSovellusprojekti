using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Undying
{
    /// <summary>
    /// Interaction logic for Undead.xaml
    /// </summary>
    public partial class Zombi : UserControl, liikkuva
    {
        // Vectors concerning zombies position in the world
        private Vector position;
        private Vector facing;
        private Vector destination;

        //Classes that govern Zombies behaviour, reactions and condition
        private ZombiBehaviour behaviour;
        private ZombiStatus status;

        // stepVector represents minimum distance zombie moves per tick
        private double stepMultiplier;

        /// <summary>
        /// Initializes new Zombie
        /// </summary>
        /// <param name="startPos">Starting position of walking cadaver</param>
        public Zombi(Vector startPos)
        {
            InitializeComponent();
            stepMultiplier = 0.5;
            position = startPos;
            behaviour = new ZombiBehaviour();
            status = new ZombiStatus();
        }


        /* 
         * valtiaan sattumanvarainen piste, jota kohti Zombi pyrkii
         * kun pääse tietylle etäisyydelle valitaan uusipiste
         * seinät pyrittään kiertämään menemällä seinien suuntaisesti (pathfinding on muun ajan murhe :D:D)
         * jos pelaaja tietyn etäisyyden päässä, aletaan jahtaamaan
         * käy kiinni, jos pääsee lähelle
         * jos muut zombit huomaavat jahtaavan, liittyvät mukaan
         */

        /// <summary>
        /// Makes Zombie do stuff and react to players presence.
        /// </summary>
        /// <param name="playerPos">Players position vector</param>
        /// <returns>Zombies new postions in the world</returns>
        public Vector act(Vector playerPos)
        {
            double distance = getDistance(playerPos);
            if (distance > 5 && distance < 300) position = stepTowards(playerPos);
            return position;
        }

        /// <summary>
        /// Calculates Zombies distance from given Vector
        /// </summary>
        /// <param name="target">Vector to calculate distance from</param>
        /// <returns>Distance from the given vector</returns>
        public double getDistance(Vector target)
        {
            return Math.Sqrt((Math.Pow(position.X - target.X, 2)) + (Math.Pow(position.Y - target.Y, 2)));
        }

        /// <summary>
        /// Calculates new position for Zombie and change Zombies position to it.
        /// Zombie is moved towards the given vector.
        /// </summary>
        /// <param name="target">Vector for Zombie to shamble towards</param>
        /// <returns>Zombies new position</returns>
        private Vector stepTowards(Vector target)
        {
            Vector direction = Vector.Subtract(target, position);
            direction.Normalize();
            direction = Vector.Multiply(stepMultiplier, direction);
            position = Vector.Add(position, direction);
            return position;
        }
    }

    //(idle,wander,hunt,charge)(dead,crippled,damaged,fine)
    public class ZombiBehaviour
    {

    }

    //(tough,aware,aggro,danger)
    public class ZombiStatus
    {

    }
}
