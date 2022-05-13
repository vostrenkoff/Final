using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	internal class Fan : AnimationSprite
	{
		public Fan() : base("tr.png",3,2)
		{
			float scale = 0.5f;
			SetScaleXY(scale);
			_animationDelay = 10;
			ES.current.onUpdate += Update;
			SetCycle(0, 5);
		}
		private void Update()
		{
			Animate();
		}
	}
}
