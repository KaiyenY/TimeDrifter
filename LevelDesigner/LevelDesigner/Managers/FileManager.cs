using LevelDesigner.MapElements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LevelDesigner.Managers
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
        private static StreamReader sr;

        /// <summary>
        /// Writes information out to a binary file.
        /// </summary>
        private static StreamWriter sw;

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
                Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            saveDialog = new SaveFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*",
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
        public static Queue<string> Load()
        {
            // Instantiate new list to read data
            Queue<string> data = new Queue<string>();

            // Open the file dialog
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = openDialog.OpenFile()) != null)
                    {
                        // Initialize the reader
                        sr = new StreamReader(stream);

                        // Read all of the data from the stream
                        string line = null;
                        while ((line = sr.ReadLine()) != null)
                        {
                            data.Enqueue(line);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    stream.Close();
                    sr.Close();
                }
            }

            return data;
        }

        /// <summary>
        /// Writes <see cref="Map"/> information to a binary file.
        /// </summary>
        public static void Write()
        {
            // Initialize a data array with the map size
            List<string> data = new List<string>
            {
                $"{Map.Tiles.GetLength(0)},{Map.Tiles.GetLength(1)}"
            };

            // Read all information from each tile into the byte array
            for (int y = 0; y < Map.Tiles.GetLength(1); y++)
            {
                for (int x = 0; x < Map.Tiles.GetLength(0); x++)
                {
                    Tile current = Map.Tiles[x, y];

                    data.Add($"{(int)current.Type}," +
                        $"{current.Rotation}," +
                        $"{current.Index[0]}," +
                        $"{current.Index[1]}," +
                        $"{current.NeighborIndices.Count}");

                    // If neighbor count > 0, then the next few lines will have a list
                    // of this tile's neighbor's indices
                    if (current.NeighborIndices.Count > 0)
                    {
                        for (int i = 0; i < current.NeighborIndices.Count; i++)
                        {
                            data.Add($"{current.NeighborIndices[0][0]},{current.NeighborIndices[0][1]}");
                        }
                    }
                }
            }

            // Open the file dialog
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = saveDialog.OpenFile()) != null)
                    {
                        // Initialize the writer
                        sw = new StreamWriter(stream);

                        // Write all of the data into the text file
                        foreach (string input in data)
                        {
                            sw.WriteLine(input);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    sw.Close();
                }
            }
        }
        #endregion
    }
}

// -- Genoah Martinelli