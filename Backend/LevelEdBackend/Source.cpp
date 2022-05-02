#define ExternalFunc _declspec(dllexport)

extern "C" {
	ExternalFunc int test()
	{
		return 69;
	}
}