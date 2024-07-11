#include <iostream>
#include "Point.h"


int main()
{
    Point p(4,5);
    cout<<p.X()<<" "<<p.Y()<<" "<<p.CountP()<<"\n";

    Point p2(p);
    cout<<p2.X()<<" "<<p2.Y()<<" "<<p2.CountP()<<"\n";

    return 0;
}