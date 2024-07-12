#include <iostream>

using namespace std;

class A
{
public:
    virtual ~A()
    {
        cout<<"A deconstruct...\n";
    }
};

class B:public A
{
public:
    B(int i){buf=new char[i];}
    ~B(){
        delete[] buf;
        cout<<"B deconstruct\n";}
private:
    char* buf;
};

int main()
{
    A* a=new B(10);
    delete a;
};