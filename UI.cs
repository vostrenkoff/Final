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

		Text menuTitle;
		Button menuStart;
		Button menuExit;
		bool showStarCount = false;
		Text starText;
		public UI()
		{
			ES.current.onGameOver += ShowGameOverMenu;
			ES.current.onRestart += HideGameOverMenu;
			ES.current.onUpdate += Update;
			ES.current.onStartGame += HideMenu;
			starText = new Text(500, 500, 1700, 100, "", 30);
			AddChild(starText);
			ShowMenu();
		}
		private void ShowGameOverMenu()
		{
			if (GetChildCount() == 1)
			{
				gameOverText = new Text(500, 500, 1920 / 2, 1080 / 2, "GAME OVER", 30);
				gameOverButton = new Button(1920 / 2, 1080 / 2 + 100, "movingBlock.png", "Restart", ES.current.Restart);
				AddChild(gameOverText);
				AddChild(gameOverButton); 
			}
		}
		private void HideGameOverMenu()
		{
			RemoveChild(gameOverButton);
			RemoveChild(gameOverText);
			gameOverButton.Destroy();
			gameOverText.Destroy();
		}
		private void ShowMenu()
		{
			menuTitle = new Text(500, 500, 1920 / 2, 280, "Ballls game", 60);
			menuStart = new Button(1920 / 2, 500, "movingBlock.png", "Start", ES.current.StartGame);
			menuExit = new Button(1920 / 2, 800, "movingBlock.png", "Exit", ES.current.Exit);
			AddChild(menuTitle);
			AddChild(menuStart);
			AddChild(menuExit);
		}
		private void HideMenu()
		{
			RemoveChild(menuTitle);
			RemoveChild(menuStart);
			RemoveChild(menuExit);
			menuTitle.Destroy();
			menuStart.Destroy();
			menuExit.Destroy();
			showStarCount = true;
		}
		private void Update()
		{
			if (showStarCount)
			{
				starText.UpdateText("Stars : " + ES.stars);
			}
			else
			{
				starText.Clear(0,0 , 0, 0);
			}
		}
	}
}
