#include <iostream>

//using namespace std;

template<class T>
void swap(T& a,T& b)
{
    T t;
    t=a;
    a=b;
    b=t;
}

int main()
{
    int x1(5);
    int x2(7);
    swap(x1,x2);
    std::cout<<x1<<" "<<x2<<"\n";
    return 0;    
}