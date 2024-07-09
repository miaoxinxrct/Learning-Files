#include "circle.h"

constexpr float FencePrice=35;
constexpr float ConcretePrice=20;


int main()
{
    float radius;

    std::cout<<"Please enter the radius of the pool: ";
    std::cin>>radius;

    Circle pool(radius);
    Circle poolRim(radius+3);

    float fenceCost=poolRim.Circumference()*FencePrice;
    std::cout<<"External fence cost is $"<<fenceCost<<"\n";

    float concreteCost=(poolRim.Area()-pool.Area())*ConcretePrice;
    std::cout<<"Concrete cost is $"<<concreteCost<<"\n";
    return 0;
}