using System;
using System.Collections.Generic;
using System.Xml;
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
            var questionList = new Stack<Question>();
            var questionList2 = new Stack<Question>();
            var quizList = new Stack<Quiz>();
            
            // Add the First Question
            questionList.Push(new Question("What is 1 + 1", new List<Answer>
                                                               {
                                                                  new Answer("0", false),
                                                                  new Answer("1", false),
                                                                  new Answer("2", true),
                                                                  new Answer("3", false)
                                                               }));

            // Add the Second Question
			questionList.Push(new Question("What is 4 * 3", new List<Answer>
                                                               {
                                                                  new Answer("|-12|", true),
                                                                  new Answer("12", true),
                                                                  new Answer("27", false),
                                                                  new Answer("24", false)
                                                               }));

            // Add The First Quiz
            quizList.Push(new Quiz("Math 1", questionList));

            // Add the First Question
			questionList2.Push(new Question("Where is the Bay of Pigs?", new List<Answer>
                                                               {
                                                                  new Answer("Germany", false),
                                                                  new Answer("Cuba", true),
                                                                  new Answer("Japan", false),
                                                                  new Answer("Hawaii", false)
                                                               }));

            // Add the Second Question
			questionList2.Push(new Question("When was M. P. T. Acharya born?", new List<Answer>
                                                               {
                                                                  new Answer("1987", false),
                                                                  new Answer("1851", false),
                                                                  new Answer("1887", true),
                                                                  new Answer("1890", false)
                                                               }));

            // Add The Second Quiz
            quizList.Push(new Quiz("History 1", questionList2));
            var b = new Booklet("Easy Questions",quizList);
            return b;
        }
    }
}
