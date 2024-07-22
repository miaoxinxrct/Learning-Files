#ifndef _JJALLOCATOR_H
#define _JJALLOCATOR_H

#include <new> //for placement new
#include <cstddef> //for ptrdiff_t,size_t
#include <cstdlib> //for exit(1)
#include <climits> //for UINT_MAX
#include <iostream> //for cerr

namespace JJ
{
    template<class T>
    inline T* _allocate(ptrdiff_t size,T* t)
    {
        set_new_handle(0);
        T* tmp=(T*)(::operator new((size_t)(size*sizeof(T))));
        if(tmp==0)
        {
            cerr<<"out of memory\n";
            exit(1);
        }
        return tmp;
    }

    template <class T>
    inline void _deallocate(T* buffer)
    {
        ::operator delete(buffer);
    }

    template <class T1,class T2>
    inline void _construct(T1* p,const T2& value)
    {
        new(p) T1(value);//placement new. invoke ctor of T1
    }

    template <class T>
    inline void _destory(T* ptr)
    {
        ptr->~T();
    }
}


#endif