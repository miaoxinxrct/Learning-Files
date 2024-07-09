#include <iostream>
#include <string>

using namespace std;

class Employee
{
public:
    Employee(string name,string id):_name(name),_id(id){}
    string name() const{return _name;}
    string ID() const{return _id;}
    virtual float salary() const =0;
    virtual void printInfo() const{cout<<_name<<" "<<_id<<" "<<salary()<<"\n";}
private:
    string _name;
    string _id;
};

class Manager:public Employee
{
public:
    Manager(string name,string id,int week):Employee(name,id)
    {
        _weekSalary=week*1000;
    }
    float salary()const {return _weekSalary;}
    //void printInfo() const{cout<<name()<<" "<<ID()<<" "<<salary()<<"\n";}
private:
    float _weekSalary;
};

class SaleWorker:public Employee
{
public:
    SaleWorker(string name,string id,int saleCount,float profit):Employee(name,id),_saleCount(saleCount),_profit(profit),_baseSalary(800)
    {
        _finalSalary=_baseSalary+_saleCount*0.05*_profit;
    }
    float salary()const override{return _finalSalary;}
    //void printInfo() const{cout<<name()<<" "<<ID()<<" "<<salary()<<"\n";}
private:
    int _saleCount;
    float _profit;
    float _baseSalary;
    float _finalSalary;
};

class HourWorker:public Employee
{
public:
    HourWorker(string name,string id,int hour,int hourMoney=100):Employee(name,id),_hour(hour)
    {
    }

    float salary() const override{return (float)_hour*_hourMoney;}



private:
    int _hour;
    float _hourMoney;
};


int main()
{
    cout<<"Enter Weeks: ";
    int weeks;
    cin>>weeks;
    Manager manager("Wang","1000",weeks);
    manager.printInfo();

    int saleCount;
    cout<<"Enter saleCount: ";
    cin>>saleCount;
    float profit;
    cout<<"Enter profit: ";
    cin>>profit;

    SaleWorker saleWorker("Li","1001",saleCount,profit);
    saleWorker.printInfo();

    int hour;
    cout<<"Enter hour: ";
    cin>>hour;
    HourWorker hourWorker("Zheng","1002",100);




    return 0;

}
