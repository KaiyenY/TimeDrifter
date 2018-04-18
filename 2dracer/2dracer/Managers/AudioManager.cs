using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace _2dracer.Managers
{
    /// <summary>
    /// Contains all sounds and 
    /// </summary>
    public class AudioManager
    {
        // Fields

        // Properties
        public static Dictionary<string, SoundEffect> SoundEffects;         // Stores all SoundEffects by name
        public static List<Song> Music;                                         // Stores all Song tracks by index
        public static string[] SEPaths;                                     // Stores all SoundEffect file paths
        public static string[] MPaths;                                      // Stores all Song track paths

        // Constructor
        static AudioManager()
        {

        }

        // Methods
        /// <summary>
        /// Plays a sound effect
        /// </summary>
        /// <param name="soundEffect">The name of the sound to play</param>
        /// <param name="volume">How loud the sound will play</param>
        /// <param name="pitch">The pitch (high / low) of the sound</param>
        /// <param name="pan">Determines volume per audio channel (left / right)</param>
        public static void PlaySound(string soundEffect, float volume = 0.5f, float pitch = 0f, float pan = 0f)
        {
            if (SoundEffects.ContainsKey(soundEffect))
            {
                SoundEffects[soundEffect].Play(volume, pitch, pan);
            }
            else
            {
                throw new Exception("Error : " + soundEffect + " does not exist!");
            }
        }

        /// <summary>
        /// Plays a song
        /// </summary>
        /// <param name="trackNumber">The track index to play</param>
        /// <param name="volume">Volume the song should play at</param>
        public static void PlayMusic(int trackNumber, float volume = 1.0f)
        {
            MediaPlayer.Volume = volume;
            MediaPlayer.Play(Music[trackNumber]);
        }

        /// <summary>
        /// Pauses whatever song is playing currently
        /// </summary>
        public static void PauseMusic()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
        }

        /// <summary>
        /// Resumes whatever song was playing
        /// </summary>
        public static void ResumeMusic()
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
        }
    }
}
