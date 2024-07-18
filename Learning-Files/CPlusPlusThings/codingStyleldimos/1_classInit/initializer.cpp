#include <iostream>

using namespace std;

class A
{
public:
    A(int a):_a(a),_p(nullptr)
    {

    }
private:
    int _a;
    int* _p;
};
int main()
{
    A aa(10);
    return 0;
}