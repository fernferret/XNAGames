using System;
using System.Collections.Generic;

namespace XNASystem
{
    class ScoreManager
    {
    	readonly String _playername;
    	private List<Score> _scores;

		public ScoreManager(String p)
		{
			_playername = p;
			_scores = new List<Score>();
		}
		public void SaveScore()
		{
			// Need Calls to Kenny's XML Loader to do this
			Console.WriteLine("Player: " + _playername);
		}
		public Score LoadScore(String player)
		{
			// Need Calls to Kenny's XML Loader to do this
			return null;
		}
		
		public void AddScore()
		{
			//_scores.Add());
		}
    }
}