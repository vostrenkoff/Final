using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}

	public static Vec2 operator +(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x + right.x, left.y + right.y);
	}
	public static Vec2 operator -(Vec2 left, Vec2 right)//subtract
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}
	public static Vec2 operator *(Vec2 left, float right)//scale
	{
		return new Vec2(left.x * right, left.y * right);
	}
	public static Vec2 operator *(float left, Vec2 right)//scale
	{
		return new Vec2(right.x * left, right.y * left);
	}

	public float Length()
	{
		float length = Mathf.Sqrt(x * x + y * y);
		return length;
	}
	
	public void Normalize()
    {
		float len = Length();
		if(len != 0)
        {
			x = x / len;
			y = y / len;
        }
    }
	public Vec2 Normalized()
	{
		float mag = this.Length();
		if (mag != 0)
			return new Vec2(this.x / mag, this.y / mag);
		else
			return this;
	}
	public void SetXY(float px, float py)
    {
		this.x = px;
		this.y = py;
    }

	public float checkDistance(Vec2 vec)
	{
		float vx = vec.x - x;
		float vy = vec.y - y;
		return Mathf.Sqrt(vx * vx + vy * vy);
	}

	public static float Deg2Rad(float degrees)
	{
		return degrees * (Mathf.PI / 180);
	}
	public static float Rad2Deg(float radians)
	{
		return radians * (180 / Mathf.PI);
	}
	
	public static Vec2 GetUnitVectorDeg(float deg)
	{
		float px = Mathf.Cos(Deg2Rad(deg));
		float py = Mathf.Sin(Deg2Rad(deg));
		return new Vec2(px, py);
	}
	public static Vec2 GetUnitVectorRad(float rad)
	{
		float px = Mathf.Cos(rad);
		float py = Mathf.Sin(rad);
		return new Vec2(px, py);
	}
	public void SetAngleDegrees(float deg)
	{
		float length = Length();
		x = Mathf.Cos(Deg2Rad(deg)) * length;
		y = Mathf.Sin(Deg2Rad(deg)) * length;
	}
	public void SetAngleRadians(float deg)
	{
		float length = Length();
		x = Mathf.Cos(deg) * length;
		y = Mathf.Sin(deg) * length;
	}

	public float GetAngleRadians()
	{
		float angle = Mathf.Atan2(y, x);
		return angle;
	}
	public float GetAngleDegrees()
	{
		return Rad2Deg(GetAngleRadians());
	}

	public static Vec2 RandomUnitVector()
	{
		Random random = new Random();
		float angle = random.Next(0, 360);
		float px = Mathf.Cos(Deg2Rad(angle));
		float py = Mathf.Sin(Deg2Rad(angle));
		return new Vec2(px, py);
	}
	public void RotateRadians(float rad)
	{
		float sin = Mathf.Sin(rad);
		float cos = Mathf.Cos(rad);

		float prevX = x;
		float prevY = y;
		x = prevX * cos - prevY * sin;
		y = prevX * sin + prevY * cos;
	}
	public void RotateDegrees(float deg)
	{
		RotateRadians(Deg2Rad(deg));
	}
	public void RotateAroundDegrees(float deg, Vec2 point)
	{
		Vec2 translatedPos = this - point;
		translatedPos.RotateDegrees(deg);
		translatedPos += point;
		this = translatedPos;
	}
	public void RotateAroundRadians(float rad, Vec2 point)
	{
		RotateAroundDegrees(Deg2Rad(rad), point);
	}
	public Vec2 Normal()
    {
        Vec2 normal = this;
		normal.RotateDegrees(90);
		return normal.Normalized();
    }

    public float Dot(Vec2 v2)
    {
		return x * v2.x + y * v2.y;
    }

	public void ReflectOver(Vec2 reflectOver, float bounciness = 1)
    {
		Vec2 reflected = this - (1 + bounciness) * (this.Dot(reflectOver)) * reflectOver;
		this = reflected;
    }

	public override string ToString () 
	{
		return String.Format ("({0},{1})", x, y);
	}
}

