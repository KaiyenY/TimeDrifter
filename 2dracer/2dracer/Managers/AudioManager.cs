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
    public static class AudioManager
    {
        #region Fields
        /// <summary>
        /// Holds a reference to the sound effects in <see cref="LoadManager"/>.
        /// </summary>
        public static Dictionary<string, SoundEffect> Sounds;

        /// <summary>
        /// Holds a reference to the music in <see cref="LoadManager"/>.
        /// </summary>
        public static Dictionary<string, Song> Music;

        /// <summary>
        /// Gives the sound effect for being in slow motion.
        /// </summary>
        private static SoundEffectInstance SlowMotion;

        /// <summary>
        /// Determines if the game is currently muted.
        /// </summary>
        public static bool Muted;

        /// <summary>
        /// Controls the overall volume of the game.
        /// </summary>
        public static float MasterVolume;

        /// <summary>
        /// Determines the volume of the sound in the game.
        /// </summary>
        public static float SoundVolume;

        /// <summary>
        /// Determines what track is currently playing.
        /// </summary>
        public static string CurrentTrack;
        #endregion

        #region Properties
        /// <summary>
        /// The current state of the <see cref="MediaPlayer"/>.
        /// </summary>
        public static MediaState State { get { return MediaPlayer.State; } }

        /// <summary>
        /// Determines the volume of the music in the game.
        /// </summary>
        public static float MusicVolume { get { return MediaPlayer.Volume; } set { MediaPlayer.Volume = value; } }
        #endregion

        #region Constructor
        static AudioManager()
        {
            Sounds = LoadManager.Sounds;
            Music = LoadManager.Music;

            SlowMotion = Sounds["SlowMotion"].CreateInstance();

            MasterVolume = 0.5f;
            SoundVolume = 1.0f;

            Muted = MediaPlayer.IsMuted = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates all of the songs during each state
        /// </summary>
        public static void Update()
        {
            switch (Game1.GameState)
            {
                case GameState.Game:
                    if (CurrentTrack != "ExtremeAction")
                    {
                        StopMusic();
                        PlayMusic("ExtremeAction", 0.25f);
                    }

                    if (State == MediaState.Paused)
                    {
                        ResumeMusic();
                    }

                    if (Player.slowMo)
                    {
                        SlowMotion.Volume = 1.0f * SoundVolume * MasterVolume;
                        SlowMotion.Play();
                    }
                    else if (!Player.slowMo && SlowMotion.State == SoundState.Playing)
                    {
                        if (SlowMotion.Volume < 0.1f)
                        {
                            SlowMotion.Stop();
                        }
                        else if (SlowMotion.Volume > 0.1f)
                        {
                            SlowMotion.Volume -= 0.05f;
                        }
                    }
                    break;
                    
                case GameState.GameOver:
                    // Some sad game over music?
                    break;

                case GameState.Menu:
                    if (CurrentTrack != "HappyRock")
                    {
                        StopMusic();
                        PlayMusic("HappyRock", 0.25f);
                    }

                    if (State == MediaState.Paused)
                    {
                        ResumeMusic();
                    }
                    break;

                case GameState.Options:
                    PauseMusic();
                    break;

                case GameState.Instructions:
                    PauseMusic();
                    break;

                case GameState.Pause:
                    PauseMusic();
                    break;

                default:
                    throw new NotImplementedException("The current GameState is not supported by the AudioManagers Update method.");
            }
        }

        /// <summary>
        /// Toggles whether sounds and music are muted or not.
        /// </summary>
        public static void ToggleMute()
        {
            Muted = MediaPlayer.IsMuted = !Muted;
        }

        /// <summary>
        /// Plays a sound effect
        /// </summary>
        /// <param name="soundEffect">The name of the sound to play</param>
        public static void PlaySound(string soundEffect, float volume = 1.0f, float pitch = 0.0f, float pan = 0.0f)
        {
            if (Sounds.ContainsKey(soundEffect))
            {
                Sounds[soundEffect].Play(volume, pitch, pan);
            }
            else
            {
                throw new Exception($"The sound effect {soundEffect} does not exist.");
            }
        }

        /// <summary>
        /// Plays a song
        /// </summary>
        /// <param name="trackName">The track name to play</param>
        /// <param name="volume">Volume the song should play at</param>
        public static void PlayMusic(string trackName, float volume = 1.0f)
        {
            if (Music.ContainsKey(trackName))
            {
                MusicVolume = volume * MasterVolume;
                MediaPlayer.Play(Music[trackName]);
                CurrentTrack = trackName;
            }
            else
            {
                throw new Exception($"The track {trackName} does not exist.");
            }
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

        /// <summary>
        /// Switches the current song track that is playing.
        /// </summary>
        public static void StopMusic()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
                CurrentTrack = null;
            }
        }
        #endregion
    }
}
