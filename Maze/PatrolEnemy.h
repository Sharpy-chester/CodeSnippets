#pragma once
#include "Monster.h"

class PatrolEnemy : public Monster
{
public:
	PatrolEnemy(SDL_Renderer* renderer, float xposition, float yposition, float endXPos);
	~PatrolEnemy();
	void Update(double deltaTime);


private:
	Bitmap* m_sprite{ nullptr };
	SDL_Renderer* m_renderer{ nullptr };
	float moveSpeed = 75.0f;
	float endX = 0;
};