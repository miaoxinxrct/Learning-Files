#include "Vector.h"
#include <stdexcept>


Vector::Vector(int s):elem{new double[s]},sz{s}
{
    if(s<0)
        throw length_error{"Vector constructor: negativate size"};
    elem=new double[s];
    sz=s;
}

Vector::Vector(std::initializer_list<double> lst)
:elem(new double[lst.size()]),sz(static_cast<int>(lst.size())) //此处用static_cast是因为标准库里面的size类型是unsigned int
{
    std::copy(lst.begin(),lst.end(),elem);
}

Vector::~Vector()
{
    delete[] elem;
}

double& Vector::operator[] (int i)
{
    if(i<0||size()<=i)
        throw out_of_range {"Vector::operator[]"};
    return elem[i];
}
int Vector::size()
{
    return sz;
}

