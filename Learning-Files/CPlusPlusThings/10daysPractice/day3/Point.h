#include <iostream>

using namespace std;

class Point
{
public:
    Point(int x,int y);
    Point(const Point& other);
    ~Point();
    int X() const { return _x;}
    int Y() const { return _y;}
    int CountP() const { cout<<"CountP: "<<_CountP<<"\n"; return _CountP; }
private:
    int _x;
    int _y;
    static int _CountP;
};