#include <iostream>

int main()
{
    std::string str=std::string("toptal");
    std::cout<<"string object: "<<str<<" @ "<<&str<<"\n";
    str += ".com";
    std::cout<<"string object: "<<str<<" @ "<<&str<<"\n";
    return 0;
}