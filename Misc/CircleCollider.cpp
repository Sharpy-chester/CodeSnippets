#include "CircleCollider.h"

CircleCollider::CircleCollider(float xposition, float yposition, float radius)
{
	_xpos = xposition;
	_ypos = yposition;
	_width = radius;
	colShape = CollisionShape::Circle;
}

CircleCollider::~CircleCollider()
{
}

void CircleCollider::isColliding(std::vector<Collider*> cols)
{

	for (Collider* col : cols)
	{

		if (col != this)
		{
			if (col->colShape == CollisionShape::Circle)
			{
				float dist = sqrt(
							(col->_xpos - _xpos) * (col->_xpos - _xpos) +
							(col->_ypos - _ypos) * (col->_ypos - _ypos)
				);
				if (dist < _width + col->_width)
				{
					std::cout << "Circle Collision" << std::endl;
					col->setCollision(true, this);
					setCollision(true, col);
					return;
				}
			}
			else if (col->colShape == CollisionShape::Rectangle)
			{
				if (CircleToRectCol(col))
				{
					std::cout << "Circle/Rect Collision" << std::endl;
					col->setCollision(true, this);
					setCollision(true, col);
					return;
				}
			}
		}
	}
	setCollision(false, NULL);
}

bool CircleCollider::CircleToRectCol(Collider* col)
{
	float distx = abs(_xpos - col->_xpos);
	float disty = abs(_ypos - col->_ypos);
	if (distx > col->_width / 2 + _width) return false;
	if (disty > col->_height / 2 + _width) return false;
	if (distx <= col->_width / 2) return true;
	if (disty <= col->_height / 2) return true;
	float distSqX = distx - col->_width / 2;
	distSqX *= distSqX;
	float distSqY = disty - col->_height / 2;
	distSqY *= distSqY;
	float cornerDist = distSqX + distSqY;
	float radSq = _width * _width;
	return (cornerDist <= radSq);
}
