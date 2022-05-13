using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	public class Block : Sprite
	{
		float mx, my;
		bool allowMove;
		public Block(uint color) : base("block.png")
		{
			float scale = 0.3f;
			SetScaleXY(scale);
			this.color = color;
			ES.current.onUpdate += Update;
			ES.current.onMove += UpdatePos;
			allowMove = true;
		}
		public Block(uint color, bool stationary) : base("block.png")
		{
			float scale = 0.3f;
			SetScaleXY(scale);
			this.color = color;
			ES.current.onUpdate += Update;
			ES.current.onMove += UpdatePos;
			allowMove = false;
		}
		public void UpdatePos()
		{
			mx = x;
			my = y;
		}
		private void UpdatePos(float offset)
		{
			my -= offset;
		}
		private void Update()
		{
			if (allowMove)
			SetXY(mx, my);
		}
	}
}
