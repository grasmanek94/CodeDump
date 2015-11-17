#include <stdio.h>

int main(void) {
	char destpos = 0;
	char sourcpos = 2;
	char shiftby = destpos - sourcpos;
	char input = 4;
	char output = shiftby >= 0 ? input << shiftby : input >> -shiftby;
	printf("%d\n", output);
	return 0;
}
