#include <cmath>
#include <iostream>

using namespace std;

class Point
{
public:
    Point(int x=0,int y=0):_x(x),_y(y){}
    ~Point(){}
    int X()const{return _x;}
    int Y()const{return _y;}
    friend float Distance(Point &a,Point &b);

private:
    int _x;
    int _y;
};

float Distance(Point& a,Point& b)
{
    double dx=a.X()-b.X();
    double dy=a.Y()-b.Y();
    return sqrt(dx*dx+dy*dy);
}


int main()
{
    Point p1(5,5);
    Point p2(6,6);
    cout<<Distance(p1,p2)<<"\n";
    return 0;
}