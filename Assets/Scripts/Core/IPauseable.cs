namespace Assets.Scripts.Core
{
	/// <summary>
	/// Interface for pause game.
	/// </summary>
	public interface IPauseable
	{
		/// <summary>
		/// Set pause in game.
		/// </summary>
		/// <param name="doSet">True to set pause, else false.</param>
		void SetPause(bool doSet);
	}
}