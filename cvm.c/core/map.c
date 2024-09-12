#include "map.h"
void CVMMap_Init(CVMMap map, int MK, int MV, int KSize, int VSize, CVMMapComp KComp, CVMMapComp VComp)
{
    map->IsInit = 1;
    map->MagicK = MK;
    map->MagicV = MV;
    map->Ks = Allocate(CVMLIST_BLOCK * KSize);
    map->KCount = 0;
    map->KLSize = CVMLIST_BLOCK;
    map->KSize = KSize;
    map->Vs = Allocate(CVMLIST_BLOCK * VSize);
    map->VCount = 0;
    map->VLSize = CVMLIST_BLOCK;
    map->VSize = VSize;
    map->KComparer = KComp;
    map->VComparer = VComp;
    map->Usages = Allocate(CVMLIST_BLOCK * sizeof(char));
}