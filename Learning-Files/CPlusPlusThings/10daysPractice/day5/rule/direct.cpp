
#include <iostream>

using namespace std;

class A
{
public:
    A(int x):_x(x)
    {
        cout<<"A construct\n";
    }
    ~A()
    {
        cout<<"A deconstruct\n";
    }
private:
    int _x;
};

class B:public A
{
public:
    B(int x): A(x)
    {
        cout<<"B construct\n";
    };
};

class C:public B
{
public:
    C(int x):B(x)
    {
        cout<<"C construct\n";
    }
};

int main()
{
    C c(1);
    return 0;
}

