#include <iostream>

using namespace std;

class Base
{
protected:
    int getX()const {return _x;}
 
public:
    void setX(int n){_x=n;}
   void showX() const {cout<<"Base::X:"<<_x<<"\n";}
private:
    int _x;
};

class Derived:protected Base
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
    //obj.setX(10);//ÒòÎª¼Ì³ÐÊÇprotected
    obj.setY(20);
    //obj.showX();
    obj.showY();
    return 0;

}