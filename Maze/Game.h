#pragma once
#include <vector>

struct SDL_Window;
struct SDL_Renderer;
class Bitmap;
class Monster;
class Arena;
class Player;
class Input;
class Key;

class Game 
{
private: 
	SDL_Window* m_Window{ nullptr };
	SDL_Renderer* m_Renderer{ nullptr };
	bool m_isRunning{ false };

	std::vector<Monster*> monsters;

	Arena* m_Arena{ nullptr };
	Player* m_player{ nullptr };
	Key* m_key{ nullptr };

	Uint64 currentFTime = SDL_GetPerformanceCounter();
	Uint64 lastFTime = 0;
	double deltaTime = 0;
	double timeElapsed = 0;

public:
	Game(Input* input);
	~Game();
	void SetDisplayColour(int r, int g, int b);
	void Update();
	void RenderMap();
};