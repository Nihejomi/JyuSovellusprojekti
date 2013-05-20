/*
 * Äänisysteemi opendata zombipeliin 
 * Versio 0.02
 * Joel Kivelä
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoundSystem
{


    public class Soundsystem
    {
        bool soundson;

        private NAudio.Wave.BlockAlignReductionStream ambience = null;


        private NAudio.Wave.DirectSoundOut output = null;
        
        // aseen äänet - jokaisella oma kanava
        private NAudio.Wave.WaveFileReader fire1 = null;
        private NAudio.Wave.WaveFileReader fire2 = null;
        private NAudio.Wave.WaveFileReader load = null;
        private NAudio.Wave.WaveFileReader empty = null;

        // luotien osumaäänet - jokaisella oma kanava
        private NAudio.Wave.WaveFileReader hitflesh1 = null;
        private NAudio.Wave.WaveFileReader hitflesh2 = null;
        private NAudio.Wave.WaveFileReader hitflesh3 = null;
        private NAudio.Wave.WaveFileReader hitglass = null;
        private NAudio.Wave.WaveFileReader hitmetal1 = null;
        private NAudio.Wave.WaveFileReader hitmetal2 = null;
        private NAudio.Wave.WaveFileReader hitmetal3 = null;
        private NAudio.Wave.WaveFileReader hitrock = null;

        // kävelyäänet - kaikki samalla kanavalla
        private NAudio.Wave.WaveFileReader walk1 = null;
        private NAudio.Wave.WaveFileReader walk2 = null;
        private NAudio.Wave.WaveFileReader walk3 = null;
        private NAudio.Wave.WaveFileReader walk4 = null;
        private NAudio.Wave.WaveFileReader walk5 = null;
        private NAudio.Wave.WaveFileReader walk6 = null;
        private NAudio.Wave.WaveFileReader walk7 = null;
        private NAudio.Wave.WaveFileReader walk8 = null;

        // zombiäänet - jokaisella oma kanava
        private NAudio.Wave.WaveFileReader zombi1 = null;
        private NAudio.Wave.WaveFileReader zombi2 = null;
        private NAudio.Wave.WaveFileReader zombi3 = null;
        private NAudio.Wave.WaveFileReader zombi4 = null;
        private NAudio.Wave.WaveFileReader zombi5 = null;
        private NAudio.Wave.WaveFileReader zombi6 = null;
        private NAudio.Wave.WaveFileReader zombi7 = null;

        // ambienssiääni, tuuli
        private NAudio.Wave.WaveFileReader wind = null;
        

        // aseäänien kanavat
        private NAudio.Wave.WaveChannel32 fire1_channel = null;
        private NAudio.Wave.WaveChannel32 fire2_channel = null;
        private NAudio.Wave.WaveChannel32 load_channel = null;
        private NAudio.Wave.WaveChannel32 empty_channel = null;

        // osumaäänien kanavat
        private NAudio.Wave.WaveChannel32 hitflesh1_channel = null;
        private NAudio.Wave.WaveChannel32 hitflesh2_channel = null;
        private NAudio.Wave.WaveChannel32 hitflesh3_channel = null;
        private NAudio.Wave.WaveChannel32 hitglass_channel = null;
        private NAudio.Wave.WaveChannel32 hitmetal1_channel = null;
        private NAudio.Wave.WaveChannel32 hitmetal2_channel = null;
        private NAudio.Wave.WaveChannel32 hitmetal3_channel = null;
        private NAudio.Wave.WaveChannel32 hitrock_channel = null;

        // kävelyäänien kanava
        private NAudio.Wave.WaveChannel32 walk_channel = null;

        // zombiäänien kanavat
        private NAudio.Wave.WaveChannel32 zombi1_channel = null;
        private NAudio.Wave.WaveChannel32 zombi2_channel = null;
        private NAudio.Wave.WaveChannel32 zombi3_channel = null;
        private NAudio.Wave.WaveChannel32 zombi4_channel = null;
        private NAudio.Wave.WaveChannel32 zombi5_channel = null;
        private NAudio.Wave.WaveChannel32 zombi6_channel = null;
        private NAudio.Wave.WaveChannel32 zombi7_channel = null;

        private NAudio.Wave.WaveChannel32 ambience_channel = null;


        // kanavien mikseri
        private NAudio.Wave.WaveMixerStream32 mikseri = null;


        public Soundsystem()
        {

            NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(
                new NAudio.Wave.Mp3FileReader("sounds/ambience-wind1.mp3"));

            ambience = new NAudio.Wave.BlockAlignReductionStream(pcm);


            fire1 = new NAudio.Wave.WaveFileReader("sounds/gun-fire1.wav");
            fire2 = new NAudio.Wave.WaveFileReader("sounds/gun-fire2.wav");
            empty = new NAudio.Wave.WaveFileReader("sounds/gun-empty.wav");
            load = new NAudio.Wave.WaveFileReader("sounds/gun-load.wav");

            hitflesh1 = new NAudio.Wave.WaveFileReader("sounds/hit-flesh1.wav");
            hitflesh2 = new NAudio.Wave.WaveFileReader("sounds/hit-flesh2.wav");
            hitflesh3 = new NAudio.Wave.WaveFileReader("sounds/hit-flesh3.wav");
            hitmetal1 = new NAudio.Wave.WaveFileReader("sounds/hit-metal1.wav");
            hitmetal2 = new NAudio.Wave.WaveFileReader("sounds/hit-metal2.wav");
            hitmetal3 = new NAudio.Wave.WaveFileReader("sounds/hit-metal3.wav");
            hitrock = new NAudio.Wave.WaveFileReader("sounds/hit-rock1.wav");
            hitglass = new NAudio.Wave.WaveFileReader("sounds/hit-glass1.wav");

            walk1 = new NAudio.Wave.WaveFileReader("sounds/walk1.wav");
            walk2 = new NAudio.Wave.WaveFileReader("sounds/walk2.wav");
            walk3 = new NAudio.Wave.WaveFileReader("sounds/walk3.wav");
            walk4 = new NAudio.Wave.WaveFileReader("sounds/walk4.wav");
            walk5 = new NAudio.Wave.WaveFileReader("sounds/walk5.wav");
            walk6 = new NAudio.Wave.WaveFileReader("sounds/walk6.wav");
            walk7 = new NAudio.Wave.WaveFileReader("sounds/walk7.wav");
            walk8 = new NAudio.Wave.WaveFileReader("sounds/walk8.wav");

            zombi1 = new NAudio.Wave.WaveFileReader("sounds/zombi-moan1.wav");
            zombi2 = new NAudio.Wave.WaveFileReader("sounds/zombi-moan2.wav");
            zombi3 = new NAudio.Wave.WaveFileReader("sounds/zombi-moan3.wav");
            zombi4 = new NAudio.Wave.WaveFileReader("sounds/zombi-moan4.wav");
            zombi5 = new NAudio.Wave.WaveFileReader("sounds/zombi-moan5.wav");
            zombi6 = new NAudio.Wave.WaveFileReader("sounds/zombi-moan6.wav");
            zombi7 = new NAudio.Wave.WaveFileReader("sounds/zombi-moan7.wav");


            fire1_channel = new NAudio.Wave.WaveChannel32(fire1);
            fire1_channel.Volume = 0.0f;
            fire2_channel = new NAudio.Wave.WaveChannel32(fire2);
            fire2_channel.Volume = 0.0f;
            load_channel = new NAudio.Wave.WaveChannel32(load);
            load_channel.Volume = 0.0f;
            empty_channel = new NAudio.Wave.WaveChannel32(empty);
            empty_channel.Volume = 0.0f;

            hitglass_channel = new NAudio.Wave.WaveChannel32(hitglass);
            hitglass_channel.Volume = 0.0f;
            hitflesh1_channel = new NAudio.Wave.WaveChannel32(hitflesh1);
            hitflesh1_channel.Volume = 0.0f;
            hitflesh2_channel = new NAudio.Wave.WaveChannel32(hitflesh2);
            hitflesh2_channel.Volume = 0.0f;
            hitflesh3_channel = new NAudio.Wave.WaveChannel32(hitflesh3);
            hitflesh3_channel.Volume = 0.0f;
            hitrock_channel = new NAudio.Wave.WaveChannel32(hitrock);
            hitrock_channel.Volume = 0.0f;
            hitmetal1_channel = new NAudio.Wave.WaveChannel32(hitmetal1);
            hitmetal1_channel.Volume = 0.0f;
            hitmetal2_channel = new NAudio.Wave.WaveChannel32(hitmetal2);
            hitmetal2_channel.Volume = 0.0f;
            hitmetal3_channel = new NAudio.Wave.WaveChannel32(hitmetal3);
            hitmetal3_channel.Volume = 0.0f;

            walk_channel = new NAudio.Wave.WaveChannel32(walk1);
            walk_channel.Volume = 0.0f;

            zombi1_channel = new NAudio.Wave.WaveChannel32(zombi1);
            zombi1_channel.Volume = 0.0f;
            zombi2_channel = new NAudio.Wave.WaveChannel32(zombi2);
            zombi2_channel.Volume = 0.0f;

            zombi3_channel = new NAudio.Wave.WaveChannel32(zombi3);
            zombi3_channel.Volume = 0.0f;

            zombi4_channel = new NAudio.Wave.WaveChannel32(zombi4);
            zombi4_channel.Volume = 0.0f;

            zombi5_channel = new NAudio.Wave.WaveChannel32(zombi5);
            zombi5_channel.Volume = 0.0f;

            zombi6_channel = new NAudio.Wave.WaveChannel32(zombi6);
            zombi6_channel.Volume = 0.0f;

            zombi7_channel = new NAudio.Wave.WaveChannel32(zombi7);
            zombi7_channel.Volume = 0.0f;

            mikseri = new NAudio.Wave.WaveMixerStream32();

            ambience_channel = new NAudio.Wave.WaveChannel32(ambience);
            ambience_channel.Volume = 0.2f;

   
            mikseri.AddInputStream(fire1_channel);
            mikseri.AddInputStream(fire2_channel);
            mikseri.AddInputStream(empty_channel);
            mikseri.AddInputStream(load_channel);

            mikseri.AddInputStream(hitflesh1_channel);
            mikseri.AddInputStream(hitflesh2_channel);
            mikseri.AddInputStream(hitflesh3_channel);
            mikseri.AddInputStream(hitmetal1_channel);
            mikseri.AddInputStream(hitmetal2_channel);
            mikseri.AddInputStream(hitmetal3_channel);
            mikseri.AddInputStream(hitrock_channel);
            mikseri.AddInputStream(hitglass_channel);

            mikseri.AddInputStream(walk_channel);

            mikseri.AddInputStream(zombi1_channel);
            mikseri.AddInputStream(zombi2_channel);
            mikseri.AddInputStream(zombi3_channel);
            mikseri.AddInputStream(zombi4_channel);
            mikseri.AddInputStream(zombi5_channel);
            mikseri.AddInputStream(zombi6_channel);
            mikseri.AddInputStream(zombi7_channel);


            
            mikseri.AddInputStream(ambience_channel);

           
            mikseri.AutoStop = false;

            output = new NAudio.Wave.DirectSoundOut();
            output.Init(mikseri);

            soundson = true;
            output.Play();
            
        }
        // tarkistaa onko ambienssiääni loppunut. jos on niin soitetaan alusta
        public void check_ambience()
        {
            if (ambience_channel.Position >= (ambience_channel.Length-5))
                ambience_channel.Position = 0;

        }


        public void Setoff()
        {
            soundson = false;
        }
        public void SetOn()
        {
            soundson = true;
        }

        public void PlaySound(string soundi, float volume)
        {
            if (soundson)
            {
                Random random = new Random();

                if (soundi == "fire1")
                {
                    
                    if (random.Next(0, 2) == 0)
                    {

                        fire1_channel.Volume = volume;
                        fire1_channel.Position = 0;
                    }
                    else
                    {
                        fire2_channel.Volume = volume;
                        fire2_channel.Position = 0;
                    }

                }

                if (soundi == "load")
                {
                    load_channel.Volume = volume;
                    load_channel.Position = 0;
                }
                if (soundi == "empty")
                {
                    empty_channel.Volume = volume;
                    empty_channel.Position = 0;
                }
                    

                if (soundi == "hit-flesh")
                {
                    int arpa = random.Next(0, 3);
                    switch (arpa)
                    {
                        case 0:
                            hitflesh1_channel.Volume = volume;
                            hitflesh1_channel.Position = 0;
                            break;
                        case 1:
                            hitflesh2_channel.Volume = volume;
                            hitflesh2_channel.Position = 0;
                            break;
                        case 2:
                            hitflesh3_channel.Volume = volume;
                            hitflesh3_channel.Position = 0;
                            break;
                    }
                    
                }

                if (soundi == "hit-metal")
                {
                    int arpa = random.Next(0, 3);
                    switch (arpa)
                    {
                        case 0:
                            hitmetal1_channel.Volume = volume;
                            hitmetal1_channel.Position = 0;
                            break;
                        case 1:
                            hitmetal2_channel.Volume = volume;
                            hitmetal2_channel.Position = 0;
                            break;
                        case 2:
                            hitmetal3_channel.Volume = volume;
                            hitmetal3_channel.Position = 0;
                            break;
                    }

                }
                if (soundi == "hit-rock")
                {
                    hitrock_channel.Volume = volume;
                    hitrock_channel.Position = 0;
                }
                if (soundi == "hit-glass")
                {
                    hitglass_channel.Volume = volume;
                    hitglass_channel.Position = 0;
                }

                if (soundi == "zombi")
                {
                    int arpa = random.Next(0, 7);
                    switch (arpa)
                    {
                        case 0:
                            zombi1_channel.Volume = volume;
                            zombi1_channel.Position = 0;
                            break;
                        case 1:
                            zombi2_channel.Volume = volume;
                            zombi2_channel.Position = 0;
                            break;
                        case 2:
                            zombi3_channel.Volume = volume;
                            zombi3_channel.Position = 0;
                            break;
                        case 3:
                            zombi4_channel.Volume = volume;
                            zombi4_channel.Position = 0;
                            break;
                        case 4:
                            zombi5_channel.Volume = volume;
                            zombi5_channel.Position = 0;
                            break;
                        case 5:
                            zombi6_channel.Volume = volume;
                            zombi6_channel.Position = 0;
                            break;
                        case 6:
                            zombi7_channel.Volume = volume;
                            zombi7_channel.Position = 0;
                            break;
                                         
                    }

                }
                if (soundi == "walk")
                {
                    int arpa = random.Next(0, 8);
                    switch (arpa)
                    {
                        case 0:
                            walk_channel = new NAudio.Wave.WaveChannel32(walk1);
                            break;
                        case 1:
                            walk_channel = new NAudio.Wave.WaveChannel32(walk2);
                            break;
                        case 2:
                            walk_channel = new NAudio.Wave.WaveChannel32(walk3);
                            break;
                        case 3:
                            walk_channel = new NAudio.Wave.WaveChannel32(walk4);
                            break;
                        case 4:
                            walk_channel = new NAudio.Wave.WaveChannel32(walk5);
                            break;
                        case 5:
                            walk_channel = new NAudio.Wave.WaveChannel32(walk6);
                            break;
                        case 6:
                            walk_channel = new NAudio.Wave.WaveChannel32(walk7);
                            break;
                        case 7:
                            walk_channel = new NAudio.Wave.WaveChannel32(walk8);
                            break;
                    }
                    walk_channel.Volume = volume;
                    walk_channel.Position = 0;
                }

            }
            
            
            
        }












    }





}
