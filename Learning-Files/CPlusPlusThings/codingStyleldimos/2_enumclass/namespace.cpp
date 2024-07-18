#include <iostream>

using namespace std;

namespace EntityType
{
    enum Enum
    {
        Ground=0,
        Human,
        Aerial,
        Total
    };
}

void foo(EntityType::Enum entityType)
{
    if(entityType==EntityType::Ground)
    {

    }
}

enum class EntityType1
{
    Ground=0,
    Human,
    Aerial,
    Total 
};

void foo(EntityType1 entityType)
{
    if(entityType==EntityType1::Ground)
    {

    }
}

int main()
{
    return 0;
}