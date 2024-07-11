#include <iostream>

using namespace std;

class A
{
private:
    int a;
public:
    A(int x):a(x){cout<<"virtual base A\n";};
    
};

class B:virtual public A
{
private:
    /* data */
public:
    B(int b):A(b){cout<<"B construct\n";}
};

class C:virtual public A
{
private:
    int c;
public:
    C(int x):A(c)
    {
        cout<<"C construct\n";
        c=x;
    }
};


class ABC:public C,public B
{
    public:
        ABC(int i,int j,int c):C(i),B(j),A(c)
        {
            cout<<"ABC Construct\n";
        }
};

int main()
{
    ABC(1,2,3);
    return 0;
}

