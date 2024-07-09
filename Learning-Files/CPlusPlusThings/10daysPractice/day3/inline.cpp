#include <iostream>

using namespace std;

constexpr double Pi=3.14159;

inline double CalArea(double radius)
{
    return Pi*radius*radius;
}

int main(int argc,char const* argv[])
{
    double r(3.0);
    double area;
    area=CalArea(r);
    cout<<area<<"\n";
    return 0;
}