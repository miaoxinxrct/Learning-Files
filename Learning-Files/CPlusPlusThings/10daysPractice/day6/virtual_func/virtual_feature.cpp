#include <iostream>

using namespace std;

class A
{
public:
    void f(){cout<<"A:f\n";}
};

class B:public A
{
public:
    void f(){cout<<"B:f\n";}
};

class C:public B
{
public:
    virtual void f(){cout<<"C:f\n";}
};

class D:public C
{
public:
    void f(){cout<<"D:f\n";}
};

class E:public D
{
public:
    void f(){cout<<"E:f\n";}
};


int main()
{
    A *pA,a;
    B *pB,b;
    C *pC,c;
    D d;
    E e;

    pA=&a;
    pA->f();
    pB=&b;
    pB->f();
    pC=&c;
    pC->f();
    pC=&d;
    pC->f();
    pC=&e;
    pC->f();

    return 0;
}