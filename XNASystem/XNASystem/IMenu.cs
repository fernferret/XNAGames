using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNASystem
{
    public interface IMenu
    {
        void Update(KeyboardState state);

        void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D box, Texture2D background);
    }
}
