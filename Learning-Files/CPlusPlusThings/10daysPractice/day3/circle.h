#include <iostream>

constexpr float Pi=3.1415926;

class Circle
{
public:
    Circle(float radius);//{_radius=radius;};
    ~Circle();//{};

    inline float Area() const{ return Pi*_radius*_radius;}
    inline float Circumference() const { return 2*Pi*_radius;}

private:
    float _radius;
};

