
#ifndef CLOCK_H
#define CLOCK_H

class Clock
{
public:
    Clock(int hour,int minute,int second);
    

    Clock(const Clock& other);
    Clock(Clock&& other);
    ~Clock();

    void setTime(int hor,int min,int sec);
    void showTime();

private:
    int _hour;
    int _minute;
    int _second;
};

#endif