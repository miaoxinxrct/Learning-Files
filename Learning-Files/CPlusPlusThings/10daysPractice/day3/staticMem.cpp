#include <iostream>

using namespace std;

class Application
{
public:
    static void f();
    static void g();

private:
    static int global;
};

int Application::global=0;

void Application::f(){global=5;}
void Application::g(){cout<<global<<"\n";}


class A
{
public:
    static void f(A a);
private:
    int x;
};

void A::f(A a)
{
    cout<<a.x;//ÕýÈ·
    //cout<<x;//´íÎó
}

int main()
{
    Application::f();
    Application::g();

    A a;
    a.f(A());


    return 0;
}