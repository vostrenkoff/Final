using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	internal class SpritePlayer : Sprite
	{
		public SpritePlayer() : base("playertemp.png")
		{
			ES.current.onUpdate += Update;
			SetOrigin(width / 2, height / 2);
			SetScaleXY(0.5f, 0.5f);
		}
		void Update()
		{
			Vec2 position = Player.globalPos;
			SetXY(position.x, position.y);
			foreach (var item in GetCollisions())
			{
				if (item is Star)
				{
					ES.stars++;
					item.Destroy();
				}
			}
		}
	}
}
