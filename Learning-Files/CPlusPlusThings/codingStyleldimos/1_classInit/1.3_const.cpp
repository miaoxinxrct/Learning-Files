#include <iostream>
#include <string>

using namespace std;

class Animal
{
public:
    Animal(int age,string name):_age(age),_name(name)
    {
        cout<<"Animal(int age,string name) is called\n";
    }

private:
    int _age;
    const string _name;
};

int main()
{
    Animal animal(10,"peter");
    return 0;
}