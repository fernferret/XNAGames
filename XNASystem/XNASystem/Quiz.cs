using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace XNASystem
{
    class Quiz
    {
        // The listing of the currently loaded questions
        List<Question> QList;

        public void loadQuestions()
        {
            //TODO: Load the questions from the current booklet into the quiz.
        }

        public void showQuestion()
        {
            //TODO: Display the current question to the user
        }
    }
}