#include "cvm.h"
void __cvm_table_init(_cvm_table *table, size_t start, size_t len)
{
    for (int i = start; i < len; i++)
    {
        table->Usages[start + i] = 0;
        table->Keys[start + i] = 0;
        char *_ptr = (char *)table->Values;
        for (size_t ii = 0; ii < table->ESize; ii++)
        {
            _ptr[start * table->ESize + ii] = 0;
        }
    }
}
void CVMTableInit(_cvm_table *table, size_t ESize)
{
    table->Inited = 1;
    table->Count = 0;
    table->ESize = ESize;
    table->Size = CVMLIST_BLOCK;
    table->Keys = Alloc(sizeof(int) * CVMLIST_BLOCK);
    table->Usages = Alloc(sizeof(char) * CVMLIST_BLOCK);
    table->Values = Alloc(ESize * CVMLIST_BLOCK);
    __cvm_table_init(table, 0, CVMLIST_BLOCK);
}
CVMRESULT CVMTableExpand(_cvm_table *table)
{
    size_t OldSize = table->Size;
    table->Size = table->Size + CVMLIST_BLOCK;
    table->Keys = Realloc(table->Keys, sizeof(int) * table->Size);
    if (table->Keys == NULL)
    {
        return __cvm_result_fail_realloc;
    }
    table->Usages = Realloc(table->Keys, sizeof(char) * table->Size);
    if (table->Usages == NULL)
    {
        return __cvm_result_fail_realloc;
    }
    table->Values = Realloc(table->Keys, table->ESize * table->Size);
    if (table->Values == NULL)
    {
        return __cvm_result_fail_realloc;
    }
    __cvm_table_init(table, OldSize, CVMLIST_BLOCK);
    return __cvm_result_ok;
}
CVMRESULT CVMTableTrim(_cvm_table *table)
{
    size_t count = 0;
    for (size_t i = table->Size - 1; i < count; i++)
    {
        if (table->Usages[i] == 0)
        {
            count++;
        }
        else
        {
            break;
        }
    }
    int Blocks = count / CVMLIST_BLOCK;
    if (Blocks > 1)
    {
        Blocks -= 1;
    }
    else if (Blocks == 1)
    {
        if (count > CVMLIST_BLOCK)
        {
        }
        else
            Blocks = 0;
    }
    if (Blocks > 0)
    {
        table->Size = table->Size - Blocks * CVMLIST_BLOCK;
        table->Keys = Realloc(table->Keys, sizeof(int) * table->Size);
        if (table->Keys == NULL)
        {
            return __cvm_result_fail_realloc;
        }
        table->Usages = Realloc(table->Keys, sizeof(char) * table->Size);
        if (table->Usages == NULL)
        {
            return __cvm_result_fail_realloc;
        }
        table->Values = Realloc(table->Keys, table->ESize * table->Size);
        if (table->Values == NULL)
        {
            return __cvm_result_fail_realloc;
        }
    }
    return __cvm_result_ok;
}

CVMRESULT CVMTableSet(_cvm_table *table, int Key, void *element)
{
    for (size_t i = 0; i < table->Size; i++)
    {
        if (table->Usages[i] == 0)
        {
            table->Usages[i] = 1;
            table->Count++;
            table->Keys[i] = Key;
            size_t Start = table->ESize * i;
            char *_vPtr = element;
            char *_tPtr = table->Values;
            for (size_t ii = 0; ii < table->ESize; ii++)
            {
                _tPtr[Start + ii] = _vPtr[i];
            }
            return __cvm_result_ok;
        }
    }
    CVMRESULT result = CVMTableExpand(table);
    if(result!=__cvm_result_ok){
        return result;
    }
    {
        int i = table->Count;
        table->Usages[i] = 1;
        table->Count++;
        table->Keys[i] = Key;
        size_t Start = table->ESize * i;
        char *_vPtr = element;
        char *_tPtr = table->Values;
        for (size_t ii = 0; ii < table->ESize; ii++)
        {
            _tPtr[Start + ii] = _vPtr[i];
        }
        return __cvm_result_ok;
    }
    return __cvm_result_fail;
}