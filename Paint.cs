using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	internal class Paint : Sprite
	{
		public Paint(uint color) :base("luzha.png")
		{
			float scale = 0.1f;
			SetScaleXY(scale);
			this.color = color;
		}
	}
}
