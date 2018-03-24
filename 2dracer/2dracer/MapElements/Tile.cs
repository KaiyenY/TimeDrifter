using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2dracer.MapElements
{
    public enum TileType
    {
        Building,
        SRoad,              // Straight bit of road
        CRoad,              // Corner of road
        TRoad,              // Three-way intersection
        FRoad               // Four-way intersection
    }

    /// <summary>
    /// A square on the map.
    /// </summary>
    public class Tile : GameObject
    {
        // Fields

        // Properties

        // Constructors
        public Tile(Vector2 position, TileType type, float rotation)
            : base(position, rotation, new Vector2(768))
        {
            sprite = Game1.tileSprites[(int)type];
        }

        // Methods
        public override void Draw()
        {
            base.Draw();
        }
    }
}

// Genoah