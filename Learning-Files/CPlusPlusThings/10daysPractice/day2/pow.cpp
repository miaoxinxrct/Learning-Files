
#include <iostream>

using namespace std;

double pow(double x,int n)
{
    double val=1.0;
    while(n--)
    {
        val*=x;
    }
    return val;
}

int main()
{
    int x;
    cin>>x;
    int wei=0;
    int sum;
    int each,chu;

    for(int i=0;i<8;i++)
    {
        each=x%10;
        chu=x/10;
        sum+=each*pow(2,wei);
        x=chu;
        wei++;
    }
    return 0;
}

