using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.UIelements;

namespace GXPEngine
{
	internal class UI : GameObject
	{
		public UI()
		{
			ES.current.onGameOver += ShowGameOverText;
		}
		private void ShowGameOverText()
		{
			Text text = new Text(500, 500, 1920 / 2, 1080 / 2, "GAME OVER", 30);
			Button button = new Button(1920 / 2, 1080 / 2 + 100, "colors.png", "Restart", ES.current.Restart);
			AddChild(text);
			AddChild(button);
		}
	}
}
