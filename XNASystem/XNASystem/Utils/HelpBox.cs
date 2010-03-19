using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.Utils
{
	class HelpBox
	{
		private int _currentXPos;
		private int _currentYPos;
		private int _currentWidth = 49;
		private int _currentHeight = 49;
		private int _finalXPos;
		private int _finalYPos;
		private int _finalWidth;
		private int _finalHeight;
		private int _speed;
		private SpriteBatch _sb;
		private Texture2D[] _t;
		private String _text;
		private SpriteFont _font;
		private byte _textAlpha;
		public HelpBox(SpriteBatch sb, int x, int y, int width, int height, int speed, Texture2D[] t, String text,SpriteFont font)
		{
			_finalXPos = x-(width/2);
			_finalYPos = y-(height/2);
			_finalWidth = width;
			_finalHeight = height;
			_currentXPos = _finalXPos;
			_currentYPos = SystemMain.Height;
			_speed = 5;
			_t = t;
			_sb = sb;
			_font = font;
			_text = text;
			_textAlpha = 0;
		}
		public void UpdatePosition()
		{
			

			if (_currentHeight < _finalHeight)
			{
				_currentHeight += 2 * _speed;
				_currentYPos -= 1 * _speed;
			}
			else if (_currentYPos < _finalYPos)
			{
				_currentYPos += _speed;
			}
			else if (_currentYPos > _finalYPos)
			{
				_currentYPos -= _speed;
			}
			else
			{
				if (_currentWidth < _finalWidth)
				{
					_currentWidth += 2 * _speed;
					_currentXPos -= 1 * _speed;
				}
				else
				{
					_textAlpha = (byte)Math.Min(_textAlpha + 5, 255);
				}
				/*else if (_currentXPos < _finalXPos)
				{
					_currentXPos += _speed;
				}
				else if (_currentXPos > _finalXPos)
				{
					_currentXPos -= _speed;
				}*/
			}
		}
		public void DrawRectangle()
		{
			// Upper Left
			_sb.Draw(_t[0], new Rectangle(_currentXPos, _currentYPos, 24, 24), Color.White);
			// Upper Right
			_sb.Draw(_t[0], new Rectangle(_currentXPos + _currentWidth, _currentYPos, 24, 24), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
			
			// Lower Left
			_sb.Draw(_t[0], new Rectangle(_currentXPos, _currentYPos + _currentHeight, 24, 24), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
			// Lower Right
			_sb.Draw(_t[0], new Rectangle(_currentXPos + 24 + _currentWidth, _currentYPos + 24 + _currentHeight, 24, 24), null, Color.White, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0);
			
			// Draw Fill
			_sb.Draw(_t[2], new Rectangle(_currentXPos + 24, _currentYPos + 24, _currentWidth - 24, _currentHeight - 24), Color.White);

			// Top Border
			_sb.Draw(_t[1], new Rectangle(_currentXPos + 24, _currentYPos, _currentWidth - 24, 24), Color.White);
			// Bottom Border
			_sb.Draw(_t[1], new Rectangle(_currentXPos + 24, _currentYPos + _currentHeight, _currentWidth - 24, 24), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);

			// Right Border
			_sb.Draw(_t[1], new Rectangle(_currentXPos + 24 + _currentWidth, _currentYPos + 24, _currentHeight - 24, 24), null, Color.White, (float)(Math.PI / 2), new Vector2(0, 0), SpriteEffects.None, 0);
			// Left Border
			_sb.Draw(_t[1], new Rectangle(_currentXPos + 24, _currentYPos + 24, _currentHeight - 24, 24), null, Color.White, (float)(Math.PI / 2), new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
			_sb.DrawString(SystemMain.FontPackage["Main"],_currentXPos+", "+_finalXPos,new Vector2(500,500), Color.White );
			var textColor = Color.White;
			textColor = new Color(textColor.R, textColor.G, textColor.B, _textAlpha);
			_sb.DrawString(_font,"test",new Vector2(_currentXPos,_currentYPos), textColor);
		}
	}
}
