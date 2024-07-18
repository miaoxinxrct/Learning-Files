#include <iostream>

using namespace std;

class X
{
public:
    int operator()(int i=0)
    {
        cout<<"X::operator("<<i<<")\n";
        return i;
    }

    int operator()(int i,int j)
    {
        cout<<"X:operator("<<i<<","<<j<<")\n";
        return i;
    }

    int operator[](int i)
    {
        cout<<"X::operator["<<i<<"]\n";
        return i;
    }

    int operator[](const char* cp)
    {
        cout<<"X::operator["<<cp<<"]\n";
        return 0;
    }
};
int main()
 {
        X obj;
        int i=obj(obj(1),2);
        int a=obj[i];
        int b=obj["1we"];
        cout<<"a="<<a<<"\n";
        cout<<"b="<<b<<"\n";
        return 0;
    }

