#include "Rect.h"

Rect::Rect(SDL_Renderer* renderer, float width, float height, float posx, float posy)
{
    _width = width;
    _height = height;
    xPosition = posx;
    yPosition = posy;
}

Rect::~Rect()
{
}

void Rect::DrawShape(SDL_Renderer* renderer, float xpos, float ypos, float height, float width)
{
    ChangeColour(renderer, colour_r, colour_g, colour_b, 255);
    SDL_Rect* rect = new SDL_Rect();
    rect->x = xpos;
    rect->y = ypos;
    rect->w = width;
    rect->h = height;
    SDL_RenderFillRect(renderer, rect);
}

void Rect::Update(SDL_Renderer* renderer)
{
    if (collider != NULL)
    {
        if (collider->colliding)
        {
            ChangeColour(renderer, 255, 0, 0, 255);
            colour_r = 255;
            colour_b = 0;
            colour_g = 0;
        }
    }
    DrawShape(renderer, xPosition, yPosition, _width, _height);
}