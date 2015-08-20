using LiveSplit.Options;
using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LiveSplit.Skyrim
{
    public class MediaPlayer
    {
        public int GeneralVolume { get; set; }
        protected WaveOut Player { get; private set; }

        public MediaPlayer()
        {
            GeneralVolume = 100;

            Player = new WaveOut();
        }

        public void Dispose()
        {
            Player.Stop();
        }

        public void PlaySound(String location, int volume)
        {
            Player.Stop();

            if (File.Exists(location))
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        AudioFileReader audioFileReader = new AudioFileReader(location);
                        audioFileReader.Volume = (volume / 100f) * (GeneralVolume / 100f);

                        Player.DeviceNumber = -1;
                        Player.Init(audioFileReader);
                        Player.Play();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                });
            }
        }

        public void PlaySound(String location)
        {
            PlaySound(location, GeneralVolume);
        }

        public void Stop()
        {
            Player.Stop();
        }
    }
}
