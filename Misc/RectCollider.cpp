#include "RectCollider.h"
#include <cmath>

RectCollider::RectCollider(float xposition, float yposition, float width, float height)
{
	_xpos = xposition;
	_ypos = yposition;
	_width = width;
	_height = height;
	colShape = CollisionShape::Rectangle;
}

RectCollider::~RectCollider()
{
}

void RectCollider::isColliding(std::vector<Collider*> cols)
{
	for (Collider* col : cols)
	{

		if (col != this)
		{

			if (col->colShape == CollisionShape::Rectangle)
			{
				if (_xpos < col->_xpos + col->_width &&
					_xpos + _width > col->_xpos &&
					_ypos < col->_ypos + col->_height &&
					_ypos + _height > col->_ypos)
				{
					std::cout << "Rect Collision" << std::endl;
					col->setCollision(true, this);
					setCollision(true, col);
					return;
				}
			}
			else if (col->colShape == CollisionShape::Circle)
			{
				if (CircleToRectCol(col))
				{
					std::cout << "Rect/Circle Collision" << std::endl;
					col->setCollision(true, this);
					setCollision(true, col);
					return;
				}
			}
		}
	}
	setCollision(false, NULL);
}

bool RectCollider::CircleToRectCol(Collider* col)
{
	float distx = std::abs(col->_xpos - _xpos);
	float disty = std::abs(col->_ypos - _ypos);
	if (distx > col->_width + _width / 2) return false;
	if (disty > col->_width + _height / 2) return false;
	if (distx <= _width / 2) return true;
	if (disty <= _height / 2) return true;
	float distSqX = distx - _width / 2;
	distSqX *= distSqX;
	float distSqY = disty - _height / 2;
	distSqY *= distSqY;
	float cornerDist = distSqX + distSqY;
	float radSq = col->_width * col->_width;
	return (cornerDist <= radSq);
}
