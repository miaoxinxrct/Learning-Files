#include "Point.h"

int Point::_CountP=0;

Point::Point(int x,int y)
{
    _x=x;
    _y=y;
    _CountP++;
}

Point::Point(const Point& other)
{
    _x=other._x;
    _y=other._y;
    _CountP++;
}

Point::~Point()
{

}