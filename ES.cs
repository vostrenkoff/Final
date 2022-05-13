using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	internal class ES
	{
		public static ES current;
		public ES()
		{
			current = this;
		}

		public static int currentLevel;
		public static int stars = 0;
		public static bool яВамЗапрещаюУмирать = false;

		public event Action onUpdate;
		public event Action onGameOver;
		public event Action onRestart;
		public event Action onStartGame;
		public event Action onExit;
		public event Action<int> onColorChange;
		public event Action<float> onMove;

		public void Update()
		{
			onUpdate?.Invoke();
		} 
		public void GameOver()
		{
			onGameOver?.Invoke();
		}
		public void Restart()
		{
			onRestart?.Invoke();
		}
		public void StartGame()
		{
			onStartGame?.Invoke();
		}
		public void Exit()
		{
			onExit?.Invoke();
		}
		public void ColorChange(int color)
		{
			onColorChange?.Invoke(color);
		}
		public void Move(float offset)
		{
			onMove?.Invoke(offset);
		}
	}
}
