#include <iostream>

using namespace std;

class Base
{
public:
    void setX(int n){_x=n;}
    int getX()const {return _x;}
    void showX() const {cout<<"Base::X:"<<_x<<"\n";}
private:
    int _x;
};

class Derived:public Base
{
public:
    void setY(int n){_y=n;}
    void setY(){_y=getX();}
    void showY()const {cout<<"Derived::Y:"<<_y<<"\n";}
private:
    int _y;
};

int main()
{
    Derived obj;
    obj.setX(10);
    obj.setY(20);
    obj.showX();
    obj.showY();
    obj.setY();
    obj.showX();
    obj.showY();
    return 0;

}