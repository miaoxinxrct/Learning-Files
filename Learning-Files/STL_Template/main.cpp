#include <iostream>
#include "jj01.h"
#include "jj02.h"


int main()
{
    int enterPro;
    cin>>enterPro;
    switch (enterPro)
    {
    case 1:
        jj01::test_array();
        break;
    case 2:
        jj02::test_vector(1000000);
        break;
    default:
        break;
    }
    return 0;
}