using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	public class Star : AnimationSprite
	{
		public Star() : base("collect.png",3,2)
		{
			_animationDelay = 20;
			SetCycle(0, 6);
			float scale = 0.1f;
			SetScaleXY(scale);
			ES.current.onUpdate += Update;
		}
		private void Update()
		{
			Animate();
		}
	}
}
