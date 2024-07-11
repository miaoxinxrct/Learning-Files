#include <iostream>


class Fred;

class Barney
{
public:
    void func()
    {
        f1->funcInFred();//这个时候就是错误的，在隐形内联函数里面实现指针指向函数的操作，而f1只是占个指针的位置，未实现
    }
private:
    Fred f;//此处是错误的，因为只是前向声明，无法实现
    Fred* f1;//这就是对的，可以声明个指针，不用管里面怎么实现
}