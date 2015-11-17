#include <iostream>

class BaseClass
{
public:
	int num;
	BaseClass(int num)
		: num(num)
	{}
};

class ChildClass : public BaseClass
{
public:
	int trk;
	ChildClass(int num, int trk)
		: BaseClass(num), trk(trk)
	{}
};

BaseClass* v = new ChildClass(11, 22);
ChildClass* w = new ChildClass(33, 44);
BaseClass x = ChildClass(55, 66);
ChildClass y = ChildClass(77, 88);

void some_Base_Function(const BaseClass& in)
{
	printf("(some_Base_Function)(BaseClass)   0x%016X::num(%d)\r\n", (unsigned long)&in, in.num);
}

void some_Base_Function(const ChildClass& in)
{
	printf("(some_Base_Function)(ChildClass)  0x%016X::num(%d)\r\n", (unsigned long)&in, in.num);
	printf("(some_Base_Function)(ChildClass)  0x%016X::trk(%d)\r\n", (unsigned long)&in, in.trk);
}

void some_Child_Function(const BaseClass& in)
{
	printf("(some_Child_Function)(BaseClass)  0x%016X::num(%d)\r\n", (unsigned long)&in, in.num);
}

void some_Child_Function(const ChildClass& in)
{
	printf("(some_Child_Function)(ChildClass) 0x%016X::num(%d)\r\n", (unsigned long)&in, in.num);
	printf("(some_Child_Function)(ChildClass) 0x%016X::trk(%d)\r\n", (unsigned long)&in, in.trk);
}

class Bar
{
private:
	int i;
public:
	Bar()
		: i(1337)
	{ }
};

class Foo
{
private:
	Bar* bar;
public:
	Foo()
		: bar(new Bar())
	{}
	~Foo()
	{
		delete bar;
		bar = nullptr;
	}

	Bar* getBar() const
	{
		return bar;
	}
};

class iFoo
{
private:
	int bar;
public:
	iFoo()
		: bar(99)
	{}

	const int* getBar() const
	{
		return &bar;
	}
};

int main()
{
	/*some_Base_Function (*v);
	some_Child_Function(*v);
	some_Base_Function (*w);
	some_Child_Function(*w);
	some_Base_Function(x);
	some_Child_Function(x);
	some_Base_Function(y);
	some_Child_Function(y);*/

	iFoo f;
	const int* b = f.getBar();
	while (true);
	return 0;
}