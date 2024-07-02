#include <iostream>

using namespace std;

struct A
{
    A(int i){}
    operator bool() const{
        return true;
        }
};

struct B{
    explicit B(int i){}
    explicit operator bool () const{return true;}

};

void doA(A a){}

void doB(B b)

int main()
{
    A a1(1);//直接初始化
    A a2=1;//复制初始化
    A a3{1};//列表初始化
    A a4={1};//复制列表初始化
    A a5=(A)1;//允许 static_cast显式转换
    doA(1);//允许int到A的隐式转换
    if(a1)
    ;//使用转换函数，A：：operator bool()从A到bool的隐式转换
    bool a6(a1);
    bool a7=a1;
    bool a8=static_cast<bool>(a1);


    B b1(1);
    //B b2=1;//这个是不行的
    B b3{1};
    //B b4={1};这个是不可以的
    B b5=(B)1;//这个是显式转换 static_cast
    //doB(b1);这个是不可以的
    if(b1)
    ;

    bool b6(b1);
    //bool b7=b1;这个是不可以
    bool b8=static_cast<bool> (b1);
    return 0;


};