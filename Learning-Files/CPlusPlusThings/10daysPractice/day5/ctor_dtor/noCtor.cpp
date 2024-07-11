#include <iostream>

using namespace std;

class Base
{
public:
    Base(){cout<<"Base construct\n";}
    Base(int x):_x(x)
    {
        cout<<"Base construct:: x="<<_x<<"\n";
    }
    ~Base()
    {
        cout<<"Base deconstruct...\n";
    }
private:
    int _x;
};

class Derived:public Base
{
public:
    // Derived(int x,int y):Base(x),_y(y)
    // {
    //     cout<< "Derived construct:: _y="<<y<<"\n";
    // }
    ~Derived()
    {
        cout<<"Derived deconstruct...\n";
    }
private:
    int _y;
};

int main()
{
    Derived d;
    return 0;
}