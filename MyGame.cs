using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.UIelements;

public class MyGame : Game
{

	float _CenterPlatformBoundary = 0;
	float _LeftPlatformBoundary = 0;
	float _RightPlatformBoundary = 0;

	float rad = 25f;

	Canvas _lineContainer = null;

	public static List<Ball> _moversBall;
	public static List<NLineSegment> lines;
	public static List<Player> _moversPlayer;

	public static bool _switch = true;
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
		_lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
	}

	public MyGame() : base(1920, 1080, false, false)
	{
		ES es = new ES();
		UI ui = new UI();
		AddChild(ui);
		_lineContainer = new Canvas(width, height);
		AddChild(_lineContainer);
		
		targetFps = 60;

		float border = 10;

		_CenterPlatformBoundary = 400f;
		_RightPlatformBoundary = 200f;
		_LeftPlatformBoundary = 500f;

		_moversPlayer = new List<Player>();
		_moversBall = new List<Ball>();
		lines = new List<NLineSegment>();

		AddLineSegment(new NLineSegment(border, height, border, 0, 0xffffffff, 2)); // borders
		AddLineSegment(new NLineSegment(width - border, 0, width - border, height, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(0, border, width, border, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(width, height - border, 0, height - border, 0xffffffff, 1));

		//AddLineSegment(new NLineSegment(500, _CenterPlatformBoundary, 300, _CenterPlatformBoundary, 0xffffffff, 1)); //platforms
		AddLineSegment(new NLineSegment(1000, height - 300, 1000, height-border, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(1100, height - 300, 1000, height - 300, 0xffff8000, 3));
		AddLineSegment(new NLineSegment(1000, height - 15, 900, height - 15, 0xffff8000, 3));
		AddLineSegment(new NLineSegment(1100, height - 500, 1100, height - 300, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(1500, height - 500, 1100, height - 500, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(1500, height - 300, 1500, height - 500, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(1600, height - 300, 1500, height - 300, 0xffff2000, 5));	
		AddLineSegment(new NLineSegment(1800, height - border, 1600, height - 300, 0xffffffff, 1));

		GenerateBlock(1129, 451, 25f);
		GenerateBlock(1470, 451, 25f);
		GenerateBlock(1296, 551, 25f);

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



		AddLineSegment(new NLineSegment(300, 1070, 300, 777, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(450, 1070, 450, 777, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(300, 777, 450, 777, 0xffffffff, 1));



		//block tool
		AddLineSegment(new NLineSegment(500 + rad, 500 - rad, 500 - rad, 500 - rad, 0xffff8001, 4));
		AddLineSegment(new NLineSegment(500 - rad, 500 - rad, 500 - rad, 500 + rad, 0xffff8002, 4));
		AddLineSegment(new NLineSegment(500 + rad, 500 + rad, 500 + rad, 500 - rad, 0xffff8003, 4));
		AddLineSegment(new NLineSegment(500 - rad, 500 + rad, 500 + rad, 500 + rad, 0xffff8004, 4));

		//jump tool
		AddLineSegment(new NLineSegment(500 + rad, 500 - rad, 500 - rad, 500 - rad, 0xffff8005, 3));


		LoadScene(1);

	}
	public void GenerateBlock(float x, float y, float rad)
    {
		AddLineSegment(new NLineSegment(x + rad, y - rad, x - rad, y - rad, 0xffffffff, 4));
		AddLineSegment(new NLineSegment(x - rad, y - rad, x - rad, y + rad, 0xffffffff, 4));
		AddLineSegment(new NLineSegment(x + rad, y + rad, x + rad, y - rad, 0xffffffff, 4));
		AddLineSegment(new NLineSegment(x - rad, y + rad, x + rad, y + rad, 0xffffffff, 4));

	}
	public void GenerateJump(float x, float y, float rad)
	{
		AddLineSegment(new NLineSegment(x + rad, y + rad, x - rad, y + rad, 0xffff8000, 3));

	}
	public void AddLineSegment(NLineSegment line)
	{
		AddChild(line);
		lines.Add(line);
	}
	public NLineSegment GetLine(int i) => lines[i];
	public int GetLineCount() => lines.Count;
	void LoadScene(int sceneNumber) {

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

		// create new scene:
		switch (sceneNumber) {
			case 1:
				_moversPlayer.Add(new Player(20, new Vec2(width / 2, height / 2), new Vec2(0, 0)));
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
			ES.current.GameOver();
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
			if (placingTool == 3)
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
		if(placingTool == 2)
        {

        }

		if (Input.GetKeyDown(Key.LEFT_CTRL) && placingTool == 1)
			{
				GenerateBlock(mx, my, 25f);
			Console.WriteLine(mx + " " + my);
			}

		if (Input.GetKeyDown(Key.LEFT_CTRL) && placingTool == 2)
		{
			GenerateJump(mx, my, 25f);
			Console.WriteLine(mx + " " + my);
		}

	}
}