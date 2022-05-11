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
		Text gameOverText;
		Button gameOverButton;

		Text starText;
		public UI()
		{
			ES.current.onGameOver += ShowGameOverMenu;
			ES.current.onRestart += HideGameOverMenu;
			ES.current.onUpdate += Update;
			starText = new Text(500, 500, 1700, 300, "", 30);
			AddChild(starText);
		}
		private void ShowGameOverMenu()
		{
			gameOverText = new Text(500, 500, 1920 / 2, 1080 / 2, "GAME OVER", 30);
			gameOverButton = new Button(1920 / 2, 1080 / 2 + 100, "colors.png", "Restart", ES.current.Restart);
			AddChild(gameOverText);
			AddChild(gameOverButton);
		}
		private void HideGameOverMenu()
		{
			Console.WriteLine(GetChildCount());
			Console.WriteLine("ddd");
			RemoveChild(gameOverButton);
			RemoveChild(gameOverText);
			gameOverButton.Destroy();
			gameOverText.Destroy();
		}
		private void Update()
		{
			starText.UpdateText("Stars : " + ES.stars);
		}
	}
}
