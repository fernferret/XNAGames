using System;
using System.Collections.Generic;
using System.Xml;
using XNASystem.QuizArch;

// This class will be used to load questions via xml in later revs
namespace XNASystem
{
    class QuestionLoader
    {
        public QuestionLoader()
        {
            PopulateSystem();
        }
        public Booklet PopulateSystem()
        {
			Booklet math = new Booklet("Math");
			//Booklet history = new Booklet("History");

			Quiz mqz1 = new Quiz("Test Quiz 1");
			Quiz mqz2 = new Quiz("Test Quiz 2");
/*			Quiz mqz3 = new Quiz("Test Quiz 3");
			Quiz mqz4 = new Quiz("Test Quiz 4");
			Quiz mqz5 = new Quiz("Test Quiz 5");
			Quiz mqz6 = new Quiz("Test Quiz 6");
			Quiz mqz7 = new Quiz("Test Quiz 7");*/
			//Quiz hqz1 = new Quiz("Test Quiz 1 second");
			//Quiz hqz2 = new Quiz("Test Quiz 2 second");

			Question mq11 = new Question("What is 1+1?", new List<Answer> { new Answer("2", true), new Answer("1", false), new Answer("4", false), new Answer("3", false) });
			Question mq12 = new Question("What is 1+5?", new List<Answer> { new Answer("6", true), new Answer("5", false), new Answer("12", false), new Answer("7", false) });
			Question mq13 = new Question("What is 13-2?", new List<Answer> { new Answer("11", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

			Question mq21 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
			Question mq22 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
			Question mq23 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

/*
			Question mq31 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
			Question mq32 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
			Question mq33 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

			Question mq41 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
			Question mq42 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
			Question mq43 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

			Question mq51 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
			Question mq52 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
			Question mq53 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

			Question mq61 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
			Question mq62 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
			Question mq63 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

			Question mq71 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
			Question mq72 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
			Question mq73 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });
*/

			mqz1.AddItem(mq11);
			mqz1.AddItem(mq12);
			mqz1.AddItem(mq13);

			mqz2.AddItem(mq21);
			mqz2.AddItem(mq22);
			mqz2.AddItem(mq23);

/*			mqz3.AddItem(mq31);
			mqz3.AddItem(mq32);
			mqz3.AddItem(mq33);

			mqz4.AddItem(mq41);
			mqz4.AddItem(mq42);
			mqz4.AddItem(mq43);

			mqz5.AddItem(mq51);
			mqz5.AddItem(mq52);
			mqz5.AddItem(mq53);

			mqz6.AddItem(mq61);
			mqz6.AddItem(mq62);
			mqz6.AddItem(mq63);

			mqz7.AddItem(mq71);
			mqz7.AddItem(mq72);
			mqz7.AddItem(mq73);*/


			math.AddItem(mqz1);
			math.AddItem(mqz2);
			//math.AddItem(mqz3);
			//math.AddItem(mqz4);
			//math.AddItem(mqz5);
			//math.AddItem(mqz6);
			//math.AddItem(mqz7);
			//history.AddItem(hqz1);
			//history.AddItem(hqz2);

			//booklets.Add(math);
            return math;
        }
    }
}
