﻿using System;
using GXPEngine.Core;
using GXPEngine.OpenGL;

namespace GXPEngine
{
	public class NLineSegment : LineSegment
	{
		private Arrow _normal;
		public bool isFan = false;
		public Vec2 _normalVec
		{
			get { return _normal.vector; }
		}
		public Vec2 normalLine { 
			get 
				{ 
				return (end - start).Normal(); 
				} 
			}
		public NLineSegment(float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1, bool isFan = false)
			: this(new Vec2(pStartX, pStartY), new Vec2(pEndX, pEndY), pColor, pLineWidth)
		{
		}

		public NLineSegment(Vec2 pStart, Vec2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1)
			: base(pStart, pEnd, pColor, pLineWidth)
		{
			_normal = new Arrow(new Vec2(0, 0), new Vec2(0, 0), 40, 0xffff0000, 1);
			AddChild(_normal);
		}

		
		override protected void RenderSelf(GLContext glContext)
		{
			if (game != null)
			{
				recalculateArrowPosition();
				Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
			}
		}

		private void recalculateArrowPosition()
		{
			_normal.startPoint = (start + end) * 0.5f;
			_normal.vector = (end - start).Normal();
		}

	}
}

