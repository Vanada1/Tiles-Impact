using UnityEngine.Tilemaps;

namespace Assets.Scripts.Core
{
	/// <summary>
	/// Tile data class.
	/// </summary>
	public class TileData
	{
		/// <summary>
		/// Returns tile X coordinate.
		/// </summary>
		public int X { get; }

		/// <summary>
		/// Return tile Y coordinate.
		/// </summary>
		public int Y { get; }

		/// <summary>
		/// Return tile.
		/// </summary>
		public TileBase Tile { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="x">Tile X coordinate.</param>
		/// <param name="y">Tile Y coordinate.</param>
		/// <param name="tile">Tile.</param>
		public TileData(int x, int y, TileBase tile)
		{
			X = x;
			Y = y;
			Tile = tile;
		}
	}
}