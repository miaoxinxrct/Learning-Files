#include <iostream>

using namespace std;

class A
{
    friend class B;

    public:
        void display()
        {
            cout<<x<<"\n";

        }
    private:
        int x;
};

class B
{
    public:
        void set(int i);
        void display();
    private:
        A a;
};

void B::set(int i)
{
    a.x=i;
}

void B::display()
{
    a.display();
}

int main()
{
    B b;
    b.set(10);
    b.display();
    return 0;
}