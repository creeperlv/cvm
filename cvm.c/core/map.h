#if !defined(__cvm_core_map__)
#define __cvm_core_map__
#include "base.h"
typedef int (*CVMMapComp)(void *, void *);
typedef struct __cvm_core_map
{
    char IsInit;
    int MagicK;
    int MagicV;
    void *Ks;
    char* Usages;
    int KCount;
    int KSize;
    int KLSize;
    void *Vs;
    int VCount;
    int VSize;
    int VLSize;
    CVMMapComp KComparer;
    CVMMapComp VComparer;
} __CVMMap;
typedef __CVMMap *CVMMap;
void CVMMap_Init(CVMMap map, int MK, int MV, int KSize, int VSize, CVMMapComp KComp, CVMMapComp VComp);
int CVMMap_AddKey(CVMMap map, void *K, void *V);
int CVMMap_RemoveKey(CVMMap map, void *K);
int CVMMap_ContainsKey(CVMMap map, void *K);
/**
 * map: Map
 * K: Value to get
 * V: Ptr to set value to
 * Return:
 *  0 - Success
 *  Other - Fail
 */
int CVMMap_GetValue(CVMMap map, void *K, void **V);
#endif // __cvm_core_map__
