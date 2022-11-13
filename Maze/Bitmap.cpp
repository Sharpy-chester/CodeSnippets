#include <string>
#include <iostream>
#include "SDL.h"
#include "SDL_Render.h"
#include "Bitmap.h"

using namespace std;

Bitmap::Bitmap(SDL_Renderer* renderer, std::string filename, int xpos, int ypos, bool useTransparency) : m_x{ xpos }, m_y{ ypos }, m_renderer{ renderer }
{
	m_bmpSurface = SDL_LoadBMP(filename.c_str());
	
	if (nullptr == m_bmpSurface)
	{
		//bmp loading failure
		std::cout << "Surface for bitmap: " << filename << " not loaded" << std::endl << SDL_GetError() << std::endl;
		return;
	}

	//find width and height of the bitmap in pixels
	m_width = m_bmpSurface->w;
	m_height = m_bmpSurface->h;
	if (useTransparency)
	{
		//this will only make it so that black pixels are made transparent
		Uint32 colourKey = SDL_MapRGB(m_bmpSurface->format, 0, 0, 0);
		SDL_SetColorKey(m_bmpSurface, SDL_TRUE, colourKey);
	}

	m_bmpTexture = SDL_CreateTextureFromSurface(renderer, m_bmpSurface);
	if (nullptr == m_bmpTexture)
	{
		std::cout << "Texture for bmp: " << filename << " not created" << std::endl << SDL_GetError() << std::endl;
	}
}

Bitmap::~Bitmap()
{

}

void Bitmap::draw()
{
	if (nullptr == m_bmpTexture)
	{
		return;
	}
	SDL_Rect destRect{ m_x, m_y, m_bmpSurface->w, m_bmpSurface->h };
	SDL_RenderCopy(m_renderer, m_bmpTexture, NULL, &destRect);
}

//returns with width of the bitmap
int Bitmap::width()
{
	return m_width;
}

//returns the height of the bitmap
int Bitmap::height()
{
	return m_height;
}
