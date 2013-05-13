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
        
        //states:(idle,wander,hunt,charge)(dead,crippled,damaged,fine)
        //statistics:(tough,aware,aggro,danger)

        // stepVector represents minimum distance zombie moves per tick
        public double stepMultiplier;

        /// <summary>
        /// Initializes new Zombie
        /// </summary>
        /// <param name="startPos">Starting position of walking cadaver</param>
        public Zombi(Vector startPos)
        {
            InitializeComponent();
            stepMultiplier = 0.5;
            position = startPos;
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
        public Vector stepTowards(Vector target)
        {
            if (getDistance(target) < 300)
            {
            Vector direction = Vector.Subtract(target, position);
            direction.Normalize();
            direction = Vector.Multiply(stepMultiplier, direction);
            position = Vector.Add(position, direction);
            }
            return position;
        }
        
        /// <summary>
        /// Returns Zombies stepMultiplier, that indicates how far Zombie is moved every tick
        /// </summary>
        /// <returns>Zombies current stepMultiplier</returns>
        public double getvectorinpituus(){
            return stepMultiplier;
        }
    }
}
