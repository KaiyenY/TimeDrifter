using LevelDesigner.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
        /// Keeps track of any neighbors around this tile.
        /// </summary>
        private List<int[]> neighborIndices;

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
        /// Holds the index from the <see cref="Map"/>.
        /// </summary>
        private int[] index;

        /// <summary>
        /// The current rotation of this tile in degrees.
        /// </summary>
        private int rotation;
        #endregion

        #region Properties
        /// <summary>
        /// Keeps track of any neighbors around this tile.
        /// </summary>
        public List<int[]> NeighborIndices { get { return neighborIndices; } set { neighborIndices = value; } }

        /// <summary>
        /// The <see cref="Point"/> this tile is located at.
        /// </summary>
        public Point Position { get { return new Point(rect.X, rect.Y); } }

        /// <summary>
        /// The <see cref="TileType"/> that corresponds to this tile.
        /// </summary>
        public TileType Type { get { return type; } }

        /// <summary>
        /// The index of this <see cref="Tile"/>.
        /// </summary>
        public int[] Index { get { return index; } }

        /// <summary>
        /// The rotation of this <see cref="Tile"/>.
        /// </summary>
        public int Rotation { get { return rotation; } }
        #endregion

        #region Constructor
        public Tile(Point position, Texture2D sprite, TileType type, int neighborCount, int rotation, int x, int y)
        {
            mainSprite = sprite;
            this.sprite = sprite;
            this.type = type;
            this.rotation = rotation;

            index = new int[2] { x, y };

            rect = new Rectangle(position, new Point(768));

            origin = new Vector2(sprite.Width / 2);

            neighborIndices = new List<int[]>(neighborCount);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines where and how many neighbors this tile has.
        /// </summary>
        public void GrabNeighbors()
        {
            if (neighborIndices.Count > 0)
            {
                if (index[0] > 0)
                {
                    if (Map.Tiles[index[0] + 1, index[1]].Type != TileType.Building)
                    {
                        neighborIndices.Add(new int[2]
                        {
                            index[0] + 1,
                            index[1]
                        });
                    }
                }
                if (index[0] < Map.Tiles.GetLength(0) - 1)
                {
                    if (Map.Tiles[index[0] - 1, index[1]].Type != TileType.Building)
                    {
                        neighborIndices.Add(new int[2]
                        {
                            index[0] - 1,
                            index[1]
                        });
                    }
                }
                if (index[1] > 0)
                {
                    if (Map.Tiles[index[0], index[1] + 1].Type != TileType.Building)
                    {
                        neighborIndices.Add(new int[2]
                        {
                            index[0],
                            index[1] + 1
                        });
                    }
                }
                if (index[1] < Map.Tiles.GetLength(1) - 1)
                {
                    if (Map.Tiles[index[0], index[1] - 1].Type != TileType.Building)
                    {
                        neighborIndices.Add(new int[2]
                        {
                            index[0],
                            index[1] - 1
                        });
                    }
                }
            }
        }

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
                        type = (TileType)Designer.SelectedTexture;

                        GrabNeighbors();

                        if (neighborIndices.Count > 0)
                        {
                            foreach(int[] index in neighborIndices)
                            {
                                Map.Tiles[index[0], index[1]].GrabNeighbors();
                            }
                        }
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