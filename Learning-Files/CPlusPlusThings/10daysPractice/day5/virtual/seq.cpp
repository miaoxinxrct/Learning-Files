#include <iostream>

using namespace std;

class A
{
public:
    A(){cout<<"Construct A  a=\n";}
private:
    int _a;
};

class B
{
public:
    B(){cout<<"Construct b  b="<<_b<<"\n";}
private:
    int _b;
};

class B1:virtual public B,virtual public A
{
public:
    B1(int i){cout<<"Construct B1\n";}
};

class B2:virtual public A,virtual public B
{
public:
    B2(int i){cout<<"Construct B2\n";}
};


class D:public B1,public B2
{
private:
    A a;
public:
    D(int m,int n):B1(m),B2(n){cout<<"Construct D\n";}
};

int main()
{
    D d(1,2);
    return 0;
}

