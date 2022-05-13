using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.UIelements;

public class MyGame : Game
{
	

	float rad = 25f;
	float border = 10;
	Sound placingsnd;

	public static List<Ball> _moversBall;
	public static List<NLineSegment> lines;
	public static List<Player> _moversPlayer;
	public static List<Star> _stars;
	public static List<Sprite> _objects;
	public static List<Sprite> blocks;

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
	Sprite hh;
	public MyGame() : base(1920, 1080, false, false)
	{
		placingsnd = new Sound("placing.wav", false, false);
		ES es = new ES();
		UI ui = new UI();
		AddChild(ui);
		ES.current.onRestart += Restart;
		ES.current.onStartGame += StartGame;
		SpritePlayer sp = new SpritePlayer();
		AddChild(sp);
		targetFps = 60;

		hh = new Sprite("menu.png");
		AddChildAt(hh,-1);


		_moversPlayer = new List<Player>();
		_moversBall = new List<Ball>();
		lines = new List<NLineSegment>();
		_stars = new List<Star>();
		_objects = new List<Sprite>();
		blocks = new List<Sprite>();


	}
	private void StartGame()
	{
		LoadScene(1);
		hh.Destroy();
	}
	public void Level1Lines()
	{

		AddLineSegment(new NLineSegment(border, height, border, 0, 0x00000000, 2)); // borders
		AddLineSegment(new NLineSegment(width - border, 0, width - border, height, 0x00000000, 1));
		AddLineSegment(new NLineSegment(0, border, width, border, 0x00000000, 1));
		AddLineSegment(new NLineSegment(width, 963, 0, 963, 0x00000000, 1));

		//AddLineSegment(new NLineSegment(500, _CenterPlatformBoundary, 300, _CenterPlatformBoundary, 0xffffffff, 1)); //platforms
		//AddLineSegment(new NLineSegment(1000, height - 300, 1000, height - border, 0xffffffff, 1));
		//AddLineSegment(new NLineSegment(1100, height - 300, 1000, height - 300, 0xffff8000, 3));
		//AddLineSegment(new NLineSegment(1000, height - 15, 900, height - 15, 0xffff8000, 3));

		AddLineSegment(new NLineSegment(1459, 611, 1459, 962, 0x00000000, 1));
		AddLineSegment(new NLineSegment(1660, 611, 1459, 611, 0x00000000, 1));
		//AddLineSegment(new NLineSegment(1500, height - 300, 1500, height - 500, 0xffffffff, 1));
		//AddLineSegment(new NLineSegment(1600, height - 300, 1500, height - 300, 0xffff2000, 5));
		AddLineSegment(new NLineSegment (1882, 780, 1660, 611, 0x00000000, 1));

		
		GenerateGreenBlock(1550, 570, 25f);

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



		AddLineSegment(new NLineSegment( 686, 630, 686, 963, 0x00000000, 1));
		AddLineSegment(new NLineSegment(890, 963, 890, 630, 0x00000000, 1));
		AddLineSegment(new NLineSegment(  890, 630, 686, 630, 0x00000000, 1));

		//colors
		AddLineSegment(new NLineSegment( 240, 960, 180, 960, 0x00000000, 11));
		Paint cyan = new Paint(0x00ffff);
		cyan.SetXY(240-60, 960-42);
		_objects.Add(cyan);
		AddLineSegment(new NLineSegment( 790, 625, 730, 625, 0x00000000, 12));
		Paint magenta = new Paint(0xff00ff);
		magenta.SetXY(790 - 60, 625 - 42);
		_objects.Add(magenta);
		AddLineSegment(new NLineSegment( 660, 960, 600, 960, 0x00000000, 13));
		Paint yellow = new Paint(0xffff00);
		yellow.SetXY(660 - 60, 960 - 42);
		_objects.Add(yellow);

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
		stars[0].SetXY(592, 894);
		stars[1].SetXY(1042, 894);
		stars[2].SetXY(787, 551);
	}
	public void Level1Bg()
	{
		Sprite bg1 = new Sprite("bg1.jpg");
		AddChildAt(bg1, 0);
		Sprite bg2 = new Sprite("bg2.png");
		bg2.SetXY(0, 100);
		AddChildAt(bg2, 1);
	}
	public void Level2Lines()
	{
		AddLineSegment(new NLineSegment(border, height, border, 0, 0xffffffff, 2)); // borders
		AddLineSegment(new NLineSegment(width - border, 0, width - border, height, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(0, border, width, border, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(width, 963, 0, 963, 0x0000000, 1));
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
		//cyan + yellow
		Block block = new Block(0x80ff80);
		block.SetXY(x-35, y-35);
		blocks.Add(block);
		block.UpdatePos();
		AddLineSegment(new NLineSegment(x + rad, y - rad, x - rad, y - rad, 0xffffffff, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y - rad, x - rad, y + rad, 0xffffffff, 4), true);
		AddLineSegment(new NLineSegment(x + rad, y + rad, x + rad, y - rad, 0xffffffff, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y + rad, x + rad, y + rad, 0xffffffff, 4), true);

	}
	public void GenerateBrownBlock(float x, float y, float rad)
	{
		//magenta + yellow
		AddLineSegment(new NLineSegment(x + rad, y - rad, x - rad, y - rad, 0xffff2000, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y - rad, x - rad, y + rad, 0xffff2000, 4), true);
		AddLineSegment(new NLineSegment(x + rad, y + rad, x + rad, y - rad, 0xffff2000, 4), true);
		AddLineSegment(new NLineSegment(x - rad, y + rad, x + rad, y + rad, 0xffff2000, 4), true);

	}
	public void GenerateBlueBlock(float x, float y, float rad)
	{
		//magenta + cyan
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
	public void AddLineSegment(NLineSegment line, bool spawnNow, out NLineSegment oLine)
	{
		oLine = line;
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
		foreach (var item in _objects)
		{
			item.Destroy();
		}
		_objects.Clear();
		foreach (var item in blocks)
		{
			item.Destroy();
		}
		blocks.Clear();

		// create new scene:
		switch (sceneNumber) {
			case 1:
				Level1Lines();
				Level1Stars();
				Level1Bg();
				_moversPlayer.Add(new Player(20, new Vec2(100, 900), new Vec2(0, 0)));
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
		foreach (var item in blocks)
		{
			AddChild(item);
		}
		foreach (var item in _objects)
		{
			AddChild(item);
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
	Trampoline floatingTrampoline;
	Fan floatingFan;
	Block floatingBlock;
	void PlacingTool()
    {
		if (Input.GetKeyDown(Key.SPACE))
		{
			placingTool += 1;
			if (placingTool == 4)
				placingTool = 0;
			if (placingTool == 2)
			{
				floatingBlock.Destroy();
				floatingTrampoline = new Trampoline();
				AddChild(floatingTrampoline);
			}
			else if (placingTool == 3)
			{
				floatingTrampoline.Destroy();
				floatingFan = new Fan();
				AddChild(floatingFan);
			}
			else if (placingTool == 0)
			{
				floatingFan.Destroy();
			}
			else if(placingTool == 1)
			{
				floatingBlock = new Block(0x80ff80, true);
				AddChild(floatingBlock);
			}	
		}
			float mx = Input.mouseX;
			float my = Input.mouseY;
			float rad = 25f;
        if (placingTool == 1)
		{
			floatingBlock.SetXY(mx - 35, my - 30);
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
			floatingTrampoline.SetXY(mx - 35, my-30);
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
			floatingFan.SetXY(mx - 35, my - 30);
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

		if (Input.GetMouseButtonDown(0) && placingTool == 1 && ES.stars > 0)
			{
			placingsnd.Play();
			//GenerateBlock(mx, my, 25f);
			//Console.WriteLine(mx + "-" + my);
			ES.stars--;
			Block block = new Block(0x80ff80, true);
			AddChild(block);
			_objects.Add(block);
			}

		if (Input.GetMouseButtonDown(0) && placingTool == 2 && ES.stars > 0)
		{
			placingsnd.Play();
			GenerateJump(mx, my, 25f);
			ES.stars--;
			Trampoline tramp = new Trampoline();
			tramp.SetXY(mx - 35, my-30);
			AddChild(tramp);
			_objects.Add(tramp);
			//Console.WriteLine(mx + "-" + my);
		}
		if (Input.GetMouseButtonDown(0) && placingTool == 3 && ES.stars > 0)
		{
			placingsnd.Play();
			GenerateFan(mx, my, 25f);
			ES.stars--;
			Fan fan = new Fan();
			fan.SetXY(mx - 35, my - 30);
			AddChild(fan);
			_objects.Add(fan);
			//Console.WriteLine(mx + "-" + my);
		}
	}
	private void Restart()
	{
		ES.stars = 0;
		LoadScene(ES.currentLevel);
	}
}