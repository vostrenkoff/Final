
using System;
using System.Collections.Generic;
using GXPEngine;


public class Player : EasyDraw
{
	public static bool drawDebugLine = false;
	public static bool wordy = false;
	public static float bounciness = 0.4f;
	public static Vec2 acceleration = new Vec2(0, 0);
	public static Vec2 velocity = new Vec2(0,0);

	public readonly int radius;
	float offset;

	bool canJump = false;
	Gun _gun;

	public float Mass
	{
		get
		{
			return 7 * radius * radius * _density;
		}
	}

	public Vec2 position
	{
		get
		{
			return _position;
		}
	}

	Vec2 _position;
	Vec2 _oldPosition;

	float _red = 1;
	float _green = 1;
	float _blue = 1;

	float _density = 1;

	const float _colorFadeSpeed = 0.025f;

	public Player(int pRadius, Vec2 pPosition, Vec2 pVelocity) : base(pRadius * 2 + 1, pRadius * 2 + 1)
	{
		radius = pRadius;
		_position = pPosition;
		velocity = pVelocity;
		ES.current.onGameOver += Sdohni;
		SetOrigin(radius, radius);
		Draw();
		UpdateScreenPosition();
		_oldPosition = new Vec2(0, 0);
		_gun = new Gun(0, 0);
	}

	public void SetFadeColor(float pRed, float pGreen, float pBlue)
	{
		_red = pRed;
		_green = pGreen;
		_blue = pBlue;
	}

	public void Update()
	{
		_gun.posX = x;
		_gun.posY = y;
		Gizmos.DrawArrow(_position.x, _position.y, velocity.x * 10, velocity.y * 10);
		Shoot();
		HandleInput();
	}
	void Shoot()
	{
		if (Input.GetKeyDown(Key.LEFT_ALT))
		{
			MyGame._switch = !MyGame._switch;
			offset = 40f;
		}
	}
	public void Step()
	{
		_oldPosition = _position;

		Move();

		UpdateScreenPosition();
		ShowDebugInfo();
	}

	void Move()
	{
		if (acceleration.y < 1)
			acceleration.y += 0.08f;
		if (acceleration.y > 1)
			acceleration.y = 1;

		velocity.x += acceleration.x;
		velocity.y += acceleration.y;
		_position += velocity;
		
		CheckBoundaryCollisions();
		UpdateColor();
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
	void CheckBoundaryCollisions()
	{
		MyGame myGame = (MyGame)game;


		foreach (NLineSegment line in MyGame.lines)
		{
			Vec2 lineToPlayer = _position - line.start;
			Vec2 normalLine = (line.end - line.start).Normal();
			float distanceTo = lineToPlayer.Dot(normalLine);
			if (distanceTo < radius)
			{
				if (_position.x > line.end.x &&
					_position.x < line.start.x &&
					_position.y < line.start.y + 20f) // platform check
				{
					Reflect(distanceTo, line);
					canJump = true;
				}
				else if (line.start.y != line.end.y &&
					_position.y > line.start.y &&
					_position.y < line.end.y &&
					_position.x < line.start.x) //border left looking side check
				{
					Reflect(distanceTo, line);
					canJump = false;
					acceleration.x = -bounciness * acceleration.x;
					if (line.lineWidth == 4)
						ES.current.GameOver();
				}
				else if (line.start.y != line.end.y &&
					_position.y < line.start.y &&
					_position.y > line.end.y &&
					_position.x > line.start.x) //border right looking side check
				{
					Reflect(distanceTo, line);
					canJump = false;
					acceleration.x = -bounciness * acceleration.x;
					if (line.lineWidth == 4)
						ES.current.GameOver();
				}
				else if (_position.x < line.end.x &&
					_position.x > line.start.x &&
					_position.y > line.start.y - 20f) // ceiling check
				{
					Reflect(distanceTo, line);
					canJump = true;
					if (line.lineWidth == 4)
						ES.current.GameOver();
				}
				 if (line.lineWidth == 3 && //jump
					_position.x > line.end.x &&
					_position.x < line.start.x &&
					_position.y < line.start.y + 20f)
                {
					//Reflect(distanceTo, line);
					velocity.y = -25.3f;
                }
				 



				//ES.current.GameOver();

			}

			// 1 - normal line
			// 2 - fan
			// 3 - jump
			// 4 - no fall damage
			if (line.lineWidth == 5 &&
					_position.x > line.end.x && _position.x < line.start.x &&
					_position.y < line.start.y + 30f && _position.y < line.end.y + 30f &&
					_position.y > line.start.y - 300f && _position.y > line.end.y - 300f) //wind force left
			{
				velocity.y -= Mass * 0.0003f;
				SetFadeColor(1, 0.2f, 0.2f);
				UpdateScreenPosition();
			}
		}
	}

	private void Sdohni()
    {
		this.Destroy();
    }
	public void Reflect(float distanceto, NLineSegment line)
	{
		Vec2 POI;
		POI = (-distanceto + radius) * line._normalVec;
		_position += POI;
		velocity.ReflectOver(line._normalVec);
		velocity.y *= bounciness;
		velocity.x *= 0.9f;
		UpdateScreenPosition();
	}
	void ShowDebugInfo()
	{
		if (drawDebugLine)
		{
			((MyGame)game).DrawLine(_oldPosition, _position);
		}
	}

	void UpdateScreenPosition()
	{
		x = _position.x;
		y = _position.y;
	}

	void Draw()
	{
		Fill(200);
		NoStroke();
		
		Ellipse(radius, radius, 2 * radius, 2 * radius);
	}

	void HandleInput()
	{
		if (Input.GetKeyDown(Key.UP) && velocity.y > -0.8f && velocity.y < 0.8f &&canJump)
		{
			//acceleration.y = -1.3f;
		}
		
		if (Input.GetKey(Key.RIGHT))
		{
			acceleration.x = 0.8f;
			velocity.x *= 0.95f;

		}
		else //reduce velocity while flying
		{
			acceleration.x *= 0.94f;
			velocity.x *= 0.94f;
		}
		if (Input.GetKey(Key.LEFT))
		{
			acceleration.x = -0.8f;
			velocity.x *= 0.95f;
		}
		else //reduce velocity while flying
		{
			acceleration.x *= 0.94f;
			velocity.x *= 0.94f;
		}

		foreach (NLineSegment line in MyGame.lines)
		{
			if (line.lineWidth == 4)
			{
				if (MyGame._switch)
				{
					line.start.y -= offset;
					line.end.y -= offset;
				}
				else
				{
					line.start.y += offset;
					line.end.y += offset;
				}
			}
		}
		if(offset != 0)
        {
			offset -= 10f;
        }


	}
}
