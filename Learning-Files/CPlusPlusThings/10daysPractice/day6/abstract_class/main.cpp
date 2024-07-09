#include <iostream>

using namespace std;

class Figure
{
public:
    void set(double x,double y)
    {
        _x=x;
        _y=y;
    }
    virtual void area() const=0;
protected:
    double _x;
    double _y;
};

class Triangle:public Figure
{
public:
    void area() const override
    {
        cout<<"Tri::area: "<<_x*_y*0.5<<"\n";
    }
};

class Rectangle:public Figure
{
public:
    void area() const override
    {
        cout<<"Rect::area: "<<_x*_y<<"\n";
    }
};

int main()
{
    Figure* pF=nullptr;

    Rectangle r;
    Triangle t;

    t.set(10,20);

    pF=&t;
    pF->area();

    r.set(10,20);
    pF=&r;
    pF->area();

    Figure& rF=t;
    rF.set(20,20);
    rF.area();

    return 0;
}