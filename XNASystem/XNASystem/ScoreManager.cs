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
		public void AddScore(Score s)
		{
			_scores.Add(s);
		}

		internal List<Score> GetScores()
		{
			return _scores;
		}

		internal Score GetCumulativeQuizScore()
		{
			var totalPercent = 0;
			var totalPoints = 0;
			var i = 1;
			foreach (var score in _scores)
			{
				totalPercent = (totalPercent + score.Percentage)/i;
				totalPoints += score.Value;
				i++;
			}
			var s = new Score(_scores[0].PlayerName,ActivityType.Quiz, totalPoints, "Cumulative Quiz Score"){Percentage = totalPercent};
			return s;
		}
	}
}