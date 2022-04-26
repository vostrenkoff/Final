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

	/*********************************** TESTS ********************************************/



	void PrintInfo() {
		Console.WriteLine("Hold spacebar to slow down the frame rate.");
		Console.WriteLine("Use arrow keys for movement.");
		Console.WriteLine("Press ALT to shoot.");

		Vec2 testVector = new Vec2(2, 5);
		Vec2 testVector2 = new Vec2(3, -1);

		Vec2 lengthcheck = new Vec2(10, 0);
		float length = lengthcheck.Length();
		Console.WriteLine("Length ok ?: " +
			(length == 10));

		Vec2 res = testVector * 2;
		Console.WriteLine("Scalar multiplication right ok ?: " +
			(res.x == 4 && res.y == 10 && testVector.x == 2 && testVector.y == 5));

		res = testVector + testVector2;
		Console.WriteLine(" + operator ok?: " +
			(res.x == 5 && res.y == 4 && testVector.x == 2 && testVector.y == 5));

		res = testVector - testVector2;
		Console.WriteLine(" - operator ok?: " +
			(res.x == -1 && res.y == 6 && testVector.x == 2 && testVector.y == 5));

		Vec2 norm = testVector.Normalized();
		float roundedX = (float)Mathf.Round(norm.x * 100) / 100;
		float roundedY = (float)Mathf.Round(norm.y * 100) / 100;

		Console.WriteLine("normalized ok?: " +
			(roundedX == 0.37f && roundedY == 0.93f));

		float rad = Vec2.Deg2Rad(180);
		rad = (float)Mathf.Round(rad * 100) / 100;
		Console.WriteLine("deg2rad ok?: " +
			(rad == 3.14f));

		float deg = Vec2.Rad2Deg(0.50f * Mathf.PI);
		Console.WriteLine("rad2deg ok?: " +
			(deg == 90));

		Vec2 UnitDeg = Vec2.GetUnitVectorDeg(60);
		float px = (float)(Mathf.Round(UnitDeg.x * 100)) / 100;
		float py = (float)(Mathf.Round(UnitDeg.y * 100)) / 100;
		Console.WriteLine("GetUnitVectorDeg is ok?: " +
			(px == 0.5f && py == 0.87f));

		Vec2 UnitRad = Vec2.GetUnitVectorRad(Vec2.Deg2Rad(60));
		Console.WriteLine("GetUnitVectorRad ok?:" +
			(RoundTo2(UnitRad.x) == 0.5f && RoundTo2(UnitRad.y) == 0.87f));

		Vec2 setDegree = new Vec2(3, 4);
		setDegree.SetAngleDegrees(45);
		Console.WriteLine("SetAngleDegrees is ok?:" +
			(RoundTo2(setDegree.x) == 3.54f && RoundTo2(setDegree.y) == 3.54f && setDegree.Length() == 5));

		Vec2 setRad = new Vec2(3, 4);
		setRad.SetAngleRadians(Vec2.Deg2Rad(45));
		Console.WriteLine("SetAngleRadians ok?:" +
				(RoundTo2(setRad.x) == 3.54f && RoundTo2(setRad.y) == 3.54f && setRad.Length() == 5));

		float angleRad = testVector.GetAngleRadians();
		Console.WriteLine("GetAngleRadians ok?: " +
			 (RoundTo2(angleRad) == 1.19f));

		float angleDeg = testVector.GetAngleDegrees();
		Console.WriteLine("GetAngleDegrees ok?: " +
			(RoundTo2(angleDeg) == 68.2f));

		Vec2 rotateTest = new Vec2(1, 2);
		rotateTest.RotateDegrees(90);
		Console.WriteLine("RotateDegrees ok?:" +
			(RoundTo2(rotateTest.x) == -2 && RoundTo2(rotateTest.y) == 1));

		rotateTest = new Vec2(2, 1);
		rotateTest.RotateRadians(0.5f);
		Console.WriteLine("RotateRadians ok?: " +
			(RoundTo2(rotateTest.x) == 1.28f && RoundTo2(rotateTest.y) == 1.84f));

		rotateTest = new Vec2(1, 0);
		rotateTest.RotateAroundDegrees(90, new Vec2(0, 1));
		Console.WriteLine("RotateAroundDegrees ok?: " +
			(RoundTo2(rotateTest.x) == 1 && RoundTo2(rotateTest.y) == 2));

		Vec2 Test = new Vec2(8, 6);
		Vec2 dotTest = new Vec2(-6, 8);
		float ress = Test.Dot(dotTest);
		Console.WriteLine("Dot ok?: " + 
			(ress == 0));
		
		Vec2 normTest1 = new Vec2(4, 3);
		normTest1 = normTest1.Normal();
		Console.WriteLine("Normal ok?: " + 
			(RoundTo2(normTest1.x) == -0.6f && RoundTo2(normTest1.y) == 0.8f));

		Vec2 refTest = new Vec2(0, -5);
		Vec2 refTest2 = new Vec2(10, 0);
		refTest.ReflectOver(Test,1);
		Console.WriteLine("Reflect ok?: " + 
			(refTest.x == 480 && refTest.y == 355));

		Vec2 distanceTest = new Vec2(4, 3);
		Vec2 distanceTest2 = new Vec2(-4, -3);
		float result = distanceTest.checkDistance(distanceTest2);

		Console.WriteLine("Check Distance ok?: " +
			(result == 10f));
	}
	static float RoundTo2(float value) => (float)(Mathf.Round(value * 100)) / 100;


	

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