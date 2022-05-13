using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	public class Trampoline : AnimationSprite
	{
		public Trampoline() : base("tr2.png", 4, 2)
		{
			ES.current.onUpdate += Update;
			_animationDelay = 3;
			float scale = 0.5f;
			SetScaleXY(scale, scale);
			SetCycle(0, 7);
		}
		int currentFrame = 0;
		int framesAlive = 320;
		int lastCycle = 0;
		int lastTouch;
		bool animate = false;
		private void Update()
		{
			framesAlive++;
			if (framesAlive - lastCycle > 100)
			{
				foreach (var item in GetCollisions())
				{
					if (item is SpritePlayer)
					{
						animate = true;
						currentFrame = framesAlive;
						lastCycle = framesAlive;
					}
				} 
			}
			if (animate && framesAlive - currentFrame < 21)
			{
				Animate();
			}
			else if (framesAlive - currentFrame > 21&&animate)
			{
				animate = false;
			}
			foreach (var item in GetCollisions())
			{
				if (item is SpritePlayer)
				{
					ES.яВамЗапрещаюУмирать = true;
					lastTouch = framesAlive;
				}
			}
			if (framesAlive - lastTouch > 120)
			{
				ES.яВамЗапрещаюУмирать = false;
			}
		}
	}
}
