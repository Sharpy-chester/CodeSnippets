#pragma once
#include <iostream>
#include <vector>
#include "Collider.h"

class RectCollider : public Collider
{
public:
	RectCollider(float xposition, float yposition, float width, float height);
	~RectCollider();
	void isColliding(std::vector<Collider*> cols);
private:
	bool CircleToRectCol(Collider* col);
};