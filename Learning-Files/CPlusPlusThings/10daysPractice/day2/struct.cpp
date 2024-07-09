#include <iostream>

using namespace std;

struct student
{
    int num;
    char name[20];
    char gender;
};

int main(int argc,char const* argv[])
{
    student stu={10,"asd",'M'};
    cout<<stu.num<<endl;
    cout<<sizeof(stu.num)<<endl;
    cout<<sizeof(stu.name)<<endl;
    cout<<sizeof(stu.gender)<<endl;
    cout<<sizeof(stu)<<endl;
    return 0;
}