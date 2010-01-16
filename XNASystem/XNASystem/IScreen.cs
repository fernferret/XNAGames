using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNASystem
{
    public interface IScreen
    {
        void Update(KeyboardState state);

        void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures);
    }
}
