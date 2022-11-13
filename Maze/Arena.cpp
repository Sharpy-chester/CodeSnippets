#include <SDL.h>
#include <iostream>
#include "Arena.h"

Arena::Arena(SDL_Renderer* renderer) : m_pRenderer{ renderer }
{
	SDL_Surface* mainSurface = SDL_CreateRGBSurface(0, m_blockWidth, m_blockHeight, 32, 0, 0, 0, 0);
	Uint32 mainBlockColour = SDL_MapRGB(mainSurface->format, 0, 0, 255);

	SDL_Surface* wallSurface = SDL_CreateRGBSurface(0, m_blockWidth, m_blockHeight, 32, 0, 0, 0, 0);
	Uint32 wallBlockColour = SDL_MapRGB(mainSurface->format, 255, 0, 205);

	SDL_FillRect(mainSurface, NULL, mainBlockColour);
	SDL_FillRect(wallSurface, NULL, wallBlockColour);

	m_mainBitmap = SDL_CreateTextureFromSurface(m_pRenderer, mainSurface);
	m_wallBitmap = SDL_CreateTextureFromSurface(m_pRenderer, wallSurface);

	m_finishZone = new Bitmap(renderer, "assets/goal.bmp", 0, 0, true);

	SDL_FreeSurface(mainSurface);
	SDL_FreeSurface(wallSurface);

	m_gridLayout.push_back("WWWWWWWWWWWWWWWWWWWW");
	m_gridLayout.push_back("W.........CF.......W");
	m_gridLayout.push_back("W..CCCCCC.CCCCC....W");
	m_gridLayout.push_back("WCCC..C...C....CCC.W");
	m_gridLayout.push_back("W........CCCCC...C.W");
	m_gridLayout.push_back("W..CCCCCC...CCCC.C.W");
	m_gridLayout.push_back("W.........C......C.W");
	m_gridLayout.push_back("W..CCCCCCCCCCCCC.C.W");
	m_gridLayout.push_back("W.CC.............C.W");
	m_gridLayout.push_back("W.CCCCCC.C.CCCCCCC.W");
	m_gridLayout.push_back("W........C.........W");
	m_gridLayout.push_back("WWWWWWWWWWWWWWWWWWWW");
}

Arena::~Arena()
{
	
}

void Arena::Render()
{
	//loops through every character in m_gridLayout and inserts whatever blocks are needed
	for (int i = 0; i < m_gridLayout.size(); i++)
	{
		for (int x = 0; x < m_gridLayout[i].length(); x++)
		{
			//draw block at coords (x, i)
			if (m_gridLayout[i].at(x) == 'W')
			{
				SDL_Rect destRect = { xpos, ypos, m_blockWidth, m_blockHeight };
				SDL_RenderCopy(m_pRenderer, m_mainBitmap, NULL, &destRect);
			}
			else if (m_gridLayout[i].at(x) == 'C')
			{
				SDL_Rect destRect = { xpos, ypos, m_blockWidth, m_blockHeight };
				SDL_RenderCopy(m_pRenderer, m_wallBitmap, NULL, &destRect);
			}
			else if (m_gridLayout[i].at(x) == 'F')
			{
				//makes sure the bitmaps coords are right
				m_finishZone->m_x = xpos;
				m_finishZone->m_y = ypos;
				m_finishZone->draw();
			}
			
			xpos += m_blockWidth;
		}
		ypos += m_blockHeight;
		xpos = 0;
	}
	ypos = 0;
}

//returns true if there is a wall at given coordinates
bool Arena::IsWall(float x, float y)
{
	int xpos = (int)(x / m_blockWidth);
	int ypos = (int)(y / m_blockWidth);
	if (m_gridLayout[ypos].at(xpos) == 'W' || m_gridLayout[ypos].at(xpos) == 'C')
		return true;

	return false;
}

//returns true if the finish zone is at given coordinates
bool Arena::FinishZoneCheck(float x, float y)
{
	int xpos = (int)(x / m_blockWidth);
	int ypos = (int)(y / m_blockWidth);
	if (m_gridLayout[ypos].at(xpos) == 'F')
		return true;

	return false;
}
