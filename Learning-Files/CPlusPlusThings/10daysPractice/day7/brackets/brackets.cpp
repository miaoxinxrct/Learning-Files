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
    };
    void showTime() const
    {
        cout<<"hour:"<<_hour<<"minute:"<<_minute<<"second:"<<_second;
    }
private:
    int _hour;
    int _minute;
    int _second;
};

int main()
{
    Time t1(12,10,11);
    t1.showTime();
    t1.operator()(11,13,14);
    t1.showTime();
    t1(15,16,17);
    t1.showTime();
    return 0;   
}