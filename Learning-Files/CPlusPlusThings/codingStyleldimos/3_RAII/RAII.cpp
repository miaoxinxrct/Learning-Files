#include <iostream>
#include <mutex>
#include <fstream>

using namespace std;

enum class shape_type
{
    circle,
    triangle,
    rectangle
};

class shape
{
public:
    shape()
    {
        cout<<"shape\n";
    };
    virtual void print()
    {
        cout<<"I am shape \n";
    }
    virtual ~shape()
    {}
};

class circle:public shape
{
public:
    circle()
    {
        cout<<"circle\n";
    }
    void print()
    {
        cout<<"I am circle\n";
    }
};

class triangle:public shape
{
public:
    triangle()
    {
        cout<<"Triangle\n";
    }
    void print()
    {
        cout<<"I am triangle\n";
    }
};

class rectangle:public shape
{
public:
    rectangle()
    {
        cout<<"rectangle\n";
    }
    void print()
    {
        cout<<"I am rectangle\n";   
    }
};

shape* create_shape(shape_type type)
{
    switch(type)
    {
        case shape_type::circle:
            return new circle();
        case shape_type::triangle:
            return new triangle();
        case shape_type::rectangle:
            return new rectangle();
    }
}

class shape_warpper
{
public:
    explicit shape_wrapper(shape* ptr=nullptr):_ptr(ptr)
    {

    }
    ~shape_wrapper()
    {
        delete _ptr;
    }

    shape* get() const
    {
        return _ptr;
    }
private:
    shape* _ptr;
};

void foo()
{
    shape_wrapper ptr(create_shape(shape_type::circle));
    ptr.get()->print();
}

int main()
{
    shape* sp=create_shape(shape_type::circle);
    sp->print();
    delete sp;

    foo();

    return 0;
}