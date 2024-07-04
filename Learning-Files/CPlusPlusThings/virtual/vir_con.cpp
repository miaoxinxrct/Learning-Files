#include <iostream>

using namespace std;

class Base{
public:
    Base(){cout <<"Base Construt \n";}
    virtual ~Base(){cout <<"Base deconstruct \n";}

    virtual void ChangeAttributes()=0;
    
    static Base* Create(int id);

    virtual Base* Clone()=0;
};

class Derived:public Base{
public:
    Derived(){cout<<"Derived construct \n";}

    Derived(const Derived& other){cout<<"Derived copy construct \n";}

    ~Derived(){cout <<"Derived deconstruct \n";}

    void ChangeAttributes(){cout<< "Derived changeattributes \n";}

    Base* Clone() override { return new Derived(*this); }

};

class Derived2:public Base{
public:
    Derived2(){cout<<"Derived2 construct \n";}

    Derived2(const Derived2& other){cout<<"Derived2 copy construct \n";}

    ~Derived2(){cout <<"Derived2 deconstruct \n";}

    void ChangeAttributes(){cout<< "Derived2 changeattributes \n";}

    Base* Clone() override { return new Derived2(*this); }

};

class Derived3:public Base{
public:
    Derived3(){cout<<"Derived3 construct \n";}

    Derived3(const Derived3& other){cout<<"Derived3 copy construct \n";}

    ~Derived3(){cout <<"Derived3 deconstruct \n";}

    void ChangeAttributes(){cout<< "Derived3 changeattributes \n";}

    Base* Clone() override { return new Derived3(*this); }

};

Base* Base::Create(int id)
{
    if(id==1)
        return new Derived;
    else if(id==2)
        return new Derived2;
    else
        return new Derived3;
};

class User
{
public:
    User():pBase(nullptr)
    {
        int input;
        cout<<"Enter ID(1,2,3): ";
        cin>>input;
        while(input!=1&&input!=2&&input!=3)
        {
            cout<<"Enter Number error!!! \n";
            cin>>input;
        }
        pBase=Base::Create(input);
    }
    ~User()
    {
        if(pBase)
        {
            delete pBase;
            pBase=nullptr;
        }
    }

    void Action()
    {
        Base* pNewBase=pBase->Clone();
        pNewBase->ChangeAttributes();
        delete pNewBase;
    }
private:
    Base* pBase;
};

int main()
{
    User* user=new User();
    user->Action();
    delete user;
}