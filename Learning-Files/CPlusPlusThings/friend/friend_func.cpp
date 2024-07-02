
#include <iostream>

using namespace std;

// class A
// {
// private:
//     int _a;
// public:
//     A (int a):_a(a){}
//     friend int geta(A& ca);
// };

// int geta(A& ca)
// {
//     return ca._a;//这个地方怎么显示访问不到？怎么这么诡异？
// }

// int main()
// {
//     A a(3);
//     cout<<geta(A)<<endl;
//     return 0;
// };

class B;
class A
{
private:
    int _numA;
public:
    A():_numA(12){}
    friend int add(A,B);
};

class B
{
private:
    int _numB;
public:
    B():_numB(13){}
    friend int add(A,B);
};

int add(A a,B b)
{
    return a._numA+b._numB;
}

int main()
{
    A objA;
    B objB;
    cout<<"Sum: "<<add(objA,objB);
    return 0;
}