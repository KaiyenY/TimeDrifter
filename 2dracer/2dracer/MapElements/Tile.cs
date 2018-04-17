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
        /// Gives the rectangle of this <see cref="Tile"/> for the player to look for.
        /// </summary>
        public Rectangle Rect { get; private set; }

        /// <summary>
        /// Grabs the <see cref="TileType"/> of this <see cref="Tile"/>.
        /// </summary>
        public TileType Type { get; private set; }
        #endregion

        #region Constructors
        public Tile(TileType type, float rotation, int xIndex, int yIndex)
            : base(new Vector2(xIndex * 768, yIndex * 768), rotation, new Vector2(768), 0.1f)
        {
            Type = type;
            sprite = SetSprite();

            Rect = new Rectangle(position.ToPoint(), new Point(768));
        }
        #endregion

        #region Methods
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