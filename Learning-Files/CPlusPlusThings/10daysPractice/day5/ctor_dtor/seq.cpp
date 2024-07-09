#include <iostream>

using namespace std;

class A
{
public:
    A(int i=0):_x(i)
    {
        cout<<"A---"<<_x<<"\n";
    }
private:
    int _x;
};

class B
{
public:
    B(int i=0):_x(i)
    {
        cout<<"B---"<<_x<<"\n";
    }
private:
    int _x;
};

class C
{
public:
    C(int i=0):_x(i)
    {
        cout<<"C---"<<_x<<"\n";
    }
private:
    int _x;
};

class D:public B
{
public:
    D(int i=0):a4(4),c2(2),c1(1),B(1)
    {
        cout<<"D---\n";
    }
    ~D(){delete a1;}
private:
    C c1,c2;
    A* a1=new A(10);
    A a0,a4;
};

int main()
{
    D d;
    return 0;
}
/*
B--- 1
C--- 1
c--- 2
a--- 10
a--- 0
a--- 4
d---
*/