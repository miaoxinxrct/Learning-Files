
#include <iostream>

using namespace std;

class A
{
public:
    A(){cout<<"A construct\n";}
    ~A(){cout<<"A deconstruct\n";}
};

class B
{
public:
    B(){cout<<"B construct\n";}
    ~B(){cout<<"B deconstruct\n";}
};


class C
{
public:
    C(){cout<<"C construct\n";}
    ~C(){cout<<"C deconstruct\n";}
private:
    A a;
    B b;
};

int main()
{
    C c;
}