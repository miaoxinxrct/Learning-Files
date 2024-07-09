#include <iostream>
using namespace std;

class Base
{
public:
    void setX(int x){_x=x;}
    int getX() const {return _x;}
private:
    int _x;
};

class Derived:public Base
{
public:
    void setY(int y){_y=y;}
    int getY()const {return _y;}
private:
    int _y;
};

void f1(Base b,int x){b.setX(x);}

void f2(Base* pb,int x){pb->setX(x);}

void f3(Base& rb,int x){rb.setX(x);}

int main()
{
    Base b1,*pb;
    Derived d1;
    b1.setX(1);
    d1.setX(2);
    b1=d1;
    cout<<"b1 x=2\n";
    cout<<b1.getX()<<endl;
    cout<<"d1 x=2\n";
    cout<<d1.getX()<<endl;
    b1.setX(10);
    cout<<"b1 x=10\n";
    cout<<b1.getX()<<endl;
    cout<<"d1 x=2\n";
    cout<<d1.getX()<<endl;

    pb=&d1;
    pb->setX(20);
    cout<<"pb-x=20\n";
    cout<<pb->getX()<<endl;
    cout<<"d1 x=20\n";
    cout<<d1.getX()<<endl;

    Base& rb=d1;
    rb.setX(30);
    cout<<"rb x=30\n";
    cout<<pb->getX()<<endl;
    cout<<"d1 x=30\n";
    cout<<d1.getX()<<endl;

    d1.setX(7);
    cout<<"d1 x=7\n";
    cout<<d1.getX()<<endl;

    f1(d1,100);
    cout<<"d1 x=7\n";
    cout<<d1.getX()<<endl;

    f2(&d1,200);
    cout<<"d1 x=200\n";
    cout<<d1.getX()<<endl;

    f3(d1,300);
    cout<<"d1 x=300\n";
    cout<<d1.getX()<<endl;

    return 0;
}
