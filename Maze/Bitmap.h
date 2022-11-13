#pragma once
#include <string>

//forward declarations
struct SDL_Surface;
struct SDL_Texture;
struct SDL_Renderer;

class Bitmap
{
private:
	SDL_Surface* m_bmpSurface{ nullptr };
	SDL_Texture* m_bmpTexture{ nullptr };
	SDL_Renderer* m_renderer{ nullptr };
	int m_width = 0, m_height = 0;

public:
	Bitmap(SDL_Renderer* renderer, std::string filename, int xpos, int ypos, bool useTransparency = false);
	~Bitmap();

	void draw();
	int m_x{ 0 }, m_y{ 0 };
	int width();
	int height();
};