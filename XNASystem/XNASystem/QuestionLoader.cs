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
            var answerList = new List<Answer>();
            var questionList = new List<Question>();
            var quizList = new List<Quiz>();
            
            // Add the First Question
            answerList.Add(new Answer("0", false));
            answerList.Add(new Answer("1", false));
            answerList.Add(new Answer("2", true));
            answerList.Add(new Answer("3", false));
            questionList.Add(new Question("What is 1 + 1", answerList));
            answerList.Clear();

            // Add the Second Question
            answerList.Add(new Answer("-12", true));
            answerList.Add(new Answer("12", true));
            answerList.Add(new Answer("27", false));
            answerList.Add(new Answer("24", false));
            questionList.Add(new Question("What is 4 * 3", answerList));
            answerList.Clear();

            // Add The First Quiz
            quizList.Add(new Quiz("Math 1", questionList));
            questionList.Clear();

            // Add the First Question
            answerList.Add(new Answer("Germany", false));
            answerList.Add(new Answer("Cuba", true));
            answerList.Add(new Answer("Japan", false));
            answerList.Add(new Answer("Hawaii", false));
            questionList.Add(new Question("Where is the Bay of Pigs?", answerList));
            answerList.Clear();

            // Add the Second Question
            answerList.Add(new Answer("1897", false));
            answerList.Add(new Answer("1851", false));
            answerList.Add(new Answer("1887", true));
            answerList.Add(new Answer("1890", false));
            questionList.Add(new Question("When was M. P. T. Acharya born?", answerList));
            answerList.Clear();

            // Add The Second Quiz
            quizList.Add(new Quiz("History 1", questionList));
            questionList.Clear();
            var b = new Booklet("Easy Questions",quizList);
            return b;
        }
    }
}
