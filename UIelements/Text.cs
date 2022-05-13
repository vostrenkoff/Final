using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.UIelements
{
	internal class Text : EasyDraw
	{
		public Text(int width, int height, int x, int y, string text, int size) : base(width, height)
		{
			SetOrigin(width/2, height/2);
			Fill(255, 255, 255);
			SetXY(x, y);
			TextSize(size);
			TextAlign(CenterMode.Center, CenterMode.Center);
			Text(text);
		}
		public void UpdateText(string text)
		{
			Clear(0, 0, 0,0);
			Text(text);
		}
	}
}
