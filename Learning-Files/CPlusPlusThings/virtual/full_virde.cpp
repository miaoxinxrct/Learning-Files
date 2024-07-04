#include <iostream>

using namespace std;

class Base
{
public:
    Base()
    {
        cout<<"Base Construct \n"<<endl;
    }

    virtual ~Base()
    {
        cout<<"Base deconstruct \n"<<endl;
    }
};

class Derived:public Base
{
public:
    Derived()
    {
        cout<<"Derived construt \n";
    }    
    ~Derived()
    {
        cout<<"Derived deconstrut \n";
    }
};

int main()
{
    Derived* d=new Derived();

    Base* b = d;
    
    delete b;
    return 0;
}