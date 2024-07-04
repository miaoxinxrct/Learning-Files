#include <iostream>

using namespace std;

union myun
{
    struct{
        int x;
        int y;
        int z;
    }u;
    int k;
}a;

int main()
{
    a.u.x=4;
    a.u.y=5;
    a.u.z=6;

    a.k=0;

    cout<<a.u.x<<a.u.y<<a.u.z<<a.k<<endl;
    
}