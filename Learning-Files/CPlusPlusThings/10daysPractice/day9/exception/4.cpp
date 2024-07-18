#include <iostream>

using namespace std;

void temperature(int t)
{
        if(t==100)
            throw "It's a boiling point\n";
        else if(t==0)
            throw "It reached the freezing point\n";
        else
            cout<<"the temperature is Ok\n";
                   
    }
int main()
{
    try
    {
    temperature(0);
    temperature(10);
    temperature(100);
    }
    catch(const char* s)
    {
        cout<<s<<"\n";
    }
    return 0;
}

