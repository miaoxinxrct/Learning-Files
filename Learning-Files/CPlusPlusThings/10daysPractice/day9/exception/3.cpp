#include <iostream>

using namespace std;

void temperature(int t)
{
    try
    {
        if(t==100)
            throw "It's a boiling point\n";
        else if(t==0)
            throw "It reached the freezing point\n";
        else
            cout<<"the temperature is Ok\n";
                   
    }
    catch(int x)
    {
        cout<<"temperature = "<<x<<"\n";
    }
    catch(char const* s)
    {
        cout<<s<<"\n";
    }
}

int main()
{
    temperature(0);
    temperature(10);
    temperature(100);
    return 0;
}
