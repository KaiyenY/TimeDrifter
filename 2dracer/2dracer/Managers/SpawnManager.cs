using Microsoft.Xna.Framework;
using _2dracer.MapElements;
using System;
using System.Collections.Generic;

namespace _2dracer.Managers
{
    /// <summary>
    /// Aids in spawning in enemies and the player.
    /// </summary>
    public static class SpawnManager
    {
        #region Fields
        private static List<Tile> spawnTiles;
        private static Random rng;
        private static int maxEnemyCount;
        private static int spawnedEnemyCount;
        private static float minSpawnRadius;
        private static float maxSpawnRadius;
        #endregion

        #region Properties
        /// <summary>
        /// The difficulty multiplier of the game.
        /// </summary>
        public static int Difficulty { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the values of this manager.
        /// </summary>
        public static void Initialize()
        {
            rng = new Random();
            spawnTiles = new List<Tile>();
            Difficulty = 1;
            maxEnemyCount = 4 * Difficulty;
            spawnedEnemyCount = 0;
            minSpawnRadius = 3.0f;
            maxSpawnRadius = 5.0f;

            
            foreach (Tile tile in Map.Tiles)
            {
                if (tile.Type != TileType.Building && tile.Type != TileType.Grass)
                {
                    spawnTiles.Add(tile);
                }
            }
        }

        /// <summary>
        /// Picks a random spawn tile and spawns the player on it.
        /// </summary>
        public static void SpawnPlayer()
        {
            int tileIndex = rng.Next(0, spawnTiles.Count - 1);

            GameMaster.Instantiate(new Player(spawnTiles[tileIndex].Position));
        }

        /// <summary>
        /// Spawns in an enemy on a random tile around the player.
        /// </summary>
        public static void SpawnEnemy()
        {
            if (spawnedEnemyCount < maxEnemyCount)
            {
                // Spawn enemy in a zone around the player
                // Use min and max spawn radius fields
            }

            // Make sure to increment when created and decrement when enemy is destroyed
            spawnedEnemyCount++;
        }
        #endregion
    }
}
