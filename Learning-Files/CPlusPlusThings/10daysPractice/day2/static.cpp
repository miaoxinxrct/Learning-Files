#include <iostream>
using namespace std;

volatile int i=1;//此处只是全局变量，声明周期很长。。。

void other(void)
{
    static int a=2;
    static int b;
    int c=10;
    a+=2;
    i+=32;
    c+=5;
    cout<<"---Other---\n";
    cout<<" i: "<<i<< " a: "<<a<<" b: "<<b<<" c: "<<c<<"\n";
    b=a;
}

int main(void)
{
    static int a;
    int b=-10;
    int c=0;

    

    cout<<"---Main---\n";
    cout<<" i: "<<i<< " a: "<<a<<" b: "<<b<<" c: "<<c<<"\n";
    c+=8;
    other();
    cout<<"---Main---\n";
    cout<<" i: "<<i<< " a: "<<a<<" b: "<<b<<" c: "<<c<<"\n";
    i+=10;
    other();
    other();

    return 0;
}