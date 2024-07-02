#include <iostream>
#include <typeinfo>

using namespace std;

class Base{
public:
    virtual void fun(){}
};
class Derived:public Base
{

};

int main()
{
    Base* b=new Derived;//如果把Derived变成Base就会报错了
    Base& obj=*b;

    Derived *d=dynamic_cast<Derived*>(b);
    if(d!=nullptr)
        cout<<"works"<<endl;
    else
        cout<<"out of work"<<endl;

    try{
        Derived& dobj=dynamic_cast<Derived&>(obj);
        cout<<"try works"<<endl;
    }catch(bad_cast bc)
    {
        cout<<bc.what()<<endl;
    }
    return 0;
}