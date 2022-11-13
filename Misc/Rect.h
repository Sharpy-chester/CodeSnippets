#pragma once
#include "Shape.h"
#include "SDL.h"

class Rect : public Shape
{
public:
	Rect(SDL_Renderer* renderer, float width, float height, float posx, float posy);
	~Rect();
	void Update(SDL_Renderer* renderer);
private:
	void DrawShape(SDL_Renderer* renderer, float xpos, float ypos, float width, float height);
	
	
	
};