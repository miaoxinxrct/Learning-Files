#ifndef VECTOR_H
#define VECTOR_H

class Vector
{
public:
    Vector(int s);
    Vector(std::initializer_list<double> lst);
    ~Vector();
    double& operator[] (int i){return elem[i];}
    int size(){return sz;}

private:
    int sz;
    double* elem;
};

class Container
{
public:
    virtual double& operator[](int)=0;
    virtual int size() const=0;
    virtual ~Container(){}
};

class Vector_container:public Container
{
public:
    Vector_container(int s):v(s){}
    ~Vector_container(){}

    double& operator[](int i) override{return v[i];}
    int size() const override{return v.size();}

private:
    Vector v;
};

class Shape
{
public:
    virtual Point center() const=0;
    virtual void move(Point to)=0;

    virtual void draw() const=0;
    virtual void rotate(int angle)=0;
    ~Shape();    
}

void rotate_all(Vector<Shape*>& v,int angle)
{
    for(auto p:v)
        p->rotate(angle);
}

class Circle:public Shape
{
public:
    Circle():x(0,0),r(0){}
    Circle(Point p,int rad);
    Point center() const override{ return x;}
    void move(Point to) override{x=to;}

    void draw() const override;
    void rotate(int) override{}
private:
    Point x;
    int r;
}

class Smiley:public Circle
{
public:
    Smiley(Point p,int rad):Circle(p,rad),mouth(nullptr){}
    ~Smiley()
    {
        delete mouth;
        for(auto p:eyes)
            delete p;
    }

    void move(Point to) override;
    void draw() const override;
    void rotate(int) override;

    void addEyes(Shape* s)
    {
        eyes.push_back(s);
    }
    void setMouth(Shape* s);
    virtual void wink(int i);

private:
    vector<Shape*> eyes;
    Shape* mouth;
}

#endif