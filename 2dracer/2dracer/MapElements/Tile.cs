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
        FRoad,              // Four-way intersection
        Grass
    }

    /// <summary>
    /// A square on the map.
    /// </summary>
    public class Tile : GameObject
    {
        // Fields
        private TileType type;

        // Properties
        public new float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public TileType Type
        {
            get { return type; }
            set { type = value; }
        }

        // Constructors
        public Tile(Vector2 position, TileType type, float rotation)
            : base(position, rotation, new Vector2(768))
        {
            sprite = Game1.tileSprites[(int)type];
            this.type = type;
        }

        // Methods
        public override void Draw()
        {
            base.Draw();
        }
    }
}

// Genoah