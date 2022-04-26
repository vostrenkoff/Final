using GXPEngine;
using System;

class Gun : Sprite
{
	public Gun(float px, float py) : base("assets/gun.png")
	{

		SetOrigin(width / 2, height / 2);

	}
	
	public Vec2 lookAtVec;
	public float posX, posY, rot;
	public void Aim()
	{

		float dx = Input.mouseX - posX;
		float dy = Input.mouseY - posY;
		
		//float targetAngle =    Mathf.Atan2(dy, dx) * 180 / Mathf.PI - rot;
		lookAtVec = new Vec2(dx, dy);
		lookAtVec.Normalize();
		rotation = lookAtVec.GetAngleDegrees();  //targetAngle;

		
	}
	public void Update()
	{
		Aim();
	}

}
