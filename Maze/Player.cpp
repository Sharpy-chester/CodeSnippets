#include <iostream>
#include <SDL.h>
#include "Player.h"

Player::Player(SDL_Renderer* renderer, Input* input, Arena* arena, Key* key, vector<Monster*> monsters, float xposition, float yposition) 
	: m_renderer{ renderer }, m_input{ input }, m_arena{ arena }, m_key{ key }, m_monsters{ monsters }, xpos{ xposition }, ypos{ yposition }
{
	sprite = new Bitmap(m_renderer, "assets/player.bmp", xpos, ypos, true);
	startX = xpos;
	startY = ypos;
}

Player::~Player()
{

}

void Player::Update(double deltaTime)
{
	Movement(deltaTime);

	sprite->draw();

	Collision();
}

void Player::Movement(double deltaTime)
{
	if (m_input->KeyIsPressed(KEY_D))
	{
		xpos += moveSpeed * (float)deltaTime;
		//this gets both right hand corners and checks if theyre colliding with a wall
		if (m_arena->IsWall(xpos + sprite->width(), ypos) || m_arena->IsWall(xpos + sprite->width(), ypos + sprite->height()))
		{
			xpos -= moveSpeed * (float)deltaTime;
		}
		if (m_arena->FinishZoneCheck(xpos + sprite->width(), ypos) || m_arena->FinishZoneCheck(xpos + sprite->width(), ypos + sprite->height()))
		{
			//if the player has picked up the key
			if (hasKey)
			{
				std::cout << "You Win!" << std::endl;
				SDL_Quit();
			}
		}
	}
	//this gets both left hand corners and checks if theyre colliding with a wall
	if (m_input->KeyIsPressed(KEY_A))
	{
		xpos -= moveSpeed * (float)deltaTime;
		if (m_arena->IsWall(xpos, ypos) || m_arena->IsWall(xpos, ypos + sprite->height()))
		{
			xpos += moveSpeed * (float)deltaTime;
		}
		if (m_arena->FinishZoneCheck(xpos, ypos) || m_arena->FinishZoneCheck(xpos, ypos + sprite->height()))
		{
			if (hasKey)
			{
				std::cout << "You Win!" << std::endl;
				SDL_Quit();
			}
		}
	}
	//this gets both top corners and checks if theyre colliding with a wall
	if (m_input->KeyIsPressed(KEY_W))
	{
		ypos -= moveSpeed * (float)deltaTime;
		if (m_arena->IsWall(xpos, ypos) || m_arena->IsWall(xpos + sprite->width(), ypos))
		{
			ypos += moveSpeed * (float)deltaTime;
		}
		if (m_arena->FinishZoneCheck(xpos, ypos) || m_arena->FinishZoneCheck(xpos + sprite->width(), ypos))
		{
			if (hasKey)
			{
				std::cout << "You Win!" << std::endl;
				SDL_Quit();
			}
		}
	}
	//this gets both bottom corners and checks if theyre colliding with a wall
	if (m_input->KeyIsPressed(KEY_S))
	{
		ypos += moveSpeed * (float)deltaTime;
		if (m_arena->IsWall(xpos, ypos + sprite->height()) || m_arena->IsWall(xpos + sprite->width(), ypos + sprite->height()))
		{
			ypos -= moveSpeed * (float)deltaTime;
		}
		if (m_arena->FinishZoneCheck(xpos, ypos + sprite->height()) || m_arena->FinishZoneCheck(xpos + sprite->width(), ypos + sprite->height()))
		{
			if (hasKey)
			{
				std::cout << "You Win!" << std::endl;
				SDL_Quit();
			}
		}
	}
	//this updates the bitmaps coordinates so that it draws to the right place
	sprite->m_x = xpos;
	sprite->m_y = ypos;
}

void Player::Collision()
{
	if (nullptr != m_key)
	{
		//AABB collision check
		if (xpos < m_key->xpos + m_key->width &&
			xpos + sprite->width() > m_key->xpos &&
			ypos < m_key->ypos + m_key->height &&
			ypos + sprite->height() > m_key->ypos)
		{
			delete m_key;
			m_key = nullptr;
			hasKey = true;
		}
	}
	for (int i = 0; i < m_monsters.size(); i++)
	{
		if (xpos < m_monsters[i]->xpos + m_monsters[i]->width &&
			xpos + sprite->width() > m_monsters[i]->xpos &&
			ypos < m_monsters[i]->ypos + m_monsters[i]->height &&
			ypos + sprite->height() > m_monsters[i]->ypos)
		{
			//if the player hits a monster, reset the players and monsters position
			xpos = startX;
			ypos = startY;
			m_monsters[i]->xpos = m_monsters[i]->startX;
			m_monsters[i]->ypos = m_monsters[i]->startY;
		}
	}
}
