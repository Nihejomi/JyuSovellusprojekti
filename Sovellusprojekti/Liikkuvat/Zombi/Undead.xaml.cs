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


        public double getDistance(Vector target)
        {
            return Math.Sqrt((Math.Pow(position.X - target.X, 2)) + (Math.Pow(position.Y - target.Y, 2)));
        }

        public Vector stepTowards(Vector target)
        {
            Vector direction = Vector.Subtract(target, position);
            direction.Normalize();
            direction = Vector.Multiply(stepMultiplier, direction);
            position = Vector.Add(position, direction);
            return position;
        }
        
        public double getvectorinpituus(){
            return stepMultiplier;
        }
    }
}
