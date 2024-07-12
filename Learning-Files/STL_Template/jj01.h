#include <array>
#include <iostream>
#include <ctime>
#include <cstdlib>
#include "assistant.h"

using namespace std;

constexpr int ASIZE=500000;

namespace jj01
{
    void test_array()
    {
        cout<<"\ntest_array().....\n";
        array<long,ASIZE> c;

        clock_t timestart=clock();
        for(long i=0;i<ASIZE;++i)
        {
            c[i]=rand();
        }
        cout<<"milli-seconds :"<<clock()-timestart<<"\n";
        cout<<"array.size() :"<<c.size()<<"\n";
        cout<<"array.front() :"<<c.front()<<"\n";
        cout<<"array.back() :"<<c.back()<<"\n";
        cout<<"array.data() :"<<c.data()<<"\n";

        long target=get_a_target_long();

        timestart=clock();
        qsort(c.data(),ASIZE,sizeof(long),compareLongs);
        long* pItem=(long*)bsearch(&target,(c.data()),ASIZE,sizeof(long),compareLongs);
        cout<<"qSort()+bsearch(),milli-seconds :"<<clock()-timestart<<"\n";
        if(pItem!=nullptr)
            cout<<"found,"<<*pItem<<"\n";
        else
            cout<<"not found!\n";
    }
} 
