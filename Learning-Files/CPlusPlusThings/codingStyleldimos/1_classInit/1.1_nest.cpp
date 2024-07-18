#include <iostream>

using namespace std;

class Animal
{
public:
    Animal()
    {
        cout<<"Animal() is called\n";
    }
    Animal(const Animal& other)
    {
        cout<<"Animal(const Animal& ) is called\n";
    }
    Animal& operator=(const Animal&)
    {
        cout<<"Animal& operator=(const Animal&) is Called\n";
        return *this;
    }
    ~Animal()
    {
        cout<<"~Animal() is called\n";
    }

private:


};

class Dog
{
public:
    Dog(const Animal& animal):_animal(animal)
    {
        cout<<"Dog(const Animal& ) is called\n";
    }
    // Dog(const Animal& animal)
    // {
    //     _animal=animal;
    //     cout<<"Dog(const Animal& ) is called\n";
    //}


    ~Dog()
    {
        cout<<"~Dog() is called\n";
    }

private:
    Animal _animal;
};

int main()
{
    Animal animal;
    cout<<std::endl;
    Dog d(animal);
    cout<<std::endl;
    return 0;
}