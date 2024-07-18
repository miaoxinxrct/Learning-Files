#include <iostream>
#include <cstring>

using namespace std;

template<class T>
T Max(T a,T b)
{
    return (a>b)?a:b;
}

template<>
const char* Max(const char* a,const char* b)
{
    return (strcmp(a,b)>0)?a:b;
}

int main()
{
    char s1[]="xyfc";
    char s2[]="xyec";
    cout<<Max(s2,s1)<<"\n";
    return 0;
}