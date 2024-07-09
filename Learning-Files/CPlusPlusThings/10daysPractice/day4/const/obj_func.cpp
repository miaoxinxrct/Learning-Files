#include <iostream>

using namespace std;

class Point
{
public:
    Point(int x,int y)
    {
        _x=x;
        _y=y;
    };

    void print() const { std::cout<<_x<<" "<<_y<<"\n";}
private:
    int _x;
    int _y;

};

int main()
{
    Point p(5,3);
    p.print();
    return 0;
}