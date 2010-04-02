using System;
using System.Collections.Generic;

namespace XNASystem.QuizArch
{
	public class Answer
	{
		private readonly Boolean _correct;
		private bool _questionAnswered;
		public Answer(String answer, Boolean correct)
		{
			TheAnswer = answer;
			_correct = correct;
		}

		public string TheAnswer { get; private set; }

		public Boolean IsCorrect()
		{
			return _correct;
		}

		public new String ToString()
		{
			return TheAnswer;
		}

		public void Choose()
		{
			_questionAnswered = true;
		}

		public bool HasBeenAnswered()
		{
			return _questionAnswered;
		}
		public Answer(byte[] bytes, ref int position)
		{
			TheAnswer = DataManager.ReadStringFromByteArray(bytes, ref position);
			_correct = DataManager.ReadBooleanFromByteArray(bytes, ref position);
		}

		public byte[] ToByteArray()
		{
			List<byte> bytes = new List<byte>();

			//Serializing answer
			bytes.Add((byte)(TheAnswer.Length / byte.MaxValue));
			bytes.Add((byte)(TheAnswer.Length % byte.MaxValue));
			foreach (char c in TheAnswer)
			{
				bytes.Add((byte)c);
			}

			//Serialize whether the answer is correct
			bytes.Add((byte)(_correct ? 1 : 0));

			return bytes.ToArray();
		}
	}
}