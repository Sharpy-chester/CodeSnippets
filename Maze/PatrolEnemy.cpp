#include <SDL.h>
#include "PatrolEnemy.h"

PatrolEnemy::PatrolEnemy(SDL_Renderer* renderer, float xposition, float yposition, float endXPos)
	: m_renderer{ renderer }, endX{ endXPos }
{
	m_sprite = new Bitmap(m_renderer, "assets/sadEnemy.bmp", xpos, ypos, true);
	width = m_sprite->width();
	height = m_sprite->height();
	xpos = xposition;
	ypos = yposition;
	startX = xpos;
	startY = ypos;
}

PatrolEnemy::~PatrolEnemy()
{
	if (nullptr != m_renderer)
	{
		SDL_DestroyRenderer(m_renderer);
		m_renderer = nullptr;
	}
	if (nullptr != m_sprite)
	{
		delete m_sprite;
		m_sprite = nullptr;
	}
}

void PatrolEnemy::Update(double deltaTime)
{
	xpos += moveSpeed * deltaTime;
	//move between startX and endX
	if (xpos > endX || xpos < startX)
	{
		moveSpeed = -moveSpeed;
		xpos += moveSpeed * deltaTime;
	}
	//update bitmap positions
	m_sprite->m_x = xpos;
	m_sprite->m_y = ypos;

	m_sprite->draw();
}
