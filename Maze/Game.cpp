#include <SDL.h>
#include <iostream>
#include <conio.h>
#include "Bitmap.h"
#include "Arena.h"
#include "Player.h"
#include "Monster.h"
#include "PatrolEnemy.h"
#include "FollowEnemy.h"
#include "Game.h"

Game::Game(Input* input)
{
	//Start up
	SDL_Init(SDL_INIT_VIDEO);
	m_Window = SDL_CreateWindow(
		"Escape",			//Window Title
		250,				//Initial x position
		50,					//Initial y position
		1000,				//width, in pixels
		600,				//height, in pixels
		0					//window behavior flags
	);

	if (nullptr == m_Window)
	{
		std::cout << "Window initialisation failed: " << SDL_GetError() << std::endl;
		std::cout << "Press a key to continue" << std::endl;
		_getch();
		return;
	}

	m_Renderer = SDL_CreateRenderer(
		m_Window,		//link the renderer to the window
		-1,				//index rendering driver
		0				//Render behavior flags
	);
	if (nullptr == m_Renderer)
	{
		std::cout << "Renderer intitialisation failed: " << SDL_GetError() << std::endl;
		std::cout << "Press a key to continue" << std::endl;
		_getch();
	}

	//object initialisation
	m_Arena = new Arena(m_Renderer);
	m_key = new Key(m_Renderer, 250, 400);
	PatrolEnemy* enemy1 = new PatrolEnemy(m_Renderer, 170, 210, 300);
	FollowEnemy* enemy2 = new FollowEnemy(m_Renderer, 500, 500, 30);
	PatrolEnemy* enemy3 = new PatrolEnemy(m_Renderer, 225, 415, 800);

	//add the newly created monsters to the vector
	monsters.push_back(enemy1);
	monsters.push_back(enemy2);
	monsters.push_back(enemy3);

	m_player = new Player(m_Renderer, input, m_Arena, m_key, monsters, 100, 100);
	enemy2->GetPlayer(m_player);
}

Game::~Game()
{
	if (nullptr != m_Window)
	{
		SDL_DestroyWindow(m_Window);
		m_Window = nullptr;
	}
	if (nullptr != m_Renderer)
	{
		SDL_DestroyRenderer(m_Renderer);
		m_Renderer = nullptr;
	}
	if (nullptr != m_Arena)
	{
		delete m_Arena;
		m_Arena = nullptr;
	}
	if (nullptr != m_player)
	{
		delete m_player;
		m_player = nullptr;
	}
	if (nullptr != m_key)
	{
		delete m_key;
		m_key = nullptr;
	}

	SDL_Quit();
}

//sets the renderers draw colour
void Game::SetDisplayColour(int r, int g, int b)
{
	if (nullptr != m_Renderer)
	{
		int result = SDL_SetRenderDrawColor(
			m_Renderer,		//target renderer
			r,				//red
			g,				//green
			b,				//blue
			255				//alpha
		);
	}
}

void Game::Update()
{
	if (nullptr == m_Renderer)
		return;

	//setting up delta time (time between each frame)
	lastFTime = currentFTime;
	currentFTime = SDL_GetPerformanceCounter();
	deltaTime = (double)(currentFTime - lastFTime) / (double)SDL_GetPerformanceFrequency();
	timeElapsed += deltaTime;

	SDL_RenderClear(m_Renderer);

	m_Arena->Render();
	m_player->Update(deltaTime);

	//update all monsters
	for (int i = 0; i < monsters.size(); i++)
	{
		monsters[i]->Update(deltaTime);
	}

	if (nullptr != m_key && !m_player->hasKey)
	{
		m_key->Update();
	}

	SDL_RenderPresent(m_Renderer);
}

void Game::RenderMap()
{
	if (nullptr == m_Renderer)
		return;

	SDL_RenderClear(m_Renderer);
	m_Arena->Render();

	SDL_RenderPresent(m_Renderer);

}
