using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XNASystem.QuizArch;

namespace XNASystemTest
{
	[TestClass]
	public class BookletStackTest
	{
		private Booklet _target;

		[TestInitialize]
		public void SetUp()
		{

		}
		[TestCleanup]
		public void CleanUp()
		{
			
		}
		[TestMethod]
		public void CreateQuizWithTitle()
		{
			_target = new Booklet("A Booklet");
			Assert.IsNotNull(_target);
		}

		[TestMethod]
		public void CreateQuizAndAddQuestion()
		{
			_target = new Booklet("A Booklet");
			var quiz = new Quiz("A Quiz");
			var question = new Question("Test",new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			Assert.IsNotNull(_target);
			quiz.AddItem(question);
			_target.AddItem(quiz);
			Assert.IsNotNull(_target);
			_target.GetOpenQuiz();
			Assert.AreEqual(_target.GetNextQuiz(), quiz);
			Assert.AreEqual(_target.GetOpenQuiz(), null);
			Assert.AreEqual(_target.GetNextQuiz(), null);
		}
		[TestMethod]
		public void CreateQuizAndAddQuestions()
		{
			_target = new Booklet("A Booklet");
			var quiz = new Quiz("A Quiz");
			var question = new Question("Test", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			for (var i = 0; i < 10; i++)
			{
				quiz.AddItem(question);
				_target.AddItem(quiz);
			}
			Assert.AreEqual(_target.GetStatus(), Status.NotStarted, "The Quiz should not have started");
			Assert.IsNotNull(_target.GetOpenQuiz());
			for (var i = 0; i < 10; i++)
			{
				// We're poping one off each time
				Assert.AreEqual(_target.GetStatus(), Status.InProgress, "The Quiz should be in progress");
				Assert.AreEqual(_target.GetNextQuiz(), quiz);
			}
			// There are no more questions left, we should have a null return
			Assert.AreEqual(_target.GetStatus(), Status.Completed, "Is it done yet?");
			Assert.AreEqual(_target.GetNextQuiz(), null);
			Assert.AreEqual(_target.GetStatus(), Status.Completed, "The Quiz should be done");
		}

		[TestMethod]
		public void LetsTryResetingAQuiz()
		{
			_target = new Booklet("A Booklet");
			var quiz = new Quiz("A Quiz");
			var question = new Question("Test", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			for (var i = 0; i < 10; i++)
			{
				quiz.AddItem(question);
				_target.AddItem(quiz);
			}
			// Take the Quiz
			Assert.IsNotNull(_target.GetOpenQuiz());
			for (var i = 0; i < 10; i++)
			{
				// We're poping one off each time
				Assert.AreEqual(_target.GetStatus(), Status.InProgress, "The Quiz should be in progress");
				Assert.AreEqual(_target.GetNextQuiz(), quiz);
			}
			// There are no more questions left, we should have a null return
			Assert.AreEqual(Status.Completed, _target.GetStatus(), "Is it done yet?");
			Assert.AreEqual(_target.GetNextQuiz(), null);
			Assert.AreEqual(Status.Completed, _target.GetStatus(), "The Quiz should be done");

		}
		[TestMethod]
		public void RandomAnswersYouSay()
		{
			_target = new Booklet("A Booklet");
			var quiz = new Quiz("A Quiz");
			var question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			var question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			var question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			var question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);
			var a = _target.GetOpenQuiz().GetRandomQuestions();
			var a2 = _target.GetOpenQuiz().GetAllQuestions();
			var a3 = _target.GetOpenQuiz().GetRandomQuestions();
			_target.AddItem(quiz);
			Assert.AreNotEqual(a, a2, "These Should not be equal (But this test will fail one every 5! times)");
		}
		[TestMethod]
		public void GetASpecificQuiz()
		{
			_target = new Booklet("A Booklet");
			var quiz = new Quiz("A Quiz 0");
			var question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			var question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			var question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			var question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 1");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 2");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 3");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 4");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 5");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 6");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 7");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 8");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);

			quiz = new Quiz("A Quiz 9");
			question = new Question("Test1", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question);
			question2 = new Question("Test2", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question2);
			question3 = new Question("Test3", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question3);
			question4 = new Question("Test4", new List<Answer> { new Answer("Test", false), new Answer("Correct", true), new Answer("Wrong", false) });
			quiz.AddItem(question4);
			_target.AddItem(quiz);
			var q = _target.GetSpecificQuiz(2);
			var p = _target.GetSpecificQuiz(6);
			_target.GetNextQuiz();
			_target.GetNextQuiz();
			_target.GetNextQuiz();
			_target.GetNextQuiz();
			var m = _target.GetAsList();
			q = _target.GetSpecificQuiz(2);
			p = _target.GetSpecificQuiz(6);
			//Assert.AreNotEqual(a, a2, "These Should not be equal (But this test will fail one every 5! times)");
		}

	}
}
