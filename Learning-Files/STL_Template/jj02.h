#include <vector>
#include <stdexcept>
#include <string>
#include <ctime>
#include <cstdlib>
#include <algorithm>
#include "assistant.h"

using namespace std;

namespace jj02
{
void test_vector(long value)
    {
        cout<<"\ntest_vector().....\n";
       
        vector<string> c;
        char buf[10];        

        clock_t timestart=clock();
        for(long i=0;i<value;++i)
        {
            try
            {
                snprintf(buf,10,"%d",rand());
                c.push_back(string(buf));
            }
            catch(exception& p)
            {
                cout<<"i="<<i<<" "<<p.what()<<"\n";
                abort();
            }
        }
        cout<<"milli-seconds :"<<clock()-timestart<<"\n";
        cout<<"vector.size() :"<<c.size()<<"\n";
        cout<<"vector.front() :"<<c.front()<<"\n";
        cout<<"vector.back() :"<<c.back()<<"\n";
        cout<<"vector.data() :"<<c.data()<<"\n";
        cout<<"vector.capcity() :"<<c.capacity()<<"\n";

        string target=get_a_target_string();
        {
            timestart=clock();
            auto pItem=::find(c.begin(),c.end(),target);
            cout<<"::find(),milli-seconds :"<<clock()-timestart<<"\n";
            if(pItem!=c.end())
                cout<<"found,"<<*pItem<<"\n";
            else
                cout<<"Not found!\n"; 
        }
        
        {
            timestart=clock();
            sort(c.begin(),c.end());
            string* pItem=(string*)bsearch(&target,(c.data()),c.size(),sizeof(string),compareStrings);
            cout<<"Sort()+bsearch(),milli-seconds :"<<clock()-timestart<<"\n";
            if(pItem!=nullptr)
                cout<<"found,"<<*pItem<<"\n";
            else
                cout<<"Not found!\n"; 
        }
        
    }
} 
