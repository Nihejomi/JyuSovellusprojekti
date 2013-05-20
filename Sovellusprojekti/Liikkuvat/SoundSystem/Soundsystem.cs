using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoundSystem
{


   






    public class Soundsystem
    {
        bool soundson;


        private NAudio.Wave.DirectSoundOut output = null;
        private NAudio.Wave.WaveFileReader wave1 = null;
        private NAudio.Wave.WaveFileReader wave2 = null;

        private NAudio.Wave.WaveChannel32 channel1 = null;
        private NAudio.Wave.WaveChannel32 channel2 = null;

        private NAudio.Wave.WaveMixerStream32 mikseri = null;


        public Soundsystem()
        {
 
            wave1 = new NAudio.Wave.WaveFileReader("sounds/gun-fire1.wav");
            wave2 = new NAudio.Wave.WaveFileReader("sounds/hit-glass1.wav");
            channel1 = new NAudio.Wave.WaveChannel32(wave1);
            channel2 = new NAudio.Wave.WaveChannel32(wave2);            
            mikseri = new NAudio.Wave.WaveMixerStream32();
   
            mikseri.AddInputStream(channel1);
            mikseri.AddInputStream(channel2);
            mikseri.AutoStop = false;

            output = new NAudio.Wave.DirectSoundOut();
            channel1.Volume = 0.0f;
            channel2.Volume = 0.0f;

            output.Init(mikseri);

            soundson = true;
            output.Play();
            
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

                if (soundi == "fire1")
                {
                 channel1.Volume = volume;
                   
                    channel1.Position = 0;
                }
                if (soundi == "hit-glass")
                {
                    channel2.Volume = volume;
                    channel2.Position = 0;
                }

            }
            
            
            
        }












    }





}
