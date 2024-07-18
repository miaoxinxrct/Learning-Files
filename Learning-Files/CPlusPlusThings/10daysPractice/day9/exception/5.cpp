#include <iostream>

using namespace std;

// void handler(int n)throw(int,char,double)
void handler(int n) throw(int)
{
    if(n==1)
        throw n;
    if(n==2)
        throw 'x';
    if(n==3)
        throw 1.1;
}

int main()
{
    cout<<"Before handler..\n";

    try
    {
        handler(1);
    }
    catch(int i)
    {
        cout<<"catch an integer \n";
    }
    catch(char c)
    {
        cout<<"catch an char  \n";
    }
    catch(double d)
    {
        cout<<"catch an double...\n";
    }
    return 0;
}