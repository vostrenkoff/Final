using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.UIelements;

public class MyGame : Game
{
	

	float rad = 25f;
	float border = 10;


	public static List<Ball> _moversBall;
	public static List<NLineSegment> lines;
	public static List<Player> _moversPlayer;
	public static List<Star> _stars;

	public static bool _switch = false;
	public static int placingTool = 0;
	

	public Player GetMoverPlayer(int index)
	{
		if (index >= 0 && index < _moversPlayer.Count)
		{
			return _moversPlayer[index];
		}
		return null;
	}

	public void DrawLine(Vec2 start, Vec2 end) {
		//_lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
	}

	public MyGame() : base(1920, 1080, false, false)
	{
		ES es = new ES();
		UI ui = new UI();
		AddChild(ui);
		ES.current.onRestart += Restart;
		ES.current.onStartGame += StartGame;
		SpritePlayer sp = new SpritePlayer();
		AddChild(sp);
		targetFps = 60;

		


		_moversPlayer = new List<Player>();
		_moversBall = new List<Ball>();
		lines = new List<NLineSegment>();
		_stars = new List<Star>();


	}
	private void StartGame()
	{
		LoadScene(1);
	}
	public void Level1Lines()
	{

		AddLineSegment(new NLineSegment(border, height, border, 0, 0xffffffff, 2)); // borders
		AddLineSegment(new NLineSegment(width - border, 0, width - border, height, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(0, border, width, border, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(width, height - border, 0, height - border, 0xffffffff, 1));

		//AddLineSegment(new NLineSegment(500, _CenterPlatformBoundary, 300, _CenterPlatformBoundary, 0xffffffff, 1)); //platforms
		AddLineSegment(new NLineSegment(1000, height - 300, 1000, height - border, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(1100, height - 300, 1000, height - 300, 0xffff8000, 3));
		AddLineSegment(new NLineSegment(1000, height - 15, 900, height - 15, 0xffff8000, 3));
		AddLineSegment(new NLineSegment(1100, height - 500, 1100, height - 300, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(1500, height - 500, 1100, height - 500, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(1500, height - 300, 1500, height - 500, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(1600, height - 300, 1500, height - 300, 0xffff2000, 5));
		AddLineSegment(new NLineSegment(1800, height - border, 1600, height - 300, 0xffffffff, 1));

		
		GenerateGreenBlock(1296, 551, 25f);

		// 1 - normal line
		// 2 - 
		// 3 - jump
		// 4 - block
		// 5 - fan


		// 11-yellow
		// 12 - blue
		// 13 - red
		//
		//
		//
		//



		AddLineSegment(new NLineSegment( 300, 777, 300, 1070,0xffffffff, 1));
		AddLineSegment(new NLineSegment(450, 1070, 450, 777, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 450, 777, 300, 777, 0xffffffff, 1));

		//colors
		AddLineSegment(new NLineSegment( 210, 1065, 150, 1065, 0xffff9999, 11));
		AddLineSegment(new NLineSegment( 410, 772, 350, 772, 0xffff8000, 12));
		AddLineSegment(new NLineSegment( 660, 1065, 600, 1065, 0xffff1000, 13));

		//block tool
		AddLineSegment(new NLineSegment(500 + rad, 500 - rad, 500 - rad, 500 - rad, 0xffff8001, 1));
		AddLineSegment(new NLineSegment(500 - rad, 500 - rad, 500 - rad, 500 + rad, 0xffff8002, 1));
		AddLineSegment(new NLineSegment(500 + rad, 500 + rad, 500 + rad, 500 - rad, 0xffff8003, 1));
		AddLineSegment(new NLineSegment(500 - rad, 500 + rad, 500 + rad, 500 + rad, 0xffff8004, 1));

		//jump tool
		AddLineSegment(new NLineSegment( 500 - rad, 500 - rad, 500 + rad, 500 - rad, 0xffff8005, 1));

		//fan tool
		AddLineSegment(new NLineSegment(500 + rad, 500 - rad, 500 - rad, 500 - rad, 0xffff2001, 1));

	}
	public void Level1Stars()
	{
		Star[] stars = new Star[3];
		for (int i = 0; i < stars.Length; i++)
		{
			stars[i] = new Star();
			_stars.Add(stars[i]);
		}
		stars[0].SetXY(230, 1000);
		stars[1].SetXY(352, 680);
		stars[2].SetXY(507, 1000);
	}
	public void Level2Lines()
	{
		AddLineSegment(new NLineSegment(border, height, border, 0, 0xffffffff, 2)); // borders
		AddLineSegment(new NLineSegment(width - border, 0, width - border, height, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(0, border, width, border, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(width, height - border, 0, height - border, 0xffffffff, 1));
		//normal lines
		AddLineSegment(new NLineSegment( 238, 178, 10, 178, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 238, 400, 238, 178, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 338, 400, 238, 400, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 500, 600, 338, 400, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 1038, 600, 500, 600, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 1038, 900, 1038, 600, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 1638, 900, 1038, 900, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 1438, 600, 1438, 900, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 1438, 600, 1500, 600, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 1500, 900, 1500, 600, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 1638, 900, 1638, 600, 0xffffffff, 1));
		AddLineSegment(new NLineSegment( 1638, 600, 1938, 600, 0xffffffff, 1));




		//blocks
		GenerateGreenBlock(1768,564,25f);
		GenerateBrownBlock(700,564,25f);
		GenerateBlueBlock(276,140,25f);


		//colors
		AddLineSegment(new NLineSegment(176, 175, 136, 175, 0xffff8000, 12));
		AddLineSegment(new NLineSegment(600, 600, 650, 600, 0xffff9999, 11));
		AddLineSegment(new NLineSegment(550, 600, 500, 600, 0xffff1000, 13));

	}
	public void GenerateGreenBlock(float x, float y, float rad)
    {
		AddLineSegment(new NLineSegment(x + rad, y - rad, x - rad, y - rad, 0xffffffff, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y - rad, x - rad, y + rad, 0xffffffff, 4), true);
		AddLineSegment(new NLineSegment(x + rad, y + rad, x + rad, y - rad, 0xffffffff, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y + rad, x + rad, y + rad, 0xffffffff, 4), true);

	}
	public void GenerateBrownBlock(float x, float y, float rad)
	{
		AddLineSegment(new NLineSegment(x + rad, y - rad, x - rad, y - rad, 0xffff2000, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y - rad, x - rad, y + rad, 0xffff2000, 4), true);
		AddLineSegment(new NLineSegment(x + rad, y + rad, x + rad, y - rad, 0xffff2000, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y + rad, x + rad, y + rad, 0xffff2000, 4), true);

	}
	public void GenerateBlueBlock(float x, float y, float rad)
	{
		AddLineSegment(new NLineSegment(x + rad, y - rad, x - rad, y - rad, 0xffff3000, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y - rad, x - rad, y + rad, 0xffff3000, 4), true);
		AddLineSegment(new NLineSegment(x + rad, y + rad, x + rad, y - rad, 0xffff3000, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y + rad, x + rad, y + rad, 0xffff3000, 4), true);

	}
	public void GenerateJump(float x, float y, float rad)
	{
		AddLineSegment(new NLineSegment(x + rad, y + rad, x - rad, y + rad, 0xffff8000, 3), true);

	}
	public void GenerateFan(float x, float y, float rad)
	{
		AddLineSegment(new NLineSegment(x + rad, y + rad, x - rad, y + rad, 0xffff2000, 5), true);

	}
	public void AddLineSegment(NLineSegment line)
	{
		lines.Add(line);
	}
	public void AddLineSegment(NLineSegment line, bool spawnNow)
	{
		AddChild(line);
		lines.Add(line);
	}
	public NLineSegment GetLine(int i) => lines[i];
	public int GetLineCount() => lines.Count;
	void LoadScene(int sceneNumber) {

		ES.currentLevel = sceneNumber;

		foreach (Player mover in _moversPlayer)
		{
			mover.Destroy();
		}
		_moversPlayer.Clear();

		foreach (Ball mover in _moversBall)
		{
			mover.Destroy();
		}
		_moversBall.Clear();
		foreach (NLineSegment lineSegment in lines)
		{
			lineSegment.Destroy();
		}
		lines.Clear();
		foreach (Star star in _stars)
		{
			star.Destroy();
		}
		_stars.Clear();

		// create new scene:
		switch (sceneNumber) {
			case 1:
				Level1Lines();
				Level1Stars();
				_moversPlayer.Add(new Player(20, new Vec2(100, 1000), new Vec2(0, 0)));
				break;
			case 2:
				Level2Lines();
				_moversPlayer.Add(new Player(20, new Vec2(100,100), new Vec2(0, 0)));
				break;
		}


		foreach (Player b in _moversPlayer)
		{
			AddChild(b);
		}
		foreach (Ball b in _moversBall)
		{
			AddChild(b);
		}
		foreach (NLineSegment b in lines)
		{
			AddChild(b);
		}
		foreach (Star b in _stars)
		{
			AddChild(b);
		}

	}

	 void StepThroughMovers() {
		
			foreach (Player mover in _moversPlayer)
			{
				mover.Step();
			}
			foreach (Ball mover in _moversBall)
			{
				mover.Step();
			}
	}
	

	void Update () {
		StepThroughMovers();
		PlacingTool();
		ES.current.Update();
		if(Input.GetKeyDown(Key.A))
		{
			LoadScene(2);
			//ES.current.GameOver();
		}
		Console.WriteLine(Input.mouseX + " " + Input.mouseY);
	}

	static void Main() {
		new MyGame().Start();
	}
	void PlacingTool()
    {
		if (Input.GetKeyDown(Key.SPACE))
		{
			placingTool += 1;
			if (placingTool == 4)
				placingTool = 0;
		}
			float mx = Input.mouseX;
			float my = Input.mouseY;
			float rad = 25f;
        if (placingTool == 1) { 
			foreach (NLineSegment line in lines) {
				if (line.color == 0xffff8001) {
					line.start.x = mx + rad;
					line.start.y = my - rad;
					line.end.x = mx - rad;
					line.end.y = my - rad;
				}
				if (line.color == 0xffff8002)
				{
					line.start.x = mx - rad;
					line.start.y = my - rad;
					line.end.x = mx - rad;
					line.end.y = my + rad;
				}
				if (line.color == 0xffff8003)
				{
					line.start.x = mx + rad;
					line.start.y = my + rad;
					line.end.x = mx + rad;
					line.end.y = my - rad;
				}
				if (line.color == 0xffff8004)
				{
					line.start.x = mx - rad;
					line.start.y = my + rad;
					line.end.x = mx + rad;
					line.end.y = my + rad;
				}
			}
		}
		else
		{
			foreach (NLineSegment line in lines)
			{
				if (line.color == 0xffff8001 || line.color == 0xffff8002 || line.color == 0xffff8003 || line.color == 0xffff8004)
				{
					line.start.x = 0;
					line.start.y = 0;
					line.end.x = 0;
					line.end.y = 0;
				}

			}
		}
		if (placingTool == 2)
		{
			foreach (NLineSegment line in lines)
			{
				
				if (line.color == 0xffff8005)
				{
					line.start.x = mx - rad;
					line.start.y = my + rad;
					line.end.x = mx + rad;
					line.end.y = my + rad;
				}
			}
		}
		else
        {
			foreach (NLineSegment line in lines)
			{
				if (line.color == 0xffff8005)
				{
					line.start.x = 0;
					line.start.y = 0;
					line.end.x = 0;
					line.end.y = 0;
				}
				
			}
		}
		if (placingTool == 3)
		{
			foreach (NLineSegment line in lines)
			{

				if (line.color == 0xffff2001)
				{
					line.start.x = mx - rad;
					line.start.y = my + rad;
					line.end.x = mx + rad;
					line.end.y = my + rad;
				}
			}
		}
		else
		{
			foreach (NLineSegment line in lines)
			{
				if (line.color == 0xffff2001)
				{
					line.start.x = 0;
					line.start.y = 0;
					line.end.x = 0;
					line.end.y = 0;
				}

			}
		}

		if (Input.GetKeyDown(Key.LEFT_CTRL) && placingTool == 1 && ES.stars > 0)
			{
			//GenerateBlock(mx, my, 25f);
			//Console.WriteLine(mx + "-" + my);
			ES.stars--;
			}

		if (Input.GetKeyDown(Key.LEFT_CTRL) && placingTool == 2 && ES.stars > 0)
		{
			GenerateJump(mx, my, 25f);
			ES.stars--;
			//Console.WriteLine(mx + "-" + my);
		}
		if (Input.GetKeyDown(Key.LEFT_CTRL) && placingTool == 3 && ES.stars > 0)
		{
			GenerateFan(mx, my, 25f);
			ES.stars--;
			//Console.WriteLine(mx + "-" + my);
		}
	}
	private void Restart()
	{
		ES.stars = 0;
		LoadScene(ES.currentLevel);
	}
}