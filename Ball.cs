using System;
using GXPEngine;

public class Ball : EasyDraw
{
	public int radius
	{
		get
		{
			return _radius;
		}
	}
	public Vec2 acceleration;
	public Vec2 velocity;
	public Vec2 position;
	public Vec2 _oldPosition;

	public bool IsDrawingLine;

	int _radius;

	float _density = 1f;

	float _red = 1;
	float _green = 1;
	float _blue = 1;

	const float _colorFadeSpeed = 0.025f;
	public Ball(int pRadius, Vec2 pPosition, Vec2 pVelocity) : base(pRadius * 2 + 1, pRadius * 2 + 1)
	{
		_radius = pRadius;
		position = pPosition;
		velocity = pVelocity;
		acceleration = new Vec2(0, 1);
		UpdateScreenPosition();
		SetOrigin(_radius, _radius);

		Draw(255, 255, 255);
	}

	public void SetFadeColor(float pRed, float pGreen, float pBlue)
	{
		_red = pRed;
		_green = pGreen;
		_blue = pBlue;
	}
	void UpdateColor()
	{
		if (_red < 1)
		{
			_red = Mathf.Min(1, _red + _colorFadeSpeed);
		}
		if (_green < 1)
		{
			_green = Mathf.Min(1, _green + _colorFadeSpeed);
		}
		if (_blue < 1)
		{
			_blue = Mathf.Min(1, _blue + _colorFadeSpeed);
		}
		SetColor(_red, _green, _blue);
	}
	void Draw(byte red, byte green, byte blue)
	{
		Fill(red, green, blue);
		Stroke(red, green, blue);
		Ellipse(_radius, _radius, 2 * _radius, 2 * _radius);
	}
	public float Mass
	{
		get
		{
			return 7 * radius * radius * _density;
		}
	}

	public void UpdateScreenPosition()
	{
		x = position.x;
		y = position.y;
	}
	void Update()
	{
		foreach (NLineSegment line in MyGame.lines)
		{
				Vec2 lineToBall = position - line.start;
				Vec2 normalLine = (line.end - line.start).Normal();
				float distanceTo = lineToBall.Dot(normalLine);
				if (distanceTo < radius)
				{
					if (x > line.end.x && x < line.start.x && y < line.start.y + 30f)
					{
						Reflect(distanceTo, line);
					}
					else if (line.start.y != line.end.y || line.start.x < line.end.x)
					{
						Reflect(distanceTo, line);
					}
				}
				if (line.lineWidth == 2 &&
					x > line.end.x && x < line.start.x &&
					y < line.start.y + 30f && y < line.end.y + 30f &&
					y > line.start.y - 300f && y > line.end.y - 300f) //wind force left
				{
					velocity.y -= Mass * 0.003f;
					SetFadeColor(1, 0.2f, 0.2f);
					UpdateScreenPosition();
				}
		}
		
	}
	public void Reflect(float distanceto, NLineSegment line)
	{
		Vec2 POI;
		POI = (-distanceto + radius) * line._normalVec;
		position += POI;
		velocity.ReflectOver(line._normalVec);
		velocity *= 0.98f;
		UpdateScreenPosition();
	}
	public void Step()
	{
		_oldPosition = position;
		velocity += acceleration;
		position += velocity;
		UpdateColor();
		if (IsDrawingLine) ((MyGame)game).DrawLine(_oldPosition, position);
		UpdateScreenPosition();
	}
}
