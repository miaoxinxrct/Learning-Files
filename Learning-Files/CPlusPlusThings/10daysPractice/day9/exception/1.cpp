#include <iostream>

using namespace std;

int main()
{
    cout<<"1  before try block..\n";
    try{
        cout<<"2  Inside try block\n";
        throw 10;
        cout<<"3  after thow\n";

    }
    catch(int i)
    {
        cout<<"4  in catch block1 exception.."<<i<<"\n";
    }
    catch(char *s)
    {
        cout<<"5 in catch block2 exception.."<<s<<"\n";
    }
    cout<<"6  after catch\n";
    return 0;
}