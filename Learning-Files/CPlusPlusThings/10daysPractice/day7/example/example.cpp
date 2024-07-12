#include <cstring>
#include <iostream>

using namespace std;

class String
{
public:
    String(const char* str="");
    ~String();

    const String& operator=(const String& other)
    {
        _length=other._length;
        strcpy(_sPtr,other._sPtr);
        return *this;
    }

    const String& operator+=(const String& other);

    bool operator==(const String& other)const;
    bool operator!=(const String& other)const;
    bool operator!()const;
    bool operator<(const String& other)const;
    bool operator>(const String& other)const;
    bool operator>=(const String& other)const;
    bool operator[](int sub);

private:
    int _length;
    char* _sPtr;
};

String::String(const char* str)
{
    _length=strlen(str);
    _sPtr=new char[strlen(str)+1];
    strcpy(_sPtr,str);
}

String::~String()
{
    delete[] _sPtr;
}

const String& String::operator+=(const String& other)
{
    _length+=other._length;
    char* temp=_sPtr;
    _sPtr=new char[_length+1];
    strcpy(_sPtr,temp);
    strcat(_sPtr,other._sPtr);
    delete[] temp;
    return *this;
}

bool String::operator==(const String& other) const
{
    return strcmp(_sPtr,other._sPtr)==0;
}

bool String::operator!=(const String& other) const
{
    return !((*this)==other);
}

bool String::operator!() const
{
    return _length==0;
}

bool String::operator<(const String& other) const
{
    return strcmp(_sPtr,other._sPtr)<0;
}

bool String::operator>(const String& other) const
{
    return other<(*this);
}

bool String::operator>=(const String& other) const
{
    return !((*this)<other);
}

char& String::operator[](int sub)
{
    return (_sPtr[sub]);
}
