#include <iostream>


class Base
{
public:
    int i;
protected:
    int j;
private:
    int k;
};

class Derived:public Base
{
public:
    void func()
    {
        i=1;
        j=2;
        //k=3;//私有成员不可访问，继承也不行
    }
};

int main()
{
    Base b;
    b.i=4;
    //b.j=5;protected 与private都不可访问
    //b.k=6;
}