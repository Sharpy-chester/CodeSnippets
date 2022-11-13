#include "Circle.h"

Circle::Circle(SDL_Renderer* renderer, float radius, float posx, float posy)
{
    _width = radius;
    xPosition = posx;
    yPosition = posy;
}

Circle::~Circle()
{
}

void Circle::DrawShape(SDL_Renderer* renderer, float xpos, float ypos, float radius)
{
    ChangeColour(renderer, colour_r, colour_g, colour_b, 255);
    int offsetx, offsety, d;
    int status;

    offsetx = 0;
    offsety = radius;
    d = radius - 1;
    status = 0;
    while (offsety >= offsetx) {
        status += SDL_RenderDrawLine(renderer, xpos - offsety, ypos + offsetx,
            xpos + offsety, ypos + offsetx);
        status += SDL_RenderDrawLine(renderer, xpos - offsetx, ypos + offsety,
            xpos + offsetx, ypos + offsety);
        status += SDL_RenderDrawLine(renderer, xpos - offsetx, ypos - offsety,
            xpos + offsetx, ypos - offsety);
        status += SDL_RenderDrawLine(renderer, xpos - offsety, ypos - offsetx,
            xpos + offsety, ypos - offsetx);

        if (status < 0) {
            status = -1;
            break;
        }

        if (d >= 2 * offsetx) {
            d -= 2 * offsetx + 1;
            offsetx += 1;
        }
        else if (d < 2 * (radius - offsety)) {
            d += 2 * offsety - 1;
            offsety -= 1;
        }
        else {
            d += 2 * (offsety - offsetx - 1);
            offsety -= 1;
            offsetx += 1;
        }
    }
}

void Circle::Update(SDL_Renderer* renderer)
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
    DrawShape(renderer, xPosition, yPosition, _width);
}