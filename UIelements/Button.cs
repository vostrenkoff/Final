using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.UIelements
{
	internal class Button : Sprite
	{
		Action buttonAction;
		int buttonX;
		int buttonY;
		bool mouseInBounds;
		int framesAlive = 0;
		int lastFrame;
		public Button(int x, int y, string spritePath, string text, Action buttonAction) : base(spritePath)
		{
			ES.current.onUpdate += Update;
			SetOrigin(width / 2, height / 2);
			SetXY(x, y);
			buttonX = x;
			buttonY = y;
			this.buttonAction = buttonAction;
			Text buttonText = new Text(width, height, 0, 0, text, 20);
			AddChild(buttonText);
		}
		private void Update()
		{
			framesAlive++;
			int x = Input.mouseX;
			int y = Input.mouseY;
			bool mouseInBoundsX = x >  buttonX - width/2 && x < buttonX + width/2;
			bool mouseInBoundsY = y > buttonY - height/2 && y < buttonY + height/2;
			mouseInBounds = mouseInBoundsX && mouseInBoundsY;
			CheckPress();
			HighlightButton();
		}
		private void HighlightButton()
		{
			if (mouseInBounds)
			{
				blendMode = BlendMode.PREMULTIPLIED;
			}
			else
			{
				blendMode = BlendMode.NORMAL;
			}
		}
		private void CheckPress()
		{
			if(Input.GetMouseButtonUp(0)&&mouseInBounds&&framesAlive-lastFrame > 3)
			{
				lastFrame = framesAlive;
				buttonAction.Invoke();
			}

		}
	}
}
