#include <iostream>

using namespace std;

class Animal
{
public:
    Animal(int age)
    {
        cout<<"Animal(int age) is called\n";
    }
    Animal(const Animal& animal)
    {
        cout<<"Animal(const Animal& animal) is called\n";
    }
    Animal& operator=(const Animal& animal)
    {
        cout<<"Animal& operator=(const Animal& animal) is called\n";
        return *this;
    }
    ~Animal()
    {
        cout<<"~Animal() is called\n";
    }
};

class Dog:public Animal
{
public:
    Dog(int age):Animal(age)
    {
        cout<<"Dog(int age) is called\n";
    }
    ~Dog()
    {
        cout<<"~Dog() is called\n";
    }
};

int main()
{
    Animal Animal(10);
    cout<<endl;
    Dog dog(100);
    cout<<endl;
    return 0;
}