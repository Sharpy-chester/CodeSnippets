#pragma once
#include <SDL.h>
#include <vector>
#include "Bitmap.h"
#include "Arena.h"
#include "Key.h"
#include "Input.h"
#include "Monster.h"

class Player
{
public:
	Player(SDL_Renderer* renderer, Input* input, Arena* arena, Key* key, vector<Monster*> monsters, float xposition, float yposition);
	~Player();
	void Update(double deltaTime);
	float moveSpeed = 150.0f;
	float xpos, ypos, startX, startY;
	bool hasKey = false;

private:
	Bitmap* sprite{ nullptr };
	Arena* m_arena{ nullptr };
	Key* m_key{ nullptr };
	SDL_Renderer* m_renderer{ nullptr };
	Input* m_input{ nullptr };
	vector<Monster*> m_monsters{ nullptr };
	void Movement(double deltaTime);
	void Collision();
	
};