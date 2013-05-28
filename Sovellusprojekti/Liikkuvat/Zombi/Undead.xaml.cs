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
        public int kaannosvuoro;
        
        /// <summary>
        /// palauttaa kaantovuoron
        /// </summary>
        /// <returns></returns>
        public int getKaantovuoro(){
        return kaannosvuoro;
        }

        //Classes that govern Zombies behaviour, reactions and condition
        private ZombiBehaviour behaviour;
        private ZombiStatus status;
        private bool dead = false;
        private bool dont = false;
        private bool etheral = false;


        // stepVector represents minimum distance zombie moves per tick
        private double stepMultiplier;

        /// <summary>
        /// Initializes new Zombie
        /// </summary>
        /// <param name="startPos">Starting position of walking cadaver</param>
        public Zombi(Vector startPos)
        {
            InitializeComponent();
            stepMultiplier = 0.9;
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
           // position = stepTowards(playerPos);
            //tätä käytettään tarkistuksessa, position asetetaan move- metodilla
            return position;
        }

        public void die()
        {
            dead = true;
        }

        public void live() { dead = false; }
        public void ghost() { etheral = true; }
        public void nope() { dont = true; }

        public bool isDead()
        {
            return dead;
        }

        public bool isAlive()
        {
            return !dead;
        }

        public bool isGhost() { return etheral; }
        public bool isNope() { return dont; }

        public Vector possibleMove(Vector target)
        {
            Vector direction = Vector.Subtract(target, position);
            direction.Normalize();
            direction = Vector.Multiply(stepMultiplier, direction);
            return Vector.Add(position, direction);
        }

        public void move(Vector target)
        {
            position = target;
        }

        public Vector getPosition()
        {
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
