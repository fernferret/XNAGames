using Microsoft.VisualStudio.TestTools.UnitTesting;
using XNASystem;

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
	}
}
