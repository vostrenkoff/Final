using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	internal class SpritePlayer : AnimationSprite
	{
		int totalChanges = 0;
		int lastColor = 0;
		int R = 0;
		int G = 0;
		int B = 0;
		Sprite inside;
		public static int[] pColor = new int[3];
		public SpritePlayer() : base("player.png",1,1)
		{
			ES.current.onUpdate += Update;
			ES.current.onRestart += ResetColor;
			ES.current.onColorChange += ColorChange;
			SetOrigin(width / 2, height / 2);
			float scale = 0.05f;
			SetScaleXY(scale, scale);
			inside = new Sprite("player_ins.png");
			inside.SetOrigin(inside.width / 2, inside.height / 2);
			//inside.SetScaleXY(0.1f, 0.1f);
			AddChild(inside);
			pColor[0] = 0;
			pColor[1] = 0;
			pColor[2] = 0;
		}
		
		void ColorChange(int color)
		{
			if (lastColor != color)
			{
				lastColor = color;
			}
			else
			{
				return;
			}

			totalChanges++;
			if (color == 1)
			{
				pColor[0] = 1;
				G += 255;
				B += 255;
			}
			else if (color == 2)
			{
				pColor[1] = 1;
				R += 255;
				B += 255;
			}
			else if(color == 3)
			{
				pColor[2] = 1;
				R += 255;
				G += 255;
			}
			int r = R / totalChanges;
			int g = G / totalChanges;
			int b = B / totalChanges;
			string _r = r != 0 ? r.ToString("X").ToLower() : "00";
			string _g = g != 0 ? g.ToString("X").ToLower() : "00";
			string _b = b != 0 ? b.ToString("X").ToLower() : "00";
			string fc = _r + _g + _b;
			Console.WriteLine(pColor);
			this.color = UInt32.Parse(fc, System.Globalization.NumberStyles.AllowHexSpecifier);
		}
		void ResetColor()
		{
			totalChanges = 0;
			pColor[0] = 0;
			pColor[1] = 0;
			pColor[2] = 0;
			R = 0;
			G = 0;
			B = 0;
			color = 0xffffff;
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
