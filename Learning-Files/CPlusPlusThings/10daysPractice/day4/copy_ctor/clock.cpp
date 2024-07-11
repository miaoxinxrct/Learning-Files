#include "clock.h"
#include <iostream>

    Clock::Clock(int hour,int minute,int second):
    _hour(hour),_minute(minute),_second(second)
    {

    }

    Clock::Clock(const Clock& other)
    {
        _hour=other._hour;
        _minute=other._minute;
        _second=other._second;
    }
    Clock::Clock(Clock&& other)
    {
        _hour=other._hour;
        _minute=other._minute;
        _second=other._second;
    }

    Clock::~Clock()
    {

    }

    void Clock::setTime(int hor,int min,int sec)
    {
        _hour=hor;
        _minute=min;
        _second=sec;
    }
    void Clock::showTime()
    {
        std::cout<<_hour<<" "<<_minute<<" "<<_second<<"\n";
    }


int main()
{
    Clock c(0,0,0);
    c.setTime(12,12,12);
    c.showTime();

    Clock c1(c);
    c1.showTime();
    c1.setTime(12,22,33);
    c1.showTime();
    return 0;
}
