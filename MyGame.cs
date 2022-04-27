using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

	float _CenterPlatformBoundary = 0;
	float _LeftPlatformBoundary = 0;
	float _RightPlatformBoundary = 0;

	Canvas _lineContainer = null;

	public static List<Ball> _moversBall;
	public static List<NLineSegment> lines;
	public static List<Player> _moversPlayer;
	

	


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

	public MyGame() : base(800, 600, false, false)
	{
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

		AddLineSegment(new NLineSegment(500, _CenterPlatformBoundary, 300, _CenterPlatformBoundary, 0xffffffff, 1)); //platforms
		AddLineSegment(new NLineSegment(300, _LeftPlatformBoundary, 100, _LeftPlatformBoundary, 0xffff0000, 2));
		AddLineSegment(new NLineSegment(700, _RightPlatformBoundary, 500, _RightPlatformBoundary, 0xffffffff, 1));

		AddLineSegment(new NLineSegment(250, 100, 0, 100, 0xffffffff, 1));
		AddLineSegment(new NLineSegment(0, 100, 100, 0, 0xffffffff, 2)); //angled lines
		AddLineSegment(new NLineSegment(700, 0, 800, 100, 0xffffffff, 2));

		LoadScene(1);

		PrintInfo();
	}
	void AddLineSegment(NLineSegment line)
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
	}

	static void Main() {
		new MyGame().Start();
	}
}