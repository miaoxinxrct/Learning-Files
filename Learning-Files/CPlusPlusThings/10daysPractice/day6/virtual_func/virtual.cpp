#include <iostream>
#include <string>

using namespace std;

class Employee
{
public:
    Employee(string name,string id):_name(name),_id(id){}
    string name() const{return _name;}
    string ID() const{return _id;}
    virtual float salary() const {return 0.0;};
    void printInfo() const{cout<<_name<<" employee"<<_id<<" "<<salary()<<"\n";}
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
    void printInfo() const{cout<<name()<<" manager "<<ID()<<" "<<salary()<<"\n";}
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

    Employee employee("Jesson","10002"),*pE;

    Manager manager("Wang","1000",128);
    manager.printInfo();

    pE=&manager;
    pE->printInfo();

    Employee &rM=manager;
    rM.printInfo();


    return 0;

}