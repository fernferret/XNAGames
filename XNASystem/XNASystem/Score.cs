using System;
using System.Collections.Generic;

namespace XNASystem
{
	public class Score
	{
		#region Variables
		/// <summary>
		/// The date when the score was achieved
		/// </summary>
		private DateTime _date;

		/// <summary>
		/// The Name of the player
		/// </summary>
		public String PlayerName { get; set; }

		/// <summary>
		/// The type of score (Game or Quiz)
		/// </summary>
		public ActivityType Type { get; set; }

		/// <summary>
		/// The value for this specific score
		/// </summary>
		public int Value { get; set; }

		/// <summary>
		/// The Title of the Quiz/ Game Level
		/// </summary>
		public String ItemTitle { get; set; }
		#endregion

		/// <summary>
		/// Creates a score for a game level or quiz
		/// </summary>
		/// <param name="p">The Player Name</param>
		/// <param name="t">The Activity Type</param>
		/// <param name="s"></param>
		/// <param name="title"></param>
		public Score(String p, ActivityType t, int s, String title)
		{
			_date = DateTime.Now;
			Value = s;
			Type = t;
			PlayerName = p;
			ItemTitle = title;
		}
		/// <summary>
		/// Returns a list of strings that will contain all the data for a save except the Type
		/// </summary>
		/// <returns>List of strings for save data</returns>
		public List<String> GetDetailsAsString()
		{
			return new List<String> { ItemTitle, PlayerName, Value.ToString(), _date.ToString("{0:f}") };
		}
		/// <summary>
		/// Returns the date, this is effectively an accessor, as we don't want modification of the date
		/// </summary>
		/// <returns>The Date the Score was achieved</returns>
		public DateTime Date()
		{
			return _date;
		}
	}
}
