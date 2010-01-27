using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XNASystem;
using XNASystem.QuizArch;

namespace XNASystemTest
{
	[TestClass]
	public class QuizStackTest
	{
		private Quiz _target;

		[TestInitialize]
		public void SetUp()
		{
			
		}
		[TestMethod]
		public void CreateQuizWithTitle()
		{
			_target = new Quiz("A Quiz");
			Assert.IsNotNull(_target);
		}

		[TestMethod]
		public void CreateQuizAndAddQuestion()
		{
			_target = new Quiz("A Quiz");
			var q = new Question("Test",
									new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			Assert.IsNotNull(_target);
			_target.AddItem(q);
			Assert.IsNotNull(_target);
			_target.GetOpenQuestion();
			Assert.AreEqual(_target.GetNextQuestion(), q);
			Assert.AreEqual(_target.GetOpenQuestion(), null);
			Assert.AreEqual(_target.GetNextQuestion(), null);
		}
		[TestMethod]
		public void CreateQuizAndAddQuestions()
		{
			_target = new Quiz("A Quiz");
			var q = new Question("Test", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			for(var i=0; i < 10; i++)
			{
				_target.AddItem(q);
			}
			Assert.AreEqual(_target.GetStatus(),Status.NotStarted,"The Quiz should not have started");
			Assert.IsNotNull(_target.GetOpenQuestion());
			for (var i = 0; i < 10; i++)
			{
				// We're poping one off each time
				Assert.AreEqual(_target.GetStatus(), Status.InProgress, "The Quiz should be in progress");
				Assert.AreEqual(_target.GetNextQuestion(), q);
			}
			// There are no more questions left, we should have a null return
			Assert.AreEqual(_target.GetStatus(), Status.Completed, "Is it done yet?");
			Assert.AreEqual(_target.GetNextQuestion(), null);
			Assert.AreEqual(_target.GetStatus(), Status.Completed, "The Quiz should be done");
		}

		[TestMethod]
		public void LetsTryResetingAQuiz()
		{
			_target = new Quiz("A Quiz");
			var q = new Question("Test", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			for (var i = 0; i < 10; i++)
			{
				_target.AddItem(q);
			}
			// Take the Quiz
			Assert.IsNotNull(_target.GetOpenQuestion());
			for (var i = 0; i < 10; i++)
			{
				// We're poping one off each time
				Assert.AreEqual(_target.GetStatus(), Status.InProgress, "The Quiz should be in progress");
				Assert.AreEqual(_target.GetNextQuestion(), q);
			}
			// There are no more questions left, we should have a null return
			Assert.AreEqual(Status.Completed, _target.GetStatus(), "Is it done yet?");
			Assert.AreEqual(_target.GetNextQuestion(), null);
			Assert.AreEqual(Status.Completed, _target.GetStatus(), "The Quiz should be done");

		}
		[TestMethod]
		public void RandomAnswersYouSay()
		{
			_target = new Quiz("A Quiz");
			var q = new Question("Test", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false), new Answer("Wrong", false), new Answer("Wrong", false) });
			_target.AddItem(q);
			var a = _target.GetOpenQuestion().GetRandomAnswers();
			var a2 = _target.GetNextQuestion().GetAllAnswers();
			_target.AddItem(q);
			Assert.AreNotEqual(a,a2,"These Should not be equal (But this test will fail one every 5! times)");
		}

	}
}
