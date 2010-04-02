
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.Utils
{
	class Instruction
	{
		private String _text;
		private int _x;
		private int _y;
		private Texture2D _image;
		private int _timeToDisplay;
		private int _currentTime;
		private Color _color;
		private const int ButtonDim = 48;
		private const int PressOffset = 80;
		private const int ButtonYOffset = -4;
		public Instruction(int x, int y, String text, Texture2D buttonImage, int seconds)
		{
			_x = x;
			_y = y;
			_text = text;
			_image = buttonImage;
			
			_currentTime = SystemMain.CurrentGameTime.TotalRealTime.Seconds;

			if(seconds >= 0)
				_timeToDisplay = seconds + _currentTime;
			else
				_timeToDisplay = -1;
			_color = new Color(Color.White,255);
		}
		public void Update()
		{
			// We have a display time (not infinite) and we're past that time, start fade
			if (_timeToDisplay >= 0 && !(_timeToDisplay > SystemMain.CurrentGameTime.TotalRealTime.Seconds) && _color.A > 0)
			{
				_color.A-=3;
			}
		}
		public void Draw()
		{
			SystemMain.GameSpriteBatch.DrawString(SystemMain.FontPackage["Title"], "Press", new Vector2(_x, _y), _color);
			SystemMain.GameSpriteBatch.Draw(_image, new Rectangle(_x + PressOffset, _y + ButtonYOffset, ButtonDim, ButtonDim), _color);
			SystemMain.GameSpriteBatch.DrawString(SystemMain.FontPackage["Title"], _text, new Vector2(PressOffset + ButtonDim + _x, _y), _color);
		}
	}
}
