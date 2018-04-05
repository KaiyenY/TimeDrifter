using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LevelDesigner
{
    /// <summary>
    /// Assists in loading and saving files for the <see cref="Map"/>.
    /// </summary>
    public static class FileManager
    {
        #region Fields
        /// <summary>
        /// Reads information coming in from a binary file.
        /// </summary>
        private static BinaryReader br;

        /// <summary>
        /// Writes information out to a binary file.
        /// </summary>
        private static BinaryWriter bw;

        /// <summary>
        /// The <see cref="OpenFileDialog"/> opens File Explorer to open designated file.
        /// </summary>
        private static OpenFileDialog openDialog;

        /// <summary>
        /// The <see cref="SaveFileDialog"/> opens File Explorer to save designated file.
        /// </summary>
        private static SaveFileDialog saveDialog;

        /// <summary>
        /// The <see cref="Stream"/> that may contain a valid <see cref="FileStream"/>.
        /// </summary>
        private static Stream stream;
        #endregion

        #region Constructor
        static FileManager()
        {
            openDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Map Files (*.map)|*.map|All files (*.*)|*.*",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            saveDialog = new SaveFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Map Files (*.map)|*.map|All files (*.*)|*.*",
                FilterIndex = 0,
                RestoreDirectory = true
            };
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads in <see cref="Map"/> information from the binary file at the specified path.
        /// </summary>
        /// <returns>A list of byte data from the specified file.</returns>
        public static List<byte> Load()
        {
            // Instantiate new list to read data
            List<byte> data = new List<byte>();

            // Open the file dialog
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = openDialog.OpenFile()) != null)
                    {
                        // Initialize the reader
                        br = new BinaryReader(stream);

                        // Read the first two bytes to get the map size
                        data.AddRange(br.ReadBytes(2));

                        // Read the rest of the file and store it all in data
                        data.AddRange(ReadAllBytes(stream));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    stream.Close();
                    br.Close();
                }
            }

            return data;
        }

        /// <summary>
        /// Allows the <see cref="BinaryReader"/> to read all bytes in a binary file.
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> that contains the binary file.</param>
        /// <returns>All bytes in this array.</returns>
        public static byte[] ReadAllBytes(Stream stream)
        {
            using (var memStream = new MemoryStream())
            {
                stream.CopyTo(memStream);
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// Writes <see cref="Map"/> information to a binary file.
        /// </summary>
        public static void Write()
        {
            // Initialize a data array with the map size
            List<byte> data = new List<byte>
            {
                (byte)Map.Tiles.GetLength(0),
                (byte)Map.Tiles.GetLength(1)
            };

            // Read all information from each tile into the byte array
            for (int y = 0; y < data[0]; y++)
            {
                for (int x = 0; x < data[1]; x++)
                {
                    Tile current = Map.Tiles[x, y];

                    // Add the type of tile
                    data.Add((byte)current.Type);

                    // Add the index of the tile
                    data.Add((byte)current.Index[0]);
                    data.Add((byte)current.Index[1]);
                }
            }

            // Open the file dialog
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream == saveDialog.OpenFile()))
                    {
                        // Initialize the writer
                        bw = new BinaryWriter(stream);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {

                }
            }
        }
        #endregion
    }
}
