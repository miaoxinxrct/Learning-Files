#include <iostream>

using namespace std;

class Time
{
public:
    Time(int hour=0,int minute=0,int second=0):_hour(hour),_minute(minute),_second(second){}
    void operator()(int hour,int minute,int second)
    {
        _hour=hour;
        _minute=minute;
        _second=second;
    }
    void showTime() const
    {
        cout<<_hour<<" "<<_minute<<" "<<_second<<"\n";
    }
private:
    int _hour;
    int _minute;
    int _second;
};

int main(int argc,char const* argv[])
{
    Time time1(12,10,11);
    time1.showTime();
    time1.operator()(23,20,34);
    time1.showTime();
    time1(10,10,10);
    time1.showTime();
    return 0;
}