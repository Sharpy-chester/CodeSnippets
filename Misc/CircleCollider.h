#pragma once
#include <iostream>
#include <vector>
#include "Collider.h"

class CircleCollider : public Collider
{
public:
	CircleCollider(float xpos, float ypos, float width);
	~CircleCollider();
	void isColliding(std::vector<Collider*> cols);
private:
	bool CircleToRectCol(Collider* col);
};