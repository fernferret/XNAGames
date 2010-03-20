using Microsoft.VisualStudio.TestTools.UnitTesting;
using XNASystem;
using XNASystem.QuizArch;
using System.Collections.Generic;

namespace XNASystemTest
{
	[TestClass]
	public class QuizTest
	{
		private Quiz target;

		[TestInitialize]
		public void SetUp()
		{
			target = new Quiz("A Quiz");
		}

		[TestMethod]
		public void CreateQuizWithTitle()
		{
			Assert.IsNotNull(target);
		}

		[TestMethod]
		public void TestMethod()
		{
			
		}

		/// <summary>
		///A test for ToString
		///</summary>
		[TestMethod()]
		public void ToStringTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			actual = target.ToString();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ToByteArray
		///</summary>
		[TestMethod()]
		public void ToByteArrayTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			byte[] expected = null; // TODO: Initialize to an appropriate value
			byte[] actual;
			actual = target.ToByteArray();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Reset
		///</summary>
		[TestMethod()]
		public void ResetTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.Reset();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for HasQuizBeenCompleted
		///</summary>
		[TestMethod()]
		[DeploymentItem("XNASystem.exe")]
		public void HasQuizBeenCompletedTest()
		{
			PrivateObject param0 = null; // TODO: Initialize to an appropriate value
			Quiz_Accessor target = new Quiz_Accessor(param0); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.HasQuizBeenCompleted();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetTotalQuestionCount
		///</summary>
		[TestMethod()]
		public void GetTotalQuestionCountTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			int expected = 0; // TODO: Initialize to an appropriate value
			int actual;
			actual = target.GetTotalQuestionCount();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetTitle
		///</summary>
		[TestMethod()]
		public void GetTitleTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			actual = target.GetTitle();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetStatus
		///</summary>
		[TestMethod()]
		public void GetStatusTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			Status expected = new Status(); // TODO: Initialize to an appropriate value
			Status actual;
			actual = target.GetStatus();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetRandomQuestions
		///</summary>
		[TestMethod()]
		public void GetRandomQuestionsTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			List<Question> expected = null; // TODO: Initialize to an appropriate value
			List<Question> actual;
			actual = target.GetRandomQuestions();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetQuestionsLeft
		///</summary>
		[TestMethod()]
		public void GetQuestionsLeftTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			int expected = 0; // TODO: Initialize to an appropriate value
			int actual;
			actual = target.GetQuestionsLeft();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetPercentDone
		///</summary>
		[TestMethod()]
		public void GetPercentDoneTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			double expected = 0F; // TODO: Initialize to an appropriate value
			double actual;
			actual = target.GetPercentDone();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetOpenQuestion
		///</summary>
		[TestMethod()]
		public void GetOpenQuestionTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			Question expected = null; // TODO: Initialize to an appropriate value
			Question actual;
			actual = target.GetOpenQuestion();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetNextQuestion
		///</summary>
		[TestMethod()]
		public void GetNextQuestionTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			Question expected = null; // TODO: Initialize to an appropriate value
			Question actual;
			actual = target.GetNextQuestion();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetAllQuestions
		///</summary>
		[TestMethod()]
		public void GetAllQuestionsTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			List<Question> expected = null; // TODO: Initialize to an appropriate value
			List<Question> actual;
			actual = target.GetAllQuestions();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for AnswerQuestion
		///</summary>
		[TestMethod()]
		public void AnswerQuestionTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			Answer a = null; // TODO: Initialize to an appropriate value
			target.AnswerQuestion(a);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for AddItem
		///</summary>
		[TestMethod()]
		public void AddItemTest()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title); // TODO: Initialize to an appropriate value
			Question item = null; // TODO: Initialize to an appropriate value
			target.AddItem(item);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for Quiz Constructor
		///</summary>
		[TestMethod()]
		public void QuizConstructorTest2()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for Quiz Constructor
		///</summary>
		[TestMethod()]
		public void QuizConstructorTest1()
		{
			string title = string.Empty; // TODO: Initialize to an appropriate value
			Stack<Question> questions = null; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(title, questions);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for Quiz Constructor
		///</summary>
		[TestMethod()]
		public void QuizConstructorTest()
		{
			byte[] bytes = null; // TODO: Initialize to an appropriate value
			int position = 0; // TODO: Initialize to an appropriate value
			int positionExpected = 0; // TODO: Initialize to an appropriate value
			Quiz target = new Quiz(bytes, ref position);
			Assert.AreEqual(positionExpected, position);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
