using _2dracer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2dracer.MapElements
{
    public enum TileType
    {
        Building,
        SRoad,              // Straight bit of road
        CRoad,              // Corner of road
        TRoad,              // Three-way intersection
        FRoad,              // Four-way intersection
        Grass
    }

    /// <summary>
    /// A square on the map.
    /// </summary>
    public class Tile : GameObject
    {
        #region Properties
        /// <summary>
        /// The node that resides within this <see cref="Tile"/>.
        /// </summary>
        public Node Node;

        /// <summary>
        /// Gives the rectangle of this <see cref="Tile"/> for the player to look for.
        /// </summary>
        public Rectangle Rect { get; private set; }

        /// <summary>
        /// Grabs the <see cref="TileType"/> of this <see cref="Tile"/>.
        /// </summary>
        public TileType Type { get; private set; }

        /// <summary>
        /// Determines whether this tile has the player
        /// </summary>
        public bool ContainsPlayer { get; private set; }
        #endregion

        #region Constructors
        public Tile(TileType type, float rotation, int xIndex, int yIndex)
            : base(new Vector2(xIndex * Map.TileSize, yIndex * Map.TileSize), rotation, new Vector2(Map.TileSize), 0.1f)
        {
            Type = type;
            sprite = SetSprite();

            origin *= 2;

            Rect = new Rectangle(position.ToPoint(), new Point((int)Map.TileSize));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates this tile to check for player
        /// </summary>
        public override void Update()
        {
            Vector2 playerPos = Vector2.Add(
                GameMaster.GameObjects[1].Position,
                new Vector2(Map.TileSize / 2));

            if (!ContainsPlayer && Rect.Contains(playerPos))
            {
                
                ContainsPlayer = true;
                color = Color.Red;
            }
            else if (!Rect.Contains(playerPos))
            {
                ContainsPlayer = false;
                color = Color.White;
            }
        }

        /// <summary>
        /// Sets the sprite of the tile.
        /// </summary>
        /// <returns>The sprite according to the tile.</returns>
        private Texture2D SetSprite()
        {
            switch (Type)
            {
                case TileType.Building:
                    return LoadManager.Sprites["Roof"];

                case TileType.CRoad:
                    return LoadManager.Sprites["CornerRoad"];

                case TileType.FRoad:
                    return LoadManager.Sprites["FIntersection"];

                case TileType.Grass:
                    return LoadManager.Sprites["Grass"];

                case TileType.SRoad:
                    return LoadManager.Sprites["StraightRoad"];

                case TileType.TRoad:
                    return LoadManager.Sprites["TIntersection"];

                default:
                    throw new NotImplementedException("The TileType you put in is not implemented in the SetSprite() method.");
            }
        }
        #endregion
    }
}

// -- Genoah Martinelli