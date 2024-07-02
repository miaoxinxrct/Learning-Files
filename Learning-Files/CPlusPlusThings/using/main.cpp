#include <iostream>

using namespace std;

class Base
{
public:
    void f()
    {
        cout<<"f()"<<endl;
    }
    void f(int n)
    {
        cout<<"Base::f(int)"<<endl;

    }
};

class Derived:private Base
{
public:
    using Base::f;
    void f(int n)
    {
        cout<<"Derived::f(int)"<<endl;
    }
};

int main()
{
    Base b;
    Derived d;
    d.f();//在Vscode上显示没有这个f成员，我觉得应该是vscode的问题
    d.f(1);
    return 0;
}