#pragma once
#include <vector>
#include <string>
#include <SDL.h>
#include "Bitmap.h"
using namespace std;

class Arena
{
private:
	vector<string> m_gridLayout;
	int m_blockWidth = 50;
	int m_blockHeight = 50;

	int xpos = 0;
	int ypos = 0;

	SDL_Texture* m_mainBitmap{ nullptr };
	SDL_Texture* m_wallBitmap{ nullptr };
	SDL_Renderer* m_pRenderer{ nullptr };

	Bitmap* m_finishZone{ nullptr };

public:
	Arena(SDL_Renderer* renderer);
	~Arena();
	void Render();
	bool IsWall(float x, float y);
	bool FinishZoneCheck(float x, float y);

};