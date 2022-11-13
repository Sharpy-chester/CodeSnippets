#pragma once
#include <vector>
#include <iostream>
#include "Shape.h"
#include "SDL.h"
#include "CollisionController.h"

class Circle : public Shape
{
public:
	Circle(SDL_Renderer* renderer, float radius, float posx, float posy);
	~Circle();
	void Update(SDL_Renderer* renderer);
private:
	void DrawShape(SDL_Renderer* renderer, float xpos, float ypos, float radius);
	
};