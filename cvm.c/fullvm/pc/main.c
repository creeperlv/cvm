#include "pc_impl.h"
void WriteInt(int d)
{
    int rev = 0;
    while (d != 0)
    {
        rev = rev * 10 + d % 10;
        d /= 10;
    }
    char buf[2];
    buf[0] = 0;
    buf[1] = 0;
    while (rev != 0)
    {
        buf[0] = rev % 10 + '0';
        write(__cvm__stdout, buf, 1);
        rev /= 10;
    }
}
void WriteULong(unsigned long d)
{
    unsigned long rev = 0;
    while (d != 0)
    {
        rev = rev * 10 + d % 10;
        d /= 10;
    }
    char buf[2];
    buf[0] = 0;
    buf[1] = 0;
    while (rev != 0)
    {
        buf[0] = rev % 10 + '0';
        write(__cvm__stdout, buf, 1);
        rev /= 10;
    }
}
int StrLen(char *str)
{
    int L = 0;
    while (1)
    {
        if (str[L] == 0)
            return L;
        L++;
    }
}
void WriteString(char *str)
{
    write(__cvm__stdout, str, StrLen(str));
}
void WriteData(char *field, unsigned long v)
{
    WriteString(field);
    WriteULong(v);
    WriteString("\n");
}
int main()
{
    WriteData("GUID:\t\t", sizeof(cvmGuid));
    WriteData("GUID2:\t\t", sizeof(cvmGuid2));
    WriteData("GPTHeader:\t", sizeof(cvmGPTHeader));
    WriteData("GPTEntry:\t", sizeof(cvmGPTEntery));
    WriteData("LBABlock:\t", sizeof(cvmLBABlock));
    EnterVideoMode();
    return 0;
}