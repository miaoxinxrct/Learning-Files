#include <iostream>

using namespace std;

class Complex
{
public:
    Complex(double r=0,double i=0):_r(r),_i(i){}
    Complex& operator+(const Complex& other);
    Complex& operator-(const Complex& other);
    Complex& operator*(const Complex& other);
    Complex& operator/(const Complex& other);
    void display() const;
private:
    double _r;
    double _i;
};

Complex& Complex::operator+(const Complex& other)
{
    return Complex(_r+other._r,_i+other._i);
}

Complex& Complex::operator-(const Complex& other)
{
    return Complex(_r-other._r,_i-other._r);
}

Complex& Complex::operator*(const Complex& other)
{
    Complex t;
    t._r=_r*other._r-_i*other._i;
    t._i=_r*other._r+_i*other._i;
    return t;
}

Complex& Complex::operator/(const Complex& other)
{
    Complex t;
    double x;
    x=1/(other._r*other._r+other._i*other._i);
    t._r=x*(_r*other._r+_i*other._i);
    t._i=x*(_i*other._r-_r*other._i);
    return t;
}

void Complex::display() const
{
    cout<<_r;
    if(_i>0)
        cout<<"+";
    if(_i!=0)
        cout<<_i<<"i\n";
}

int main()
{
      Complex c1(1, 2), c2(3, 4), c3, c4, c5, c6;
  Complex a, b(2, 3);
  a = b + 2; //ÕýÈ·
  // a=2+b;       			//´íÎó
  a.display();
  c3 = c1 + c2;
  c4 = c1 - c2;
  c5 = c1 * c2;
  c6 = c1 / c2;
  c1.display();
  c2.display();
  c3.display();
  c4.display();
  c5.display();
  c6.display();
  
  return 0;
}