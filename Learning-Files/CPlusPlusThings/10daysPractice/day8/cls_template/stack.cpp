#include <iostream>

using namespace std;

template<class T,int MAXSIZE>
class Stack
{
public:
    Stack()
    {
        _top=0;
    }

    void push(T e);
    T& pop();
    bool empty() const
    {
        if(top<=-1)
            return 1;
        else
            return 0;
    }

    void setEmpty()
    {
        top=-1;
    }

    bool full() const
    {
        if(top>=MAXSIZE-1)
            return 1;
        else
            return 0;
    }
private:
    T _elem[MAXSIZE];
    int _top;
};

template<class T,int MAXSIZE>
void Stack<T,MAXSIZE>::push(T t)
{
    if(full())
        return;
    _top+=1;
    _elem[_top]=t;

}

template<class T,int MAXSIZE>
T& Stack<T,MAXSIZE>::pop()
{
    if(empty())
        return 0;
    return _elem[_top--];
}