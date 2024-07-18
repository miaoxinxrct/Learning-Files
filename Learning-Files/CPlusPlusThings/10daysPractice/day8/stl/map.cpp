#include <iostream>
#include <map>
#include <string>

using namespace std;

int main()
{
    string name[]={"ww","ee","wee"};
    float salary[]={1200,1500,1800};
    map<string,double> sal;
    map<string,double>::iterator p;

    for(int i=0;i<3;i++)
    {
        sal.insert(make_pair(name[i],salary[i]));
    }
    sal["Tom"]=1700;
    sal["Peter"]=1678;
    for(auto elem=sal.begin();elem!=sal.end();elem++)
    {

        cout<<elem->first<<":"<<elem->second<<"\n";
    }
    string person;
    int flag=1;
    cin>>person;
    for(auto elem=sal.begin();elem!=sal.end();elem++)
    {
        if(elem->first==person)
        {   
            cout<<elem->first<<":"<<elem->second<<"\n";
            flag=0;
        }
    }
    if(flag)
        cout<<"查无此人\n";
    cout<<"删除人员：\n";
    cin>>person;
    
    map<string,double>::iterator it;
    it=sal.find(person);
    if(it!=sal.end())
    {
        cout<<"Find\n"<<(*it).first<<":"<<(*it).second<<"\n";
        sal.erase(it);

    }
    for(auto elem=sal.begin();elem!=sal.end();elem++)
    {
        cout<<elem->first<<":"<<elem->second<<"\n";
    }
    return 0;
}