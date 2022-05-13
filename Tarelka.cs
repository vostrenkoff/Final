using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	internal class Tarelka : AnimationSprite
	{
		public Tarelka():base("win.png", 3, 2)
		{
			SetCycle(0, 6);
			_animationDelay = 10;
			ES.current.onUpdate += Update;
		}
		private void Update()
		{
			Animate();
		}
	}
}
