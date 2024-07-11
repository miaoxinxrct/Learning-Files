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
    Time operator++()
    {
        ++_second;
        if(_second==60)
        {
            _second=0;
            ++_minute;
            if(_minute==60)
            {
                _minute=0;
                _hour++;
                if(_hour==24)
                    _hour=0;
            }
        }
        return *this;
    }
    Time operator++(int n)
    {
        Time tmp=*this;
        ++(*this);
        return tmp;
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

int main()
{
    Time t(23,59,59);
    ++t;
    cout<<"1\n";
    t.showTime();
    cout<<"2\n";
    (t++).showTime();
    cout<<"3\n";
    t.showTime();
    return 0;
}