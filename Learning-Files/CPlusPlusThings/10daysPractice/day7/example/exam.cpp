#include <iostream>
#include <cstring>

using namespace std;

class String
{
public:
    String(const char* str="");
    ~String();
    String& operator=(const String& other);
    String& operator+=(const String& other);
    
    bool operator==(const String& other) const;
    bool operator!=(const String& other) const;

    char operator[](int index) const;

private:
    friend ostream& operator<<(ostream& os,const String& str)
    {
        return os<<str._sPtr;
    }
private:
    char* _sPtr;
    int _length;
};

String::String(const char* str)
{
    _length=strlen(str);
    _sPtr=new char[_length+1];
    strcpy(_sPtr,str);
}

String& String::operator=(const String& other)
{
    _length=other._length;
    _sPtr=new char[_length+1];
    strcpy(_sPtr,other._sPtr);
    return *this;
}

String::~String()
{
    delete _sPtr;
}


String& String::operator+=(const String& other)
{
    _length+=other._length;
    char* tempPtr=_sPtr;
    _sPtr=new char[_length+1];
    strcpy(_sPtr,tempPtr);
    strcat(_sPtr,other._sPtr);
    delete[] tempPtr;
    return *this;
}

bool String::operator==(const String& other) const
{
    return strcmp(_sPtr,other._sPtr)==0;
}

bool String::operator!=(const String& other) const
{
    return *this==other;
}

char String::operator[](int index) const
{
    return _sPtr[index];
}