using LevelDesigner.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LevelDesigner.MapElements
{
    public enum TileType
    {
        Building,
        SRoad,
        CRoad,
        TRoad,
        FRoad,
        Grass
    }

    /// <summary>
    /// Creates a tile in the designer
    /// </summary>
    public class Tile
    {
        #region Fields
        /// <summary>
        /// The <see cref="Rectangle"/> that corresponds to this tile.
        /// </summary>
        private Rectangle rect;

        /// <summary>
        /// The <see cref="Texture2D"/> that corresponds to this tile.
        /// </summary>
        private Texture2D sprite;
        
        /// <summary>
        /// The main sprite that corresponds to this tile.
        /// </summary>
        private Texture2D mainSprite;

        private Vector2 origin;

        /// <summary>
        /// The <see cref="TileType"/> that corresponds to this tile.
        /// </summary>
        private TileType type;

        /// <summary>
        /// Determines in which direction(s) this <see cref="Tile"/> has a neighbor.
        /// </summary>
        private bool[] hasNeighbor;

        /// <summary>
        /// Holds the index from the <see cref="Map"/>.
        /// </summary>
        private int[] index;

        /// <summary>
        /// Holds indices of this tile's neighbors.
        /// </summary>
        private int[,] neighborIndices;

        /// <summary>
        /// The current rotation of this tile in degrees.
        /// </summary>
        private int rotation;
        #endregion

        #region Properties
        /// <summary>
        /// The <see cref="Point"/> this tile is located at.
        /// </summary>
        public Point Position { get { return new Point(rect.X, rect.Y); } }

        /// <summary>
        /// The <see cref="TileType"/> that corresponds to this tile.
        /// </summary>
        public TileType Type { get { return type; } }

        /// <summary>
        /// Determines in which direction(s) this <see cref="Tile"/> has a neighbor.
        /// </summary>
        public bool[] HasNeighbor { get { return hasNeighbor; } set { hasNeighbor = value; } }

        /// <summary>
        /// The index of this <see cref="Tile"/>.
        /// </summary>
        public int[] Index { get { return index; } }

        /// <summary>
        /// Stores the indices of neighboring tiles
        /// </summary>
        public int[,] NeighborIndices { get { return neighborIndices; } set { neighborIndices = value; } }

        /// <summary>
        /// The rotation of this <see cref="Tile"/>.
        /// </summary>
        private int Rotation { get { return rotation; } }
        #endregion

        #region Constructor
        public Tile(Point position, Texture2D sprite, TileType type, int rotation, int x, int y)
        {
            hasNeighbor = new bool[4];
            neighborIndices = new int[4, 4];

            mainSprite = sprite;
            this.sprite = sprite;
            this.type = type;
            this.rotation = rotation;

            index = new int[2] { x, y };

            rect = new Rectangle(position, new Point(768));

            origin = new Vector2(sprite.Width / 2);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates tile's logic.
        /// </summary>
        public void Update()
        {
            // Make sure the rotation stays within a value a byte can store
            if (Math.Abs(rotation) > 180)
            {
                rotation = 90 * Math.Sign(rotation);
            }

            if (rect.Contains(Vector2.Add(Vector2.Add(Input.MousePos().ToVector2(), Camera.Position), new Vector2(384))))
            {
                if (Designer.SelectedTexture != -1)
                {
                    sprite = Designer.TileSprites[Designer.SelectedTexture];

                    if (Input.MouseReleased(MouseButton.Right))
                    {
                        mainSprite = sprite;
                    }
                }

                int direction = Input.MouseScroll();

                if (direction != 0)
                {
                    rotation += 90 * direction;
                }
            }
            else
            {
                if (mainSprite != sprite)
                {
                    sprite = mainSprite;
                }
            }
        }

        /// <summary>
        /// Draws tile to the screen
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, rect, null, Color.White, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0f);
        }
        #endregion
    }
}

// -- Genoah Martinelli