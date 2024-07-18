#include <cstring>
#include <iostream>

using namespace std;

constexpr int Size=5;

template<class T>
class Array
{
public:
    Array()
    {
        for(int i=0;i<Size;i++)
        {
            a[i]=0;
        }
    }
    T& operator[](int i);
    void sort();
private:
    T a[Size];
};

template<class T>
T& Array<T>::operator[](int i)
{
    if(i<0||i>=Size)
        exit(1);
    else
        return a[i];
}

template<class T>
void Array<T>::sort()
{
    for(int i=0;i<Size-1;i++)
    {
        for(int j=i;j<Size;j++)
        {
            if(a[i]<a[j])
            {
                T t=a[i];
                a[i]=a[j];
                a[j]=t;
            }
        }
    }
}

template<>
void Array<char*>::sort()
{
    
}

