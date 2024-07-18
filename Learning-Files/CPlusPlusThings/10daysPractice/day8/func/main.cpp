#include <iostream>

using namespace std;

template<class T>
T& Max(T& a,T b)
{
    return (a>b)?a:b;
}
template<class T>
T& Min(T& a,T b)
{
    return (a>b)?b:a;
}

int main()
{
    double a=2,b=3.4;
    cout<<Min(a,b)<<"\n";
    return 0;
}