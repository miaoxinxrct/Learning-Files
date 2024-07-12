#include <iostream>

using namespace std;

enum  Weekday
{
    Sunday=0,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
};

int main(int argc,char const* argv[])
{
    enum Weekday _wek=Tuesday;
    for(int i=_wek;i!=Saturday;i++)
    {
        cout<<i<<endl;
        cout<<_wek+Friday<<endl;
    }
    return 0;

}