using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

using Gahame.GameUtils;
using Gahame.GameScreens;

using System;
using System.Collections.Generic;

namespace Gahame.GameAudio
{
    public class Music
    {

        // Load tracks with dis
        ContentManager content;

        // Le tracks
        public Dictionary<string , SoundEffectInstance> Tracks;
        
        // COnstructor
        public Music()
        {
            Tracks = new Dictionary<string, SoundEffectInstance>();
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        // Load Track
        public void LoadTrack(ContentManager content, string path, string name)
        {
            Tracks.Add(name, content.Load<SoundEffect>(path).CreateInstance());
        }

        // Unload garbage
        public void UnloadContent()
        {

            // Loop through the tracks and Delet
            foreach (KeyValuePair<string, SoundEffectInstance> track in Tracks)
            {

                track.Value.Stop();
                track.Value.Dispose();

            }

            content.Unload();

        }

        // Play every track
        public void PlayAllTracks()
        {

            // Loop through the tracks and play that stouf
            foreach (KeyValuePair<string, SoundEffectInstance> track in Tracks)
            {

                track.Value.Play();
                
            }

        }

        // set volume of a track
        public void SetVolume(string trackName, float volume)
        {
            Tracks[trackName].Volume = volume;
        }

        // Fade Track
        public void FadeTrack(string trackName, float newVolume, float fadeSpeed)
        {
            SoundEffectInstance tempSound = Tracks[trackName];
            tempSound.Volume = MyMaths.Approach(tempSound.Volume, newVolume, fadeSpeed);
        }


    }
}
