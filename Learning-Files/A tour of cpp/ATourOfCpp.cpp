#include <iostream>
#include <cmath>
#include "Vector.h"


double sqrt_sum(const Vector& v)
{
    double sum=0;
    for(int i=0;i!=v.size();++i)
        sum+=std::sqrt(v[i]);
    return sum;
}

double read_and_sum(int s)
{
    Vector v(s);
    for(int i=0;i<s;++i)
    {
        std::cin>>v[i];
    }
    double sum=0.0;
    for(int i=0;i!=v.size();++i)
    {
        sum+=v[i];
    }
    return sum;
}
